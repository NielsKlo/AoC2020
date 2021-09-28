using System;
using System.Collections.Generic;

class DayThirteen{
    public void Run(){
        Console.WriteLine(  System.IO.Directory.GetCurrentDirectory());
        List<string> strList = Input.GetStrings("input_files/Day13.txt");
        List<int> bussIDs = GetBussIDs(strList[1]);
        List<string> allSlots = new List<string>(strList[1].Split(","));
        List<Buss> busses = MakeBusses(bussIDs, allSlots);
        busses.ForEach(Console.WriteLine);

    }

    private List<int> GetBussIDs(string data){
        string[] splitData = data.Split(",");
        List<int> busses = new List<int>();
        foreach(string str in splitData){
            if(int.TryParse(str, out int n)) busses.Add(n);
        }
        return busses;
    }

    private int FindOffset(int bussId, List<string> allSlots){
        int index = allSlots.FindIndex((slot) => slot.Equals(bussId.ToString()));
        return - (29 - index);
    }

    private List<Buss> MakeBusses(List<int> busses, List<string> allSlots){
        List<Buss> bussList = new List<Buss>();
        foreach(int buss in busses){
            bussList.Add(new Buss(buss, FindOffset(buss, allSlots)));
        }
        return bussList;
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

    private int FindEarliestTime(List<int> busses){
        return 0;
    }
}

class Buss{
    public readonly int id;
    public readonly int offset;

    public Buss(int id, int offset){
        this.id = id;
        this.offset = offset;
    }

    public override string ToString(){
        return "id: " + id + ", offset: " + offset;
    }
}