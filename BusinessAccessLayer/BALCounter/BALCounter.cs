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
        public BusinessObjects.Models.Counter selectCounterById(int counterId, int bankId)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.selectCounterById(counterId, bankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public List<BusinessObjects.Models.Counter> selectCountersByBranchId(int pBranchId, int pBankId)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.selectCountersByBranchId(pBranchId, pBankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum insertCounter(BusinessObjects.Models.Counter counter, int bankId)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.insertCounter(counter, bankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notInserted;
            }
        }
        public BusinessObjects.Models.ResultsEnum updateCounter(BusinessObjects.Models.Counter counter, int bankId)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.updateCounter(counter, bankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notUpdated;
            }
        }
        public BusinessObjects.Models.sqlResultsEnum deleteCounterById(int counterId, int bankId, int branchId)
        {
            try
            {
                BusinessObjects.Models.sqlResultsEnum checkDelete;
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                    checkDelete = dALCounter.deleteAllocateCounterServiceByCounterId(counterId, bankId);
                    if (checkDelete == BusinessObjects.Models.sqlResultsEnum.success)
                    {
                        checkDelete = dALCounter.deleteCounterById(counterId, bankId, branchId);
                        if (checkDelete == BusinessObjects.Models.sqlResultsEnum.success)
                        {
                            scope.Complete();
                        }
                    }
                }
                return checkDelete;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.sqlResultsEnum.failed;
            }
        }
    }
}
