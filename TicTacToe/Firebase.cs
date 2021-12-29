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
    public class Firebase
    {

        private static IFirebaseClient client = null;

        private static IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "",
            BasePath = ""
        };

        static private IFirebaseClient setClient()
        {

            try
            {
                client = new FireSharp.FirebaseClient(fcon);
                Console.WriteLine("Connection to firebase established!");
                return client;
            }
            catch
            {
                Console.WriteLine("Firebase Error!");
                return client;
            }
        }


        public char[] getGrid()
        {
            var _pos = client.Get("XoList");
            char[] pos = _pos.ResultAs<char[]>();

            return pos;
        }

        public void ChangeTurn(char player,char[] pos)
        {
            if (player == 'o')
            {
                _ = client.Set("Turn", 'x');
            }
            else
            {
                _ = client.Set("Turn", 'o');
            }
            _ = client.Set("XoList", pos);
        }

        public bool CheckTurn(char player)
        {
            var _turn = client.Get("Turn");
            char turn = _turn.ResultAs<char>();

            return (turn == player);
        }

        public void CheckJoin(char player, char[] pos)
        {
            //check
            var checkXO = client.Get("Player");
            char _player = checkXO.ResultAs<char>();


            if (_player == 'o')
            {
                player = 'x';
                _ = client.Set("Player", 'x');
                Console.WriteLine("Joind the game as x");
            }
            else
            {
                _ = client.Set("Player", 'o');
                Console.WriteLine("Waiting for player to join");
                bool search = true;
                while (search)
                {
                    checkXO = client.Get("Player");
                    _player = checkXO.ResultAs<char>();
                    if (_player == 'x')
                    {
                        Console.WriteLine("A player has joined!");
                        search = false;
                    }
                }
                _ = client.Set("Turn", 'o');
            }
            _ = client.Set("XoList", pos);
        }


    }
}
