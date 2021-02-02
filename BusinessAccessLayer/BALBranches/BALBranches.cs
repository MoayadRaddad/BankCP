﻿using BusinessCommon.ExceptionsWriter;
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
        public BusinessObjects.Models.Branch selectBranchesById(int branchId)
        {
            try
            {
                DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                return dALBranches.selectBranchesById(branchId);
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
        public BusinessObjects.Models.Branch insertBranch(BusinessObjects.Models.Branch branch)
        {
            try
            {
                DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                return dALBranches.insertBranch(branch);
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
                DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                return dALBranches.updateBranch(branch);
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
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALBranches.DALBranches dALBranches = new DataAccessLayer.DALBranches.DALBranches();
                    dALBranches.deleteCountersByBranchId(branchId);
                    dALBranches.deleteBranchById(branchId);
                    scope.Complete();
                }
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