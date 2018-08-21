using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Rook : Figure
    {
        public Rook(String color)
        {
            this.color = color;
            name = "rook";

        }
        override public bool pseudoPossible(int srcx, int srcy, int x, int y, Move mymove)
        {
            if (base.pseudoPossible(srcx, srcy, x, y, mymove))
            {
                
                return isVertHor(srcx, srcy, x, y) && isWayFreeVertHor(srcx, srcy, x, y,mymove);
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
            }
            
            return ret;
        }
        public override void doMove(int srcx, int srcy, int destx, int desty, Move mymove)
        {
            base.doMove(srcx, srcy, destx, desty, mymove);
            if (srcx == 0)
            {
                if (color == "white")
                {
                    mymove.WRook1moved = true;
                }
                else
                {
                    mymove.BRook1moved = true;
                }
            }else if (srcx == 7)
            {
                if (color == "white")
                {
                    mymove.WRook2moved = true;
                }
                else
                {
                    mymove.BRook2moved = true;
                }
            }
        }
    }
}
