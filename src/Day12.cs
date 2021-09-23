using System;
using System.Collections.Generic;

class DayTwelve{
    public void Run(){
        List<string> strList = Input.GetStrings("input_files/Day12.txt");
        List<Instruction> instructions = GetInstructions(strList);
        Ship ship = new Ship();
        int count = 0;
        foreach(Instruction instruction in instructions){
            ship.FollowInstruction(instruction);
            System.Console.WriteLine("Following instruction: {0}, distance: {1}", ++count, ship.GetDistance());
        }
        ship.PrintDistance();
    }

    private List<Instruction> GetInstructions(List<string> strList){
        List<Instruction> instructions = new List<Instruction>();
        foreach(string str in strList){
            instructions.Add(new Instruction(str));
        }
        return instructions;
    }
}

class Ship{
    int currentEast;
    int currentNorth;
    int waypointEast;
    int waypointNorth;

    public Ship(){
        currentEast = 0;
        currentNorth = 0;
        waypointEast = 10;
        waypointNorth = 1;
    }

    public void FollowInstruction(Instruction instruction){
        switch(instruction.type){
            case 'N': this.MoveNorth(instruction.value); break;
            case 'S': this.MoveSouth(instruction.value); break;
            case 'E': this.MoveEast(instruction.value); break;
            case 'W': this.MoveWest(instruction.value); break;
            case 'L': this.TurnLeft(instruction.value); break;
            case 'R': this.TurnRight(instruction.value); break;
            case 'F': this.MoveForward(instruction.value); break;
            default: break;
        }
    }

    private void MoveNorth(int value){
        waypointNorth += value;
    }

    private void MoveSouth(int value){
        waypointNorth -= value;
    }

    private void MoveEast(int value){
        waypointEast += value;
    }

    private void MoveWest(int value){
        waypointEast -= value;
    }

    private void TurnLeft(int value){
        for(int i = 0; i < value / 90; i++){
            int oldEast = waypointEast;
            int oldNorth = waypointNorth;
            waypointEast = (- oldNorth);
            waypointNorth = oldEast;
        }
    }

    private void TurnRight(int value){
        for(int i = 0; i < value / 90; i++){
            int oldEast = waypointEast;
            int oldNorth = waypointNorth;
            waypointEast = waypointNorth;
            waypointNorth = (- oldEast);
        }
    }

    private void MoveForward(int value){
        currentEast += waypointEast * value;
        currentNorth += waypointNorth * value;
    }

    public void PrintDistance(){
        System.Console.WriteLine(this.GetDistance());
    }

    public string GetDistance(){
        return Math.Abs(currentEast) + Math.Abs(currentNorth) + "";
    }
}

class Instruction{
    public readonly char type;
    public readonly int value;

    public Instruction(string str){
        type = str[0];
        value = Int32.Parse(str.Substring(1));
    }
}

enum Direction{
    NORTH, EAST, SOUTH, WEST
}

static class EnumExtension {
    public static Direction TurnLeft(this Direction oldDirection) => oldDirection switch {
        Direction.NORTH => Direction.WEST,
        Direction.WEST => Direction.SOUTH,
        Direction.SOUTH => Direction.EAST,
        Direction.EAST => Direction.NORTH,
        _ => throw new ArgumentOutOfRangeException(),
    };

        public static Direction TurnRight(this Direction oldDirection) => oldDirection switch {
        Direction.NORTH => Direction.EAST,
        Direction.WEST => Direction.NORTH,
        Direction.SOUTH => Direction.WEST,
        Direction.EAST => Direction.SOUTH,
        _ => throw new ArgumentOutOfRangeException(),
    };
}