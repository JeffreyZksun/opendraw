
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using FRContainer;
using FRText;

// Document save all the data.

namespace FRPaint
{
    public class PtDocument
    {
        public PtDocument()
        {
            m_dFloatTolerance = 1e-7;

            m_NodeList = new FRList<GraphicNode>();

            m_Database = new FRDatabase();
        }

        public void AddNode(GraphicNode Node)
        {
            m_NodeList.Add(Node);
        }

        public void RemoveNode(GraphicNode Node)
        {
            m_NodeList.Remove(Node);
        }

        public FRList<GraphicNode> GetGraphicNodes()
        {
            return m_NodeList;
        }

        public FontManager GetFontManager()
        {
            return m_Database.GetFontManager();
        }

        public FRDatabase Database
        {
            get { return m_Database; }
        }

        #region Attributes


        public PtDataScheme DataScheme
        {
            get { return m_Database.DataScheme; }
        }

        #endregion

        #region Data

        // Define the tolerance when do float calculation
        public double m_dFloatTolerance;

        private FRDatabase m_Database;

        private FRList<GraphicNode> m_NodeList;

        #endregion
    }
}
