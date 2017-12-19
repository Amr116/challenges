import string

# Check if word is ASCII
def is_ascii(word):
    return all(ord(c) < 128 for c in word)

def notPartOfPhrase(word, anagram):
	# ASCII subtract anagram characters.
	invert_anagram = set(string.ascii_lowercase).difference(set(anagram))
	# Non ASCII Character
	non_ascii = set(string.printable).difference(set(string.ascii_lowercase))

	# Check intersection 
	if set(invert_anagram) & set(word) or set(non_ascii) & set(word):
		return False

	return True


def contains(word,anagram):
	# check intersection
	if set(anagram) & set(word):
		# check equality of characters numbers.
		for char in anagram:
			if word.count(char)>anagram.count(char):
				return False
		return True
	return False


def filterWordList(fileName, anagramCombine):
	wordlist = set(open(fileName))
	wordlist = set(map(lambda s: s.strip(), wordlist))

	dictionary = []
	# Only interested on word, that 
	# equal on the number characters in anagram ex.: ttttry
	# all it's characters is part of anagram
	# contains only ascii characters.
	for word in wordlist:
		if contains(word,anagramCombine) and notPartOfPhrase(word, anagramCombine) and is_ascii(word):
			dictionary.append(word)

	return dictionary