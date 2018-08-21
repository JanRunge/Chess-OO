using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace WindowsFormsApp3
{
    public class Game
    {
        //public String[,] chessboard;
        public Board chessboard;
        public String[,] chessboard_human= {{ "A1", "white_pawn", "", "", "", "", "black_pawn", "black_rook" },    //x=1
                             { "B1","white_pawn","","","","","black_pawn","black_knight"},  //x=2
                             { "C1","white_pawn","","","","","black_pawn","black_bishop"},  //x=3
                             { "D1" ,"white_pawn","","","","","black_pawn","black_queen"},   //x=4
                             { "E1"  ,"white_pawn","","","","","black_pawn","black_king"},    //x=5
                             { "F1","white_pawn","","","","","black_pawn","black_bishop"},  //x=6
                             { "G1","white_pawn","","","","","black_pawn","black_knight"},  //x=7
                             { "H1"  ,"white_pawn","","","","","black_pawn","black_rook"},    //x=8
                                };
        
        public String state;
        public int time_left_for_turn = 1000;
        public String moveState;
        public String movedenial;
        public Move currentMove;
        public string winner;
        public AI ki;

        public Game()
        {
            init();
        }
        void init()
        {
            state = "running";
            Figure whiteKing    = new King  ("white"); 
            Figure whiteRook1   = new Rook  ("white");
            Figure whiteRook2   = new Rook  ("white");
            Figure whiteBishop1 = new Bishop("white");
            Figure whiteBishop2 = new Bishop("white");
            Figure whiteKnight1 = new Knight("white");
            Figure whiteKnight2 = new Knight("white");
            Figure whiteQueen   = new Queen ("white");
            Figure whitePawn1   =new Pawn("white");
            Figure whitePawn2   =new Pawn("white");
            Figure whitePawn3   =new Pawn("white");
            Figure whitePawn4   =new Pawn("white");
            Figure whitePawn5   =new Pawn("white");
            Figure whitePawn6   =new Pawn("white");
            Figure whitePawn7   =new Pawn("white");
            Figure whitePawn8   =new Pawn("white");
            

            Figure blackKing    = new King  ("black"); 
            Figure blackRook1   = new Rook  ("black");
            Figure blackRook2   = new Rook  ("black");
            Figure blackBishop1 = new Bishop("black");
            Figure blackBishop2 = new Bishop("black");
            Figure blackKnight1 = new Knight("black");
            Figure blackKnight2 = new Knight("black");
            Figure blackQueen   = new Queen ("black");
            Figure blackPawn1   = new Pawn("black");
            Figure blackPawn2   = new Pawn("black");
            Figure blackPawn3   = new Pawn("black");
            Figure blackPawn4   = new Pawn("black");
            Figure blackPawn5   = new Pawn("black");
            Figure blackPawn6   = new Pawn("black");
            Figure blackPawn7   = new Pawn("black");
            Figure blackPawn8   = new Pawn("black");

           







            

            this.chessboard=new Board(new Figure[,] {  {whiteRook1    ,whitePawn1,null,null,null,null,blackPawn1,blackRook1  },  //x=1
                                             {whiteKnight1  ,whitePawn2,null,null,null,null,blackPawn2,blackKnight1},  //x=2
                                             {whiteBishop1  ,whitePawn3,null,null,null,null,blackPawn3,blackBishop1},  //x=3
                                             {whiteQueen    ,whitePawn4,null,null,null,null,blackPawn4,blackQueen  },  //x=4
                                             {whiteKing     ,whitePawn5,null,null,null,null,blackPawn5,blackKing   },  //x=5
                                             {whiteBishop2  ,whitePawn6,null,null,null,null,blackPawn6,blackBishop2},  //x=6
                                             {whiteKnight2  ,whitePawn7,null,null,null,null,blackPawn7,blackKnight2},  //x=7
                                             {whiteRook2    ,whitePawn8,null,null,null,null,blackPawn8,blackRook2  },  //x=8
                                });
            currentMove = new Move(this.chessboard);  


        }
        
        public void make_move(int[] from, int[] to)
        {
            currentMove = currentMove.MakeAMove(from[0], from[1], to[0], to[1], true);
            afterMove();
        }
        public void make_promotion(String wish)
        {
            currentMove = currentMove.promote(wish);
            afterMove();
        }
        public void afterMove()
        {
            if (currentMove.Checkmate())
            {
                state = "over";
                if (currentMove.turn == "white")
                {
                    winner = "black";
                }
                else
                {
                    winner = "white";
                }
            }
            
        }

    }
}

