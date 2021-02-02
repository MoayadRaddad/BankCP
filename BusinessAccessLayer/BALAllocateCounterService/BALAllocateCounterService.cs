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
        public int insertAllocateCounterService(List<int> lstService, int counterId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (int serviceId in lstService)
                    {
                        int insertCheck = dALAllocateCounterService.insertAllocateCounterService(serviceId, counterId);
                        if (insertCheck == 0)
                        {
                            return 0;
                        }
                    }
                    scope.Complete();
                }
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int deleteAllocateCounterService(int allocateId)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                int insertCheck = dALAllocateCounterService.deleteAllocateCounterService(allocateId);
                if (insertCheck == 0)
                {
                    return 0;
                }
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
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
