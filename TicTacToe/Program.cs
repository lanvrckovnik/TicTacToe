// Program end problem... Seperate write and print. Make a board with numbers. Reverse CheckIfEnd check to xo rather than ' '.
//Simpler board
//More readable code


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;


namespace TicTacToe
{
    class Program
    {
 
        static void Main(string[] args)
        {
            Firebase firebase = new Firebase();
            Internal internalProcces = new Internal();
            
           
            char[] pos = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
            string board = ($" {pos[0]} | {pos[1]} | {pos[2]} \n" +
                            "-----------\n" +
                            $" {pos[3]} | {pos[4]} | {pos[5]} \n" +
                            "-----------\n" +
                            $" {pos[6]} | {pos[7]} | {pos[8]} \n");
            int inputNum = 0;
            //char turn = 'o';
            bool canContinue = true;
            bool gameEnd = false;
            string winner = "Draw";
            char player = 'o';
            

            Console.WriteLine("Heloo welocome to tic tac toe!");
            Console.WriteLine("This is how the board looks like:");
            Console.WriteLine(board);
            Console.WriteLine("Input the number of the field you want to fill.");
            Console.WriteLine(" 0 | 1 | 2 \n" +
                              "-----------\n" +
                              " 3 | 4 | 5 \n" +
                              "-----------\n" +
                              " 6 | 7 | 8 \n");


            //check, signal, wait, connect
            firebase.CheckJoin(player, pos);

            while (!gameEnd)
            {
                if (firebase.CheckTurn(player))
                {

                    pos = firebase.getGrid();

                    //Print
                    Print();

                    //Input
                    inputNum = internalProcces.Input();

                    //CheckIfOkay
                    CheckIfOkay();

                    if (canContinue)
                    {
                        //Write 
                        internalProcces.Write(player, inputNum, pos);
                        firebase.ChangeTurn(player, pos);
                    }

                    //CheckIfEnd
                    CheckIfEnd();

                    //Print
                    Print();
                    
                    canContinue = true;
                }
            }
           

            

            



            void CheckIfEnd()
            {
                //ful
                gameEnd = true;
                foreach (char c in pos)
                {
                    if (c != 'x' &&
                        c != 'o')
                    {
                        gameEnd = false;
                    }
                }
                //win [ooozzzzzz][zzzooozzz][zzzzzzooo][ozzozzozz][zozzozzoz][zzozzozzo][ozzzozzzo][zzozozozz]
                //        012,       345,        678,     036,        147,       258,       048,        246 
                //ooo = 333 o = 111
                //xxx = 360 x = 120
                int[,] wincomb = { {0, 1, 2 }, {3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 }};
                
                for(int i = 0; i<wincomb.GetLength(0); i++)
                {
                    if (pos[wincomb[i, 0]] + pos[wincomb[i, 1]] + pos[wincomb[i, 2]] == 333)
                    {
                        winner = "o";
                        gameEnd = true;
                    }else if(pos[wincomb[i, 0]] + pos[wincomb[i, 1]] + pos[wincomb[i, 2]] == 333)
                    {
                        winner = "x";
                        gameEnd = true;
                    }
                }

                if (gameEnd)
                {
                    //_ = client.Set("Player", 'x');
                    Console.WriteLine("The game has ended!");
                    if(winner != "Draw")
                    {
                        Console.WriteLine($"The winner is {winner}");
                    }
                    else
                    {
                        Console.WriteLine($"It was a {winner}");
                    }
                }
            }

            void CheckIfOkay()
            {
                if (inputNum < 9 && pos[inputNum] != 'o' && pos[inputNum] != 'x')
                {

                }
                else {
                    Console.WriteLine("Invalid input!");
                    canContinue = false; //reset
                    
                }
            }
            
            
            void Print()
            {
                Console.WriteLine($" {pos[0]} | {pos[1]} | {pos[2]} \n" + "-----------\n" + $" {pos[3]} | {pos[4]} | {pos[5]} \n" + "-----------\n" + $" {pos[6]} | {pos[7]} | {pos[8]} \n");            
                
            }
            Console.ReadKey();
        }
    }
}
