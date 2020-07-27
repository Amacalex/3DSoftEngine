using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGameEngine
{
    interface IUpdate { void Update(); }
    delegate void Update();
    delegate void Draw(object sender, PaintEventArgs e);
    static class GameLoop 
    {
        static event Update OnUpdate;
        static event Draw OnDraw;
        public static void Init()
        {
            OnUpdate = null;
            OnDraw = null;
        }

        public static event Update onUpdate { add => OnUpdate += value; remove => OnUpdate -= value; }

        public static event Draw onDraw { add => OnDraw += value; remove => onDraw -= value; }

        public static void Update(object sender, EventArgs e)
        {
            OnUpdate?.Invoke();
        }

        public static void Draw(object sender, PaintEventArgs e)
        {
            OnDraw?.Invoke(sender, e);
        }
    }
}
