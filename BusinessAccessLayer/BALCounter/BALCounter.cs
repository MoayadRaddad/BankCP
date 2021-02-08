﻿using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessAccessLayer.BALCounter
{
    public class BALCounter
    {
        public BusinessObjects.Models.Counter selectCounterById(int counterId)
        {
            try
            {
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.selectCounterById(counterId);
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
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.selectCountersByBranchId(pBranchId, pBankId);
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
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.insertCounter(counter);
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
                DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                return dALCounter.updateCounter(counter);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ResultsEnum deleteCounterById(int counterId, int branchId)
        {
            try
            {
                BusinessObjects.Models.ResultsEnum checkDelete;
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALCounter.DALCounter dALCounter = new DataAccessLayer.DALCounter.DALCounter();
                    checkDelete = dALCounter.deleteAllocateCounterServiceByCounterId(counterId, branchId);
                    if (checkDelete == BusinessObjects.Models.ResultsEnum.deleted)
                    {
                        checkDelete = dALCounter.deleteCounterById(counterId, branchId);
                        if (checkDelete == BusinessObjects.Models.ResultsEnum.deleted)
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
                return BusinessObjects.Models.ResultsEnum.notDeleted;
            }
        }
    }
}
