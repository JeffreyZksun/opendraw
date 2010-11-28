using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


// The frame is responsible for delegate the message

namespace FRPaint
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();

            this.MouseWheel += new MouseEventHandler(OnMouseWheel);

            InitializeApplication();
        }

        private void InitializeApplication()
        {
            drawBox.Width = this.Width;
            drawBox.Height = this.Height;

            PtApp.Get().Initialize(this, GetDrawBox(), StatusLabel);
        }

        public FROpenGL.OpenGLSheet GetDrawBox()
        {
            return drawBox;
        }

        public void SetStatus(string strStatus)
        {
            StatusLabel.Items[0].Text = strStatus;
        }
       
        
        private void MainFrame_Load(object sender, EventArgs e)
        {
            ResizeView();
        }

        // OPT - this code should be optimized.
        private void ResizeView()
        {
            SketchSheets.Left = -3;
            SketchSheets.Top = 25;// +22;
            SketchSheets.Width = this.Width - 3;
            SketchSheets.Height = this.Height - StatusLabel.Height - 57 - 22;

            drawBox.Width = SketchSheets.Width - 80;
            drawBox.Height = SketchSheets.Height - 80;
        }    

        private void MainFrame_SizeChanged(object sender, EventArgs e)
        {
            ResizeView();
        }
        

        private void drawBox_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if(PtApp.ActiveView != null)
            {
                PtApp.ActiveView.OnMouseWheel(sender, e);
            }
        }
        
    }
}