using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WindowsFormsApp3
{
    public class Figure
    {
        public String color;
        public String name;
        public bool tryMove(int srcx, int srcy, int x, int y,Move mymove)
        {
            if(mymove == null)
            {
                throw new System.ArgumentException("tyMove got a null move", "original");
            }
            
            bool ret = false;
            if (possible(srcx, srcy, x, y,mymove))//und da ist keine einheit usw
            {
                doMove(srcx, srcy, x, y,mymove);
                ret = true;

            }
            return ret;
        }
        public virtual ArrayList placesICanGo(int srcx, int srcy,Move mymove)
        {
            ArrayList ret = new ArrayList();

            for (int k = 0; k < 8; k++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (possible(srcx, srcy, c, k,mymove))
                    {
                        ret.Add(new int[] {c, k});
                    }
                    
                }
            }
            return ret;
        }
        public virtual bool pseudoPossible(int srcx, int sry, int x, int y, Move mymove)
        {//die untergeordneten Klassen müssen das hier überschreiben.
            return x < 8 && x > -1 && y < 8 && y > -1 && !allied_fig_on(x, y,mymove);
        }
        public bool possible(int srcx, int sry, int x, int y, Move mymove) {
            if (mymove.turn == this.color && pseudoPossible(srcx, sry, x, y, mymove))
            {


                //boardtoconsole(mymove.BoardBefore.c);//##############
                doMove(srcx, sry, x, y, mymove);
                //boardtoconsole(mymove.BoardBefore.c);//##############
                Move newerMove = new Move(mymove);
                bool ret = !inCheck(newerMove);
                mymove.reset();
                //boardtoconsole(mymove.BoardBefore.c);//##############
                return ret;
            }
            else
            {
                return false;
            }
        }    
        public void boardtoconsole(Figure[,] board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[j, i] != null)
                    {
                        Console.Write(string.Format("{0} ", board[j, i].color + "_" + board[j, i].name + j + "|" + i));
                    }
                    else
                    {
                        Console.Write(string.Format("{0} ", "empty"));
                    }

                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write("ARRAY END");
            Console.ReadLine();
        }
        public bool allied_fig_on(int x, int y, Move mymove)
        {
            if (mymove.BoardBefore.c[(x), (y)] == null)
            {
                return false;
            }
            return mymove.BoardBefore.c[(x), (y)].color == this.color;
        }
        virtual public void doMove(int srcx, int srcy, int destx, int desty, Move mymove)
        {
            if (mymove == null || mymove.BoardAfter == null)
            {
                throw new System.ArgumentException("tyMove got a null move", "original");
            }
            mymove.BoardAfter.c[srcx, srcy] = null;
            mymove.BoardAfter.c[destx, desty] = this;
        }
        public bool enemy_fig_on(int x, int y, Move mymove)
        {
            if (mymove.BoardBefore.c[(x), (y)] == null)
            {
                return false;
            }
            return mymove.BoardBefore.c[(x), (y)].color != this.color;
        }
        protected bool isDiagonal(int srcx, int srcy, int destx, int desty)
        {
            return Math.Abs(srcx - destx) == Math.Abs(srcy - desty);
        }
        protected bool isVertHor(int srcx, int srcy, int destx, int desty)//ob das ziel sich in einer geraden linie befindet, dh im sinne des turms erreichbar is
        {
            return (srcx == destx && srcy != desty) || (srcy == desty && srcx != destx);
        }
        protected bool isWayFreeDiag(int srcx, int srcy, int destx, int desty, Move mymove)
        {
            int i = 1;
            int squaresToCheck = Math.Abs(srcx - destx) - 1;//der letzte square darf ein gegner sein, dh wir überprüfen nur alle vorher
            bool possible = true;
            if (destx > srcx)
            {
                if (desty > srcy)
                {
                    while (i <= squaresToCheck)
                    {
                        if (mymove.BoardBefore.c[(srcx + i),(srcy + i)] != null)
                        {
                            possible = false;
                            break;
                        }
                        i++;
                    }
                }
                else//es gilt: y<this.y
                {
                    while (i <= squaresToCheck)
                    {
                        if (mymove.BoardBefore.c[(srcx + i),(srcy - i)] != null)
                        {
                            possible = false;
                            break;
                        }
                        i++;
                    }
                }
            }
            else//es gilt: x<this.x
            {
                if (desty > srcy)
                {
                    while (i <= squaresToCheck)
                    {
                        if (mymove.BoardBefore.c[(srcx - i), (srcy + i)] != null)
                        {
                            possible = false;
                            break;
                        }
                        i++;
                    }
                }
                else//es gilt: y<this.y
                {
                    while (i <= squaresToCheck)
                    {
                        if (mymove.BoardBefore.c[(srcx - i),(srcy - i)] != null)
                        {
                            possible = false;
                            break;
                        }
                        i++;
                    }
                }
            }
            return possible;
        }
        protected bool isWayFreeVertHor(int srcx, int srcy, int destx, int desty, Move mymove)
        {
            int i = 1;
            bool possible = true;
            int squaresToCheck = Math.Max((Math.Abs(srcx - destx) - 1),(Math.Abs(srcy - desty) - 1));
            if (destx > srcx)
            {
                while (i <= squaresToCheck)
                {
                    if (mymove.BoardBefore.c[(srcx + i),(srcy)] != null)
                    {
                        possible = false;
                        break;
                    }
                    i++;
                }
            }
            else if (destx < srcx)
            {
                while (i <= squaresToCheck)
                {
                    if (mymove.BoardBefore.c[(srcx - i),(srcy)] != null)
                    {
                        possible = false;
                        break;
                    }
                    i++;
                }
            }
            else if (desty > srcy)
            {
                while (i <= squaresToCheck)
                {
                    if (mymove.BoardBefore.c[(srcx),(srcy + i)] != null)
                    {
                        possible = false;
                        break;
                    }
                    i++;
                }
            }
            else if (desty < srcy)
            {
                while (i <= squaresToCheck)
                {
                    if (mymove.BoardBefore.c[(srcx),(srcy - i)] != null)
                    {
                        possible = false;
                        break;
                    }
                    i++;
                }
            }
            return possible;
        }
        public bool inCheck(Move mv)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (mv.BoardBefore.c[(i), (c)] != null && mv.BoardBefore.c[(i), (c)].pseudoPossible(i, c, mv.enemyKing[0], mv.enemyKing[1], mv))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        

    }
}
