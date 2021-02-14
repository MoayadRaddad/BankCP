using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessAccessLayer.BALBranches
{
    public class BALBranches
    {
        public BusinessObjects.Models.Branch selectBranchById(int branchId, int bankId)
        {
            try
            {
                DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                return dALBranches.selectBranchById(branchId, bankId);
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
                DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                return dALBranches.selectBranchesByBankId(pBankId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum insertBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                return dALBranches.insertBranch(branch);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notInserted;
            }
        }
        public BusinessObjects.Models.ResultsEnum updateBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                return dALBranches.updateBranch(branch);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.ResultsEnum.notUpdated;
            }
        }
        public BusinessObjects.Models.sqlResultsEnum deleteBranchById(int branchId, int bankId)
        {
            try
            {
                BusinessObjects.Models.sqlResultsEnum checkDelete;
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                    checkDelete = dALBranches.deleteCountersByBranchId(branchId, bankId);
                    if(checkDelete == BusinessObjects.Models.sqlResultsEnum.success)
                    {
                        checkDelete = dALBranches.deleteBranchById(branchId, bankId);
                        if (checkDelete == BusinessObjects.Models.sqlResultsEnum.success)
                        {
                            scope.Complete();
                        }
                    }
                }
                return checkDelete;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return BusinessObjects.Models.sqlResultsEnum.failed;
            }
        }
    }
}
