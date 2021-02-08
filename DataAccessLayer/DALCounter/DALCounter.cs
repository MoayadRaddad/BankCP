using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALCounter
{
    public class DALCounter
    {
        public BusinessObjects.Models.Counter selectCounterById(int counterId)
        {
            try
            {
                string pquery = "SELECT * FROM tblCounters where id = @counterId";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@counterId", counterId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, counterParams);
                if (dataSet != null)
                {
                    BusinessObjects.Models.Counter counter = new BusinessObjects.Models.Counter();
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        DataRow dataRow = dataSet.Tables[0].Rows[0];
                        counter.id = Convert.ToInt32(dataRow["id"]);
                        counter.enName = dataRow["enName"].ToString();
                        counter.arName = dataRow["arName"].ToString();
                        counter.active = Convert.ToBoolean(dataRow["active"]);
                        counter.type = dataRow["type"].ToString();
                        counter.branchId = Convert.ToInt32(dataRow["branchId"]);
                        return counter;
                    }
                    else
                    {
                        counter.id = -1;
                        return counter;
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
        public List<BusinessObjects.Models.Counter> selectCountersByBranchId(int pBranchId, int pBankId)
        {
            try
            {
                List<BusinessObjects.Models.Counter> lstCounters = new List<BusinessObjects.Models.Counter>();
                string pquery = "sp_selectCountersByBranchId";
                List<SqlParameter> ServiceParams = new List<SqlParameter>();
                ServiceParams.Add(new SqlParameter("@branchId", pBranchId));
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
                                BusinessObjects.Models.Counter counter = new BusinessObjects.Models.Counter();
                                counter.id = Convert.ToInt32(dataRow["id"]);
                                counter.enName = dataRow["enName"].ToString();
                                counter.arName = dataRow["arName"].ToString();
                                counter.active = Convert.ToBoolean(dataRow["active"]);
                                counter.type = dataRow["type"].ToString();
                                counter.branchId = Convert.ToInt32(dataRow["branchId"]);
                                lstCounters.Add(counter);
                            }
                        }
                        else
                        {
                            BusinessObjects.Models.Counter counter = new BusinessObjects.Models.Counter();
                            counter.id = -1;
                            lstCounters.Add(counter);
                            return lstCounters;
                        }
                    }
                    return lstCounters;
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
        public BusinessObjects.Models.Counter insertCounter(BusinessObjects.Models.Counter counter)
        {
            try
            {
                string pquery = "sp_insertCounter";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@enName", counter.enName));
                counterParams.Add(new SqlParameter("@arName", counter.arName));
                counterParams.Add(new SqlParameter("@active", counter.active));
                counterParams.Add(new SqlParameter("@type", counter.type));
                counterParams.Add(new SqlParameter("@branchId", counter.branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                counter.id = Convert.ToInt32(dBHelper.executeScalarProc(pquery, counterParams));
                return counter;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Counter updateCounter(BusinessObjects.Models.Counter counter)
        {
            try
            {
                string storedProc = "sp_updateCounter";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@id", counter.id));
                counterParams.Add(new SqlParameter("@enName", counter.enName));
                counterParams.Add(new SqlParameter("@arName", counter.arName));
                counterParams.Add(new SqlParameter("@active", counter.active));
                counterParams.Add(new SqlParameter("@type", counter.type));
                counterParams.Add(new SqlParameter("@branchId", counter.branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                counter.id = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, counterParams));
                return counter;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteAllocateCounterServiceByCounterId(int counterId, int branchId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_deleteAllocateCounterServiceByCounterId";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@counterId", counterId));
                counterParams.Add(new SqlParameter("@branchId", branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int check = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, counterParams));
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
        public BusinessObjects.Models.ResultsEnum deleteCounterById(int counterId, int branchId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_deleteCounter";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@id", counterId));
                counterParams.Add(new SqlParameter("@branchId", branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int check = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, counterParams));
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
