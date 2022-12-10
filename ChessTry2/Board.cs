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
            int x = 0;
            int y = 0;
            while (true)
            {
                Console.CursorVisible = false;
                Console.Clear();
                draw(x, y);
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
                        movethepiece(x,y);
                        break;
                }
            }
          
            void draw(int a, int b)
            {
                foreach (Cell c in Cells)
                {
                    string symb = "▀";
                    Console.SetCursorPosition(c.Coordinates.x * scale, c.Coordinates.y * scale / 2);
                    for (int x = 0; x <= scale / 2; x++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if(c.Coordinates.x == a && c.Coordinates.y == b)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if(c.Coordinates.x == a && c.Coordinates.y - 1 == b)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if(c.Coordinates.x - 1 == a && c.Coordinates.y == b && x == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (c.Coordinates.x - 1 == a && c.Coordinates.y - 1 == b && x == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write(symb);
                        Console.Write(" ");
                    }
                 
                    for (int y = 1; y <= scale / 2 ; y++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (c.Coordinates.x == a && c.Coordinates.y == b)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(c.Coordinates.x * scale, c.Coordinates.y * scale / 2 + y);
                            Console.Write(symb);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if(c.Coordinates.x - 1 == a && c.Coordinates.y == b)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(c.Coordinates.x * scale, c.Coordinates.y * scale / 2 + y);
                            Console.Write(symb);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.SetCursorPosition(c.Coordinates.x * scale, c.Coordinates.y * scale / 2 + y);
                            Console.Write(symb);
                        }
                    }
                    if (c.Coordinates.x == 7)
                    {
                        for (int x = 1; x <= scale / 2; x++)
                        {
                            if (c.Coordinates.x == a && c.Coordinates.y == b)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            Console.SetCursorPosition((c.Coordinates.x + 1) * scale, c.Coordinates.y * scale / 2 + x);
                            Console.Write(symb);
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (c.Coordinates.y == 7)
                    {
                        
                        Console.SetCursorPosition(c.Coordinates.x * scale, (c.Coordinates.y + 1) * scale / 2);
                        for (int x = 0 ; x <= scale / 2; x++)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            if (c.Coordinates.x == a && c.Coordinates.y == b)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            else if (c.Coordinates.x - 1 == a && c.Coordinates.y == b && x == 0) 
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            } 
                            Console.Write(symb);
                            Console.Write(" ");
                        }
     
                    }
                }
                foreach (Piece p in WhitePieces)
                {

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(p.Coordinates.x * scale + scale / 2, p.Coordinates.y * scale / 2 + scale / 4);
                        Console.Write(p.name);
                        Console.ForegroundColor = ConsoleColor.White;
                }
                foreach (Piece p in BlackPieces)
                {      
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(p.Coordinates.x * scale + scale / 2, p.Coordinates.y * scale / 2 + scale / 4);
                        Console.Write(p.name);
                        Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition(100, 0);
                Console.WriteLine(a + " " + b);
            }
            
        }
        public void movethepiece(int x, int y)
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
                        Console.SetCursorPosition(100, index + 1);
                        Console.WriteLine("Available Move [" + index + "]" + move.x + "," + move.y);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(move.x * scale + scale / 3 + 1, move.y * scale / 2 + scale / 5);
                        Console.Write("{"+index+"}");
                        Console.ForegroundColor = ConsoleColor.White;
                        index++;
                    }
                    Console.SetCursorPosition(100, index + 2);
                    Console.WriteLine("Select the INDEX to move the PIECE, 0 to exit");
                    int select = int.Parse(Console.ReadLine());
                    if (select == 0)
                    {

                    }
                    else
                    {
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
                    Console.SetCursorPosition(100, index + 2);
                    Console.WriteLine("No move available haha lol");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.SetCursorPosition(100, 3);
                Console.WriteLine("ITS NOT YOUR PIECE, DUMB FUCK!");
                Console.ReadKey();
            }
        }
    }
}
