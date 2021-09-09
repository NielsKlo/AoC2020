using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class DayFour{
    List<Passport> passportList = new List<Passport>();
    public void Run(){
        FillList();
        int validPassportCount = CountValidPassports();
        System.Console.WriteLine("Total passports: " + passportList.Count);
        System.Console.WriteLine("Valid passports: " + validPassportCount);
    }

    void FillList(){
        List<string> strList = Input.GetStrings("input_files/Day04.txt");
        FillPassportList(strList);
    }

    void FillPassportList(List<string> strList){
        string parts = "";
        foreach(string str in strList){
            if(str != String.Empty) {
                parts += " " + str;
            }
            else {
                if(parts != String.Empty) passportList.Add(new Passport(parts));
                parts = "";
            }
        }
        if(parts != String.Empty) passportList.Add(new Passport(parts));
    }
    int CountValidPassports(){
        int count = 0;
        foreach(Passport passport in passportList){
            if(passport.IsValid()) count++;
        }
        return count;
    }
}

class Passport{
    string byr, iyr, eyr, hgt, hcl, ecl, pid, cid;        
    static List<string> validEclList = new List<string>(){"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};


    public Passport(string info){
        info = info.Trim();
        string[] parts = info.Split(' ');
        foreach(string part in parts){
            FillInfo(part);
        }
    }

    void FillInfo(string kvPair){
        string[] parts = kvPair.Split(':');
        string key = parts[0];
        string value = parts[1];
        switch(key) {
            case "byr": byr = value; break;
            case "iyr": iyr = value; break;
            case "eyr": eyr = value; break;
            case "hgt": hgt = value; break;
            case "hcl": hcl = value; break;
            case "ecl": ecl = value; break;
            case "pid": pid = value; break;
            case "cid": cid = value; break;
        }
    }

    public bool IsValid(){
        return 
            ValidateByr() &&
            ValidateIyr() &&
            ValidateEyr() &&
            ValidateHgt() &&
            ValidateHcl() &&
            ValidateEcl() &&
            ValidatePid();
    }

    bool ValidateByr(){
        if(String.IsNullOrEmpty(byr)) return false;
        int year = Int32.Parse(byr);
        return year >= 1920 && year <= 2002;
    }

    bool ValidateIyr(){
        if(String.IsNullOrEmpty(iyr)) return false;
        int year = Int32.Parse(iyr);
        return year >= 2010 && year <= 2020;
    }

    bool ValidateEyr(){
        if(String.IsNullOrEmpty(eyr)) return false;
        int year = Int32.Parse(eyr);
        return year >= 2020 && year <= 2030;
    }

    bool ValidateHgt(){
        if(String.IsNullOrEmpty(hgt)) return false;
        string heightValue = hgt.Substring(0, hgt.Length - 2);
        if(hgt.Contains("cm")) return ValidCm(heightValue);
        else if(hgt.Contains("in")) return ValidInch(heightValue);
        else return false;
    }

    bool ValidCm(string value){
        int height = Int32.Parse(value);
        return height >= 150 && height <= 193;
    }

    bool ValidInch(string value){
        int height = Int32.Parse(value);
        return height >= 59 && height <= 76;
    }

    bool ValidateHcl(){
        if(String.IsNullOrEmpty(hcl)) return false;
        if(hcl[0] != '#' || hcl.Length != 7) return false;
        string colorValue = hcl.Substring(1);
        return Regex.IsMatch(colorValue, @"[0-9a-f]+$");
    }

    bool ValidateEcl(){
        if(String.IsNullOrEmpty(ecl)) return false;
        return Passport.validEclList.Contains(ecl);
    }

    bool ValidatePid(){
        if(String.IsNullOrEmpty(pid)) return false;
        if(pid.Length != 9) return false;
        return Regex.IsMatch(pid, @"[0-9]+$");
    }

    public override string ToString(){
        return $"byr:{byr}, iyr:{iyr}, eyr:{eyr}, hgt:{hgt}, hcl:{hcl}, ecl:{ecl}, pid:{pid}, cid:{cid}";
    }
}