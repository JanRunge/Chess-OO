using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class Knight : Figure
    {
        List<int[]> moves= new List<int[]>();
       public Knight( String color)//konstruktor
        {
            this.color = color;
            name = "knight";
            moves.Add(new int[] {-1, 2});
            moves.Add(new int[] { 1, 2});
            moves.Add(new int[] { 2, 1});
            moves.Add(new int[] { 2,-1});
            moves.Add(new int[] { 1,-2});
            moves.Add(new int[] {-1,-2});
            moves.Add(new int[] {-2,-1});
            moves.Add(new int[] {-2, 1});

        }
         public override bool pseudoPossible(int srcx, int srcy, int destx,int desty, Move mymove)
        {
            if (base.pseudoPossible(srcx, srcy, destx, desty, mymove))
            {
                bool possible = false;
                                           
                int Abs0 = Math.Abs(srcx - destx);
                int Abs1 = Math.Abs(srcy - desty);
                if ((Abs0 + Abs1 == 3) && (Abs0 == 1 || Abs0 == 2) && (Abs1 == 1 || Abs1 == 2))
                {
                    possible = true;
                }
                return possible;
            }
            else
            {
                return false;
            }
           
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
            public bool knights_func()
        {
            return false;
        }

    }
}
