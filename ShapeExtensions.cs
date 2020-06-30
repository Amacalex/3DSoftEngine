using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    static class ShapeExtensions
    {
        public static bool Rectangles_Collide(this Rectangle2 a, Rectangle2 b)
        {
            Func<double, double, double, double, bool> overlapping
                = (minA, maxA, minB, maxB) =>
                    minB <= maxA && minA <= maxB;
            double
                aLeft = a.Origin.X,
                aRight = aLeft + a.Size.X,
                bLeft = b.Origin.X,
                bRight = bLeft + b.Size.X,
                aBottom = a.Origin.Y,
                aTop = aBottom + a.Size.Y,
                bBottom = b.Origin.Y,
                bTop = bBottom + b.Size.Y;

            return
                overlapping(aLeft, aRight, bLeft, bRight)
                && overlapping(aBottom, aTop, bBottom, bTop);
        }

        public static bool Circles_Collide(this Circle2 a, Circle2 b)
            => (a.Center - b.Center).Length() <= a.Radius + b.Radius;
        public static bool equivalent_lines(this Line2 a, Line2 b)
            => !a.Dir.parallel_vectors(b.Dir) ?
            true : (a.Base - b.Base).parallel_vectors(a.Dir);
        public static bool lines_collide(this Line2 a, Line2 b)
            => a.Dir.parallel_vectors(b.Dir) ? a.equivalent_lines(b) : true;
        public static bool on_one_side(this Line2 a, Segment2 s)
            => (
                    (a.Dir.rotate_vector_90() * (s.Point1 - a.Base))
                    * (a.Dir.rotate_vector_90() * (s.Point2 - a.Base))
               ) > 0;
        public static Range sort_range(this Range r)
            => (r.Min < r.Max) ? new Range(r.Max, r.Min) : r;

        public static Range project_segment(this Segment2 s, Vector2 onto)
            =>(
                new Range(onto.Unit() * s.Point1,
                onto.Unit() * s.Point2)
            ).sort_range();
        public static bool overlapping_ranges(this Range a, Range b)
        {
            Func<double, double, double, double, bool> overlapping
                = (minA, maxA, minB, maxB) =>
                    minB <= maxA && minA <= maxB;
            return overlapping(a.Min, a.Max, b.Min, b.Max);
        }
        public static bool segments_collide(this Segment2 a, Segment2 b)
        {

            Line2 
                axisA = new Line2(a.Point1, a.Point2 - a.Point1);

            if (axisA.on_one_side(b))
                return false; 

            Line2 
                axisB = new Line2(b.Point1, b.Point2 - b.Point1);

            if (axisB.on_one_side(a))
                return false; 

            if (axisA.Dir.parallel_vectors(axisB.Dir))
            {
                return
                    a.project_segment(axisA.Dir)
                    .overlapping_ranges(b.project_segment(axisA.Dir));
            }
            else
                return false;
        }

        public static Range range_hull(this Range a, Range b)
         => new Range(
                a.Min < b.Min ? a.Min : b.Min,
                b.Max > b.Max ? a.Max : b.Max
             );

        public static Segment2 oriented_rectangle_edge(
            this OrientedRectangle2 r, int nr)
        {
            Vector2 
                a = r.HalfExtend,
                b = r.HalfExtend;
            switch(nr % 4)
            {
                case 0:
                    a.X = -a.X;
                    break;
                case 1:
                    b.Y = -b.Y;
                    break;
                case 2:
                    a.Y = -a.Y;
                    break;
                default:
                    a = -a;
                    b.X = -b.X;
                    break;
            }

            a = a.Rotate_Vector(r.Rotation);
            a += r.Center;
            b = b.Rotate_Vector(r.Rotation);
            b += r.Center;
            return 
                new Segment2(a, b);
        }

        public static bool seperating_axis_for_oriented_rectangle(
            this Segment2 axis, OrientedRectangle2 r)
        {
            Segment2 
                rEdge0 = r.oriented_rectangle_edge(0),
                rEdge2 = r.oriented_rectangle_edge(2);
            Vector2 
                n = axis.Point1 - axis.Point2;
            Range
                axisRange = axis.project_segment(n),
                r0Range = rEdge0.project_segment(n),
                r2Range = rEdge2.project_segment(n),
                rProjection = r0Range.range_hull(r2Range);
            return !axisRange.overlapping_ranges(rProjection);
        }

        public static bool Oriented_Rectangles_Collide(
            OrientedRectangle2 a, OrientedRectangle2 b)
        {
            Segment2 edge = a.oriented_rectangle_edge(0);

            if (edge.seperating_axis_for_oriented_rectangle(b))
                return false;
            else {
                edge = a.oriented_rectangle_edge(1);

                if (edge.seperating_axis_for_oriented_rectangle(b))
                    return false;
                else
                {
                    edge = b.oriented_rectangle_edge(0);

                    if (edge.seperating_axis_for_oriented_rectangle(a))
                        return false;
                    else
                    {
                        edge = b.oriented_rectangle_edge(1);
                        return !edge.seperating_axis_for_oriented_rectangle(a);
                    }
                }
            }
        }

        public static bool Circle_Point_Collide(this Circle2 c, Vector2 p)
            => (c.Center - p).Length() <= c.Radius;

        public static bool Circle_Line_Collide(this Circle2 c, Line2 l)
            => c.Circle_Point_Collide(l.Base + (c.Center-l.Base).project(l.Dir));
        
        public static bool Circle_Segment_Collide(this Circle2 c, Segment2 s)
        {
            if (c.Circle_Point_Collide(s.Point1)) return true;
            else if (c.Circle_Point_Collide(s.Point2)) return true;

            Vector2 d = s.Point2 - s.Point1;
            Vector2 p = (c.Center-s.Point1).project(d);

            return
                c.Circle_Point_Collide(s.Point1+p)
                && p.Length() <= d.Length()
                && 0 <= p * d;
        }

        public static double clamp_on_range(this double x, double min, double max)
        {
            if (x < min) return min;
            else if (max < x) return max;
            else return x;
        }

        public static Vector2 clamp_on_rectangle(this Vector2 p, Rectangle2 r)        
            => new Vector2(
                p.X.clamp_on_range(r.Origin.X, r.Origin.X + r.Size.Y),
                p.Y.clamp_on_range(r.Origin.Y, r.Origin.Y + r.Size.Y));

        public static bool circle_rectangle_collide(this Circle2 c, Rectangle2 r)
            => c.Circle_Point_Collide(c.Center.clamp_on_rectangle(r));

        public static bool circle_oriented_rectangle_collide(
            this Circle2 c, OrientedRectangle2 r)
        {
            Rectangle2 lr = new Rectangle2(new Vector2(0, 0), r.HalfExtend * 2);
            Circle2 lc = new Circle2(new Vector2(0, 0), c.Radius);
            
            lc.Center = (c.Center - r.Center)
                .Rotate_Vector(-r.Rotation) 
                + r.HalfExtend;

            return lc.circle_rectangle_collide(lr);
        }

        public static bool point_rectangle_collide(this Vector2 p, Rectangle2 r)
        {
            double
                left = r.Origin.X,
                right = left + r.Size.X,
                bottom = r.Origin.Y,
                top = bottom + r.Size.Y;

            return 
                left <= p.X 
                && 
                bottom <= p.Y 
                && 
                p.X <= right 
                && 
                p.Y <= top;
        }

        public static bool line_rectangle_collide(this Line2 l, Rectangle2 r)
        {
            Vector2 n = l.Dir.rotate_vector_90();
            double dp1, dp2, dp3, dp4;
            Vector2
                c1 = r.Origin,
                c2 = c1 + r.Size,
                c3 = new Vector2(c2.X, c1.Y),
                c4 = new Vector2(c1.X, c2.Y);

            c1 -= l.Base;
            c2 -= l.Base;
            c3 -= l.Base;
            c4 -= l.Base;

            dp1 = n * c1;
            dp2 = n * c2;
            dp3 = n * c3;
            dp4 = n * c4;

            return
                (dp1 * dp2 <= 0)
                ||
                (dp2 * dp3 <= 0)
                ||
                (dp3 * dp4 <= 0);
        }
        
        public static bool rectangle_segment_collide(this Rectangle2 r, Segment2 s)
        {
            Line2 sLine = new Line2(s.Point1, s.Point2 - s.Point1);
            if (!sLine.line_rectangle_collide(r)) return false;

            Range 
                rRange = new Range(r.Origin.X, r.Origin.X+r.Size.X),
                sRange = new Range(s.Point1.X, s.Point2.X);
            
            sRange = sRange.sort_range();
            if (!rRange.overlapping_ranges(sRange)) return false;

            rRange.Min = r.Origin.Y;
            rRange.Max = r.Origin.Y + r.Size.Y;
            sRange.Min = s.Point1.Y;
            sRange.Max = s.Point2.Y;
            sRange = sRange.sort_range();
            return rRange.overlapping_ranges(sRange);
        }

        public static Vector2 rectangle_corner(this Rectangle2 r, int nr)
        {
            Vector2 corner = r.Origin;
            switch(nr % 4)
            {
                case 0:
                    corner.X += r.Size.X;
                    break;
                case 1:
                    corner = corner + r.Size;
                    break;
                case 2:
                    corner.Y += r.Size.Y;
                    break;
                default:
                    /*corner = r.origin;*/
                    break;
            }
            return corner;
        }

        public static bool seperating_axis_for_rectangle(
            this Segment2 axis, Rectangle2 r)
        {
            Vector2 
                n = axis.Point1 - axis.Point2;

            Segment2
                rEdgeA = new Segment2(
                    r.rectangle_corner(0),
                    r.rectangle_corner(1)
                    ),
                rEdgeB = new Segment2(
                    r.rectangle_corner(2),
                    r.rectangle_corner(3));
            Range
                rEdgeARange = rEdgeA.project_segment(n),
                rEdgeBRange = rEdgeB.project_segment(n),
                rProjection = rEdgeARange.range_hull(rEdgeBRange),
                axisRange = axis.project_segment(n);

            return !axisRange.overlapping_ranges(rProjection);
        }

        public static Vector2 oriented_rectangle_corner(
            this OrientedRectangle2 r, int nr)
        {
            Vector2
                c = r.HalfExtend;
            switch(nr % 4)
            {
                case 0:
                    c.X = -c.X;
                    break;
                case 1:
                    /*c=r.haldExtend*/
                    break;
                case 2:
                    c.Y = -c.Y;
                    break;
                case 3:
                    c.Y = -c.Y;
                    break;
                default:
                    c = -c;
                    break;
            }
            c = c.Rotate_Vector(r.Rotation);
            return c + r.Center;
        }

        public static double Min(this double a, double b)
            => a < b ? a : b;
        public static double Max(this double a, double b)
            => a > b ? a : b;

        public static Rectangle2 enlarge_rectangle_point(
            this Rectangle2 r, Vector2 p)
        {
            Rectangle2
                enlarged = new Rectangle2(
                    new Vector2(
                        r.Origin.X.Min(p.X),
                        r.Origin.Y.Min(p.Y)), 
                    new Vector2(
                        (r.Origin.X + r.Size.Y).Max(p.Y),
                        (r.Origin.Y+r.Size.Y).Max(p.Y))
                    );

            enlarged.Size = enlarged.Size - enlarged.Origin;
            return enlarged;
        }

        public static bool oriented_rectangle_collide(
            this OrientedRectangle2 r, Rectangle2 aar)
        {
            Rectangle2
                orHull = r.oriented_rectangle_hull();

            if (!orHull.Rectangles_Collide(aar)) 
                return false;

            Segment2
                edge = r.oriented_rectangle_edge(0);

            if (edge.seperating_axis_for_rectangle(aar)) 
                return false;

            edge = r.oriented_rectangle_edge(1);

            return 
                !edge.seperating_axis_for_rectangle(aar);
        }

        public 
            static 
            bool line_point_collide(this Line2 l, Vector2 p)
         => l.Base.Points_Collide(p) 
            ?
            true 
            :
            (p-l.Base).parallel_vectors(l.Dir);
        

        public 
            static 
            bool point_segment_collide(this Vector2 p, Segment2 s)
        {
            Vector2
                d = s.Point2 - s.Point1,
                lp = p - s.Point1,
                pr = lp.project(d);
            return
                lp.equal_vectors(pr)
                &&
                pr.Length() <= d.Length()
                &&
                0 <= pr * d;
        }

        public static bool oriented_rectangle_point_collide 
            (this OrientedRectangle2 r, Vector2 p)

        {
            Rectangle2
                lr = new Rectangle2(
                    new Vector2(0,0),
                    r.HalfExtend*2
                    );

            Vector2
                lp = p - r.Center;
            lp.Rotate_Vector(-r.Rotation);
            lp += r.HalfExtend;

            return lp.point_rectangle_collide(lr);
        }

        public static bool line_segment_collide
            (this Line2 l, Segment2 s)
            => !l.on_one_side(s);
        public static bool line_oriented_rectangle_collide
            ( this Line2 l, OrientedRectangle2 r )
        {
            Rectangle2
                lr = new Rectangle2(
                    new Vector2(0, 0),
                    r.HalfExtend * 2
                    );
            
                return new Line2
                (
                        (l.Base-r.Center).Rotate_Vector(-r.Rotation)+r.HalfExtend,
                        l.Dir.Rotate_Vector(r.Rotation)       
                ).line_oriented_rectangle_collide(r);
        }

        public static bool oriented_segment_collide
            ( this OrientedRectangle2 r, Segment2 s)
        {
            Rectangle2
                lr = new Rectangle2(
                    new Vector2(0,0),
                    r.HalfExtend * 2
                    );

            Segment2
                ls = new Segment2(
                    (s.Point1 - r.Center).Rotate_Vector(r.Rotation) + r.HalfExtend,
                    (s.Point2-r.Center).Rotate_Vector(-r.Rotation) + r.HalfExtend
                    );

            return lr.rectangle_segment_collide(ls);
        }

        public static Rectangle2 oriented_rectangle_hull
            (this OrientedRectangle2 r)
        {
            Rectangle2
                h = new Rectangle2(
                    r.Center,
                    new Vector2(0,0)
                    );
            Vector2 
                corner;

            for (int nr = 0; nr < 4; nr++)
            {
                corner = r.oriented_rectangle_corner(nr);
                h = h.enlarge_rectangle_point(corner);
            }
            return h;
        }

        public static Circle2 oriented_rectangle_circle_hull
            (this OrientedRectangle2 r)
            => new Circle2(
                    r.Center,
                    r.HalfExtend.Length()
                    );

        public static Rectangle2 enlarge_rectangle_rectangle
            (this Rectangle2 r, Rectangle2 extender)
        {
            Vector2
                maxCorner = extender.Origin + extender.Size;
            Rectangle2
                enlarged = r.enlarge_rectangle_point(maxCorner);
            return 
                enlarged.enlarge_rectangle_point(extender.Origin);
        }

    }
}
