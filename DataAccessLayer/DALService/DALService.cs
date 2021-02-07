using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALService
{
    public class DALService
    {
        public BusinessObjects.Models.Service selectServicesById(int serviceId)
        {
            try
            {
                string pquery = "SELECT * FROM tblService where id = @serviceId";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@serviceId", serviceId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, serviceParams);
                if (dataSet != null)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    BusinessObjects.Models.Service service = new BusinessObjects.Models.Service();
                    service.id = Convert.ToInt32(dataRow["id"]);
                    service.enName = dataRow["enName"].ToString();
                    service.arName = dataRow["arName"].ToString();
                    service.active = Convert.ToBoolean(dataRow["active"]);
                    service.maxNumOfTickets = Convert.ToInt32(dataRow["maxNumOfTickets"]);
                    service.bankId = Convert.ToInt32(dataRow["bankId"]);
                    return service;
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
        public List<BusinessObjects.Models.Service> selectServicesByBankId(int pBankId)
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
                        service.maxNumOfTickets = Convert.ToInt32(dataRow["maxNumOfTickets"]);
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
        public BusinessObjects.Models.Service insertService(BusinessObjects.Models.Service service)
        {
            try
            {
                string pquery = "insert into tblService OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@bankId,@active,@maxNumOfTickets)";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@enName", service.enName));
                serviceParams.Add(new SqlParameter("@arName", service.arName));
                serviceParams.Add(new SqlParameter("@bankId", service.bankId));
                serviceParams.Add(new SqlParameter("@active", service.active));
                serviceParams.Add(new SqlParameter("@maxNumOfTickets", service.maxNumOfTickets));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                service.id = Convert.ToInt32(dBHelper.executeScalar(pquery, serviceParams));
                return service;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Service updateService(BusinessObjects.Models.Service service)
        {
            try
            {
                string pquery = "update tblservice set enName = @enName,arName = @arName,active = @active,maxNumOfTickets = @maxNumOfTickets where id = @id";
                List<SqlParameter> screnParams = new List<SqlParameter>();
                screnParams.Add(new SqlParameter("@id", service.id));
                screnParams.Add(new SqlParameter("@enName", service.enName));
                screnParams.Add(new SqlParameter("@arName", service.arName));
                screnParams.Add(new SqlParameter("@active", service.active));
                screnParams.Add(new SqlParameter("@maxNumOfTickets", service.maxNumOfTickets));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screnParams);
                return service;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteAllocateCounterServiceByServiceId(int serviceId)
        {
            try
            {
                string pquery = string.Empty;
                pquery = "delete from tblAllocateCounterService where serviceId = @serviceId";
                List<SqlParameter> screnParams = new List<SqlParameter>();
                screnParams.Add(new SqlParameter("@serviceId", serviceId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screnParams);
                return BusinessObjects.Models.ResultsEnum.deleted;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notDeleted;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteServiceById(int serviceId)
        {
            try
            {
                string pquery = string.Empty;
                pquery = "delete from tblservice where id = @id";
                List<SqlParameter> screnParams = new List<SqlParameter>();
                screnParams.Add(new SqlParameter("@id", serviceId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screnParams);
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
