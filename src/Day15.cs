using System.Collections.Generic;

class DayFifteen{
    public void Run(){
        int[] input = new int[]{ 15, 12, 0, 14, 3, 1 };
        Dictionary<int, int> memory = StartMemory(input);
        int value = IterateSequence(memory, targetIndex: 29_999_998);
        System.Console.WriteLine($"Value is: {value}");
    }

    private Dictionary<int, int> StartMemory(int[] input)
    {
        Dictionary<int, int> memory = new();
        for(int i = 0; i < input.Length; i++)
        {
            memory.Add(input[i], i);
        }
        return memory;
    }

    private int IterateSequence(Dictionary<int, int> memory, int targetIndex)
    {
        int nextValue = 0;

        for (int i = memory.Count; i <= targetIndex; i++)
        {
            if (memory.ContainsKey(nextValue))
            {
                int lastIndex = memory[nextValue];
                memory[nextValue] = i;
                nextValue = i - lastIndex;
            } else
            {
                memory[nextValue] = i;
                nextValue = 0;
            }
        }

        return nextValue;
    }

    private int FindNextNumber(List<int> sequence){
        int lastNumber = sequence[^1];
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