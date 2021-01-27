using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Text;

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
        public int deleteServiceById(int ServiceId)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.deleteServiceById(ServiceId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
