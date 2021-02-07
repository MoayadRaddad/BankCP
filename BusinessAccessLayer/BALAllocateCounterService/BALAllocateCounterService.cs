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
        public List<BusinessObjects.Models.AllocateCounterService> selectAllocateCounterService(int counterId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                return dALAllocateCounterService.selectAllocateCounterService(counterId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum insertAllocateCounterService(List<int> lstService, int counterId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (int serviceId in lstService)
                    {
                        BusinessObjects.Models.ResultsEnum insertCheck = dALAllocateCounterService.insertAllocateCounterService(serviceId, counterId);
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
        public BusinessObjects.Models.ResultsEnum deleteAllocateCounterService(int allocateId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                BusinessObjects.Models.ResultsEnum deleteCheck = dALAllocateCounterService.deleteAllocateCounterService(allocateId);
                return deleteCheck;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notDeleted;
            }
        }
        public List<BusinessObjects.Models.Service> selectNotAllocateServicesByBankId(int pBankId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                return dALAllocateCounterService.selectNotAllocateServicesByBankId(pBankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
