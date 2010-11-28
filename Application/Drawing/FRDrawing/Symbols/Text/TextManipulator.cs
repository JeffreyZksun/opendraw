
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-23 Create
//
//////////////////////////////////////////////////////////////////////////

using FRText;

namespace FRPaint
{

    public class TextManipulator : Manipulator
    {
        public TextManipulator(TextInstance src)
        {
            m_Instance = src;
        }

        public override void DoubClick() 
        {
            TextDialogData DlgData = new TextDialogData();
            DlgData.m_Text = m_Instance.GetText().TextString;

            if (TextDialogUtil.ShowDialog(DlgData))
            {
                RichString str = new RichString(PtApp.ActiveDocument.GetFontManager());
                str.TextString = DlgData.m_Text;
                str.FontID = DlgData.GetFontID();
                m_Instance.SetText(str);
            }

        }

        protected TextInstance m_Instance;
    };
}
