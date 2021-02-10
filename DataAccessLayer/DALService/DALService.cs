using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using BusinessObjects.Models;

namespace DataAccessLayer.DALService
{
    public class DALService
    {
        public Service selectServiceById(int serviceId, int bankId)
        {
            try
            {
                string pquery = "SELECT * FROM tblService where id = @serviceId and bankId = @bankId";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@serviceId", serviceId));
                serviceParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, serviceParams);
                if (dataSet != null)
                {
                    Service service = new Service();
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        DataRow dataRow = dataSet.Tables[0].Rows[0];
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
                        service.id = 0;
                        return service;
                    }
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
        public List<Service> selectServicesByBankId(int pBankId)
        {
            try
            {
                List<Service> lstServices = new List<Service>();
                string pquery = "sp_selectServicesByBankId";
                List<SqlParameter> ServiceParams = new List<SqlParameter>();
                ServiceParams.Add(new SqlParameter("@bankId", pBankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapterProc(pquery, ServiceParams);
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        if (Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]) != 0)
                        {
                            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                            {
                                Service service = new Service();
                                service.id = Convert.ToInt32(dataRow["id"]);
                                service.enName = dataRow["enName"].ToString();
                                service.arName = dataRow["arName"].ToString();
                                service.active = Convert.ToBoolean(dataRow["active"]);
                                service.maxNumOfTickets = Convert.ToInt32(dataRow["maxNumOfTickets"]);
                                service.bankId = Convert.ToInt32(dataRow["bankId"]);
                                lstServices.Add(service);
                            }
                        }
                        else
                        {
                            Service service = new Service();
                            service.id = 0;
                            lstServices.Add(service);
                            return lstServices;
                        }
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
        public ResultsEnum insertService(Service service)
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
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(pquery, serviceParams));
                if ((sqlResultsEnum)returnValue == sqlResultsEnum.failed)
                {
                    return ResultsEnum.deleted;
                }
                else
                {
                    return ResultsEnum.inserted;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return ResultsEnum.notInserted;
            }
        }
        public ResultsEnum updateService(Service service)
        {
            try
            {
                string storedProc = "update tblservice set enName = @enName,arName = @arName,active = @active,maxNumOfTickets = @maxNumOfTickets OUTPUT INSERTED.IDENTITYCOL where id = @id and bankId = @bankId";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@id", service.id));
                serviceParams.Add(new SqlParameter("@enName", service.enName));
                serviceParams.Add(new SqlParameter("@arName", service.arName));
                serviceParams.Add(new SqlParameter("@active", service.active));
                serviceParams.Add(new SqlParameter("@maxNumOfTickets", service.maxNumOfTickets));
                serviceParams.Add(new SqlParameter("@bankId", service.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, serviceParams));
                if ((sqlResultsEnum)returnValue == sqlResultsEnum.failed)
                {
                    return ResultsEnum.deleted;
                }
                else
                {
                    return ResultsEnum.updated;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return ResultsEnum.notUpdated;
            }
        }
        public ResultsEnum deleteAllocateCounterServiceByServiceId(int serviceId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "delete from tblAllocateCounterService OUTPUT DELETED.IDENTITYCOL where serviceId = @serviceId and bankId = @bankId";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@serviceId", serviceId));
                serviceParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, serviceParams));
                return ResultsEnum.deleted;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return ResultsEnum.notDeleted;
            }
        }
        public ResultsEnum deleteServiceById(int serviceId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "delete from tblservice OUTPUT DELETED.IDENTITYCOL where id = @id and bankId = @bankId";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@id", serviceId));
                serviceParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, serviceParams));
                if ((sqlResultsEnum)returnValue == sqlResultsEnum.failed)
                {
                    return ResultsEnum.notDeleted;
                }
                else
                {
                    return ResultsEnum.deleted;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return ResultsEnum.notDeleted;
            }
        }
    }
}