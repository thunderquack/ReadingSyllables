import rusyllab #https://github.com/Koziev/rusyllab
from os import listdir
from os.path import isfile, join
from collections import Counter
import json

onlyfiles = [f for f in listdir("./Texts") if isfile(join("./Texts", f))]

syllables_to_remove = ['князь', 'княж']

u_syllables = Counter()
for f in onlyfiles:

    file_name = join("./Texts", f)
    file_content = open(file_name, "r", encoding="utf-8")
    sx = rusyllab.split_words(file_content.read().lower().split())
    without_len_one = [x for x in sx if len(x) > 1]
    u_syllables = u_syllables + Counter(without_len_one)

for ignore in syllables_to_remove:
    del u_syllables[ignore]
    
f = open('result.json','w')
f.write(json.dumps([{'name':key, 'value':value} for key,value in u_syllables.most_common()]))
f.close()

print('job is done')