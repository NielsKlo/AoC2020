using System;
using System.Collections.Generic;

class DayNine{
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day09.txt");
        List<long> longList = GetNumbers(strList);
        long irregularNumber = 144381670; //GetFirstIrregularNumber(longList);
        List<long> contiguousList = FindContiguousSet(irregularNumber, longList);
        long answer = FindEncryptionWeakness(contiguousList);
        System.Console.WriteLine(answer);
    }

    private List<long> GetNumbers(List<string> strList){
        List<long> longList = new List<long>();
        foreach(string str in strList){
            longList.Add(long.Parse(str));
        }
        return longList;
    }

    private long GetFirstIrregularNumber(List<long> longList){
        bool contained = true;
        long number = 0;
        for(int i = 25; i < longList.Count && contained; i++){
            contained = CheckIfContained(longList, i);
            if(!contained) number = longList[i];
        }
        return number;
    }

    private bool CheckIfContained(List<long> longList, int index){
        List<long> possibilities = new List<long>();
        List<long> previousNumbers = longList.GetRange(index - 25, 25);

        for(int i = 0; i < 24; i++){
            for(int j = 24; j > i; j--){
                possibilities.Add(previousNumbers[i] + previousNumbers[j]);
            }
        }
        return possibilities.Contains(longList[index]);
    }

    private List<long> FindContiguousSet(long irregularNumber, List<long> longList){
        for(int i = 0; i < longList.Count; i++){
            long sum = 0;
            List<long> partList = new List<long>();
            for(int j = i; j < longList.Count; j++){
                sum += longList[j];
                partList.Add(longList[j]);

                if(sum > irregularNumber) break;
                if(sum == irregularNumber){
                    System.Console.WriteLine("Found the list!");
                    return partList;
                }
            }
        }
        return null;
    }

    private long FindEncryptionWeakness(List<long> contiguousList){
        long lowest = Int64.MaxValue;
        long highest = 0;
        foreach(long number in contiguousList){
            if(number < lowest) lowest = number;
            if(number > highest) highest = number;
        }
        return lowest + highest;
    }
}