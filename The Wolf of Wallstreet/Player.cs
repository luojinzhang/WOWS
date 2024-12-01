using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace The_Wolf_of_Wallstreet
{
    class Player
    {
        int cash;

        public int Cash
        {
            get { return cash; }
            set { cash = value; }
        }
        SolidBrush brush;
        StringFormat format;
        public Player()
        {
            cash = 5000000;
            brush = new SolidBrush(Color.LightGreen);
            format = new StringFormat();
            format.Alignment = StringAlignment.Center;
        }
        public void drawCash(Graphics grp, Point destPoint, Font fontCash)
        {
            grp.DrawString("$" + cash.ToString("#,##0"), fontCash, brush, destPoint, format);
        }

        
        public void buyStock(Stock stock)
        {
            if (cash - Convert.ToInt32(stock.Price) * 1000 < 0)
            {
                MessageBox.Show("Not enough cash");
            }
            else
            {
                cash -= Convert.ToInt32(stock.Price) * 1000;
            }
        }
        public void sellStock(Stock stock)
        {
            cash += Convert.ToInt32(stock.Price) * 1000;
        }
       
    }
}
