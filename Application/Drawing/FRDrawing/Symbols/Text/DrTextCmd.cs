
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using FRText;

namespace FRPaint
{
    class DrTextCmd : DMStateCommand
    {
        public DrTextCmd()
            : base("DrTextCmd", "&Text")
        {
            m_Instance = null;
        }

        public override CommandState GetNextState()
        {
             return new PointInferenceState();;
        }

        protected override bool OnActive()
        {          
            return base.OnActive();
        }

        protected override bool OnExcute()
        {
            SetStatus("Input Text");

            if(!base.OnExcute())
                return false;

            return true;
        }

        protected override GraphicNode GetNode()
        {
            Fragment.DataFragment fragment = PtApp.ActiveDocument.Database.GetFragment();
            m_Instance = new TextInstance(fragment);
            TextNode Node = new TextNode(m_Instance);

            return Node;
        }

        // For updating real time.
        public override void UpdateGraphics(GePoint MousePosition)
        {
            m_Instance.SetPosition(MousePosition);
        }

        // It was called by target class when d-click
        public override void ShowDialog() 
        {
            TextDialogData DlgData = new TextDialogData();

            if (TextDialogUtil.ShowDialog(DlgData))
            {
                RichString str = new RichString(PtApp.ActiveDocument.GetFontManager());
                str.TextString = DlgData.m_Text;
                str.FontID = DlgData.GetFontID();
                m_Instance.SetText(str);

                // commit the command
                PtApp.Get().CmdResolver.OnDone();
            }
            else
            {
                PtApp.Get().CmdResolver.OnCancel();
            }

        }

        protected override void OnCommit()
        {            
            base.OnCommit();
        }

        private TextInstance m_Instance;
    }
}
