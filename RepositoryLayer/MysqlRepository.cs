using System;
using System.Data;
using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;

namespace RepositoryLayer
{
    /// <summary>
    /// Helper class that makes it easier to work with the provider.
    /// </summary>
    public static class MysqlRepository
    {
        public static string ConnectionString_ReadOnly
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString-MirrorDB"].ConnectionString; }
        }

        public static string ConnectionString_Writable
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; }
        }

        public static MySqlConnection GetConnection_ReadOnly()
        {
            var conn = new MySqlConnection {ConnectionString = ConnectionString_ReadOnly};
            conn.CheckAndOpen();
            return conn;
        }

        public static MySqlConnection GetConnection_Writable()
        {
            var conn = new MySqlConnection {ConnectionString = ConnectionString_Writable};
            conn.CheckAndOpen();
            return conn;
        }

        #region ExecuteNonQuery

        public static int ExecuteNonQueryAndKeepConnection(MySqlConnection connection, MySqlTransaction transaction, string commandText, CommandType commandType, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQueryAndKeepConnection(connection, transaction, commandText, commandType, true, commandParameters);
        }

        public static int ExecuteNonQueryAndKeepConnection(MySqlConnection connection, MySqlTransaction transaction, string commandText, CommandType commandType, bool logDbError, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            var sql = commandText;
            if (commandParameters != null)
                foreach (var p in commandParameters)
                {
                    cmd.Parameters.Add(p);
                    sql = sql.Replace(p.ParameterName, p.Value as string);
                }

            var result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null) cmd.Transaction.Rollback();
                if (cmd.Connection != null) cmd.Connection.Close();
                throw;
            }

            cmd.Parameters.Clear();

            return result;
        }

        public static int ExecuteNonQueryAndKeepConnection(MySqlConnection connection, MySqlTransaction transaction, string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQueryAndKeepConnection(connection, transaction, commandText, CommandType.Text, commandParameters);
        }

        public static int ExecuteNonQueryAndKeepConnection(MySqlConnection connection, string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQueryAndKeepConnection(connection, null, commandText, commandParameters);
        }

        public static int ExecuteNonQueryAndCloseConnection(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (var cn = new MySqlConnection(connectionString))
            {
                cn.CheckAndOpen();

                //call the overload that takes a connection in place of the connection string
                return ExecuteNonQueryAndKeepConnection(cn, commandText, parms);
            }
        }

        #endregion

        #region ExecuteDataSet

        public static DataRow ExecuteDatarow(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            DataRow row = null;
            using (var cn = new MySqlConnection(connectionString))
            {
                cn.CheckAndOpen();

                //call the overload that takes a connection in place of the connection string
                row = ExecuteDatarow(cn, null, commandText, parms);
            }
            return row;
        }

        public static DataRow ExecuteDatarow(MySqlConnection connection, MySqlTransaction transaction, string commandText, params MySqlParameter[] parms)
        {
            var dt = ExecuteDataTable(connection, transaction, commandText, parms);
            if (dt == null) return null;
            if (dt.Rows.Count == 0) return null;
            return dt.Rows[0];
        }

        public static DataTable ExecuteDataTable(MySqlConnection connection, MySqlTransaction transaction, string commandText, params MySqlParameter[] parms)
        {
            var ds = ExecuteDataset(connection, transaction, commandText, parms);
            if (ds == null) return null;
            if (ds.Tables.Count == 0) return null;

            var dtTarget = ds.Tables[0].Copy();

            return dtTarget;
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, MySqlTransaction transaction, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteDataset(connection, transaction, commandText, (MySqlParameter[])null);
        }

        public static DataSet ExecuteDataset(MySqlConnection connection, MySqlTransaction transaction, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (var p in commandParameters)
                    cmd.Parameters.Add(p);

            //create the DataAdapter
            var da = new MySqlDataAdapter(cmd);

            //create the DataSet
            var ds = new DataSet();
            try
            {
                //fill the DataSet using default values for DataTable names, etc.
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null) cmd.Transaction.Rollback();
                if (cmd.Connection != null) cmd.Connection.Close();
                throw;
            }

            // detach the MySqlParameters from the command object, so they can be used again.			
            cmd.Parameters.Clear();

            //return the dataset
            return ds;
        }

        public static void UpdateDataSet(string connectionString, string commandText, DataSet ds, string tablename)
        {
            var cn = new MySqlConnection(connectionString);
            cn.CheckAndOpen();
            var da = new MySqlDataAdapter(commandText, cn);
            var cb = new MySqlCommandBuilder(da);
            da.Update(ds, tablename);
            cn.Close();
        }

        #endregion

        #region ExecuteDataReader

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, MySqlTransaction transaction, string commandText, CommandType commandType, MySqlParameter[] commandParameters, bool ExternalConn)
        {
            //create a command and prepare it for execution
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            if (commandParameters != null)
                foreach (var p in commandParameters)
                    cmd.Parameters.Add(p);

            //create a reader
            MySqlDataReader dr;
            try
            {
                // call ExecuteReader with the appropriate CommandBehavior
                if (ExternalConn)
                {
                    dr = cmd.ExecuteReader();
                }
                else
                {
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null) cmd.Transaction.Rollback();
                if (cmd.Connection != null) cmd.Connection.Close();
                throw;
            }

            return dr;
        }

        public static MySqlDataReader ExecuteReader(string connectionString, string commandText)
        {
            //pass through the call providing null for the set of SqlParameters
            return ExecuteReader(connectionString, commandText, (MySqlParameter[])null);
        }

        public static MySqlDataReader ExecuteReader(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection
            var cn = new MySqlConnection(connectionString);
            cn.CheckAndOpen();

            try
            {
                //call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(cn, null, commandText, CommandType.Text, commandParameters, false);
            }
            catch
            {
                //if we fail to return the SqlDatReader, we need to close the connection ourselves
                cn.Close();
                throw;
            }
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection
            var cn = new MySqlConnection(connectionString);
            cn.CheckAndOpen();

            try
            {
                //call the private overload that takes an internally owned connection in place of the connection string
                return ExecuteReader(cn, null, commandText, commandType, commandParameters, false);
            }
            catch
            {
                //if we fail to return the SqlDatReader, we need to close the connection ourselves
                cn.Close();
                throw;
            }
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, string commandText, MySqlParameter[] commandParameters, bool ExternalConn)
        {
            return ExecuteReader(connection, null, commandText, CommandType.Text, commandParameters, ExternalConn);
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, string commandText, CommandType commandType, MySqlParameter[] commandParameters, bool ExternalConn)
        {
            return ExecuteReader(connection, null, commandText, commandType, commandParameters, ExternalConn);
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection connection, MySqlTransaction transaction, string commandText, MySqlParameter[] commandParameters, bool ExternalConn)
        {
            return ExecuteReader(connection, null, commandText, CommandType.Text, commandParameters, ExternalConn);
        }

        #endregion

        #region ExecuteScalar

        public static object ExecuteScalar(string connectionString, string commandText)
        {
            //pass through the call providing null for the set of MySqlParameters
            return ExecuteScalar(connectionString, commandText, (MySqlParameter[])null);
        }

        public static object ExecuteScalar(string connectionString, string commandText, params MySqlParameter[] commandParameters)
        {
            //create & open a SqlConnection, and dispose of it after we are done.
            using (var cn = new MySqlConnection(connectionString))
            {
                cn.CheckAndOpen();

                //call the overload that takes a connection in place of the connection string
                return ExecuteScalarWithOpenConnection(cn, null, commandText, commandParameters);
            }
        }

        public static object ExecuteScalarWithOpenConnection(MySqlConnection connection, MySqlTransaction transaction, string commandText, params MySqlParameter[] commandParameters)
        {
            //create a command and prepare it for execution
            var cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandText = commandText;
            cmd.CommandType = CommandType.Text;

            if (commandParameters != null)
                foreach (var p in commandParameters)
                    cmd.Parameters.Add(p);

            object retval = null;
            try
            {
                //execute the command & return the results
                retval = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null) cmd.Transaction.Rollback();
                if (cmd.Connection != null) cmd.Connection.Close();
                throw;
            }

            // detach the SqlParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return retval;
        }

        #endregion
    }

    public static class MySqlExtensions
    {
        public static void CheckAndOpen(this MySqlConnection connection)
        {
            if (!connection.Ping())
            {
                connection.Open();
            }
        }
    }
}