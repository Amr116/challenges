#!/usr/bin/env python3

import re
import sys
import hashlib

def TpChallenge():
	
	anagram = "poultry outwits ants"
	tHash = "4624d200580677270a54ccff86b9610e"

	# list words of length 4
	pool4 = []
	# list words of length 7
	pool7 = []
	with open('wordlist', 'r') as fh:

		for line in fh:
			lh = line.strip()

			if(len(lh) is 4):

				if all(i in anagram for i in lh ):
					pool4.append(lh)

			if(len(lh) is 7 ):

				if all(i in anagram for i in lh ):
					pool7.append(lh)

		# length of pool7 is the smallest
		for i in range(0, len(pool7)-1):

			for j in range(i+1, len(pool7)):

				for k in range(0, len(pool4)):

					secretPhrase = ' '.join([pool7[i], pool7[j], pool4[k]])
					
					mdSum = hashlib.md5()
					mdSum.update(secretPhrase.encode())
					found = mdSum.hexdigest()
					if found == tHash:
						print(secretPhrase)
						sys.exit(0)

		

if __name__ == '__main__':
	TpChallenge()



