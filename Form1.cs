/* Made by Alex McCulloch.
 * Form1.cs is formated only temporarily to test out the backing 
 * vector3D graphics classes I made in the other files.
 * Expect this class will significantly change in the near future.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGameEngine
{
    public partial class Form1 : Form
    {
        Color[] colors;
        Triangle tri1;
        Bitmap bmImage;
        TextureBrush tBrush;
        GraphicsPath gp;
        Matrix4X4 proj;
        float sx,sy,sz,x,y,z,sax,say,saz,ax,ay,az;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            x = 0; y = 0; z=0;
            sx = 0; sy = 0; sz = 0;

            ax = 0;ay = 0;az = 0;
            sax = 0;say = 0;saz = 0;

            //bmImage = new Bitmap(@"C:\Users\Anna\Desktop\sgrass.png");
            //tBrush = new TextureBrush(bmImage);
            //gp = new GraphicsPath();
            
            proj = new Projection3D(this.Size.Width, this.Size.Height, 76, 0.1, 1000);
            
            tri1 = new Triangle(
                    new Vector4(1,-1,0,1),//top left
                    new Vector4(-1, -1, 0, 1),//top right
                    new Vector4(0, 1,0, 1)//bottom center
                    )
            { Pos = new Vector3(0,0, 3),
              Scale = new Vector2(300, 300)
            };

            timer1.Interval = 16;
            timer1.Start();
        }

        private float Clamp(float val, float min, float max)
        {
            if (val <= max && val >= min) return val;

            else if (val >= max) return max;

            else if (val <= min) return min;

            else return 0.0f;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Left == e.KeyCode && sx < 0) { sx = 0; }
            if (Keys.Right == e.KeyCode && sx > 0) { sx = 0; }
            
            if (Keys.Up == e.KeyCode && sy <0) { sy = 0; }
            if (Keys.Down == e.KeyCode && sy >0) { sy = 0; }

            if (Keys.W == e.KeyCode && sz > 0) { sz = 0; }
            if (Keys.S == e.KeyCode && sz < 0 ) { sz = 0; }

            if (Keys.D == e.KeyCode && saz > 0) { saz = 0; }
            if (Keys.A == e.KeyCode && saz < 0) { saz = 0; }

            if (Keys.E == e.KeyCode && sax < 0) { sax = 0; }
            if (Keys.Q == e.KeyCode && sax > 0) { sax = 0; }

            if (Keys.R == e.KeyCode && say > 0) { say = 0; }
            if (Keys.F == e.KeyCode && say < 0) { say = 0; }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Left == e.KeyCode) { sx = -.8f; }
            else if (Keys.Right == e.KeyCode) { sx = .8f; }

            if (Keys.Up == e.KeyCode) { sy = -.8f; }
            else if (Keys.Down == e.KeyCode) { sy = .8f; }

            if (Keys.W == e.KeyCode) { sz = .2f; }
            else if (Keys.S == e.KeyCode) { sz = -.2f; }

            if (Keys.D == e.KeyCode ) { saz = .4f; }
            if (Keys.A == e.KeyCode ) { saz = -.4f; }

            if (Keys.E == e.KeyCode) { sax = -.4f; }
            if (Keys.Q == e.KeyCode) { sax = .4f; }

            if (Keys.R == e.KeyCode) { say = .4f; }
            if (Keys.F == e.KeyCode) { say = -.4f; }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            x += sx;y += sy; z += sz;
            ax += sax; ay += say; az += saz; 

            sax = Clamp(sax, -5, 5); say = Clamp(say, -5, 5); saz = Clamp(saz, -5, 5);
            sx = Clamp(sx, -10, 10); sy = Clamp(sy, -10, 10); sz = Clamp(sz, -10, 10);
            x = Clamp(x, -30, 30); y = Clamp(y, -30, 30); z = Clamp(z, -30, 30);
            ax = Clamp(ax, -10, 10); ay = Clamp(ay, -10, 10) ; az = Clamp(az, -10, 10);
            
            //gp.Reset();
            //tBrush.ResetTransform();
            tri1.Reset();
            tri1.Pos += new Vector3(x, y, z);
            tri1.Rotation += new Vector3(ax, ay, az);
            tri1.Update();
            PointF[] p1 = tri1.GetPoints(proj,this.Size);
            //gp.AddPolygon(p1);
            //gp.CloseFigure();
            //e.Graphics.FillPath(tBrush,gp);
            e.Graphics.FillPolygon(Brushes.Wheat, p1);
            x *= .96f; y *= .96f; z *= .7f;
            ax *= .92f; ay *= .92f; az *= .92f; 
        }
            
        private void timer1_Tick(object sender, EventArgs e)
        { this.Refresh(); }        
    }
}
