import pymorphy2
from os import listdir
from os.path import isfile, join
from tqdm import tqdm
import rusyllab  # https://github.com/Koziev/rusyllab
import json
import regex

def only_russian_letters(name):
    res = regex.search(r"^\p{IsCyrillic}*$", name)
    if res:
        return res.string
    else:
        return ''

morph = pymorphy2.MorphAnalyzer()

print("starting to process files")
onlyfiles = [f for f in listdir("./Texts") if isfile(join("./Texts", f))]

words_set = set()

print("normalizing words")
for f in onlyfiles:
    file_name = join("./Texts", f)
    print("file name", file_name)
    file_content = open(file_name, "r", encoding="utf-8")
    words = file_content.read().lower().split()
    print("number of words:", len(words))
    i = 0
    for w in tqdm(words):
        word = only_russian_letters(w)
        if len(word) > 1:
            res = morph.parse(word)[0]
            if res.tag.POS == "NOUN":
                words_set.add(res.normal_form)
                i += 1

    print("file processed, words added:", i)

words_list = {}
syllables_file_name = "./syllables_rating.json"
syllables = json.load(open(syllables_file_name, "r", encoding="utf-8"))
for w in tqdm(words_set):
    sx = rusyllab.split_word(w)
    res = 0
    for syllable in sx:
        if len(syllable) == 1:
            continue
        element = next((s for s in syllables if s["name"] == syllable), 0)
        if element == 0:
            res = 10000
            break
        if element["id"] > res:
            res = element["id"]
    words_list[w] = res
words_list = dict(sorted(words_list.items(), key=lambda item: item[1]))
words_file_name = "./words_rating.json"
words = open(words_file_name, "w", encoding="utf-8")
json.dump(words_list, words, ensure_ascii=False)

print("finished")
