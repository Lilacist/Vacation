/*---------------------------------------------
整理自用 C# WinForm 常用Ui组件
适用版本：.Net 4.6 (32、64位)
设计：Song Qiao Ke  
          Email: Qiaoke_Song@163.com
          QQ：2452243110
最后更新：2018.2.23
-----------------------------------------------*/

using System;
using System.Drawing;
using System.Windows.Forms;

public class Cube
{
    public int width = 0;
    public int height = 0;
    public int depth = 0;
    double xRotation = 0.0;
    double yRotation = 0.0;
    double zRotation = 0.0;
    Math3D.Camera camera1 = new Math3D.Camera();
    Math3D.Point3D cubeOrigin;
    public Point a, b, c, d;

    public double RotateX
    {
        get { return xRotation; }
        set { xRotation = value; }
    }

    public double RotateY
    {
        get { return yRotation; }
        set { yRotation = value; }
    }

    public double RotateZ
    {
        get { return zRotation; }
        set { zRotation = value; }
    }

    public Cube(int Width, int Height, int Depth)
    {
        width = Width;
        height = Height;
        depth = Depth;
        cubeOrigin = new Math3D.Point3D(width / 2, height / 2, depth / 2);
    }

    public static Rectangle getBounds(PointF[] points)
    {
        double left = points[0].X;
        double right = points[0].X;
        double top = points[0].Y;
        double bottom = points[0].Y;
        for (int i = 1; i < points.Length; i++)
        {
            if (points[i].X < left)
                left = points[i].X;
            if (points[i].X > right)
                right = points[i].X;
            if (points[i].Y < top)
                top = points[i].Y;
            if (points[i].Y > bottom)
                bottom = points[i].Y;
        }
        return new Rectangle(0, 0, (int)Math.Round(right - left), (int)Math.Round(bottom - top));
    }

    public void calcCube(Point drawOrigin)
    {
        PointF[] point3D = new PointF[24];
        Point tmpOrigin = new Point(0, 0);
        Math3D.Point3D point0 = new Math3D.Point3D(0, 0, 0);
        double zoom = (double)Screen.PrimaryScreen.Bounds.Width / 1.5;
        Math3D.Point3D[] cubePoints = fillCubeVertices(width, height, depth);
        Math3D.Point3D anchorPoint = (Math3D.Point3D)cubePoints[4];
        double cameraZ = -(((anchorPoint.X - cubeOrigin.X) * zoom) / cubeOrigin.X) + anchorPoint.Z;
        camera1.Position = new Math3D.Point3D(cubeOrigin.X, cubeOrigin.Y, cameraZ);
        cubePoints = Math3D.Translate(cubePoints, cubeOrigin, point0);
        cubePoints = Math3D.RotateX(cubePoints, xRotation);
        cubePoints = Math3D.RotateY(cubePoints, yRotation);
        cubePoints = Math3D.RotateZ(cubePoints, zRotation);
        cubePoints = Math3D.Translate(cubePoints, point0, cubeOrigin);
        Math3D.Point3D vec;
        for (int i = 0; i < point3D.Length; i++)
        {
            vec = cubePoints[i];
            if (vec.Z - camera1.Position.Z >= 0)
            {
                point3D[i].X = (int)((double)-(vec.X - camera1.Position.X) / (-0.1f) * zoom) + drawOrigin.X;
                point3D[i].Y = (int)((double)(vec.Y - camera1.Position.Y) / (-0.1f) * zoom) + drawOrigin.Y;
            }
            else
            {
                tmpOrigin.X = (int)((double)(cubeOrigin.X - camera1.Position.X) / (double)(cubeOrigin.Z - camera1.Position.Z) * zoom) + drawOrigin.X;
                tmpOrigin.Y = (int)((double)-(cubeOrigin.Y - camera1.Position.Y) / (double)(cubeOrigin.Z - camera1.Position.Z) * zoom) + drawOrigin.Y;

                point3D[i].X = (float)((vec.X - camera1.Position.X) / (vec.Z - camera1.Position.Z) * zoom + drawOrigin.X);
                point3D[i].Y = (float)(-(vec.Y - camera1.Position.Y) / (vec.Z - camera1.Position.Z) * zoom + drawOrigin.Y);

                point3D[i].X = (int)point3D[i].X;
                point3D[i].Y = (int)point3D[i].Y;
            }
        }
        a = Point.Round(point3D[4]);
        b = Point.Round(point3D[5]);
        c = Point.Round(point3D[6]);
        d = Point.Round(point3D[7]);
    }

    public static Math3D.Point3D[] fillCubeVertices(int width, int height, int depth)
    {
        Math3D.Point3D[] verts = new Math3D.Point3D[24];
        //front face
        verts[0] = new Math3D.Point3D(0, 0, 0);
        verts[1] = new Math3D.Point3D(0, height, 0);
        verts[2] = new Math3D.Point3D(width, height, 0);
        verts[3] = new Math3D.Point3D(width, 0, 0);
        //back face
        verts[4] = new Math3D.Point3D(0, 0, depth);
        verts[5] = new Math3D.Point3D(0, height, depth);
        verts[6] = new Math3D.Point3D(width, height, depth);
        verts[7] = new Math3D.Point3D(width, 0, depth);
        //left face
        verts[8] = new Math3D.Point3D(0, 0, 0);
        verts[9] = new Math3D.Point3D(0, 0, depth);
        verts[10] = new Math3D.Point3D(0, height, depth);
        verts[11] = new Math3D.Point3D(0, height, 0);
        //right face
        verts[12] = new Math3D.Point3D(width, 0, 0);
        verts[13] = new Math3D.Point3D(width, 0, depth);
        verts[14] = new Math3D.Point3D(width, height, depth);
        verts[15] = new Math3D.Point3D(width, height, 0);
        //top face
        verts[16] = new Math3D.Point3D(0, height, 0);
        verts[17] = new Math3D.Point3D(0, height, depth);
        verts[18] = new Math3D.Point3D(width, height, depth);
        verts[19] = new Math3D.Point3D(width, height, 0);
        //bottom face
        verts[20] = new Math3D.Point3D(0, 0, 0);
        verts[21] = new Math3D.Point3D(0, 0, depth);
        verts[22] = new Math3D.Point3D(width, 0, depth);
        verts[23] = new Math3D.Point3D(width, 0, 0);
        return verts;
    }
}