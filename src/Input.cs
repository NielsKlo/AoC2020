using System.Collections.Generic;
using System.IO;

class Input{
    public static List<string> GetStrings(string file){
        List<string> stringList = new List<string>();
        using (StreamReader reader = File.OpenText(file)){
            string str;
            while((str = reader.ReadLine()) != null){
                stringList.Add(str);
            }
        }
        return stringList;
    }
}