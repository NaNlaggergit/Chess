using Chess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace Chess
{
    public abstract class Piece
    {
        public int X;
        public int Y;
        public Piece(int x, int y)
        {
            if (x < 0 || x > 7)
                throw new InvalidCoordException("X должны быть в пределах 0 <= x < 8");
            if (y < 0 || y > 7)
                throw new InvalidCoordException("X должны быть в пределах 0 <= y < 8");
            X = x;
            Y = y;
        }
        public bool IsBorder(int x, int y,Board board)
        {
            if (x < 0 || x > 7)
                return true;
            if (y < 0 || y > 7)
                return true;
            if (board.Map[x, y] is ShadowFlame)
                return true;
            return false;
        }
        public abstract List<Point> ListDestroyPiece(Board board);
    }
    public class InvalidCoordException : Exception
    {
        public InvalidCoordException(string message) : base(message)
        {
        }
    }
    public class Board
    {
        public Piece[,] Map = new Piece[8,8];
        public List<Piece> Pieces = new List<Piece>();
        public void AddPiece(string name,int x,int y)
        {
            switch (name)
            {
                case "king":
                    Map[x, y] = new King(x,y);
                    Pieces.Add(Map[x,y]);
                    break;
                case "queen":
                    Map[x, y] = new Queen(x, y);
                    Pieces.Add(Map[x, y]);
                    break;
                case "rook":
                    Map[x, y] = new Rook(x, y);
                    Pieces.Add(Map[x, y]);
                    break;
                case "bishop":
                    Map[x, y] = new Bishop(x, y);
                    Pieces.Add(Map[x, y]);
                    break;
                case "knight":
                    Map[x, y] = new Knight(x, y);
                    Pieces.Add(Map[x, y]);
                    break;
                case "shadow":
                    Map[x, y] = new Shadow(x, y);
                    Pieces.Add(Map[x, y]);
                    break;
                case "shadowflame":
                    Map[x, y] = new ShadowFlame(x, y);
                    Pieces.Add(Map[x, y]);
                    break;
            }
        }
    }
    public class King:Piece
{
        public King(int x,int y) : base(x, y)
        {
        }
        public override List<Point> ListDestroyPiece(Board board)
        {
            var listDestroy=new List<Point>();
            if (!IsBorder(X + 1, Y, board) && board.Map[X+1,Y]!=null)
                listDestroy.Add(new Point(X+1,Y));
            if (!IsBorder(X -1, Y, board) && board.Map[X - 1, Y] != null)
                listDestroy.Add(new Point(X-1 , Y));
            if (!IsBorder(X , Y+1, board) && board.Map[X , Y+1] != null)
                listDestroy.Add(new Point(X, Y+1));
            if (!IsBorder(X , Y-1, board) && board.Map[X , Y-1] != null)
                listDestroy.Add(new Point(X, Y-1));
            if (!IsBorder(X+1 , Y+1, board) && board.Map[X + 1, Y+1] != null)
                listDestroy.Add(new Point(X + 1, Y+1));
            if (!IsBorder(X +1, Y-1, board) && board.Map[X + 1, Y-1] != null)
                listDestroy.Add(new Point(X + 1, Y - 1));
            if (!IsBorder(X -1, Y-1, board) && board.Map[X - 1, Y-1] != null)
                listDestroy.Add(new Point(X - 1, Y-1));
            if (!IsBorder(X -1, Y+1, board) && board.Map[X - 1, Y+1] != null)
                listDestroy.Add(new Point(X - 1, Y+1));
            return listDestroy;
        }
    } 
    public class Queen : Piece
    {
        public Queen(int x, int y) : base(x, y)
        {
        }
        public override List<Point> ListDestroyPiece(Board board)
        {
            var listDestroy = new List<Point>();
            for (int x=X+1; !IsBorder(x,Y, board); x++)
            {
                if(!IsBorder(x, Y, board) && board.Map[x, Y] != null)
                {
                    listDestroy.Add(new Point(x,Y));
                    break;
                }
            }
            for (int x = X - 1; !IsBorder(x, Y, board); x--)
            {
                if (!IsBorder(x, Y, board) && board.Map[x, Y] != null)
                {
                    listDestroy.Add(new Point(x, Y));
                    break;
                }
            }
            for (int y = Y+1; !IsBorder(X, y, board); y++)
            {
                if (!IsBorder(X, y, board) && board.Map[X, y] != null)
                {
                    listDestroy.Add(new Point(X, y));
                    break;
                }
            }
            for (int y = Y - 1; !IsBorder(X, y, board); y--)
            {
                if (!IsBorder(X, y, board) && board.Map[X, y] != null)
                {
                    listDestroy.Add(new Point(X, y));
                    break;
                }
            }
            for (int y = Y +1,x=X+1; !IsBorder(x, y, board); y++,x++)
            {
                if (!IsBorder(x, y, board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            for (int y = Y - 1, x = X + 1; !IsBorder(x, y, board); y--, x++)
            {
                if (!IsBorder(x, y,board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            for (int y = Y - 1, x = X - 1; !IsBorder(x, y, board); y--, x--)
            {
                if (!IsBorder(x, y, board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            for (int y = Y + 1, x = X - 1; !IsBorder(x, y, board); y++, x--)
            {
                if (!IsBorder(x, y, board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            return listDestroy;
        }
    }
    public class Rook : Piece
    {
        public Rook(int x, int y) : base(x, y)
        {
        }
        public override List<Point> ListDestroyPiece(Board board)
        {
            var listDestroy = new List<Point>();
            for (int x = X + 1; !IsBorder(x, Y, board); x++)
            {
                if (!IsBorder(x, Y, board) && board.Map[x, Y] != null)
                {
                    listDestroy.Add(new Point(x, Y));
                    break;
                }
            }
            for (int x = X - 1; !IsBorder(x, Y, board); x--)
            {
                if (!IsBorder(x, Y, board) && board.Map[x, Y] != null)
                {
                    listDestroy.Add(new Point(x, Y));
                    break;
                }
            }
            for (int y = Y + 1; !IsBorder(X, y, board); y++)
            {
                if (!IsBorder(X, y, board) && board.Map[X, y] != null)
                {
                    listDestroy.Add(new Point(X, y));
                    break;
                }
            }
            for (int y = Y - 1; !IsBorder(X, y, board); y--)
            {
                if (!IsBorder(X, y, board) && board.Map[X, y] != null)
                {
                    listDestroy.Add(new Point(X, y));
                    break;
                }
            }
            return listDestroy;
        }
    }
    public class Bishop : Piece
    {
        public Bishop(int x, int y) : base(x, y)
        {
        }
        public override List<Point> ListDestroyPiece(Board board)
        {
            var listDestroy = new List<Point>();
            for (int y = Y + 1, x = X + 1; !IsBorder(x, y, board); y++, x++)
            {
                if (!IsBorder(x, y, board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            for (int y = Y - 1, x = X + 1; !IsBorder(x, y, board); y--, x++)
            {
                if (!IsBorder(x, y, board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            for (int y = Y - 1, x = X - 1; !IsBorder(x, y, board); y--, x--)
            {
                if (!IsBorder(x, y, board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            for (int y = Y + 1, x = X - 1; !IsBorder(x, y, board); y++, x--)
            {
                if (!IsBorder(x, y, board) && board.Map[x, y] != null)
                {
                    listDestroy.Add(new Point(x, y));
                    break;
                }
            }
            return listDestroy;
        }
    }
    public class Knight : Piece
    {
        public Knight(int x, int y) : base(x, y)
        {

        }
        public override List<Point> ListDestroyPiece(Board board)
        {
            var listDestroy=new List<Point>();
            if(!IsBorder(X + 1, Y+2, board) && board.Map[X + 1, Y+2] != null)
                listDestroy.Add(new Point(X+1, Y+2));
            if (!IsBorder(X + 2, Y + 1, board) && board.Map[X + 2, Y + 1] != null)
                listDestroy.Add(new Point(X + 2, Y + 1));
            if (!IsBorder(X + 2, Y -1, board) && board.Map[X + 2, Y -1] != null)
                listDestroy.Add(new Point(X + 2, Y - 1));
            if (!IsBorder(X + 1, Y - 2, board) && board.Map[X + 1, Y - 2] != null)
                listDestroy.Add(new Point(X + 1, Y - 2));
            if (!IsBorder(X - 1, Y - 2, board) && board.Map[X - 1, Y - 2] != null)
                listDestroy.Add(new Point(X - 1, Y - 2));
            if (!IsBorder(X -2 , Y - 1, board) && board.Map[X -2, Y - 1] != null)
                listDestroy.Add(new Point(X - 2, Y - 1));
            if (!IsBorder(X -2, Y +1, board) && board.Map[X -2, Y +1] != null)
                listDestroy.Add(new Point(X -2, Y +1));
            if (!IsBorder(X - 1, Y + 2, board) && board.Map[X -1, Y + 2] != null)
                listDestroy.Add(new Point(X - 1, Y + 2));
            return new List<Point>();
        }
    }
    public class ShadowFlame:Piece
    {
        public ShadowFlame(int x, int y) : base(x, y)
        {

        }

        public override List<Point> ListDestroyPiece(Board board)
        {
            return new List<Point>();
        }

    }
    public class Shadow : Queen
    {
        public Shadow(int x, int y) : base(x, y)
        {

        }
    }

}
