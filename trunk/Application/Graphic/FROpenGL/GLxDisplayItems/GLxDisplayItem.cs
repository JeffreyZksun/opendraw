
//////////////////////////////////////////////////////////////////////////
//
// Draw the graphics with OpenGL. This classes aren't used by the 
// system now.
// 
// Author: Sun Zhongkui
//
// History:
//  2008-2-17 Create
//
//////////////////////////////////////////////////////////////////////////

using CsGL.OpenGL;
using System;
using System.Runtime.InteropServices;

// Define the graphics drawn by OpenGL.
// It wrapped the drawing detail.

// [modified] - It indicates whether we changed the data of the item 
// since the last display list.
// Default value is false when construct the object.
// When we modify the data of the item, we should set it as true.

// Refactor these classes
// Finish - GLxDisplayItem GLxDisplayQuadric GLxDisplayDisk GLxDisplayCircle
// GLxDisplayLine GLxDisplayPoint

namespace FROpenGL.GLxDisplayItems
{
    public class GLxDisplayItem
    {

        #region Draw routines
        public virtual void Draw()
        {
            //	If you want you're custom object to be drawn, then override this function.

            //	The drawing of points is very simple, go through the point array, set
            //	the intensity, draw the point.

            //	This is the first function that must be called, DoPreDraw. It basicly
            //	sets up certain settings and organises the stack so that what you draw
            //	now doesn't interfere with what you will draw next. You actually 
            //	only need to draw it it returns true, when it returns false
            //	the object is actually drawn via a display list.
			
            if (DoPreDraw())
            {
                OnDraw();

                //	Finalize the drawing process.
                DoPostDraw();
            }

        }
        protected virtual void OnDraw(){}

        /// <summary>
        /// Classes derived from SceneObject can call this to perform common drawing
        /// operations, it pushes the modelview stack, set's attributes etc.
        /// </summary>
        /// <param name="gl"></param>
        /// <returns>True if the object must be drawn.</returns>
        protected internal virtual bool DoPreDraw()
        {
            //	DoPreDraw can go one of two ways.
            //	1. There is a valid displaylist, and 'modified' is false.
            //	This is ace, just call the display list. Fast as hell.
            //	2. There is no display list or 'modified' is true.
            //	This is OK, we do the slower version of the drawing while 
            //	compiling a display list at the same time. As long as the object
            //	doesn't get modified again, it'll just call the list next time.

            if (!m_bVisible) return false;

            if (displayList != 0 && m_modified != true)
            {
                GL.glCallList(displayList);
                return false;
            }

            //	If we have a list, destroy it. Then create a new one.
            if (displayList != 0)
                GL.glDeleteLists(displayList, 1);
            displayList = GL.glGenLists(1);

            //	From now on we are compiling the display list...
            GL.glNewList(displayList, GL.GL_COMPILE_AND_EXECUTE);

            GL.glPushAttrib(GL.GL_ALL_ATTRIB_BITS);

            GL.glDisable(GL.GL_LIGHTING);
            GL.glDepthFunc(GL.GL_ALWAYS);

            // Set color
            GL.glColor4f(m_ColorR, m_ColorG, m_ColorB, m_ColorAlpha);

            // bend
            if (m_bEnableBend)
            {
                GL.glEnable(GL.GL_BLEND);
                // Source color - the color will be drawn
                // Detination color - The existing color on the scene. 
                // If the color weight is greater than one, it will be set as one.
                // (Rs*Sr+Rd*Dr, Gs*Sg+Gd*Dg, Bs*Sb+Bd*Db, As*Sa+Ad*Da)
                GL.glBlendFunc(GL.GL_ONE, GL.GL_ONE);
            }

            return true;
        }

        /// <summary>
        /// This is always called after drawing, useful to restore the matrix stack etc.
        /// </summary>
        /// <param name="gl"></param>
        protected internal virtual void DoPostDraw()
        {
            if (m_bEnableBend)
                GL.glDisable(GL.GL_BLEND);

            //	Unset the material.
            GL.glPopAttrib();

            //	End the display list (this function only ever gets called
            //	if DoPreDraw returns true).
            GL.glEndList();
            m_modified = false;	//	Back in display list country until it changes again.
        }
        #endregion

        #region Set routines
        public void SetColor(float r, float g, float b, float a)
        {
            m_ColorR = r;
            m_ColorG = g;
            m_ColorB = b;
            m_ColorAlpha = a;

            m_modified = true;
        }

        public void EnableBend(uint sfactor, uint dfactor)
        {
            m_bEnableBend = true;
            m_sfactor = sfactor;
            m_dfactor = dfactor;
            m_modified = true;
        }

        public void EnableBend()
        {
            m_bEnableBend = true;
            m_modified = true;
        }

        public void DisableBend()
        {
            m_bEnableBend = false;
            m_modified = true;
        }

        public bool Visible
        {
            get { return m_bVisible; }
            set { m_bVisible = value; }
        }

        #endregion

        #region Data
        /// <summary>
        /// Any changes to the display item must set 'modified' to true, to make
        /// sure the display list is correctly recreated.
        /// </summary>
        protected bool m_modified = false;

        /// <summary>
        /// This is the OpenGL display list code.
        /// </summary>
        protected uint displayList = 0;

        // Save the color of the display item.
        protected float m_ColorR = 1;
        protected float m_ColorG = 0;
        protected float m_ColorB = 0;
        protected float m_ColorAlpha = 1;

        // Indicate whether we enable bend effect.
        private bool m_bEnableBend = false;
        private uint m_sfactor = GL.GL_SRC_ALPHA;
        private uint m_dfactor = GL.GL_ONE_MINUS_SRC_ALPHA;

        private bool m_bVisible = true;
        #endregion

    }
}
