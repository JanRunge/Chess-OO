using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    
    public class Board
    {
        public Figure[,] c;
        
        public Board(Figure[,] board)
        {
            c = board;
        }
        public Board()
        {

        }
        public Board copy()
        {
            return new Board(new Figure[,] { { c[0,0],c[0,1],c[0,2],c[0,3],c[0,4],c[0,5],c[0,6],c[0,7]},    //x=1
                                            { c[1,0],c[1,1],c[1,2],c[1,3],c[1,4],c[1,5],c[1,6],c[1,7]},  //x=2
                                            { c[2,0],c[2,1],c[2,2],c[2,3],c[2,4],c[2,5],c[2,6],c[2,7]},  //x=3
                                            { c[3,0],c[3,1],c[3,2],c[3,3],c[3,4],c[3,5],c[3,6],c[3,7]},   //x=4
                                            { c[4,0],c[4,1],c[4,2],c[4,3],c[4,4],c[4,5],c[4,6],c[4,7]},    //x=5
                                            { c[5,0],c[5,1],c[5,2],c[5,3],c[5,4],c[5,5],c[5,6],c[5,7]},  //x=6
                                            { c[6,0],c[6,1],c[6,2],c[6,3],c[6,4],c[6,5],c[6,6],c[6,7]},  //x=7
                                            { c[7,0],c[7,1],c[7,2],c[7,3],c[7,4],c[7,5],c[7,6],c[7,7]},    //x=8
                                }
                        );
        
        }
    }
}
