using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Wolf_of_Wallstreet
{
    class GameButton : Sprite
    {
        int x, y, w, h;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public GameButton(Bitmap picture, int offsetX, int offsetY, int frameWidth, int frameHeight, int x, int y, int w, int h)
            : base(picture, offsetX, offsetY, w, h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.width = frameWidth;
            this.height = frameHeight;
        }
        public override void draw(Graphics grp, Rectangle destRec)
        {
            base.draw(grp, destRec);
        }
        public override void draw(Graphics grp, Rectangle destRec, int offsetX, int offsetY)
        {
            base.draw(grp, destRec, offsetX, offsetY);
        }
    }
}
