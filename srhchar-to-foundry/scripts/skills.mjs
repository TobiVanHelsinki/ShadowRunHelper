import { ThingType } from './srh-reader.mjs';
import { clean, escapeHtml, paragraphs } from './text.mjs';

/** German names and common variants of the skills in the shadowrun5e compendium. */
const GERMAN_SKILLS = {
  'projektilwaffen': 'Archery',
  'bogenschießen': 'Archery',
  'schnellfeuerwaffen': 'Automatics',
  'schnellfeuerwaffe': 'Automatics',
  'klingenwaffen': 'Blades',
  'knüppel': 'Clubs',
  'schwere waffen': 'Heavy Weapons',
  'gewehre': 'Longarms',
  'gewehr': 'Longarms',
  'pistolen': 'Pistols',
  'pistole': 'Pistols',
  'wurfwaffen': 'Throwing Weapons',
  'waffenloser kampf': 'Unarmed Combat',
  'exotische nahkampfwaffe': 'Exotic Melee Weapon',
  'exotische nahkampfwaffen': 'Exotic Melee Weapon',
  'exotische fernkampfwaffe': 'Exotic Ranged Weapon',
  'exotische fernkampfwaffen': 'Exotic Ranged Weapon',
  'verkleiden': 'Disguise',
  'tauchen': 'Diving',
  'entfesseln': 'Escape Artist',
  'fliegen': 'Flight',
  'freifall': 'Free Fall',
  'akrobatik': 'Gymnastics',
  'acrobatik': 'Gymnastics',
  'fingerfertigkeit': 'Palming',
  'wahrnehmung': 'Perception',
  'laufen': 'Running',
  'schleichen': 'Sneaking',
  'überleben': 'Survival',
  'survival': 'Survival',
  'schwimmen': 'Swimming',
  'spurenlesen': 'Tracking',
  'überreden': 'Con',
  'überzeugen': 'Con',
  'gebräuche': 'Etiquette',
  'verkörperung': 'Impersonation',
  'unterricht': 'Instruction',
  'einschüchtern': 'Intimidation',
  'führung': 'Leadership',
  'verhandlung': 'Negotiation',
  'verhandeln': 'Negotiation',
  'vorführung': 'Performance',
  'alchemie': 'Alchemy',
  'arkana': 'Arcana',
  'fokusherstellung': 'Artificing',
  'askennen': 'Assensing',
  'astralkampf': 'Astral Combat',
  'verbannen': 'Banishing',
  'binden': 'Binding',
  'antimagie': 'Counterspelling',
  'entzaubern': 'Disenchanting',
  'ritualzauberei': 'Ritual Spellcasting',
  'spruchzauberei': 'Spellcasting',
  'herbeirufen': 'Summoning',
  'kompilieren': 'Compiling',
  'dekompilieren': 'Decompiling',
  'registrieren': 'Registering',
  'luftfahrtmechanik': 'Aeronautics Mechanic',
  'fahrzeugmechanik': 'Automotive Mechanic',
  'industriemechanik': 'Industrial Mechanic',
  'seefahrtmechanik': 'Nautical Mechanic',
  'tierführung': 'Animal Handling',
  'waffenbau': 'Armorer',
  'handwerk': 'Artisan',
  'kunsthandwerk': 'Artisan',
  'biotechnologie': 'Biotechnology',
  'chemie': 'Chemistry',
  'computer': 'Computer',
  'matrixkampf': 'Cybercombat',
  'kybernetik': 'Cybertechnology',
  'sprengstoffe': 'Demolitions',
  'elektronische kriegsführung': 'Electronic Warfare',
  'erste hilfe': 'First Aid',
  'fälschen': 'Forgery',
  'hacking': 'Hacking',
  'hardware': 'Hardware',
  'schlosser': 'Locksmith',
  'medizin': 'Medicine',
  'navigation': 'Navigation',
  'software': 'Software',
  'geschütze': 'Gunnery',
  'raumfahrzeuge': 'Pilot Aerospace',
  'flugzeuge': 'Pilot Aircraft',
  'flugzeuge (flugdrohne)': 'Pilot Aircraft',
  'luftfahrzeuge': 'Pilot Aircraft',
  'läufer': 'Pilot Walker',
  'bodenfahrzeuge': 'Pilot Ground Craft',
  'exotisches fahrzeug': 'Pilot Exotic Vehicle',
  'exotische fahrzeuge': 'Pilot Exotic Vehicle',
  'schiffe': 'Pilot Watercraft',
  'wasserfahrzeuge': 'Pilot Watercraft',
};

/** Accepts German names and the English compendium names. */
const ACTIVE_SKILLS = new Map(Object.entries(GERMAN_SKILLS));
for (const english of Object.values(GERMAN_SKILLS)) ACTIVE_SKILLS.set(english.toLowerCase(), english);

