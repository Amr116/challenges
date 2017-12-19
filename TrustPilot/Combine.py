import time
import hashlib
import multiprocessing as mp

from itertools import chain, combinations, permutations

def wordPermutations(words):
	return chain(*map(lambda x: permutations(words, x), [3]))

def wordCombination(words):
	return chain(*map(lambda x: combinations(words, x), [3]))

def combineAndVerify(dictionary, md5Hash, anagramLength):
	found = 0
	start = time.time()
	secret_phrase = []
	# Generate the combinations
	for wc in wordCombination(dictionary):
		# Check if the length of items of this combination is equal to anagram length
		if sum(len(item) for item in wc) == anagramLength:
			# Generate the permutations
			for wp in wordPermutations(wc):
				# joining those words together with space between them,
				# Calculate the md5 hash, and check the results exists in md5Hash
				if hashlib.md5(" ".join(wp).encode()).hexdigest() in md5Hash:
					execute_time = ("Execution time = {0:.3f}".format(time.time() - start))
					secret_phrase.append((" ".join(wp), hashlib.md5(" ".join(wp).encode()).hexdigest(), execute_time))
					found += 1
					# We are only careing about 3 secret phrase
					if found == 3:
						return secret_phrase

		if found == 3:
			return secret_phrase

	return secret_phrase
