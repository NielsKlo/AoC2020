using System;
using System.Collections.Generic;

class DayThirteen{
    public void Run(){
        Console.WriteLine(  System.IO.Directory.GetCurrentDirectory());
        List<string> strList = Input.GetStrings("input_files/Day13.txt");
        List<int> bussIDs = GetBussIDs(strList[1]);
        List<string> allSlots = new List<string>(strList[1].Split(","));
        int biggestBussIndex = FindBiggestBussIndex(allSlots);
        List<Buss> busses = MakeBusses(bussIDs, allSlots, biggestBussIndex);
        
        long i = FindEarliestTime(busses, biggestBussIndex);
        System.Console.WriteLine("Earliest time: " + i);
    }

    private List<int> GetBussIDs(string data){
        string[] splitData = data.Split(",");
        List<int> busses = new List<int>();
        foreach(string str in splitData){
            if(int.TryParse(str, out int n)) busses.Add(n);
        }
        return busses;
    }

    private List<Buss> MakeBusses(List<int> busses, List<string> allSlots, int biggestBussIndex){
        List<Buss> bussList = new List<Buss>();
        foreach(int buss in busses){
            bussList.Add(new Buss(buss, FindOffset(buss, allSlots, biggestBussIndex)));
        }
        return bussList;
    }

    private int FindBiggestBussIndex(List<string> allSlots){
        int biggest = 0;
        int biggestIndex = 0;
        for(int i = 0; i < allSlots.Count; i++){
            if(Int32.TryParse(allSlots[i], out int result)){
                if(result > biggest){
                    biggest = result;
                    biggestIndex = i;
                }
            }
        }
        return biggestIndex;
    }

    private int FindOffset(int bussId, List<string> allSlots, int biggestBussIndex){
        int index = allSlots.FindIndex((slot) => slot.Equals(bussId.ToString()));
        return - (biggestBussIndex - index);
    }

    // private int FindFastestBuss(List<int> busses, out int wait){
    //     int fastestBuss = 0;
    //     int shortestWait = Int32.MaxValue;
    //     foreach(int buss in busses){
    //         int nextTime = buss * (earliestTime / buss + 1);
    //         int waitingTime = nextTime - earliestTime;
    //         if(waitingTime < shortestWait) {
    //             shortestWait = waitingTime;
    //             fastestBuss = buss;
    //         }
    //     }
    //     wait = shortestWait;
    //     return fastestBuss;
    // }

    private long FindEarliestTime(List<Buss> busses, int biggestBussIndex){
        busses.Sort((Buss a, Buss b) => b.id - a.id); 
        Buss slowest = busses[0]; 
        busses.Remove(slowest);
        for(long i = slowest.id;; i += (slowest.id * 13 * 17 * 19 * 29)){
            System.Console.WriteLine(i);
            if(CheckPattern(i)) return i - biggestBussIndex;
        }
    }

    private bool CheckPattern(long time){
        if((time + 31) % 15456713 == 0) return true;
        return false;
    }
}

class Buss{
    public readonly int id;
    public int offset;

    public Buss(int id, int offset){
        this.id = id;
        this.offset = offset;
    }

    public bool IsInPattern(long time){
        long timeForBuss = time + offset;
        long timeDifference = timeForBuss % id;
        return timeDifference == 0;
    }

    public override string ToString(){
        return "id: " + id + ", offset: " + offset;
    }
}