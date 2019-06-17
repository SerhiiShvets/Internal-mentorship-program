using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TestProject.Common.Core.Interfaces;

namespace TestProject.TaskLibrary.Tasks.Lesson3
{
    public class Task1 : IRunnable
    {
        public void Run()
        {
            string[] strings =
                {
                 "You only live forever in the lights you make",
                 "When we were young we used to say",
                 "That you only hear the music when your heart begins to break",
                 "Now we are the kids from yesterday"
                };
            //1
            for (int i = 0; i < strings.Length; i++)
            {
                Console.Write($"The number of words in the string *{strings[i]}* is equal to ");
                CalculateTheNumberOfWordsInEachSentence(strings[i]);
            }
            //2
            var listOfWords = new List<string>();
            foreach (string s in strings)
            {
                var splitString = s.Split(new char[] { ' ' });
                listOfWords.AddRange(splitString);
            }
            var arrayOfWords = new string[listOfWords.Count];

            for (int i = 0; i < arrayOfWords.Length; i++)
            {
                arrayOfWords[i] = listOfWords[i];
            }

            IEnumerable<string> wordsThatStartWithVowels =  from word
                                                            in arrayOfWords
                                                            where word.ToLower().StartsWith('a') ||
                                                                  word.ToLower().StartsWith('o') ||
                                                                  word.ToLower().StartsWith('e') ||
                                                                  word.ToLower().StartsWith('i') ||
                                                                  word.ToLower().StartsWith('u')
                                                            select word;
            Console.WriteLine("The words that start with vowels are: " + string.Join(", ", wordsThatStartWithVowels)+".");
            //3
            var theLongestWord = from word
                                  in arrayOfWords
                                  where word.Length == arrayOfWords.Max(s => s.Length)
                                  select word;
            Console.WriteLine("The longest word is "+string.Join(" ", theLongestWord));
            //4
            var numbersOfWordsInSentences = from str in strings
                                            select str.Split(new char[] { ' ' }).Length;
            Console.WriteLine("The average number of words in a sentence is " 
                + string.Join(" ", numbersOfWordsInSentences.Average()));
            //5
            var wordsAscending = arrayOfWords.OrderBy(s => s)         
                                             .Select(s => s.ToLower());

            Console.WriteLine("The distinct words in alphabetical order are: "
                + string.Join(" ", wordsAscending.Distinct()));

            Console.ReadKey();
        }
        //1
        public static void CalculateTheNumberOfWordsInEachSentence(string stringToCalculateWordsIn)
        {
            string[] splitString = stringToCalculateWordsIn.Split(new char[] {' '});

            
            Console.WriteLine(string.Join(" ", splitString.Count()));

        }

        
    }
}
