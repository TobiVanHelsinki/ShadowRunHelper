import { ThingType as T } from './srh-reader.mjs';
import { buildSkills } from './skills.mjs';
import { clean, paragraphs, escapeHtml } from './text.mjs';

const ATTRIBUTES = {
  Konsti: 'body',
  Geschick: 'agility',
  Reaktion: 'reaction',
  Staerke: 'strength',
  Charisma: 'charisma',
  Logik: 'logic',
  Intuition: 'intuition',
  Willen: 'willpower',
  Magie: 'magic',
  Resonanz: 'resonance',
};

const LIMITS = { Limit_K: 'physical', Limit_G: 'mental', Limit_S: 'social' };

/** Device values that the system treats as attributes and limits. */
const MATRIX_VALUES = {
  Datenverarbeitung: 'data_processing',
  Firewall: 'firewall',
  Angriff: 'attack',
  Schleicher: 'sleaze',
};

const METATYPES = { mensch: 'human', human: 'human', elf: 'elf', elfe: 'elf', zwerg: 'dwarf', dwarf: 'dwarf', ork: 'ork', troll: 'troll' };

const round = (value) => Math.round(Number(value) || 0);
const atLeastZero = (value) => Math.max(0, round(value));
const isNumeric = (text) => /^[+-]?\d+([.,]\d+)?$/.test(text);
const firstMatch = (text, table) => table.find(([pattern]) => pattern.test(text))?.[1] ?? '';

function itemName(thing) {
  const name = clean(thing.Bezeichner) || '(ohne Name)';
  const typ = clean(thing.Typ);
  return typ && !isNumeric(typ) ? `${typ}: ${name}` : name;
}

function describe(thing, extraLines = []) {
  const zusatz = clean(thing.Zusatz).replace(/^[,+\s]+/, '');
  const lines = [zusatz, thing.Auswirkungen, thing.Auswirkung, thing.Option, thing.Modifications, ...extraLines, thing.Notiz];
  if (thing.Besitz === false) lines.unshift('Nicht im Besitz');
  return { value: lines.flatMap(paragraphs).join('') };
}

function technology(char, thing, { rating = char.own(thing, 'Value'), equipped = thing.Aktiv === true } = {}) {
  // Rows that were never filled in have 0, even worn armor and installed implants.
  return { rating: atLeastZero(rating), quantity: atLeastZero(thing.Anzahl) || 1, equipped };
}

const RANGED_WEAPON_SKILLS = [
  [/bogen|armbrust/i, 'archery'],
  [/tomahawk|tomahwk|wurf/i, 'throwing_weapons'],
  [/sturmgewehr|maschinenpistole|\bmp\b|\bsmg\b|\bsg\b|schnellfeuer|p93/i, 'automatics'],
  [/laser|plasma|flammenwerfer/i, 'exotic_ranged_weapon'],
  [/granat|rakete|geschütz|\bmg\b/i, 'heavy_weapons'],
  [/pistole|\bsp\b|\blp\b|taser|revolver/i, 'pistols'],
  [/gewehr|flinte|schrot/i, 'longarms'],
];

const MELEE_WEAPON_SKILLS = [
  [/schwert|messer|klinge|katana|dolch|axt/i, 'blades'],
  [/knüppel|stab|stock|keule/i, 'clubs'],
  [/faust|klaue|waffenlos|taser|h(ö|oe)rner|sporn|handschuh/i, 'unarmed_combat'],
];

/** Values of SR5.weaponRangeCategories. Their schema has no negative ranges, so "none" is 0 here. */
const RANGE_CATEGORIES = {
  taser: { short: 5, medium: 10, long: 15, extreme: 20 },
  flamethrower: { short: 15, medium: 20, long: 0, extreme: 0 },
  shotgunSlug: { short: 10, medium: 40, long: 80, extreme: 150 },
  assaultRifle: { short: 25, medium: 150, long: 350, extreme: 550 },
  sniperRifle: { short: 50, medium: 350, long: 800, extreme: 1500 },
  smg: { short: 10, medium: 40, long: 80, extreme: 150 },
  machinePistol: { short: 5, medium: 15, long: 30, extreme: 50 },
  holdOutPistol: { short: 5, medium: 15, long: 30, extreme: 50 },
  heavyPistol: { short: 5, medium: 20, long: 40, extreme: 60 },
  lightPistol: { short: 5, medium: 15, long: 30, extreme: 50 },
  grenadeLauncher: { short: 50, medium: 100, long: 150, extreme: 500 },
  missileLauncher: { short: 70, medium: 150, long: 450, extreme: 1500 },
  bow: { short: 1, medium: 10, long: 30, extreme: 60, attribute: 'strength' },
};

