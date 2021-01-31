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
        public BusinessObjects.Models.Counter selectCountersById(int counterId)
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
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    BusinessObjects.Models.Counter counter = new BusinessObjects.Models.Counter();
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
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public List<BusinessObjects.Models.Counter> selectCountersByBranchId(int pBranchId)
        {
            try
            {
                List<BusinessObjects.Models.Counter> lstCounters = new List<BusinessObjects.Models.Counter>();
                string pquery = "SELECT * FROM tblCounters where branchId = @branchId";
                List<SqlParameter> ServiceParams = new List<SqlParameter>();
                ServiceParams.Add(new SqlParameter("@branchId", pBranchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, ServiceParams);
                if (dataSet != null)
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
                string pquery = "insert into tblCounters OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@active,@type,@branchId)";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@enName", counter.enName));
                counterParams.Add(new SqlParameter("@arName", counter.arName));
                counterParams.Add(new SqlParameter("@active", counter.active));
                counterParams.Add(new SqlParameter("@type", counter.type));
                counterParams.Add(new SqlParameter("@branchId", counter.branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                counter.id = Convert.ToInt32(dBHelper.executeScalar(pquery, counterParams));
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
                string pquery = "update tblCounters set enName = @enName,arName = @arName,active = @active,type = @type where id = @id";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@id", counter.id));
                counterParams.Add(new SqlParameter("@enName", counter.enName));
                counterParams.Add(new SqlParameter("@arName", counter.arName));
                counterParams.Add(new SqlParameter("@active", counter.active));
                counterParams.Add(new SqlParameter("@type", counter.type));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, counterParams);
                return counter;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public int deleteAllocateCounterServiceByCounterId(int counterId)
        {
            try
            {
                string pquery = string.Empty;
                pquery = "delete from tblAllocateCounterService where counterId = @counterId";
                List<SqlParameter> screnParams = new List<SqlParameter>();
                screnParams.Add(new SqlParameter("@counterId", counterId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screnParams);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int deleteCounterById(int counterId)
        {
            try
            {
                string pquery = string.Empty;
                pquery = "delete from tblCounters where id = @id";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@id", counterId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, counterParams);
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
