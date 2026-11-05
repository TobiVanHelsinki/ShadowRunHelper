#!/usr/bin/env node
// Converts character files into JSON files for "Import Data" in Foundry.
//   node tools/convert.mjs <Datei oder Ordner> [Zielordner]
// Without a target, the files go to the folder "foundry" next to the input.
import { mkdir, readFile, readdir, stat, writeFile } from 'node:fs/promises';
import { basename, dirname, extname, join } from 'node:path';
import { convertCharacter } from '../scripts/convert.mjs';
import { buildDocuments } from '../scripts/foundry-json.mjs';
import { failureReport, hintCount, successReport } from '../scripts/report.mjs';
import { parseSrhChar } from '../scripts/srh-reader.mjs';

const EXTENSIONS = new Set(['.srhchar', '.srchchar', '.srwin']);
const REPORT_NAME = 'Bericht.md';

async function collectFiles(path) {
  if (!(await stat(path)).isDirectory()) return [path];
  const entries = await readdir(path, { withFileTypes: true });
  const nested = await Promise.all(entries.map((entry) => {
    const child = join(path, entry.name);
    if (entry.isDirectory()) return collectFiles(child);
    return EXTENSIONS.has(extname(entry.name).toLowerCase()) ? [child] : [];
  }));
  return nested.flat();
}

const fileName = (name) => `${name.replace(/[\\/:*?"<>|]/g, '_').trim()}.json`;

async function writeDocument(folder, name, document) {
  await writeFile(join(folder, fileName(name)), `${JSON.stringify(document, null, 1)}\n`);
  return fileName(name);
}

const [target, output] = process.argv.slice(2);
if (!target) {
  console.error('Aufruf: node tools/convert.mjs <Datei oder Ordner> [Zielordner]');
  process.exit(2);
}

const isFolder = (await stat(target)).isDirectory();
const root = output ?? join(isFolder ? target : dirname(target), 'foundry');
const template = JSON.parse(await readFile(new URL('../data/character-template.json', import.meta.url), 'utf8'));

let failed = 0;
for (const file of await collectFiles(target)) {
  try {
    const char = parseSrhChar(await readFile(file, 'utf8'));
    const converted = convertCharacter(char, { fallbackName: basename(file, extname(file)) });
    const { character, vehicles } = buildDocuments(converted, template);

    const folder = join(root, fileName(character.name).replace(/\.json$/, ''));
    await mkdir(folder, { recursive: true });
    const fileNames = [await writeDocument(folder, character.name, character)];
    for (const [index, vehicle] of vehicles.entries()) {
      fileNames.push(await writeDocument(folder, `Fahrzeug ${String(index + 1).padStart(2, '0')} - ${vehicle.name}`, vehicle));
    }

    const report = join(folder, REPORT_NAME);
    await writeFile(report, successReport({ file, char, converted, character, vehicles, fileNames, template }));
    console.log(`OK     ${file}: ${hintCount(converted.warnings.length)}, Bericht in ${report}`);
  } catch (error) {
    failed += 1;
    const folder = join(root, basename(file, extname(file)));
    await mkdir(folder, { recursive: true });
    const report = join(folder, REPORT_NAME);
    await writeFile(report, failureReport(file, error));
    console.log(`FEHLER ${file}: ${error.message} Bericht in ${report}`);
  }
}
console.log(failed ? `${failed} Dateien nicht umgewandelt.` : 'Alle Dateien umgewandelt.');
process.exitCode = failed ? 1 : 0;
