using NUnit.Framework;
using FRMath;

namespace FRMathTest
{
    [TestFixture]
    public class VectorTest
    {
        /// <summary>
        ///A test for Vector(GePoint, GePoint)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void ConstructorTest()
        {
            GePoint startPoint = new GePoint(0, 0, 0);
            GePoint endPoint = new GePoint(1, 1, 1);

            Vector expected = new Vector(1, 1, 1);

            Vector actual = new Vector(startPoint, endPoint);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Vector(GePoint, GePoint).");
        }

        /// <summary>
        ///A test for Vector()
        ///</summary>
        [Test]
        [Category("Vector")]
        public void ConstructorTest2()
        {
            Vector expected = UnitVector.kXAxis.Vector;

            Vector actual = new Vector();

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Vector().");
        }

        /// <summary>
        ///A test for Cross (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void CrossTest()
        {
            Vector A = new Vector(1,2,3);

            Vector B = new Vector(3,4,5); 

            Vector expected = new Vector(-2, 4, -2);
            Vector actual = A.Cross(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Cross.");
        }

        /// <summary>
        ///A test for Cross (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void CrossTest2()
        {
            Vector A = new Vector(1.2, -2.3, 5);

            Vector B = new Vector(-5.6, 8, -2.9);

            Vector expected = new Vector(-33.33, -24.52, -3.28);
            Vector actual = A.Cross(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Cross.");
        }

        /// <summary>
        ///A test for Cross (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void CrossTest3()
        {
            Vector A = new Vector(0,0,0);

            Vector B = new Vector(-5.6, 8, -2.9);

            Vector expected = new Vector(0,0,0);
            Vector actual = A.Cross(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Cross.");
        }

        /// <summary>
        ///A test for Dot (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void DotTest()
        {
            Vector A = new Vector(1, 2, 3);

            Vector B = new Vector(-5.6, 8, -2.9);

            double expected = 1.7;
            double actual = A.Dot(B);
            
            Assert.AreEqual(expected, actual
                , MathUtil.kTolerance,"FR.Math.Vector.Dot.");
        }

        /// <summary>
        ///A test for Dot (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void DotTest2()
        {
            Vector A = new Vector(0,0,0);

            Vector B = new Vector(-5.6, 8, -2.9);

            double expected = 0;
            double actual = A.Dot(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Dot.");
        }

        /// <summary>
        ///A test for Dot (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void DotTest3()
        {
            Vector A = new Vector(0, 0, 0);

            Vector B = new Vector(0, 0, 0);

            double expected = 0;
            double actual = A.Dot(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Dot.");
        }


        /// <summary>
        ///A test for Dot (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void DotTest4()
        {
            Vector A = new Vector(56, 89, -52.3);

            Vector B = new Vector(-3.2, -5, 0);

            double expected = -624.2;
            double actual = A.Dot(B);

            Assert.AreEqual(expected, actual,
                MathUtil.kTolerance, "FR.Math.Vector.Dot.");
        }

        /// <summary>
        ///A test for AngleTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void AngleToTest()
        {
            Vector A = new Vector(0,1,0);

            Vector B = new Vector(1,0,0);

            double expected = MathUtil.PI / 2;
            double actual = A.AngleTo(B);

            Assert.AreEqual(expected, actual,
                MathUtil.kTolerance, "FR.Math.Vector.Dot.");
        }

        /// <summary>
        ///A test for AngleTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void AngleToTest2()
        {
            Vector A = new Vector(0, 0, 0);

            Vector B = new Vector(1, 0, 0);

            double expected = 0;
            double actual = A.AngleTo(B);

            Assert.AreEqual(expected, actual,
                MathUtil.kTolerance, "FR.Math.Vector.Dot.");
        }

        /// <summary>
        ///A test for AngleTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void AngleToTest3()
        {
            Vector A = new Vector(0, 0, 0);

            Vector B = new Vector(0, 0, 0);

            double expected = 0;
            double actual = A.AngleTo(B);

            Assert.AreEqual(expected, actual,
                MathUtil.kTolerance, "FR.Math.Vector.Dot.");
        }

        /// <summary>
        ///A test for AngleTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void AngleToTest4()
        {
            Vector A = new Vector(1, 0, 0);

            Vector B = new Vector(2, -2, 0);

            double expected = MathUtil.PI / 4;
            double actual = A.AngleTo(B);

            Assert.AreEqual(expected, actual,
                MathUtil.kTolerance, "FR.Math.Vector.Dot.");
        }

        /// <summary>
        ///A test for Copy (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void CopyTest()
        {
            Vector target = new Vector(4, -2.5, 3.8);

            Vector expected = new Vector(4, -2.5, 3.8);
            Vector actual = new Vector();
            actual.Copy(target);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Copy");
        }

        /// <summary>
        ///A test for IsZero ()
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsZeroTest()
        {
            Vector target = new Vector(4, -2.5, 3.8);

            bool expected = false;
            bool actual = target.IsZero();

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsZero");
        }

        /// <summary>
        ///A test for IsZero ()
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsZeroTest2()
        {
            Vector target = new Vector(0, 0, 0);

            bool expected = true;
            bool actual = target.IsZero();

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsZero");
        }

        /// <summary>
        ///A test for IsSameDirectionTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsSameDirectionToTest()
        {
            Vector A = new Vector(0, 0, 0);
            Vector B = new Vector(1, -5, 9);

            bool expected = true;
            bool actual = A.IsSameDirectionTo(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsSameDirectionTo");
        }

        /// <summary>
        ///A test for IsSameDirectionTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsSameDirectionToTest2()
        {
            Vector A = new Vector(2, -10, 18);
            Vector B = new Vector(1, -5, 9);

            bool expected = true;
            bool actual = A.IsSameDirectionTo(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsSameDirectionTo");
        }

        /// <summary>
        ///A test for IsSameDirectionTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsSameDirectionToTest3()
        {
            Vector A = new Vector(-1, 5, -9);
            Vector B = new Vector(1, -5, 9);

            bool expected = false;
            bool actual = A.IsSameDirectionTo(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsSameDirectionTo");
        }

        /// <summary>
        ///A test for IsParallelTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsParallelToTest()
        {
            Vector A = new Vector(0, 0, 0);
            Vector B = new Vector(1, -5, 9);

            bool expected = true;
            bool actual = A.IsParallelTo(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsParallelTo");
        }

        /// <summary>
        ///A test for IsParallelTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsParallelToTest2()
        {
            Vector A = new Vector(-1, 5, -9);
            Vector B = new Vector(1, -5, 9);

            bool expected = true;
            bool actual = A.IsParallelTo(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsParallelTo");
        }

        /// <summary>
        ///A test for IsPerpendicularTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsPerpendicularToTest()
        {
            Vector A = new Vector(0, 1, 0);
            Vector B = new Vector(-1, 0, 0);

            bool expected = true;
            bool actual = A.IsPerpendicularTo(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsPerpendicularTo");
        }

        /// <summary>
        ///A test for IsPerpendicularTo (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void IsPerpendicularToTest2()
        {
            Vector A = new Vector(2, 2, 0);
            Vector B = new Vector(2, -2, 0);

            bool expected = true;
            bool actual = A.IsPerpendicularTo(B);

            Assert.AreEqual(expected, actual, "FR.Math.Vector.IsPerpendicularTo");
        }

        /// <summary>
        ///A test for Clone ()
        ///</summary>
        [Test]
        [Category("Vector")]
        public void CloneTest()
        {
            Vector expected = new Vector(2, 2, -0.5);
            Vector actual = expected.Clone();

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Clone");
        }

        /// <summary>
        ///A test for Clone ()
        ///</summary>
        [Test]
        [Category("Vector")]
        public void CloneTest2()
        {
            Vector target = new Vector(2, 2, -0.5);

            Vector expected = new Vector(2, 2, -0.5);
            Vector actual = target.Clone();
            target.X = 10;

            Assert.AreEqual(expected, actual, "FR.Math.Vector.Clone");
        }

        /// <summary>
        ///A test for operator - (Vector)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void NegativeTest()
        {
            Vector target = new Vector(2, 2, -0.5);

            Vector expected = new Vector(-2, -2, 0.5);
            Vector actual = -target;

            Assert.AreEqual(expected, actual, "FR.Math.Vector.operator - (Vector)");
        }

        /// <summary>
        ///A test for operator * (Vector, double)
        ///</summary>
        [Test]
        [Category("Vector")]
        public void MultiplyTest()
        {
            Vector target = new Vector(2, 2, -0.5);

            Vector expected = new Vector(10, 10, -2.5);
            Vector actual = target * 5;

            Assert.AreEqual(expected, actual, "FR.Math.Vector.operator * (Vector, double)");
        }

        /// <summary>
        ///A test for this[uint]
        ///</summary>
        [Test]
        [Category("Vector")]
        public void ItemAccessTest()
        {
            Vector target = new Vector(2, 2, -0.5);
            target[1] = 20;
            target[2] = 20;
            target[3] = -18;

            Assert.AreEqual(20, target[1], "FR.Math.Vector.this[uint]");
            Assert.AreEqual(20, target[2], "FR.Math.Vector.this[uint]");
            Assert.AreEqual(-18, target[3], "FR.Math.Vector.this[uint]");
        }

        /// <summary>
        ///A test for X, Y, Z
        ///</summary>
        [Test]
        [Category("Vector")]
        public void ItemAccessTest2()
        {
            Vector target = new Vector(2, 2, -0.5);
            target.X = 10;
            target.Y = 10;
            target.Z = -18;

            Assert.AreEqual(10, target.X, "FR.Math.Vector.X");
            Assert.AreEqual(10, target.Y, "FR.Math.Vector.Y");
            Assert.AreEqual(-18, target.Z, "FR.Math.Vector.Z");
        }

        /// <summary>
        ///A test for UnitVector 
        ///</summary>
        [Test]
        [Category("Vector")]
        public void UnitVectorTest()
        {
            Vector target = new Vector(0, 20, 0);

            UnitVector expected = new UnitVector(0, 1, 0);
            UnitVector actual = target.UnitVector;

            Assert.AreEqual(expected, actual, "FR.Math.Vector.UnitVector");
        }
    }
}
