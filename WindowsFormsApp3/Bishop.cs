using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Bishop : Figure
    {
        public Bishop(String color)
        {
            this.color = color;
            name = "bishop";

        }
        public override ArrayList placesICanGo(int x, int y, Move mymove)
        {
            ArrayList ret = new ArrayList();
            if(this.color == mymove.turn)
            {
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
        override public bool pseudoPossible(int srcx, int srcy, int destx, int desty, Move mymove)
        {
            if (base.pseudoPossible(srcx, srcy, destx, desty, mymove))
            {
                return isDiagonal(srcx, srcy, destx, desty) && isWayFreeDiag(srcx, srcy, destx, desty, mymove);
            }
            else
            {
                return false;
            }
                
        }
    }
}
