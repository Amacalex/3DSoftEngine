using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace _2DGameEngine
{
    struct Line2
    {
        Vector2 b;
        Vector2 dir;
        public Line2(Vector2 b = default, Vector2 dir = default)
        { this.b = b; this.dir = dir; }
        public Vector2 Base { get => b; set => b = value; }
        public Vector2 Dir { get => dir; set => dir = value; }
    }

    struct Segment2
    {
        Vector2 point1;
        Vector2 point2;
        public Segment2(Vector2 p1 = default, Vector2 p2 = default)
        { this.point1 = p1; this.point2 = p2; }
        public Vector2 Point1 { get => point1; set=> point1 = value; }
        public Vector2 Point2 { get => point2; set => point2 = value; }
    }
    struct Circle2
    {
        Vector2 center;
        double radius;
        public Circle2(Vector2 center=default, double radius = 0)
        { this.center = center; this.radius = radius; }
        public Vector2 Center { get => center; set => center = value; }
        public double Radius { get => radius; set => radius = value; }
    }
    struct Rectangle2
    {
        Vector2 origin;
        Vector2 size;
        public Rectangle2(Vector2 origin=default, Vector2 size = default)
        { this.origin = origin; this.size = size; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public Vector2 Size { get => size; set => size = value; }
    }
    
    struct OrientedRectangle2
    {
        Vector2 center;
        Vector2 halfExtend;
        double rotation;
        public OrientedRectangle2(Vector2 center=default, Vector2 halfExtend=default, double rotation=0)
        { this.center = center; this.halfExtend = halfExtend; this.rotation = rotation; }
        public Vector2 Center { get => center; set => center = value; }
        public Vector2 HalfExtend { get => halfExtend; set => halfExtend = value; }
        public double Rotation { get => rotation; set => rotation = value; }
    }

    struct Range
    {
        double min, max;
        public Range(double min=0f,double max=0f)
        { this.min = min; this.max = max; }
        public double Min { get => min; set => min = value; }
        public double Max { get => max; set => max = value; }
    }

    class Triangle
    {
        Vector3 pos;
        Vector3 rotation;
        Vector2 scale;
        Point center;
        Vector4 p1,p2,p3,w1,w2,w3;
        
        public Triangle(Vector4 p1, Vector4 p2, Vector4 p3)
        {
            this.p1 = p1; this.p2 = p2; this.p3 = p3;
            Reset();
            pos = new Vector3(0, 0, 0);
            rotation = new Vector3(0, 0, 0);
            scale = new Vector3(1, 1, 1);
        }

        public void resetUpdate()
        { Reset(); Update(); }

        public void Reset()
        {
            this.w1 = new Vector4(p1.X, p1.Y, p1.Z, p1.W);
            this.w2 = new Vector4(p2.X, p2.Y, p2.Z, p2.W);
            this.w3 = new Vector4(p3.X, p3.Y, p3.Z, p3.W);
        }
        public void Update()
        {
            Rotation_X_3D rx = new Rotation_X_3D(rotation.X);
            Rotation_Y_3D ry = new Rotation_Y_3D(rotation.Y);
            Rotate_Z_3D rz = new Rotate_Z_3D(rotation.Z);
            
            w1 *= rx; w2 *= rx; w3 *= rx;
            w1 *= ry; w2 *= ry; w3 *= ry;
            w1 *= rz; w2 *= rz; w3 *= rz;

            Scale3D s = new Scale3D(scale.X, scale.Y, 1);
            w1 *= s; w2 *= s; w3 *= s;

            Translation3D t = new Translation3D(pos.X, pos.Y, pos.Z);
            w1 *= t; w2 *= t; w3 *= t;

        }

        public PointF GetCenter(Matrix4X4 proj)
        {
            PointF p1 = GetPoint1(proj);
            PointF p2 = GetPoint1(proj);
            PointF p3 = GetPoint1(proj);

            return new PointF(
                    (p1.X+p2.X+p3.X)/3,
                    (p1.Y+p2.Y+p3.Y)/3
                    );
        }

        public PointF GetPoint1(Matrix4X4 proj)
        {
            Vector4 p = new Vector4(w1.X, w1.Y, w1.Z, w1.W);
            p *= proj;
            return new PointF((float)(p.X / p.Z), (float)(p.Y / p.Z));
        }
        public PointF GetPoint2(Matrix4X4 proj)
        {
            Vector4 p = new Vector4(w2.X, w2.Y, w2.Z, w2.W);
            p *= proj;
            return new PointF((float)(p.X / p.Z), (float)(p.Y / p.Z));
        }
        public PointF GetPoint3(Matrix4X4 proj)
        {
            Vector4 p = new Vector4(w3.X, w3.Y, w3.Z, w3.W);
            p *= proj;
            return new PointF((float)(p.X / p.Z), (float)(p.Y / p.Z));
        }

        public PointF[] GetPoints(Matrix4X4 proj, Size screen)
        {
            Size half = new Size(screen.Width / 2, screen.Height / 2);
            return new PointF[3]
                  { GetPoint1(proj)+half, GetPoint2(proj)+half, GetPoint3(proj)+half };
        }
    
        public Vector4 P1
        { get => p1; set => p1 = value; }

        public Vector4 P2
        { get => p2; set => p2 = value; }

        public Vector4 P3
        { get => p3; set => p3 = value; }

        public Vector4[] Vertices => new Vector4[3] { P1, P2, P3 };

        public Vector3 Pos
        {
            get => pos;
            set => pos = value; 
        }

        public Vector3 Rotation
        {
            get => rotation;
            set => rotation = value;
        }
        
        public Vector2 Scale
        {
            get => scale;
            set =>  scale = value;
        }
    }

    class Rectangle
    {        
        Vector3 pos;
        Vector2 scale;
        Vector3 rotation;

        public Rectangle(Vector3 pos, Vector2 scale, Vector3 rotation)
        { this.scale = scale;this.pos = pos; this.rotation = rotation; }

        public Vector3 Pos { get => pos; set => pos = value; }
        public Vector2 Scale { get => scale; set => scale = value; }
        public Vector3 Rotation { get => rotation; set => rotation = value; }
    }
}
