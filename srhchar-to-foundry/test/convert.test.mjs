// Run with: node --test srhchar-to-foundry/test
import assert from 'node:assert/strict';
import { readFile } from 'node:fs/promises';
import { test } from 'node:test';
import { convertCharacter } from '../scripts/convert.mjs';
import { buildDocuments } from '../scripts/foundry-json.mjs';
import { failureReport, successReport, unusedFields } from '../scripts/report.mjs';
import { mergeSkills } from '../scripts/skills.mjs';
import { parseSrhChar, UnsupportedFileError } from '../scripts/srh-reader.mjs';
import { paragraphs, rtfToText } from '../scripts/text.mjs';

const connect = (id, ownerId, base, links = [], name = 'Value') => ({
  $id: id,
  BaseValue: base,
  Connected: { $values: links.map(($ref) => ({ $ref })) },
  Name: name,
  Owner: { $ref: ownerId },
  Active: true,
});

/** A small file in the format of version 1.8. Refs point both forward and backward, as in real files. */
const version18 = () => JSON.stringify({
  $id: '1',
  FILE_VERSION_NUMBER: '1.8',
  Person: { $id: '2', Alias: 'Testrunner', MetaTyp: 'Zwerg', Kontostand: 100, Karma_Aktuell: 3, Karma_Gesamt: 9, Edge_Gesamt: 4, Edge_Aktuell: 2 },
  CTRLAttribut: {
    $id: '3',
    Data: { $values: [{ $ref: '10' }, { $ref: '20' }] },
    Logik: { $id: '10', ThingType: 11, Bezeichner: 'Logik', Value: connect('11', '10', 5, ['31']) },
    Resonanz: { $id: '20', ThingType: 11, Bezeichner: 'Resonanz', Value: connect('21', '20', 6) },
  },
  CTRLImplantat: {
    Data: {
      $values: [{ $id: '30', ThingType: 6, Bezeichner: 'Cerebralbooster', Typ: 'Bioware', Essenz: 0.2, Aktiv: true, Besitz: true, Value: connect('31', '30', 1) }],
    },
  },
  CTRLFertigkeit: {
    Data: {
      $values: [
        { $id: '40', ThingType: 2, Bezeichner: 'Hacking', Typ: 'Cracken', Value: connect('41', '40', 4) },
        { $id: '42', ThingType: 2, Bezeichner: 'Englisch', Typ: 'Sprache', Value: connect('43', '42', 3) },
        { $id: '44', ThingType: 2, Bezeichner: 'Hacking spez. Hosts', Typ: 'Aktion', Value: connect('45', '44', 6) },
      ],
    },
  },
  CTRLHandlung: {
    Data: {
      $values: [{
        $id: '50',
        ThingType: 1,
        Bezeichner: 'Eiliges Hacken',
        Typ: 'Matrix',
        Value: connect('51', '50', 0, ['41', '11', '31']),
        Limit: connect('52', '50', 0, ['61'], 'Limit'),
        Against: connect('53', '50', 0, ['21'], 'Against'),
      }],
    },
  },
  CTRLKommlink: {
    Data: {
      $values: [{ $id: '60', ThingType: 14, Bezeichner: 'Sekretär', Aktiv: true, Value: connect('62', '60', 4), Datenverarbeitung: connect('61', '60', 2, [], 'Datenverarbeitung'), Firewall: connect('63', '60', 3, [], 'Firewall') }],
    },
  },
  CTRLItem: { Data: { $values: [{ $id: '70', ThingType: 3, Bezeichner: '', Value: connect('71', '70', 1337) }] } },
});

