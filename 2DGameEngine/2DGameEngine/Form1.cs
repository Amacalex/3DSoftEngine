/* Form1.cs is formated only temporarily to test out the backing 
 * 
 * 3D graphics classes I made in the other files.
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGameEngine
{
    public partial class Form1 : Form
    {
        GameObject gameObject;
        Matrix4X4 proj;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            World.Initialize();
            this.Paint += GameLoop.Draw;
            this.timer1.Tick += GameLoop.Update;
            GameObject g_obj1 = new GameObject(new Vector2(0,0));
            g_obj1.Instantiate();
            RigidBody2D r = g_obj1.AddRigidBody2D();
            r.AddCollider(new Rectangle2(new Vector2(0, 0), new Vector2(10, 10)));

            timer1.Interval = 16;
            timer1.Start();
        }

        private double Clamp(double val, double min, double max)
        {
            if (val <= max && val >= min) return val;

            else if (val >= max) return max;

            else if (val <= min) return min;

            else return 0.0f;
        }
    
        private void timer1_Tick(object sender, EventArgs e)
        { this.Refresh(); }        
    }
}
