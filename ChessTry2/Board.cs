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
        public ConsoleColor primary { get; set; }
        public ConsoleColor secondary { get; set; }
        public Board(int scale, ConsoleColor primary, ConsoleColor secondary)
        {
            this.primary = primary;
            this.secondary = secondary;
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
                        Piece Rook1 = new Piece("R", new Coordinates(x, 7), 0);
                        WhitePieces.Add(Rook1);
                        break;
                    case 1:
                        Piece Knight1 = new Piece("H", new Coordinates(x, 7), 0);
                        WhitePieces.Add(Knight1);
                        break;
                    case 2:
                        Piece Bishop1 = new Piece("B", new Coordinates(x, 7), 0);
                        WhitePieces.Add(Bishop1);
                        break;
                    case 3:
                        Piece Queen = new Piece("Q", new Coordinates(x, 7), 0);
                        WhitePieces.Add(Queen);
                        break;
                    case 4:
                        Piece King = new Piece("K", new Coordinates(x, 7), 0);
                        WhitePieces.Add(King);
                        break;
                    case 5:
                        Piece Bishop2 = new Piece("B", new Coordinates(x, 7), 0);
                        WhitePieces.Add(Bishop2);
                        break;
                    case 6:
                        Piece Knight2 = new Piece("H", new Coordinates(x, 7), 0);
                        WhitePieces.Add(Knight2);
                        break;
                    case 7:
                        Piece Rook2 = new Piece("R", new Coordinates(x, 7), 0);
                        WhitePieces.Add(Rook2);
                        break;
                }
                BlackPieces = new List<Piece>();
                for (int b = 0; b < 8; b++)
                {
                    Piece piece = new Piece("P", new Coordinates(b, 1), 1);
                    BlackPieces.Add(piece);
                }
                for (int b = 0; b < 8; b++)
                {
                    switch (b)
                    {
                        case 0:
                            Piece Rook1 = new Piece("R", new Coordinates(b, 0), 1);
                            BlackPieces.Add(Rook1);
                            break;
                        case 1:
                            Piece Knight1 = new Piece("H", new Coordinates(b, 0), 1);
                            BlackPieces.Add(Knight1);
                            break;
                        case 2:
                            Piece Bishop1 = new Piece("B", new Coordinates(b, 0), 1);
                            BlackPieces.Add(Bishop1);
                            break;
                        case 3:
                            Piece Queen = new Piece("Q", new Coordinates(b, 0), 1);
                            BlackPieces.Add(Queen);
                            break;
                        case 4:
                            Piece King = new Piece("K", new Coordinates(b, 0), 1);
                            BlackPieces.Add(King);
                            break;
                        case 5:
                            Piece Bishop2 = new Piece("B", new Coordinates(b, 0), 1);
                            BlackPieces.Add(Bishop2);
                            break;
                        case 6:
                            Piece Knight2 = new Piece("H", new Coordinates(b, 0), 1);
                            BlackPieces.Add(Knight2);
                            break;
                        case 7:
                            Piece Rook2 = new Piece("R", new Coordinates(b, 0), 1);
                            BlackPieces.Add(Rook2);
                            break;
                    }
                }
            }
        }
        public void select()
        {
            int scalee = scale / 2;
            int x = 0;
            int y = 0;
            while (true)
            {
                Console.CursorVisible = false;
                drawboard(scalee, x, y);
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
                            if (a == y && b == x)
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
                                        Console.BackgroundColor = primary;
                                        Console.ForegroundColor = primary;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = secondary;
                                        Console.ForegroundColor = secondary;
                                    }
                                }
                                else
                                {
                                    if (x % 2 == 1)
                                    {
                                        Console.BackgroundColor = primary;
                                        Console.ForegroundColor = primary;
                                    }
                                    else
                                    {
                                        Console.BackgroundColor = secondary;
                                        Console.ForegroundColor = secondary;
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

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor= determinebackground(p.Coordinates.x, p.Coordinates.y);
                if(p.Coordinates.x == a && p.Coordinates.y == b)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                drawpiece(p.Coordinates.x, p.Coordinates.y, scale * 2, p.name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            foreach (Piece p in BlackPieces)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.BackgroundColor = determinebackground(p.Coordinates.x, p.Coordinates.y);
                if (p.Coordinates.x == a && p.Coordinates.y == b)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                drawpiece(p.Coordinates.x, p.Coordinates.y, scale * 2, p.name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.SetCursorPosition(this.scale * 8 + 1, 0);
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
                        Console.SetCursorPosition(scale * 8 + 1, index + 1);
                        Console.WriteLine("Available Move [" + index + "]" + move.x + "," + move.y);
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = determinebackground(move.x, move.y);
                        Console.SetCursorPosition((move.x * scale + scale / 3 + 1) - 2, move.y * scale / 2 + scale / 6);
                        Console.Write(index);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        index++;
                    }
                    Console.SetCursorPosition(scale * 8 + 1, index + 2);
                    Console.WriteLine("Select the INDEX to move the PIECE, 0 to exit");
                    int select = int.Parse(Console.ReadLine());
                    if (select == 0)
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        if (modulo == 0)
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
                        if (modulo == 1)
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
        public void drawpiece(int x, int y, int scaleee, string name)
        {
            switch (name)
            {
                case "P":
                    for (int i = 0; i < 4; i++)
                    {
                        int xmove = 0;
                        if (i == 0)
                        {
                            xmove = 3;
                        }
                        if (i == 1)
                        {
                            xmove = 2;
                        }
                        if (i == 2)
                        {
                            xmove = 3;
                        }
                        if (i == 3)
                        {
                            xmove = 1;
                        }
                        string pawn1 = "▄▄";
                        string pawn2 = "████";
                        string pawn3 = "██";
                        string pawn4 = "▄████▄";
                        string[] pawn = { pawn1, pawn2, pawn3, pawn4 };
                        Console.SetCursorPosition((x * scaleee) + xmove, (y * scaleee / 2) + i);
                        Console.Write(pawn[i]);
                    }
                    break;
                case "H":
                    for (int i = 0; i < 4; i++)
                    {
                        int xmove = 0;
                        if (i == 0)
                        {
                            xmove = 2;
                        }
                        if (i == 1)
                        {
                            xmove = 1;
                        }
                        if (i == 2)
                        {
                            xmove = 3;
                        }
                        if (i == 3)
                        {
                            xmove = 1;
                        }
                        string knight1 = "█▄▄█";
                        string knight2 = "▄████▀";
                        string knight3 = "██";
                        string knight4 = "▄████▄";
                        string[] knight = { knight1, knight2, knight3, knight4 };
                        Console.SetCursorPosition((x * scaleee) + xmove, (y * scaleee / 2) + i);
                        Console.Write(knight[i]);
                    }
                    break;
                case "B":
                    for (int i = 0; i < 4; i++)
                    {
                        int xmove = 0;
                        if (i == 0)
                        {
                            xmove = 1;
                        }
                        if (i == 1)
                        {
                            xmove = 3;
                        }
                        if (i == 2)
                        {
                            xmove = 3;
                        }
                        if (i == 3)
                        {
                            xmove = 1;
                        }
                        string bishop1 = "▄▄██▄▄";
                        string bishop2 = "██";
                        string bishop3 = "██";
                        string bishop4 = "▄████▄";
                        string[] bishop = { bishop1, bishop2, bishop3, bishop4 };
                        Console.SetCursorPosition((x * scaleee) + xmove, (y * scaleee / 2) + i);
                        Console.Write(bishop[i]);
                    }
                    break;
                case "R":
                    for (int i = 0; i < 4; i++)
                    {
                        int xmove = 0;
                        if (i == 0)
                        {
                            xmove = 1;
                        }
                        if (i == 1)
                        {
                            xmove = 2;
                        }
                        if (i == 2)
                        {
                            xmove = 2;
                        }
                        if (i == 3)
                        {
                            xmove = 1;
                        }
                        string rook1 = "█▄▄▄▄█";
                        string rook2 = "████";
                        string rook3 = "████";
                        string rook4 = "▄████▄";
                        string[] rook = { rook1, rook2, rook3, rook4 };
                        Console.SetCursorPosition((x * scaleee) + xmove, (y * scaleee / 2) + i);
                        Console.Write(rook[i]);
                    }
                    break;
                case "Q":
                    for (int i = 0; i < 4; i++)
                    {
                        int xmove = 0;
                        if (i == 0)
                        {
                            xmove = 0;
                        }
                        if (i == 1)
                        {
                            xmove = 1;
                        }
                        if (i == 2)
                        {
                            xmove = 1;
                        }
                        if (i == 3)
                        {
                            xmove = 0;
                        }
                        string queen1 = "█▄█▄▄█▄█";
                        string queen2 = "▄████▄";
                        string queen3 = "▀████▀";
                        string queen4 = "▄▄████▄▄";
                        string[] queen = { queen1, queen2, queen3, queen4 };
                        Console.SetCursorPosition((x * scaleee) + xmove, (y * scaleee / 2) + i);
                        Console.Write(queen[i]);
                    }
                    break;
                case "K":
                    for (int i = 0; i < 4; i++)
                    {
                        int xmove = 0;
                        if (i == 0)
                        {
                            xmove = 1;
                        }
                        if (i == 1)
                        {
                            xmove = 0;
                        }
                        if (i == 2)
                        {
                            xmove = 0;
                        }
                        if (i == 3)
                        {
                            xmove = 0;
                        }
                        string king1 = "▀▀██▀▀";
                        string king2 = "▀▄████▄▀";
                        string king3 = "▄▀████▀▄";
                        string king4 = "▄▄████▄▄";
                        string[] king = { king1, king2, king3, king4 };
                        Console.SetCursorPosition((x * scaleee) + xmove, (y * scaleee / 2) + i);
                        Console.Write(king[i]);
                    }
                    break;
            }
        }
        public ConsoleColor determinebackground(int x, int y)
        {
            if(y%2 == 0)
            {
                if(x%2 == 0)
                {
                    return primary;
                }
                else
                {
                    return secondary;
                }
            }
            else
            {
                if(x%2 == 0)
                {
                    return secondary;
                }
                else
                {
                    return primary;
                }
            }
            return 0;
        }
    }
}
