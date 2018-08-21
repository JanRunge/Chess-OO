using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class Pawn : Figure
    {
        private int startingRank;
        private int enPassentRank;
        private int enPassentEnemy;
        private int promotionRank;
        private int[] allowedDirection;
        List<int[]> moves = new List<int[]>();
        public Pawn(String color)
        {
            this.color = color;
            name = "pawn";

        
            if (color == "white")
            {
                allowedDirection = new int[] { 1, 2 };
                startingRank = 1;
                enPassentRank = 5;  //auf welchen squre wir gehen, wenn wir enpassen nutzen
                enPassentEnemy = 4; //wo der gegenerische bauer steht, wenn en passent erlabt ist
                promotionRank = 7;
                moves.Add(new int[] { -1, 1 });
                moves.Add(new int[] { 1, 1 });
                moves.Add(new int[] { 0, 1 });
                moves.Add(new int[] { 0, 2 });
            }
            else
            {
                allowedDirection = new int[] { -1, -2 };
                startingRank = 6;
                enPassentRank = 2;
                enPassentEnemy = 3;
                promotionRank = 0;
                moves.Add(new int[] { -1, -1 });
                moves.Add(new int[] { 1, -1 });
                moves.Add(new int[] { 0, -1 });
                moves.Add(new int[] { 0, -2 });
            }

        }
        public override ArrayList placesICanGo(int x, int y, Move mymove)
        {
            ArrayList ret = new ArrayList();
            for (int i = 0; i < moves.Count; i++)
            {
                if (possible(x, y, x + moves[i][0], y + moves[i][1], mymove))
                {
                    ret.Add(new int[] { x + moves[i][0], y + moves[i][1] });
                }

            }
            return ret;
        }
        override public bool  pseudoPossible(int srcx, int srcy, int x, int y, Move mymove) {
            if (base.pseudoPossible(srcx,srcy,x, y,mymove) && Math.Abs(srcx - x) <= 1)
            {
                bool possible = false;
                if (x == srcx && mymove.BoardBefore.c[x, y] == null)//du bleibst in deiner spalte
                {
                   // mygame.movedenial = (String)(this.y + allowedDirection[0] + " ");
                    if (srcy + allowedDirection[0] == y)
                    {
                        possible = true;
                    }
                    else
                    {
                        if (startingRank == srcy && srcy + allowedDirection[1] == y && mymove.BoardBefore.c[x, (srcy + allowedDirection[0])] == null)//du darfst doppelt springen
                        {
                            possible = true;

                        }
                        else
                        {
                            //mygame.movedenial = mygame.movedenial + "no big jump allowed";
                        }
                    }
                }
                else if (enemy_fig_on(x, y,mymove))
                {
                    if (srcy + allowedDirection[0] == y && Math.Abs(srcx - x) == 1)
                    {
                        possible = true;
                    }
                    else
                    {
                        //mygame.movedenial = "trying to hit unallowed square";
                    }
                }
                else if (mymove.previousMove.enPassant_rank == x && y == this.enPassentRank && Math.Abs(srcy - y) <= 1)
                {
                    possible = true;
                }
                else
                {
                    //mygame.movedenial = "rank problem";

                }
                return possible;
            }
            else
            {
                return false;
            }
            
        }
        override public void doMove(int srcx, int srcy, int x, int y, Move mymove)
        {

            if (Math.Abs(y- srcy) ==2)
            {
                mymove.enPassant_rank = srcx;
            }else if(x!=srcx && mymove.BoardBefore.c[x, y] == null)//du hast ein en passent ausgeführt
            {
                mymove.BoardAfter.c[x, enPassentEnemy] = null;
            }

            if (y == promotionRank)
            {
                mymove.promotion = true;
                mymove.promotionX = x;
                mymove.promotionY = promotionRank;
            }
            base.doMove(srcx, srcy,x,y, mymove);
        }
       
    }
}
