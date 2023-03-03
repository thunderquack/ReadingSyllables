import rusyllab  # https://github.com/Koziev/rusyllab
from os import listdir
from os.path import isfile, join
from collections import Counter
import json

print('starting to process wordlist')

file_name = 'words_rating.json'
file_content = open(file_name, "r", encoding="utf-8")
words = json.load(file_content)

sx = rusyllab.split_words(words.keys())
without_len_one = [x for x in sx if len(x) > 1]
u_syllables = Counter(without_len_one)

for w in words:
    sx = rusyllab.split_words(w)
    without_len_one = [x for x in sx if len(x) > 1]
    u_syllables = u_syllables + Counter(without_len_one)

onlyfiles = [f for f in listdir("./Texts") if isfile(join("./Texts", f))]

#syllables_to_remove = ["�����", "����"]

for f in onlyfiles:
    file_name = join("./Texts", f)
    print('file name', file_name)
    file_content = open(file_name, "r", encoding="utf-8")
    sx = rusyllab.split_words(file_content.read().lower().split())
    without_len_one = [x for x in sx if len(x) > 1]
    u_syllables = u_syllables + Counter(without_len_one)

for ignore in syllables_to_remove:
    del u_syllables[ignore]

f = open("result.json", "w")
f.write(
    json.dumps(
        [{"id":idx+1, "name": element[0], "value": element[1]} for idx, element in enumerate(u_syllables.most_common())]
    )
)
f.close()

print("job is done")