using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    interface Iid { string ID { get; set; } }
    class GameObjectId
    {
        static int counter;
        string idName;
        
        public GameObjectId()
        {
            counter += 1;
            idName = "GamObject: " + counter.ToString();
        
                
        }

        public string ID { get => idName; }
    }
}
