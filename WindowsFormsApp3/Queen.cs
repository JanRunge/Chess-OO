using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Queen : Figure
    {
        public Queen(String color)
        {
            this.color = color;
            name = "queen";

        }
        override public bool pseudoPossible(int srcx, int srcy, int x, int y, Move mymove)
        {
            if (base.pseudoPossible( srcx,  srcy,  x,  y, mymove))
            {
                return (isVertHor(srcx, srcy, x, y) && isWayFreeVertHor(srcx, srcy, x, y,mymove)) || (isDiagonal(srcx, srcy, x, y) && isWayFreeDiag(srcx, srcy, x, y, mymove));
            }
            else
            {
                return false;
            }
                
        }
        public override ArrayList placesICanGo(int x, int y, Move mymove)
        {
            ArrayList ret = new ArrayList();
            if (this.color == mymove.turn)
            {
                for (int i = 1; i < 8; i++)
                {
                    if (this.pseudoPossible(x, y, (x + i), y, mymove))
                    {
                        doMove(x, y, x + i, y, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x + i, y });
                        }
                        mymove.reset();

                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    if (pseudoPossible(x, y, x - i, y, mymove))
                    {
                        doMove(x, y, x - i, y, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x - i, y });
                        }
                        mymove.reset();


                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++)
                {
                    if (pseudoPossible(x, y, x, y + i, mymove))
                    {
                        doMove(x, y, x, y + i, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x, y + i });
                        }
                        mymove.reset();

                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {

                    if (possible(x, y, x, y - i, mymove))
                    {
                        doMove(x, y, x, y - i, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x, y - i });
                        }
                        mymove.reset();
                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    Console.WriteLine("" + i + "" + x + "" + y + "" + (x + i) + "" + (y + i));
                    Console.Read();
                    if (pseudoPossible(x, y, x + i, y + i, mymove))
                    {
                        doMove(x, y, x + i, y + i, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x + i, y + i });
                        }
                        mymove.reset();

                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    if (pseudoPossible(x, y, x + i, y - i, mymove))
                    {
                        doMove(x, y, x + i, y - i, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x + i, y - i });
                        }
                        mymove.reset();

                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = 1; i < 8; i++)
                {
                    if (pseudoPossible(x, y, x - i, y + i, mymove))
                    {
                        doMove(x, y, x - i, y + i, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x - i, y + i });
                        }
                        mymove.reset();

                    }
                    else
                    {
                        break;
                    }
                }
                for (int i = 1; i < 8; i++)
                {
                    if (pseudoPossible(x, y, x - i, y - i, mymove))
                    {
                        doMove(x, y, x - i, y - i, mymove);
                        //boardtoconsole(mymove.BoardBefore.c);//##############
                        Move newerMove = new Move(mymove);
                        if (!inCheck(newerMove))
                        {
                            ret.Add(new int[] { x - i, y - i });
                        }
                        mymove.reset();

                    }
                    else
                    {
                        break;
                    }
                }
            }
            

            return ret;
        
        }
    }
}
