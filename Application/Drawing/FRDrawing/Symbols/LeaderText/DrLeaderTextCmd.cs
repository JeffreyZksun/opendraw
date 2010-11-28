
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-12-11 Create
//
//////////////////////////////////////////////////////////////////////////

using FRMath;
using FRText;

using FRPaint.Fragment;

namespace FRPaint
{
    class DrLeaderTextCmd : DMStateCommand
    {
        public DrLeaderTextCmd()
            : base("DrLeaderTextCmd", "&LeaderText")
        {
            m_Instance = null;
        }

        #region State machine callbacks.
        public override CommandState GetNextState()
        {
            if (!m_StateMgr.DataItems.Empty())
            {
                PositionDataItem Pos = (PositionDataItem)m_StateMgr.DataItems.Back();
                m_Instance.AddLastPointBefore(Pos.Point);
            }

            return new PointInferenceState();
        }

        // For updating real time.
        public override void UpdateGraphics(GePoint MousePosition)
        {
            m_Instance.SetLastPoint(MousePosition);
            m_Instance.Compute();
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
        #endregion

        #region Command events
        protected override bool OnExcute()
        {
            SetStatus("Leader Text");


            return base.OnExcute();
        }

        protected override GraphicNode GetNode()
        {
            DataFragment fragment = PtApp.ActiveDocument.Database.GetFragment();
            m_Instance = new LeaderTextInstance(fragment, new PointState(fragment));
            LeaderTextNode Node = new LeaderTextNode(m_Instance);

            return Node;
        }
        #endregion

        private LeaderTextInstance m_Instance;
    }
}
