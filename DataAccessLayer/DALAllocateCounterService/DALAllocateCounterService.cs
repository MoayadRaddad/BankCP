using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessLayer.DALAllocateCounterService
{
    public class DALAllocateCounterService
    {
        public List<AllocateCounterService> selectAllocateCounterService(int counterId, int bankId)
        {
            try
            {
                List<AllocateCounterService> lstAllocateCounterService = new List<AllocateCounterService>();
                string pquery = "sp_selectAllocateCounterService";
                List<SqlParameter> AllocateCounterServiceParams = new List<SqlParameter>();
                AllocateCounterServiceParams.Add(new SqlParameter("@counterId", counterId));
                AllocateCounterServiceParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapterProc(pquery, AllocateCounterServiceParams);
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        if (Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]) > 0)
                        {
                            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                            {
                                AllocateCounterService allocateCounterService = new AllocateCounterService();
                                allocateCounterService.id = Convert.ToInt32(dataRow["id"]);
                                allocateCounterService.counterId = Convert.ToInt32(dataRow["counterId"]);
                                allocateCounterService.serviceId = Convert.ToInt32(dataRow["serviceId"]);
                                allocateCounterService.serviceEnName = dataRow["serviceEnName"].ToString();
                                allocateCounterService.serviceArName = dataRow["serviceArName"].ToString();
                                lstAllocateCounterService.Add(allocateCounterService);
                            }
                        }
                        else
                        {
                            AllocateCounterService allocateCounterService = new AllocateCounterService();
                            allocateCounterService.id = Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]);
                            lstAllocateCounterService.Add(allocateCounterService);
                            return lstAllocateCounterService;
                        }
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
        public ResultsEnum insertAllocateCounterService(int serviceId, int counterId, int bankId)
        {
            try
            {
                string storedProc = "sp_insertAllocateCounterService";
                List<SqlParameter> allocateParams = new List<SqlParameter>();
                allocateParams.Add(new SqlParameter("@id", counterId));
                allocateParams.Add(new SqlParameter("@serviceId", serviceId));
                allocateParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, allocateParams));
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
        public ResultsEnum deleteAllocateCounterService(int allocateId, int counterId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "delete from tblAllocateCounterService OUTPUT DELETED.IDENTITYCOL where id = @id and counterId = @counterId and bankId = @bankId";
                List<SqlParameter> allocateParams = new List<SqlParameter>();
                allocateParams.Add(new SqlParameter("@id", allocateId));
                allocateParams.Add(new SqlParameter("@bankId", bankId));
                allocateParams.Add(new SqlParameter("@counterId", counterId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, allocateParams));
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
