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
        public List<BusinessObjects.Models.AllocateCounterService> selectAllocateCounterService(int counterId, int bankId)
        {
            try
            {
                List<BusinessObjects.Models.AllocateCounterService> lstAllocateCounterService = new List<BusinessObjects.Models.AllocateCounterService>();
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
                                BusinessObjects.Models.AllocateCounterService allocateCounterService = new BusinessObjects.Models.AllocateCounterService();
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
                            BusinessObjects.Models.AllocateCounterService allocateCounterService = new BusinessObjects.Models.AllocateCounterService();
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
        public BusinessObjects.Models.ResultsEnum insertAllocateCounterService(int serviceId, int counterId, int bankId)
        {
            try
            {
                string pquery = "sp_insertAllocateCounterService";
                List<SqlParameter> allocateParams = new List<SqlParameter>();
                allocateParams.Add(new SqlParameter("@counterId", counterId));
                allocateParams.Add(new SqlParameter("@serviceId", serviceId));
                allocateParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                if (Convert.ToInt32(dBHelper.executeScalarProc(pquery, allocateParams)) != 0)
                {
                    return BusinessObjects.Models.ResultsEnum.inserted;
                }
                else
                {
                    return BusinessObjects.Models.ResultsEnum.notInserted;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notInserted;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteAllocateCounterService(int allocateId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_deleteAllocateCounterService";
                List<SqlParameter> allocateParams = new List<SqlParameter>();
                allocateParams.Add(new SqlParameter("@id", allocateId));
                allocateParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int check = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, allocateParams));
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
