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
        public BusinessObjects.Models.Branch selectBranchesById(int branchId)
        {
            try
            {
                string pquery = "SELECT * FROM tblBranches where id = @branchId";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@branchId", branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, branchParams);
                if (dataSet != null)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    BusinessObjects.Models.Branch branch = new BusinessObjects.Models.Branch();
                    branch.id = Convert.ToInt32(dataRow["id"]);
                    branch.enName = dataRow["enName"].ToString();
                    branch.arName = dataRow["arName"].ToString();
                    branch.active = Convert.ToBoolean(dataRow["active"]);
                    branch.bankId = Convert.ToInt32(dataRow["bankId"]);
                    return branch;
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
                string pquery = "SELECT * FROM tblBranches where bankId = @bankId";
                List<SqlParameter> branchParams = new List<SqlParameter>();
                branchParams.Add(new SqlParameter("@bankId", pBankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, branchParams);
                if (dataSet != null)
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
                string pquery = "insert into tblBranches OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@active,@bankId)";
                List<SqlParameter> bankParams = new List<SqlParameter>();
                bankParams.Add(new SqlParameter("@enName", branch.enName));
                bankParams.Add(new SqlParameter("@arName", branch.arName));
                bankParams.Add(new SqlParameter("@active", branch.active));
                bankParams.Add(new SqlParameter("@bankId", branch.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                branch.id = Convert.ToInt32(dBHelper.executeScalar(pquery, bankParams));
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
                string pquery = "update tblBranches set enName = @enName,arName = @arName,active = @active where id = @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", branch.id));
                screenParams.Add(new SqlParameter("@enName", branch.enName));
                screenParams.Add(new SqlParameter("@arName", branch.arName));
                screenParams.Add(new SqlParameter("@active", branch.active));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                return branch;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public int deleteBranchById(int branchId)
        {
            try
            {
                string pquery = string.Empty;
                pquery = "delete from tblBranches where id = @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int deleteCountersByBranchId(int branchId)
        {
            try
            {
                string pquery = string.Empty;
                pquery = "sp_Delete_Allocate_Counter";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@branchId", branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQueryProc(pquery, screenParams);
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