/** Only types that name one category. A plain "Pistole" or "Gewehr" stays manual. */
const WEAPON_RANGE_CATEGORIES = [
  [/taser/i, 'taser'],
  [/flammenwerfer/i, 'flamethrower'],
  [/schrot|flinte/i, 'shotgunSlug'],
  [/sturmgewehr|\bsg\b/i, 'assaultRifle'],
  [/scharfsch|sniper/i, 'sniperRifle'],
  [/maschinenpistole|\bmp\b|\bsmg\b|p93/i, 'smg'],
  [/reihenpistole/i, 'machinePistol'],
  [/hold.?out|taschenpistole/i, 'holdOutPistol'],
  [/schwere pistole|\bsp\b/i, 'heavyPistol'],
  [/leichte pistole|\blp\b/i, 'lightPistol'],
  [/granatwerfer/i, 'grenadeLauncher'],
  [/raketenwerfer/i, 'missileLauncher'],
  [/\bbogen\b/i, 'bow'],
];

function rangesOf(thing, warn) {
  const category = firstMatch(`${thing.Typ} ${thing.Bezeichner}`, WEAPON_RANGE_CATEGORIES);
  if (!category) {
    warn(`Waffe „${itemName(thing)}“: Reichweiten nicht erkannt, bitte in Foundry setzen.`);
    return { category: 'manual' };
  }
  return { category, ...RANGE_CATEGORIES[category] };
}

function weapon({ char, warn }, thing, ranged) {
  const damage = round(char.own(thing, 'Value'));
  const ap = round(char.own(thing, 'DK'));
  const skill = firstMatch(`${thing.Typ} ${thing.Bezeichner}`, ranged ? RANGED_WEAPON_SKILLS : MELEE_WEAPON_SKILLS);
  if (!skill) warn(`Waffe „${itemName(thing)}“: Fertigkeit nicht erkannt, bitte in Foundry setzen.`);

  const modes = clean(thing.Modi).toUpperCase();
  return {
    type: 'weapon',
    system: {
      description: describe(thing),
      category: ranged ? 'range' : 'melee',
      technology: technology(char, thing, { rating: 1 }),
      action: {
        type: ranged ? 'varies' : 'complex',
        test: ranged ? 'RangedAttackTest' : 'MeleeAttackTest',
        attribute: 'agility',
        skill,
        limit: { attribute: 'physical', base: round(char.own(thing, 'Precision')) },
        damage: {
          base: damage,
          value: damage,
          type: { base: /^\s*g/i.test(thing.SchadenTyp ?? '') ? 'stun' : 'physical' },
          ap: { base: ap, value: ap },
        },
        opposed: { test: 'PhysicalDefenseTest', resist: { test: 'PhysicalResistTest' } },
      },
      ...(ranged
        ? {
          range: {
            ranges: rangesOf(thing, warn),
            rc: { base: round(char.own(thing, 'RK')) },
            modes: {
              single_shot: /EM|SS/.test(modes),
              semi_auto: /HM|SA/.test(modes),
              burst_fire: /SM|BF/.test(modes),
              full_auto: /AM|FA/.test(modes),
            },
          },
        }
        : { melee: { reach: round(char.own(thing, 'Reichweite')) } }),
    },
  };
}

function device({ char }, thing, category) {
  const att = (name) => atLeastZero(char.own(thing, name));
  return {
    type: 'device',
    system: {
      description: describe(thing),
      category,
      technology: technology(char, thing),
      atts: {
        att1: { value: att('Angriff'), att: 'attack', editable: true },
        att2: { value: att('Schleicher'), att: 'sleaze', editable: true },
        att3: { value: att('Datenverarbeitung'), att: 'data_processing', editable: true },
        att4: { value: att('Firewall'), att: 'firewall', editable: true },
      },
      programs: atLeastZero(thing.Programmanzahl),
    },
  };
}

