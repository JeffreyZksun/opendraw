
//////////////////////////////////////////////////////////////////////////
//
// FRD - Parse the dimension value
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using FRContainer;

namespace FRPaint
{
    // Parse a string to get the dimension from it.
    public class DimensionParser
    {
        // Define the supported dimension units
        public enum DimensionUnit
        {
            eMM,
            eCM,
            eM,
            eInch,
            eUnitCount // indicate the count of units
        }

        public DimensionParser()
        {
            m_UnitStringList = new FRList<string>();
            for (int i = 0; i < (int)DimensionUnit.eUnitCount; i++)
                m_UnitStringList.Add("");

            // These strings need globalization/ localization
            m_UnitStringList[(int)DimensionUnit.eMM] = "mm";
            m_UnitStringList[(int)DimensionUnit.eCM] = "cm";
            m_UnitStringList[(int)DimensionUnit.eM] = "m";
            m_UnitStringList[(int)DimensionUnit.eInch] = "in";
            // End 

            m_Period = '.';
            m_Unit = DimensionUnit.eMM;
        }

        public bool GetDimension(string DigitalString, ref double dDimension)
        {
            if (!IsValidDigital(DigitalString)) return false;

            DigitalStringContext ctx = new DigitalStringContext();
            ctx.m_OriginalString = DigitalString;
            ctx.m_Period = m_Period;

            // Parse the string.
            bool result = DigitalParser.Parse(ctx);

            string StrValue = ctx.m_NBP.ToString() + ctx.m_Period.ToString()
                + ctx.m_NAP.ToString();

            try
            {
                double dValue = double.Parse(StrValue);
                dDimension = GetSign(ctx.m_CBN.ToString()) * dValue;
            }
            catch
            {
                Debug.Assert(false);
                return false;
            }

            return true;
        }

        // Check whether the sting can be parsed to a valid dimension
        public bool IsValidDigital(string DigitalString)
        {
            Debug.Assert(DigitalString != null);
            if (null == DigitalString) return false;

            if (DigitalString.Trim().Length == 0) return false;

            DigitalStringContext ctx = new DigitalStringContext();
            ctx.m_OriginalString = DigitalString.Trim();
            ctx.m_Period = m_Period;

            // Parse the string.
            bool result = DigitalParser.Parse(ctx);

            // Analyze the result
            if (!IsSign(ctx.m_CBN.ToString()))
                return false;
            if (!IsValidUnit(ctx.m_CAN.ToString()))
                return false;

            return true;
        }

        // If there is only white space and "+" or "-" in this string, 
        // return true
        private bool IsSign(string str)
        {
            // Remove all the white space.
            string TrimedStr = str.Trim();

            if (TrimedStr.Length == 0)
                return true;
            if (TrimedStr.Length == 1)
            {
                if (TrimedStr == "+") return true;
                if (TrimedStr == "-") return true;
            }

            return false;
        }

        // If there is only white space and unit string in this string, return true
        // The unit string is specified by m_Unit
        private bool IsValidUnit(string str)
        {
            // Remove all the white space.
            string TrimedStr = str.Trim();

            return TrimedStr == m_UnitStringList[(int)m_Unit];
        }

        private int GetSign(string str)
        {
            // Remove all the white space.
            string TrimedStr = str.Trim();

            if (TrimedStr == "-")
                return -1;
            else
                return 1;
        }

        private FRList<string> m_UnitStringList;
        private Char m_Period;
        private DimensionUnit m_Unit;
    }
}
