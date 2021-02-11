using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALLogin
{
    public class DALLogin
    {

        public BusinessObjects.Models.User userLogin(BusinessObjects.Models.User pUser)
        {
            try
            {
                string pquery = "select tblUsers.*, tblBanks.name from tblUsers inner join tblBanks on tblUsers.bankId = tblBanks.id where tblBanks.name = @bankName and tblUsers.userName = @userName and tblUsers.password = @password";
                List<SqlParameter> UserParams = new List<SqlParameter>();
                UserParams.Add(new SqlParameter("@bankName", pUser.bankName));
                UserParams.Add(new SqlParameter("@userName", pUser.userName));
                UserParams.Add(new SqlParameter("@password", pUser.password));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, UserParams);
                if (dataSet != null)
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        pUser.id = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
                        pUser.bankId = Convert.ToInt32(dataSet.Tables[0].Rows[0][3].ToString());
                        pUser.bankName = dataSet.Tables[0].Rows[0][4].ToString();
                    }
                    else
                    {
                        pUser.id = 0;
                    }
                }
                else
                {
                    return null;
                }
                return pUser;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
