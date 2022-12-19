using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChessTry2
{
    internal class Piece
    {
        public string name { get; set; }
        public Coordinates Coordinates { get; set; }
        public int color { get; set; }
        public int movec { get; set; }
        public int step { get; set; } 
        public Piece()
        {

        }
        public Piece(Coordinates c)
        {
            this.Coordinates = c;
        }
        public Piece(string name, Coordinates Coordinates, int color)
        {
            this.name = name;
            this.Coordinates = Coordinates;
            this.color = color;
            movec = 0;
            if(name == "P")
            {
                step = 1;
            }
        }
        public List<Coordinates> move(string piece, Coordinates c, List<Piece> wp, List<Piece> bp, int color)
        {
            switch (piece)
            {
                case "P":
                    List<Coordinates> legalmovesp = new List<Coordinates>();
                    legalmovesp = pawn(c,wp,bp, color );
                    return legalmovesp;
                    break;
                case "K":
                    List<Coordinates> legalmovesk= new List<Coordinates>();
                    legalmovesk = king(c, wp, bp,color);
                    return legalmovesk;
                    break;
                case "B":
                    List<Coordinates> legalmovesb = new List<Coordinates>();
                    legalmovesb = bishop(c, wp, bp,color);
                    return legalmovesb;
                    break;
                case "H":
                    List<Coordinates> legalmovesh = new List<Coordinates>();
                    legalmovesh = knight(c, wp, bp,color);
                    return legalmovesh;
                    break;
                case "Q":
                    List<Coordinates> legalmovesq = new List<Coordinates>();
                    legalmovesq = queen(c, wp, bp,color);
                    return legalmovesq;
                    break;
                case "R":
                    List<Coordinates> legalmovesr = new List<Coordinates>();
                    legalmovesr = rook(c, wp, bp,color);
                    return legalmovesr;
                    break;
            }
            return null;
        }

        public List<Coordinates> pawn(Coordinates c, List<Piece> wp, List<Piece> bp, int color)
        {
            List<Coordinates> moves = legalmoves();
            for(int i = 0; i < moves.Count; i++)
            {
                if (moves[i].x < 0 || moves[i].y < 0 || moves[i].x > 7 || moves[i].y > 7)
                {
                    moves.RemoveAt(i);
                }
            }
            List<Coordinates> legalmoves()
            {
                if (color == 0)
                {
                    bool x = true;
                    bool y = false;
                    bool z = false;
                    List<Coordinates> movess = new List<Coordinates>();
                    foreach (Piece p1 in wp)
                    {
                        if (p1.Coordinates.y == c.y - 1 && p1.Coordinates.x == c.x)
                        {
                            x = false;
                        }
                    }
                    foreach (Piece p1 in bp)
                    {
                        if (p1.Coordinates.y == c.y - 1 && p1.Coordinates.x == c.x)
                        {
                            x = false;
                        }
                    }
                    if (x == true)
                    {
                        Coordinates move = new Coordinates(c.x, c.y - 1);
                        movess.Add(move);
                        if (c.y == 6)
                        {
                            bool marsh = true;
                            foreach (Piece p1 in bp)
                            {
                                if (p1.Coordinates.y == c.y - 2 && p1.Coordinates.x == c.x)
                                {
                                    marsh = false;
                                }
                            }
                            foreach (Piece p1 in wp)
                            {
                                if (p1.Coordinates.y == c.y - 2 && p1.Coordinates.x == c.x)
                                {
                                    marsh = false;
                                }
                            }
                            if (marsh == true)
                            {
                                movess.Add(new Coordinates(c.x, c.y - 2));
                            }
                        }
                    }
                    foreach (Piece p1 in bp)
                    {
                        if (p1.Coordinates.y == c.y - 1 && p1.Coordinates.x == c.x - 1)
                        {
                            y = true;
                        }
                        else if (p1.Coordinates.y == c.y - 1 && p1.Coordinates.x == c.x + 1)
                        {
                            z = true;
                        }
                    }
                    if (y == true)
                    {
                        Coordinates move = new Coordinates(c.x - 1, c.y - 1);
                        movess.Add(move);
                    }
                    if (z == true)
                    {
                        Coordinates move = new Coordinates(c.x + 1, c.y - 1);
                        movess.Add(move);
                    }

                    return movess;
                }
                else
                {
                    bool x = true;
                    bool y = false;
                    bool z = false;
                    List<Coordinates> movess = new List<Coordinates>();

                    foreach (Piece p1 in bp)
                    {
                        if (p1.Coordinates.y == c.y + 1 && p1.Coordinates.x == c.x)
                        {
                            x = false;
                        }
                    }
                    foreach (Piece p1 in wp)
                    {
                        if (p1.Coordinates.y == c.y + 1 && p1.Coordinates.x == c.x)
                        {
                            x = false;
                        }
                    }
                    if (x == true)
                    {
                        Coordinates move = new Coordinates(c.x, c.y + 1);
                        movess.Add(move);
                        if (c.y == 1)
                        {
                            bool marsh = true;
                            foreach (Piece p1 in bp)
                            {
                                if (p1.Coordinates.y == c.y + 2 && p1.Coordinates.x == c.x)
                                {
                                    marsh = false;
                                }
                            }
                            foreach (Piece p1 in wp)
                            {
                                if (p1.Coordinates.y == c.y + 2 && p1.Coordinates.x == c.x)
                                {
                                    marsh = false;
                                }
                            }
                            if (marsh == true)
                            {
                                movess.Add(new Coordinates(c.x, c.y + 2));
                            }
                        }
                    }
                    foreach (Piece p1 in wp)
                    {
                        if (p1.Coordinates.y == c.y + 1 && p1.Coordinates.x == c.x - 1)
                        {
                            y = true;
                        }
                        else if (p1.Coordinates.y == c.y + 1 && p1.Coordinates.x == c.x + 1)
                        {
                            z = true;
                        }
                    }
                    if (y == true)
                    {
                        Coordinates move = new Coordinates(c.x - 1, c.y + 1);
                        movess.Add(move);
                    }
                    if (z == true)
                    {
                        Coordinates move = new Coordinates(c.x + 1, c.y + 1);
                        movess.Add(move);
                    }
                    return movess;
                }
            }
            return moves;
        }







        public List<Coordinates> knight(Coordinates c, List<Piece> wp, List<Piece> bp, int color)
        {
            if(color == 0)
            {
                List<Coordinates> moves = new List<Coordinates>();
                List<Coordinates> leftmoves = lmoves();
                List<Coordinates> rightmoves = rmoves();
                List<Coordinates> upmoves = umoves();
                List<Coordinates> downmoves = dmoves();
                foreach (Coordinates lcoordinate in leftmoves)
                {
                    moves.Add(lcoordinate);
                }
                foreach (Coordinates rcoordinate in rightmoves)
                {
                    moves.Add(rcoordinate);
                }
                foreach (Coordinates ucoordinate in upmoves)
                {
                    moves.Add(ucoordinate);
                }
                foreach (Coordinates dcoordinate in downmoves)
                {
                    moves.Add(dcoordinate);
                }
                List<Coordinates> lmoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool up = true;
                    bool down = true;
                    if (c.x - 2 >= 0)
                    {
                        foreach (Piece piece in wp)
                        {
                            if (c.x - 2 == piece.Coordinates.x && c.y - 1 == piece.Coordinates.y || c.y - 1 < 0)
                            {
                                up = false;
                            }
                            if (c.x - 2 == piece.Coordinates.x && c.y + 1 == piece.Coordinates.y || c.y + 1 > 7)
                            {
                                down = false;
                            }
                        }
                        if (up == true)
                        {
                            Coordinates upmove = new Coordinates(c.x - 2, c.y - 1);
                            moves.Add(upmove);
                        }
                        if (down == true)
                        {
                            Coordinates downmove = new Coordinates(c.x - 2, c.y + 1);
                            moves.Add(downmove);
                        }
                    }
                    return moves;
                }
                List<Coordinates> rmoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool up = true;
                    bool down = true;
                    if (c.x + 2 <= 7)
                    {
                        foreach (Piece piece in wp)
                        {
                            if (c.x + 2 == piece.Coordinates.x && c.y - 1 == piece.Coordinates.y || c.y - 1 < 0)
                            {
                                up = false;
                            }
                            if (c.x + 2 == piece.Coordinates.x && c.y + 1 == piece.Coordinates.y || c.y + 1 > 7)
                            {
                                down = false;
                            }
                        }
                        if (up == true)
                        {
                            Coordinates upmove = new Coordinates(c.x + 2, c.y - 1);
                            moves.Add(upmove);
                        }
                        if (down == true)
                        {
                            Coordinates downmove = new Coordinates(c.x + 2, c.y + 1);
                            moves.Add(downmove);
                        }
                    }
                    return moves;
                }

                List<Coordinates> umoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool right = true;
                    bool left = true;
                    if (c.y - 2 >= 0)
                    {
                        foreach (Piece piece in wp)
                        {
                            if (c.y - 2 == piece.Coordinates.y && c.x + 1 == piece.Coordinates.x || c.x + 1 > 7)
                            {
                                right = false;
                            }
                            if (c.y - 2 == piece.Coordinates.y && c.x - 1 == piece.Coordinates.x || c.x - 1 < 0)
                            {
                                left = false;
                            }
                        }
                        if (right == true)
                        {
                            Coordinates rightmove = new Coordinates(c.x + 1, c.y - 2);
                            moves.Add(rightmove);
                        }
                        if (left == true)
                        {
                            Coordinates leftmove = new Coordinates(c.x - 1, c.y - 2);
                            moves.Add(leftmove);
                        }
                    }
                    return moves;
                }

                List<Coordinates> dmoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool right = true;
                    bool left = true;
                    if (c.y + 2 <= 7)
                    {
                        foreach (Piece piece in wp)
                        {
                            if (c.y + 2 == piece.Coordinates.y && c.x + 1 == piece.Coordinates.x || c.x + 1 > 7)
                            {
                                right = false;
                            }
                        }
                        foreach (Piece piece in wp)
                        {
                            if (c.y + 2 == piece.Coordinates.y && c.x - 1 == piece.Coordinates.x || c.x - 1 < 0)
                            {
                                left = false;
                            }
                        }
                        if (right == true)
                        {
                            Coordinates rightmove = new Coordinates(c.x + 1, c.y + 2);
                            moves.Add(rightmove);
                        }
                        if (left == true)
                        {
                            Coordinates leftmove = new Coordinates(c.x - 1, c.y + 2);
                            moves.Add(leftmove);
                        }
                    }
                    return moves;
                }
                return moves;
            }
            else
            {
                List<Coordinates> moves = new List<Coordinates>();
                List<Coordinates> leftmoves = lmoves();
                List<Coordinates> rightmoves = rmoves();
                List<Coordinates> upmoves = umoves();
                List<Coordinates> downmoves = dmoves();
                foreach (Coordinates lcoordinate in leftmoves)
                {
                    moves.Add(lcoordinate);
                }
                foreach (Coordinates rcoordinate in rightmoves)
                {
                    moves.Add(rcoordinate);
                }
                foreach (Coordinates ucoordinate in upmoves)
                {
                    moves.Add(ucoordinate);
                }
                foreach (Coordinates dcoordinate in downmoves)
                {
                    moves.Add(dcoordinate);
                }
                List<Coordinates> lmoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool up = true;
                    bool down = true;
                    if (c.x - 2 >= 0)
                    {
                        foreach (Piece piece in bp)
                        {
                            if (c.x - 2 == piece.Coordinates.x && c.y - 1 == piece.Coordinates.y || c.y - 1 < 0)
                            {
                                up = false;
                            }
                            if (c.x - 2 == piece.Coordinates.x && c.y + 1 == piece.Coordinates.y || c.y + 1 > 7)
                            {
                                down = false;
                            }
                        }
                        if (up == true)
                        {
                            Coordinates upmove = new Coordinates(c.x - 2, c.y - 1);
                            moves.Add(upmove);
                        }
                        if (down == true)
                        {
                            Coordinates downmove = new Coordinates(c.x - 2, c.y + 1);
                            moves.Add(downmove);
                        }
                    }
                    return moves;
                }

                List<Coordinates> rmoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool up = true;
                    bool down = true;
                    if (c.x + 2 <= 7)
                    {
                        foreach (Piece piece in bp)
                        {
                            if (c.x + 2 == piece.Coordinates.x && c.y - 1 == piece.Coordinates.y || c.y - 1 < 0)
                            {
                                up = false;
                            }
                            if (c.x + 2 == piece.Coordinates.x && c.y + 1 == piece.Coordinates.y || c.y + 1 > 7)
                            {
                                down = false;
                            }
                        }
                        if (up == true)
                        {
                            Coordinates upmove = new Coordinates(c.x + 2, c.y - 1);
                            moves.Add(upmove);
                        }
                        if (down == true)
                        {
                            Coordinates downmove = new Coordinates(c.x + 2, c.y + 1);
                            moves.Add(downmove);
                        }
                    }
                    return moves;
                }

                List<Coordinates> umoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool right = true;
                    bool left = true;
                    if (c.y - 2 >= 0)
                    {
                        foreach (Piece piece in bp)
                        {
                            if (c.y - 2 == piece.Coordinates.y && c.x + 1 == piece.Coordinates.x || c.x + 1 > 7)
                            {
                                right = false;
                            }
                            if (c.y - 2 == piece.Coordinates.y && c.x - 1 == piece.Coordinates.x || c.x - 1 < 0)
                            {
                                left = false;
                            }
                        }
                        if (right == true)
                        {
                            Coordinates rightmove = new Coordinates(c.x + 1, c.y - 2);
                            moves.Add(rightmove);
                        }
                        if (left == true)
                        {
                            Coordinates leftmove = new Coordinates(c.x - 1, c.y - 2);
                            moves.Add(leftmove);
                        }
                    }
                    return moves;
                }

                List<Coordinates> dmoves()
                {
                    List<Coordinates> moves = new List<Coordinates>();
                    bool right = true;
                    bool left = true;
                    if (c.y + 2 <= 7)
                    {
                        foreach (Piece piece in bp)
                        {
                            if (c.y + 2 == piece.Coordinates.y && c.x + 1 == piece.Coordinates.x || c.x + 1 > 7)
                            {
                                right = false;
                            }
                            if (c.y + 2 == piece.Coordinates.y && c.x - 1 == piece.Coordinates.x || c.x - 1 < 0)
                            {
                                left = false;
                            }
                        }
                        if (right == true)
                        {
                            Coordinates rightmove = new Coordinates(c.x + 1, c.y + 2);
                            moves.Add(rightmove);
                        }
                        if (left == true)
                        {
                            Coordinates leftmove = new Coordinates(c.x - 1, c.y + 2);
                            moves.Add(leftmove);
                        }
                    }
                    return moves;
                }
                return moves;
            }
            return null;
        }


        




        public List<Coordinates> bishop(Coordinates c, List<Piece> wp, List<Piece> bp, int color)
        {
            List<Coordinates> moves = legalmoves(color);
            List<Coordinates> legalmoves(int colorr)
            {
                bool rd = true;
                bool ru = true;
                bool ld = true;
                bool lu = true;
                List<Coordinates> movess = new List<Coordinates>();
                for (int j =  0 ; j < 8; j++)
                {      
                    Coordinates leftdown = new Coordinates(c.x - j, c.y + j);
                    Coordinates rightdown = new Coordinates(c.x + j, c.y + j);
                    Coordinates rightup = new Coordinates(c.x + j, c.y - j);
                    Coordinates leftup = new Coordinates(c.x - j, c.y - j);
              
                    if (rightdown.x >= 0 && rightdown.x <= 7 && rightdown.y >= 0 && rightdown.y <= 7 && rd == true && rightdown.x != c.x && rightdown.y != c.y)
                    {
                        foreach(Piece piece in wp)
                        {
                            switch (color)
                            {
                               case 0:
                                    if (rightdown.x == piece.Coordinates.x && rightdown.y == piece.Coordinates.y)
                                    {
                                        rd = false;
                                    }
                                    break;
                               case 1:
                                    if(rightdown.x == piece.Coordinates.x + 1 && rightdown.y == piece.Coordinates.y + 1)
                                    {
                                        rd = false;
                                    }
                                    break;
                            }                         
                        }
                        foreach(Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (rightdown.x == piece1.Coordinates.x && rightdown.y == piece1.Coordinates.y)
                                    {
                                        rd = false;
                                    }
                                    break;
                                case 0:
                                    if (rightdown.x == piece1.Coordinates.x + 1 && rightdown.y == piece1.Coordinates.y + 1)
                                    {
                                        rd = false;
                                    }
                                    break;
                            }
                        }
                        if(rd != false)
                        {
                            movess.Add(rightdown);
                        }
                    }
                    if (rightup.x >= 0 && rightup.x <= 7 && rightup.y >= 0 && rightup.y <= 7 && ru == true && rightup.x != c.x && rightup.y != c.y)
                    {
                        foreach (Piece piece in wp)
                        {
                            switch (color)
                            {
                                case 0:
                                    if (rightup.x == piece.Coordinates.x && rightup.y == piece.Coordinates.y)
                                    {
                                        ru = false;
                                    }
                                    break;
                                case 1:
                                    if (rightup.x == piece.Coordinates.x + 1 && rightup.y == piece.Coordinates.y - 1)
                                    {
                                        ru = false;
                                    }
                                    break;
                            }   
                        }
                        foreach (Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (rightup.x == piece1.Coordinates.x && rightup.y == piece1.Coordinates.y)
                                    {
                                        ru = false;
                                    }
                                    break;
                                case 0:
                                    if (rightup.x == piece1.Coordinates.x + 1 && rightup.y == piece1.Coordinates.y - 1)
                                    {
                                        ru = false;
                                    }
                                    break;
                            }
                        }
                        if(ru != false)
                        {
                            movess.Add(rightup);
                        }
                    }
                    if (leftup.x >= 0 && leftup.x <= 7 && leftup.y >= 0 && leftup.y <= 7 && lu == true && leftup.x != c.x && leftup.y != c.y)
                    {
                        foreach (Piece piece in wp)
                        {
                            switch (color)
                            {
                                case 0:
                                    if (leftup.x == piece.Coordinates.x && leftup.y == piece.Coordinates.y)
                                    {
                                        lu = false;
                                    }
                                    break;
                                case 1:
                                    {
                                        if (leftup.x == piece.Coordinates.x - 1 && leftup.y == piece.Coordinates.y - 1)
                                        {
                                            lu = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        foreach (Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (leftup.x == piece1.Coordinates.x && leftup.y == piece1.Coordinates.y)
                                    {
                                        lu = false;
                                    }
                                    break;
                                case 0:
                                    {
                                        if (leftup.x == piece1.Coordinates.x - 1 && leftup.y == piece1.Coordinates.y - 1)
                                        {
                                            lu = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        if(lu != false)
                        {
                            movess.Add(leftup);
                        }
                    }
                    if (leftdown.x >= 0 && leftdown.x <= 7 && leftdown.y >= 0 && leftdown.y <= 7 && ld == true && leftdown.x != c.x && leftdown.y != c.y)
                    {
                        foreach (Piece piece in wp)
                        {
                            switch (color)
                            {
                                case 0:
                                    if (leftdown.x == piece.Coordinates.x && leftdown.y == piece.Coordinates.y)
                                    {
                                        ld = false;
                                    }
                                    break;
                                case 1:
                                    if (leftdown.x == piece.Coordinates.x - 1 && leftdown.y == piece.Coordinates.y + 1)
                                    {
                                        ld = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (leftdown.x == piece1.Coordinates.x && leftdown.y == piece1.Coordinates.y)
                                    {
                                        ld = false;
                                    }
                                    break;
                                case 0:
                                    if (leftdown.x == piece1.Coordinates.x - 1 && leftdown.y == piece1.Coordinates.y + 1)
                                    {
                                        ld = false;
                                    }
                                    break;
                            }
                        }
                        if (ld != false)
                        {
                            movess.Add(leftdown);
                        }
                    }
                }
                return movess;
            }
            return moves;
        }
        public List<Coordinates> rook(Coordinates c, List<Piece> wp, List<Piece> bp, int color)
        {
            List<Coordinates> moves = legalmoves(color);
            List<Coordinates> legalmoves(int colorr)
            {
                List<Coordinates> movess = new List<Coordinates>();
                bool u = true;
                bool d = true;
                bool l = true;
                bool r = true;
                for(int i = 0; i < 8;i++)
                {
                    Coordinates up = new Coordinates(c.x, c.y - i);
                    Coordinates down = new Coordinates(c.x, c.y + i);
                    Coordinates left = new Coordinates(c.x - i, c.y);
                    Coordinates right = new Coordinates(c.x + i, c.y);
                    if( up.y >= 0 && u == true && up.y != c.y) 
                    {
                        foreach(Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                   if(wpc.Coordinates.x == up.x && wpc.Coordinates.y == up.y)
                                    {
                                        u = false;
                                    }
                                   break;
                                case 1:
                                    if(wpc.Coordinates.x == up.x && wpc.Coordinates.y - 1 == up.y)
                                    {
                                        u = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == up.x && bpc.Coordinates.y == up.y)
                                    {
                                        u = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x == up.x && bpc.Coordinates.y - 1 == up.y)
                                    {
                                        u = false;
                                    }
                                    break;
                            }
                        }
                        if(u != false)
                        {
                            movess.Add(up);
                        }             
                    }
                    if(d == true && down.y <= 7 && down.y != c.y)
                    {
                        foreach (Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                    if (wpc.Coordinates.x == down.x && wpc.Coordinates.y == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                                case 1:
                                    if (wpc.Coordinates.x == down.x && wpc.Coordinates.y + 1 == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == down.x && bpc.Coordinates.y == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x == down.x && bpc.Coordinates.y + 1 == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                            }
                        }
                       if(d != false)
                        {
                            movess.Add(down);
                        }                
                    }
                    if(l == true && left.x >= 0 && left.x != c.x)
                    {
                        foreach (Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                    if (wpc.Coordinates.x == left.x && wpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                                case 1:
                                    if (wpc.Coordinates.x - 1 == left.x && wpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == left.x && bpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x - 1 == left.x && bpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                            }
                        }
                    if(l != false)
                        {
                            movess.Add(left);
                        }                
                   }
                    if (r == true && right.x <= 7 && right.x != c.x)
                   {
                        foreach (Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                    if (wpc.Coordinates.x == right.x && wpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                                case 1:
                                    if (wpc.Coordinates.x + 1 == right.x && wpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == right.x && bpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x + 1 == right.x && bpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                            }
                        }
                        if(r != false)
                        {
                            movess.Add(right);
                        }         
                    }
                }
                return movess;
            }
            return moves;
        }
        public List<Coordinates> queen(Coordinates c, List<Piece> wp, List<Piece> bp, int color)
        {
            List<Coordinates> moves = legalmovesperpendicular(color);
            List<Coordinates> diagonalmoves = legalmovesdiagonal(color);
            foreach(Coordinates dc in diagonalmoves)
            {
                moves.Add(dc);
            }
            List<Coordinates> legalmovesdiagonal(int colorr)
            {
                bool rd = true;
                bool ru = true;
                bool ld = true;
                bool lu = true;
                List<Coordinates> movess = new List<Coordinates>();
                for (int j = 0; j < 8; j++)
                {
                    Coordinates leftdown = new Coordinates(c.x - j, c.y + j);
                    Coordinates rightdown = new Coordinates(c.x + j, c.y + j);
                    Coordinates rightup = new Coordinates(c.x + j, c.y - j);
                    Coordinates leftup = new Coordinates(c.x - j, c.y - j);

                    if (rightdown.x >= 0 && rightdown.x <= 7 && rightdown.y >= 0 && rightdown.y <= 7 && rd == true && rightdown.x != c.x && rightdown.y != c.y)
                    {
                        foreach (Piece piece in wp)
                        {
                            switch (color)
                            {
                                case 0:
                                    if (rightdown.x == piece.Coordinates.x && rightdown.y == piece.Coordinates.y)
                                    {
                                        rd = false;
                                    }
                                    break;
                                case 1:
                                    if (rightdown.x == piece.Coordinates.x + 1 && rightdown.y == piece.Coordinates.y + 1)
                                    {
                                        rd = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (rightdown.x == piece1.Coordinates.x && rightdown.y == piece1.Coordinates.y)
                                    {
                                        rd = false;
                                    }
                                    break;
                                case 0:
                                    if (rightdown.x == piece1.Coordinates.x + 1 && rightdown.y == piece1.Coordinates.y + 1)
                                    {
                                        rd = false;
                                    }
                                    break;
                            }
                        }
                        if (rd != false)
                        {
                            movess.Add(rightdown);
                        }
                    }
                    if (rightup.x >= 0 && rightup.x <= 7 && rightup.y >= 0 && rightup.y <= 7 && ru == true && rightup.x != c.x && rightup.y != c.y)
                    {
                        foreach (Piece piece in wp)
                        {
                            switch (color)
                            {
                                case 0:
                                    if (rightup.x == piece.Coordinates.x && rightup.y == piece.Coordinates.y)
                                    {
                                        ru = false;
                                    }
                                    break;
                                case 1:
                                    if (rightup.x == piece.Coordinates.x + 1 && rightup.y == piece.Coordinates.y - 1)
                                    {
                                        ru = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (rightup.x == piece1.Coordinates.x && rightup.y == piece1.Coordinates.y)
                                    {
                                        ru = false;
                                    }
                                    break;
                                case 0:
                                    if (rightup.x == piece1.Coordinates.x + 1 && rightup.y == piece1.Coordinates.y - 1)
                                    {
                                        ru = false;
                                    }
                                    break;
                            }
                        }
                        if (ru != false)
                        {
                            movess.Add(rightup);
                        }
                    }
                    if (leftup.x >= 0 && leftup.x <= 7 && leftup.y >= 0 && leftup.y <= 7 && lu == true && leftup.x != c.x && leftup.y != c.y)
                    {
                        foreach (Piece piece in wp)
                        {
                            switch (color)
                            {
                                case 0:
                                    if (leftup.x == piece.Coordinates.x && leftup.y == piece.Coordinates.y)
                                    {
                                        lu = false;
                                    }
                                    break;
                                case 1:
                                    {
                                        if (leftup.x == piece.Coordinates.x - 1 && leftup.y == piece.Coordinates.y - 1)
                                        {
                                            lu = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        foreach (Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (leftup.x == piece1.Coordinates.x && leftup.y == piece1.Coordinates.y)
                                    {
                                        lu = false;
                                    }
                                    break;
                                case 0:
                                    {
                                        if (leftup.x == piece1.Coordinates.x - 1 && leftup.y == piece1.Coordinates.y - 1)
                                        {
                                            lu = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        if (lu != false)
                        {
                            movess.Add(leftup);
                        }
                    }
                    if (leftdown.x >= 0 && leftdown.x <= 7 && leftdown.y >= 0 && leftdown.y <= 7 && ld == true && leftdown.x != c.x && leftdown.y != c.y)
                    {
                        foreach (Piece piece in wp)
                        {
                            switch (color)
                            {
                                case 0:
                                    if (leftdown.x == piece.Coordinates.x && leftdown.y == piece.Coordinates.y)
                                    {
                                        ld = false;
                                    }
                                    break;
                                case 1:
                                    if (leftdown.x == piece.Coordinates.x - 1 && leftdown.y == piece.Coordinates.y + 1)
                                    {
                                        ld = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece piece1 in bp)
                        {
                            switch (color)
                            {
                                case 1:
                                    if (leftdown.x == piece1.Coordinates.x && leftdown.y == piece1.Coordinates.y)
                                    {
                                        ld = false;
                                    }
                                    break;
                                case 0:
                                    if (leftdown.x == piece1.Coordinates.x - 1 && leftdown.y == piece1.Coordinates.y + 1)
                                    {
                                        ld = false;
                                    }
                                    break;
                            }
                        }
                        if (ld != false)
                        {
                            movess.Add(leftdown);
                        }
                    }
                }
                return movess;
            }
            List<Coordinates> legalmovesperpendicular(int colorr)
            {
                List<Coordinates> movess = new List<Coordinates>();
                bool u = true;
                bool d = true;
                bool l = true;
                bool r = true;
                for (int i = 0; i < 8; i++)
                {
                    Coordinates up = new Coordinates(c.x, c.y - i);
                    Coordinates down = new Coordinates(c.x, c.y + i);
                    Coordinates left = new Coordinates(c.x - i, c.y);
                    Coordinates right = new Coordinates(c.x + i, c.y);
                    if (up.y >= 0 && u == true && up.y != c.y)
                    {
                        foreach (Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                    if (wpc.Coordinates.x == up.x && wpc.Coordinates.y == up.y)
                                    {
                                        u = false;
                                    }
                                    break;
                                case 1:
                                    if (wpc.Coordinates.x == up.x && wpc.Coordinates.y - 1 == up.y)
                                    {
                                        u = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == up.x && bpc.Coordinates.y == up.y)
                                    {
                                        u = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x == up.x && bpc.Coordinates.y - 1 == up.y)
                                    {
                                        u = false;
                                    }
                                    break;
                            }
                        }
                        if (u != false)
                        {
                            movess.Add(up);
                        }
                    }
                    if (d == true && down.y <= 7 && down.y != c.y)
                    {
                        foreach (Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                    if (wpc.Coordinates.x == down.x && wpc.Coordinates.y == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                                case 1:
                                    if (wpc.Coordinates.x == down.x && wpc.Coordinates.y + 1 == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == down.x && bpc.Coordinates.y == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x == down.x && bpc.Coordinates.y + 1 == down.y)
                                    {
                                        d = false;
                                    }
                                    break;
                            }
                        }
                        if (d != false)
                        {
                            movess.Add(down);
                        }
                    }
                    if (l == true && left.x >= 0 && left.x != c.x)
                    {
                        foreach (Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                    if (wpc.Coordinates.x == left.x && wpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                                case 1:
                                    if (wpc.Coordinates.x - 1 == left.x && wpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == left.x && bpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x - 1 == left.x && bpc.Coordinates.y == left.y)
                                    {
                                        l = false;
                                    }
                                    break;
                            }
                        }
                        if (l != false)
                        {
                            movess.Add(left);
                        }
                    }
                    if (r == true && right.x <= 7 && right.x != c.x)
                    {
                        foreach (Piece wpc in wp)
                        {
                            switch (colorr)
                            {
                                case 0:
                                    if (wpc.Coordinates.x == right.x && wpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                                case 1:
                                    if (wpc.Coordinates.x + 1 == right.x && wpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                            }
                        }
                        foreach (Piece bpc in bp)
                        {
                            switch (colorr)
                            {
                                case 1:
                                    if (bpc.Coordinates.x == right.x && bpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                                case 0:
                                    if (bpc.Coordinates.x + 1 == right.x && bpc.Coordinates.y == right.y)
                                    {
                                        r = false;
                                    }
                                    break;
                            }
                        }
                        if (r != false)
                        {
                            movess.Add(right);
                        }
                    }
                }
                return movess;
            }
            return moves;
        }
     
        public List<Coordinates> king(Coordinates c, List<Piece> wp, List<Piece> bp, int color)
        {
            List<Coordinates> moves = new List<Coordinates>();
            List<Coordinates> unorginal = new List<Coordinates>();
            Coordinates up = new Coordinates(c.x, c.y - 1); Coordinates down = new Coordinates(c.x, c.y + 1);
            Coordinates right = new Coordinates(c.x + 1, c.y); Coordinates left = new Coordinates(c.x - 1, c.y);
            Coordinates upright = new Coordinates(c.x + 1, c.y - 1); Coordinates downright = new Coordinates(c.x + 1, c.y + 1);
            Coordinates leftdown = new Coordinates(c.x - 1, c.y + 1); Coordinates leftup = new Coordinates(c.x - 1, c.y - 1);
            bool u=true; bool d=true; bool r=true; bool l = true; bool ur = true; bool dr = true; bool ld = true; bool lu = true; 
            unorginal.Add(up); unorginal.Add(down); unorginal.Add(right); unorginal.Add(left); 
            unorginal.Add(upright); unorginal.Add(downright); unorginal.Add(leftdown); unorginal.Add(leftup);
            foreach(Coordinates move in unorginal)
            {
                if(color == 0)
                {
                    foreach (Piece piece in wp)
                    {
                        if (piece.Coordinates.x == move.x && piece.Coordinates.y == move.y)
                        {
                            if (c.x == move.x && c.y - 1 == move.y)
                            {
                                u = false;
                            }
                            else if (c.x == move.x && c.y + 1 == move.y)
                            {
                                d = false;
                            }
                            else if (c.x - 1 == move.x && c.y == move.y)
                            {
                                l = false;
                            }
                            else if (c.x + 1 == move.x && c.y == move.y)
                            {
                                r = false;
                            }
                            else if (c.x + 1 == move.x && c.y + 1 == move.y)
                            {
                                dr = false;
                            }
                            else if (c.x - 1 == move.x && c.y + 1 == move.y)
                            {
                                ld = false;
                            }
                            else if (c.x - 1 == move.x && c.y - 1 == move.y)
                            {
                                lu = false;
                            }
                            else if (c.x + 1 == move.x && c.y - 1 == move.y)
                            {
                                ur = false;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Piece piece in bp)
                    {
                        if (piece.Coordinates.x == move.x && piece.Coordinates.y == move.y)
                        {
                            if (c.x == move.x && c.y - 1 == move.y)
                            {
                                u = false;
                            }
                            else if (c.x == move.x && c.y + 1 == move.y)
                            {
                                d = false;
                            }
                            else if (c.x - 1 == move.x && c.y == move.y)
                            {
                                l = false;
                            }
                            else if (c.x + 1 == move.x && c.y == move.y)
                            {
                                r = false;
                            }
                            else if (c.x + 1 == move.x && c.y + 1 == move.y)
                            {
                                dr = false;
                            }
                            else if (c.x - 1 == move.x && c.y + 1 == move.y)
                            {
                                ld = false;
                            }
                            else if (c.x - 1 == move.x && c.y - 1 == move.y)
                            {
                                lu = false;
                            }
                            else if (c.x + 1 == move.x && c.y - 1 == move.y)
                            {
                                ur = false;
                            }
                        }
                    }
                }
            }
            if (u == true)
            {
                if (up.x < 0 || up.y < 0 || up.x > 7 || up.y > 7)
                {
                }
                else
                {
                    moves.Add(up);
                }
            }
            if (d == true)
            {
                if (down.x < 0 || down.y < 0 || down.x > 7 || down.y > 7)
                {
                }
                else
                {
                    moves.Add(down);
                }
            }
            if (l == true)
            {
                if (left.x < 0 || left.y < 0 || left.x > 7 || left.y > 7)
                {
                }
                else
                {
                    moves.Add(left);
                }
            }
            if(r == true)
            {
                if (right.x < 0 || right.y < 0 || right.x > 7 || right.y > 7)
                {
                }
                else
                {
                    moves.Add(right);
                }
            }
            if (dr == true)
            {
                if (downright.x < 0 || downright.y < 0 || downright.x > 7 || downright.y > 7)
                {
                }
                else
                {
                    moves.Add(downright);
                }
            }
            if (ur == true)
            {
                if (upright.x < 0 || upright.y < 0 || upright.x > 7 || upright.y > 7)
                {
                }
                else
                {
                    moves.Add(upright);
                }
            }
            if (ld == true)
            {
                if (leftdown.x < 0 || leftdown.y < 0 || leftdown.x > 7 || leftdown.y > 7)
                {
                }
                else
                {
                    moves.Add(leftdown);
                }
            }
            if (lu == true)
            {
                if (leftup.x < 0 || leftup.y < 0 || leftup.x > 7 || leftup.y > 7)
                {
                }
                else
                {
                    moves.Add(leftup);
                }
            }
            return moves;
        }

      
    }
}
