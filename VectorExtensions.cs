/*
Made by Alex McCulloch

In this class I extended my custom made Vector Math Library
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGameEngine
{
    static class VectorExtensions
    {
        public static double Length(this Vector2 v)
         => Math.Sqrt(v.X * v.X) + Math.Sqrt(v.Y * v.Y); 
           
        public static Vector2 Unit(this Vector2 v)
        { 
            double length = v.Length(); 
            return (0 < length) ? v / length :  v; 
        }

        public static Vector2 Rotate_Vector(this Vector2 v, double degrees)
        {
            double
                radian = degrees.degrees_to_radians(),
                sine = Math.Sin(radian),
                cosine = Math.Cos(radian);

            return 
                new Vector2(
                    v.X*cosine-v.Y*sine,
                    v.X*sine+v.Y*cosine
                );
        }
        public static double degrees_to_radians(this double degrees)
            => degrees * Math.PI/180.0;
        public static double radian_to_degrees(this double radian)
            => radian * 180.0 / Math.PI;

        public static Vector2 project(this Vector2 project, Vector2 onto)
        { 
            double d = onto * onto;
            if (0 < d) { 
                double dp = project * onto; 
                return onto * dp / d; 
            }
            else return onto;
        }

        
        public static bool Points_Collide(this Vector2 a, Vector2 b)
            => a.X.Equals(b.X) && a.Y.Equals(b.Y);

        public static Vector2 rotate_vector_90(this Vector2 v)
            => new Vector2(-v.Y,v.X);
        public static bool parallel_vectors(this Vector2 a, Vector2 b)
            => (0.0).Equals(a.rotate_vector_90() * b);
        public static bool equal_vectors(this Vector2 a, Vector2 b)
            => a.X.Equals(b.X) && b.Y.Equals(b.Y);
        


        public static Vector4 Down(this Vector4 v)
            => new Vector4(0, 1, 0, 1);
        public static List<Vector4> AddDown(this List<Vector4> lv)
        { lv.Add(new Vector4(0, 1, 0, 1)); return lv; }
        public static Vector4 Up(this Vector4 v)
            => new Vector4(0, -1, 0, 1);
        public static List<Vector4> AddUp(this List<Vector4> lv)
        { lv.Add(new Vector4(0, -1, 0, 1)); return lv; }
        public static Vector4 Left(this Vector4 v)
            => new Vector4(-1,0,0,1);
        public static List<Vector4> AddLeft(this List<Vector4> lv)
        { lv.Add(new Vector4(-1, 0, 0, 1)); return lv; }
        public static Vector4 Right(this Vector4 v)
            => new Vector4(1, 0, 0, 1);
        public static List<Vector4> AddRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, 0, 0, 1)); return lv; }
        public static Vector4 BottomLeft(this Vector4 v)
            => new Vector4(-1, 1, 0, 1);
        public static List<Vector4> AddBottomLeft(this List<Vector4> lv)
        { lv.Add(new Vector4(-1, 1, 0, 1)); return lv; }
        public static Vector4 BottomRight(this Vector4 v)
            => new Vector4(1,1,0,1);
        public static List<Vector4> AddBottomRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, 1, 0, 1)); return lv; }
        public static Vector4 TopLeft(this Vector4 v)
            => new Vector4(-1, -1, 0, 1);
        public static List<Vector4> AddTopLeft(this List<Vector4> lv)
        { lv.Add(new Vector4(-1, -1, 0, 1)); return lv; }
        public static Vector4 TopRight(this Vector4 v)
            => new Vector4(1, -1, 0, 1);
        public static List<Vector4> AddTopRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, -1, 0, 1)); return lv; }
        public static Vector4 Forward(this Vector4 v)
            => new Vector4(0, 0, 1, 1);
        public static List<Vector4> AddForward(this List<Vector4> lv)
        { lv.Add(new Vector4(0, 0, 1, 1));return lv;  }
        public static Vector4 ForwardLeft(this Vector4 v)
            => new Vector4(-1, 0, 1, 1);
        public static List<Vector4> AddForwardLeft(this List<Vector4> lv)
        { lv.Add(new Vector4(-1, 0, 1, 1));return lv; }
        public static Vector4 ForwardRight(this Vector4 v)
            => new Vector4(1, 0, 1, 1);
        public static List<Vector4> AddForwardRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, 0, 1, 1));return lv; }
        public static Vector4 ForwardUp(this Vector4 v)
            => new Vector4(0, -1, 1, 1);
        public static List<Vector4> AddForwardUp(this List<Vector4> lv)
        { lv.Add(new Vector4(0,-1,1,1)); return lv; }
        public static Vector4 ForwardUpLeft(this Vector4 v)
            => new Vector4(-1, -1, 1, 1);
        public static List<Vector4> AddForwardUpLeft(this List<Vector4> lv)
        { lv.Add(new Vector4(-1, -1, 1, 1)); return lv; }
        public static Vector4 ForwardUpRight(this Vector4 v)
            => new Vector4(1, -1, 1, 1);
        public static List<Vector4> AddForwardUpRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, -1, 1, 1)); return lv; }
        public static Vector4 ForwardDown(this Vector4 v)
            => new Vector4(0, 1, 1, 1);
        public static List<Vector4> AddForwardDown(this List<Vector4> lv)
        { lv.Add(new Vector4(0, 1, 1, 1));return lv; }
        public static Vector4 ForwardDownLeft(this Vector4 v)
            => new Vector4(-1,1,1,1);
        public static List<Vector4> AddForwardDownLeft(this List<Vector4> lv)
        { lv.Add(new Vector4(-1, 1, 1, 1));return lv; }
        public static Vector4 ForwardDownRight(this Vector4 v)
            => new Vector4(1, 1, 1, 1);
        public static List<Vector4> AddForwardDownRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, 1, 1, 1)); return lv; }
        public static Vector4 Backward(this Vector4 v)
            => new Vector4(0, 0, -1, 1);
        public static List<Vector4> AddBackward(this List<Vector4> lv)
        { lv.Add(new Vector4(0, 0, -1, 1));return lv; }
        public static Vector4 BackwardUp(this Vector4 v)
            => new Vector4(0,-1,-1,1);
        public static List<Vector4> AddBackwardUp(this List<Vector4> lv)
        { lv.Add(new Vector4(0, -1, -1, 1)); return lv; }
        public static Vector4 BackwardUpLeft(this Vector4 v)
            => new Vector4(-1,-1,-1,1);
        public static List<Vector4> AddBackwardUpLeft(this List<Vector4> lv) 
        { lv.Add(new Vector4(-1, -1, -1, 1));return lv; }
        public static Vector4 BackwardUpRight(this Vector4 v)
            => new Vector4(1, -1, -1, 1);
        public static List<Vector4> AddBackwardUpRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, -1, -1, 1));return lv; }
        public static Vector4 BackwardDown(this Vector4 v)
            => new Vector4(0, 1, -1, 1);
        public static List<Vector4> AddBackwardDown(this List<Vector4> lv)
        { lv.Add(new Vector4(0, 1, -1, 1)); return lv; }
        public static Vector4 BackwardDownLeft(this Vector4 v)
            => new Vector4(-1, 1, -1, 1);
        public static List<Vector4> AddBackwardDownLeft(this List<Vector4> lv)
        { lv.Add(new Vector4(-1, 1, -1, 1));return lv; }
        public static Vector4 BackwardDownRight(this Vector4 v)
            => new Vector4(1, 1, -1, 1);
        public static List<Vector4> AddBackwardDownRight(this List<Vector4> lv)
        { lv.Add(new Vector4(1, 1, -1, 1));return lv; }
        public static Vector4 SwapXY(this Vector4 v)
            => new Vector4(v.Y, v.X, v.Z, v.W);
        public static Vector4 SwapXZ(this Vector4 v)
            => new Vector4(v.Z, v.Y, v.X, v.W);
        public static Vector4 SwapYZ(this Vector4 v)
            => new Vector4(v.X, v.Z, v.X, v.W);
    }
}
