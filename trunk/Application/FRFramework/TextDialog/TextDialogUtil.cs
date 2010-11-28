
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Windows.Forms;

namespace FRPaint
{
    public class TextDialogUtil
    {
        // If click OK, return true. Else, return Cancel.
        public static bool ShowDialog(TextDialogData DlgData)
        {

            // show dialog
            TextDialog dlg = new TextDialog(DlgData);
            DialogResult ret = dlg.ShowDialog();

            if (ret == DialogResult.OK)
            {
                return true;
            }
            else
                return false;
        }
    }
}
