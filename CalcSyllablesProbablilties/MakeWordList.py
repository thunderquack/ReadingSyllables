import rusyllab  # https://github.com/Koziev/rusyllab
from os import listdir
from os.path import isfile, join
from collections import Counter
import json

words_file_name = './Words/RussianWords.txt'
words = open(words_file_name, "r", encoding="utf-8").read().splitlines()


print('finished')
