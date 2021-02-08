using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALService
{
    public class BALService
    {
        public BusinessObjects.Models.Service selectServiceById(int ServiceId)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.selectServiceById(ServiceId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public List<BusinessObjects.Models.Service> selectServicesByBankId(int pBankId)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALService = new DataAccessLayer.DALService.DALService();
                return dALService.selectServicesByBankId(pBankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Service insertService(BusinessObjects.Models.Service service)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.insertService(service);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Service updateService(BusinessObjects.Models.Service Service)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.updateService(Service);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteServiceById(int serviceId, int bankId)
        {
            try
            {
                BusinessObjects.Models.ResultsEnum checkDelete;
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                    checkDelete = dALServices.deleteAllocateCounterServiceByServiceId(serviceId, bankId);
                    if (checkDelete == BusinessObjects.Models.ResultsEnum.deleted)
                    {
                        checkDelete = dALServices.deleteServiceById(serviceId, bankId);
                        if (checkDelete == BusinessObjects.Models.ResultsEnum.deleted)
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
                return BusinessObjects.Models.ResultsEnum.notDeleted;
            }
        }
    }
}
