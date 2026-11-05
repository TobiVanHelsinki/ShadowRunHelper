#!/usr/bin/env node
// Creates data/character-template.json from the export of an empty character.
// Run this again with a new export when an update of the system changes the skills.
//   node tools/make-template.mjs <Export eines leeren Charakters>
import { mkdir, readFile, writeFile } from 'node:fs/promises';

const [exportFile] = process.argv.slice(2);
if (!exportFile) {
  console.error('Aufruf: node tools/make-template.mjs <Export eines leeren Charakters>');
  process.exit(2);
}

const actor = JSON.parse(await readFile(exportFile, 'utf8'));
const others = actor.items.filter((item) => item.type !== 'skill');
if (actor.type !== 'character' || others.length > 0) {
  console.error(`Das ist kein leerer Charakter: Typ ${actor.type}, ${others.length} Einträge außer Fertigkeiten.`);
  process.exit(1);
}

const { coreVersion, systemId, systemVersion } = actor._stats;
const template = {
  name: 'Vorlage',
  type: actor.type,
  img: actor.img,
  folder: null,
  flags: actor.flags,
  ownership: actor.ownership,
  prototypeToken: { ...actor.prototypeToken, name: 'Vorlage' },
  system: actor.system,
  items: actor.items.map(({ name, type, img, flags, system }) => ({ name, type, img, flags, system })),
  effects: [],
  _stats: { coreVersion, systemId, systemVersion },
};

const target = new URL('../data/character-template.json', import.meta.url);
await mkdir(new URL('.', target), { recursive: true });
await writeFile(target, `${JSON.stringify(template, null, 1)}\n`);
console.log(`Vorlage für ${systemId} ${systemVersion} mit ${template.items.length} Fertigkeiten geschrieben.`);
