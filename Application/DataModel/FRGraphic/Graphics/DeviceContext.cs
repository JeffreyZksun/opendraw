
//////////////////////////////////////////////////////////////////////////
//
// Encapsulate the graphic device. The graphic will be draw in this context.
// It supports screen only now. But can add print later.
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-31 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Diagnostics;

namespace FRGraphic
{
    public class DeviceContext
    {
        public DeviceContext(GraphicDevice device)
        {
            Debug.Assert(device != null);

            m_Device = device;
        }

        public GraphicDevice GraphicDevice
        {
            get { return m_Device; }
        }

        private GraphicDevice m_Device;
    }
}
