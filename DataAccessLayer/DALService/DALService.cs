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
        public BusinessObjects.Models.Service selectServiceById(int serviceId)
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
                    BusinessObjects.Models.Service service = new BusinessObjects.Models.Service();
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
                        service.id = -1;
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
        public List<BusinessObjects.Models.Service> selectServicesByBankId(int pBankId)
        {
            try
            {
                List<BusinessObjects.Models.Service> lstServices = new List<BusinessObjects.Models.Service>();
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
                                BusinessObjects.Models.Service service = new BusinessObjects.Models.Service();
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
                            BusinessObjects.Models.Service service = new BusinessObjects.Models.Service();
                            service.id = -1;
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
        public BusinessObjects.Models.Service insertService(BusinessObjects.Models.Service service)
        {
            try
            {
                string pquery = "sp_insertService";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@enName", service.enName));
                serviceParams.Add(new SqlParameter("@arName", service.arName));
                serviceParams.Add(new SqlParameter("@bankId", service.bankId));
                serviceParams.Add(new SqlParameter("@active", service.active));
                serviceParams.Add(new SqlParameter("@maxNumOfTickets", service.maxNumOfTickets));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                service.id = Convert.ToInt32(dBHelper.executeScalarProc(pquery, serviceParams));
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
                string storedProc = "sp_updateService";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@id", service.id));
                serviceParams.Add(new SqlParameter("@enName", service.enName));
                serviceParams.Add(new SqlParameter("@arName", service.arName));
                serviceParams.Add(new SqlParameter("@active", service.active));
                serviceParams.Add(new SqlParameter("@maxNumOfTickets", service.maxNumOfTickets));
                serviceParams.Add(new SqlParameter("@bankId", service.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                service.id = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, serviceParams));
                return service;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteAllocateCounterServiceByServiceId(int serviceId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_deleteAllocateCounterServiceByServiceId";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@serviceId", serviceId));
                serviceParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int check = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, serviceParams));
                if (check != -1)
                {
                    if (check != 0)
                    {
                        return BusinessObjects.Models.ResultsEnum.deleted;
                    }
                    else
                    {
                        return BusinessObjects.Models.ResultsEnum.notDeleted;
                    }
                }
                else
                {
                    return BusinessObjects.Models.ResultsEnum.notAuthorize;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notDeleted;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteServiceById(int serviceId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_deleteService";
                List<SqlParameter> serviceParams = new List<SqlParameter>();
                serviceParams.Add(new SqlParameter("@id", serviceId));
                serviceParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int check = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, serviceParams));
                if (check != -1)
                {
                    if (check != 0)
                    {
                        return BusinessObjects.Models.ResultsEnum.deleted;
                    }
                    else
                    {
                        return BusinessObjects.Models.ResultsEnum.notDeleted;
                    }
                }
                else
                {
                    return BusinessObjects.Models.ResultsEnum.notAuthorize;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notDeleted;
            }
        }
    }
}
