/** Mirrors the ThingDefs enum in SharedCode/Resources/Types.cs. */
export const ThingType = Object.freeze({
  Handlung: 1,
  Fertigkeit: 2,
  Item: 3,
  Programm: 4,
  Munition: 5,
  Implantat: 6,
  Vorteil: 7,
  Nachteil: 8,
  Connection: 9,
  Sin: 10,
  Attribut: 11,
  Nahkampfwaffe: 12,
  Fernkampfwaffe: 13,
  Kommlink: 14,
  CyberDeck: 15,
  Vehikel: 16,
  Panzerung: 17,
  Eigenschaft: 18,
  Adeptenkraft: 19,
  Geist: 20,
  Foki: 21,
  Stroemung: 22,
  Tradition: 23,
  Zaubersprueche: 24,
  Berechnet: 25,
  KomplexeForm: 26,
  Sprite: 27,
  Widgets: 28,
  Wandlung: 29,
  Initiation: 30,
  Note: 31,
  Favorite: 32,
  Drones: 33,
});

const SUPPORTED_VERSIONS = new Set(['1.5', '1.6', '1.7', '1.8']);

/** Where file versions before 1.8 keep what 1.8 stores in a ConnectProperty. */
const LEGACY_PROPS = {
  Value: { values: ['Wert'], links: ['LinkedThings', 'WertZusammensetzung', 'PoolZusammensetzung'] },
  Limit: { values: ['Grenze'], links: ['GrenzeZusammensetzung'] },
  Against: { values: ['Gegen'], links: ['GegenZusammensetzung'] },
  DK: { values: ['PB'] },
  Precision: { values: ['Praezision'] },
  Capacity: { values: ['Kapazitaet'] },
  RK: { values: ['Rueckstoss'] },
};

/** PropertyID values of old link entries, see LinkList2CalcProp in CharHolderIO.cs. */
const LEGACY_PROPERTY_IDS = {
  Gegen: 'Against',
  Grenze: 'Limit',
  Praezision: 'Precision',
  Kapazitaet: 'Capacity',
  PB: 'DK',
  Rueckstoss: 'RK',
};

export class UnsupportedFileError extends Error {}

/** Read access to a character file: resolves $ref entries and hides the file version differences. */
export class SrhChar {
  #ids = new Map();
  #types = new Map();
  #props = new Map();
  #connects = new Map();

  constructor(raw) {
    this.raw = raw;
    this.version = String(raw.FILE_VERSION_NUMBER);
    this.#index(raw);
    this.person = this.deref(raw.Person) ?? {};
    this.things = this.#collectThings();
  }

