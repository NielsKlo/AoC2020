using System;
using System.Collections.Generic;

namespace AOC2020{
    class DayTwo{
        public void Run(){
            List<string> strList = Input.GetStrings("input_files/Day02.txt");
            List<Entry> entryList = new List<Entry>();
            foreach(string str in strList){
                entryList.Add(new Entry(str));
            }
            int validPasswordCount = CountValidPasswords(entryList);
            System.Console.WriteLine(validPasswordCount);
        }

        int CountValidPasswords(List<Entry> list){
            int count = 0;
            foreach(Entry entry in list){
                if(entry.IsValid()) count++;
            }
            return count;
        }
    }

    class Entry{
        int lower;
        int higher;
        char id;
        string password;
        
        public Entry(string line){
            string[] parts = line.Split(':');
            password = parts[1].Trim();

            parts = parts[0].Split(' ');
            id = char.Parse(parts[1]);

            parts = parts[0].Split('-');
            higher = Int32.Parse(parts[1]);
            lower = Int32.Parse(parts[0]);
        }

        public bool IsValid(){
            char[] passwordArray = password.ToCharArray();
            char first = passwordArray[lower - 1];
            char second = passwordArray[higher - 1];
            if(first == id && second == id) return false;
            if(first == id || second == id) return true;
            else return false;
        }

        int countOccurrences(){
            int count = 0;
            foreach(char c in password.ToCharArray()){
                if(c == id) count++;
            }
            return count;
        }
    }
}