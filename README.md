# numbers-to-words-converter
Convert numbers into words. Code is written in C#.

Convert any positive numerical sequence of digits within a valid range of an integer integral data type, into strings (words).
The algorithm uses arrays, for loops and if-else-if statements as well as several local functions inside the main program.

There are declarations of global arrays with fixed elements of numbers and strings which are used according with the range of the input number. To achive defensive programming, there is also a boolean to handle the while loop which controls the data entered by the user at run-time.

Because the main purpose of the program is to convert a sequence of digits and transform them into a complete sentence, like a bank draft, there is no practical sense for a two or more digit number to start from zero (0).

With the use of method TryParse(), a three-step verification is performed on the given number:
- If the number consists of more than one digits its first digit must not be equal to zero (0).
- The input number must not exceed the size of an integral data type. We limit the usage of numbers up to an integer.
- The input number must consists of numbers only, and not of any other character.

According with the digits of the given number, we use the string.Length inside a switch statement to control multiple conditions.

* Local Functions *

StringBuilder():
It is used to create a combo of two words that correspond to two digits of the input number.
It takes three formal parameters, one is the array that contains the stored numbers the user submitted.
The second parameter represents the input number and the third is used as an index for method Remove() which removes characters according with the value of the index variable.
There is an iteration as many times as the input number size and use of the method Concat() to add the hyphen symbol (-) between the two strings.

PopulateArray():
It initialize an array with the input number(s) given by the user. It takes an array as a formal parameter.

ConvertDigitToChar():
It takes the given number - one digit only within a range of 0..9 - and returns a string representation of that number or digit.
In order to acquire only the first digit, it uses the method Substring() to extract it from the input number.

ConvertTwoDigitsToStrings():
It converts two digits within a valid range of numbers from 10 to 99 into strings. It is called when there is a combination of two numbers in order to seperate them among others.
Two arrays of string are used, one is for the stored numbers and it is initialized with the size equal to the size of the input number.
The second is populated with the input numbers by calling the appropriate function.
There are several if statements to handle the different conditions of the input digits and the returned string number is created by calling the function StringBuilder().

ConvertDigitsToStrings():
The function is called when we want to handle a number of more than two digits. Moreover, it uses the same arrays of strings as the previous function.
It includes three string variables and each one holds the value of the string combination of two digits separately.
Firstly, the array of stored numbers is initialized with the corresponding numbers as characters by calling function ConvertDigitToChar().

Furthermore, according with the length of the given number (or the number of the digits), a two digit string number from 10 to 99 is created by calling the method Concat() before calling the function ConvertTwoDigitsToStrings() which then takes the returned value of the previously called function as a formal parameter and convert it to a combo of two strings (or words), for example 'Ten' or 'Twenty-Five'.

This is done as many times as required in order to achive the transformation of a number with more than two digits.
Lastly, to represent the fixed words like 'Hundred' or 'Million' we use the corresponding array of numsAsStrings_beyond99[].

