using System;
using System.Collections.Generic;

class DaySeven{
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day07.txt");
        List<Bag> bagList = Bag.ToBags(strList);
        Bag shinyGoldBag = bagList.Find(bag => bag.MatchesColor("shiny gold"));
        Console.WriteLine(shinyGoldBag.CountHeldBags(1));
    }

    int GetContainmentCount(string color, List<Bag> bagList){
        int count = 0;
        foreach(Bag bag in bagList){
            if(bag.ContainsBag(color)) count++;
        }
        return count;
    }
}

class Bag{
    public static List<Bag> ToBags(List<string> strList){
        List<Bag> allBags = new List<Bag>();
        foreach(string str in strList){
            allBags.Add(new Bag(str));
        }
        foreach(Bag bag in allBags){
            bag.FillBag(allBags);
        }
        return allBags;
    }

    string color;
    Dictionary<string, int> contentInfo = new Dictionary<string, int>();
    Dictionary<Bag, int> contentBags = new Dictionary<Bag, int>();

    private Bag(string specifications){
        string[] parts = specifications.Split(" contain ");
        color = parts[0].Substring(0, parts[0].Length - 5);

        string contents = parts[1];
        parts = contents.Split(",");
        foreach(string str in parts){
            if(str[0].Equals('n')) break;

            int index = str.IndexOf("bag");
            string item = str.Substring(0, index).Trim();

            int amount = Int32.Parse(item.Substring(0, 1));
            string color = item.Substring(2);
            contentInfo.Add(color, amount);
        }
    }

    void FillBag(List<Bag> allBags){
        foreach(KeyValuePair<string, int> entry in contentInfo){
            foreach(Bag bag in allBags){
                if(entry.Key == bag.color){
                    contentBags.Add(bag, entry.Value);
                }
            }
        }
    }

    public bool ContainsBag(string color){
        if(contentInfo.Count == 0) return false;

        if(contentInfo.ContainsKey(color)) return true;

        foreach(KeyValuePair<Bag, int> entry in contentBags){
            if(entry.Key.ContainsBag(color)) return true;
        }

        return false;
    }

    public bool MatchesColor(string color){
        return this.color.Equals(color);
    }

    public int CountHeldBags(int multiplier){
        System.Console.WriteLine(this.color);
        int count = 0;
        foreach(KeyValuePair<Bag, int> entry in contentBags){
            count += (entry.Value * multiplier);
            count += entry.Key.CountHeldBags(entry.Value * multiplier);
        }  
        return count;
    }
}