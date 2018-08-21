using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Game main_game;
        public ArrayList coloredButtons;
        int clicks = 0;
        int fromCol;
        int fromRow;
        AI ki;
        System.Windows.Forms.Button[,] buttons;
        public Form1()
        {
            InitializeComponent();
            coloredButtons= new ArrayList();
            buttons = new System.Windows.Forms.Button[,] {  { this.button57,this.button49,this.button41,this.button33,this.button25,this.button17,this.button9 ,this.button1},  //x=1
                                                            { this.button58,this.button50,this.button42,this.button34,this.button26,this.button18,this.button10,this.button2},  //x=2
                                                            { this.button59,this.button51,this.button43,this.button35,this.button27,this.button19,this.button11,this.button3},  //x=3
                                                            { this.button60,this.button52,this.button44,this.button36,this.button28,this.button20,this.button12,this.button4},  //x=4
                                                            { this.button61,this.button53,this.button45,this.button37,this.button29,this.button21,this.button13,this.button5},  //x=5
                                                            { this.button62,this.button54,this.button46,this.button38,this.button30,this.button22,this.button14,this.button6},  //x=6
                                                            { this.button63,this.button55,this.button47,this.button39,this.button31,this.button23,this.button15,this.button7},  //x=7
                                                            { this.button64,this.button56,this.button48,this.button40,this.button32,this.button24,this.button16,this.button8},  //x=8
                                                         };

            //redraw_board();

        }
        private void startGame(String type)
        {
            //type ist entwder "ai" oder "human"
            button69.Visible = false;
            button70.Visible = false;
            main_game=new Game();
            redraw_board();
            if (type == "ai")
            {
                ki = new AI("black", this);
                main_game.ki = ki;
            }
            
        }
        public void click(int column, int row)
        {
            textBox1.Text = column.ToString()+"|"+row.ToString();
            if(main_game.currentMove.BoardBefore.c[(column), (row)] !=null)
            {
                textBox3.Text = main_game.currentMove.BoardBefore.c[(column), (row)].color+" "+ main_game.currentMove.BoardBefore.c[(column), (row)].name;
                //textBox6.Text=
                textBox6.Text = main_game.currentMove.num +"";
            }
            
            clicks = clicks + 1;
            if (clicks == 1)
            {
                main_game.moveState = "none";
                fromCol = column;
                fromRow = row;
                if(main_game.currentMove.BoardBefore.c[fromCol, fromRow] != null)
                {
                    foreach(int[] PlaceHeCanGo in main_game.currentMove.BoardBefore.c[fromCol, fromRow].placesICanGo(fromCol, fromRow, main_game.currentMove))
                    {
                        colorButton(PlaceHeCanGo[0], PlaceHeCanGo[1], Color.Green);

                    }
                }
            }
            else
            {
                uncolorButtons();
                //enter move
                if (main_game.currentMove.BoardBefore.c[fromCol, fromRow] != null)
                {
                    main_game.moveState = "sending";
                    main_game.make_move(new int[] { fromCol, fromRow }
                                       , new int[] { column, row });
                    if (main_game.currentMove.promotion)
                    {
                        //highlight promoter
                        colorButton(main_game.currentMove.promotionX, main_game.currentMove.promotionY, Color.Green);
                        button65.Visible = true;
                        button66.Visible = true;
                        button67.Visible = true;
                        button68.Visible = true;
                    }
                    if (main_game.currentMove.AmIInCheck())
                    {
                        colorButton(main_game.currentMove.playerKing[0], main_game.currentMove.playerKing[1], Color.Red);
                    }
                    if (main_game.winner != null)
                    {
                        textBox7.Text = main_game.winner + " side wins!";
                    }
                    redraw_board();
                }
                else {
                    main_game.moveState = "no fig";
                }
                clicks = 0;
                fromCol = -1;
                fromRow = -1;
            }
            textBox2.Text = fromCol.ToString() + "|" +fromRow.ToString();
            textBox4.Text = main_game.moveState;
            textBox5.Text = main_game.movedenial;

            label4.Text = main_game.currentMove.turn + "'s Turn";
            if (ki != null)
            {
                textBox8.Text = "" + ki.evaluateBoard(main_game.currentMove.BoardBefore);
                textBox9.Text = "" + ki.evaluateBoardPlus(main_game.currentMove);
                if (ki.Color == main_game.currentMove.turn)
                {
                    ki.call();
                    textBox2.Text = fromCol.ToString() + "|" + fromRow.ToString();
                    textBox4.Text = main_game.moveState;
                    textBox5.Text = main_game.movedenial;

                    label4.Text = main_game.currentMove.turn + "'s Turn";
                    textBox8.Text = "" + ki.evaluateBoard(main_game.currentMove.BoardBefore);
                    textBox9.Text = "" + ki.evaluateBoardPlus(main_game.currentMove);
                    label10.Text = "evaluated "+ki.evaluatedPositions+"positions in "+ ki.time_needed;
                    redraw_board();
                }
            }
            



        }
        public void colorButton(int x, int y, Color newColor)
        {
            coloredButtons.Add(new ArrayList() { x, y, buttons[x, y].BackColor });
            buttons[x, y].BackColor = newColor;
        }
        public void uncolorButtons()
        {
            foreach (ArrayList Buttoninfo in coloredButtons)
            {
                buttons[(int)Buttoninfo[0], (int)Buttoninfo[1]].BackColor = (Color)Buttoninfo[2];
            }
            coloredButtons.Clear();
        }

        public void SetTextbox3(String val) {
            textBox3.Text = val;
        }
        public void redraw_board ()
        {
            int chessboard_count = 8;
            Button but;
            for (int x = 0; x < chessboard_count; x++)
            {
                for (int y = 0; y < chessboard_count; y++)
                {
                    if (main_game.currentMove.BoardAfter.c[(x), (y)] != null)
                    {
                        but = buttons[(x), (y)];
                       but.Image = ResizeImage(
                                                 Image.FromFile(main_game.currentMove.BoardAfter.c[(x), (y)].color
                                                + "_" + main_game.currentMove.BoardAfter.c[(x), (y)].name
                                                + "_white.png"
                                                , true)
                                                , but.Width
                                                , but.Height);
                        //this.buttons[(x), (y)].Text = main_game.currentMove.BoardAfter.c[(x), (y)].color + " " + main_game.currentMove.BoardAfter.c[(x), (y)].name;
                    }
                    else {
                        this.buttons[(x), (y)].Image = null;
                    }
                    



                }
            }
            

               


        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            click(0, 7);
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            click(1, 7);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            click(2, 7);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            click(3, 7);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            click(4, 7);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            click(5, 7);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            click(6, 7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            click(7, 7);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            click(0, 6);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            click(1, 6);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            click(2, 6);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            click(3, 6);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            click(4, 6);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            click(5, 6);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            click(6, 6);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            click(7, 6);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            click(0, 5);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            click(1, 5);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            click(2, 5);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            click(3, 5);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            click(4, 5);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            click(5, 5);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            click(6, 5);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            click(7, 5);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            click(0, 4);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            click(1, 4);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            click(2, 4);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            click(3, 4);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            click(4, 4);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            click(5, 4);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            click(6, 4);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            click(7, 4);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            click(0, 3);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            click(1, 3);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            click(2, 3);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            click(3, 3);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            click(4, 3);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            click(5, 3);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            click(6, 3);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            click(7, 3);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            click(0, 2);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            click(1, 2);

        }

        private void button43_Click(object sender, EventArgs e)
        {
            click(2, 2);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            click(3, 2);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            click(4, 2);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            click(5, 2);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            click(6, 2);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            click(7, 2);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            click(0, 1);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            click(1, 1);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            click(2, 1);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            click(3, 1);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            click(4, 1);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            click(5, 1);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            click(6, 1);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            click(7, 1);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            click(0, 0);
        }

        private void button58_Click(object sender, EventArgs e)
        {
            click(1, 0);
        }

        private void button59_Click(object sender, EventArgs e)
        {
            click(2, 0);
        }

        private void button60_Click(object sender, EventArgs e)
        {
            click(3, 0);
        }

        private void button61_Click(object sender, EventArgs e)
        {
            click(4, 0);
        }

        private void button62_Click(object sender, EventArgs e)
        {
            click(5, 0);
        }

        private void button63_Click(object sender, EventArgs e)
        {
            click(6, 0);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            click(7, 0);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button65_Click(object sender, EventArgs e)
        {

            unvisiblepromotion();
            main_game.make_promotion("queen");
            redraw_board();
        }

        private void button66_Click(object sender, EventArgs e)
        {
            unvisiblepromotion();
            main_game.make_promotion("rook");
            redraw_board();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            unvisiblepromotion();
            main_game.make_promotion("knight");
            redraw_board();
        }

        private void button68_Click(object sender, EventArgs e)
        {
            unvisiblepromotion();
            main_game.make_promotion("bishop");
            redraw_board();
        }
        private void unvisiblepromotion()
        {
            button65.Visible = false;
            button66.Visible = false;
            button67.Visible = false;
            button68.Visible = false;
            uncolorButtons();
        }

        private void button69_Click(object sender, EventArgs e)
        {
            //play against ai
            startGame("ai");
        }

        private void button70_Click(object sender, EventArgs e)
        {
            //play against human
            startGame("human");
        }

        private void button71_Click(object sender, EventArgs e)
        {
            this.ki.call();
            this.redraw_board();
        }
    }
}
