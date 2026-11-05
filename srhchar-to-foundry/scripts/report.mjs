import { ThingType } from './srh-reader.mjs';

const TYPE_NAMES = Object.fromEntries(Object.entries(ThingType).map(([name, id]) => [id, name]));

/** Fields of the file that end up in the result. Used for all thing types alike. */
const USED_FIELDS = new Set([
  'Against', 'Aktiv', 'Alias', 'Angriff', 'Anzahl', 'Augenfarbe', 'Auswirkung', 'Auswirkungen', 'Beschleunigung',
  'Besitz', 'Bezeichner', 'BirthDate', 'Capacity', 'Char_Typ', 'DK', 'Datenverarbeitung', 'Dauer', 'Dienste', 'Duration',
  'Echos', 'Edge_Aktuell', 'Edge_Gesamt', 'Einfluss', 'Entzug', 'Essenz', 'Fading', 'Firewall', 'Geb_Reg', 'Geburtsdatum',
  'Geschlecht', 'Geschwindigkeit', 'Gewicht', 'Groesse', 'Haarfarbe', 'Handling', 'Hautfarbe', 'Kapazitaet', 'Karma_Aktuell',
  'Karma_Gesamt', 'Kontostand', 'Lebesstil', 'LifeStyleCost', 'Limit', 'Loyal', 'MetaTyp', 'MetaTyp_sub', 'Metamagie',
  'Modi', 'Modifications', 'Notiz', 'Notizen', 'Option', 'Optionen', 'Panzerung', 'Paragon', 'Pilot', 'Precision',
  'Programmanzahl', 'Prominenz', 'RK', 'Reichweite', 'Rumpf', 'Runs', 'SchadenTyp', 'Schaden_G', 'Schaden_G_max_mod',
  'Schaden_K', 'Schaden_K_max_mod', 'SchlechterRuf', 'Schleicher', 'Schutzpatron', 'Sensor', 'Sitze', 'Strassenruf',
  'Target', 'Text', 'Typ', 'Value', 'Zusammenfassung', 'Zusatz',
  // Older names of the fields above, see LEGACY_PROPS in srh-reader.mjs.
  'Wert', 'Grenze', 'Gegen', 'PB', 'Praezision', 'Rueckstoss',
  'LinkedThings', 'WertZusammensetzung', 'PoolZusammensetzung', 'GrenzeZusammensetzung', 'GegenZusammensetzung',
]);

/** Fields that only concern the app itself. */
const APP_FIELDS = new Set(['ThingType', 'Order', 'IsFavorite', 'FavoriteIndex']);

function isFilled(value) {
  if (value === null || value === undefined || value === '' || value === 0 || value === false) return false;
  if (typeof value !== 'object') return true;
  if ('BaseValue' in value) return Boolean(value.BaseValue) || (value.Connected?.$values?.length ?? 0) > 0;
  if (Array.isArray(value)) return value.length > 0;
  if ('$values' in value) return value.$values.length > 0;
  return Object.keys(value).some((key) => !key.startsWith('$'));
}

function sample(value) {
  const text = typeof value === 'object' ? String(value.BaseValue ?? JSON.stringify(value)) : String(value);
  return text.length > 40 ? `${text.slice(0, 40)}…` : text;
}

/** Filled fields of the file that the converter doesn't use, one row per field and type. */
export function unusedFields(char) {
  const rows = new Map();
  const collect = (owner, source) => {
    for (const [field, value] of Object.entries(source)) {
      if (field.startsWith('$') || USED_FIELDS.has(field) || APP_FIELDS.has(field) || !isFilled(value)) continue;
      const key = `${owner}|${field}`;
      if (!rows.has(key)) rows.set(key, { owner, field, count: 0, example: sample(value) });
      rows.get(key).count += 1;
    }
  };
  collect('Person', char.person);
  for (const thing of char.things) collect(TYPE_NAMES[char.typeOf(thing)] ?? `Typ ${char.typeOf(thing)}`, thing);
  return [...rows.values()].sort((a, b) => a.owner.localeCompare(b.owner) || a.field.localeCompare(b.field));
}