  #index(node) {
    if (Array.isArray(node)) {
      for (const entry of node) this.#index(entry);
    } else if (node && typeof node === 'object') {
      if (node.$id !== undefined) this.#ids.set(String(node.$id), node);
      for (const value of Object.values(node)) this.#index(value);
    }
  }

  #collectThings() {
    const things = new Set();
    for (const key of Object.keys(this.raw).filter((name) => name.startsWith('CTRL'))) {
      const controller = this.deref(this.raw[key]);
      if (!controller) continue;
      for (const entry of this.values(controller.Data)) {
        const thing = this.deref(entry);
        if (!thing || typeof thing !== 'object') continue;
        things.add(thing);
        this.#types.set(thing, thing.ThingType ?? controller.eDataTyp);
      }
    }
    return [...things];
  }

  deref(node) {
    if (node && typeof node === 'object' && node.$ref !== undefined) return this.#ids.get(String(node.$ref));
    return node;
  }

  /** Items of a serialized collection, which is either an array or { $values: [] }. */
  values(node) {
    const resolved = this.deref(node);
    if (Array.isArray(resolved)) return resolved;
    return resolved?.$values ?? [];
  }

  typeOf(thing) {
    return this.#types.get(thing) ?? thing.ThingType;
  }

  attribute(name) {
    return this.deref(this.deref(this.raw.CTRLAttribut)?.[name]);
  }

  calculated(name) {
    return this.deref(this.deref(this.raw.CTRLBerechnet)?.[name]);
  }

  /** A value of a thing, for example its Value or Limit, independent of the file version. */
  prop(thing, name) {
    let props = this.#props.get(thing);
    if (!props) {
      props = new Map();
      this.#props.set(thing, props);
    }
    if (!props.has(name)) props.set(name, this.#readProp(thing, name));
    return props.get(name);
  }

  #readProp(thing, name) {
    const legacy = LEGACY_PROPS[name] ?? {};
    const valueKeys = [name, ...(legacy.values ?? [])];

    for (const key of valueKeys) {
      const value = this.deref(thing[key]);
      if (value && typeof value === 'object' && 'BaseValue' in value) return this.#connectProp(value);
    }

    const numeric = valueKeys.map((key) => thing[key]).find((value) => typeof value === 'number');
    // The app resets these fields when it reads version 1.5, see ConvertWithRightVersion in CharHolderIO.cs.
    const isResetField = this.version === '1.5' && this.typeOf(thing) === ThingType.Handlung
      && ['Value', 'Limit', 'Against'].includes(name);
    const linkKey = (legacy.links ?? []).find((key) => this.values(thing[key]).length > 0);

    return {
      name,
      owner: thing,
      base: isResetField ? 0 : (numeric ?? 0),
      active: 'Aktiv' in thing ? thing.Aktiv === true : true,
      legacyLinks: linkKey ? this.values(thing[linkKey]) : [],
    };
  }

  #connectProp(connect) {
    let prop = this.#connects.get(connect);
    if (!prop) {
      prop = {
        name: connect.Name ?? 'Value',
        owner: this.deref(connect.Owner) ?? null,
        base: Number(connect.BaseValue) || 0,
        active: connect.Active !== false,
        connect,
      };
      this.#connects.set(connect, prop);
    }
    return prop;
  }

  /** The values a prop adds up. */
  linksOf(prop) {
    if (!prop.links) {
      prop.links = prop.connect
        ? this.values(prop.connect.Connected)
          .map((entry) => this.deref(entry))
          .filter((entry) => entry && typeof entry === 'object')
          .map((entry) => this.#connectProp(entry))
        : prop.legacyLinks.flatMap((entry) => this.#readLegacyLink(entry));
    }
    return prop.links;
  }

  #readLegacyLink(entry) {
    const link = this.deref(entry);
    const owner = this.deref(link?.Object);
    if (!owner || typeof owner !== 'object') return [];
    const id = link.PropertyID ?? link.strProperty ?? '';
    return [this.prop(owner, LEGACY_PROPERTY_IDS[id] ?? (id || 'Value'))];
  }

  /** Same calculation as ConnectProperty.Recalculate, but safe against circular links in broken files. */
  total(prop, path = new Set()) {
    if (path.has(prop) || !prop.active) return 0;
    path.add(prop);
    const sum = prop.base + this.linksOf(prop).reduce((acc, link) => acc + this.total(link, path), 0);
    path.delete(prop);
    return sum;
  }

  /** What the links of a prop add on top of its own base value. */
  bonus(prop) {
    return this.linksOf(prop).reduce((acc, link) => acc + this.total(link, new Set([prop])), 0);
  }

  /** The value of a thing itself. Unlike total this also works for items that are switched off. */
  own(thing, name) {
    const prop = this.prop(thing, name);
    return prop.base + this.bonus(prop);
  }
}

export function parseSrhChar(text) {
  let raw;
  try {
    raw = JSON.parse(text.replace(/^﻿/, ''));
  } catch {
    throw new UnsupportedFileError('Keine JSON-Datei. Vermutlich das Format von vor 2016, das auch die App nicht mehr lädt.');
  }
  const version = raw?.FILE_VERSION_NUMBER;
  if (!SUPPORTED_VERSIONS.has(String(version))) {
    throw new UnsupportedFileError(`Dateiversion ${version ?? 'unbekannt'} wird nicht unterstützt, nur 1.5 bis 1.8.`);
  }
  return new SrhChar(raw);
}
