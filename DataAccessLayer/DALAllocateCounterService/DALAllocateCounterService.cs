using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALAllocateCounterService
{
    public class DALAllocateCounterService
    {
        public List<BusinessObjects.Models.AllocateCounterService> selectAllocateCounterService(int counterId)
        {
            try
            {
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = new List<BusinessObjects.Models.AllocateCounterService>();
                string pquery = "SELECT tblAllocateCounterService.*,tblService.enName as serviceEnName,tblService.arName as serviceArName FROM tblAllocateCounterService inner join tblService on tblAllocateCounterService.serviceId = tblService.id where tblAllocateCounterService.counterId = @counterId";
                List<SqlParameter> AllocateCounterServiceParams = new List<SqlParameter>();
                AllocateCounterServiceParams.Add(new SqlParameter("@counterId", counterId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, AllocateCounterServiceParams);
                if (dataSet != null)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        BusinessObjects.Models.AllocateCounterService allocateCounterService = new BusinessObjects.Models.AllocateCounterService();
                        allocateCounterService.id = Convert.ToInt32(dataRow["id"]);
                        allocateCounterService.counterId = Convert.ToInt32(dataRow["counterId"]);
                        allocateCounterService.serviceId = Convert.ToInt32(dataRow["serviceId"]);
                        allocateCounterService.serviceEnName = dataRow["serviceEnName"].ToString();
                        allocateCounterService.serviceArName = dataRow["serviceArName"].ToString();
                        lstAllocateCounterService.Add(allocateCounterService);
                    }
                    return lstAllocateCounterService;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public int insertAllocateCounterService(int serviceId, int counterId)
        {
            try
            {
                string pquery = "insert into tblAllocateCounterService OUTPUT INSERTED.IDENTITYCOL  values (@counterId,@serviceId)";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@counterId", counterId));
                screenParams.Add(new SqlParameter("@serviceId", serviceId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                if(Convert.ToInt32(dBHelper.executeScalar(pquery, screenParams)) != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
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
                List<BusinessObjects.Models.Service> lstServices = new List<BusinessObjects.Models.Service>();
                string pquery = "SELECT * FROM tblService where bankId = @bankId";
                List<SqlParameter> ServiceParams = new List<SqlParameter>();
                ServiceParams.Add(new SqlParameter("@bankId", pBankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, ServiceParams);
                if (dataSet != null)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        BusinessObjects.Models.Service service = new BusinessObjects.Models.Service();
                        service.id = Convert.ToInt32(dataRow["id"]);
                        service.enName = dataRow["enName"].ToString();
                        service.arName = dataRow["arName"].ToString();
                        service.active = Convert.ToBoolean(dataRow["active"]);
                        service.tickets = Convert.ToInt32(dataRow["tickets"]);
                        service.bankId = Convert.ToInt32(dataRow["bankId"]);
                        lstServices.Add(service);
                    }
                    return lstServices;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public int deleteAllocateCounterService(int allocateId)
        {
            try
            {
                string pquery = string.Empty;
                pquery = "delete from tblAllocateCounterService where id = @id";
                List<SqlParameter> allocateParams = new List<SqlParameter>();
                allocateParams.Add(new SqlParameter("@id", allocateId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, allocateParams);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
