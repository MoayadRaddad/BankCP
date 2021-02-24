using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BusinessCommon.ExceptionsWriter
{
    public class ExceptionsWriter
    {
        public static void saveEventsAndExceptions(Exception ex, string message, EventLogEntryType type)
        {
            try
            {
                saveEventsAndExceptionsToLogFile(ex, message, type);
                saveEventsAndErrors(ex, message, type);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// public method get exception or event, handle it and save it to log file with json type
        /// </summary>
        public static void saveEventsAndExceptionsToLogFile(Exception ex, string message, EventLogEntryType type)
        {
            try
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Exceptions.json";
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    if (ex == null)
                    {
                        BusinessObjects.Models.logInfo loginfo = new BusinessObjects.Models.logInfo();
                        loginfo.Date = DateTime.Now;
                        loginfo.Type = type.ToString();
                        loginfo.Message = message;
                        var JException = JsonConvert.SerializeObject(loginfo);
                        writer.WriteLine(JException);
                    }
                    else
                    {
                        while (ex != null)
                        {
                            BusinessObjects.Models.ApplicationException applicationException = new BusinessObjects.Models.ApplicationException();
                            applicationException.Date = DateTime.Now;
                            applicationException.Type = ex.GetType().FullName;
                            applicationException.Message = ex.Message;
                            applicationException.StackTrace = ex.StackTrace;
                            applicationException.DeveloperMessage = message;
                             var JException = JsonConvert.SerializeObject(applicationException);
                            writer.WriteLine(JException);
                            ex = ex.InnerException;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// public method get exception or event, save it to windows event viewer
        /// </summary>
        public static void saveEventsAndErrors(Exception ex, string message, EventLogEntryType type)
        {
            try
            {
                StringBuilder sbExceptionMessage = new StringBuilder();
                if (ex == null)
                {
                    sbExceptionMessage.Append("Date" + Environment.NewLine);
                    sbExceptionMessage.Append(DateTime.Now.ToString() + Environment.NewLine);
                    sbExceptionMessage.Append("Type" + Environment.NewLine);
                    sbExceptionMessage.Append(type.ToString() + Environment.NewLine);
                    sbExceptionMessage.Append("Message" + Environment.NewLine);
                    sbExceptionMessage.Append(message + Environment.NewLine);
                }
                else
                {
                    sbExceptionMessage.Append("Date" + Environment.NewLine);
                    sbExceptionMessage.Append(DateTime.Now.ToString());
                    sbExceptionMessage.Append("Exception Type" + Environment.NewLine);
                    sbExceptionMessage.Append(ex.GetType().Name);
                    sbExceptionMessage.Append(Environment.NewLine + Environment.NewLine);
                    sbExceptionMessage.Append("Message" + Environment.NewLine);
                    sbExceptionMessage.Append(ex.Message + Environment.NewLine + Environment.NewLine);
                    sbExceptionMessage.Append("Stack Trace" + Environment.NewLine);
                    sbExceptionMessage.Append(ex.StackTrace + Environment.NewLine + Environment.NewLine);
                    sbExceptionMessage.Append("Developer Message" + Environment.NewLine);
                    sbExceptionMessage.Append(message + Environment.NewLine + Environment.NewLine);
                    Exception innerException = ex.InnerException;
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
                }
                EventLog.WriteEntry("BankConfigurationPortal", sbExceptionMessage.ToString(), type);
            }
            catch (Exception exception)
            {
                saveEventsAndExceptionsToLogFile(exception, message, EventLogEntryType.Error);
            }
        }
    }
}