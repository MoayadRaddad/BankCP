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
        public int insertDeleteAllocateCounterService(List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService)
        {
            try
            {
                DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService dALAllocateCounterService = new DataAccessLayer.DALAllocateCounterService.DALAllocateCounterService();
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (BusinessObjects.Models.AllocateCounterService pAllocateCounterService in lstAllocateCounterService)
                    {
                        if(pAllocateCounterService.id != 0 && pAllocateCounterService.isDeleted == true)
                        {
                            int deleteCheck = dALAllocateCounterService.deleteAllocateCounterService(pAllocateCounterService);
                            if (deleteCheck == 0)
                            {
                                return 0;
                            }
                        }
                        else if (pAllocateCounterService.id == 0)
                        {
                            int insertCheck = dALAllocateCounterService.insertAllocateCounterService(pAllocateCounterService);
                            if (insertCheck == 0)
                            {
                                return 0;
                            }
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
