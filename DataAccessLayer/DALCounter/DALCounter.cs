﻿using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using System.Diagnostics;

namespace DataAccessLayer.DALCounter
{
    public class DALCounter
    {
        public Counter selectCounterById(int counterId, int bankId)
        {
            try
            {
                string pquery = "SELECT * FROM tblCounters where id = @id and bankId = @bankId";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@id", counterId));
                counterParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, counterParams);
                if (dataSet != null)
                {
                    Counter counter = new Counter();
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
                        counter.id = 0;
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
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public List<Counter> selectCountersByBranchId(int pBranchId, int pBankId)
        {
            try
            {
                List<Counter> lstCounters = new List<Counter>();
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
                                Counter counter = new Counter();
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
                            Counter counter = new Counter();
                            counter.id = 0;
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
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public ResultsEnum insertCounter(Counter counter, int bankId)
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
                counterParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalarProc(pquery, counterParams));
                if ((sqlResultsEnum)returnValue == sqlResultsEnum.failed)
                {
                    return ResultsEnum.notFound;
                }
                else
                {
                    return ResultsEnum.inserted;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return ResultsEnum.notInserted;
            }
        }
        public ResultsEnum updateCounter(Counter counter, int bankId)
        {
            try
            {
                string storedProc = "update tblCounters set enName = @enName,arName = @arName,active = @active,type = @type OUTPUT INSERTED.IDENTITYCOL where id = @id and bankId = @bankId";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@id", counter.id));
                counterParams.Add(new SqlParameter("@enName", counter.enName));
                counterParams.Add(new SqlParameter("@arName", counter.arName));
                counterParams.Add(new SqlParameter("@active", counter.active));
                counterParams.Add(new SqlParameter("@type", counter.type));
                counterParams.Add(new SqlParameter("@branchId", counter.branchId));
                counterParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, counterParams));
                if ((sqlResultsEnum)returnValue == sqlResultsEnum.failed)
                {
                    return ResultsEnum.notFound;
                }
                else
                {
                    return ResultsEnum.updated;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return ResultsEnum.notUpdated;
            }
        }
        public sqlResultsEnum deleteAllocateCounterServiceByCounterId(int counterId, int bankId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "sp_deleteAllocateCounterServiceByCounterId";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@counterId", counterId));
                counterParams.Add(new SqlParameter("@bankId", bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalarProc(storedProc, counterParams));
                return (sqlResultsEnum)returnValue;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return sqlResultsEnum.failed;
            }
        }
        public sqlResultsEnum deleteCounterById(int counterId, int bankId, int branchId)
        {
            try
            {
                string storedProc = string.Empty;
                storedProc = "delete from tblCounters OUTPUT DELETED.IDENTITYCOL where id = @id and branchId = @branchId and bankId = @bankId";
                List<SqlParameter> counterParams = new List<SqlParameter>();
                counterParams.Add(new SqlParameter("@id", counterId));
                counterParams.Add(new SqlParameter("@bankId", bankId));
                counterParams.Add(new SqlParameter("@branchId", branchId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                int returnValue = Convert.ToInt32(dBHelper.executeScalar(storedProc, counterParams));
                if ((sqlResultsEnum)returnValue == sqlResultsEnum.failed)
                {
                    return sqlResultsEnum.failed;
                }
                else
                {
                    return sqlResultsEnum.success;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return sqlResultsEnum.failed;
            }
        }
    }
}
