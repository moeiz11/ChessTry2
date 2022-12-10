using System;
using System.Security.Cryptography.X509Certificates;

namespace ChessTry2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int scale = 8;
            Board board = new Board(scale);
            board.select();
          

        }
    }
}
