from asyncio.windows_events import NULL
import rusyllab  # https://github.com/Koziev/rusyllab
from os import listdir
from os.path import isfile, join
from collections import Counter
import json

words_file_name = "./Words/RussianWords.txt"
words = open(words_file_name, "r", encoding="utf-8").read().splitlines()
syllables_file_name = "./result.json"
syllables = json.load(open(syllables_file_name, "r", encoding="utf-8"))

words_list = {}

for word in words:
    sx = rusyllab.split_word(word.lower())
    rating = 0
    for syllable in sx:
        element = next((s for s in syllables if s["name"] == syllable), 0)
        if element != 0 and element["id"] > rating:
            rating = element["id"]
    if rating != 0:
        words_list[word] = rating
words_list = sorted(words_list.items(), key=lambda item: item[1])

words_file_name = "./syllables_rating.json"
words = open(words_file_name, "w", encoding="utf-8")
json.dump(words_list, words)

print("finished")
