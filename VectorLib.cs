/*
Made by Alex McCulloch

In this class I made my own custom made Vector Math Library

*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace _2DGameEngine
{
    class Vector2
    {
        double x, y;
        public Vector2(double x, double y)
        { this.x = x; this.y = y; }
        public double X { get => this.x; set => this.x = value; }
        public double Y { get => this.y; set => this.y = value; }
        public static Vector2 operator +(Vector2 a, Vector2 b)
            => new Vector2(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b)
            => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator -(Vector2 a, double b)
            => new Vector2(a.X - b, a.Y - b);
        public static Vector2 operator -(double a, Vector2 b)
            => new Vector2(a - b.X, a - b.Y);
        public static Vector2 operator -(Vector2 a)
            => new Vector2(-a.X, -a.Y);
        public static double operator *(Vector2 a, Vector2 b)
            => a.X * b.X + a.Y * b.Y;
        public static Vector2 operator *(Vector2 a, double b)
            => new Vector2(a.X * b, a.Y * b);
        public static Vector2 operator *(double a, Vector2 b)
            => new Vector2(a * b.X, a * b.Y);
        public static Vector2 operator /(double a, Vector2 b)
            => new Vector2(a / b.X, a / b.Y);
        public static Vector2 operator /(Vector2 a, double b)
            => new Vector2(a.X / b, a.Y / b);
        
    }
    class Vector3 : Vector2
    {
        double z;
        public Vector3(double x, double y, double z) : base(x,y)
            => this.z = z;
        public double Z { get => this.z; set => this.z = value; }
        public static Vector3 operator +(Vector3 a, Vector3 b)
            => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3 operator -(Vector3 a, Vector3 b)
            => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }

    class Vector4 : Vector3
    {
        double w;
        public Vector4(double x, double y, double z, double w) : base(x,y,z)
            => this.w = w;
        public double W { get => this.w; set => this.w = value; }
        public static double operator *(Vector4 a, Vector4 b)
            => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        public static Vector4 operator *(double a, Vector4 b)
            => new Vector4(a*b.X, a*b.Y, a*b.Z, a*b.W);
        public static Vector4 operator *(Vector4 a, double b)
            => new Vector4(a.X*b, a.Y*b, a.Z*b, a.W*b);
    }

    class Matrix4X4
    {
        Vector4 r1;
        Vector4 r2;
        Vector4 r3;
        Vector4 r4;
        public Matrix4X4(Vector4 r1, Vector4 r2, Vector4 r3, Vector4 r4)
        {
            this.r1 = r1; this.r2 = r2; this.r3 = r3; this.r4 = r4;
        }
        public Vector4 R1 { get => r1; set => r1 = value; }
        public Vector4 R2 { get => r2; set => r2 = value; }
        public Vector4 R3 { get => r3; set => r3 = value; }
        public Vector4 R4 { get => r4; set => r4 = value; }
        public Vector4 C1
        {
            get => new Vector4(R1.X, R2.X, R3.X, R4.X);
            set
            {
                R1.X = value.X; R2.X = value.Y;
                R3.X = value.Z; R4.X = value.W;
            }
        }
        public Vector4 C2
        {
            get => new Vector4(R1.Y, R2.Y, R3.Y, R4.Y);
            set
            {
                R1.Y = value.X; R2.Y = value.Y;
                R3.Y = value.Z; R4.Y = value.W;
            }
        }
        public Vector4 C3
        {
            get => new Vector4(R1.Z, R2.Z, R3.Z, R4.Z);
            set
            {
                R1.Z = value.X; R2.Z = value.Y;
                R3.Z = value.Z; R4.Z = value.W;
            }
        }
        public Vector4 C4
        {
            get => new Vector4(R1.W, R2.W, R3.W, R4.W);
            set
            {
                R1.W = value.X; R2.W = value.Y;
                R3.W = value.Z; R4.W = value.W;
            }
        }
        public static Matrix4X4 operator *(Matrix4X4 a, Matrix4X4 b)
        {
            Vector4[] rows = new Vector4[4]
            {
                new Vector4(a.R1 * b.C1, a.R1 * b.C2, a.R1 * b.C3, a.R1 * b.C4),
                new Vector4(a.R2 * b.C1, a.R2 * b.C2, a.R3 * b.C3, a.R4 * b.C4),
                new Vector4(a.R3 * b.C1, a.R3 * b.C2, a.R3 * b.C3, a.R4 * b.C4),
                new Vector4(a.R4 * b.C1, a.R4 * b.C2, a.R4 * b.C3, a.R4 * b.C4)
            };

            return new Matrix4X4(rows[0], rows[1], rows[2], rows[3]);
        }

        public static Vector4 operator *(Vector4 a, Matrix4X4 b)
            => new Vector4(a * b.R1, a * b.R2, a * b.R3, a * b.R4);
        public static Vector4 operator *(Matrix4X4 a, Vector4 b)
            => new Vector4(a.R1 * b, a.R2 * b, a.R3 * b, a.R4 * b);
    }

    class Translation3D : Matrix4X4
    {
        public Translation3D(double x,double y, double z) 
            : base( new Vector4(1, 0, 0, x),
                  new Vector4(0, 1, 0, y),
                new Vector4(0, 0, 1, z), 
                new Vector4(0, 0, 0, 1)) 
        {}
    } 

    class Scale3D : Matrix4X4
    {
        public Scale3D(double x, double y, double z)
            : base(new Vector4(x, 0, 0, 0),
                  new Vector4(0, y, 0, 0),
                  new Vector4(0, 0, z, 0),
                  new Vector4(0, 0, 0, 1))
        {}

        public void SetScale(double x, double y, double z)
        {
            this.R1 = new Vector4(x, 0, 0, 0);
            this.R2 = new Vector4(0, y, 0, 0);
            this.R3 = new Vector4(0, 0, z, 0);
            this.R4 = new Vector4(0, 0, 0, 1);
        }
        
    }

    class Rotation_X_3D : Matrix4X4
    {
        public Rotation_X_3D(double angle) :
            base(new Vector4(1,0,0,0),
                new Vector4(0,Math.Cos(angle*Math.PI/180),-Math.Sin(angle * Math.PI / 180),0),
                new Vector4(0,Math.Sin(angle * Math.PI / 180),Math.Cos(angle * Math.PI / 180),0),
                new Vector4(0,0,0,1))
        {}

        public void SetRotate_X(double angle)
        {
            this.R1 = new Vector4(1, 0, 0, 0);
            this.R2 = new Vector4(0, Math.Cos(angle * Math.PI / 180), -Math.Sin(angle * Math.PI/180), 0);
            this.R3 = new Vector4(0, Math.Sin(angle * Math.PI/180), Math.Cos(angle * Math.PI/180), 0);
            this.R4 = new Vector4(0, 0, 0, 1);
        }
    }

    class Rotation_Y_3D : Matrix4X4
    {
        public Rotation_Y_3D( double angle) :
            base(new Vector4(Math.Cos(angle * Math.PI/180), 0, Math.Sin(angle * Math.PI/180), 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(-Math.Sin(angle * Math.PI/180), 0, Math.Cos(angle * Math.PI/180), 0),
                new Vector4(0, 0, 0, 1))
        {}
        public void SetRotate_Y( double angle) 
        {
            this.R1 = new Vector4(Math.Cos(angle * Math.PI/180), 0, Math.Sin(angle * Math.PI/180), 0);
            this.R2 = new Vector4(0, 1, 0, 0);
            this.R3 = new Vector4(-Math.Sin(angle * Math.PI/180), 0, Math.Cos(angle * Math.PI/180), 0);
            this.R4 = new Vector4(0, 0, 0, 1);
        }
    }

    class Rotate_Z_3D : Matrix4X4
    {
        public Rotate_Z_3D(double angle)
            : base(new Vector4(Math.Cos(angle * Math.PI / 180), -Math.Sin(angle * Math.PI / 180), 0, 0),
                  new Vector4(Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180), 0, 0),
                  new Vector4(0, 0, 1, 0),
                  new Vector4(0, 0, 0, 1))
        { }
        public void SetRotate_Z(double angle)
        {
            this.R1 = new Vector4(Math.Cos(angle * Math.PI / 180), -Math.Sin(angle * Math.PI / 180), 0, 0);
            this.R2 = new Vector4(Math.Sin(angle * Math.PI / 180), Math.Cos(angle * Math.PI / 180), 0, 0);
            this.R3 = new Vector4(0, 0, 1, 0);
            this.R4 = new Vector4(0, 0, 0, 1);
        }
    }

    class Projection3D : Matrix4X4
    {
        public Projection3D(double width, double height, double fov, double zn, double zf)
            : base(
                  new Vector4((height / width) * Math.Atan2(fov, 2), 0, 0, 0),
                  new Vector4(0, Math.Atan2(fov, 2), 0, 0),
                  new Vector4(0, 0, (zf + zn) / (zf - zn), 1),
                  new Vector4(0, 0, (-zn * zf) / (zf - zn), 0)
                  )
                  { }

        public void SetProjection(double fovx, double fovy, double zf,double zn)
        {
            this.R1 = new Vector4(Math.Atan2(fovx, 2), 0, 0, 0);
            this.R2 = new Vector4(0, Math.Atan2(fovy, 2), 0, 0);
            this.R3 = new Vector4(0, 0, -((zf + zn) / (zf - zn)), -(2 * (zn * zf) / (zf - zn)));
            this.R4 = new Vector4(0, 0, -1, 0);
        }
    }

    
}
