﻿using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace DataAccessLayer.DALBank
{
    public class DALBank
    {
        public BusinessObjects.Models.Bank getBankById(int id)
        {
            try
            {
                string pquery = "SELECT id,name FROM tblBanks WHERE id = @id";
                List<SqlParameter> bankParams = new List<SqlParameter>();
                bankParams.Add(new SqlParameter("@id", id));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, bankParams);
                BusinessObjects.Models.Bank pBank = new BusinessObjects.Models.Bank();
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        pBank.id = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
                        pBank.name = dataSet.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        pBank.id = 0;
                    }
                }
                else
                {
                    return null;
                }
                return pBank;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.Bank checkBankExist(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                string pquery = "SELECT id,name FROM tblBanks WHERE name = @name";
                List<SqlParameter> bankParams = new List<SqlParameter>();
                bankParams.Add(new SqlParameter("@name", pBank.name));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, bankParams);
                if (dataSet != null)
                {
                    if(dataSet.Tables[0].Rows.Count > 0)
                    {
                        pBank.id = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        pBank.id = 0;
                    }
                }
                else
                {
                    return null;
                }
                return pBank;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.Bank insertBank(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                string pquery = "insert into tblBanks OUTPUT INSERTED.IDENTITYCOL  values (@name)";
                List<SqlParameter> bankParams = new List<SqlParameter>();
                bankParams.Add(new SqlParameter("@name", pBank.name));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pBank.id = Convert.ToInt32(dBHelper.executeScalar(pquery, bankParams));
                return pBank;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
    }
}
