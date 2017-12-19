# Solution for Trustpilot challenge <followthewhiterabbit>
* challenge Name : followthewhiterabbit
* Challenge url: http://followthewhiterabbit.trustpilot.com/cs/step3.html
* Prerequisites: Python 3+
* Usage: python3 Main.py

## Author

* **Amr Elsayed**
<p>You are welcome to send me an email regards to any technical questions<a href="mailto:<a href='mailto:amr.elsayed.dk@gmail.com'>amr.elsayed.dk@gmail.com</a>"> @email</a></p>

### Solution Steps
The solution of the problem divided into two modules:

1. Filter the given file [wordlist](https://github.com/Amr116/chllanges/Trustpilot) according to the information on challenge url.
	- anagram of the phrase is: "poultry outwits ants"
	```
	The above line tells me a lot.
	```
		1. All characters are ASCII lowercase.
		2. What is target characters?
		3. Length of phrase.
		4. Maximum number of each characters in secret phease.
		5. How many words in secret phease (Suggest: Three elements)


2. Generate combinations, permutations and Verify md5 hash.
	- The Filter module will produce new words list of length 1659.
		```
		1. Generate all the combinations possible with three elements.
		2. For each combinations check the length of it's charachters.
		
		```
		- if length == anagrame characheters length, then
			
			```
			1. Generate all the permutations possible for those three words
			2. for each permutations, joining those words together with space between them.
			3. Calculate the md5 hash for the phrase

			# if the results of the above step exists in the md5 hash list, then
				
				```
				1. append the found secret phrase to the return variable