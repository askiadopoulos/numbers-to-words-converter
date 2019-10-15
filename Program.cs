using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersToWordsConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Arrays
            string[] digitAsChars = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] digitAsStrings = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] numbersAsStrings_per10 = new string[] { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string[] numbersAsStrings_11To19 = new string[] { "Eleven", "Twelve", "Thirteen", "Forteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] numsAsStrings_beyond99 = new string[] { "Hundred", "Thousand", "Million", "Billion" };

            // Global variables
            string inputNumber = string.Empty;
            bool inputIsValid = false;

            // Check the input whether it is a valid integer within a range or character(s)
            while (inputIsValid == false)
            {
                Console.Write("Convert this Number: ");
                inputNumber = Console.ReadLine();

                if (long.TryParse(inputNumber, out long userInput))
                {
                    if (inputNumber.Length > 1 && inputNumber.Substring(0, 1).Equals("0"))
                    {
                        Console.WriteLine("The first digit of a given number must be > 0 !\n");
                    }
                    else if (long.Parse(inputNumber) > 2147483647)
                    {
                        Console.WriteLine("Given number must be within a valid range of a positive integer (0..2147483647) !\n");
                        inputIsValid = false;
                    }
                    else
                    {
                        inputIsValid = true;
                    }
                }
                else
                {
                    Console.WriteLine("NaN, please try again...\n");
                    inputIsValid = false;
                }
            }

            while (inputIsValid)
            {
                switch (inputNumber.Length)
                {
                    case 1:
                        Console.WriteLine("\nAnswer: {0}", ConvertDigitToChar(inputNumber));
                        break;
                    case 2:
                        Console.WriteLine("\nAnswer: {0}", ConvertTwoDigitsToStrings(inputNumber));
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        Console.WriteLine("\nAnswer: {0}", ConvertDigitsToStrings(inputNumber));
                        break;
                }
                inputIsValid = false;
            }
            Console.WriteLine();

            // Local Functions

            // Converts a single positive numerical digit (0..9) into one single character
            string ConvertDigitToChar(string numberAsInput)
            {
                string number = string.Empty;

                for (int i = 0; i < digitAsChars.Length; i++)
                {
                    if (numberAsInput.Substring(0, 1) == digitAsChars[i])
                    {
                        number = digitAsStrings[i];
                        break;
                    }
                }
                return number;
            }

            // Converts two positive numerical digits (10..99) into strings
            string ConvertTwoDigitsToStrings(string numberAsInput)
            {
                string[] arrayOfStoredNumbers = new string[numberAsInput.Length];
                string[] arrayOfInputNumbers = PopulateArray(numberAsInput);
                string number = string.Empty;

                // If the first digit is 0
                if (arrayOfInputNumbers[0] == digitAsChars[0])
                {
                    arrayOfStoredNumbers[0] = digitAsStrings[0];

                    for (int i = 0; i < digitAsChars.Length; i++)
                    {
                        if (arrayOfInputNumbers[1] == digitAsChars[i])
                        {
                            arrayOfStoredNumbers[1] = digitAsStrings[i];
                            break;
                        }
                    }
                    number = StringBuilder(arrayOfStoredNumbers, numberAsInput, 1);
                }

                // If the second digit is 0
                else if (arrayOfInputNumbers[1] == digitAsChars[0])
                {
                    for (int i = 0; i < digitAsChars.Length; i++)
                    {
                        if (arrayOfInputNumbers[0] == digitAsChars[i])
                        {
                            arrayOfStoredNumbers[0] = numbersAsStrings_per10[i - 1];
                            break;
                        }
                    }
                    number = StringBuilder(arrayOfStoredNumbers, numberAsInput, 2);
                }
                // If the first digit is 1
                else if (arrayOfInputNumbers[0] == digitAsChars[1])
                {
                    for (int i = 0; i < digitAsChars.Length; i++)
                    {
                        if (arrayOfInputNumbers[1] == digitAsChars[i])
                        {
                            arrayOfStoredNumbers[0] = numbersAsStrings_11To19[i - 1];
                            break;
                        }
                    }
                    number = StringBuilder(arrayOfStoredNumbers, numberAsInput, 2);
                }
                else
                {
                    for (int i = 0; i < digitAsChars.Length; i++)
                    {
                        if (arrayOfInputNumbers[0] == digitAsChars[i])
                        {
                            arrayOfStoredNumbers[0] = numbersAsStrings_per10[i - 1];
                        }
                        if (arrayOfInputNumbers[1] == digitAsChars[i])
                        {
                            arrayOfStoredNumbers[1] = digitAsStrings[i];
                        }
                    }
                    number = StringBuilder(arrayOfStoredNumbers, numberAsInput, 1);
                }
                return number;
            }

            // Converts positive numerical digits (100..2.147.483.647) into strings
            string ConvertDigitsToStrings(string numberAsInput)
            {
                string[] arrayOfStoredNumbers = new string[numberAsInput.Length];
                string[] arrayOfInputNumbers = PopulateArray(numberAsInput);
                string comboOne = string.Empty, comboTwo = string.Empty, comboThree = string.Empty;

                for (int i = 0; i < arrayOfStoredNumbers.Length; i++)
                {
                    arrayOfStoredNumbers[i] = ConvertDigitToChar(arrayOfInputNumbers[i]);
                }

                if (numberAsInput.Length == 3)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[1], arrayOfInputNumbers[2]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);

                    return arrayOfStoredNumbers[0] + " " + numsAsStrings_beyond99[0] + " " + comboOne;
                }
                else if (numberAsInput.Length == 4)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[2], arrayOfInputNumbers[3]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);

                    return arrayOfStoredNumbers[0] + " " + numsAsStrings_beyond99[1] + " " + arrayOfStoredNumbers[1] + " " +
                           numsAsStrings_beyond99[0] + " " + comboOne;
                }
                else if (numberAsInput.Length == 5)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[0], arrayOfInputNumbers[1]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);
                    comboTwo = string.Concat(arrayOfInputNumbers[3], arrayOfInputNumbers[4]);
                    comboTwo = ConvertTwoDigitsToStrings(comboTwo);

                    return comboOne + " " + numsAsStrings_beyond99[1] + " " + arrayOfStoredNumbers[2] + " " + numsAsStrings_beyond99[0]
                                  + " " + comboTwo;
                }
                else if (numberAsInput.Length == 6)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[1], arrayOfInputNumbers[2]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);
                    comboTwo = string.Concat(arrayOfInputNumbers[4], arrayOfInputNumbers[5]);
                    comboTwo = ConvertTwoDigitsToStrings(comboTwo);

                    return arrayOfStoredNumbers[0] + " " + numsAsStrings_beyond99[0] + " " + comboOne + " " + numsAsStrings_beyond99[1]
                                                   + " " + arrayOfStoredNumbers[3] + " " + numsAsStrings_beyond99[0] + " " + comboTwo;
                }
                else if (numberAsInput.Length == 7)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[2], arrayOfInputNumbers[3]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);
                    comboTwo = string.Concat(arrayOfInputNumbers[5], arrayOfInputNumbers[6]);
                    comboTwo = ConvertTwoDigitsToStrings(comboTwo);

                    return arrayOfStoredNumbers[0] + " " + numsAsStrings_beyond99[2] + " " + arrayOfStoredNumbers[1] + " "
                                                   + numsAsStrings_beyond99[0] + " " + comboOne + " " + numsAsStrings_beyond99[1]
                                                   + " " + arrayOfStoredNumbers[4] + " " + numsAsStrings_beyond99[0] + " " + comboTwo;
                }
                else if (numberAsInput.Length == 8)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[0], arrayOfInputNumbers[1]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);
                    comboTwo = string.Concat(arrayOfInputNumbers[3], arrayOfInputNumbers[4]);
                    comboTwo = ConvertTwoDigitsToStrings(comboTwo);
                    comboThree = string.Concat(arrayOfInputNumbers[6], arrayOfInputNumbers[7]);
                    comboThree = ConvertTwoDigitsToStrings(comboThree);

                    return comboOne + " " + numsAsStrings_beyond99[2] + " " + arrayOfStoredNumbers[2] + " " + numsAsStrings_beyond99[0]
                                  + " " + comboTwo + " " + numsAsStrings_beyond99[1] + " " + arrayOfStoredNumbers[5] + " "
                                  + numsAsStrings_beyond99[0] + " " + comboThree;
                }
                else if (numberAsInput.Length == 9)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[1], arrayOfInputNumbers[2]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);
                    comboTwo = string.Concat(arrayOfInputNumbers[4], arrayOfInputNumbers[5]);
                    comboTwo = ConvertTwoDigitsToStrings(comboTwo);
                    comboThree = string.Concat(arrayOfInputNumbers[7], arrayOfInputNumbers[8]);
                    comboThree = ConvertTwoDigitsToStrings(comboThree);

                    return arrayOfStoredNumbers[0] + " " + numsAsStrings_beyond99[0] + " " + comboOne + " " + numsAsStrings_beyond99[2]
                                                   + " " + arrayOfStoredNumbers[3] + " " + numsAsStrings_beyond99[0] + " " + comboTwo
                                                   + " " + numsAsStrings_beyond99[1] + " " + arrayOfStoredNumbers[6] + " "
                                                   + numsAsStrings_beyond99[0] + " " + comboThree;
                }
                else if (numberAsInput.Length == 10)
                {
                    comboOne = string.Concat(arrayOfInputNumbers[2], arrayOfInputNumbers[3]);
                    comboOne = ConvertTwoDigitsToStrings(comboOne);
                    comboTwo = string.Concat(arrayOfInputNumbers[5], arrayOfInputNumbers[6]);
                    comboTwo = ConvertTwoDigitsToStrings(comboTwo);
                    comboThree = string.Concat(arrayOfInputNumbers[8], arrayOfInputNumbers[9]);
                    comboThree = ConvertTwoDigitsToStrings(comboThree);

                    return arrayOfStoredNumbers[0] + " " + numsAsStrings_beyond99[3] + " " + arrayOfStoredNumbers[1] + " "
                                                   + numsAsStrings_beyond99[0] + " " + comboOne + " " + numsAsStrings_beyond99[2] + " "
                                                   + arrayOfStoredNumbers[4] + " " + numsAsStrings_beyond99[0] + " " + comboTwo + " "
                                                   + numsAsStrings_beyond99[1] + " " + arrayOfStoredNumbers[7] + " "
                                                   + numsAsStrings_beyond99[0] + " " + comboThree;
                }
                else
                    Console.WriteLine("No more than 10 digit numbers are allowed\n");

                return comboOne;
            }

            // Initialize an array with digits as characters
            string[] PopulateArray(string numberAsInput)
            {
                string[] arrayOfStoredNumbers = new string[numberAsInput.Length];

                for (int i = 0; i < numberAsInput.Length; i++)
                {
                    arrayOfStoredNumbers[i] = numberAsInput.Substring(i, 1);
                }
                return arrayOfStoredNumbers;
            }

            // Build a string from a sequence of digits
            string StringBuilder(string[] arrayOfStoredNumbers, string numberAsInput, int index)
            {
                string number = string.Empty;

                for (int i = 0; i < numberAsInput.Length; i++)
                {
                    number += string.Concat(arrayOfStoredNumbers[i], "-");
                }
                number = number.Remove(number.Length - index, index);
                return number;
            }

        }
    }
}
