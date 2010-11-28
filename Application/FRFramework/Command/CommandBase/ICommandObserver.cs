
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-11-30 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRPaint
{
    // Observer
    // Use the observer to implement the relationship 
    // [button *--1 command]
    public interface ICommandObserver
    {
        void OnStart();
        void OnFinish();
    }
}
