﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Internal
    {
        public int Input()
        {
            try
            {
                int inputNum = Int32.Parse(Console.ReadLine())/*-1*/;
                return inputNum;
            }
            catch (Exception e)
            {
                Console.WriteLine("Not a valid input");
                return 9; //shitty solution
            }
        }

        public char[] Write(char player, int inputNum, char[] pos)
        {
            pos[inputNum] = player;
            return pos;
        }


    }
}
