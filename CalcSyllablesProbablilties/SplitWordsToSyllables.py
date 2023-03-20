import json
import rusyllab  # https://github.com/Koziev/rusyllab

file_name = "words_rating.json"
file_content = open(file_name, "r", encoding="utf-8")
res = json.load(file_content)
words_out = {}
for w in res.keys():
    sx = rusyllab.split_words(w.split())
    joined_word = "|".join(sx)
    sxset = set()
    for s in sx:
        if len(s) > 1:
            sxset.add(s)
    words_out[w] = {"split_word": joined_word, "syllables": sxset}

for w in words_out:
    words_out[w]['syllables'] = list(words_out[w]['syllables'])

file_name = "structured_words.json"
f = open(file_name, "w", encoding="utf-8")
f.write(
    json.dumps(
        words_out,
        ensure_ascii=False,
    )
)
print("job is done")