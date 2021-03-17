using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALBank
{
    public class BALBank
    {
        public BusinessObjects.Models.Bank getBankById(int id)
        {
            try
            {
                DataAccessLayer.DALBank.DALBank dALBank = new DataAccessLayer.DALBank.DALBank();
                return dALBank.getBankById(id);
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
                DataAccessLayer.DALBank.DALBank dALBank = new DataAccessLayer.DALBank.DALBank();
                return dALBank.checkBankExist(pBank);
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
                DataAccessLayer.DALBank.DALBank dALbank = new DataAccessLayer.DALBank.DALBank();
                return dALbank.insertBank(pBank);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        ////sql transaction scope test
        //public static int insertBankTestScope()
        //{
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            DataAccessLayer.DALBank.DALBank dALbank = new DataAccessLayer.DALBank.DALBank();
        //            BusinessObjects.Models.Bank bank = new BusinessObjects.Models.Bank();
        //            bank.name = "s";
        //            dALbank.insertBank(bank);
        //            throw new IndexOutOfRangeException();
        //            dALbank.insertBank(bank);
        //            scope.Complete();
        //        }
        //        return 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
        //        return 0;
        //    }
        //}
    }
}