test('reads version 1.8 values, links and refs', () => {
  const { actor, items, skills, bonusEffect } = convertCharacter(parseSrhChar(version18()));

  assert.equal(actor.name, 'Testrunner');
  assert.equal(actor.system.metatype, 'dwarf');
  assert.equal(actor.system.special, 'resonance');
  assert.deepEqual(actor.system.karma, { value: 3, max: 9 });
  assert.equal(actor.system.attributes.logic.base, 5);
  assert.deepEqual(actor.system.attributes.edge, { base: 4, uses: 2 });
  assert.deepEqual(bonusEffect.changes, [{ key: 'system.attributes.logic.value', value: '1', type: 'add' }]);

  const hacking = skills.find((skill) => skill.name === 'Hacking');
  assert.equal(hacking.rating, 4);
  assert.deepEqual(hacking.specializations, ['Hosts']);
  assert.equal(skills.find((skill) => skill.name === 'Englisch').category, 'language');

  const booster = items.find((item) => item.name === 'Bioware: Cerebralbooster');
  assert.equal(booster.type, 'bioware');
  assert.equal(booster.system.essence, 0.2);

  const { action } = items.find((item) => item.name === 'Matrix: Eiliges Hacken').system;
  assert.equal(action.attribute, 'logic');
  assert.equal(action.skill, 'hacking');
  assert.equal(action.mod, 1);
  assert.equal(action.limit.attribute, 'data_processing');
  assert.equal(action.opposed.attribute, 'resonance');

  assert.equal(items.find((item) => item.type === 'device').system.atts.att3.value, 2);
  assert.ok(!items.some((item) => item.type === 'equipment'), 'separator rows are skipped');
});

test('reads the version 1.7 file of the app tests', async () => {
  const text = await readFile(new URL('../../SharedCodeTest/assets/Flash.SRHChar', import.meta.url), 'utf8');
  const { actor, items } = convertCharacter(parseSrhChar(text));

  assert.equal(actor.name, 'Flash');
  assert.equal(actor.system.special, 'resonance');
  assert.deepEqual(actor.system.karma, { value: 51, max: 211 });
  assert.equal(actor.system.attributes.body.base, 5);
  assert.match(actor.system.description.value, /fahrender Händler/);

  const contact = items.find((item) => item.name === 'Auftragsschieber: Black Cat');
  assert.equal(contact.system.connection, 2);
  assert.equal(contact.system.loyalty, 1);

  const rank = items.find((item) => item.name.endsWith('Militärischer Rang'));
  assert.equal(rank.type, 'quality');
  assert.match(rank.system.description.value, /Polizeimeister/);
  assert.match(rank.system.description.value, /Soz Limit Erhöhen/);
});

/** A file with only the given controllers, plus the attributes that skills link to. */
const fileWith = (controllers) => JSON.stringify({
  FILE_VERSION_NUMBER: '1.8',
  Person: { Alias: 'Test' },
  CTRLAttribut: {
    Data: { $values: [{ $ref: '10' }, { $ref: '12' }] },
    Geschick: { $id: '10', ThingType: 11, Bezeichner: 'Geschicklichkeit', Value: connect('11', '10', 6) },
    Charisma: { $id: '12', ThingType: 11, Bezeichner: 'Charisma', Value: connect('13', '12', 4) },
  },
  ...controllers,
});

const skillRow = (id, name, typ, base, links = []) => ({ $id: id, ThingType: 2, Bezeichner: name, Typ: typ, Value: connect(`${id}v`, id, base, links) });

