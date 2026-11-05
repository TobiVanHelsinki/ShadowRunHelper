const decoder = new TextDecoder('windows-1252');

/** RTF destinations whose content is metadata, not text. */
const SKIPPED_DESTINATIONS = new Set(['fonttbl', 'colortbl', 'stylesheet', 'info', 'pict', 'header', 'footer']);

export function clean(value) {
  return typeof value === 'string' ? value.trim() : '';
}

export function escapeHtml(value) {
  return String(value)
    .replaceAll('&', '&amp;')
    .replaceAll('<', '&lt;')
    .replaceAll('>', '&gt;')
    .replaceAll('"', '&quot;');
}

/** Extracts the plain text of an RTF document. Formatting is dropped, paragraphs stay. */
export function rtfToText(rtf) {
  const stack = [];
  let skipping = false;
  let unicodeFallback = 1;
  let pendingFallback = 0;
  let out = '';
  let i = 0;

  const emit = (text) => {
    if (skipping) return;
    if (pendingFallback > 0) {
      pendingFallback -= 1;
      return;
    }
    out += text;
  };

  while (i < rtf.length) {
    const ch = rtf[i];
    if (ch === '{') {
      stack.push({ skipping, unicodeFallback });
      i += 1;
    } else if (ch === '}') {
      const state = stack.pop();
      if (state) ({ skipping, unicodeFallback } = state);
      i += 1;
    } else if (ch === '\r' || ch === '\n') {
      i += 1;
    } else if (ch !== '\\') {
      emit(ch);
      i += 1;
    } else {
      const next = rtf[i + 1];
      if (next === '\\' || next === '{' || next === '}') {
        emit(next);
        i += 2;
      } else if (next === "'") {
        emit(decoder.decode(Uint8Array.of(parseInt(rtf.slice(i + 2, i + 4), 16))));
        i += 4;
      } else if (next === '*') {
        skipping = true;
        i += 2;
      } else {
        const match = /^([a-zA-Z]+)(-?\d+)? ?/.exec(rtf.slice(i + 1, i + 40));
        if (!match) {
          i += 2;
          continue;
        }
        const [token, word, parameter] = match;
        i += 1 + token.length;
        if (SKIPPED_DESTINATIONS.has(word)) skipping = true;
        else if (word === 'par' || word === 'line') out += skipping ? '' : '\n';
        else if (word === 'tab') emit('\t');
        else if (word === 'uc') unicodeFallback = Number(parameter);
        else if (word === 'u') {
          const code = Number(parameter);
          emit(String.fromCharCode(code < 0 ? code + 65536 : code));
          pendingFallback = unicodeFallback;
        }
      }
    }
  }
  return out;
}

/** Turns plain text or RTF into a list of HTML paragraphs. */
export function paragraphs(value) {
  const text = clean(value);
  if (!text) return [];
  const plain = text.startsWith('{\\rtf') ? rtfToText(text) : text;
  return plain
    .split(/\r\n|\n|\r/)
    .map((line) => line.trim())
    .filter(Boolean)
    .map((line) => `<p>${escapeHtml(line)}</p>`);
}
