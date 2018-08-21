using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{

    public class AI
    {
        Dictionary<string, int[,]> Worths =
            new Dictionary<string, int[,]>();
        Dictionary<string, int> directWorths =
            new Dictionary<string, int>();
        Dictionary<string, int[]> Colorchanger =
           new Dictionary<string, int[]>();
        Dictionary<string, int> mult =
          new Dictionary<string, int>();
        public String Color;
        public String Enemy;
        Form1 myForm;
        public String time_needed;
        public int evaluatedPositions;

        public int evaluateBoard(Board brd)
        {
            int value = 0;
            Figure fig;
            int piecevalue;
            for (int i = 0; i < 8; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (brd.c[i, c] != null)
                    {
                        fig = brd.c[i, c];
                        piecevalue=
                        directWorths[fig.name]
                        + Worths[fig.name][i, Colorchanger[fig.color][c]];
                        value = value + (piecevalue * mult[fig.color]);
                    }
                }
            }
            return value;

        }


        public int evaluateBoardPlus(Move mv)//evaluates the board, but only from the sight of the current player####################### not working 
        {
            
            int eval = evaluateBoard(mv.BoardBefore);
            int moveableFigures=0;
            int totalMoves = 0;
            ArrayList movesOfFigure;
            String turn = mv.turn;

            for (int i = 0; i < 8; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (mv.BoardBefore.c[i, c] != null)
                    {
                        movesOfFigure = mv.BoardBefore.c[i, c].placesICanGo(i, c, mv);
                        if (movesOfFigure.Count > 0)
                        {
                            Console.WriteLine((movesOfFigure.Count * this.mult[mv.BoardBefore.c[i, c].color])+ mv.BoardBefore.c[i, c].name+"("+i+""+c+")"+ movesOfFigure.Count);
                            moveableFigures = moveableFigures + (this.mult[mv.BoardBefore.c[i, c].color]);
                            totalMoves = totalMoves + (movesOfFigure.Count * this.mult[mv.BoardBefore.c[i, c].color]);
                        }
                            
                        
                    }
                }
            }
            Console.WriteLine("eval"+ eval+ " moveableFigures" + moveableFigures + " totalMoves"+ totalMoves);
            Console.Read();
            mv.turn = turn;
            return 10*eval + 20*moveableFigures + 3*totalMoves;

        }

        public AI(String Color, Form1 form)
        {
            Worths.Add("king", new int[,]   { {-30,-40,-40,-50,-50,-40,-40,-30},
                                            {-30,-40,-40,-50,-50,-40,-40,-30},
                                            {-30,-40,-40,-50,-50,-40,-40,-30},
                                            {-30,-40,-40,-50,-50,-40,-40,-30},
                                            {-20,-30,-30,-40,-40,-30,-30,-20},
                                            {-10, 20, 20,-20,-20, 20, 20,-10},
                                            { 20, 20, 20, 0 , 0 , 20, 20, 20},
                                            { 20, 30, 30, 0 , 0 , 30, 30, 20}
                                        });
            Worths.Add("rook", new int[,]   { { 0, 0, 0, 0, 0, 0, 0, 0},
                                            { 5,10,10,10,10,10,10, 5},
                                            {-5, 0, 0, 0, 0, 0, 0,-5},
                                            {-5, 0, 0, 0, 0, 0, 0,-5},
                                            {-5, 0, 0, 0, 0, 0, 0,-5},
                                            {-5, 0, 0, 0, 0, 0, 0,-5},
                                            {-5, 0, 0, 0, 0, 0, 0,-5},
                                            { 0, 0, 0, 5, 5, 0, 0, 0}
                                        });
            Worths.Add("knight",new int[,]{ {-50,-40,-30,-30,-30,-30,-40,-50},
                                            {-40,-20,  0,  0,  0,  0,-20,-40},
                                            {-30,  0, 10, 15, 15, 10,  0,-30},
                                            {-30,  5, 15, 20, 20, 15,  5,-30},
                                            {-30,  0, 15, 20, 20, 15,  0,-30},
                                            {-30,  5, 10, 15, 15, 10,  5,-30},
                                            {-40,-20,  0,  5,  5,  0,-20,-40},
                                            {-50,-40,-30,-30,-30,-30,-40,-50}
                                        });
            Worths.Add("bishop",new int[,]{ {-20,-10,-10,-10,-10,-10,-10,-20,},
                                            {-10,  0,  0,  0,  0,  0,  0,-10,},
                                            {-10,  0,  5, 10, 10,  5,  0,-10,},
                                            {-10,  5,  5, 10, 10,  5,  5,-10,},
                                            {-10,  0, 10, 10, 10, 10,  0,-10,},
                                            {-10, 10, 10, 10, 10, 10, 10,-10,},
                                            {-10,  5,  0,  0,  0,  0,  5,-10,},
                                            {-20,-10,-10,-10,-10,-10,-10,-20 }
                                        });
            Worths.Add("queen",new int[,]{  {-20,-10,-10,- 5,- 5,-10,-10,-20,},
                                            {-10,  0,  0,  0,  0,  0,  0,-10,},
                                            {-10,  0,  5,  5,  5,  5,  0,-10,},
                                            {- 5,  0,  5,  5,  5,  5,  0,- 5,},
                                            {  0,  0,  5,  5,  5,  5,  0,  0,},
                                            {-10,  5,  5,  5,  5,  5,  5,-10,},
                                            {-10,  0,  0,  0,  0,  0,  0,-10,},
                                            {-20,-10,-10,- 5,- 5,-10,-10,-20 }
                                        });
            Worths.Add("pawn", new int[,]{  {-20,-10,-10,- 5,- 5,-10,-10,-20},
                                            {-10,  0,  0,  0,  0,  0,  0,-10},
                                            {-10,  0,  5,  5,  5,  5,  0,-10},
                                            {- 5,  0,  5,  5,  5,  5,  0,- 5},
                                            {  0,  0,  5,  5,  5,  5,  0,  0},
                                            {-10,  5,  5,  5,  5,  5,  5,-10},
                                            {-10,  0,  0,  0,  0,  0,  0,-10},
                                            {-20,-10,-10,- 5,- 5,-10,-10,-20}
                                        });
            directWorths.Add("king", 9000);
            directWorths.Add("queen" ,900);
            directWorths.Add("rook"  ,500);
            directWorths.Add("knight",300);
            directWorths.Add("bishop",300);
            directWorths.Add("pawn"  ,100);

            Colorchanger.Add("white", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });
            Colorchanger.Add("black", new int[] { 7, 6, 5, 4, 3, 2, 1, 0 });
            mult.Add("white", 1);
            mult.Add("black", -1);

            this.Color = Color;
            if (Color == "white")
            {
                this.Enemy = "black";
            }
            else
            {
                this.Enemy = "white";
            }
            
            this.myForm = form;
        }
        public void call()
        {
            //List<int[]> Moves;
            //int currentBoardEval = this.evaluateBoard(myForm.main_game.currentMove.BoardBefore);
            //Moves = possibleMoves();
            //Random rnd = new Random();

            //int num = rnd.Next(0, Moves.Count);
            //this.myForm.main_game.make_move(new int[] { Moves[num][0], Moves[num][1] }, new int[] { Moves[num][2], Moves[num][3] });
            //this.myForm.click(Moves[num][2], Moves[num][3]);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            evaluatedPositions = 0;
            int[] bestMove = findBestMove(3, myForm.main_game.currentMove);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            time_needed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            //this.myForm.main_game.make_move(new int[] { bestMove[1], bestMove[2] }, new int[] { bestMove[3], bestMove[4] });
            this.myForm.main_game.make_move( new int[] { bestMove[1], bestMove[2] }, new int[] { bestMove[3], bestMove[4] } );
            //this.myForm.click(bestMove[3], bestMove[4]);


        }
        private int[] findBestMove(int layer, Move move)
        {
            Move newerMove;
            List<int[]> possibleMove = possibleMoves(move);
            List<int[]> returnAuswahl =new List<int[]>();
            List<int[]> ret = new List<int[]>();
            Console.WriteLine("Ai sitting at layer "+layer);
            Console.Read();
            if (layer != 1)
            {
                for (var i = 0; i < possibleMove.Count; i++)
                {


                    newerMove = move.MakeAMove(possibleMove[i][0]
                                              , possibleMove[i][1]
                                              , possibleMove[i][2]
                                              , possibleMove[i][3]
                                              ,false);
                    returnAuswahl.Add(findBestMove(layer - 1, newerMove));
                    returnAuswahl[i][1] = possibleMove[i][0];
                    returnAuswahl[i][2] = possibleMove[i][1];
                    returnAuswahl[i][3] = possibleMove[i][2];
                    returnAuswahl[i][4] = possibleMove[i][3];

                    move.reset();
                }
                if (returnAuswahl.Count == 0)//schachmatt für den, der gerade am zug ist,
                {
                    Console.WriteLine("possible checkmate detected");
                    Console.Read();
                    returnAuswahl = new List<int[]>();
                    int[] Moveinfo = new int[] {-999999999,
                                                0
                                              , 0
                                              , 0
                                              , 0 };
                    returnAuswahl.Add(Moveinfo);
                    Moveinfo = new int[] {999999999,
                                                0
                                              , 0
                                              , 0
                                              , 0 };
                    returnAuswahl.Add(Moveinfo);
                    if (move.turn == Color)
                    {
                        return returnAuswahl[bestEvaluation(returnAuswahl, Enemy)];
                    }
                    else
                    {
                        return returnAuswahl[bestEvaluation(returnAuswahl, Color)];
                    }
                }
                if (move.turn == Color)
                {
                    return returnAuswahl[bestEvaluation(returnAuswahl, Color)];
                }
                else
                {
                    return returnAuswahl[bestEvaluation(returnAuswahl, Enemy)];
                }

                //gib mir den Move mit dem Besten board

            }
            else if (layer == 1)
            { 
                List<int[]> some = new List<int[]>();
                for (var i = 0; i < possibleMove.Count; i++)
                {
                    evaluatedPositions++;
                    Console.WriteLine("trying move from " + possibleMove[i][0] + "" + possibleMove[i][1] + "(" + move.BoardAfter.c[possibleMove[i][0], possibleMove[i][1]].name + ") to " + possibleMove[i][2] + "" + possibleMove[i][3] );
                    newerMove = move.MakeAMove(possibleMove[i][0]
                                              , possibleMove[i][1]
                                              , possibleMove[i][2]
                                              , possibleMove[i][3]
                                              ,false);
                    int[] Moveinfo = new int[] { evaluateBoard(newerMove.BoardAfter),
                                                possibleMove[i][0]
                                              , possibleMove[i][1]
                                              , possibleMove[i][2]
                                              , possibleMove[i][3] };
                    some.Add(Moveinfo);
                    move.reset();
                }
                if (some.Count == 0)//schachmatt für den, der gerade am zug ist,
                {
                    some.Clear();
                    int[] Moveinfo = new int[] {-999999999,
                                                0
                                              , 0
                                              , 0
                                              , 0 };
                    some.Add(Moveinfo);
                    Moveinfo       = new int[] {999999999,
                                                0
                                              , 0
                                              , 0
                                              , 0 };
                    some.Add(Moveinfo);
                    if (move.turn == Color)
                    {
                        return some[bestEvaluation(some, Enemy)];
                    }
                    else
                    {
                        return some[bestEvaluation(some, Color)];
                    }

                }
                else
                {
                    if (move.turn == Color)
                    {
                        return some[bestEvaluation(some, Color)];
                    }
                    else
                    {
                        return some[bestEvaluation(some, Enemy)];
                    }
                }
                
            }
            Console.WriteLine("didnt retrun move");Console.Read();
            return ret[0];
            
        }
        private int[] findBestMove(int layer,int alpha, int beta, Move move)
        {
            Move newerMove;
            List<int[]> possibleMove = possibleMoves(move);
            List<int[]> returnAuswahl = new List<int[]>();
            List<int[]> ret = new List<int[]>();
            Console.WriteLine("Ai sitting at layer " + layer);
            Console.Read();
            if (layer == 0)
            {

                int[] rett = { evaluateBoard(move.BoardAfter), 0, 0, 0 };
                return rett;

            }
            else
            {
                for (var i = 0; i < possibleMove.Count; i++)
                {
                    newerMove = move.MakeAMove(possibleMove[i][0]
                                              , possibleMove[i][1]
                                              , possibleMove[i][2]
                                              , possibleMove[i][3]
                                              , false);
                    returnAuswahl.Add(findBestMove(layer - 1, newerMove));
                    returnAuswahl[i][1] = possibleMove[i][0];
                    returnAuswahl[i][2] = possibleMove[i][1];
                    returnAuswahl[i][3] = possibleMove[i][2];
                    returnAuswahl[i][4] = possibleMove[i][3];
                    if (move.turn == "white")//max
                    {
                        
                        alpha = Math.Max(alpha, returnAuswahl[i][0]);
                        if (alpha < beta)
                        {
                           // return bestEvaluation(returnAuswahl, move.turn);
                        }
                    }
                    else//min
                    {

                    }
                }
                

            }
            Console.WriteLine("didnt retrun move"); Console.Read();
            return ret[0];

        }
        private int bestEvaluation(List<int[]> auswahl,String color)
        {
            int besteval=0;
            int bestevalindex=0;
            if (color == "black")
            {
                for (int i = 0; i < auswahl.Count; i++){
                    if(auswahl[i][0]< besteval)
                    {
                        besteval = auswahl[i][0];
                        bestevalindex = i;
                    }
                }
                //errechne min
            }
            else
            {
                for (int i = 0; i < auswahl.Count; i++)
                {
                    if (auswahl[i][0] > besteval)
                    {
                        besteval = auswahl[i][0];
                        bestevalindex = i;
                    }
                }
            }
            return bestevalindex;
        }

        private List<int[]> possibleMoves(Move mv)
        {
            List<int[]> ret = new List<int[]>();
            Board brd = mv.BoardBefore;
            for (int i = 0; i < 8; i++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (brd.c[i, c] != null )
                    {
                        foreach (int[] PlaceHeCanGo in brd.c[i, c].placesICanGo(i, c, mv))
                        {
                            ret.Add(new int[] { i, c, PlaceHeCanGo[0], PlaceHeCanGo[1] });
                        }
                           

                    }
                }
            }


            return ret;
        }
    }
}