test('keeps attributes and bonuses out of skill ratings', () => {
  const { skills, warnings } = convertCharacter(parseSrhChar(fileWith({
    CTRLItem: { Data: { $values: [{ $id: '30', ThingType: 3, Bezeichner: 'Linsen', Value: connect('31', '30', 3) }] } },
    CTRLFertigkeit: {
      Data: {
        $values: [
          skillRow('40', 'Überzeugen', '', 3, ['13']),
          skillRow('41', 'Schleichen', 'Aktion', 5, ['11', '31']),
          skillRow('42', 'Forensik', '', 2, ['11']),
          skillRow('43', 'Psychologie', '', 2),
          skillRow('44', 'Laser', 'Exotische Waffen', 5, ['11']),
          skillRow('45', 'Survival', 'Wissen Natur', 3, ['11']),
        ],
      },
    },
  })));
  const find = (name) => skills.find((skill) => skill.name === name);

  assert.equal(find('Con').rating, 3);
  assert.equal(find('Sneaking').rating, 5);
  assert.match(find('Sneaking').description, /Linsen \+3/);
  assert.ok(warnings.some((warning) => warning.includes('Linsen +3')));
  assert.deepEqual([find('Forensik').category, find('Forensik').attribute], ['active', 'agility']);
  assert.equal(find('Psychologie').category, 'knowledge');
  assert.deepEqual([find('Exotic Ranged Weapon').rating, find('Exotic Ranged Weapon').specializations], [5, ['Laser']]);
  assert.equal(find('Survival').category, 'active');
});

test('takes the rating of a parent skill from an action of the same name', () => {
  const { skills } = convertCharacter(parseSrhChar(fileWith({
    CTRLHandlung: { Data: { $values: [{ $id: '50', ThingType: 1, Bezeichner: 'Gebräuche', Value: connect('51', '50', 3, ['13']) }] } },
    CTRLFertigkeit: { Data: { $values: [skillRow('40', 'Konzerne', 'Gebräuche Spezialisierung', 2, ['51'])] } },
  })));
  const etiquette = skills.find((skill) => skill.name === 'Etiquette');
  assert.equal(etiquette.rating, 3);
  assert.deepEqual(etiquette.specializations, ['Konzerne']);
});

test('uses spec for a specialization next to its skill', () => {
  const { items } = convertCharacter(parseSrhChar(fileWith({
    CTRLFertigkeit: {
      Data: {
        $values: [
          skillRow('40', 'Vorführung', '', 3),
          skillRow('41', 'Gesang', 'Vorführung-Spezialisierung', 2),
        ],
      },
    },
    CTRLHandlung: { Data: { $values: [{ $id: '50', ThingType: 1, Bezeichner: 'Musikprobe', Value: connect('51', '50', 0, ['40v', '41v']) }] } },
  })));
  const { action } = items.find((item) => item.name === 'Musikprobe').system;
  assert.deepEqual([action.skill, action.spec, action.mod], ['performance', true, 0]);
});

test('recognizes weapons, sprites and signed fading', () => {
  const { items } = convertCharacter(parseSrhChar(fileWith({
    CTRLFernkampfwaffe: { Data: { $values: [{ $id: '60', ThingType: 13, Bezeichner: 'Ares Predator V', Typ: 'SP', Value: connect('61', '60', 8) }] } },
    CTRLNahkampfwaffe: { Data: { $values: [{ $id: '62', ThingType: 12, Bezeichner: 'Hörner', Value: connect('63', '62', 8) }] } },
    CTRLSprite: { Data: { $values: [{ $id: '64', ThingType: 27, Bezeichner: 'Alpha', Typ: 'Stör', Value: connect('65', '64', 6) }] } },
    CTRLKomplexeForm: { Data: { $values: [{ $id: '66', ThingType: 26, Bezeichner: 'Petze', Fading: 'Stufe - 2' }] } },
  })));
  const find = (name) => items.find((item) => item.name.endsWith(name)).system;

  assert.equal(find('Predator V').action.skill, 'pistols');
  assert.equal(find('Hörner').action.skill, 'unarmed_combat');
  assert.equal(find('Alpha').sprite.type, 'fault');
  assert.equal(find('Petze').fade, -2);
});

