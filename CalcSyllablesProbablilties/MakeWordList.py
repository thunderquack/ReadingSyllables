from asyncio.windows_events import NULL
import rusyllab  # https://github.com/Koziev/rusyllab
from os import listdir
from os.path import isfile, join
from collections import Counter
import json

words_file_name = './Words/RussianWords.txt'
words = open(words_file_name, "r", encoding="utf-8").read().splitlines()
syllables_file_name = './result.json'
syllables = json.load(open(syllables_file_name, 'r', encoding='utf-8'))

for word in words:
    sx = rusyllab.split_word(word.lower())
    rating = len(syllables) + 1
    for syllable in sx:
        element = next(s for s in syllables if s['name']==syllable)
        if (element['id']<rating):
            rating = element['id']
    print(word, rating)


print('finished')
