using System;
using System.Collections.Generic;
using System.Text;
using FRGraphic;

namespace FRPaint
{
    public class PtSelectContext : SelectContext
    {
        public PtSelectContext()
        {
            m_SelectionSet = null;
        }

        public SelectionSet CurrentSelectionSet
        {
            get { return m_SelectionSet; }
            set { m_SelectionSet = value; }
        }

        private SelectionSet m_SelectionSet;
    }
}
