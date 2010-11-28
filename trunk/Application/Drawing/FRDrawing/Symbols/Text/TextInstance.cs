//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRMath;
using FRText;
using FRPaint.Fragment;
using System.Diagnostics;

namespace FRPaint
{
    public class TextInstance : SymbolConstraint
    {
        public TextInstance(DataFragment fragment)
            : base(fragment, new TextGraphicState(fragment))
        {
            m_String = new RichString(PtApp.ActiveDocument.GetFontManager());
        }

        #region Override for the compute pipeline
        public override void Compute()
        {
            TextGraphicState symState = GetSymbolState() as TextGraphicState;

            symState.Position = m_Position;
            symState.TextString = m_String;

            ObserverManager.Instance.SendEvent(new SubjectEvent(this, EventType.eUpdate));
        }
        #endregion

        public void SetPosition(GePoint Position)
        {
            Debug.Assert(Position != null);
            if (Position != null)
                m_Position = (GePoint)Position.Clone();

            //if (m_String.FontID > -1)
            //GraphicChangeNotify();
        }

        // N/A maybe we should clone the str here.
        public void SetText(RichString str)
        {
            m_String = str;

            //if (m_Position != null)
            //GraphicChangeNotify();
        }

        public RichString GetText()
        {
            return m_String;
        }

        private RichString m_String;
        private GePoint m_Position;
    };
}
