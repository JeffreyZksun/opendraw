
using NUnit.Framework;
using FRMath;

namespace FRMathTest
{
    [TestFixture]
    public class Matrix44Test
    {

        /// <summary>
        ///A test for Equals (object)
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void EqualsTest()
        {
            Matrix44 target = new Matrix44();

            object obj = new Matrix44(); 

            bool expected = true;
            bool actual = target.Equals(obj);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.Equals did not return the expected value.");
        }

        /// <summary>
        ///A test for GetAllScaling ()
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void GetAllScalingTest()
        {
            Matrix44 target = new Matrix44();
            target[3, 3] = 10;

            double expected = 0.1;
            double actual = target.GetAllScaling();

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.GetAllScaling did not return the expected value.");
        }

        /// <summary>
        ///A test for Identity
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void IdentityTest()
        {
            Matrix44 target = Matrix44.Identity;

            Assert.AreEqual(1, target[0, 0], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[1, 0], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[2, 0], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[3, 0], "FR.Math.Matrix44.Identity was not set correctly.");

            Assert.AreEqual(0, target[0, 1], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(1, target[1, 1], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[2, 1], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[3, 1], "FR.Math.Matrix44.Identity was not set correctly.");

            Assert.AreEqual(0, target[0, 2], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[1, 2], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(1, target[2, 2], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[3, 2], "FR.Math.Matrix44.Identity was not set correctly.");

            Assert.AreEqual(0, target[0, 3], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[1, 3], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(0, target[2, 3], "FR.Math.Matrix44.Identity was not set correctly.");
            Assert.AreEqual(1, target[3, 3], "FR.Math.Matrix44.Identity was not set correctly.");
        }

        /// <summary>
        ///A test for Inverse
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void InverseTest()
        {
            Matrix44 expected = new Matrix44();

            Matrix44 actual = expected.Inverse; 

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.Inverse was not set correctly.");
        }

        /// <summary>
        ///A test for operator * (GePoint, Matrix44)
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void MultiplyTest()
        {
            GePoint point = new GePoint(0,0,0); 
            GePoint to = new GePoint(1,2,3);

            Matrix44 trans = new Matrix44();
            trans.SetTranslation(to - point);

            GePoint expected = to;
            GePoint actual = trans * point;

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.operator * did not return the expected value.");
        }

        /// <summary>
        ///A test for RightMutiply(Matrix44)
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void RightMutiplyTest()
        {
            Matrix44 target = Matrix44.Identity;
            target.SetTranslation(new Vector(10, -2.5, 3));

            GePoint point = new GePoint(0, 0, 0);
            GePoint to = new GePoint(1, 2, 3);

            Matrix44 A = new Matrix44();
            A.SetTranslation(to - point);

            Matrix44 expected = target * A;
            Matrix44 actual = A.Clone();
            actual.LeftMultiply(target);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.RightMutiply did not return the expected value.");
        }

        /// <summary>
        ///A test for Clone()
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void CloneTest()
        {
            Matrix44 expected = Matrix44.Identity;
            expected.SetTranslation(new Vector(10, -2.5, 3));

            Matrix44 actual = expected.Clone();

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.Clone did not return the expected value.");
        }

        /// <summary>
        ///A test for Clone()
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void CloneTest2()
        {
            Matrix44 expected = Matrix44.Identity;
            expected.SetTranslation(new Vector(10, -2.5, 3));

            Matrix44 actual = expected.Clone();
            actual.SetScaling(10);

            Assert.AreNotEqual(expected, actual, "FR.Math.Matrix44.Clone did not return the expected value.");
        }

        /// <summary>
        ///A test for SetTranslation ( Vector )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetTranslationTest()
        {
            Vector dis = new Vector(10, 20, 30);

            Matrix44 actual = Matrix44.Identity;
            actual.SetTranslation(dis);

            Matrix44 expected = Matrix44.Identity;
            expected[0, 3] = dis.X;
            expected[1, 3] = dis.Y;
            expected[2, 3] = dis.Z;

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetTranslation did not return the expected value.");
        }

        /// <summary>
        ///A test for SetTranslation ( Vector )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetTranslationTest2()
        {
            Vector dis = new Vector(10, 20, 30);

            Matrix44 trans = Matrix44.Identity;
            trans.SetTranslation(dis);

            GePoint firtPosition = new GePoint(0, 0, 0);
            GePoint actual = trans * firtPosition;

            GePoint expected = new GePoint(10, 20, 30);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetTranslation did not return the expected value.");
        }

        /// <summary>
        ///A test for SetScaling ( double )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetScalingTest()
        {

            Matrix44 trans = Matrix44.Identity;
            trans.SetScaling(5);

            GePoint firtPosition = new GePoint(2, 3, 4);
            GePoint actual = trans * firtPosition;

            GePoint expected = new GePoint(10, 15, 20);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetScaling did not return the expected value.");
        }

        /// <summary>
        ///A test for SetScaling ( double. GePoint )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetScalingTest2()
        {
            GePoint basePosition = new GePoint(2, 3, 4);

            Matrix44 trans = Matrix44.Identity;
            trans.SetScaling(5, basePosition);

            GePoint actual = trans * basePosition;

            GePoint expected = new GePoint(2, 3, 4);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetScaling did not return the expected value.");
        }

        /// <summary>
        ///A test for SetScaling ( double. GePoint )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetScalingTest3()
        {
            GePoint basePosition = new GePoint(-1, 3.2, 2);

            Matrix44 trans = Matrix44.Identity;
            trans.SetScaling(5, basePosition);

            GePoint firtPosition = new GePoint(2, 3, 4);
            GePoint actual = trans * firtPosition;

            GePoint expected = new GePoint(14, 2.2, 12);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetScaling did not return the expected value.");
        }

        /// <summary>
        ///A test for SetRotate (double , UnitVector , GePoint )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetRotateTest()
        {
            GePoint basePosition = new GePoint(-1, 3.2, 2);
            UnitVector axis = UnitVector.kXAxis;

            Matrix44 trans = Matrix44.Identity;
            trans.SetRotate(2, axis, basePosition);

            GePoint target = new GePoint(2, 3, 4);
            GePoint actual = trans * target;

            GePoint expected = new GePoint(2, 1.46463, 0.985847);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetRotate did not return the expected value.");
        }

        /// <summary>
        ///A test for SetRotate (double , UnitVector , GePoint )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetRotateTest2()
        {
            GePoint basePosition = new GePoint(8, -2, 0);
            UnitVector axis = new UnitVector(1, -0.5, 6);

            Matrix44 trans = Matrix44.Identity;
            trans.SetRotate(1, axis, basePosition);

            GePoint target = new GePoint(-2, 3, 4);
            GePoint actual = trans * target;

            GePoint expected = new GePoint(-1.67301, -8.19326, 3.01273);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetRotate did not return the expected value.");
        }

        /// <summary>
        ///A test for SetRotate (double , UnitVector , GePoint )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetRotateTest3()
        {
            UnitVector axis = UnitVector.kZAxis;
            GePoint basePosition = GePoint.kOrigin;

            Matrix44 trans = Matrix44.Identity;
            trans.SetRotate(MathUtil.PI / 2, axis, basePosition);

            GePoint target = new GePoint(2, 0, 0);
            GePoint actual = trans * target;

            GePoint expected = new GePoint(0, 2, 0);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetRotate did not return the expected value.");
        }

        /// <summary>
        ///A test for SetRotate (double , UnitVector , GePoint )
        ///</summary>
        [Test]
        [Category("Matrix44")]
        public void SetRotateTest4()
        {
            UnitVector startDir = UnitVector.kXAxis;
            UnitVector endDir = UnitVector.kYAxis;
            GePoint basePosition = GePoint.kOrigin;

            Matrix44 trans = Matrix44.Identity;
            trans.SetRotate(startDir, endDir, basePosition);

            GePoint target = new GePoint(2,0,0);
            GePoint actual = trans * target;

            GePoint expected = new GePoint(0,2,0);

            Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetRotate did not return the expected value.");
        }

        ///// <summary>
        /////A test for operator * (Matrix44, Matrix44)
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void MultiplyTest1()
        //{
        //    Matrix44 lhs = null; // TODO: Initialize to an appropriate value

        //    Matrix44 rhs = null; // TODO: Initialize to an appropriate value

        //    Matrix44 expected = null;
        //    Matrix44 actual;

        //    actual = lhs * rhs;

        //    Assert.AreEqual(expected, actual, "FR.Math.Matrix44.operator * did not return the expected value.");
        //}

        ///// <summary>
        /////A test for SetRotate (double, UnitVector, GePoint)
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void SetRotateTest()
        //{
        //    Matrix44 target = new Matrix44();

        //    double AngleThita = 0;

        //    UnitVector axis = null;

        //    GePoint basePoint = new GePoint(0,0,0);

        //    Matrix44 expected = null;
        //    Matrix44 actual;

        //    actual = target.SetRotate(AngleThita, axis, basePoint);

        //    Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetRotate did not return the expected value.");
        //}

        ///// <summary>
        /////A test for SetRotate (UnitVector, UnitVector, GePoint)
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void SetRotateTest1()
        //{
        //    Matrix44 target = new Matrix44();

        //    UnitVector fromDirection = null; // TODO: Initialize to an appropriate value

        //    UnitVector toDirection = null; // TODO: Initialize to an appropriate value

        //    GePoint basePoint = null; // TODO: Initialize to an appropriate value

        //    Matrix44 expected = null;
        //    Matrix44 actual;

        //    actual = target.SetRotate(fromDirection, toDirection, basePoint);

        //    Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetRotate did not return the expected value.");
        //}

        ///// <summary>
        /////A test for SetScaling (double)
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void SetScalingTest()
        //{
        //    Matrix44 target = new Matrix44();

        //    double allScale = 0; // TODO: Initialize to an appropriate value

        //    Matrix44 expected = null;
        //    Matrix44 actual;

        //    actual = target.SetScaling(allScale);

        //    Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetScaling did not return the expected value.");
        //}

        ///// <summary>
        /////A test for SetScaling (double, GePoint)
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void SetScalingTest1()
        //{
        //    Matrix44 target = new Matrix44();

        //    double allScale = 0; // TODO: Initialize to an appropriate value

        //    GePoint basePoint = null; // TODO: Initialize to an appropriate value

        //    Matrix44 expected = null;
        //    Matrix44 actual;

        //    actual = target.SetScaling(allScale, basePoint);

        //    Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetScaling did not return the expected value.");
        //}

        ///// <summary>
        /////A test for SetTranslation (Vector)
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void SetTranslationTest()
        //{
        //    Matrix44 target = new Matrix44();

        //    Vector distance = null; // TODO: Initialize to an appropriate value

        //    Matrix44 expected = null;
        //    Matrix44 actual;

        //    actual = target.SetTranslation(distance);

        //    Assert.AreEqual(expected, actual, "FR.Math.Matrix44.SetTranslation did not return the expected value.");
        //}

        ///// <summary>
        /////A test for this[int row, int column]
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void ItemTest()
        //{
        //    Matrix44 target = new Matrix44();

        //    double val = 0; // TODO: Assign to an appropriate value for the property

        //    int row = 0; // TODO: Initialize to an appropriate value

        //    int column = 0; // TODO: Initialize to an appropriate value

        //    target[row, column] = val;


        //    Assert.AreEqual(val, target[row, column], "FR.Math.Matrix44.this was not set correctly.");
        //}

        ///// <summary>
        /////A test for UnitizeSubmatrixA ()
        /////</summary>
        //[Test]
        //[Category("Matrix44")]
        //public void UnitizeSubmatrixATest()
        //{
        //    Matrix44 target = new Matrix44();


        //    accessor.UnitizeSubmatrixA();

        //}

        #region Helper routines

        //private void Matrix44Equals(double expected, Matrix44 actual, string routine)
        //{
        //    Assert.IsNotNull(actual, routine + ": The actual matrix is NULL, but the expect matrix is not.");

        //    for (int i = 0; i < 4; i++)
        //        for (int j = 0; j < 4; j++)
        //        {
        //            Assert.AreEqual(expected, actual[i, j],
        //        routine + ": Data item [" + i + "," + j + "] are not equal.");
        //        }
        //}
  
        #endregion
    }
}
