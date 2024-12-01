using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Wolf_of_Wallstreet
{
    class Sprite
    {
        protected int offsetX, offsetY, width, height;
        protected Bitmap bmpPic;
        protected Rectangle destRec;

        public Rectangle DestRec
        {
            get { return destRec; }
            set { destRec = value; }
        }
        public Sprite(Bitmap picture, int offsetX, int offsetY, int w, int h)
        {
            this.bmpPic = picture;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.width = w;
            this.height = h;
        }
        public virtual void draw(Graphics grp, Rectangle destRec)
        {
            Rectangle srcRec;
            this.destRec = destRec;
            srcRec = new Rectangle(offsetX, offsetY, width, height);
            grp.DrawImage (this.bmpPic,destRec,srcRec,GraphicsUnit.Pixel);
        }
        public virtual void draw(Graphics grp, Rectangle destRec, int offsetX, int offsetY)
        {
            Rectangle srcRec;
            this.destRec = destRec;
            srcRec = new Rectangle(offsetX, offsetY, this.width, this.height);
            grp.DrawImage(this.bmpPic, destRec, srcRec, GraphicsUnit.Pixel);
        }
    }
}
