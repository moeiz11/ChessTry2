using System;
using System.Security.Cryptography.X509Certificates;
namespace ChessTry2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            int scale = 8;
            Board board = new Board(scale, ConsoleColor.White, ConsoleColor.Green);
            board.start();
        }
    }
}
