using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALService
{
    public class BALService
    {
        public BusinessObjects.Models.Service selectServicesById(int ServiceId)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.selectServicesById(ServiceId);
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
        public BusinessObjects.Models.ResultsEnum deleteServiceById(int ServiceId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                    BusinessObjects.Models.ResultsEnum checkDelete = dALServices.deleteAllocateCounterServiceByServiceId(ServiceId);
                    if(checkDelete == BusinessObjects.Models.ResultsEnum.notDeleted)
                    {
                        return BusinessObjects.Models.ResultsEnum.notDeleted;
                    }
                    checkDelete = dALServices.deleteServiceById(ServiceId);
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
