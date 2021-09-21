using System.Collections.Generic;

class DayNine{
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day09.txt");
        List<long> longList = GetNumbers(strList);
        long irregularNumber = 144381670; //GetFirstIrregularNumber(longList);
        FindContiguousSet(irregularNumber, longList);
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

    private void FindContiguousSet(long irregularNumber, List<long> longList){
        
    }
}