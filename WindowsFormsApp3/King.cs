using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class King : Figure
    {
        List<int[]> moves = new List<int[]>();
        public King(String color)
        {
            this.color = color;
            name = "king";
            moves.Add(new int[] { -1, -1 });
            moves.Add(new int[] { -1, 0 });
            moves.Add(new int[] { -1, 1 });
            moves.Add(new int[] { 0, -1 });
            moves.Add(new int[] { 0, 1 });
            moves.Add(new int[] { 1, -1 });
            moves.Add(new int[] { 1, 0 });
            moves.Add(new int[] { 1, 1 });
            moves.Add(new int[] { -2, 0 });
            moves.Add(new int[] { 2, 0 });
        }
        
        public override ArrayList placesICanGo(int x, int y, Move mymove)
        {
            ArrayList ret = new ArrayList();
            for (int i = 0; i < moves.Count; i++)
            {
                if (possible(x, y, x+moves[i][0], y+moves[i][1], mymove))
                {
                    ret.Add(new int[] { x + moves[i][0], y + moves[i][1] });
                }
                
            }
            return ret;
        }
        override public bool pseudoPossible(int srcx, int srcy, int x, int y, Move mymove)
        {
            bool ret = false;
            if (base.pseudoPossible(srcx, srcy, x, y, mymove))
            {
                if(Math.Abs(srcx - x)<=1 &&  Math.Abs(srcy - y) <= 1)
                {
                    ret = true;
                }
                else if(Math.Abs(srcx - x)==2 && srcy==y)
                {
                    if (color == "white" && !mymove.WKingMoved)
                    {
                            if (srcx < x )//rochade auf der Königsseite
                            {
                                if (!mymove.WRook2moved && mymove.BoardBefore.c[7, 0].possible(7, 0, 5, 0, mymove))//der weg ist frei
                                {

                                    doMove(4, 0, 5, 0, mymove);
                                    //boardtoconsole(mymove.BoardBefore.c);//##############
                                    Move newerMove = new Move(mymove);
                                    for (int i = 0; i < 8; i++)
                                    {
                                        for (int c = 0; c < 8; c++)
                                        {
                                            if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                            {
                                                mymove.reset();
                                                return false;
                                            }
                                        }
                                    }
                                    mymove.reset();
                                    doMove(5, 0, 6, 0, mymove);
                                    //boardtoconsole(mymove.BoardBefore.c);//##############
                                    newerMove = new Move(mymove);
                                    for (int i = 0; i < 8; i++)
                                    {
                                        for (int c = 0; c < 8; c++)
                                        {
                                            if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                            {
                                                mymove.reset();
                                                return false;
                                            }
                                        }
                                    }
                                    mymove.reset();
                                    ret = true;
                                }
                        }
                        else
                        {
                            //Console.Write("0"); Console.ReadLine();
                            if (!mymove.WRook1moved && mymove.BoardBefore.c[0, 0].possible(0, 0, 3, 0, mymove))//der weg ist frei
                            {
                                //Console.Write("1"); Console.ReadLine();
                                doMove(4, 0, 3, 0, mymove);
                                //boardtoconsole(mymove.BoardBefore.c);//##############
                                Move newerMove = new Move(mymove);
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int c = 0; c < 8; c++)
                                    {
                                        if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                        {
                                            Console.Write(i+"x"+c+"y can reach the king at "+ newerMove.enemyKing[0]+"|"+ newerMove.enemyKing[1]); Console.ReadLine();
                                            mymove.reset();
                                            return false;
                                        }
                                    }
                                }
                                //Console.Write("2"); Console.ReadLine();
                                mymove.reset();
                                doMove(3, 0, 2, 0, mymove);
                                //boardtoconsole(mymove.BoardBefore.c);//##############
                                newerMove = new Move(mymove);
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int c = 0; c < 8; c++)
                                    {
                                        if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                        {
                                            Console.Write(i + "x" + c + "y can reach the king at " + newerMove.enemyKing[0] + "|" + newerMove.enemyKing[1]); Console.ReadLine();
                                            mymove.reset();
                                            return false;
                                        }
                                    }
                                }
                                //Console.Write("3"); Console.ReadLine();
                                mymove.reset();
                                ret = true;
                            }
                        }
                        
                    }
                    else if(!mymove.BKingMoved) //schwarz
                    {
                        if (srcx < x)//rochade auf der Königsseite
                        {
                            if (!mymove.BRook2moved && mymove.BoardBefore.c[7, 7].possible(7, 7, 5, 7, mymove))//der weg ist frei
                            {

                                doMove(4, 7, 5, 7, mymove);
                                //boardtoconsole(mymove.BoardBefore.c);//##############
                                Move newerMove = new Move(mymove);
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int c = 0; c < 8; c++)
                                    {
                                        if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                        {
                                            mymove.reset();
                                            return false;
                                        }
                                    }
                                }
                                mymove.reset();
                                doMove(5, 7, 6, 7, mymove);
                                //boardtoconsole(mymove.BoardBefore.c);//##############
                                newerMove = new Move(mymove);
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int c = 0; c < 8; c++)
                                    {
                                        if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                        {
                                            mymove.reset();
                                            return false;
                                        }
                                    }
                                }
                                mymove.reset();
                                ret = true;
                            }
                        }
                        else
                        {
                            if (!mymove.BRook1moved && mymove.BoardBefore.c[0, 7].possible(0, 7, 3, 7, mymove))//der weg ist frei
                            {
                                doMove(4, 7, 3, 7, mymove);
                                //boardtoconsole(mymove.BoardBefore.c);//##############
                                Move newerMove = new Move(mymove);
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int c = 0; c < 8; c++)
                                    {
                                        if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                        {
                                            Console.Write(i + "x" + c + "y can reach the king at " + newerMove.enemyKing[0] + "|" + newerMove.enemyKing[1]); Console.ReadLine();
                                            mymove.reset();
                                            return false;
                                        }
                                    }
                                }
                                Console.Write("2"); Console.ReadLine();
                                mymove.reset();
                                doMove(3, 7, 2, 7, mymove);
                                //boardtoconsole(mymove.BoardBefore.c);//##############
                                newerMove = new Move(mymove);
                                for (int i = 0; i < 8; i++)
                                {
                                    for (int c = 0; c < 8; c++)
                                    {
                                        if (newerMove.BoardBefore.c[(i), (c)] != null && newerMove.BoardBefore.c[(i), (c)].pseudoPossible(i, c, newerMove.enemyKing[0], newerMove.enemyKing[1], newerMove))
                                        {
                                            Console.Write(i + "x" + c + "y can reach the king at " + newerMove.enemyKing[0] + "|" + newerMove.enemyKing[1]); Console.ReadLine();
                                            mymove.reset();
                                            return false;
                                        }
                                    }
                                }
                                Console.Write("3"); Console.ReadLine();
                                mymove.reset();
                                ret = true;
                            }
                        }
                    }
                    
                }
            }
            else
            {
                ret= false;
            }
            return ret;
        }
        
        override public void doMove(int srcx, int srcy, int destx, int desty, Move mymove)
        {
            if (color == "white")
            {
                mymove.WKingMoved = true;
                
            }
            else
            {
                mymove.BKingMoved = true;
            }
            
            if (Math.Abs(srcx - destx) == 2 && srcy == desty)
            {
                rochade(srcx, srcy, destx, desty, mymove, desty);
            }
            base.doMove(srcx, srcy, destx, desty,mymove);
        }
        
        public void rochade(int srcx, int srcy, int destx, int desty, Move mymove, int rank)
        {

            if (srcx > destx)
            {
                mymove.BoardAfter.c[3,rank] = mymove.BoardAfter.c[0, rank];
                mymove.BoardAfter.c[0, rank] =null;
            }
            else
            {
                mymove.BoardAfter.c[5, rank] = mymove.BoardAfter.c[7,rank];
                mymove.BoardAfter.c[7, rank] = null;
            }
            
        }

    }
}
