import rusyllab  # https://github.com/Koziev/rusyllab
from collections import Counter
import json

print('starting to process wordlist')

file_name = 'words_rating.json'
file_content = open(file_name, "r", encoding="utf-8")
words = json.load(file_content)

sx = rusyllab.split_words(words.keys())
without_len_one = [x for x in sx if len(x) > 1]
u_syllables = Counter(without_len_one)

f = open("result.json", "w", encoding="utf-8")
f.write(
    json.dumps(
        [{"id":idx+1, "name": element[0], "value": element[1]} for idx, element in enumerate(u_syllables.most_common())],
        ensure_ascii=False
    )
)
f.close()

print("job is done")