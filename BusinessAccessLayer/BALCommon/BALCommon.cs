using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.BALCommon
{
    public class BALCommon
    {
        public bool checkExist(string tableName, int id)
        {
            try
            {
                DataAccessLayer.DALCommon.DALCommon dALCommon = new DataAccessLayer.DALCommon.DALCommon();
                return dALCommon.checkExist(tableName, id);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
    }
}
