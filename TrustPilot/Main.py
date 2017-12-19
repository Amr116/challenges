import sys
import time
from Filter import filterWordList
from Combine import combineAndVerify

def main(fileName, anagramCombine, md5Hash, anagramLength):

	start = time.time()
	dictionary = filterWordList(fileName, anagramCombine)
	secret_phrase = combineAndVerify(dictionary, md5Hash, anagramLength)
	print("Secret Phrases::\n"+str(secret_phrase))
	print("Total Execution time = {0:.3f}".format(time.time() - start))

if __name__ == '__main__':

	file_name = "wordlist"

	anagram = "poultry outwits ants"

	anagram_combine= anagram.replace(" ","")
	anagram_length = len(anagram_combine)

	md5_hash = ["e4820b45d2277f3844eac66c903e84be", "23170acc097c24edb98fc5488ab033fe", "665e5bcb0c20062fe8abaaf4628bb154"]

	main(file_name, anagram_combine, md5_hash, anagram_length)
