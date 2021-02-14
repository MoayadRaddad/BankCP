using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessAccessLayer.BALAllocateCounterService
{
    public class BALAllocateCounterService
    {
        public List<BusinessObjects.Models.AllocateCounterService> selectAllocateCounterService(int counterId, int bankId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                return dALAllocateCounterService.selectAllocateCounterService(counterId, bankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum insertAllocateCounterService(List<int> lstService, int counterId, int bankId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (int serviceId in lstService)
                    {
                        BusinessObjects.Models.ResultsEnum insertCheck = dALAllocateCounterService.insertAllocateCounterService(serviceId, counterId, bankId);
                        if (insertCheck == BusinessObjects.Models.ResultsEnum.notInserted)
                        {
                            return BusinessObjects.Models.ResultsEnum.notInserted;
                        }
                    }
                    scope.Complete();
                }
                return BusinessObjects.Models.ResultsEnum.inserted;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notInserted;
            }
        }
        public BusinessObjects.Models.sqlResultsEnum deleteAllocateCounterService(int allocateId, int counterId, int bankId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                return dALAllocateCounterService.deleteAllocateCounterService(allocateId, counterId, bankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.sqlResultsEnum.failed;
            }
        }
    }
}
