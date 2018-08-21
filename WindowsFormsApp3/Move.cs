using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WindowsFormsApp3
{
    public class Move
    {
        public Move             previousMove;
        public Move             nextMove;
        public int              num;
        public Board        BoardAfter = new Board();

        /**Alle Parameter, die für diesen Zug zählen**/
        public Board BoardBefore;
        public int[] playerKing;
        public int[] enemyKing;

        public bool WKingMoved ;
        public bool WRook1moved;
        public bool WRook2moved;
        public bool BKingMoved;
        public bool BRook1moved;
        public bool BRook2moved;

        public bool promotion;
        public int promotionX;
        public int promotionY;


        public String turn;
        public int enPassant_rank = -1;

        public Move(Move lastMove)
        {

            //do the move
            init(lastMove);



        }
        public void init(Move lastMove)
        {
            this.previousMove = lastMove;
            BoardBefore = previousMove.BoardAfter.copy();
            BoardAfter = BoardBefore.copy();
            if (previousMove.turn == "white")
            {
                turn = "black";
            }
            else
            {
                turn = "white";
            }
            int[][] Kings = findKings(BoardBefore);
            if (BoardBefore.c[(Kings[0][0]), (Kings[0][1])].color == turn)
            {
                playerKing = new int[] { Kings[0][0], Kings[0][1] };
                enemyKing = new int[] { Kings[1][0], Kings[1][1] };
            }
            else
            {
                playerKing = new int[] { Kings[1][0], Kings[1][1] };
                enemyKing = new int[] { Kings[0][0], Kings[0][1] };
            }
            nextMove = null;
            num = previousMove.num + 1;
            enPassant_rank = -1;
           WKingMoved   = previousMove.WKingMoved;
           WRook1moved  = previousMove.WRook1moved;
           WRook2moved  = previousMove.WRook2moved;
           BKingMoved   = previousMove.BKingMoved;
           BRook1moved  = previousMove.BRook1moved;
           promotion = false;
            promotionX = -1;
            promotionY = -1;
        }
        public Move(Board chessboard)
        { 
            BoardBefore = chessboard.copy();
            BoardAfter = chessboard.copy();

            previousMove = new Move(this);
            previousMove.WKingMoved = false;
            previousMove.WRook1moved = false;
            previousMove.WRook2moved = false;
            previousMove.BKingMoved = false;
            previousMove.BRook1moved = false;
            previousMove.BRook2moved = false;
            previousMove.turn = "black";
            previousMove.num = 0;
            previousMove.BoardBefore = chessboard.copy();
            previousMove.BoardAfter = chessboard.copy();
            previousMove.nextMove = this;



            init(previousMove);
            
            //do the move




        }
        
        public int[][] findKings(Board chessboard)
        {
            int Kingsfound = 0;
            int[][] ret = new int[2][];
            for (int i = 0; i < 8; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (chessboard.c[(i), (c)] != null && chessboard.c[(i), (c)].name == "king")
                    {
                        ret[Kingsfound] = new int[2] {i,c};
                        Kingsfound++;
                        if (Kingsfound == 2)
                        {
                            return ret;
                        }

                    }
                        
                }
            }
            return ret;
        }
        public Move MakeAMove(int srcx,int srcy,int destx, int desty, bool checkLegal)
        {
            bool moveSucceeded;
            if (checkLegal)
            {
                moveSucceeded = BoardBefore.c[srcx, srcy].tryMove(srcx, srcy, destx, desty, this);
            }
            else
            {
                moveSucceeded = true;
                BoardBefore.c[srcx, srcy].doMove(srcx, srcy, destx, desty, this);
            }
            
            if (moveSucceeded)
            {
                if (this.promotion)
                {
                    return this;
                }
                else
                {
                    nextMove = new Move(this);
                    nextMove.WKingMoved = this.WKingMoved;
                    nextMove.WRook1moved = this.WRook1moved;
                    nextMove.WRook2moved = this.WRook2moved;
                    nextMove.BKingMoved = this.BKingMoved;
                    nextMove.BRook1moved = this.BRook1moved;
                    nextMove.BRook2moved = this.BRook2moved;
                    return nextMove;
                }

            }
            else
            {
                init(this.previousMove);
                //wenn move nicht gültig war
                return this;
            }
            
        }
        public void reset()
        {
            init(this.previousMove);
        }
        public Move promote(String wishedPiece)
        {
            
            if (wishedPiece == "queen")
            {
                this.BoardAfter.c[promotionX, promotionY] = new Queen(turn);
            }
            else if (wishedPiece == "knight")
            {
                this.BoardAfter.c[promotionX, promotionY] = new Knight(turn);
            }
            else if (wishedPiece == "rook")
            {
                this.BoardAfter.c[promotionX, promotionY] = new Rook(turn);
            }
            else if (wishedPiece == "bishop")
            {
                this.BoardAfter.c[promotionX, promotionY] = new Bishop(turn);
            }


            nextMove = new Move(this);
            nextMove.WKingMoved = this.WKingMoved;
            nextMove.WRook1moved = this.WRook1moved;
            nextMove.WRook2moved = this.WRook2moved;
            nextMove.BKingMoved = this.BKingMoved;
            nextMove.BRook1moved = this.BRook1moved;
            nextMove.BRook2moved = this.BRook2moved;
            return nextMove;
        }
        public bool Checkmate()
        {
            ArrayList ret =  new ArrayList();
            for (int k = 0; k < 8; k++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (this.BoardBefore.c[k, c] != null)
                    {
                        ret = this.BoardBefore.c[k, c].placesICanGo(k, c, this);
                        if (ret.Count > 0)
                        {
                            return false;
                        }
                    }
                    

                }
            }
            
            return true;
        }
        public bool AmIInCheck()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (BoardBefore.c[(i), (c)] != null && BoardBefore.c[(i), (c)].pseudoPossible(i, c, playerKing[0], playerKing[1], this))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
