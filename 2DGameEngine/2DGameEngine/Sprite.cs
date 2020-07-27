using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGameEngine
{
    class Sprite
    {
        Image image;
        Point pos;

        public Sprite(Image image,int x, int y)
        {
            this.image = image;
            pos = new Point(x, y);
            GameLoop.onDraw += GameLoop_onDraw;
        }

        public int X { set => pos.X = value; get => pos.X; }
        public int Y { set => pos.X = value; get => pos.Y; }
        public Point Pos { set => pos = value; get => pos; }

        private void GameLoop_onDraw(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, pos);
        }
    }
}
