using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessAccessLayer.BALCounter
{
    public class BALCounter
    {
        public BusinessObjects.Models.Counter selectCountersById(int counterId)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.selectCountersById(counterId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public List<BusinessObjects.Models.Counter> selectCountersByBranchId(int pBranchId)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.selectCountersByBranchId(pBranchId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Counter insertCounter(BusinessObjects.Models.Counter counter)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.insertCounter(counter);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Counter updateCounter(BusinessObjects.Models.Counter counter)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.updateCounter(counter);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteCounterById(int counterId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                    BusinessObjects.Models.ResultsEnum checkDelete = dALCounter.deleteAllocateCounterServiceByCounterId(counterId);
                    if (checkDelete == BusinessObjects.Models.ResultsEnum.notDeleted)
                    {
                        return BusinessObjects.Models.ResultsEnum.notDeleted;
                    }
                    checkDelete = dALCounter.deleteCounterById(counterId);
                    if (checkDelete == BusinessObjects.Models.ResultsEnum.notDeleted)
                    {
                        return BusinessObjects.Models.ResultsEnum.notDeleted;
                    }
                    scope.Complete();
                }
                return BusinessObjects.Models.ResultsEnum.deleted;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notDeleted;
            }
        }
    }
}
