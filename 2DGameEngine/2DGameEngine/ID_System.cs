using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    delegate void newID(Iid obj,string identity_name);
    delegate void setChildID(Iid child, Iid parent);
    static class ID_System
    {
        static int counter;
        static event newID NewID;
        static event setChildID SetChildID;
        public static void Init()
        {
            NewID += ID_System_AddID;
            SetChildID += ID_System_SetChildID;
        }

        private static void ID_System_SetChildID(Iid child, Iid parent)
            => child.ID = parent.ID;

        public static setChildID GetSetChildID => SetChildID;
        public static newID GetnewID => NewID;

        private static void ID_System_AddID(Iid obj, string identity_name)
        {
            counter += 1;
            obj.ID = identity_name + ": " + counter.ToString();
        }
    }
}
