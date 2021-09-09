using System;
using System.Collections.Generic;

class DayFive {
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day05.txt");
        List<Pass> passList = GetPassList(strList);
        List<int> openSeats = FindOpenSeats(passList);
        openSeats.ForEach(Console.WriteLine);
    }

    List<Pass> GetPassList(List<string> strList){
        List<Pass> passList = new List<Pass>();
        foreach(string str in strList){
            passList.Add(new Pass(str));
        }
        return passList;
    }

    int FindHighestPassID(List<Pass> passList){
        int highestPassID = 0;
        foreach(Pass pass in passList){
            int passID = pass.GetSeatID();
            if(passID > highestPassID) highestPassID = passID;
        }
        return highestPassID;
    }

    List<int> FindOpenSeats(List<Pass> passList){
        List<int> openSeats = new List<int>();
        for(int i = 0; i < 1024; i++){
            openSeats.Add(i);
        }
        foreach(Pass pass in passList){
            openSeats.Remove(pass.GetSeatID());
        }
        return openSeats;
    }
}

class Pass{
    int row;
    int column;

    public Pass(string str){
        FindRow(str.Substring(0, 7));
        FindColumn(str.Substring(7));
    }

    void FindRow(string rowString){
        int lowest = 0;
        int highest = 127;
        int divider = 64;
        foreach(char c in rowString){
            if(c == 'F') highest -= divider;
            if(c == 'B') lowest += divider;
            divider /= 2;
        }
        row = lowest;
    }

    void FindColumn(string columnString){
        int lowest = 0;
        int highest = 7;
        int divider = 4;
        foreach(char c in columnString){
            if(c == 'L') highest -= divider;
            if(c == 'R') lowest += divider;
            divider /= 2;
        }
        column = lowest;
    }

    public int GetSeatID(){
        return (row * 8) + column;
    }

}