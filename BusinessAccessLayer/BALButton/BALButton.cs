using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALButton
{
    public class BALButton
    {
        public List<T> selectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                return button.selectButtonsbyScreenId<T>(pScreenId, btnType);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public List<BusinessObjects.Models.CustomIssueTicketButton> selectIssueTicketbyBranchIdAnsScreenId(int pBankId, int pBranchId, int pScreenId)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                return button.selectIssueTicketbyBranchIdAnsScreenId(pBankId, pBranchId, pScreenId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public List<BusinessObjects.Models.CustomShowMessageButton> selectShowMessagebyBranchIdAnsScreenId(int pBankId, int pBranchId, int pScreenId)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                return button.selectShowMessagebyBranchIdAnsScreenId(pBankId, pBranchId, pScreenId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons selectIssueTicketAndShowMessageButtons(int pBankId, int pBranchId, int pScreenId)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons lstButtons = new BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons();
                return button.selectIssueTicketAndShowMessageButtons(pBankId, pBranchId, pScreenId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons selectIssueTicketAndShowMessageButtonsByBankName(string pBankName, int pBranchId, int pScreenId)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons lstButtons = new BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons();
                return button.selectIssueTicketAndShowMessageButtonsByBankName(pBankName, pBranchId, pScreenId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public int deleteScreenAndButtonByScreenId(int pScreenId)
        {
            try
            {
                int check;
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                    DataAccessLayer.DALScreen.DALScreen screen = new DataAccessLayer.DALScreen.DALScreen();
                    button.deleteAllButtonByScreenId(pScreenId);
                    check = screen.deleteScreenById(pScreenId);
                    scope.Complete();
                }
                return check;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return 0;
            }
        }
        public bool checkIfButtonIsDeleted(int pButtonId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton dALButton = new DataAccessLayer.DALButton.DALButton();
                return dALButton.checkIfButtonIsDeleted(pButtonId, btnType);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return false;
            }
        }
    }
}
