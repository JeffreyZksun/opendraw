//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2008-10-16 Create
//
//////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Text;
using FRPaint.Fragment;
using FRMath;
using FRGraphic;
using System.Diagnostics;
using FRModeling;

namespace FRPaint
{
    class ExtrusionGraphicState: SymbolGraphicState
    {
        public ExtrusionGraphicState(DataFragment fragment)
            : base(fragment)
        {
        }

        public override void GenerateGraphics(DisplayItemList DLList)
        {
            Debug.Assert(DLList != null);
            if (null == DLList) return;

            // create a 3D feature.
            DisplayItemList extDL = Extrusion.GetDisplayList();
            DLList.AddItem(extDL);

            Matrix44 trans = Matrix44.Identity;
            trans.SetScaling(2);
            trans.SetRotate(MathUtil.PI / 4, UnitVector.kXAxis, GePoint.kOrigin);
            trans.SetTranslation(new Vector(50, 50, 0));
            DLList.Transformation = trans;
        }
    }
}
