# SRHChar to Foundry

Converts ShadowRunHelper characters (`.SRHChar`) into JSON files that Foundry VTT can import with its built-in **Import Data** function. The target is the [Shadowrun 5e system](https://github.com/SR5-FoundryVTT/SR5-FoundryVTT).

- Reads file versions 1.5 to 1.8, the same versions the app can still load.
- Written for Foundry 14.370 and `shadowrun5e` 0.37.9.
- Needs only Node.js (tested with version 20) and has no dependencies.
- Reports and messages are in German.

## Convert

```sh
node srhchar-to-foundry/tools/convert.mjs <file or folder> [target folder]
```

Without a target folder, the output goes to `foundry/` next to the input. Each character gets its own folder:

- one JSON file for the character
- one JSON file for each vehicle or drone
- `Bericht.md`, a report

The report is always written, even if there are no problems or the conversion fails. It lists the values taken over, what you should check by hand, and every filled field of the file that the converter doesn't use.

## Import into Foundry

Repeat this for each JSON file you need:

1. Create a new actor in the Actors tab: **Character** for a character, **Vehicle** for a vehicle.
2. Right-click the actor in the list and choose **Import Data**.
3. Select the JSON file.

The import replaces all data of the actor, so use a new actor or one you want to overwrite. Set the driver of a vehicle by hand, because the ID of the character only exists once it is in Foundry.

## What goes where

| ShadowRunHelper | Foundry |
| --- | --- |
| Attributes, karma, nuyen, edge, damage | values of the actor |
| Bonuses on attributes from links | effect „Boni aus SRHChar-Verknüpfungen“ |
| Bonuses on limits, condition monitor and movement | modifiers of the actor |
| Skills | `skill`, added to the default skill set. The rating is the skill's own value, without the attribute |
| Bonuses on skills from links | description of the skill and a note in the report |
| Skill groups | resolved into the ratings of their skills |
| Languages with a rating of 9 or more, or `M`/`MS` after the name | native language |
| Actions | `action` with attributes, skill, limit and opposed test from the links |
| Qualities | `quality` |
| Connections | `contact` |
| SINs | `sin` |
| Ranged and melee weapons | `weapon`. Ranges come from the weapon type, because the app doesn't store any |
| Armor | `armor` |
| Commlinks, cyberdecks | `device` |
| Programs, ammunition | `program`, `ammo` |
| Implants | `cyberware`, or `bioware` with „bio“ in the type or name |
| Items, foci, widgets | `equipment` |
| Spells, complex forms, adept powers | `spell`, `complex_form`, `adept_power` |
| Spirits, sprites | `call_in_action` |
| Submersions, initiations | `echo`, `metamagic` |
| Vehicles, drones | separate `vehicle` actors |
| Notes, stream, tradition, personal data | description of the actor |

Entries are named `Type: Name` if the app has a type for them.

## Known limits

- The app often keeps ammunition, accuracy and recoil only as text. The text stays in the weapon's description, but the values have to be set by hand.
- Bonuses on skills don't count towards dice pools in Foundry.
- Vehicles have no template yet, so their files only contain name, type and values.
- Not every item type has been tested in Foundry. Characters with spells, spirits or attribute bonuses are worth a closer look after the import.

## Develop

```sh
node --test srhchar-to-foundry/test
```

`data/character-template.json` holds the structure and skills of an empty character. If an update of the system changes the skills, export a newly created character from Foundry and create the template again:

```sh
node srhchar-to-foundry/tools/make-template.mjs <export>
```

Keep real characters out of the repository. The folder `dev-test/` in this folder is ignored by Git and meant for test files and generated output. Keep that in mind if you copy this folder by hand, because the copy includes it.

## License

The template contains skill data from the [Shadowrun 5e system for Foundry VTT](https://github.com/SR5-FoundryVTT/SR5-FoundryVTT), which is licensed under GPL-3.0, like this project.
