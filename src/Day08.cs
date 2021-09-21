using System;
using System.Collections.Generic;

class DayEight{
    public static int accumulator = 0;
    public static int pointer = 0;

    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day08.txt");
        bool foundAnswer = false;
        int i = 0;

        for(; i < strList.Count && !foundAnswer; i++){        
            accumulator = 0;
            pointer = 0;
            foundAnswer = RunSimulation(i, GetCommands(strList));
        }

        System.Console.WriteLine(i);
        System.Console.WriteLine(accumulator);
    }

    private bool RunSimulation(int changedIndex, List<Command> commandList){
        Command currentTestCommand = commandList[changedIndex];
        if(currentTestCommand.commandType.Equals("acc")) return false;

        SwapCommand(currentTestCommand);
        bool result = ExecuteCommands(commandList);
        SwapCommand(currentTestCommand);

        return result;
    }

    private void SwapCommand(Command command){
        if(command.commandType.Equals("jmp")) command.commandType = "nop";
        else if(command.commandType.Equals("nop")) command.commandType = "jmp";
    }

    private bool ExecuteCommands(List<Command> commandList){
        int count = 0;
        while(pointer < commandList.Count){
            Command currentCommand = commandList[pointer];
            if(!currentCommand.WasVisited()) {
                currentCommand.Execute();
            }
            else {
                System.Console.WriteLine("Looping..." + count);
                return false;
            }
            count++;
        }
        System.Console.WriteLine("Not looping!!!");
        return true;
    }

    private List<Command> GetCommands(List<string> strList){
        List<Command> commandList = new List<Command>();
        foreach(string str in strList){
            commandList.Add(new Command(str));
        }
        return commandList;
    }
}

class Command{
    
    public string commandType;
    public int value;
    bool visited;

    public Command(string info){
        string[] parts = info.Split(" ");
        commandType = parts[0];
        value = Int32.Parse(parts[1]);
    }

    public void Execute(){
        switch(commandType){
            case "acc": this.Accumulate(); break;
            case "jmp": this.Jump(); break;
            case "nop": this.Skip(); break;
        }
        visited = true;
    }

    private void Accumulate(){
        DayEight.accumulator += value;
        DayEight.pointer++;
    }

    private void Jump(){
        DayEight.pointer += value;
    }

    private void Skip(){
        DayEight.pointer++;
    }

    public bool WasVisited(){
        return visited;
    }

    public override string ToString()
    {
        return commandType + " " + value; 
    }
}