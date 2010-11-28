//////////////////////////////////////////////////////////////////////////
//
// This attribute indicates the entry class of the component.
// 
//  [FRComponentEntry("Description...", Company = "CompanyName...")]
//   public class ExtensionComponent : FWComponent{}
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-21 Create
//
//////////////////////////////////////////////////////////////////////////

using System;

namespace FRFramework.Extension
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false,
        Inherited = false)]
    public class FRComponentEntryAttribute : Attribute
    {
        public FRComponentEntryAttribute(String description)
        {
            m_description = description;
            m_company = "";
        }

        public String Description
        {
            get { return m_description; }
        }

        public String Company
        {
            get { return m_company; }
            set { m_company = value; }
        }

        private String m_description;
        private String m_company;
    }
}
