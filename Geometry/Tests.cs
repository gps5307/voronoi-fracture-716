using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
// was going to do this but I don't have Windows 
// using System.Windows.Forms;


namespace Geometry
{
    [TestFixture]
    public class Tests {
        [Test]

        public static void testTriangulate()
        {
            var points = new List<Point>
            {
                new Point(0.0, 0.0),
                new Point(3.0, 4.0),
                new Point(5.4, 3.1),
                new Point(2.3, 10.0),
                new Point(3.0, 4.0),
                new Point(10.2, 3.1),
                new Point(3.0, 4.0),
                new Point(2.1, 8.0),
                new Point(7.4, 0.5)
            };
            var triangles = DelaunayTriangulation.Triangulate(points);
            // verified that equal points do not screw this up
            Assert.That(triangles.Count, Is.EqualTo(8));
            // more in depth testing here coming soon.
            
        }

        public static void testEdges() {
            var p1 = new Point(0.0, 0.0);
            var p2 = new Point(3.0, 4.0);
            var p3 = new Point(5.4, 3.1);
            var p4 = new Point(2.3, 10.0);
            var p5 = new Point(3.0, 4.0);
            var e1 = new Edge(p1, p2);
            var e2 = new Edge(p2, p3);
            var e3 = new Edge(p2, p1);
            var e4 = new Edge(p5, p3);
            Assert.That(e1.Equals(e2), Is.False);
            Assert.That(e1.Equals(e3), Is.True);
            Assert.That(e2.Equals(e4), Is.True);
            Assert.That(e1.GetHashCode(), !Is.EqualTo(e2.GetHashCode()));
            Assert.That(e1.GetHashCode(), Is.EqualTo(e3.GetHashCode()));
            Assert.That(e2.GetHashCode(), Is.EqualTo(e4.GetHashCode()));
        }

        public static void testPoints() 
        {
            var p1 = new Point(0.0, 0.0);
            var p2 = new Point(3.0, 4.0);
            var p3 = new Point(5.4, 3.1);
            var p4 = new Point(2.3, 10.0);
            var p5 = new Point(3.0, 4.0);
            var notPoint = new Edge(p1, p1);
            Assert.That(p1.Equals(notPoint), Is.False);
            Assert.That(!p1.Equals(p2));
            Assert.That(!p3.Equals(p4));
            Assert.That(p4.Equals(p4));
            Assert.That(p2.Equals(p5));
            Assert.That(p1.Distance(p2), Is.EqualTo(p2.Distance(p1)));
            Assert.That(p1.Distance(p2), Is.EqualTo(5.0));
            Assert.That(p2.GetHashCode(), Is.EqualTo(p5.GetHashCode()));

        }
        

        public static void testTriangle() 
        {
            var p1 = new Point(0.0, 0.0);
            var p2 = new Point(3.0, 4.0);
            var p3 = new Point(5.4, 3.1);
            var p4 = new Point(2.3, 10.0);
            var p5 = new Point(3.0, 4.0);
            var p6 = new Point(4.5, 3.2);
            var p7 = new Point(4.5, 4.1);
            
            Assert.Throws<ArgumentException>(() => new Triangle(p1, p1, p3));
            Assert.Throws<ArgumentException>(() => new Triangle(p1, p2, p5));
            var t1 = new Triangle(p1, p2, p3);
            Assert.That(t1.Edges.Count, Is.EqualTo(3));
            Assert.That(t1.Edges.Contains(new Edge(p1, p2)), Is.True);
            // verified that this part works properly
            Assert.That(t1.ContainsInCircumcircle(p4), Is.False);
            Assert.That(t1.ContainsInCircumcircle(p5), Is.True);
            Assert.That(t1.ContainsInCircumcircle(p3), Is.True);
            Assert.That(t1.ContainsInCircumcircle(p6), Is.True);
            Assert.That(t1.ContainsInCircumcircle(p7), Is.False);
            

        }

