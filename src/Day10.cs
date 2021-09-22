using System;
using System.Collections.Generic;

class DayTen{
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day10.txt");
        List<int> intList = GetIntegers(strList);
        List<Adapter> adapters = GetAdapters(intList);
        //CheckPossibilities(adapters);
        long possibilities = CountPossibilities(adapters);
        System.Console.WriteLine(possibilities);
    }

    private List<int> GetIntegers(List<string> strList){
        List<int> intList = new List<int>();
        foreach(string str in strList){
            intList.Add(Int32.Parse(str));
        }
        intList.Sort();
        intList.Add(intList[intList.Count - 1] + 3);
        intList.Add(0);
        intList.Sort();
        return intList;
    }

    private List<Adapter> GetAdapters(List<int> intList){
        List<Adapter> adapters = new List<Adapter>();
        foreach(int i in intList){
            adapters.Add(new Adapter(i));
        }
        return adapters;
    }

    private void FindDifferenceCounts(out int ones, out int twos, out int threes, List<int> intList){
        ones = 0; twos = 0; threes = 1;
        
        switch(intList[0]){
            case 1: ones++; break;
            case 2: twos++; break;
            case 3: threes++; break;
            default: break;
        }

        for(int i = 0; i < intList.Count - 1; i++){
            int difference = intList[i+1] - intList[i];

            switch(difference){
                case 1: ones++; break;
                case 2: twos++; break;
                case 3: threes++; break;
                default: break;
            }
        }
    }

    private void CheckPossibilities(List<Adapter> adapters){
        for(int i = adapters.Count - 1; i >= 0; i--){
            for(int j = i - 1; j > i - 4 && j >= 0; j--){
                int difference = adapters[i].joltage - adapters[j].joltage;
                if(difference < 4) adapters[j].paths++;
            }
        }
    }

    private long CountPossibilities(List<Adapter> adapters){
        adapters[adapters.Count - 1].paths = 1;
        for(int i = adapters.Count - 1; i >= 0; i--){
            for(int j = i + 1; j < i + 4 && j < adapters.Count; j++){
                if(adapters[j].joltage - adapters[i].joltage < 4) {
                    adapters[i].paths += adapters[j].paths;
                    System.Console.WriteLine(adapters[i] + " looking at " + adapters[j].joltage);
                }
            }
        }
        return adapters[0].paths;
    }
}

class Adapter{
    public readonly int joltage;
    public long paths = 0;
    
    public Adapter(int joltage){
        this.joltage = joltage;
    }

    public override string ToString(){
        return joltage + "   " + paths;
    }
}