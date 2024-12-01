using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Wolf_of_Wallstreet
{
    class Stock
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string code;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        float pool, quantity, defaultPrice, changed, startPrice, price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        int trend = 1;
        int count = 0;
        SolidBrush brush;
        static Random rnd;
        double interval;


        public Stock(string name, string code, int defaultPrice)
        {
            this.name = name;
            this.code = code;
            this.defaultPrice = defaultPrice;
            this.quantity = 10000000;
            this.pool = 100000000 - this.quantity;
            this.startPrice = defaultPrice * pool / quantity;
            this.changed = price - startPrice;
            brush = new SolidBrush(Color.Yellow);
            rnd = new Random();
        }

        public void randomGenerateStockAmount()
        {
            // check if the quantity has changed 20 times ?
            if (count >= 10)
            {
                // if yes then
                int check = rnd.Next(2);
                if (check == 1)
                {
                    trend *= -1;
                }
                count = 0;
            }
            // generate the numbers of stocks change and re-define the quantity and pool
           
            this.quantity += trend * rnd.Next(1000, 100000);
            this.pool = 100000000 - this.quantity;
            count++;
        }
        public void calculatePrice()
        {
            //Price value on market = Default price * pool value / number of stocks available on market
            this.price = defaultPrice * pool / quantity;

            // make sure the changed is compare to the start price of the begining
            this.changed = price - startPrice;
        }
        public void drawString(Graphics grph, float x, float y, Font font, GameClock clock, double interval)
        {
            // before draw do the logic 
            // generate new stock amounts every ticks
            this.interval += interval;
            if (this.interval == 625)
            {
                randomGenerateStockAmount();

                // calculate price
                calculatePrice();

                // make sure the start price change to the lastest price after new day
                if (clock.CurrentTime == 8)
                {
                    this.startPrice = this.price;
                }
                this.interval = 0;
            }
            // draw the name and code of stock
            grph.DrawString(code, font, brush, x, y);

            // draw the price and the change
            grph.DrawString(price.ToString("0.0") + "/ " + changed.ToString("0.0"), font, brush, x + 100, y);

            // draw quantity
            grph.DrawString(quantity.ToString("#,##0"), font, brush, x + 200, y);
        }

        public void playerBoughtStock()
        {
            this.quantity -= 1000;
            this.pool -= 1000;
        }
        public void playerSellStock()
        {
            this.quantity += 1000;
            this.pool += 1000;
        }
    }
}
