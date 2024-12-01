using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace The_Wolf_of_Wallstreet
{
    public partial class Form1 : Form
    {
        Bitmap bmpBackBuffer;
        Graphics graBackBuffer;
        Sprite spriteBackGround, spriteTitle, spriteButton, spriteBoard;
        Rectangle destRec;
        SolidBrush brush;
        AnimSprite animSpriteCoin;
        Point mouse;
        GameButton gameButtonStart, gameButtonExit, gameButtonEasy, gameButtonNormal, gameButtonHard;
        GameClock clock;
        Font font;
        PrivateFontCollection privateFontCollection;
        Point destPoint;
        Player player;
        PlayerStock[] playerStock;
        Stock stockFpt, stockVng;
        bool boolStart, boolExit, boolChooseDifficult, boolPickBuy, boolPickSell = false;
        int goal;
        double ellapsed;
        public Form1()
        {
            InitializeComponent();
            bmpBackBuffer = new Bitmap(this.Width, this.Height);
            graBackBuffer = Graphics.FromImage(bmpBackBuffer);
            this.BackgroundImage = bmpBackBuffer;
            spriteBackGround = new Sprite(GameResources.The_Wolf_of_Wall_Street, 0, 0, GameResources.The_Wolf_of_Wall_Street.Width, GameResources.The_Wolf_of_Wall_Street.Height);
            spriteTitle = new Sprite(GameResources.banner, 0, 0, GameResources.banner.Width, GameResources.banner.Height);
            spriteButton = new Sprite(GameResources.sprite_button, 0, 0, 316, 140);
            player = new Player();
            animSpriteCoin = new AnimSprite(GameResources.sprite_coin, 0, 0, 44, 40, 10, Color.Transparent);
            gameButtonStart = new GameButton(GameResources.sprite_button, 0, 140 * 2, 316, 140, 330, 400, 158, 70);
            gameButtonExit = new GameButton(GameResources.sprite_button, 0, 140, 316, 140, 330, 480, 158, 70);
            gameButtonEasy = new GameButton(GameResources.sprite_button, 0, 0, 316, 140, 330, 200, 158, 70);
            gameButtonHard = new GameButton(GameResources.sprite_button, 316, 0, 316, 140, 330, 280, 158, 70);
            gameButtonNormal = new GameButton(GameResources.sprite_button, 316 * 2, 0, 316, 140, 330, 360, 158, 70);
            clock = new GameClock();
            boolStart = false;
            brush = new SolidBrush(this.BackColor);
            privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddFontFile("Resources/alarm clock.ttf");
            privateFontCollection.AddFontFile("Resources/Hanged Letters.ttf");
            privateFontCollection.AddFontFile("Resources/CHINESETAKEAWAY.ttf");
            font = new Font(privateFontCollection.Families[0], 12f);
            stockFpt = new Stock("FPT TELECOM", "FPT", 5);
            stockVng = new Stock("VINAGAME", "VNG", 3);
            playerStock = new PlayerStock[2];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // draw background

            destRec = new Rectangle(0, 0, this.Width, this.Height);
            spriteBackGround.draw(graBackBuffer, destRec);

            //draw title

            destRec = new Rectangle(200, 100, 400, 200);
            spriteTitle.draw(graBackBuffer, destRec);

            //draw button
            //draw newgame button

            destRec = new Rectangle(330, 400, 158, 70);
            gameButtonStart.draw(graBackBuffer, destRec);

            //draw exit button

            destRec = new Rectangle(330, 480, 158, 70);
            gameButtonExit.draw(graBackBuffer, destRec);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            //create a point that points the mouse position

            mouse = this.PointToClient(Cursor.Position);

            // draw button when hover over

            if (boolStart == false && boolExit == false)
            {
                if (e.Y >= gameButtonStart.Y - 30 && e.Y <= gameButtonStart.Y + 70 - 30)
                {
                    gameButtonStart.draw(graBackBuffer, gameButtonStart.DestRec, 316 * 1, 140 * 2);
                }
                else
                {
                    gameButtonStart.draw(graBackBuffer, gameButtonStart.DestRec);
                }

                if (e.Y >= gameButtonExit.Y - 30 && e.Y <= gameButtonExit.Y + 70 - 30)
                {
                    gameButtonExit.draw(graBackBuffer, gameButtonExit.DestRec, 316 * 1, 140);
                }
                else
                {
                    gameButtonExit.draw(graBackBuffer, gameButtonExit.DestRec);
                }
                this.Refresh();
            }

            // check if mouse hover over difficult button

            else if (boolStart == true && timerAnim.Enabled == false && boolChooseDifficult == false)
            {
                if (mouse.Y >= 200 - 30 && mouse.Y <= 270 - 30 || mouse.Y >= 280 - 30 && mouse.Y <= 280 + 70 - 30 || mouse.Y >= 360 - 30 && mouse.Y <= 360 + 70 - 30)
                {
                    timerAnim.Start();
                }
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // if buy stock

            if (boolPickBuy == true)
            {
                //fpt
                if (e.X >= 50 && e.X <= 350 - 10 && e.Y >= 150 + 10 && e.Y < 150 + 10 + 12)
                {
                    stockFpt.playerBoughtStock();
                    player.buyStock(stockFpt);
                    if (playerStock[0] == null)
                    {
                        playerStock[0] = new PlayerStock(stockFpt);
                    }
                    playerStock[0].stockBought();
                }
                //vinagame
                else if (e.X >= 50 && e.X <= 350 - 10 && e.Y >= 150 + 40 && e.Y < 150 + 40 + 12)
                {
                    stockVng.playerBoughtStock();
                    player.buyStock(stockVng);
                    if (playerStock[1] == null)
                    {
                        playerStock[1] = new PlayerStock(stockVng);
                    }
                    playerStock[1].stockBought();
                }
            }

            //if sell stock

            if (boolPickSell == true)
            {
                if (e.X >= 450 && e.X <= 450 + 300 - 10 && e.Y >= 150 + 10 && e.Y < 150 + 10 + 12 && playerStock[0] != null && playerStock[0].NumberOfStockBought >= 1000)
                {
                    stockFpt.playerSellStock();
                    player.sellStock(stockFpt);
                    playerStock[0].stockSold();
                }
                else if (e.X >= 450 && e.X <= 450 + 300 - 10 && e.Y >= 150 + 40 && e.Y < 150 + 40 + 12 && playerStock[1] != null && playerStock[1].NumberOfStockBought >= 1000)
                {
                    stockVng.playerSellStock();
                    player.sellStock(stockVng);
                    playerStock[1].stockSold();
                }
            }

            // draw ingame screen and start the time clock when pick difficult

            if (boolStart == true && boolChooseDifficult == false)
            {
                // limit the time for clock in-game

                if (mouse.Y >= 200 - 30 && mouse.Y <= 270 - 30 || mouse.Y >= 280 - 30 && mouse.Y <= 280 + 70 - 30 || mouse.Y >= 360 - 30 && mouse.Y <= 360 + 70 - 30)
                {
                    if (mouse.Y >= 200 - 30 && mouse.Y <= 270 - 30)
                    {
                        clock.MaxDay = 1;
                        goal = 7000000;
                    }
                    if (mouse.Y >= 280 - 30 && mouse.Y <= 280 + 70 - 30)
                    {
                        clock.MaxDay = 3;
                        goal = 10000000;
                    }
                    else if (mouse.Y >= 360 - 30 && mouse.Y <= 360 + 70 - 30)
                    {
                        clock.MaxDay = 7;
                        goal = 20000000;
                    }
                    timerAnim.Stop();
                    boolChooseDifficult = true;
                    timerClock.Start();
                }
            }
            // clicked after choose difficult
            else if (boolChooseDifficult == true && timerClock.Enabled == true)
            {
                // if clicked news button
                if (e.X >= 25 && e.X <= 25 + GameResources.News_Events_button.Width && e.Y >= 25 && e.Y <= GameResources.News_Events_button.Height + 25)
                {
                    MessageBox.Show("News");
                }

                // if press buy and sell button
                // clicked buy button , the value of mouse now is "buy"
                if (e.Y >= 470 - 30 && e.Y <= 470 + 100 - 30 && e.X >= 200 - 250 / 2 && e.X <= 200 - 250 / 2 + 250)
                {
                    if (boolPickBuy == false || boolPickSell == true)
                    {
                        boolPickBuy = true;
                        boolPickSell = false;
                    }
                    else if (boolPickBuy == true)
                    {
                        boolPickBuy = false;
                    }
                }
                //clicked sell button , the value of mouse now is "sell"
                if (e.Y >= 470 - 30 && e.Y <= 470 + 100 - 30 && e.X >= 450 + 150 - 250 / 2 && e.X <= 450 + 150 - 250 / 2 + 250)
                {
                    if (boolPickSell == false || boolPickBuy == true)
                    {
                        boolPickBuy = false;
                        boolPickSell = true;
                    }
                    else if (boolPickSell == true)
                    {
                        boolPickSell = false;
                    }
                }
            }


            // draw difficult button when clicked

            if (boolStart == false && boolExit == false)
            {
                if (e.Y >= gameButtonStart.Y - 30 && e.Y <= gameButtonStart.Y + 70 - 30)
                {
                    timerDelay.Start();
                    boolStart = true;
                    gameButtonStart.draw(graBackBuffer, gameButtonStart.DestRec, 316 * 2, 140 * 2);
                }
                if (e.Y >= gameButtonExit.Y - 30 && e.Y <= gameButtonExit.Y + 70 - 30)
                {
                    timerDelay.Start();
                    boolExit = true;
                    gameButtonExit.draw(graBackBuffer, gameButtonExit.DestRec, 316 * 2, 140 * 1);
                }
            }


            this.Refresh();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            ellapsed++;

            // do the clicked button command

            if (boolStart == true && ellapsed >= 5)
            {
                // Clear old screen

                graBackBuffer.Clear(this.BackColor);

                // Create new screen

                destRec = new Rectangle(0, 0, this.Width, this.Height);
                spriteBackGround.draw(graBackBuffer, destRec);
                this.Refresh();

                //Create difficult screen and button

                difficult_screen();
                this.Refresh();
                ellapsed = 0;
                timerDelay.Stop();
                timerDelay.Dispose();
            }
            if (boolExit == true && ellapsed >= 5)
            {
                this.Close();
            }
        }

        private void difficult_screen()
        {
            //draw easy button
            destRec = new Rectangle(330, 200, 158, 70);
            gameButtonEasy.draw(graBackBuffer, destRec);


            //draw normal button
            destRec = new Rectangle(330, 280, 158, 70);
            gameButtonNormal.draw(graBackBuffer, destRec);

            //draw hard button
            destRec = new Rectangle(330, 280 + 80, 158, 70);
            gameButtonHard.draw(graBackBuffer, destRec);
        }
        Rectangle oldRec;
        private void timerAnim_Tick(object sender, EventArgs e)
        {
            // re-draw the background and difficult button
            if (boolChooseDifficult == false && boolStart == true)
            {
                destRec = new Rectangle(0, 0, this.Width, this.Height);
                spriteBackGround.draw(graBackBuffer, destRec);
                difficult_screen();

                // check if mouse hover on button and draw the coin animation

                if (mouse.Y >= 200 - 30 && mouse.Y <= 270 - 30 || mouse.Y >= 280 - 30 && mouse.Y <= 280 + 70 - 30 || mouse.Y >= 360 - 30 && mouse.Y <= 360 + 70 - 30)
                {
                    if (mouse.Y >= 200 - 30 && mouse.Y <= 270 - 30)
                    {
                        destRec = new Rectangle(330 + 158, 220, 44, 40);
                    }
                    if (mouse.Y >= 280 - 30 && mouse.Y <= 280 + 70 - 30)
                    {
                        destRec = new Rectangle(330 + 158, 300, 44, 40);
                    }
                    else if (mouse.Y >= 360 - 30 && mouse.Y <= 360 + 70 - 30)
                    {
                        destRec = new Rectangle(330 + 158, 380, 44, 40);
                    }
                    animSpriteCoin.draw_X_sprite(graBackBuffer, destRec, timerAnim.Interval + 250);
                    oldRec = destRec;
                }
                else
                {
                    destRec = new Rectangle(0, 0, this.Width, this.Height);
                    spriteBackGround.draw(graBackBuffer, destRec);
                    difficult_screen();
                    animSpriteCoin.CurrentFrame = 0;
                }
            }
            this.Refresh();
        }
        private void timerClock_Tick(object sender, EventArgs e)
        {
            // re-draw the background screen
            graBackBuffer.Clear(this.BackColor);
            destRec = new Rectangle(0, 0, this.Width, this.Height);
            spriteBackGround = new Sprite(GameResources.NYSE, 0, 0, GameResources.NYSE.Width, GameResources.NYSE.Height);
            spriteBackGround.draw(graBackBuffer, destRec);
            // check if reach goal
            if (player.Cash >= goal)
            {
                this.Refresh();
                timerClock.Stop();
                MessageBox.Show("You win");
            }

            // draw in-game background, market board, buy & sell button, inventory board
            else
            {
                // re-draw the gameClock
                font = new Font(privateFontCollection.Families[0], 12f);
                destPoint = new Point(600, 50);
                clock.drawString(graBackBuffer, destPoint, font, "DAY : ");
                destPoint = new Point(600, 100);
                clock.drawString(graBackBuffer, destPoint, font, "TIME: ");

                // re-draw everytime clock tick

                destPoint = new Point(650, 100);
                clock.clockTick(graBackBuffer, destPoint, font, timerClock.Interval);


                // draw market blackboard
                spriteBoard = new Sprite(GameResources.blackboard, 0, 0, 300, 300);
                destRec = new Rectangle(50, 150, 300, 300);
                spriteBoard.draw(graBackBuffer, destRec);

                // draw the stocks
                font = new Font(privateFontCollection.Families[0], 12f);
                stockFpt.drawString(graBackBuffer, 50, 150 + 20, font, clock, timerClock.Interval);
                stockVng.drawString(graBackBuffer, 50, 150 + 12 + 40, font, clock, timerClock.Interval);

                // draw inventory board
                spriteBoard = new Sprite(GameResources.inventory, 0, 0, 300, 300);
                destRec = new Rectangle(450, 150, 300, 300);
                spriteBoard.draw(graBackBuffer, destRec);


                //draw inventory
                for (int i = 0; i < playerStock.Length; i++)
                {
                    if (playerStock[i] != null && playerStock[i].NumberOfStockBought > 0)
                    {
                        int j = 0;
                        if (i != 0)
                        {
                            j = 12;
                        }
                        destPoint = new Point(450, 150 + 20 * (i + 1) + j);
                        playerStock[i].drawInventory(graBackBuffer, destPoint, font);
                    }
                }

                //draw goal
                font = new Font(privateFontCollection.Families[1],15f);
                destPoint = new Point(25, 100);
                graBackBuffer.DrawString("Goal : $" + goal.ToString("#,##0"), font, Brushes.Khaki, destPoint);

                //draw buy & sell buttons

                spriteButton = new Sprite(GameResources.buy_3x2, 0, 0, 407, 160);
                destRec = new Rectangle(200 - 250 / 2, 470, 250, 100);
                spriteButton.draw(graBackBuffer, destRec, 0, 0);

                destRec = new Rectangle(450 + 150 - 250 / 2, 470, 250, 100);
                spriteButton.draw(graBackBuffer, destRec, 0, 160);

                // redraw buy button with hover mode

                if (mouse.Y >= 470 - 30 && mouse.Y <= 470 + 100 - 30 && mouse.X >= 200 - 250 / 2 && mouse.X <= 200 - 250 / 2 + 250)
                {
                    spriteButton = new Sprite(GameResources.buy_3x2, 0, 0, 407, 160);
                    destRec = new Rectangle(200 - 250 / 2, 470, 250, 100);
                    spriteButton.draw(graBackBuffer, destRec, 407, 0);
                }
                if (mouse.Y >= 470 - 30 && mouse.Y <= 470 + 100 - 30 && mouse.X >= 450 + 150 - 250 / 2 && mouse.X <= 450 + 150 - 250 / 2 + 250)
                {
                    spriteButton = new Sprite(GameResources.buy_3x2, 0, 0, 407, 160);
                    destRec = new Rectangle(450 + 150 - 250 / 2, 470, 250, 100);
                    spriteButton.draw(graBackBuffer, destRec, 407, 160);
                }

                //draw cash value
                Font fontCash = new Font(privateFontCollection.Families[2], 50f);
                destPoint = new Point(800 / 2, 0);
                player.drawCash(graBackBuffer, destPoint, fontCash);

                //draw buy and sell symbol at mouse if buy and sell was clicked
                font = new Font(privateFontCollection.Families[1], 20f);
                if (boolPickBuy == true)
                {
                    graBackBuffer.DrawString("Buy ?", font, Brushes.Crimson, mouse.X + 10, mouse.Y);
                }
                else if (boolPickSell == true)
                {
                    graBackBuffer.DrawString("Sell ?", font, Brushes.Crimson, mouse.X + 10, mouse.Y);
                }

                this.Refresh();

                // check if the game should end when clock over time

                if (clock.CurrentDay > clock.MaxDay)
                {
                    timerClock.Stop();
                    MessageBox.Show("example");
                }
            }
        }

    }
}