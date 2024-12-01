using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace The_Wolf_of_Wallstreet
{
    class AnimSprite : Sprite
    {
        int currentFrame = 0;
        public int CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }
        double interval = 0;
        int numberOfFrames;
        Rectangle oldDestRec;
        SolidBrush brush;
        public AnimSprite(Bitmap picture, int offsetX, int offsetY, int w, int h, int numberOfFrames, Color formColor)
            :base (picture,offsetX,offsetY,w,h)
        {
            this.numberOfFrames = numberOfFrames;
            brush = new SolidBrush (formColor);
        }
        public void draw_X_sprite(Graphics grp, Rectangle destRec, double interval)
        {
           grp.FillRectangle(brush, oldDestRec);
           this.interval += interval;
           Rectangle srcRect;
           srcRect = new Rectangle(currentFrame * this.width, offsetY, width, height);
           grp.DrawImage(bmpPic, destRec, srcRect, GraphicsUnit.Pixel);
           if (this.interval >= 300)
           {
               currentFrame++;
               this.interval = 0;
           }
           
           if (this.currentFrame == numberOfFrames)
           {
               currentFrame = 0;
           }
           oldDestRec = destRec;
        }
        public void draw_Y_sprite(Graphics grp, Rectangle destRec, double interval)
        {
            grp.FillRectangle(brush, oldDestRec);
            this.interval += interval;
            Rectangle srcRect;
            srcRect = new Rectangle(offsetX, this.numberOfFrames * currentFrame, width, height);
            grp.DrawImage(bmpPic, destRec, srcRect, GraphicsUnit.Pixel);
            if (this.interval >= 300)
            {
                currentFrame++;
                this.interval = 0;
            }

            if (this.currentFrame == numberOfFrames)
            {
                currentFrame = 0;
            }
            oldDestRec = destRec;
        }
    }
}
