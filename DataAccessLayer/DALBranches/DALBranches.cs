using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;

namespace DataAccessLayer.DALBranches
{
    public class DALBranches
    {
        public Branch selectBranchById(int branchId, int bankId)
        {
            try
            {
                string query = "SELECT * FROM tblBranches where id = @branchId and bankId = @bankId";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@branchId", branchId));
                branchParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(query, branchParams);
                if (dataSet != null)
                {
                    Branch branch = new Branch();
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
                        branch.id = 0;
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
        public List<Branch> selectBranchesByBankId(int pBankId)
        {
            try
            {
                List<Branch> lstBranches = new List<Branch>();
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
                                Branch branch = new Branch();
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
                            Branch branch = new Branch();
                            branch.id = 0;
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
        public ResultsEnum insertBranch(Branch branch)
        {
            try
            {
                string storedProc = "insert into tblBranches OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@active,@bankId)";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@enName", branch.enName));
                branchParams.Add(new SqlParameter("@arName", branch.arName));
                branchParams.Add(new SqlParameter("@active", branch.active));
                branchParams.Add(new SqlParameter("@bankId", branch.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, branchParams));
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
        public ResultsEnum updateBranch(Branch branch)
        {
            try
            {
                string storedProc = "update tblBranches set enName = @enName,arName = @arName,active = @active OUTPUT INSERTED.IDENTITYCOL where id = @id and bankId = @bankId";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@id", branch.id));
                branchParams.Add(new SqlParameter("@enName", branch.enName));
                branchParams.Add(new SqlParameter("@arName", branch.arName));
                branchParams.Add(new SqlParameter("@active", branch.active));
                branchParams.Add(new SqlParameter("@bankId", branch.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, branchParams));
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
        public ResultsEnum deleteCountersByBranchId(int branchId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_Delete_Allocate_Counter";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@branchId", branchId));
                branchParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, branchParams));
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
        public ResultsEnum deleteBranchById(int branchId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "delete from tblBranches OUTPUT DELETED.IDENTITYCOL where id = @id and bankId = @bankId";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@id", branchId));
                branchParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, branchParams));
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
