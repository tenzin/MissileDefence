using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MissileDefence
{
    public class Rect4
    {
        /// <summary>
        /// The data in the 4 points class called Rect4
        /// </summary>
        public Vector2[] point;

        /// <summary>
        /// Default constructor (0,0) (0,0) (0,0) (0,0)</summary>
        public Rect4()
        {
            point = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                point[i].X = 0;
                point[i].Y = 0;
            }
        }

        /// <summary>
        /// Construct from rectangle clockwise winding</summary>
        public Rect4(Rectangle r)
        {
            point = new Vector2[4];
            point[0].X = r.Left;
            point[0].Y = r.Top;

            point[1].X = r.Right;
            point[1].Y = r.Top;

            point[2].X = r.Right;
            point[2].Y = r.Bottom;

            point[3].X = r.Left;
            point[3].Y = r.Bottom;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="r"></param>
        public Rect4(Rect4 r) // Copy Constructor
        {
            point = new Vector2[4];
            point[0].X = r.point[0].X;
            point[0].Y = r.point[0].Y;

            point[1].X = r.point[1].X;
            point[1].Y = r.point[1].Y;

            point[2].X = r.point[2].X;
            point[2].Y = r.point[2].Y;

            point[3].X = r.point[3].X;
            point[3].Y = r.point[3].Y;
        }

        /// <summary>
        /// Rotates the rect4 by a given angle in radians
        /// </summary>
        /// <param name="centerOfRotation"></param>
        /// <param name="angleInRadians"></param>
        public void rotateRect(Vector2 centerOfRotation, float angleInRadians)
        {

            point[0] = rotatePoint(point[0], centerOfRotation, -angleInRadians);
            point[1] = rotatePoint(point[1], centerOfRotation, -angleInRadians);
            point[2] = rotatePoint(point[2], centerOfRotation, -angleInRadians);
            point[3] = rotatePoint(point[3], centerOfRotation, -angleInRadians);
        }

        /// <summary>
        /// Rotates the rect4 by a given angle in degrees
        /// </summary>
        /// <param name="centerOfRotation"></param>
        /// <param name="angleInDegrees"></param>
        public void rotateRectDeg(Vector2 centerOfRotation, float angleInDegrees)
        {
            rotateRect(centerOfRotation, angleInDegrees * (float)Math.PI / 180);
        }

        /// <summary>
        /// This returns an axis aligned bounding box based on the four corners of Rect4.
        /// The points should be a convex polygon, but this routine will work in all cases
        /// (note it can probably be done faster using the Max and Min functions but it deliberately this way so students can understand it)
        /// </summary>
        public Rectangle getAABoundingRect()
        {
            float Top = point[0].Y;
            float Left = point[0].X;
            float Bottom = point[0].Y;
            float Right = point[0].X;

            if (point[1].X < Left) Left = point[1].X;
            if (point[2].X < Left) Left = point[2].X;
            if (point[3].X < Left) Left = point[3].X;

            if (point[1].Y < Top) Top = point[1].Y;
            if (point[2].Y < Top) Top = point[2].Y;
            if (point[3].Y < Top) Top = point[3].Y;

            if (point[1].X > Right) Right = point[1].X;
            if (point[2].X > Right) Right = point[2].X;
            if (point[3].X > Right) Right = point[3].X;

            if (point[1].Y > Bottom) Bottom = point[1].Y;
            if (point[2].Y > Bottom) Bottom = point[2].Y;
            if (point[3].Y > Bottom) Bottom = point[3].Y;

            // now have bounds in Top, left bottomm and right - covert to rectangle

            return new Rectangle((int)Left, (int)Top, (int)(Right - Left), (int)(Bottom - Top));
        }

        /// <summary>
        /// Rotate a single point about an arbitay center radians
        /// </summary>
        /// <param name="point"></param>
        /// <param name="centerOfRotation"></param>
        /// <param name="angleInRadians"></param>
        /// <returns></returns>
        public static Vector2 rotatePoint(Vector2 point, Vector2 centerOfRotation, float angleInRadians)
        {
            float tmpx, tmpy, tx, ty; // more temporaries than we really need but its very clear how it works with them
            Vector2 retv; // new value

            /* set to origin */
            tmpx = point.X - centerOfRotation.X;
            tmpy = point.Y - centerOfRotation.Y;

            // apply rotate
            tx = (tmpy * (float)Math.Sin(angleInRadians)) + (tmpx * (float)Math.Cos(angleInRadians));
            ty = (tmpy * (float)Math.Cos(angleInRadians)) - (tmpx * (float)Math.Sin(angleInRadians));

            retv.X = tx + centerOfRotation.X;
            retv.Y = ty + centerOfRotation.Y;
            return retv;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)point[0].X, (int)point[0].Y, (int)(point[1].X - point[0].X), (int)(point[2].Y - point[0].X));
        }
    }
}
