
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2008-9-5 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using FRText;
using FRPaint.Fragment;

// Database save all the data.
// A database is a file.

namespace FRPaint
{
    public class FRDatabase
    {
        public FRDatabase()
        {

            m_FontManager = new FontManager();

            m_DataScheme = new PtDataScheme();

            m_InstanceList = new FRList<SymbolConstraint>();

            m_Fragment = new DataFragment();
        }

        public void AddInstance(SymbolConstraint Node)
        {
            m_InstanceList.Add(Node);
        }

        public void RemoveInstance(SymbolConstraint Node)
        {
            m_InstanceList.Remove(Node);
        }

        public FRList<SymbolConstraint> GetInstances()
        {
            return m_InstanceList;
        }

        public FontManager GetFontManager()
        {
            return m_FontManager;
        }

        public DataFragment GetFragment()
        {
            return m_Fragment;
        }

        #region Attributes


        public PtDataScheme DataScheme
        {
            get { return m_DataScheme; }
        }

        #endregion

        #region Data

        private FontManager m_FontManager;

        private PtDataScheme m_DataScheme;

        private FRList<SymbolConstraint> m_InstanceList;

        private DataFragment m_Fragment;

        #endregion
    }
}
