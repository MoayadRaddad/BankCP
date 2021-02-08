using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALBranches
{
    public class DALBranches
    {
        public BusinessObjects.Models.Branch selectBranchById(int branchId)
        {
            try
            {
                string query = "SELECT * FROM tblBranches where id = @branchId";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@branchId", branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(query, branchParams);
                if (dataSet != null)
                {
                    BusinessObjects.Models.Branch branch = new BusinessObjects.Models.Branch();
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        DataRow dataRow = dataSet.Tables[0].Rows[0];
                        branch.id = Convert.ToInt32(dataRow["id"]);
                        branch.enName = dataRow["enName"].ToString();
                        branch.arName = dataRow["arName"].ToString();
                        branch.active = Convert.ToBoolean(dataRow["active"]);
                        branch.bankId = Convert.ToInt32(dataRow["bankId"]);
                        return branch;
                    }
                    else
                    {
                        branch.id = -1;
                        return branch;
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
        public List<BusinessObjects.Models.Branch> selectBranchesByBankId(int pBankId)
        {
            try
            {
                List<BusinessObjects.Models.Branch> lstBranches = new List<BusinessObjects.Models.Branch>();
                string storedProc = "sp_selectBranchesByBankId";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@bankId", pBankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapterProc(storedProc, branchParams);
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        if (Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]) != 0)
                        {
                            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                            {
                                BusinessObjects.Models.Branch branch = new BusinessObjects.Models.Branch();
                                branch.id = Convert.ToInt32(dataRow["id"]);
                                branch.enName = dataRow["enName"].ToString();
                                branch.arName = dataRow["arName"].ToString();
                                branch.active = Convert.ToBoolean(dataRow["active"]);
                                branch.bankId = Convert.ToInt32(dataRow["bankId"]);
                                lstBranches.Add(branch);
                            }
                        }
                        else
                        {
                            BusinessObjects.Models.Branch branch = new BusinessObjects.Models.Branch();
                            branch.id = -1;
                            lstBranches.Add(branch);
                            return lstBranches;
                        }
                    }
                    return lstBranches;
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
        public BusinessObjects.Models.Branch insertBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                string storedProc = "sp_insertBranch";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@enName", branch.enName));
                branchParams.Add(new SqlParameter("@arName", branch.arName));
                branchParams.Add(new SqlParameter("@active", branch.active));
                branchParams.Add(new SqlParameter("@bankId", branch.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                branch.id = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, branchParams ));
                return branch;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Branch updateBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                string storedProc = "sp_updateBranch";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@id", branch.id));
                branchParams.Add(new SqlParameter("@enName", branch.enName));
                branchParams.Add(new SqlParameter("@arName", branch.arName));
                branchParams.Add(new SqlParameter("@active", branch.active));
                branchParams.Add(new SqlParameter("@bankId", branch.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                branch.id = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, branchParams));
                return branch;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteBranchById(int branchId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_deleteBranch";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@id", branchId));
                branchParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int check = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, branchParams));
                if(check != -1)
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
        public BusinessObjects.Models.ResultsEnum deleteCountersByBranchId(int branchId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_Delete_Allocate_Counter";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@branchId", branchId));
                branchParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int check = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, branchParams));
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
