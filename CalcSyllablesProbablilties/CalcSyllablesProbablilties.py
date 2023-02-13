import rusyllab #https://github.com/Koziev/rusyllab
from os import listdir
from os.path import isfile, join
from collections import Counter

onlyfiles = [f for f in listdir("./Texts") if isfile(join("./Texts", f))]

for f in onlyfiles:
    file_name = join("./Texts", f)
    file_content = open(file_name, "r", encoding="utf-8")
    sx = rusyllab.split_words(file_content.read().lower().split())
    without_len_one = [x for x in sx if len(x) > 1]
    u_syllables = Counter(without_len_one)
    print(u_syllables)