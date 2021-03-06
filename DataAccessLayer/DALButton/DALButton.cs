﻿using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace DataAccessLayer.DALButton
{
    public class DALButton
    {
        public List<T> selectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                List<T> lstButtons = new List<T>();
                string pquery = "SELECT * FROM tbl" + btnType.ToString() + "Button where screenId = @screenId";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, screenParams);
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (btnType == BusinessObjects.Models.btnType.ShowMessage)
                    {
                        var btn = new BusinessObjects.Models.ShowMessageButton(Convert.ToInt32(dataRow["id"]), dataRow["enName"].ToString(), dataRow["arName"].ToString(),
                            dataRow["messageAR"].ToString(), dataRow["messageEN"].ToString(), Convert.ToInt32(dataRow["screenId"]));
                        lstButtons.Add((T)(object)btn);
                    }
                    else
                    {
                        var btn = new BusinessObjects.Models.IssueTicketButton(Convert.ToInt32(dataRow["id"]), dataRow["enName"].ToString(), dataRow["arName"].ToString(),
                            Convert.ToInt32(dataRow["serviceId"]), Convert.ToInt32(dataRow["screenId"]));
                        lstButtons.Add((T)(object)btn);
                    }
                }
                return lstButtons;
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
                List<BusinessObjects.Models.CustomIssueTicketButton> lstIssueTicketButtons = new List<BusinessObjects.Models.CustomIssueTicketButton>();
                string pquery = "selectIssueTicketbyBranchIdAndScreen";
                List<SqlParameter> issueTicketButtonsParams = new List<SqlParameter>();
                issueTicketButtonsParams.Add(new SqlParameter("@bankId", pBankId));
                issueTicketButtonsParams.Add(new SqlParameter("@branchId", pBranchId));
                issueTicketButtonsParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapterProc(pquery, issueTicketButtonsParams);
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        if (Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]) > 0)
                        {
                            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                            {
                                BusinessObjects.Models.CustomIssueTicketButton issueTicketButton = new BusinessObjects.Models.CustomIssueTicketButton();
                                issueTicketButton.id = Convert.ToInt32(dataRow["id"]);
                                issueTicketButton.enName = dataRow["enName"].ToString();
                                issueTicketButton.arName = dataRow["arName"].ToString();
                                issueTicketButton.serviceId = Convert.ToInt32(dataRow["serviceId"]);
                                issueTicketButton.screenId = Convert.ToInt32(dataRow["screenId"]);
                                lstIssueTicketButtons.Add(issueTicketButton);
                            }
                        }
                    }
                    return lstIssueTicketButtons;
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
        public List<BusinessObjects.Models.CustomShowMessageButton> selectShowMessagebyBranchIdAnsScreenId(int pBankId, int pBranchId, int pScreenId)
        {
            try
            {
                List<BusinessObjects.Models.CustomShowMessageButton> lstShowMessageButtons = new List<BusinessObjects.Models.CustomShowMessageButton>();
                string pquery = "selectShowMessagebyBranchIdAndScreen";
                List<SqlParameter> showMessageButtonsParams = new List<SqlParameter>();
                showMessageButtonsParams.Add(new SqlParameter("@bankId", pBankId));
                showMessageButtonsParams.Add(new SqlParameter("@branchId", pBranchId));
                showMessageButtonsParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapterProc(pquery, showMessageButtonsParams);
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count != 0)
                    {
                        if (Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]) > 0)
                        {
                            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                            {
                                BusinessObjects.Models.CustomShowMessageButton showMessageButton = new BusinessObjects.Models.CustomShowMessageButton();
                                showMessageButton.id = Convert.ToInt32(dataRow["id"]);
                                showMessageButton.enName = dataRow["enName"].ToString();
                                showMessageButton.arName = dataRow["arName"].ToString();
                                showMessageButton.messageEN = dataRow["messageEN"].ToString();
                                showMessageButton.messageAR = dataRow["messageAR"].ToString();
                                showMessageButton.screenId = Convert.ToInt32(dataRow["screenId"]);
                                lstShowMessageButtons.Add(showMessageButton);
                            }
                        }
                    }
                    return lstShowMessageButtons;
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
        public BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons selectIssueTicketAndShowMessageButtons(int pBankId, int pBranchId, int pScreenId)
        {
            try
            {
                BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons lstButtons = new BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons();
                string pquery = "sp_selectIssueTicketAndShowMessageButtons";
                List<SqlParameter> buttonsParams = new List<SqlParameter>();
                buttonsParams.Add(new SqlParameter("@bankId", pBankId));
                buttonsParams.Add(new SqlParameter("@branchId", pBranchId));
                buttonsParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapterProc(pquery, buttonsParams);
                if (dataSet == null) return null;
                if (dataSet.Tables[0].Rows.Count != 0)
                {
                    if (Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]) > 0)
                    {
                        List<BusinessObjects.Models.CustomIssueTicketButton> lstIssueTicketButtons = new List<BusinessObjects.Models.CustomIssueTicketButton>();
                        List<BusinessObjects.Models.CustomShowMessageButton> lstShowMessageButtons = new List<BusinessObjects.Models.CustomShowMessageButton>();
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            if (dataRow["type"].ToString() == "IssueTicket")
                            {
                                BusinessObjects.Models.CustomIssueTicketButton issueTicketButton = new BusinessObjects.Models.CustomIssueTicketButton();
                                issueTicketButton.id = Convert.ToInt32(dataRow["id"]);
                                issueTicketButton.enName = dataRow["enName"].ToString();
                                issueTicketButton.arName = dataRow["arName"].ToString();
                                issueTicketButton.serviceId = Convert.ToInt32(dataRow["serviceId"]);
                                issueTicketButton.screenId = Convert.ToInt32(dataRow["screenId"]);
                                lstIssueTicketButtons.Add(issueTicketButton);
                            }
                            else
                            {
                                BusinessObjects.Models.CustomShowMessageButton showMessageButton = new BusinessObjects.Models.CustomShowMessageButton();
                                showMessageButton.id = Convert.ToInt32(dataRow["id"]);
                                showMessageButton.enName = dataRow["enName"].ToString();
                                showMessageButton.arName = dataRow["arName"].ToString();
                                showMessageButton.messageEN = dataRow["messageEN"].ToString();
                                showMessageButton.messageAR = dataRow["messageAR"].ToString();
                                showMessageButton.screenId = Convert.ToInt32(dataRow["screenId"]);
                                lstShowMessageButtons.Add(showMessageButton);
                            }
                        }
                        lstButtons.issueTicketButtons = lstIssueTicketButtons;
                        lstButtons.showMessageButtons = lstShowMessageButtons;
                    }
                }
                return lstButtons;
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
                BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons lstButtons = new BusinessObjects.Models.CustomIssueTicketAndShowMessageButtons();
                string pquery = "sp_selectIssueTicketAndShowMessageButtonsByBankName";
                List<SqlParameter> buttonsParams = new List<SqlParameter>();
                buttonsParams.Add(new SqlParameter("@bankName", pBankName));
                buttonsParams.Add(new SqlParameter("@branchId", pBranchId));
                buttonsParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapterProc(pquery, buttonsParams);
                if (dataSet == null) return null;
                if (dataSet.Tables[0].Rows.Count != 0)
                {
                    if (Convert.ToInt32((dataSet.Tables[0].Rows[0])["id"]) > 0)
                    {
                        List<BusinessObjects.Models.CustomIssueTicketButton> lstIssueTicketButtons = new List<BusinessObjects.Models.CustomIssueTicketButton>();
                        List<BusinessObjects.Models.CustomShowMessageButton> lstShowMessageButtons = new List<BusinessObjects.Models.CustomShowMessageButton>();
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            if (dataRow["type"].ToString() == "IssueTicket")
                            {
                                BusinessObjects.Models.CustomIssueTicketButton issueTicketButton = new BusinessObjects.Models.CustomIssueTicketButton();
                                issueTicketButton.id = Convert.ToInt32(dataRow["id"]);
                                issueTicketButton.enName = dataRow["enName"].ToString();
                                issueTicketButton.arName = dataRow["arName"].ToString();
                                issueTicketButton.serviceId = Convert.ToInt32(dataRow["serviceId"]);
                                issueTicketButton.screenId = Convert.ToInt32(dataRow["screenId"]);
                                lstIssueTicketButtons.Add(issueTicketButton);
                            }
                            else
                            {
                                BusinessObjects.Models.CustomShowMessageButton showMessageButton = new BusinessObjects.Models.CustomShowMessageButton();
                                showMessageButton.id = Convert.ToInt32(dataRow["id"]);
                                showMessageButton.enName = dataRow["enName"].ToString();
                                showMessageButton.arName = dataRow["arName"].ToString();
                                showMessageButton.messageEN = dataRow["messageEN"].ToString();
                                showMessageButton.messageAR = dataRow["messageAR"].ToString();
                                showMessageButton.screenId = Convert.ToInt32(dataRow["screenId"]);
                                lstShowMessageButtons.Add(showMessageButton);
                            }
                        }
                        lstButtons.issueTicketButtons = lstIssueTicketButtons;
                        lstButtons.showMessageButtons = lstShowMessageButtons;
                    }
                }
                return lstButtons;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton insertShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                string pquery = "insert into tblShowMessageButton OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@messageEN,@messageAR,@screenId,0)";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@enName", pButton.enName));
                screenParams.Add(new SqlParameter("@arName", pButton.arName));
                screenParams.Add(new SqlParameter("@messageEN", pButton.messageEN));
                screenParams.Add(new SqlParameter("@messageAR", pButton.messageAR));
                screenParams.Add(new SqlParameter("@screenId", pButton.screenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pButton.id = Convert.ToInt32(dBHelper.executeScalar(pquery, screenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton updateShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                string pquery = "update tblShowMessageButton set enName = @enName,arName = @arName,messageAR = @messageAR,messageEN = @messageEN where id = @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", pButton.id));
                screenParams.Add(new SqlParameter("@enName", pButton.enName));
                screenParams.Add(new SqlParameter("@arName", pButton.arName));
                screenParams.Add(new SqlParameter("@messageEN", pButton.messageEN));
                screenParams.Add(new SqlParameter("@messageAR", pButton.messageAR));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton insertIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                string pquery = "insert into tblIssueTicketButton OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@serviceId,@screenId,0)";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@enName", pButton.enName));
                screenParams.Add(new SqlParameter("@arName", pButton.arName));
                screenParams.Add(new SqlParameter("@serviceId", pButton.serviceId));
                screenParams.Add(new SqlParameter("@screenId", pButton.screenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pButton.id = Convert.ToInt32(dBHelper.executeScalar(pquery, screenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton updateIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                string pquery = "update tblIssueTicketButton set enName = @enName,ARName = @arName,serviceId = @serviceId where id = @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", pButton.id));
                screenParams.Add(new SqlParameter("@enName", pButton.enName));
                screenParams.Add(new SqlParameter("@arName", pButton.arName));
                screenParams.Add(new SqlParameter("@serviceId", pButton.serviceId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public int deleteButtonsConditional(IDictionary<int, string> pButtonsIds, string ConditionColumn)
        {
            try
            {
                foreach (var item in pButtonsIds)
                {
                    if (Convert.ToInt32(item.Key) != 0)
                    {
                        string pquery = string.Empty;
                        pquery = "delete from tbl" + item.Value.ToString() + "Button where " + ConditionColumn + " = @id";
                        List<SqlParameter> screenParams = new List<SqlParameter>();
                        screenParams.Add(new SqlParameter("@id", item.Key));
                        DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                        dBHelper.executeNonQuery(pquery, screenParams);
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return 0;
            }
        }
        /// <summary>
        /// This function delete all button from the two tables for the screen by Id
        /// </summary>
        /// <param name="pScreenId"></param>
        /// <returns></returns>
        public int deleteAllButtonByScreenId(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblShowMessageButton where screenId = @screenId";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                pquery = "delete from tblIssueTicketButton where screenId = @screenId";
                screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@screenId", pScreenId));
                dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                return 1;
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
                string pquery = String.Format("select * from tbl{0}Button where id = @id", btnType.ToString());
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", pButtonId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                var rowEffected = dBHelper.executeScalar(pquery, screenParams);
                return rowEffected != null ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return false;
            }
        }
    }
}