export const hintCount = (count) => `${count} ${count === 1 ? 'Hinweis' : 'Hinweise'}`;

const cell = (value) => String(value).replace(/\|/g, '\\|').replace(/\s+/g, ' ');

function table(header, rows) {
  return [`| ${header.join(' | ')} |`, `| ${header.map(() => '---').join(' | ')} |`,
    ...rows.map((row) => `| ${row.map(cell).join(' | ')} |`)].join('\n');
}

function list(lines, empty) {
  return lines.length ? lines.map((line) => `- ${line}`).join('\n') : empty;
}

function summaryRows({ system }, skillCount, template) {
  const attribute = (key) => system.attributes[key]?.base ?? 0;
  return [
    ['Metatyp', system.metatype],
    ['Art', system.special],
    ['Nuyen', system.nuyen],
    ['Karma', `${system.karma.value} von ${system.karma.max}`],
    ['Straßenruf', system.street_cred],
    ['Attribute', `KON ${attribute('body')}, GES ${attribute('agility')}, REA ${attribute('reaction')}, STR ${attribute('strength')}, `
      + `WIL ${attribute('willpower')}, LOG ${attribute('logic')}, INT ${attribute('intuition')}, CHA ${attribute('charisma')}`],
    ['Edge', attribute('edge')],
    ['Magie, Resonanz', `${attribute('magic')}, ${attribute('resonance')}`],
    ['Fertigkeiten', `${skillCount}, davon ${template.items.length} aus der Vorlage`],
  ];
}

/** The report for a converted file. */
export function successReport({ file, char, converted, character, vehicles, fileNames, template }) {
  const skills = character.items.filter((item) => item.type === 'skill').length;
  const counts = new Map();
  for (const item of converted.items) counts.set(item.type, (counts.get(item.type) ?? 0) + 1);
  const unused = unusedFields(char);
  const stats = template._stats;

  return `# Bericht: ${character.name}

**Status: umgewandelt, ${hintCount(converted.warnings.length)}, ${unused.length} nicht übernommene Felder**

- Quelle: \`${file}\`, Dateiversion ${char.version}
- Ziel: ${stats.systemId} ${stats.systemVersion}, Foundry ${stats.coreVersion}

## Dateien für den Import

${list(fileNames.map((name) => `\`${name}\``), 'keine')}

## Übernommene Werte

${table(['Wert', character.name], summaryRows(character, skills, template))}

## Einträge

${table(['Typ', 'Anzahl'], [...[...counts].sort(), ['Fahrzeuge', vehicles.length], ['Leer- und Trennzeilen, ausgelassen', converted.skippedRows]])}

Bonus-Effekt auf Attribute: ${converted.bonusEffect ? converted.bonusEffect.sources.join('; ') : 'keiner'}

## Hinweise

${list(converted.warnings, 'Keine.')}

## Nicht übernommene Felder

Diese Felder sind in der Datei gefüllt, der Konverter verwendet sie aber nicht.

${unused.length ? table(['Bereich', 'Feld', 'Anzahl', 'Beispiel'], unused.map(({ owner, field, count, example }) => [owner, field, count, example])) : 'Keine.'}

## Immer von Hand prüfen

- Munition, Präzision und Rückstoß von Waffen, wenn sie in der App nur im Text stehen. Die Beschreibung der Waffe enthält den Text.
- Fahrer von Fahrzeugen, weil die ID des Chars erst in Foundry entsteht.
- Boni auf Fertigkeiten. Sie stehen nur in der Beschreibung und fließen nicht in die Würfelpools ein.
`;
}

/** The report for a file that could not be converted. */
export function failureReport(file, error) {
  return `# Bericht: ${file}

**Status: nicht umgewandelt**

${error.message}

Es wurden keine Dateien für den Import geschrieben.
`;
}
