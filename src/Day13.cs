using System;
using System.Collections.Generic;

class DayThirteen{
    int earliestTime;

    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day13.txt");
        earliestTime = Int32.Parse(strList[0]);
        List<int> bussIDs = GetBussIDs(strList[1]);
        bussIDs.ForEach(Console.WriteLine);
        int waitingTime = 0;
        int fastestBuss = FindFastestBuss(bussIDs, out waitingTime);
        System.Console.WriteLine("Answer: {0}", (fastestBuss * waitingTime));
    }

    private List<int> GetBussIDs(string data){
        string[] splitData = data.Split(",");
        List<int> busses = new List<int>();
        foreach(string str in splitData){
            if(int.TryParse(str, out int n)) busses.Add(n);
        }
        return busses;
    }

    private int FindFastestBuss(List<int> busses, out int wait){
        int fastestBuss = 0;
        int shortestWait = Int32.MaxValue;
        foreach(int buss in busses){
            int nextTime = buss * (earliestTime / buss + 1);
            int waitingTime = nextTime - earliestTime;
            if(waitingTime < shortestWait) {
                shortestWait = waitingTime;
                fastestBuss = buss;
            }
        }
        wait = shortestWait;
        return fastestBuss;
    }
}