const SKILL_GROUPS = new Set([
  'athletik', 'beschwören', 'biotech', 'cracken', 'einfluss', 'elektronik', 'feuerwaffen', 'heimlichkeit',
  'hexerei', 'mechanik', 'nahkampf', 'natur', 'schauspielerei', 'tasken', 'verzaubern',
]);

/** Same rule as SkillNamingFlow.nameToKey in the shadowrun5e system. */
export function nameToKey(name) {
  return name.trim().replace(/[\s-]+/g, '_').toLowerCase();
}

/** Characters mark native languages with a very high rating or an M or MS after the name. */
function isNativeLanguage(name, rating) {
  return rating >= 9 || /[\s(]MS?\)?$/.test(name);
}

function knowledgeTypeOf(typ) {
  if (/stra(ß|ss)e|street/.test(typ)) return 'street';
  if (/akadem/.test(typ)) return 'academic';
  if (/beruf|prof/.test(typ)) return 'professional';
  if (/hobby|interess|freizeit/.test(typ)) return 'interests';
  if (/wissen|knowledge/.test(typ)) return 'academic';
  return null;
}

function createEntry(name, typ, linkedAttribute, warn) {
  const lowerTyp = typ.toLowerCase();
  const base = { rating: 0, specializations: [], description: '', knowledgeType: 'academic' };

  if (/sprach/.test(lowerTyp)) return { ...base, name, category: 'language', attribute: 'intuition' };

  const packName = ACTIVE_SKILLS.get(name.toLowerCase());
  // A type like "Wissen Natur" names a skill group, so the row is no knowledge skill.
  const namesGroup = [...SKILL_GROUPS].some((group) => lowerTyp.includes(group));

  const knowledgeType = knowledgeTypeOf(lowerTyp);
  if (knowledgeType && !(packName && namesGroup)) {
    const attribute = linkedAttribute || (knowledgeType === 'street' || knowledgeType === 'interests' ? 'intuition' : 'logic');
    return { ...base, name, category: 'knowledge', knowledgeType, attribute };
  }

  if (packName) return { ...base, name: packName, packName, category: 'active', attribute: '' };

  // The system has one skill for exotic ranged weapons, the weapon becomes its specialization.
  if (/exotisch/.test(lowerTyp)) {
    const exotic = 'Exotic Ranged Weapon';
    return { ...base, name: exotic, packName: exotic, category: 'active', attribute: 'agility', specializations: [name] };
  }

  if (linkedAttribute) return { ...base, name, category: 'active', attribute: linkedAttribute };

  if (!typ) {
    warn(`Fertigkeit „${name}“ hat weder Typ noch Attribut und wurde als Wissensfertigkeit angelegt.`);
    return { ...base, name, category: 'knowledge', attribute: 'logic' };
  }

  warn(`Fertigkeit „${name}“ ist nicht im System bekannt und wurde ohne Attribut angelegt.`);
  return { ...base, name, category: 'active', attribute: '' };
}

function specializationOf(char, thing, name, typ) {
  const byName = /^(.+?)\s+spez\.?\s+(.+)$/i.exec(name);
  if (byName) return { parentName: byName[1], specName: byName[2] };

  const byTyp = /^(.+?)[\s-]*(?:spezialisierung|sepzialisierung|spez\.?)$/i.exec(typ);
  if (byTyp) return { parentName: byTyp[1], specName: name };

  const parent = char.linksOf(char.prop(thing, 'Value'))
    .find((link) => link.name === 'Value' && link.owner && char.typeOf(link.owner) === ThingType.Fertigkeit);
  return parent ? { parentThing: parent.owner, specName: name } : null;
}

/**
 * Reads all skills of a character. Group rows are dissolved into the ratings of their
 * skills and specializations become part of their parent skill.
 * @returns wanted skills, and for each skill row the key that actions refer to
 */
