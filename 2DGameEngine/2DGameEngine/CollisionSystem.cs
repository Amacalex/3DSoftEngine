using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    delegate bool Collision2D(ISolid g1, ISolid g2);
    class CollisionSystem 
    {
        public event Collision2D Collided;
        
        public CollisionSystem()
        {
            GameLoop.onUpdate += GameLoop_onUpdate;
            Collided += EventSystem_RegisterCollider;
        }

        private void GameLoop_onUpdate()
        {
                
        }

        private bool EventSystem_RegisterCollider(ISolid s1, ISolid s2)
        {
            if (s1 is Rectangle2 && s2 is Rectangle2)
            {
                Rectangle2
                    rect1 = (Rectangle2)s1,
                    rect2 = (Rectangle2)s2;
                
                return rect1.Rectangles_Collide(rect2); ;
            }
            else if(s1 is Circle2 && s2 is Circle2)
            {
                Circle2
                    circ1 = (Circle2)s1,
                    circ2 = (Circle2)s2;
                return circ1.Circles_Collide(circ2);
            }
            else if (s1 is Line2 && s2 is Line2)
            {
                Line2
                    line1 = (Line2)s1,
                    line2 = (Line2)s2;

                return line1.lines_collide(line2);
            }
            else if (s1 is Segment2 && s2 is Segment2)
            {
                Segment2
                    seg1 = (Segment2)s1,
                    seg2 = (Segment2)s2;
                return seg1.segments_collide(seg2);
            }
            else if(s1 is Rectangle2 && s2 is Circle2)
            {
                Rectangle2 rect1 = (Rectangle2)s1;
                Circle2 circ1 = (Circle2)s2;
                return circ1.circle_rectangle_collide(rect1);
            }
            else if (s1 is Circle2 && s2 is Rectangle2)
            {
                Circle2 circ1 = (Circle2)s1;
                Rectangle2 rect1 = (Rectangle2)s2;
                return circ1.circle_rectangle_collide(rect1);
            }
            else if (s1 is Rectangle2 && s2 is Line2)
            {
                Rectangle2 rect1 = (Rectangle2)s1;
                Line2 line1 = (Line2)s2;
                return line1.line_rectangle_collide(rect1);
            }
            else if(s1 is Line2 && s2 is Rectangle2)
            {
                Line2 line1 = (Line2)s1;
                Rectangle2 rect1 = (Rectangle2)s2;
                return line1.line_rectangle_collide(rect1);
            }
            else if (s1 is Circle2 && s2 is Line2)
            {
                Circle2 circ1 = (Circle2)s1;
                Line2 line1 = (Line2)s2;
                return circ1.Circle_Line_Collide(line1);
            }
            else if(s1 is Line2 && s2 is Circle2)
            {
                Line2 line1 = (Line2)s1;
                Circle2 circ1 = (Circle2)s2;
                return circ1.Circle_Line_Collide(line1);
            }
            else if (s1 is Segment2 && s2 is Rectangle2)
            {
                Segment2 seg1 = (Segment2)s1;
                Rectangle2 rect1 = (Rectangle2)s2;
                return rect1.rectangle_segment_collide(seg1);
            }
            else if(s1 is Rectangle2 && s2 is Segment2)
            {
                Rectangle2 rect1 = (Rectangle2)s1;
                Segment2 seg1 = (Segment2)s2;
                return rect1.rectangle_segment_collide(seg1);
            }
            else if (s1 is Circle2 && s2 is Segment2)
            {
                Circle2 circ1 = (Circle2)s1;
                Segment2 seg1 = (Segment2)s2;
                return circ1.Circle_Segment_Collide(seg1);
            }
            else if(s1 is Segment2 && s2 is Circle2)
            {
                Segment2 seg1 = (Segment2)s1;
                Circle2 circ1 = (Circle2)s2;
                return circ1.Circle_Segment_Collide(seg1);
            }
            else if(s1 is Segment2 && s2 is Line2)
            {
                Segment2 seg1 = (Segment2)s1;
                Line2 line1 = (Line2)s2;
                return line1.line_segment_collide(seg1);
            }
            else if(s1 is Line2 && s2 is Segment2)
            {
                Line2 line1 = (Line2)s1;
                Segment2 seg1 = (Segment2)s2;
                return line1.line_segment_collide(seg1);
            }
            else
            {
                return false;
            }
        }

        
    }
}
