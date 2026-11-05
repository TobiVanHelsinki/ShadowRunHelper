import { mergeSkills } from './skills.mjs';

const ID_CHARS = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

function newId() {
  return Array.from({ length: 16 }, () => ID_CHARS[Math.floor(Math.random() * ID_CHARS.length)]).join('');
}

function isPlainObject(value) {
  return value !== null && typeof value === 'object' && !Array.isArray(value);
}

/** Values of source win, nested objects are merged, arrays are replaced. */
function deepMerge(target, source) {
  const result = { ...target };
  for (const [key, value] of Object.entries(source)) {
    result[key] = isPlainObject(value) && isPlainObject(result[key]) ? deepMerge(result[key], value) : value;
  }
  return result;
}

/** The form of effects in an export of shadowrun5e 0.37.9: every change names a target of the effect. */
function bonusEffectDocument({ changes, sources }, stats) {
  const targetId = newId();
  return {
    _id: newId(),
    name: 'Boni aus SRHChar-Verknüpfungen',
    type: 'base',
    description: sources.join('<br>'),
    disabled: false,
    transfer: true,
    system: {
      changes: changes.map((change) => ({
        ...change,
        value: Number(change.value),
        priority: 0,
        target: targetId,
        phase: 'initial',
      })),
      targets: [{ id: targetId, applyTo: 'actor', conditions: [], onlyForItemTest: false, name: '' }],
    },
    _stats: stats,
  };
}

/**
 * Turns converted data into documents for "Import Data" in Foundry.
 * @param converted result of convertCharacter
 * @param template the character written by tools/make-template.mjs
 */
export function buildDocuments(converted, template) {
  const { items: templateItems, ...templateActor } = template;
  const stats = template._stats;
  const withId = (item) => ({ ...item, _id: newId(), _stats: stats });
  const { name } = converted.actor;

  const character = {
    ...templateActor,
    name,
    prototypeToken: { ...template.prototypeToken, name },
    system: deepMerge(template.system, converted.actor.system),
    items: [...mergeSkills(templateItems, converted.skills), ...converted.items].map(withId),
    effects: converted.bonusEffect ? [bonusEffectDocument(converted.bonusEffect, stats)] : [],
  };

  const vehicles = converted.vehicles.map((vehicle) => ({ ...vehicle, _stats: stats }));

  return { character, vehicles };
}