        public static void testGenerator() 
        {
            var points = new List<Point>
            {
                new Point(0.0, 0.0),
                new Point(3.0, 4.0),
                new Point(5.4, 3.1),
                new Point(2.3, 10.0),
                new Point(10.2, 3.1),
                new Point(2.1, 8.0),
                new Point(7.4, 0.5)
            };
            var points2 = new List<Point>
            {
                new Point(0.0, 0.0),
                new Point(2.3, 10.0),
                new Point(10.2, 3.1),
                new Point(2.1, 8.0),
                new Point(3.0, 4.0),
                new Point(5.4, 3.1),
                new Point(7.4, 0.5)
            };
            var triangles = DelaunayTriangulation.Triangulate(points);
            var voronoiCells = VoronoiGenerator.FromDelaunay(triangles);
            Assert.That(voronoiCells.Count, Is.EqualTo(7));
            Assert.That(voronoiCells.Any(obj => obj.Site.Equals(new Point(0.0, 0.0))), Is.True);
            Assert.That(voronoiCells.Any(obj => obj.Site.Equals(new Point(3.0, 4.0))), Is.True);
            Assert.That(voronoiCells.Any(obj => obj.Site.Equals(new Point(5.4, 3.1))), Is.True);
            Assert.That(voronoiCells.Any(obj => obj.Site.Equals(new Point(2.1, 8.0))), Is.True);
            int index = voronoiCells.FindIndex(obj => obj.Site.Equals(new Point(2.1, 8.0)));
            Assert.That(voronoiCells.Any(obj => obj.Site.Equals(new Point(2.3, 10.0))), Is.True);
            Assert.That(voronoiCells.Any(obj => obj.Site.Equals(new Point(10.2, 3.1))), Is.True);
            Assert.That(voronoiCells.Any(obj => obj.Site.Equals(new Point(7.4, 0.5))), Is.True);
            double reasonableOffset = 1e-9;
            // will make offset actually work soon, for now I just hardcoded this one after verifying externally. This is just to test sorting
            int indexC = voronoiCells[index].Cell.FindIndex(obj => obj.Equals(new Point(-2.36025641025641, 4.895192307692308)));
            Assert.That(indexC, !Is.EqualTo(-1));
            Assert.That(voronoiCells[index].Cell.Count, Is.EqualTo(5));
            Assert.That(voronoiCells[index].Cell[indexC%5], Is.EqualTo(new Point(-2.36025641025641, 4.895192307692308)));
            Assert.That(voronoiCells[index].Cell[(indexC+1)%5], Is.EqualTo(new Point(5.355460750853241, 6.63122866894198)));
            Assert.That(voronoiCells[index].Cell[(indexC+2)%5], Is.EqualTo(new Point(7.800000000000001, 8.277551020408165)));
            Assert.That(voronoiCells[index].Cell[(indexC+3)%5], Is.EqualTo(new Point(7.8926658905704326, 8.430733410942958)));
            Assert.That(voronoiCells[index].Cell[(indexC+4)%5], Is.EqualTo(new Point(-30.426923076923064, 12.2626923076923)));
            var triangles2 = DelaunayTriangulation.Triangulate(points2);
            var voronoiCells2 = VoronoiGenerator.FromDelaunay(triangles2);
            Assert.That(voronoiCells2.Count, Is.EqualTo(7));
            int index2 = voronoiCells2.FindIndex(obj => obj.Site.Equals(new Point(2.1, 8.0)));
            int indexC2 = voronoiCells2[index2].Cell.FindIndex(obj => obj.Equals(new Point(-2.36025641025641, 4.895192307692308)));
            Assert.That(voronoiCells2[index2].Cell[indexC2%5], Is.EqualTo(new Point(-2.36025641025641, 4.895192307692308)));
            Assert.That(voronoiCells2[index2].Cell[(indexC2+1)%5], Is.EqualTo(new Point(5.355460750853241, 6.63122866894198)));
            Assert.That(voronoiCells2[index2].Cell[(indexC2+2)%5], Is.EqualTo(new Point(7.800000000000001, 8.277551020408165)));
            Assert.That(voronoiCells2[index2].Cell[(indexC2+3)%5], Is.EqualTo(new Point(7.8926658905704326, 8.430733410942958)));
            Assert.That(voronoiCells2[index2].Cell[(indexC2+4)%5], Is.EqualTo(new Point(-30.426923076923064, 12.2626923076923)));
            // going to make a version that isn't exclusive to test CCW soon.

        }

        public static void AllTests()
        {
            
            testPoints();
            testTriangle();
            testTriangulate();
            testTriangle();
            testGenerator();

        }
    }
    
}

