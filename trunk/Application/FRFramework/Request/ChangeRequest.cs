
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// This class will control all the changes centrally.
// All the data model change / transacted change should be 
// done in the request.
// This class will be respinsible for transaction and transcript.
//
// History:
//  2009-3-27 Create
//
//////////////////////////////////////////////////////////////////////////

namespace FRPaint
{
    public class ChangeRequest
    {
        public void Execute()
        {
            OnWriteTranscript();
            StartTransaction();
            if (OnExecute())
                CommitTransaction();
            else
                AbortTransaction();
        }

        #region virtual funcions should be implemented by the derived classes
        protected virtual bool OnExecute()
        {
            // We don't want to save an empty transaction,
            // when the derived class do nothing.
            return false;
        }

        protected virtual void OnWriteTranscript()
        {

        }
        #endregion

        #region Transaction
        private void StartTransaction()
        {

        }

        private void CommitTransaction()
        {

        }
        private void AbortTransaction()
        {

        }
        #endregion
    }
}
