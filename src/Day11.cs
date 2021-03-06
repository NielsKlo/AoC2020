using System;
using System.Collections.Generic;

class DayEleven{
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day11.txt");
        Map map = new Map(strList);
        bool changed = true;
        while(changed){
            changed = map.ProcessRound();
        }
        int occupiedSeats = map.GetOccupiedSeatCount();
        System.Console.WriteLine(occupiedSeats);
    }
}

class Map {
    char[,] oldMap;
    char[,] currentMap;
    int lineCount;
    int charCount;

    public Map(List<string> strList){
        lineCount = strList.Count;
        charCount = strList[0].Length;
        currentMap = new char[lineCount, charCount];
        oldMap = new char[lineCount, charCount];

        for(int i = 0; i < lineCount; i++){
            for(int j = 0; j < charCount; j++){
                currentMap[i, j] = strList[i][j];
            }
        }
    }

    public bool ProcessRound(){
        Array.Copy(currentMap, oldMap, currentMap.Length);
        bool changed = false;

        for(int i = 0; i < lineCount; i++){
            for(int j = 0; j < charCount; j++){
                if(ProcessSeat(i, j)){
                    changed = true;
                }
            }
        }

        return changed;
    }

    private bool ProcessSeat(int i, int j){
        switch(currentMap[i, j]){
            case 'L': return ProcessEmptySeat(i, j); 
            case '#': return ProcessOccupiedSeat(i, j); 
            default: return false;
        }
    }

    private bool ProcessEmptySeat(int i, int j){
        int occupiedCount = GetOccupiedNeighbourCount(i, j);
        if(occupiedCount == 0){
            currentMap[i, j] = '#';
            return true;
        }
        return false;
    }

    private bool ProcessOccupiedSeat(int i, int j){
        int occupiedCount = GetOccupiedNeighbourCount(i, j);
        if(occupiedCount >= 5) {
            currentMap[i, j] = 'L';
            return true;
        }
        return false;
    }

    private int GetOccupiedNeighbourCount(int i, int j){
        int count = 0;

        for(int a = -1; a < 2; a++){
            for(int b = -1; b < 2; b++){
                if(a == 0 && b == 0) continue;
                if(DirectionHasSeat(i, j, a, b)) count++;
            }
        }
        return count;
    }

    private bool DirectionHasSeat(int i, int j, int iDirection, int jDirection){
        int potentialI = i + iDirection;
        int potentialJ = j + jDirection;

        for(int multiplier = 2; IsInArray(potentialI, potentialJ); multiplier++){
            if(oldMap[potentialI, potentialJ] == '#') return true;
            if(oldMap[potentialI, potentialJ] == 'L') return false;

            potentialI = i + multiplier * iDirection;
            potentialJ = j + multiplier * jDirection;
        }

        return false;
    }

    private bool IsInArray(int potentialI, int potentialJ){
        return potentialI >= 0 && potentialI < lineCount
            && potentialJ >= 0 && potentialJ < charCount;
    }

    public int GetOccupiedSeatCount(){
        int count = 0;
        foreach(char c in currentMap){
            if(c == '#') count++;
        }
        return count;
    }

    public void PrintMap(){
        for(int i = 0; i < currentMap.GetLength(0); i++){
            for(int j = 0; j < currentMap.GetLength(1); j++){
                Console.Write(currentMap[i,j]);
            }
            Console.WriteLine();
        }
    }
}