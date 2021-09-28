using System.Collections.Generic;

class DayFifteen{
    public void Run(){
        int[] input = new int[]{15,12,0,14,3,1};
        List<int> sequence = MakeSequence(input);
        System.Console.WriteLine(sequence[29999999]);
    }

    private List<int> MakeSequence(int[] input){
        List<int> sequence = new List<int>(input);

        for(int i = 5; i < 30000000; i++){
            if(i % 100000 == 0) System.Console.WriteLine(i);
            int nextNumber = FindNextNumber(sequence);
            sequence.Add(nextNumber);
        }

        return sequence;
    }

    private int FindNextNumber(List<int> sequence){
        int lastNumber = sequence[sequence.Count - 1];
        int distance = 0;

        for(int i = sequence.Count - 2; i >= 0; i--){
            if(sequence[i] == lastNumber){
                distance = (sequence.Count -1) - i;
                break;
            }
        }
        
        return distance;
    }
}

//Todo:
//Object with two fields: values and a list of locations.
//Or a Dictionary with values as key and locations list as value.