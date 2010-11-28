
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
// a.	Architecture: It is a state command, use the target to trace the mouse behavior, and update the view transform. Like manipulator.
// c.	Workflow: Invoke command, show the orbit graphic, change the mouse shape. View observer.
// d.	Workflow: Target. Left button down->drag->change the view transformation->update the view-> end drag/ button up.
// e.	Algorithm: Use 1/3 of the diameter of the circle as the benchmark to get the rotate angle. Rotate based on the axis on the screen plane and upright to the mouse movement direction. 
// 
// History:
//  2009-3-27 Create
//
//////////////////////////////////////////////////////////////////////////

using FRPaint;

namespace FRPaint
{
    class DrViewRotateCmd : StateCommand
    {
        public DrViewRotateCmd()
            : base("DrViewRotateCmd", "&Rotate")
        { }

        protected override State GetInitialState()
        {
            return new RotateNodeState();
        }
    }
}
