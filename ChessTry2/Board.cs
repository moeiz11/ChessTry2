using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Timers;
using System.Xml;
namespace ChessTry2
{
    struct ec
    {
        public ec(Coordinates cpos, List<Coordinates> pmoves)
        {
            this.cpos = cpos;
            this.pmoves = pmoves;
        }
        public Coordinates cpos { get; set; }
        public List<Coordinates> pmoves { get; set; }
    }
    internal class Board
    {
        public List<Piece> WhitePieces { get; set; }
        public List<Piece> BlackPieces { get; set; }
        public List<Cell> Cells { get; set; }
        public Piece p { get; set; }
        public int scale { get; set; }
        public int movecounter { get; set; } = 0;
        public int changecounter { get; set; } = 0;
        public ConsoleColor primary { get; set; }
        public ConsoleColor secondary { get; set; }
        public Piece lastpiece { get; set; }

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
        public Coordinates getkingcoords(int color, List<Piece> wp, List<Piece> bp)
        {
            if (color == 0)
            {
                Coordinates coordinates = new Coordinates();
                foreach (Piece piece in wp)
                {
                    if (piece.name == "K")
                    {
                        coordinates = piece.Coordinates;
                    }
                }
                return coordinates;
            }
            else
            {
                Coordinates coordinates = new Coordinates();
                foreach (Piece piece in bp)
                {
                    if (piece.name == "K")
                    {
                        coordinates = piece.Coordinates;
                    }
                }
                return coordinates;
            }      
        }
        public bool checkthreat(List<Piece> WhitePiecesList, List<Piece> BlackPiecesList)
        {
            List<Piece> wpl = WhitePiecesList;
            List<Piece> bpl = BlackPiecesList;
            Piece WhiteKing = new Piece(getkingcoords(0,wpl, bpl));
            Piece BlackKing = new Piece(getkingcoords(1,wpl,bpl));  
            if(movecounter%2 == 0)
            {
               foreach(Piece bp in bpl)
                {
                    List<Coordinates> fmoves = bp.move(bp.name,bp.Coordinates,wpl,bpl,bp.color);
                    foreach (Coordinates fmove in fmoves)
                    {
                        if (fmove.x == WhiteKing.Coordinates.x && fmove.y == WhiteKing.Coordinates.y)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                foreach(Piece wp in wpl)
                {
                    List<Coordinates> fmoves = wp.move(wp.name, wp.Coordinates, wpl, bpl, wp.color);
                    foreach (Coordinates fmove in fmoves)
                    {
                        if (fmove.x == BlackKing.Coordinates.x && fmove.y == BlackKing.Coordinates.y)
                        {
                            return true;
                        }
                    }        
                }
            }
            return false;
        }
        public Coordinates enpassant(string name, List<Piece> whitepieceslist, List<Piece> blackpieceslist, int color, Piece lastpiece, Coordinates c)
        {
            List<Piece> wpl = whitepieceslist;
            List<Piece> bpl = blackpieceslist;
            bool passant = false;
            if (name == "P" && c.y == 3 && movecounter > 0 && color == 0 && lastpiece.name == "P" && lastpiece.color == 1 && lastpiece.step == 0)
            {
                foreach(Piece wp in wpl)
                {
                    if(wp.name == "P" && wp.Coordinates.y == lastpiece.Coordinates.y)
                    {
                        if(wp.Coordinates.x - 1 == lastpiece.Coordinates.x || wp.Coordinates.x + 1 == lastpiece.Coordinates.x)
                        {
                           // Console.Clear();
                           // Console.WriteLine("VERARSCHT");
                           // Console.ReadKey();
                            passant = true;
                        }
                    }
                    if (passant)
                    {

                       Coordinates pas = new Coordinates(lastpiece.Coordinates.x, lastpiece.Coordinates.y - 1);
                       return pas;
                    }
                }
            }
            if (name == "P" && c.y == 4 && movecounter > 0 && color == 1 && lastpiece.name == "P" && lastpiece.color == 0 && lastpiece.step == 0)
            {
                foreach(Piece bp in bpl)
                {
                    if (bp.name == "P" && bp.Coordinates.y == lastpiece.Coordinates.y)
                    {
                        if(bp.Coordinates.x - 1 == lastpiece.Coordinates.x || bp.Coordinates.x + 1 == lastpiece.Coordinates.x)
                        {
                           // Console.Clear();
                           // Console.WriteLine("VERARSCHT");
                           // Console.ReadKey();
                            passant = true;
                        }
                    }
                    if (passant)
                    {
                       Coordinates pas = new Coordinates(lastpiece.Coordinates.x, lastpiece.Coordinates.y + 1);
                       return pas;
                    }
                }
            }
            return null;
        }
        public List<ec> checkmate(List<Piece> WhitePiecesList, List<Piece> BlackPiecesList)
        {
           List<Piece> wpl = WhitePiecesList;
           List<Piece> bpl = BlackPiecesList;
           List<ec> onlypossiblepieces = new List<ec>();
           if(movecounter%2 == 0)
           {
               foreach(Piece wp in wpl)
               {
                    List<Coordinates> fmoves = wp.move(wp.name, wp.Coordinates, wpl, bpl, wp.color);
                    Coordinates cpos = wp.Coordinates;
                    List<Coordinates> pmoves = new List<Coordinates>();
                    foreach (Coordinates fmove in fmoves)
                    {
                        wp.Coordinates = fmove;
                        List<Piece> blackpieceschanged = new List<Piece>(); 
                        foreach(Piece bp in bpl)
                        {
                            if(fmove.x != bp.Coordinates.x || fmove.y != bp.Coordinates.y)
                            {
                                blackpieceschanged.Add(bp);
                            }
                        }
                        if(blackpieceschanged.Count == bpl.Count)
                        {
                            if (!checkthreat(wpl, bpl))
                            {
                                pmoves.Add(fmove);
                            }
                        }
                        else
                        {
                            if (!checkthreat(wpl, blackpieceschanged))
                            {
                                pmoves.Add(fmove);
                            }
                        }
                    }
                    ec p = new ec(cpos, pmoves);
                    onlypossiblepieces.Add(p);
                    wp.Coordinates = cpos;
               }
           }
           else
           {
               foreach (Piece bp in bpl)
               {
                   List<Coordinates> fmoves = bp.move(bp.name, bp.Coordinates, wpl, bpl, bp.color);
                   Coordinates cpos = bp.Coordinates;
                   List<Coordinates> pmoves = new List<Coordinates>();
                   foreach (Coordinates fmove in fmoves)
                   {
                       bp.Coordinates = fmove;
                       List<Piece> whitepieceschanged = new List<Piece>();
                        foreach (Piece wp in wpl)
                        {
                            if (fmove.x != bp.Coordinates.x || fmove.y != bp.Coordinates.y)
                            {
                                whitepieceschanged.Add(wp);
                            }
                        }
                        if(whitepieceschanged.Count == wpl.Count)
                        {
                            if (!checkthreat(wpl, bpl))
                            {
                                pmoves.Add(fmove);
                            }
                        }
                        else
                        {
                            if (!checkthreat(whitepieceschanged, bpl))
                            {
                                pmoves.Add(fmove);
                            }
                        }             
                   }
                   ec p = new ec(cpos, pmoves);
                   onlypossiblepieces.Add(p);
                   bp.Coordinates = cpos;
               }
           }
           return onlypossiblepieces;
        }
        public void start()
        {
            bool run = true;
            int scalee = scale / 2;
            int x = 0;
            int y = 0;
            drawboard(scalee, x, y);
            while (run)
            {
                Console.CursorVisible = false;
                bool check = false;
                drawboard(scalee, x, y);
                List<ec> lmoves = new List<ec>(); 
                if (checkthreat(WhitePieces, BlackPieces))
                {
                    check = true;
                    List<ec> legalmoves = checkmate(WhitePieces, BlackPieces);
                    bool checkmated = true;
                    for (int j = 0; j < legalmoves.Count; j++)
                    {
                        if (legalmoves[j].pmoves.Count > 0)
                        {
                            checkmated = false;
                        }
                    }
                    if (checkmated)
                    {
                        break;
                    }
                    lmoves = legalmoves;
                }
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
                        if (check)
                        {
                            checkmoves(x, y, scalee, lmoves);
                        }
                        else
                        {
                            movethepiece(x, y, scalee);
                        }
                        break;
                }
            }
        }

        public void checkmoves(int x, int y, int scalee, List<ec> legalmoves)
        {
            if (movecounter % 2 == 0)
            {
                foreach (Piece wp in WhitePieces)
                {
                    if (x == wp.Coordinates.x && y == wp.Coordinates.y)
                    {
                        foreach (ec pm in legalmoves)
                        {
                            if (wp.Coordinates.x == pm.cpos.x && wp.Coordinates.y == pm.cpos.y)
                            {
                                movethepiece(scalee, x, y, pm.pmoves);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Piece bp in BlackPieces)
                {
                    if (x == bp.Coordinates.x && y == bp.Coordinates.y)
                    {
                        foreach (ec pm in legalmoves)
                        {
                            if (bp.Coordinates.x == pm.cpos.x && bp.Coordinates.y == pm.cpos.y)
                            {
                                movethepiece(scalee, x, y, pm.pmoves);
                            }
                        }
                    }
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
                Console.BackgroundColor = determinebackground(p.Coordinates.x, p.Coordinates.y);
                if (p.Coordinates.x == a && p.Coordinates.y == b)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                drawpiece(p.Coordinates.x, p.Coordinates.y, this.scale, p.name);
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
                drawpiece(p.Coordinates.x, p.Coordinates.y, this.scale, p.name);
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
                bool passant = false;
                List<Coordinates> pseudolegalmoves = p.move(name, c, WhitePieces, BlackPieces, color);
                Coordinates enpas = enpassant(name,WhitePieces, BlackPieces, color, this.lastpiece, c);
                if (enpas != null)
                {
                    pseudolegalmoves.Add(enpas);
                    passant = true;
                }
                List<Coordinates> legalmoves = new List<Coordinates>();
                if (modulo == 0)
                {
                    foreach (Piece piece in WhitePieces)
                    {
                        if (piece.Coordinates.x == c.x && piece.Coordinates.y == y)
                        {
                            foreach (Coordinates fmove in pseudolegalmoves)
                            {
                                piece.Coordinates = fmove;
                                List<Piece> blackpieceschanged = new List<Piece>();
                                foreach (Piece bp in BlackPieces)
                                {
                                    if (fmove.x != bp.Coordinates.x || fmove.y != bp.Coordinates.y)
                                    {
                                        if (passant == true && lastpiece == bp)
                                        { 
                                        }
                                        else
                                        {
                                            blackpieceschanged.Add(bp);
                                        }
                                    }
                                }
                                if (blackpieceschanged.Count == BlackPieces.Count)
                                {
                                    if (!checkthreat(WhitePieces, BlackPieces))
                                    {
                                        legalmoves.Add(fmove);
                                    }
                                }
                                else
                                {
                                    if (!checkthreat(WhitePieces, blackpieceschanged))
                                    {
                                        legalmoves.Add(fmove);
                                    }
                                }
                                piece.Coordinates = c;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Piece piece in BlackPieces)
                    {
                        if (piece.Coordinates.x == c.x && piece.Coordinates.y == y)
                        {
                            foreach (Coordinates fmove in pseudolegalmoves)
                            {
                                piece.Coordinates = fmove;
                                List<Piece> whitepieceschanged = new List<Piece>();
                                foreach (Piece wp in WhitePieces)
                                {
                                    if (fmove.x != wp.Coordinates.x || fmove.y != wp.Coordinates.y)
                                    {
                                        if (passant == true && lastpiece == wp)
                                        {
                                        }
                                        else
                                        {
                                            whitepieceschanged.Add(wp);
                                        }
                                    }
                                }
                                if (whitepieceschanged.Count == WhitePieces.Count)
                                {
                                    if (!checkthreat(WhitePieces, BlackPieces))
                                    {
                                        legalmoves.Add(fmove);
                                    }
                                }
                                else
                                {
                                    if (!checkthreat(whitepieceschanged, BlackPieces))
                                    {
                                        legalmoves.Add(fmove);
                                    }
                                }
                                piece.Coordinates = c;
                            }
                        }
                    }
                }
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
                                    if (wp.name == "P" && c.y == 6 && legalmoves[select - 1].y + 2 == c.y)
                                    {
                                        wp.step = wp.step - 1;
                                    }
                                    else
                                    {
                                        wp.step = 2;
                                    }
                                    if (enpas != null)
                                    {
                                        if(enpas == legalmoves[select - 1])
                                        {
                                            for (int i = 0; i < BlackPieces.Count; i++)
                                            {
                                                if (BlackPieces[i].Coordinates.x == wp.Coordinates.x && BlackPieces[i].Coordinates.y == wp.Coordinates.y + 1)
                                                {
                                                    BlackPieces.RemoveAt(i);
                                                }
                                            }
                                        }
                                    }
                                    for (int i = 0; i < BlackPieces.Count; i++)
                                    {
                                        if (BlackPieces[i].Coordinates.x == wp.Coordinates.x && BlackPieces[i].Coordinates.y == wp.Coordinates.y)
                                        {
                                            BlackPieces.RemoveAt(i);
                                        }
                                    }
                                    lastpiece = wp;
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
                                    if (bp.name == "P" && c.y == 1 && legalmoves[select - 1].y - 2 == c.y)
                                    {
                                        bp.step = bp.step - 1;
                                    }
                                    else
                                    {
                                        bp.step = 2;
                                    }
                                    if (enpas != null)
                                    {
                                        if (enpas == legalmoves[select - 1])
                                        {
                                            for (int i = 0; i < WhitePieces.Count; i++)
                                            {
                                                if (WhitePieces[i].Coordinates.x == bp.Coordinates.x && WhitePieces[i].Coordinates.y == bp.Coordinates.y - 1)
                                                {
                                                    WhitePieces.RemoveAt(i);
                                                }
                                            }
                                        }
                                    }
                                    for (int i = 0; i < WhitePieces.Count; i++)
                                    {
                                        if (WhitePieces[i].Coordinates.x == bp.Coordinates.x && WhitePieces[i].Coordinates.y == bp.Coordinates.y)
                                        { 
                                            WhitePieces.RemoveAt(i);
                                        }
                                    }
                                    lastpiece = bp;
                                    movecounter++;
                                }
                            }

                        }
                        promotionchecker(color, scalee,x,y);
                    }

                }
                else
                {
                    Console.SetCursorPosition(scale * 8 + 1, 3);
                    Console.WriteLine("No move available haha lol");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.SetCursorPosition(this.scale * 8 + 1, 3);
                Console.WriteLine("ITS NOT YOUR PIECE, DUMB FUCK!");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void movethepiece(int scalee, int x, int y, List<Coordinates> coordinates)
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
                List<Coordinates> legalmoves = coordinates;
                Coordinates enpas = enpassant(name, WhitePieces, BlackPieces, color, this.lastpiece, c);
                if (enpas != null)
                { 
                   
                    if(color == 0)
                    {
                        foreach (Piece wp in WhitePieces)
                        {
                            if (wp.Coordinates.x == c.x && wp.Coordinates.y == c.y)
                            {
                                wp.Coordinates = enpas;
                                List<Piece> bpl = new List<Piece>();
                                foreach(Piece bp in BlackPieces)
                                {
                                    if (bp.Coordinates.x == wp.Coordinates.x && bp.Coordinates.y == wp.Coordinates.y + 1)
                                    {
                                    }
                                    else
                                    {
                                        bpl.Add(bp);
                                    }
                                }
                                if (!checkthreat(WhitePieces, bpl))
                                {
                                    legalmoves.Add(enpas);
                                }
                                wp.Coordinates = c;
                            }
                        }
                    }
                    else
                    {
                        foreach (Piece bp in BlackPieces)
                        {
                            if (bp.Coordinates.x == c.x && bp.Coordinates.y == c.y)
                            {
                                bp.Coordinates = enpas;
                                List<Piece> wpl = new List<Piece>();
                                foreach (Piece wp in WhitePieces)
                                {
                                    if (wp.Coordinates.x == bp.Coordinates.x && wp.Coordinates.y == bp.Coordinates.y - 1)
                                    {
                                    }
                                    else
                                    {
                                        wpl.Add(wp);
                                    }
                                }
                                if (!checkthreat(wpl, BlackPieces))
                                {
                                    legalmoves.Add(enpas);
                                }
                                bp.Coordinates = c;
                            }
                        }
                    }
                }
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
                                    if (wp.name == "P" && c.y == 6 && legalmoves[select - 1].y + 2 == c.y)
                                    {
                                        wp.step = wp.step - 1;
                                    }
                                    else
                                    {
                                        wp.step = 2;
                                    }
                                    if (enpas != null)
                                    {
                                        if (enpas == legalmoves[select - 1])
                                        {
                                            for (int i = 0; i < BlackPieces.Count; i++)
                                            {
                                                if (BlackPieces[i].Coordinates.x == wp.Coordinates.x && BlackPieces[i].Coordinates.y == wp.Coordinates.y + 1)
                                                {
                                                    BlackPieces.RemoveAt(i);
                                                }
                                            }
                                        }
                                    }
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
                                    if (bp.name == "P" && c.y == 1 && legalmoves[select - 1].y - 2 == c.y)
                                    {
                                        bp.step = bp.step - 1;
                                    }
                                    else
                                    {
                                        bp.step = 2;
                                    }
                                    if (enpas != null)
                                    {
                                        if (enpas == legalmoves[select - 1])
                                        {
                                            for (int i = 0; i < WhitePieces.Count; i++)
                                            {
                                                if (WhitePieces[i].Coordinates.x == bp.Coordinates.x && WhitePieces[i].Coordinates.y == bp.Coordinates.y - 1)
                                                {
                                                    WhitePieces.RemoveAt(i);
                                                }
                                            }
                                        }
                                    }
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
                        promotionchecker(color, scalee, x, y);
                    }
                }
                else
                {
                    Console.SetCursorPosition(scale * 8 + 1, index + 2);
                    Console.WriteLine("No move available haha lol");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
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
                            xmove = 1;
                        }
                        if(i == 3)
                        {
                            xmove = 1;
                        }
                        string pawn1 =  "▄▄";
                        string pawn2 = "████";
                        string pawn3 ="▄▄██▄▄";
                        string pawn4 ="▀▀▀▀▀▀"; 	
                        string[] pawn = { pawn1, pawn2, pawn3, pawn4};
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
                        string knight1 =    "▄  ▄";
                        string knight2 =  "▄████▀";
                        string knight3 =    "██";
                        string knight4 =  "▀▀▀▀▀▀";
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
                            xmove = 3;
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
                        string bishop1 =   "▄▄";
                        string bishop2 = "▀▀██▀▀";
                        string bishop3 =   "██";
                        string bishop4 = "▀▀▀▀▀▀";
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
                            xmove = 1;
                        }
                        if (i == 2)
                        {
                            xmove = 2;
                        }
                        if (i == 3)
                        {
                            xmove = 1;
                        }
                        string rook1 ="▄    ▄";
                        string rook2 ="▀████▀";
                        string rook3 = "████";
                        string rook4 ="▀▀▀▀▀▀";
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
                            xmove = 1;
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
                            xmove = 1;
                        }
                        string queen1 = "▄ ▄▄ ▄";
                        string queen2 = "▄████▄";
                        string queen3 = "▀████▀";
                        string queen4 = "▀▀▀▀▀▀";
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
                            xmove = 3;
                        }
                        if (i == 1)
                        {
                            xmove = 0;
                        }
                        if (i == 2)
                        {
                            xmove = 1;
                        }
                        if (i == 3)
                        {
                            xmove = 1;
                        }
                        string king1 =    "▄▄";
                        string king2 = "▀▄████▄▀";
                        string king3 =  "▀████▀";
                        string king4 =  "▀▀▀▀▀▀";
                        string[] king = { king1, king2, king3, king4 };
                        Console.SetCursorPosition((x * scaleee) + xmove, (y * scaleee / 2) + i);
                        Console.Write(king[i]);
                    }
                    break;
            }
        }

        public void promotionchecker(int color, int scle, int x, int y)
        {
            switch (color)
            {
                case 0:
                    string[] figures = { "Q", "R", "H", "B" };
                    string[] figuresdp = { "Queen", "Rook", "Knight", "Bishop" };
                    foreach (Piece wp in WhitePieces)
                    {

                        if(wp.name == "P" && wp.Coordinates.y == 0)
                        {
                            drawboard(scle, x, y);
                            Console.SetCursorPosition(scale * 8 + 1, 2);
                            Console.WriteLine("YOUR PROMOTED!, select the corresponding index to transform the Piece.");
                            for (int i = 0; i < figures.Length; i++)
                            {
                                Console.SetCursorPosition(scale * 8 + 1, i + 4);
                                Console.WriteLine("{" + (i+1) + "}" + figuresdp[i]);
                            }
                            int choose = int.Parse(Console.ReadLine());
                            wp.name = figures[choose - 1];
                            Console.Clear();
                        }
                    }
                    break;
                case 1:
                    string[] bfigures = { "Q", "R", "H", "B" };
                    string[] bfiguresdp = { "Queen", "Rook", "Knight", "Bishop" };
                    foreach (Piece bp in BlackPieces)
                    {

                        if (bp.name == "P" && bp.Coordinates.y == 7)
                        {
                            drawboard(scle, x, y);
                            Console.SetCursorPosition(scale * 8 + 1, 2);
                            Console.WriteLine("YOUR PROMOTED!, select the corresponding index to transform the Piece.");
                            for (int i = 0; i < bfigures.Length; i++)
                            {
                                Console.SetCursorPosition(scale * 8 + 1, i + 4);
                                Console.WriteLine("{" + (i + 1) + "}" + bfiguresdp[i]);
                            }
                            int choose = int.Parse(Console.ReadLine());
                            bp.name = bfigures[choose - 1];
                            Console.Clear();
                        }
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

        public void testdraw()
        {
            List<ec> pp = checkmate(WhitePieces, BlackPieces);
            Console.Clear();
            foreach (ec p in pp)
            {
                string name = "";
                foreach (Piece piece in WhitePieces)
                {
                    if (p.cpos.x == piece.Coordinates.x && p.cpos.y == piece.Coordinates.y)
                    {
                        name = piece.name;
                    }
                }
                foreach (Piece piece in BlackPieces)
                {
                    if (p.cpos.x == piece.Coordinates.x && p.cpos.y == piece.Coordinates.y)
                    {
                        name = piece.name;
                    }
                }
                Console.WriteLine(name);
                foreach (Coordinates c in p.pmoves)
                {
                    Console.WriteLine("      Coordinates: " + c.x + "," + c.y);
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
