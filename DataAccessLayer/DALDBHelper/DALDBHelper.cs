using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using BusinessCommon.ExceptionsWriter;
using System.Configuration;
using System.Diagnostics;

namespace DataAccessLayer.DALDBHelper
{
    public class DALDBHelper
    {
        public static string connectionString;
        public DALDBHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        }
        public int executeNonQuery(string query, List<SqlParameter> parametros)
        {
            try
            {
                return nonQuery(query, parametros);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return -1;
            }
        }
        public int executeNonQueryProc(string query, List<SqlParameter> parametros)
        {
            try
            {
                return nonQueryProc(query, parametros);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return -1;
            }
        }
        public object executeScalar(string query, List<SqlParameter> parametros)
        {
            try
            {
                return scalar(query, parametros);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public object executeScalarProc(string storedProc, List<SqlParameter> parametros)
        {
            try
            {
                return scalarProc(storedProc, parametros);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public DataSet executeAdapter( string commandText, List<SqlParameter> commandParameters)
        {
            try
            {
                var cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(connectionString);
                cmd.Connection = con;
                cmd.CommandText = commandText;
                foreach (SqlParameter param in commandParameters)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        public DataSet executeAdapterProc(string procName, List<SqlParameter> commandParameters)
        {
            try
            {
                var cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(connectionString);
                cmd.Connection = con;
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter param in commandParameters)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        #region Private Methods
        private int nonQuery(string query, List<SqlParameter> parametros)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteNonQuery();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return -1;
            }
        }
        private int nonQueryProc(string query, List<SqlParameter> parametros)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteNonQuery();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return -1;
            }
        }
        private object scalar(string query, List<SqlParameter> parametros)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteScalar();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        private object scalarProc(string storedProc, List<SqlParameter> parametros)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = storedProc;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteScalar();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveEventsAndExceptions(ex, "Exceptions not handled", EventLogEntryType.Error);
                return null;
            }
        }
        #endregion
    }

}