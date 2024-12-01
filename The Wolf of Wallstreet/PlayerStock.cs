using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Wolf_of_Wallstreet
{
    class PlayerStock
    {
        int numberOfStockBought;

        public int NumberOfStockBought
        {
            get { return numberOfStockBought; }
            set { numberOfStockBought = value; }
        }
        string name, code;
        public PlayerStock(Stock stock)
        {
            this.name = stock.Name;
            this.code = stock.Code;
            this.numberOfStockBought = 0;
        }
        public void stockBought()
        {
            this.numberOfStockBought += 1000;
        }
        public void stockSold()
        {
            this.numberOfStockBought -= 1000;
        }

        public void drawInventory(Graphics grp, Point destPoint, Font font)
        {
            grp.DrawString(this.name, font, Brushes.Yellow, destPoint);
            Point temp = destPoint;
            destPoint = new Point(temp.X + 200, temp.Y);
            grp.DrawString(this.numberOfStockBought.ToString(), font, Brushes.Yellow, destPoint);
        }
    }
}
