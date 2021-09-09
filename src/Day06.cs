using System.Collections.Generic;
using System;

class DaySix{
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day06.txt");
        List<Group> groupList = GetGroups(strList);
        int totalAnswerCount = GetTotalAnswerCount(groupList);
        System.Console.WriteLine("total answer count: " +totalAnswerCount);
    }

    List<Group> GetGroups(List<string> strList){
        List<Group> groupList = new List<Group>();
        string groupAnswers = "";
        foreach(string str in strList){
            if(str != String.Empty) {
                groupAnswers += " " + str;
            }
            else {
                if(groupAnswers != String.Empty) groupList.Add(new Group(groupAnswers.Trim()));
                groupAnswers = "";
            }
        }
        if(groupAnswers != String.Empty) groupList.Add(new Group(groupAnswers.Trim()));
        return groupList;
    }

    int GetTotalAnswerCount(List<Group> groupList){
        int totalAnswerCount = 0;
        foreach(Group group in groupList){
            totalAnswerCount += group.GetAnswerCount();
        }
        return totalAnswerCount;
    }
}

class Group {
    string[] allAnswers;
    List<char> presentAnswers = new List<char>();

    public Group(string answers){
        this.allAnswers = answers.Split(' ');
    }

    public int GetAnswerCount(){
        FillPresentAnswers();
        int count = 0;

        foreach(char answer in presentAnswers){
            if(CheckAllAnswers(answer)) count++;
        }

        return count;
    }

    void FillPresentAnswers(){
        foreach(string str in allAnswers){
            foreach(char c in str){
                if(!presentAnswers.Contains(c)) presentAnswers.Add(c);
            }
        }
    }

    bool CheckAllAnswers(char answer){
        int count = 0;
        int persons = allAnswers.Length;

        foreach(string str in allAnswers){
            if(str.Contains(answer)) count ++;
        }

        return count == persons;
    }
}