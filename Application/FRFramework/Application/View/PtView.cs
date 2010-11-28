
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-27 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

using FRContainer;
using FRMath;
using FROpenGL;
using FRGraphic;
using FRText;


// Platform dependence 
// PtView: From, Graphics

// View get data from document

namespace FRPaint
{
    public class PtView
    {
        #region Constructors and initialization
        public PtView(OpenGLSheet PicBox)
        {
            m_PictureBox = PicBox;

            // Add observers.
            m_ViewObserverList = new FRList<ViewPaintObserver>();
            m_ViewObserverList.Add(new NotesObserver());
            m_ViewObserverList.Add(new SelectionObserver());
            m_ViewObserverList.Add(new PreviewObserver());
            m_TransientObjObserver = new TransientObjectObserver();
            m_ViewObserverList.Add(m_TransientObjObserver);

            // Initialize the transform matrix.
            m_DeviceToWorld = Matrix44.Identity;
            m_DeviceToWorld.SetTranslation(GePoint.kOrigin - new GePoint(10, 10));

            // Hook up the events.
            m_PictureBox.MouseDown += new MouseEventHandler(OnMouseDown);
            m_PictureBox.MouseUp += new MouseEventHandler(OnMouseUp);
            m_PictureBox.MouseMove += new MouseEventHandler(OnMouseMove);
            m_PictureBox.MouseDoubleClick += new MouseEventHandler(OnMouseDoubleClick);
            m_PictureBox.MouseClick += new MouseEventHandler(OnMouseClick);
            m_PictureBox.MouseWheel += new MouseEventHandler(OnMouseWheel);

            m_PictureBox.DrawingRender += new System.Windows.Forms.PaintEventHandler(DrawingRender);
            m_PictureBox.DeviceType = OpenGLSheet.GraphicDeviceType.OPENGL;

            m_SelectionManager = new SelectionManager();
        }
        #endregion