test('marks native languages', () => {
  const { skills } = convertCharacter(parseSrhChar(fileWith({
    CTRLFertigkeit: {
      Data: {
        $values: [
          skillRow('40', 'Deutsch M', 'Sprache', 0),
          skillRow('41', 'Spanisch (MS)', 'Sprachfertigkeit', 9),
          skillRow('42', 'Latein', 'Sprache', 99),
          skillRow('43', 'Japanisch', 'Sprache', 2),
        ],
      },
    },
  })));
  const native = (name) => skills.find((skill) => skill.name === name).isNative;

  assert.equal(native('Deutsch M'), true);
  assert.equal(native('Spanisch (MS)'), true);
  assert.equal(native('Latein'), true);
  assert.equal(native('Japanisch'), false);
  assert.deepEqual(mergeSkills([], skills)[0].system.skill.language, { isNative: true });
});

test('treats quantity 0 as not filled in, except for ammunition', () => {
  const { items, warnings } = convertCharacter(parseSrhChar(fileWith({
    CTRLPanzerung: { Data: { $values: [{ $id: '60', ThingType: 17, Bezeichner: 'Jacke', Anzahl: 0, Value: connect('61', '60', 12) }] } },
    CTRLItem: { Data: { $values: [{ $id: '62', ThingType: 3, Bezeichner: 'Seil', Anzahl: 4, Value: connect('63', '62', 0) }] } },
    CTRLMunition: { Data: { $values: [{ $id: '64', ThingType: 5, Bezeichner: 'Gel', Anzahl: 0, Value: connect('65', '64', 0) }] } },
  })));
  const quantity = (name) => items.find((item) => item.name === name).system.technology.quantity;

  assert.equal(quantity('Jacke'), 1);
  assert.equal(quantity('Seil'), 4);
  assert.equal(quantity('Gel'), 0);
  assert.ok(warnings.some((warning) => warning.startsWith('1 Einträge') && warning.includes('Jacke')));
});

test('derives ranges from the weapon type', () => {
  const weapon = (id, typ, name) => ({ $id: id, ThingType: 13, Typ: typ, Bezeichner: name, Value: connect(`${id}v`, id, 8) });
  const { items, warnings } = convertCharacter(parseSrhChar(fileWith({
    CTRLFernkampfwaffe: {
      Data: {
        $values: [
          weapon('60', 'MP', 'Ingram'),
          weapon('61', 'SP', 'Predator'),
          weapon('62', 'Pistole', 'Taser Defiance'),
          weapon('63', 'Flammenwerfer', 'Blazer'),
          weapon('64', 'Laser', 'Redline'),
        ],
      },
    },
  })));
  const ranges = (name) => items.find((item) => item.name.endsWith(name)).system.range.ranges;

  assert.deepEqual(ranges('Ingram'), { category: 'smg', short: 10, medium: 40, long: 80, extreme: 150 });
  assert.equal(ranges('Predator').category, 'heavyPistol');
  assert.equal(ranges('Taser Defiance').category, 'taser');
  assert.equal(ranges('Blazer').long, 0, 'the schema allows no -1');
  assert.deepEqual(ranges('Redline'), { category: 'manual' });
  assert.ok(warnings.some((warning) => warning.includes('Redline') && warning.includes('Reichweiten')));
});

test('reports values, warnings and fields that were not used', async () => {
  const template = JSON.parse(await readFile(new URL('../data/character-template.json', import.meta.url), 'utf8'));
  const char = parseSrhChar(fileWith({
    CTRLCyberDeck: { Data: { $values: [{ $id: '60', ThingType: 15, Bezeichner: 'Deck', Order: 3, Schaden: 3, Firewall_o: 0, Value: connect('61', '60', 4) }] } },
    CTRLFernkampfwaffe: { Data: { $values: [{ $id: '62', ThingType: 13, Typ: 'Laser', Bezeichner: 'Redline', Value: connect('63', '62', 8) }] } },
    CTRLItem: { Data: { $values: [{ $id: '70', ThingType: 3, Bezeichner: '', Value: connect('71', '70', 1337) }] } },
  }));
  const converted = convertCharacter(char);
  const { character, vehicles } = buildDocuments(converted, template);

  assert.deepEqual(unusedFields(char).map(({ owner, field }) => [owner, field]), [['CyberDeck', 'Schaden']], 'skips app fields and empty fields');

  const report = successReport({ file: 'a.SRHChar', char, converted, character, vehicles, fileNames: ['Test.json'], template });
  assert.match(report, /\| CyberDeck \| Schaden \| 1 \| 3 \|/);
  assert.match(report, /Redline.*Reichweiten/);
  assert.match(report, /`Test\.json`/);
  assert.match(report, /ausgelassen \| 1 \|/);

  assert.match(failureReport('a.SRHChar', new Error('kaputt')), /nicht umgewandelt\*\*\n\nkaputt/);
});

