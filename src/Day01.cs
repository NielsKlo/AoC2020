using System;
using System.IO;
using System.Collections.Generic;

namespace AOC2020
{
    class DayOne
    {
        List<int> lowNumbers = new List<int>();
        List<int> medNumbers = new List<int>();
        List<int> highNumbers = new List<int>();
        int numberOne;
        int numberTwo;
        int numberThree;

        public void Run(){
            MakeLists(lowNumbers, highNumbers, medNumbers);
            FindNumbers(out numberOne, out numberTwo, out numberThree);
            System.Console.WriteLine($"Number one: {numberOne}, Number two: {numberTwo}, Number three: {numberThree}");
            System.Console.WriteLine($"Product is: {numberOne * numberTwo * numberThree}");
        }

        void MakeLists(List<int> lowNumbers, List<int> medNumbers, List<int> highNumbers){
            using (StreamReader reader = File.OpenText("input_files/Day01.txt")){
                string s;
                while((s = reader.ReadLine()) != null){
                    int number = Int32.Parse(s);
                    
                    highNumbers.Add(number);
                    lowNumbers.Add(number);
                    medNumbers.Add(number);
                }
            }
        }

        void FindNumbers(out int numberOne, out int numberTwo, out int numberThree){
            numberOne = 0; numberTwo = 0; numberThree = 0;
            foreach(int lowNum in lowNumbers){
                foreach(int highNum in highNumbers){
                    foreach(int medNum in medNumbers){
                        if(lowNum + highNum + medNum == 2020){
                            numberOne = lowNum;
                            numberTwo = medNum;
                            numberThree = highNum;
                            return;
                        }
                    }
                }
            }
        }
    }

}

//TODO: IO to get two lists.
//Loop through list One.
//Remove first element from list Two after each loop.