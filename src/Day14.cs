using System;
using System.Text;
using System.Collections.Generic;

class DayFourteen{
    private string currentMask = "";
    private Dictionary<long, long> memory = new Dictionary<long, long>();

    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day14.txt");
        strList.ForEach(ProcessString);
        long totalValue = GetTotalValue();
        System.Console.WriteLine("Answer: " + totalValue);
    }

    private void ProcessString(string str){
        string type = str.Split(" = ")[0];

        switch(type.Substring(0, 3)){
            case "mas": ProcessMask(str); break;
            case "mem": ProcessMem(str); break;
        }
    }

    private void ProcessMask(string str){
        string newMask = str.Split(" = ")[1];
        currentMask = newMask;
    }

    private void ProcessMem(string str){
        string[] parts = str.Split(" = ");

        string memoryLocation = parts[0].Substring(4, parts[0].Length - 5);
        memoryLocation = Convert.ToString(Int32.Parse(memoryLocation), 2);
        string memoryValue = Convert.ToString(Int64.Parse(parts[1]), 2);

        memoryLocation = ParseInput(memoryLocation);
        memoryValue = ParseInput(memoryValue);

        ProcessMemWithFloatingBits(memoryLocation, memoryValue);
    }

    private void ProcessMemWithFloatingBits(string memoryLocation, string memoryValue){
        string rawLocation = ApplyMaskToLocation(currentMask, memoryLocation);
        Queue<string> locationQueue = new Queue<string>();

        locationQueue.Enqueue(rawLocation);

        while(locationQueue.TryDequeue(out string potentialLocation)){

            if(potentialLocation.Contains('X')) AddLocationsToQueue(locationQueue, potentialLocation);
            else {
                AddToMemory(potentialLocation, memoryValue);
            }
        }
        //Bug is in here.
        //Don't place the 0/1s in the mask but in the location.
    }

    private void AddLocationsToQueue(Queue<string> maskQueue, string unreadyMask){
        int indexOfX = unreadyMask.IndexOf('X');
        StringBuilder builder = new StringBuilder(unreadyMask);

        string zero = builder.Replace(builder[indexOfX], '0', indexOfX, 1).ToString();
        string one = builder.Replace(builder[indexOfX], '1', indexOfX, 1).ToString();

        maskQueue.Enqueue(zero);
        maskQueue.Enqueue(one);
    }

    private string ApplyMaskToLocation(string mask, string memoryLocation){
        StringBuilder builder = new StringBuilder(memoryLocation);
        for(int i = 0; i < mask.Length; i++){
            if(mask[i] == '1'){
                builder.Replace(memoryLocation[i], '1', i, 1);
            } else if(mask[i] == 'X'){
                builder.Replace(memoryLocation[i], 'X', i, 1);
            }
        }
        return builder.ToString();
    }

    private void AddToMemory(string memoryLocation, string memoryValue){
        long key = Convert.ToInt64(memoryLocation, 2);
        long value = Convert.ToInt64(memoryValue, 2);

        memory[key] = value;
    }

    private string ParseInput(string initValue){
        StringBuilder builder = new StringBuilder();
        builder.Append('0', 36 - initValue.Length).Append(initValue);
        return builder.ToString();
    }

    private long GetTotalValue(){
        long sum = 0;
        foreach(KeyValuePair<long, long> entry in memory){
            sum += entry.Value;
        }
        return sum;
    }
}