test('rejects files the app cannot load either', () => {
  assert.throws(() => parseSrhChar('1};;kein json'), UnsupportedFileError);
  assert.throws(() => parseSrhChar('{"FILE_VERSION_NUMBER":"1130"}'), UnsupportedFileError);
  assert.throws(() => parseSrhChar('{"AttributController":{}}'), UnsupportedFileError);
});

test('turns RTF into paragraphs', () => {
  const rtf = "{\\rtf1\\ansi{\\fonttbl{\\f0 Segoe UI;}}{\\*\\generator Riched20;}\\pard H\\'e4ndler\\par\r\nZweite \\u8364? Zeile\\par}";
  assert.equal(rtfToText(rtf).trim(), 'Händler\nZweite € Zeile');
  assert.deepEqual(paragraphs(rtf), ['<p>Händler</p>', '<p>Zweite € Zeile</p>']);
  assert.deepEqual(paragraphs('a < b'), ['<p>a &lt; b</p>']);
});

test('merges wanted skills into the template', () => {
  const templateSkill = (name) => ({ name, type: 'skill', system: { skill: { category: 'active', rating: 0, specializations: [] } } });
  const template = [templateSkill('Hacking'), templateSkill('Free-Fall')];

  const items = mergeSkills(template, [
    { name: 'Hacking', category: 'active', rating: 4, specializations: ['Hosts'] },
    { name: 'Free Fall', category: 'active', rating: 2, specializations: [] },
    { name: 'Forensik', category: 'knowledge', knowledgeType: 'professional', attribute: 'logic', rating: 3, specializations: [] },
  ]);

  assert.equal(items.length, 3);
  assert.equal(items[0].system.skill.rating, 4);
  assert.deepEqual(items[0].system.skill.specializations, [{ name: 'Hosts' }]);
  assert.equal(items[1].system.skill.rating, 2, 'different spelling, same skill');
  assert.equal(items[2].system.skill.knowledgeType, 'professional');
  assert.equal(template[0].system.skill.rating, 0, 'template stays untouched');
});

test('builds documents for the import of Foundry from the real template', async () => {
  const template = JSON.parse(await readFile(new URL('../data/character-template.json', import.meta.url), 'utf8'));
  const { character } = buildDocuments(convertCharacter(parseSrhChar(version18())), template);

  assert.equal(character.name, 'Testrunner');
  assert.equal(character.prototypeToken.name, 'Testrunner');
  assert.equal(character.system.skillset, template.system.skillset);
  assert.equal(character.system.attributes.logic.base, 5);
  assert.equal(character.system.track.physical.max, template.system.track.physical.max, 'template values stay');

  const skills = character.items.filter((item) => item.type === 'skill');
  assert.ok(skills.length >= template.items.length);
  assert.equal(skills.find((item) => item.name === 'Hacking').system.skill.rating, 4);
  assert.equal(skills.find((item) => item.name === 'Hacking').img, template.items.find((item) => item.name === 'Hacking').img);

  const ids = character.items.map((item) => item._id);
  assert.ok(ids.every((id) => /^[A-Za-z0-9]{16}$/.test(id)));
  assert.equal(new Set(ids).size, ids.length);

  const [effect] = character.effects;
  assert.equal(effect.system.changes[0].target, effect.system.targets[0].id);
  assert.equal(effect.system.changes[0].value, 1);
});