function quality({ char }, thing, type) {
  const typ = clean(thing.Typ);
  return {
    type: 'quality',
    system: {
      description: describe(thing),
      type,
      rating: atLeastZero(char.own(thing, 'Value')),
      karma: isNumeric(typ) ? Math.abs(round(typ.replace(',', '.'))) : 0,
    },
  };
}

const SPELL_CATEGORIES = [
  [/kampf|combat/i, 'combat'],
  [/wahrnehm|entdeck|detect/i, 'detection'],
  [/heil|health/i, 'health'],
  [/illus/i, 'illusion'],
  [/manip/i, 'manipulation'],
];
const SPELL_RANGES = [[/ber[üu]hr|touch/i, 'touch'], [/\(f\)|fläche|los.?\(?a/i, 'los_a'], [/bf|sicht|los/i, 'los']];
const SPELL_DURATIONS = [[/sust|auf|^a/i, 'sustained'], [/perm|^p/i, 'permanent'], [/sof|inst|^s/i, 'instant']];
const FORM_DURATIONS = [[/sust|auf|^a/i, 'sustained'], [/perm|^p/i, 'permanent'], [/sof|imm|^s/i, 'immediate']];
const FORM_TARGETS = [
  [/ger[äa]t|device/i, 'device'],
  [/persona/i, 'persona'],
  [/datei|file/i, 'file'],
  [/selbst|self/i, 'self'],
  [/sprite/i, 'sprite'],
  [/host/i, 'host'],
];

function spell(_, thing) {
  const typ = `${thing.Typ} ${thing.Zusatz}`;
  const duration = clean(thing.Duration) || clean(thing.Dauer);
  const range = clean(thing.Reichweite);
  const lines = [range && `Reichweite: ${range}`, duration && `Dauer: ${duration}`];
  return {
    type: 'spell',
    system: {
      description: describe(thing, lines),
      type: firstMatch(typ, [[/mana/i, 'mana'], [/phys/i, 'physical']]),
      category: firstMatch(typ, SPELL_CATEGORIES),
      drain: round(thing.Entzug),
      range: firstMatch(range, SPELL_RANGES),
      duration: firstMatch(duration, SPELL_DURATIONS),
      action: { attribute: 'magic', skill: 'spellcasting' },
    },
  };
}

function complexForm(_, thing) {
  const target = clean(thing.Target);
  const duration = clean(thing.Duration) || clean(thing.Dauer);
  const fading = clean(thing.Fading);
  const lines = [target && `Ziel: ${target}`, duration && `Dauer: ${duration}`, fading && `Schwund: ${fading}`];
  return {
    type: 'complex_form',
    system: {
      description: describe(thing, lines),
      target: firstMatch(target, FORM_TARGETS) || 'other',
      duration: firstMatch(duration, FORM_DURATIONS) || 'immediate',
      fade: round(/[+-]\s*\d+|\d+/.exec(fading)?.[0].replace(/\s/g, '') ?? thing.Entzug),
      action: {
        type: 'complex',
        test: 'ComplexFormTest',
        categories: ['complex_form'],
        attribute: 'resonance',
        skill: 'software',
        followed: { test: 'FadeTest' },
      },
    },
  };
}

const SPRITE_TYPES = [
  [/fehler|fault|stör/i, 'fault'],
  [/masch|machine/i, 'machine'],
  [/kris|crack|infiltrator/i, 'crack'],
  [/kurier|courier/i, 'courier'],
  [/daten|data/i, 'data'],
  [/begleit|companion/i, 'companion'],
  [/general|allround/i, 'generalist'],
];
const SPIRIT_TYPES = [
  [/luft|air/i, 'air'],
  [/feuer|fire/i, 'fire'],
  [/erde|earth/i, 'earth'],
  [/wasser|water/i, 'water'],
  [/mensch|\bman\b/i, 'man'],
  [/tier|beast/i, 'beasts'],
  [/pflanz|plant/i, 'plant'],
  [/schutz|guardian/i, 'guardian'],
];

function helperLines(thing) {
  const services = clean(thing.Dienste);
  return [services && `Dienste: ${services}`, thing.Geb_Reg === true ? 'Gebunden/Registriert' : ''];
}

function sprite({ char }, thing) {
  return {
    name: clean(thing.Bezeichner) || clean(thing.Typ) || 'Sprite',
    type: 'call_in_action',
    system: {
      description: describe(thing, helperLines(thing)),
      actor_type: 'sprite',
      sprite: { type: firstMatch(clean(thing.Typ), SPRITE_TYPES), level: Math.max(1, round(char.own(thing, 'Value'))) },
      action: {
        type: 'complex',
        test: 'CompileSpriteTest',
        opposed: { test: 'OpposedCompileSpriteTest' },
        followed: { test: 'FadeTest' },
      },
    },
  };
}

function spirit({ char }, thing) {
  return {
    name: clean(thing.Bezeichner) || clean(thing.Typ) || 'Geist',
    type: 'call_in_action',
    system: {
      description: describe(thing, helperLines(thing)),
      actor_type: 'spirit',
      spirit: { type: firstMatch(clean(thing.Typ), SPIRIT_TYPES), force: Math.max(1, round(char.own(thing, 'Value'))) },
      action: {
        type: 'complex',
        test: 'SummonSpiritTest',
        opposed: { test: 'OpposedSummonSpiritTest' },
        followed: { test: 'DrainTest' },
      },
    },
  };
}

/** Splits the links of a value into what an action can store as attribute, skill and modifier. */
function readPool({ char, attributeOf, skillOf, warn }, prop, owner) {
  const pool = { attributes: [], skill: '', spec: false, mod: prop.base, sources: [] };
  for (const link of char.linksOf(prop)) {
    const attribute = MATRIX_VALUES[link.name] ?? (link.name === 'Value' ? attributeOf.get(link.owner) : undefined);
    if (attribute && pool.attributes.length < 2) {
      pool.attributes.push(attribute);
      continue;
    }
    const skill = link.name === 'Value' ? skillOf.get(link.owner) : undefined;
    // A skill and its specialization are one entry in Foundry, which adds the bonus through spec.
    if (skill && pool.skill === skill.key) {
      pool.spec ||= skill.spec;
      continue;
    }
    if (skill && !pool.skill) {
      pool.skill = skill.key;
      pool.spec = skill.spec;
      continue;
    }
    if (attribute || skill) warn(`Handlung „${itemName(owner)}“ nutzt mehr Attribute oder Fertigkeiten als Foundry kennt, der Rest steht im Modifikator.`);
    const value = char.total(link);
    if (value) {
      pool.mod += value;
      pool.sources.push(`${clean(link.owner?.Bezeichner) || '?'} ${value > 0 ? '+' : ''}${value}`);
    }
  }
  pool.mod = round(pool.mod);
  return pool;
}

function action(context, thing) {
  const { char, limitOf } = context;
  const pool = readPool(context, char.prop(thing, 'Value'), thing);
  const against = readPool(context, char.prop(thing, 'Against'), thing);

  const limitProp = char.prop(thing, 'Limit');
  const limit = { base: limitProp.base, attribute: '' };
  for (const link of char.linksOf(limitProp)) {
    const attribute = MATRIX_VALUES[link.name] ?? (link.name === 'Value' ? limitOf.get(link.owner) : undefined);
    if (attribute && !limit.attribute) limit.attribute = attribute;
    else limit.base += char.total(link);
  }
  limit.base = round(limit.base);

  const hasOpposed = against.attributes.length > 0 || against.skill || against.mod !== 0;
  return {
    type: 'action',
    system: {
      description: describe(thing),
      action: {
        test: 'SuccessTest',
        attribute: pool.attributes[0] ?? '',
        attribute2: pool.attributes[1] ?? '',
        skill: pool.skill,
        spec: pool.spec,
        mod: pool.mod,
        mod_description: pool.sources.join(', '),
        limit,
        ...(hasOpposed
          ? {
            opposed: {
              test: 'OpposedTest',
              attribute: against.attributes[0] ?? '',
              attribute2: against.attributes[1] ?? '',
              skill: against.skill,
              mod: against.mod,
            },
          }
          : {}),
      },
    },
  };
}

const ITEM_BUILDERS = {
  [T.Handlung]: action,
  [T.Item]: ({ char }, t) => ({ type: 'equipment', system: { description: describe(t), technology: technology(char, t) } }),
  [T.Foki]: ({ char }, t) => ({ type: 'equipment', system: { description: describe(t), technology: technology(char, t) } }),
  [T.Widgets]: ({ char }, t) => ({ type: 'equipment', system: { description: describe(t), technology: technology(char, t) } }),
  [T.Programm]: ({ char }, t) => ({
    type: 'program',
    system: {
      description: describe(t, [t.Optionen]),
      type: firstMatch(`${t.Typ}`, [[/agent/i, 'agent'], [/hack|\bhck\b/i, 'hacking_program']]) || 'common_program',
      technology: technology(char, t),
    },
  }),
  [T.Munition]: ({ char }, t) => ({
    type: 'ammo',
    system: {
      description: describe(t),
      technology: { quantity: atLeastZero(t.Anzahl ?? 1) },
      damage: round(char.own(t, 'Value')),
      ap: round(char.own(t, 'DK')),
      accuracy: round(char.own(t, 'Precision')),
    },
  }),
  [T.Implantat]: ({ char }, t) => ({
    type: /bio/i.test(`${t.Typ} ${t.Bezeichner}`) ? 'bioware' : 'cyberware',
    system: {
      description: describe(t),
      essence: Number(t.Essenz) || 0,
      capacity: { total: atLeastZero(t.Kapazitaet) },
      technology: technology(char, t, { equipped: t.Besitz !== false }),
    },
  }),
  [T.Vorteil]: (context, t) => quality(context, t, 'positive'),
  [T.Nachteil]: (context, t) => quality(context, t, 'negative'),
  [T.Connection]: (_, t) => ({
    type: 'contact',
    system: { description: describe(t), connection: atLeastZero(t.Einfluss), loyalty: atLeastZero(t.Loyal) },
  }),
  [T.Sin]: ({ char }, t) => ({ type: 'sin', system: { description: describe(t), technology: { rating: atLeastZero(char.own(t, 'Value')) } } }),
  [T.Nahkampfwaffe]: (context, t) => weapon(context, t, false),
  [T.Fernkampfwaffe]: (context, t) => weapon(context, t, true),
  [T.Kommlink]: (context, t) => device(context, t, 'commlink'),
  [T.CyberDeck]: (context, t) => device(context, t, 'cyberdeck'),
  [T.Panzerung]: ({ char }, t) => {
    const armor = round(char.own(t, 'Value'));
    return {
      type: 'armor',
      system: {
        description: describe(t),
        armor: { base: armor, value: armor, accessory: true },
        capacity: { total: atLeastZero(char.own(t, 'Capacity')) },
        technology: technology(char, t, { rating: 1 }),
      },
    };
  },
  [T.Adeptenkraft]: ({ char }, t) => ({
    type: 'adept_power',
    system: {
      description: describe(t),
      level: atLeastZero(char.own(t, 'Value')),
      pp: isNumeric(clean(t.Typ)) ? Math.max(0, Number(clean(t.Typ).replace(',', '.'))) : 0,
    },
  }),
  [T.Geist]: spirit,
  [T.Sprite]: sprite,
  [T.Zaubersprueche]: spell,
  [T.KomplexeForm]: complexForm,
  [T.Wandlung]: (_, t) => ({ type: 'echo', system: { description: describe(t, [t.Paragon, t.Echos]) } }),
  [T.Initiation]: (_, t) => ({ type: 'metamagic', system: { description: describe(t, [t.Schutzpatron, t.Metamagie]) } }),
};

/** Values the character holds elsewhere, so they don't become items. */
const NOT_ITEMS = new Set([T.Fertigkeit, T.Attribut, T.Berechnet, T.Favorite, T.Note, T.Stroemung, T.Tradition, T.Vehikel, T.Drones]);

function isEmptyRow(char, thing) {
  if (clean(thing.Bezeichner)) return false;
  // The app marks separator rows with a value of 1337, see Thing.IsSeperator.
  if (char.prop(thing, 'Value').base === 1337) return true;
  return !clean(thing.Typ) && !clean(thing.Notiz) && !clean(thing.Text);
}

function vehicle({ char }, thing, isDrone) {
  const stat = (name) => ({ base: round(char.own(thing, name)) });
  const handling = stat('Handling');
  const speed = stat('Geschwindigkeit');
  const acceleration = stat('Beschleunigung');
  const weight = char.own(thing, 'Gewicht');
  return {
    name: itemName(thing),
    type: 'vehicle',
    system: {
      description: describe(thing, [weight ? `Gewicht: ${weight}` : '']),
      isDrone,
      vehicle_stats: {
        pilot: stat('Pilot'),
        handling,
        off_road_handling: handling,
        speed,
        off_road_speed: speed,
        acceleration,
        off_road_acceleration: acceleration,
        sensor: stat('Sensor'),
        seats: stat('Sitze'),
      },
      attributes: { body: stat('Rumpf') },
      armor: { rating: stat('Panzerung') },
    },
  };
}

function biography(char, things) {
  const p = char.person;
  const summary = [p.Alias, p.Char_Typ, p.MetaTyp, p.MetaTyp_sub, p.Geschlecht].map(clean).filter(Boolean).join(', ');
  const labelled = [
    ['Geburtsdatum', p.Geburtsdatum || clean(p.BirthDate).slice(0, 10)],
    ['Hautfarbe', p.Hautfarbe],
    ['Haarfarbe', p.Haarfarbe],
    ['Augenfarbe', p.Augenfarbe],
    ['Größe', p.Groesse],
    ['Gewicht', p.Gewicht],
    ['Runs', p.Runs],
  ].filter(([, value]) => value).map(([label, value]) => `${label}: ${value}`);

  const parts = [summary, ...labelled, p.Zusammenfassung, p.Notizen].flatMap(paragraphs);

  for (const t of things.filter((entry) => [T.Stroemung, T.Tradition].includes(char.typeOf(entry)))) {
    const label = char.typeOf(t) === T.Stroemung ? 'Strömung' : 'Tradition';
    parts.push(`<h3>${label}: ${escapeHtml(clean(t.Bezeichner))}</h3>`);
    parts.push(...[t.Paragon, t.Echos, t.Schutzpatron, t.Metamagie, t.Zusatz, t.Notiz].flatMap(paragraphs));
  }
  for (const note of things.filter((entry) => char.typeOf(entry) === T.Note)) {
    const title = clean(note.Bezeichner);
    if (title) parts.push(`<h3>${escapeHtml(title)}</h3>`);
    parts.push(...[note.Text, note.Notiz].flatMap(paragraphs));
  }
  return parts.join('');
}

function lifestyle(person) {
  const text = clean(person.Lebesstil);
  const cost = Number(person.LifeStyleCost) || 0;
  if (!text && !cost) return null;
  return {
    name: text || 'Lebensstil',
    type: 'lifestyle',
    system: {
      type: firstMatch(text, [
        [/stra(ß|ss)e|street/i, 'street'],
        [/squat/i, 'squatter'],
        [/unter|low/i, 'low'],
        [/mittel|medium/i, 'medium'],
        [/ober|high/i, 'high'],
        [/lux/i, 'luxury'],
      ]) || 'other',
      permanent: /permanent/i.test(text),
      cost,
    },
  };
}

function attributesOf(char, warnings) {
  const attributes = {};
  const changes = [];
  const sources = [];
  const attributeOf = new Map();

  for (const [srhName, key] of Object.entries(ATTRIBUTES)) {
    const thing = char.attribute(srhName);
    if (!thing) {
      warnings.push(`Attribut ${srhName} fehlt in der Datei.`);
      continue;
    }
    attributeOf.set(thing, key);
    const prop = char.prop(thing, 'Value');
    attributes[key] = { base: atLeastZero(prop.base) };

    const bonus = round(char.bonus(prop));
    if (bonus !== 0) {
      changes.push({ key: `system.attributes.${key}.value`, value: String(bonus), type: 'add' });
      const names = char.linksOf(prop).map((link) => clean(link.owner?.Bezeichner)).filter(Boolean).join(', ');
      sources.push(`${clean(thing.Bezeichner) || srhName} ${bonus > 0 ? '+' : ''}${bonus} (${names})`);
    }
  }
  return { attributes, attributeOf, bonusEffect: changes.length ? { changes, sources } : null };
}

function modifiersOf(char) {
  const linkBonus = (name) => {
    const thing = char.calculated(name);
    return thing ? char.bonus(char.prop(thing, 'Value')) : 0;
  };
  const p = char.person;
  const modifiers = {
    physical_limit: round(linkBonus('Limit_K')),
    mental_limit: round(linkBonus('Limit_G')),
    social_limit: round(linkBonus('Limit_S')),
    physical_track: round((Number(p.Schaden_K_max_mod) || 0) + linkBonus('MaxDamageK')),
    stun_track: round((Number(p.Schaden_G_max_mod) || 0) + linkBonus('MaxDamageG')),
    walk: round(linkBonus('Laufen')),
    run: round(linkBonus('Rennen')),
    lift_carry: round(linkBonus('Tragen')),
    essence: (Number(p.Essenz) || 0) + linkBonus('Essenz'),
  };
  return Object.fromEntries(Object.entries(modifiers).filter(([, value]) => value !== 0));
}

/**
 * Converts a character into plain data for Foundry.
 * Skills are returned separately because their final form needs the compendium.
 */
export function convertCharacter(char, { fallbackName = 'Unbenannt' } = {}) {
  const warnings = [];
  const warn = (message) => warnings.push(message);
  const p = char.person;

  const { attributes, attributeOf, bonusEffect } = attributesOf(char, warnings);
  const { skills, skillOf } = buildSkills(char, attributeOf, warn);

  const limitOf = new Map();
  for (const [srhName, key] of Object.entries(LIMITS)) {
    const thing = char.calculated(srhName);
    if (thing) limitOf.set(thing, key);
  }
  const magic = char.attribute('Magie');
  if (magic) limitOf.set(magic, 'magic');

  const context = { char, warn, attributeOf, limitOf, skillOf };
  const items = [];
  const vehicles = [];
  const things = char.things.filter((thing) => !isEmptyRow(char, thing));

  for (const thing of things) {
    const type = char.typeOf(thing);
    if (type === T.Vehikel || type === T.Drones) vehicles.push(vehicle(context, thing, type === T.Drones));
    if (NOT_ITEMS.has(type)) continue;

    const build = ITEM_BUILDERS[type];
    if (build) {
      items.push({ name: itemName(thing), ...build(context, thing) });
    } else {
      warn(`Eintrag „${itemName(thing)}“ hat den unbekannten Typ ${type} und wurde als Ausrüstung übernommen.`);
      items.push({ name: itemName(thing), type: 'equipment', system: { description: describe(thing) } });
    }
  }

  const noQuantity = things.filter((thing) => thing.Anzahl === 0 && !NOT_ITEMS.has(char.typeOf(thing)) && char.typeOf(thing) !== T.Munition);
  if (noQuantity.length) warn(`${noQuantity.length} Einträge hatten die Anzahl 0 und wurden auf 1 gesetzt: ${noQuantity.map(itemName).join(', ')}.`);

  const lifestyleItem = lifestyle(p);
  if (lifestyleItem) items.push(lifestyleItem);

  const metatypeText = clean(p.MetaTyp);
  const metatype = METATYPES[metatypeText.toLowerCase()];
  if (metatypeText && !metatype) warn(`Metatyp „${metatypeText}“ ist nicht bekannt und steht nur in der Beschreibung.`);

  const resonance = attributes.resonance?.base ?? 0;
  const magicValue = attributes.magic?.base ?? 0;
  const nuyen = round(p.Kontostand);
  if (nuyen < 0) warn(`Kontostand ist negativ (${nuyen}) und wurde auf 0 gesetzt.`);

  attributes.edge = { base: atLeastZero(p.Edge_Gesamt), uses: atLeastZero(p.Edge_Aktuell) };

  return {
    actor: {
      name: clean(p.Alias) || fallbackName,
      type: 'character',
      system: {
        ...(metatype ? { metatype } : {}),
        special: resonance > 0 ? 'resonance' : (magicValue > 0 ? 'magic' : 'mundane'),
        nuyen: Math.max(0, nuyen),
        karma: { value: atLeastZero(p.Karma_Aktuell), max: atLeastZero(p.Karma_Gesamt) },
        street_cred: atLeastZero(p.Strassenruf),
        notoriety: atLeastZero(p.SchlechterRuf),
        public_awareness: atLeastZero(p.Prominenz),
        description: { value: biography(char, things) },
        attributes,
        track: { physical: { value: atLeastZero(p.Schaden_K) }, stun: { value: atLeastZero(p.Schaden_G) } },
        modifiers: modifiersOf(char),
      },
    },
    items,
    skills,
    vehicles,
    bonusEffect,
    warnings,
    skippedRows: char.things.length - things.length,
  };
}