export function buildSkills(char, attributeOf, warn) {
  const rows = char.things.filter((t) => char.typeOf(t) === ThingType.Fertigkeit && clean(t.Bezeichner));

  const groupRatings = new Map();
  const skillRows = [];
  for (const thing of rows) {
    const lowerName = clean(thing.Bezeichner).toLowerCase();
    const typ = clean(thing.Typ);
    if (SKILL_GROUPS.has(lowerName) && (!typ || /gruppe|aktion/i.test(typ))) {
      groupRatings.set(lowerName, Math.max(0, Math.round(char.prop(thing, 'Value').base)));
    } else {
      skillRows.push(thing);
    }
  }
  const skillRowSet = new Set(skillRows);

  const entries = new Map();
  const upsert = (entry) => {
    const key = `${entry.category}|${entry.name.toLowerCase()}`;
    if (!entries.has(key)) return entries.set(key, entry).get(key);
    const existing = entries.get(key);
    existing.specializations.push(...entry.specializations);
    return existing;
  };

  const skillOf = new Map();
  const specializations = [];

  const addSkillRow = (thing) => {
    const typ = clean(thing.Typ);
    const name = clean(thing.Bezeichner);
    const prop = char.prop(thing, 'Value');
    const links = char.linksOf(prop);
    const linkedAttribute = links.map((link) => attributeOf.get(link.owner)).find(Boolean) ?? '';
    const entry = upsert(createEntry(name, typ, linkedAttribute, warn));

    // Links to attributes form the dice pool in the app, so the rating is the base value alone.
    const ownRating = Math.max(0, Math.round(prop.base));
    entry.rating = Math.max(entry.rating, ownRating, groupRatings.get(typ.toLowerCase()) ?? 0);
    entry.description ||= paragraphs(thing.Notiz).join('');
    if (entry.category === 'language') entry.isNative = isNativeLanguage(name, ownRating);

    const bonuses = links
      .filter((link) => !attributeOf.has(link.owner) && char.total(link) !== 0)
      .map((link) => `${clean(link.owner?.Bezeichner) || '?'} ${char.total(link) > 0 ? '+' : ''}${char.total(link)}`);
    if (bonuses.length) {
      const text = bonuses.join(', ');
      entry.description += `<p>Boni aus SRHChar: ${escapeHtml(text)}</p>`;
      warn(`Fertigkeit „${name}“: Boni (${text}) sind nicht in der Stufe enthalten und stehen in der Beschreibung.`);
    }
    skillOf.set(thing, { key: nameToKey(entry.name), spec: false });
  };

  for (const thing of skillRows) {
    const name = clean(thing.Bezeichner);
    const spec = specializationOf(char, thing, name, clean(thing.Typ));
    const usableParent = spec && (!spec.parentThing || skillRowSet.has(spec.parentThing));
    if (usableParent) specializations.push({ thing, ...spec });
    else addSkillRow(thing);
  }

  for (const { thing, parentName, parentThing, specName } of specializations) {
    if (parentThing && !skillOf.has(parentThing)) {
      // The parent is a specialization itself, so this row stays a skill of its own.
      addSkillRow(thing);
      continue;
    }
    const parentKey = parentThing ? skillOf.get(parentThing).key : nameToKey(ACTIVE_SKILLS.get(parentName.toLowerCase()) ?? parentName);
    const parent = [...entries.values()].find((entry) => nameToKey(entry.name) === parentKey)
      ?? upsert(createEntry(parentName, '', '', warn));
    parent.specializations.push(specName);
    // Some characters keep the rating of the parent skill in an action of the same name.
    const parentAction = char.linksOf(char.prop(thing, 'Value')).find((link) => char.typeOf(link.owner) === ThingType.Handlung);
    if (parentAction) parent.rating = Math.max(parent.rating, Math.round(parentAction.base));
    skillOf.set(thing, { key: nameToKey(parent.name), spec: true });
  }

  for (const [group, rating] of groupRatings) {
    const used = skillRows.some((thing) => clean(thing.Typ).toLowerCase() === group);
    if (rating > 0 && !used) warn(`Fertigkeitsgruppe „${group}“ (Stufe ${rating}) hat keine zugeordneten Fertigkeiten und wurde nicht übernommen.`);
  }

  return { skills: [...entries.values()], skillOf };
}

/** Applies the wanted skills to the skills of the template and adds skills the template doesn't have. */
export function mergeSkills(templateSkills, wantedSkills) {
  const items = structuredClone(templateSkills);
  // By key, because names can differ in spelling, for example Free Fall and Free-Fall.
  const find = (name, category) => items.find((item) => nameToKey(item.name) === nameToKey(name)
    && item.system.skill.category === category);

  for (const wanted of wantedSkills) {
    let item = find(wanted.name, wanted.category);
    if (!item) {
      item = {
        name: wanted.name,
        type: 'skill',
        system: {
          type: 'skill',
          skill: { category: wanted.category, attribute: wanted.attribute, defaulting: false },
        },
      };
      items.push(item);
    }
    const { skill } = item.system;
    skill.rating = wanted.rating;
    if (wanted.category === 'knowledge') skill.knowledgeType = wanted.knowledgeType;
    if (wanted.isNative) skill.language = { isNative: true };
    skill.specializations = wanted.specializations.map((name) => ({ name }));
    if (wanted.description) item.system.description = { ...item.system.description, value: wanted.description };
  }
  return items;
}
