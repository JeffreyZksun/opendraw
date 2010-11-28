
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;

using FRMath.Adaptor;

namespace FRMath
{
    public class GePoint : Geometry
    {
        #region Constructors
        public GePoint()
        {
            m_Proxy = MathAdaptorFactory.Instance.CreatePointAdaptor(0, 0, 0);
        }

        public GePoint(double xx, double yy)
        {
            m_Proxy = MathAdaptorFactory.Instance.CreatePointAdaptor(xx, yy, 0);
        }

        public GePoint(double xx, double yy, double zz)
        {
            m_Proxy = MathAdaptorFactory.Instance.CreatePointAdaptor(xx, yy, zz);
        }       

        public GePoint(GePoint src)
        {
            m_Proxy = MathAdaptorFactory.Instance.CreatePointAdaptor(
                src.X, src.Y, src.Z);
        }

        public override Geometry Clone()
        {
            GePoint NewPoint = new GePoint();
            NewPoint.m_Proxy = this.m_Proxy.Clone();
            return NewPoint;
        }
        #endregion

        #region Distance

        // The shortest distance of two points.
        public double DistanceTo(GePoint point)
        {
            double dist = _Proxy.DistanceTo(point._Proxy);

            return dist;
        }

        // The shortest distance of point to plane.
        public double DistanceTo(GePlane plane)
        {
            Debug.Assert(plane != null);

            return this._Proxy.DistanceTo(plane._Proxy);
        }
        #endregion

        public GePoint Transform(Matrix44 trans)
        {
            Debug.Assert(trans != null);
            if (null == trans) return this;

            _Proxy.Transform(trans._Proxy);

            return this;
        }

        #region Geometry relationship
        public bool IsEqualTo(GePoint point)
        {
            return _Proxy.IsEqualTo(point._Proxy);
        }

        public bool IsOn(GePlane Plane)
        {
            if (null == Plane) return false;
            return _Proxy.IsOn(Plane._Proxy);
        }

        public bool IsOn(GeLine Line)
        {
            if (null == Line) return false;
            return _Proxy.IsOn(Line._Proxy);
        }

        public bool IsOn(GeLineSeg LinSeg)
        {
            if (null == LinSeg) return false;

            Debug.Assert(false, "NO IMPL, Can remove later");
            return LinSeg.Contains(this);
        }

        public void Move(Vector distance)
        {
            if (null == distance) return;

            _Proxy.Move(distance._Proxy);
        }
        #endregion

        #region Override operator - +
        public static Vector operator -(GePoint ep,
        GePoint sp)
        {
            return new Vector(ep._Proxy - sp._Proxy);
        }

        public static GePoint operator +(GePoint sp,
        Vector distance)
        {
            return new GePoint(sp._Proxy + distance._Proxy);
        }

        #endregion

        #region Override Object
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            if (!(obj is GePoint)) return false;

            GePoint rhs = (GePoint)obj;
            return _Proxy.Equals(rhs._Proxy);
        }

        public override string ToString()
        {
            return _Proxy.ToString();
        }

        public override int GetHashCode()
        {
            return _Proxy.GetHashCode();
        }
        #endregion

        #region Attributes
        public float X
        {
            get { return (float)_Proxy.X; }
            set { _Proxy.X = value; }
        }
        public float Y
        {
            get { return (float)_Proxy.Y; }
            set { _Proxy.Y = value; }
        }
        public float Z
        {
            get { return (float)_Proxy.Z; }
            set { _Proxy.Z = value; }
        }

        public static GePoint kOrigin
        {
            get { return new GePoint(0, 0, 0); }
        }

        #endregion

        #region Proxy
        public GePoint(PointAdaptor proxy)
        {
            m_Proxy = proxy;
        }
        public PointAdaptor _Proxy
        {
            get { return m_Proxy; }
        }
        private PointAdaptor m_Proxy;
        #endregion

    }
}
