using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALService
{
    public class BALService
    {
        public BusinessObjects.Models.Service selectServiceById(int ServiceId, int bankId)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.selectServiceById(ServiceId, bankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
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
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum insertService(BusinessObjects.Models.Service service)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.insertService(service);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return BusinessObjects.Models.ResultsEnum.notInserted;
            }
        }
        public BusinessObjects.Models.ResultsEnum updateService(BusinessObjects.Models.Service Service)
        {
            try
            {
                DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                return dALServices.updateService(Service);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return BusinessObjects.Models.ResultsEnum.notUpdated;
            }
        }
        public BusinessObjects.Models.sqlResultsEnum deleteServiceById(int serviceId, int bankId)
        {
            try
            {
                BusinessObjects.Models.sqlResultsEnum checkDelete;
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALService.DALService dALServices = new DataAccessLayer.DALService.DALService();
                    checkDelete = dALServices.deleteAllocateCounterServiceByServiceId(serviceId, bankId);
                    if (checkDelete == BusinessObjects.Models.sqlResultsEnum.success)
                    {
                        checkDelete = dALServices.deleteServiceById(serviceId, bankId);
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
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return BusinessObjects.Models.sqlResultsEnum.failed;
            }
        }
    }
}
