using NUnit.Framework;
using FRMath;

namespace FRMathTest
{
    [TestFixture]
    public class UnitVectorTest
    {

        /// <summary>
        ///A test for UnitVector(double, double, double)
        ///</summary>
        [Test]
        [Category("UnitVector")]
        public void ConstructorTest()
        {
            UnitVector target = new UnitVector(10, 100, -26);

            double expected = 1;
            double actual = target.Vector.Length();

            Assert.AreEqual(expected, actual, "FR.Math.UnitVector.UnitVector");
        }

        /// <summary>
        ///A test for X,Y,Z and [unit]
        ///</summary>
        [Test]
        [Category("UnitVector")]
        public void ItemAccessTest()
        {
            UnitVector target = (new UnitVector(10, 100, -26));

            Assert.AreEqual(target[1], target.X, "FR.Math.UnitVector.X");
            Assert.AreEqual(target[2], target.Y, "FR.Math.UnitVector.Y");
            Assert.AreEqual(target[3], target.Z, "FR.Math.UnitVector.Z");
        }

        /// <summary>
        ///A test for this[uint]
        ///</summary>
        [Test]
        [Category("UnitVector")]
        public void ItemAccessTest2()
        {
            Vector target = (new UnitVector(5, 8, 10)).Vector;

            Assert.AreNotEqual(5, target[1], "FR.Math.UnitVector.this[uint]");
            Assert.AreNotEqual(8, target[2], "FR.Math.UnitVector.this[uint]");
            Assert.AreNotEqual(10, target[3], "FR.Math.UnitVector.this[uint]");
        }

        /// <summary>
        ///A test for length
        ///</summary>
        [Test]
        [Category("UnitVector")]
        public void ItemAccessTest3()
        {
            double expected = 1.0;

            Vector actual = (new UnitVector(10, 100, -26)).Vector;

            Assert.AreEqual(expected, actual.Length(), "FR.Math.UnitVector.X,Y,Z");
        }
    }
}
