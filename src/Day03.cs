using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020{
    class DayThree{
        int currentX = 0;
        int currentY = 0;
        int mapWidth;
        int mapLength;
        List<string> map;
        List<int> treeCounts = new List<int>();

        public void Run(){
            map = Input.GetStrings("input_files/Day03.txt");
            mapWidth = map[0].Length;
            mapLength = map.Count;
            treeCounts.Add(RideToboggan(1, 1));
            treeCounts.Add(RideToboggan(3, 1));
            treeCounts.Add(RideToboggan(5, 1));
            treeCounts.Add(RideToboggan(7, 1));
            treeCounts.Add(RideToboggan(1, 2));
            System.Console.WriteLine(TreeProduct());
        }

        int RideToboggan(int deltaX = 3, int deltaY = 1){
            int treeCount = 0;
            while(currentY < mapLength - deltaY){
                if(TakeStep(deltaX, deltaY)) treeCount++;
            }
            System.Console.WriteLine(treeCount);
            currentX = 0;
            currentY = 0;
            return treeCount;
        }

        bool TakeStep(int deltaX, int deltaY){
            currentX += deltaX;
            currentY += deltaY;
            return map[currentY][currentX % mapWidth] == '#';
        }

        long TreeProduct(){
            long product = 1;
            foreach(int trees in treeCounts){
                product *= trees;
            }
            return product;
        }
    }
}