        #region Mouse event
        public void OnMouseWheel(object sender, MouseEventArgs e)
        {
            EventContext MouseContext = new EventContext();

            SetMouseContext(e, MouseContext);

            MouseContext.Type = EventContext.EventType.eMouseWheel;
            MouseContext.Delta = e.Delta;

            PtApp.Get().OnEvent(MouseContext);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            EventContext MouseContext = new EventContext();

            SetMouseContext(e, MouseContext);

            if (e.Button == MouseButtons.Left)
                MouseContext.Type = EventContext.EventType.eLeftButtionClick;
            else
                MouseContext.Type = EventContext.EventType.eRightButtonClick;

            PtApp.Get().OnEvent(MouseContext);   
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {

            EventContext MouseContext = new EventContext();

            SetMouseContext(e, MouseContext);

            if (e.Button == MouseButtons.Left)
            {
                MouseContext.Type = EventContext.EventType.eLeftDoubleClick;
            }
            else if (e.Button == MouseButtons.Right)
            {
                MouseContext.Type = EventContext.EventType.eRightDoubleClick;
            }

            PtApp.Get().OnEvent(MouseContext);          

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            EventContext MouseContext = new EventContext();

            SetMouseContext(e, MouseContext);

            MouseContext.Type = EventContext.EventType.eMouseMove;
            PtApp.Get().OnEvent(MouseContext);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            EventContext MouseContext = new EventContext();

            SetMouseContext(e, MouseContext);

            if (e.Button == MouseButtons.Left)
                MouseContext.Type = EventContext.EventType.eLeftButtonDown;
            else if (e.Button == MouseButtons.Middle)
                MouseContext.Type = EventContext.EventType.eMiddleButtonDown;

            PtApp.Get().OnEvent(MouseContext);
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {

            EventContext MouseContext = new EventContext();

            SetMouseContext(e, MouseContext);

            if (e.Button == MouseButtons.Left)
                MouseContext.Type = EventContext.EventType.eLeftButtonUp;
            else if (e.Button == MouseButtons.Middle)
                MouseContext.Type = EventContext.EventType.eMiddleButtonUp;

            PtApp.Get().OnEvent(MouseContext);
        }

        private void SetMouseContext(MouseEventArgs e, EventContext MouseContext)
        {
            Debug.Assert(MouseContext != null);

            GePoint mouseDevicePoint = new GePoint(e.X, e.Y);
            MouseContext.SetMouseDevicePoint(mouseDevicePoint);
            //MouseContext.m_MousePoint = DeviceToWorld(mouseDevicePoint); // new GePoint(e.X, e.Y);  
            
            MouseButtons button = e.Button;
            if (button == MouseButtons.Left)
                MouseContext.SetButtonType(EventContext.ButtonType.eLeft);
            else if (button == MouseButtons.Right)
                MouseContext.SetButtonType(EventContext.ButtonType.eRight);
            else if (button == MouseButtons.Middle)
                MouseContext.SetButtonType(EventContext.ButtonType.eMiddle);
            else
                MouseContext.SetButtonType(EventContext.ButtonType.eNone);

        }
        #endregion

        #region Draw Graphics
        private void DrawingRender(object sender, PaintEventArgs e)
        {
            if (m_PictureBox == null) return;

            // Draw the graphics
            DeviceContext DeviceCtx = new DeviceContext(GraphicDevice);
            foreach (ViewPaintObserver item in m_ViewObserverList)
                item.OnPaintDrawing(DeviceCtx);
        } 

        public void UpdateView()
        {
            if (m_PictureBox == null) return;

            m_PictureBox.Invalidate();
        }
        
        #endregion

        #region Observer
        public void AddObserver(ViewPaintObserver observer)
        {
            m_ViewObserverList.Add(observer);
        }

        public void RemoveObserver(ViewPaintObserver observer)
        {
            m_ViewObserverList.Remove(observer);
        }
        #endregion

        #region Graphic Device related

        public GraphicDevice GraphicDevice
        {
            get
            {
                Debug.Assert(m_PictureBox != null);
                return m_PictureBox.GetGraphicDevice();
            }
        }

        // This method should be removed later        
        public Graphics GetGDIGraphics()
        {
            if (m_PictureBox == null) return null;
            return m_PictureBox.CreateGraphics();
        }

        public GeRectangle GetRichStringRange(RichString str)
        {
            Graphics gp = GetGDIGraphics();

            // Measure string.
            SizeF stringSize = new SizeF();

            FontManager manager = PtApp.ActiveDocument.GetFontManager();

            Font StrFont = null;
            if (str.FontID != -1)
                StrFont = manager.GetFont(str.FontID);
            else
                StrFont = manager.GetApplicationDefaultFont();

            // Device coordinates
            stringSize = gp.MeasureString(
                   str.GetString(), StrFont);

            // Defect - We should convert to World coordinates here.
            GeRectangle rec = new GeRectangle(new GePoint(0, 0)
                , new GePoint(stringSize.Width, stringSize.Height));
            return rec;
        }

        #endregion

        #region Cursor shape.
        public void SetCursorShape(string CursorResource)
        {
            try
            {
                Cursor newCursor = PtApp.Instance.CreateCursor(CursorResource);
                m_PictureBox.Cursor = newCursor;
            }
            catch
            {
                Debug.Assert(false, "Error when set cursor");
            }

        }
        #endregion

        #region Select
        public void Select(PtSelectContext sltCtx)
        {
            PtDocument doc = PtApp.GetActiveDocument();
            FRList<GraphicNode> NodeList = doc.GetGraphicNodes();

            // Find 
            foreach (GraphicNode NodeItem in NodeList)
            {
                sltCtx.ResetSelectionFlag();
                NodeItem.Qualify(sltCtx);

                if (!sltCtx.CurrentSelectionSet.Empty() && !sltCtx.MultiSelect)
                    break;
            }
        }
        #endregion

        #region Attributes
        public TransientObjectObserver TransientObjectObserver
        {
            get { return m_TransientObjObserver; }
        }

        public SelectionManager SelectionMgr
        {
            get { return m_SelectionManager; }
        }
        #endregion

        #region Coordinate change
        public ViewTransformManipulator GetTranslationManipulator()
        {
            if(null == m_TransManipulator)
            {
                m_TransManipulator = new ViewTransformManipulator(m_DeviceToWorld);
            }
            return m_TransManipulator;
        }

        public GePoint DeviceToWorld(GePoint devicePoint)
        {
            Debug.Assert(devicePoint != null);
            if (null == devicePoint) return null;

            return m_DeviceToWorld * devicePoint;
        }

        public GePoint WorldToDevice(GePoint worldPoint)
        {
            Debug.Assert(worldPoint != null);
            if (null == worldPoint) return null;

            Matrix44 inverse = m_DeviceToWorld.Inverse as Matrix44;
            if(inverse != null)
                return inverse * worldPoint;

            return null;
        }

        public double GetWorldToDeviceScale()
        {
            return 1 / m_DeviceToWorld.GetAllScaling();
        }

        public Matrix44 GetWorldToDeviceMatrix()
        {
            Matrix44 inverse = m_DeviceToWorld.Inverse as Matrix44;
            return inverse;
        }

        public Matrix44 DeviceToWorldMatrix
        {
            get { return m_DeviceToWorld; }
            set { m_DeviceToWorld = value; }
        }
        #endregion

        #region Graphic device - Device coordination
        // Get the 
        public GePoint ViewCenter
        {
            get
            {
                GePoint deviceCenter = new GePoint(m_PictureBox.Width / 2
                    , m_PictureBox.Height / 2, 0);
                return DeviceToWorld(deviceCenter);
            }
        }

        public double ViewWidth
        {
            get { return m_PictureBox.Width; }
        }

        public double ViewHeight
        {
            get { return m_PictureBox.Height; }
        }
        #endregion

        #region Data
        private OpenGLSheet m_PictureBox;

        private FRList<ViewPaintObserver> m_ViewObserverList;

        private TransientObjectObserver m_TransientObjObserver;

        private Matrix44 m_DeviceToWorld;
        private ViewTransformManipulator m_TransManipulator;

        private SelectionManager m_SelectionManager;
        #endregion
    };
}
