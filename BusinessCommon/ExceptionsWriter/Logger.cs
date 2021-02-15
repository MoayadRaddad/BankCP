using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCommon.ExceptionsWriter
{
    public class Logger
    {
        public static void Log(Exception exception)
        {
            try
            {
                StringBuilder sbExceptionMessage = new StringBuilder();
                sbExceptionMessage.Append("Exception Type" + Environment.NewLine);
                sbExceptionMessage.Append(exception.GetType().Name);
                sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
                sbExceptionMessage.Append("Message" + Environment.NewLine);
                sbExceptionMessage.Append(exception.Message + Environment.NewLine + Environment.NewLine);
                sbExceptionMessage.Append("Stack Trace" + Environment.NewLine);
                sbExceptionMessage.Append(exception.StackTrace + Environment.NewLine + Environment.NewLine);
                Exception innerException = exception.InnerException;
                while (innerException != null)
                {
                    sbExceptionMessage.Append("Exception Type" + Environment.NewLine);
                    sbExceptionMessage.Append(innerException.GetType().Name);
                    sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
                    sbExceptionMessage.Append("Message" + Environment.NewLine);
                    sbExceptionMessage.Append(innerException.Message + Environment.NewLine + Environment.NewLine);
                    sbExceptionMessage.Append("Stack Trace" + Environment.NewLine);
                    sbExceptionMessage.Append(innerException.StackTrace + Environment.NewLine + Environment.NewLine);
                    innerException = innerException.InnerException;
                }
                if (!EventLog.SourceExists("BankConfigurationPortal"))
                {
                    EventLog.CreateEventSource("BankConfigurationPortal", "BankConfigurationPortalLogs");
                }
                EventLog log = new EventLog("BankConfigurationPortalLogs");
                log.Source = "BankConfigurationPortal";
                log.WriteEntry(sbExceptionMessage.ToString(), EventLogEntryType.Error);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
            }
        }
    }
}
