
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using FRContainer;
using System.Diagnostics;

namespace FRGraphic
{
    public class SelectContext
    {
        public SelectContext()
        {
            //m_dSelTolerance = 5;
            m_RefPoint = new GePoint(0, 0);
            m_bMultiSelect = false;

            m_HittedDLPoints = new FRMultiMap<double, DisplayItem>();
            m_HittedDLItems = new FRMultiMap<double, DisplayItem>();

            m_SelectTolerance = 5;
            m_Device = null;
        }

        // If we find a item satisfy the context condition,
        // these flag will be set.
        // so we should reset them before the next process.
        public void ResetSelectionFlag()
        {
            //m_Creator = null;
            m_HittedDLItems.Clear();
            m_HittedDLPoints.Clear();
            //m_bIsHitted = false;
        }

        // Save the quality result
        public void AddItem(double distance, DisplayItem item)
        {
            Debug.Assert(item != null);
            if (null == item) return;

            m_HittedDLItems.AddItem(distance, item);
            if (item is DisplayItemPoint)
                m_HittedDLPoints.AddItem(distance, item);
        }

        #region Attributes

        public bool MultiSelect
        {
            get { return m_bMultiSelect; }
        }

        public bool IsHitted
        {
            get { return !m_HittedDLItems.Empty(); }
        }

        // We'll create selection based on the returned item
        // principle - point first,distance first
        // We should use this principle here.
        public DisplayItem GetHittedItem()
        {
            if (!m_HittedDLPoints.Empty())
                return m_HittedDLPoints.Values[0];
            else if(!m_HittedDLItems.Empty())
                return m_HittedDLItems.Values[0];
            else
                return null;
        }

        public double SelectTolerance
        {
            get { return m_SelectTolerance; }
        }

        public GePoint WorldPoint
        {
            get { return m_RefPoint; }
            set { m_RefPoint = value; }
        }

        public GraphicDevice GraphicDevice
        {
            get { return m_Device; }
        }
        #endregion

        #region Datam_result

        #region Intialize when create the context

        private GePoint m_RefPoint;
        //public double m_dSelTolerance;// Selection tolerance

        // Indicate whether we do the multi-select or single-select
        // If single-select, we should return when find a selection.
        private bool m_bMultiSelect;

        private double m_SelectTolerance;
        private GraphicDevice m_Device; // The width of the string depends the device.

        #endregion

        #region Change in the process of select


        // Save the display items satisfy the specific tolerance.
        // So we can decide we should use with display list.
        // Only save display item points.
        private FRMultiMap<double, DisplayItem> m_HittedDLPoints;
        // Save all the display items.
        private FRMultiMap<double, DisplayItem> m_HittedDLItems;

        #endregion
        #endregion


    };
}
