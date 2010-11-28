
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System;
using System.Text;

namespace FRPaint
{
    // Finite state machine used for parsing the string
    // This is 12.345.
    // 
    /////////////////////////Example//////////////
    // // Get initial data
    // DigitalStringContext ctx = new DigitalStringContext();
    // ctx.m_OriginalString = "This is 12.345, number";
    // ctx.m_Period = ".";

    // // Parse the string.
    // bool result = DigitalParser.Parse(ctx);
    ////////////////////////////////////////

    #region State machine description
    // There are four states.
    // [1]. CBNState - Char before number
    // [2]. NBPState - Number before period
    // [3]. NAPState - Number after period
    // [4]. CANState - Char after number
    //
    // State switch diagram.
    //
    // [IN]---->[1]
    // [1]-(Period)->[2]    [1]-(#)->[3]   [1]-(else)->[1]
    // [2]-(Period)->[2]    [2]-(#)->[3]   [2]-(else)->[4]
    // [3]-(#)->[3]         [3]-(else)->[4]
    // [4]-( )->[4]     
    // [4]--->[OUT]
    //
    #endregion


    // Context for the state machine
    public class DigitalStringContext
    {
        public DigitalStringContext()
        {
            m_OriginalString = "";
            m_Period = '.';

            m_CBN = new StringBuilder();
            m_NBP = new StringBuilder();
            m_NAP = new StringBuilder();
            m_CAN = new StringBuilder();

            m_iPosition = 0;
        }

        // Check whether the string is parsed successful.
        public bool IsSuccessful()
        {
            return m_iPosition == m_OriginalString.Length;
        }

        // Don't encapsulate these data for brief.

        // Original value. These two value must be input by user.
        public String m_OriginalString;
        public Char m_Period;

        // Record the current position, changed by the state machine
        public int m_iPosition;

        // Result value, changed by the state machine
        public StringBuilder m_CBN; // Chars before number
        public StringBuilder m_NBP; // Numbers before period
        public StringBuilder m_NAP; // Numbers after period
        public StringBuilder m_CAN; // Chars after number
    };

    public class DigitalParser
    {
        public static bool Parse(DigitalStringContext ctx)
        {
            if (ctx == null || ctx.m_OriginalString == null) return false;

            DPState CurrentState = new CBNState();

            while (CurrentState != null)
            {
                CurrentState = CurrentState.Excute(ctx);
            }

            return ctx.IsSuccessful();
        }

    };

    abstract class DPState
    {

        // Return the next state
        public abstract DPState Excute(DigitalStringContext ctx);

        protected char GetProcessChar(DigitalStringContext ctx)
        {
            return ctx.m_OriginalString[ctx.m_iPosition];
        }

        protected bool IsDigit(char v)
        {
            return v >= 0x0030 && v <= 0x0039;
        }

        protected bool IsPeriod(char v, DigitalStringContext ctx)
        {
            return v == ctx.m_Period;
        }

        protected bool IsFinished(DigitalStringContext ctx)
        {
            return ctx.IsSuccessful();
        }
    }

    // Char before number
    class CBNState : DPState
    {
        public override DPState Excute(DigitalStringContext ctx)
        {
            if (null == ctx || IsFinished(ctx)) return null;

            char CurrentChar = GetProcessChar(ctx);

            ctx.m_iPosition++;

            if (IsPeriod(CurrentChar, ctx))
            {
                return new NAPState();
            }
            else if (IsDigit(CurrentChar))
            {
                ctx.m_NBP.Append(CurrentChar);

                return new NBPState();
            }

            ctx.m_CBN.Append(CurrentChar);
            return this;
        }

    };

    // Number before period
    class NBPState : DPState
    {
        public override DPState Excute(DigitalStringContext ctx)
        {
            if (null == ctx || IsFinished(ctx)) return null;

            char CurrentChar = GetProcessChar(ctx);

            ctx.m_iPosition++;

            if (IsPeriod(CurrentChar, ctx))
            {
                return new NAPState();
            }
            else if (IsDigit(CurrentChar))
            {
                ctx.m_NBP.Append(CurrentChar);

                return this;
            }

            ctx.m_CAN.Append(CurrentChar);

            return new CANState();
        }
    };

    // Number after period
    class NAPState : DPState
    {
        public override DPState Excute(DigitalStringContext ctx)
        {
            if (null == ctx || IsFinished(ctx)) return null;

            char CurrentChar = GetProcessChar(ctx);

            ctx.m_iPosition++;

            if (IsDigit(CurrentChar))
            {
                ctx.m_NAP.Append(CurrentChar);

                return this;
            }

            ctx.m_CAN.Append(CurrentChar);
            return new CANState();
        }
    };

    // Char after number
    class CANState : DPState
    {
        public override DPState Excute(DigitalStringContext ctx)
        {
            if (null == ctx || IsFinished(ctx)) return null;

            char CurrentChar = GetProcessChar(ctx);

            ctx.m_iPosition++;

            ctx.m_CAN.Append(CurrentChar);

            return this;
        }
    };


}
