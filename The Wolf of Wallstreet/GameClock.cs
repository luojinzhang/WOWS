using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Wolf_of_Wallstreet
{
    class GameClock
    {
        int currentDay = 1;

        public int CurrentDay
        {
            get { return currentDay; }
            set { currentDay = value; }
        }
        int maxDay = 0;
        SolidBrush brush;
        TimeSpan interval;
        string timeSpanHour;
        string timeSpanDay;
        double timeEllapsed=0;
        public int MaxDay
        {
            get { return maxDay; }
            set { maxDay = value; }
        }

        double currentTime = 8;

        public double CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; }
        }
        double maxTime = 16;

        public double MaxTime
        {
            get { return maxTime; }
            set { maxTime = value; }
        }


        public GameClock()
        {
            brush = new SolidBrush(Color.Yellow);

        }

        public void drawString(Graphics grp, Point destPoint, Font newFont, string text)
        {
            // draw string

            grp.DrawString(text, newFont, brush, destPoint);
        }

        public void clockTick(Graphics grp, Point destPoint, Font newFont, double ellapsed)
        {
            // 1 day = 480 mins in-game : 5 mins 
            // 480 ticks : 300s
            // 1,6 tick / 1s
            // 1 tick / 625ms
            // 1 tick / (1/60) hour
            // 1 tick / currentTime += 1/60
            // change the currentTime to string contain hour : minute

            // if ellapsed increase then draw string

            //draw time

            interval = TimeSpan.FromHours(currentTime);
            timeSpanHour = interval.ToString();

            grp.DrawString(timeSpanHour, newFont, brush, destPoint);

            // o day cho this.timeellapsed += ellapsed 

            this.timeEllapsed += ellapsed;

            // o day cho no == 625 ms thi moi chay
            if (this.timeEllapsed == 625)
            {
                currentTime += 1.0 / 60;
                timeEllapsed = 0;
            }

            //draw number of days
            interval = TimeSpan.FromDays(currentDay);
            timeSpanDay = interval.Days.ToString();
            Point temp = destPoint;
            destPoint = new Point(temp.X, temp.Y - 50);
            grp.DrawString(timeSpanDay, newFont, brush, destPoint);
            if (currentTime >= maxTime)
            {
                currentDay++;
                currentTime = 8;
            }

            // if currentDay > maxDay , the game clock will stop tick
        }
    }
}
