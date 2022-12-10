using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChessTry2
{
    internal class Board
    {
        public List<Piece> WhitePieces { get; set; }
        public List<Piece> BlackPieces { get; set; }
        public List<Cell> Cells { get; set; }
        public Piece p { get; set; }
        public int scale { get; set; }
        public int movecounter { get; set; }
        public Board(int scale)
        {
            p = new Piece();
            this.scale = scale;
            Cells = new List<Cell>();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Cell cell = new Cell(new Coordinates(x, y));
                    Cells.Add(cell);
                }
            }
            WhitePieces = new List<Piece>();
            for (int x = 0; x < 8; x++)
            {
                Piece piece = new Piece("P", new Coordinates(x, 6), 0);
                WhitePieces.Add(piece);
            }
            for (int x = 0; x < 8; x++)
            {
                switch (x)
                {
                    case 0:
                        Piece Rook1 = new Piece("R", new Coordinates(x, 7),0);
                        WhitePieces.Add(Rook1);
                        break;
                    case 1:
                        Piece Knight1 = new Piece("H", new Coordinates(x, 7),0);
                        WhitePieces.Add(Knight1);
                        break;
                    case 2:
                        Piece Bishop1 = new Piece("B", new Coordinates(x, 7),0);
                        WhitePieces.Add(Bishop1);
                        break;
                    case 3:
                        Piece Queen = new Piece("Q", new Coordinates(x, 7),0);
                        WhitePieces.Add(Queen);
                        break;
                    case 4:
                        Piece King = new Piece("K", new Coordinates(x, 7),0);
                        WhitePieces.Add(King);
                        break;
                    case 5:
                        Piece Bishop2 = new Piece("B", new Coordinates(x, 7),0);
                        WhitePieces.Add(Bishop2);
                        break;
                    case 6:
                        Piece Knight2 = new Piece("H", new Coordinates(x, 7),0);
                        WhitePieces.Add(Knight2);
                        break;
                    case 7:
                        Piece Rook2 = new Piece("R", new Coordinates(x, 7),0);
                        WhitePieces.Add(Rook2);
                        break;
                }
                BlackPieces = new List<Piece>();
                for (int b = 0; b < 8; b++)
                {
                    Piece piece = new Piece("P", new Coordinates(b, 1),1);
                    BlackPieces.Add(piece);
                }
                for (int b = 0; b < 8; b++)
                {
                    switch (b)
                    {
                        case 0:
                            Piece Rook1 = new Piece("R", new Coordinates(b, 0),1);
                            BlackPieces.Add(Rook1);
                            break;
                        case 1:
                            Piece Knight1 = new Piece("H", new Coordinates(b, 0),1);
                            BlackPieces.Add(Knight1);
                            break;
                        case 2:
                            Piece Bishop1 = new Piece("B", new Coordinates(b, 0),1);
                            BlackPieces.Add(Bishop1);
                            break;
                        case 3:
                            Piece Queen = new Piece("Q", new Coordinates(b, 0),1);
                            BlackPieces.Add(Queen);
                            break;
                        case 4:
                            Piece King = new Piece("K", new Coordinates(b, 0),1);
                            BlackPieces.Add(King);
                            break;
                        case 5:
                            Piece Bishop2 = new Piece("B", new Coordinates(b, 0),1);
                            BlackPieces.Add(Bishop2);
                            break;
                        case 6:
                            Piece Knight2 = new Piece("H", new Coordinates(b, 0),1);
                            BlackPieces.Add(Knight2);
                            break;
                        case 7:
                            Piece Rook2 = new Piece("R", new Coordinates(b, 0),1);
                            BlackPieces.Add(Rook2);
                            break;
                    }
                }
            }
        }
        public void select()
        {
            int scalee = scale/2;
            int x = 0;
            int y = 0;
            while (true)
            {
                Console.CursorVisible = false;
                drawboard(scalee,x,y);
                var command = Console.ReadKey().Key;
                switch (command)
                {
                    case ConsoleKey.RightArrow:
                        if (x == 7)
                        {
                            x = 0;
                        }
                        else
                        {
                            x++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x == 0)
                        {
                            x = 7;
                        }
                        else
                        {
                            x--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (y == 7)
                        {
                            y = 7;
                        }
                        else
                        {
                            y++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y == 0)
                        {
                            y = 0;
                        }
                        else
                        {
                            y--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        movethepiece(x, y, scalee);
                        break;
                }
            }
          
            
        }
        public void drawboard(int scale, int a, int b)
        {

                string symb = " ";
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        for (int i = 0; i < scale; i++)
                        {
                            Console.SetCursorPosition(y * scale * 2, i + x * scale);
                            for (int j = 0; j < scale; j++)
                            {
                                if(a == y && b == x)
                                {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.BackgroundColor = ConsoleColor.Red;
                                }
                            else
                            {
                                if (y % 2 == 0)
                                {
                                    if (x % 2 == 0)
                                    {
                                        Console.BackgroundColor = ConsoleColor.White;
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    }
                                }
                                else
                                {
                                    if (x % 2 == 1)
                                    {
                                        Console.BackgroundColor = ConsoleColor.White;
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    }
                                }
                            }
                                Console.Write(symb + " ");
                                Console.BackgroundColor = ConsoleColor.Black;
                            }
                        }
                    }
                }
            foreach (Piece p in WhitePieces)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(p.Coordinates.x * scale*2 + scale  , p.Coordinates.y * scale + scale / 2 );
                Console.Write(p.name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            foreach (Piece p in BlackPieces)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(p.Coordinates.x * scale*2 + scale , p.Coordinates.y * scale + scale / 2 );
                Console.Write(p.name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(this.scale*8 + 1, 0);
            Console.WriteLine(a + " " + b);

        }
        public void movethepiece(int x, int y, int scalee)
        {
            string name = "";
            Coordinates c = new Coordinates();
            int modulo = movecounter % 2;
            int color = 0;
            switch (modulo)
            {
                case 0:
                    foreach (Piece wp in WhitePieces)
                    {
                        if (wp.Coordinates.x == x && wp.Coordinates.y == y)
                        {
                            name = wp.name;
                            c = wp.Coordinates;
                            color = wp.color;
                        }
                    }
                    break;
                case 1:
                    foreach (Piece bp in BlackPieces)
                    {
                        if (bp.Coordinates.x == x && bp.Coordinates.y == y)
                        {
                            name = bp.name;
                            c = bp.Coordinates;
                            color = bp.color;
                        }
                    }
                    break;
            }
            if (name != "")
            {
                int index = 1;
                List<Coordinates> legalmoves = p.move(name, c, WhitePieces, BlackPieces, movecounter, color);
                if (legalmoves.Count > 0)
                {
                    foreach (Coordinates move in legalmoves)
                    {
                        Console.SetCursorPosition(scale*8 + 1, index + 1);
                        Console.WriteLine("Available Move [" + index + "]" + move.x + "," + move.y);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition((move.x * scale + scale / 3 + 1)-2, move.y * scale / 2 + scale / 5);
                        Console.Write("{"+index+"}");
                        Console.ForegroundColor = ConsoleColor.White;
                        index++;
                    }
                    Console.SetCursorPosition(scale*8 + 1, index + 2);
                    Console.WriteLine("Select the INDEX to move the PIECE, 0 to exit");
                    int select = int.Parse(Console.ReadLine());
                    if (select == 0)
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        if(modulo == 0)
                        {
                            foreach (Piece wp in WhitePieces)
                            {
                                if (wp.Coordinates == c)
                                {
                                    wp.Coordinates = legalmoves[select - 1];
                                    for (int i = 0; i < BlackPieces.Count; i++)
                                    {
                                        if (BlackPieces[i].Coordinates.x == wp.Coordinates.x && BlackPieces[i].Coordinates.y == wp.Coordinates.y)
                                        {
                                            BlackPieces.RemoveAt(i);
                                        }
                                    }
                                    movecounter++;
                                }
                            }
                        }
                        if(modulo == 1)
                        {
                            foreach (Piece bp in BlackPieces)
                            {
                                if (bp.Coordinates == c)
                                {
                                    bp.Coordinates = legalmoves[select - 1];
                                    for (int i = 0; i < WhitePieces.Count; i++)
                                    {
                                        if (WhitePieces[i].Coordinates.x == bp.Coordinates.x && WhitePieces[i].Coordinates.y == bp.Coordinates.y)
                                        {
                                            WhitePieces.RemoveAt(i);
                                        }
                                    }
                                    movecounter++;
                                }
                            }
                            
                        }          
                    }
                
                }
                else
                {
                    drawboard(scalee, x, y);
                    Console.SetCursorPosition(scale * 8 + 1, index + 2);
                    Console.WriteLine("No move available haha lol");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                drawboard(scalee, x, y);
                Console.SetCursorPosition(this.scale * 8 + 1, 3);
                Console.WriteLine("ITS NOT YOUR PIECE, DUMB FUCK!");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
