/*
 |------------------------------
 | Personal Toolkit
 |------------------------------

 July/13/2011       1.0.0       Lenin Rodríguez     Created
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SystemEngine.Common
{
    
    /// <summary>
    ///  MSSQL Connection provides a layer to connect to a MSSQL Database using a .NET configuration file  
    /// </summary>
    public class MSSQLConnection
    {
        string _connectionString;
        string _defaultConnection;
        SqlConnection _sqlConn;
        SqlCommand _sqlCommand;
        SqlDataAdapter _sqlDataAdapter;
        DataSet _rows;
        SqlDataReader _sqlReader;

        string _message;      
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="defaultConnection">Key Name in AppSettings in Default .NET Configuration File</param>
        public MSSQLConnection(string defaultConnection) {
            _defaultConnection = defaultConnection;
            _connectionString = "";
            _rows = new DataSet();
        }
        /// <summary>
        /// Destructor
        /// </summary>
        ~MSSQLConnection() { closeConnection();}

        public enum ReturnTypes { Reader = 1, Dataset = 2 };
        public string getMessage() { return _message; }
        public DataSet getDataset() { return _rows; }
        public SqlDataReader getReader() { return _sqlReader; }
        
        /// <summary>
        /// Loads a Connection String, Using a Value
        /// </summary>
        /// <returns>True: If Connection String is not null or blank</returns>
        bool loadConnectionString() {
            bool booldev = false;
            _connectionString = System.Configuration.ConfigurationManager.AppSettings[_defaultConnection];
            if (!(_connectionString == String.Empty)) { booldev = true; } 
            return booldev;
        }
        /// <summary>
        ///  Opens a connection to a MSSQL Database
        /// </summary>
        /// <returns>True: If a connection to a MSSQL Database can be opened</returns>
        bool openConnection() {
            bool boolDev = false;
                if (loadConnectionString())
                {
                    try
                    {
                        if (_sqlConn == null) { 
                        _sqlConn = new SqlConnection(_connectionString);
                        _sqlConn.Open();
                        boolDev = true;
                        }
                        else if (_sqlConn.State == ConnectionState.Closed) { _sqlConn.Open(); boolDev = true;}
                        else if (_sqlConn.State == ConnectionState.Open) { boolDev = true; }
                    }
                    catch (SqlException sqlex) { _message = "SQL Exception. Message=" + sqlex.Message; }
                    catch (Exception ex) { _message = "Exception. Message=" + ex.Message;  }
                }
                else
                {
                    _message = "Database Connection String cannot be Null or Blank";
                }
            return boolDev;
        }
        /// <summary>
        /// Closes the current connection to a MSQSQL Database
        /// </summary>
        /// <returns>True: If the current connection to a MSQLSQL Database could be closed</returns>
        public bool closeConnection() {
            bool booldev = false;
            try
            {
                if (!(_sqlReader == null)) _sqlReader.Close();
                _sqlConn.Close();
                booldev = true;
            }
            catch (Exception ex) { _message = "Exception. Message=" + ex.Message; }            
            return booldev;
        }
        /// <summary>
        /// Executes a SQL Non Query Statement
        /// </summary>
        /// <param name="sqlStatement">Use Data Manipulation Language as Insert, Update, Delete</param>
        /// <returns>True: If the Non Query Statement could be executed in database</returns>
        public bool executeNonQuery(string sqlStatement) {
            bool booldev = false;
            if (openConnection())
            {
                try
                {
                    _sqlCommand = new SqlCommand(sqlStatement, _sqlConn);
                    _sqlCommand.CommandType = System.Data.CommandType.Text;
                    _sqlCommand.ExecuteNonQuery();
                    booldev = true;
                }
                catch (SqlException sqlex) { _message = "SQL Exception. Message=" + sqlex.Message; }
                catch (Exception ex) { _message = "Exception. Message = " + ex.Message; }
                finally {
                    closeConnection();
                }
            }
            else{
                _message = "Impossible to connect to Database. " + _message;
            }
            return booldev;
        }                
        /// <summary>
        /// Executes a SQL Query in Database
        /// </summary>
        /// <remarks>
        /// If a reader is used to return the data, remember to close connection before using the object again. 
        /// </remarks>
        /// <example>
        /// This example shows how to execute a query returning a reader.        
        /// <code>        
        /// MSSQLConnection myConn = new MSSQLConnection("Database");
        /// myConn.executeQuery("Select getDate()", MSSQLConnection.ReturnTypes.Reader);
        /// myConn.closeConnection();
        /// </code>
        /// </example>
        /// <param name="query">Query String</param>
        /// <param name="returns">The data structure used to return the query values</param>
        /// <returns>True: If the query could be executed in database</returns>
        
        public bool executeQuery(string query, ReturnTypes returns) {            
            bool booldev = false;
            if (openConnection())
            {
                try
                {
                    _sqlCommand = new SqlCommand(query, _sqlConn);
                    _sqlCommand.CommandType = System.Data.CommandType.Text;                   
                    switch (returns)
                    {
                        case ReturnTypes.Reader:
                            _sqlReader = _sqlCommand.ExecuteReader();
                            booldev = true;
                            break;
                        case ReturnTypes.Dataset:
                            _sqlDataAdapter = new SqlDataAdapter();
                            _sqlDataAdapter.SelectCommand = _sqlCommand;
                            _rows = new DataSet();
                            _sqlDataAdapter.Fill(_rows);
                            closeConnection();
                            booldev = true;
                            break;
                    }
                }
                catch (SqlException sqlex) { _message = "SQL Exception. Message=" + sqlex.Message; }
                catch (Exception ex) { _message = "Exception. Message = " + ex.Message; }
            }
            else {
                _message = "Impossible to connect to Database. " + _message;
            }
            return booldev;
        }
        /// <summary>
        /// Executes a Stored Procedure in a Database
        /// </summary>
        /// <remarks>
        /// If a Reader is used to return the data, remember to close connection before using the object again. 
        /// </remarks>
        /// <example>
        /// This example shows how to execute a stored procedure returning a reader.
        /// <code>        
        /// MSSQLConnection myConn = new MSSQLConnection("Database");
        /// System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
        /// parms.Add("parameter", System.Data.SqlDbType.Int).Value = value;            
        /// myConn.executeSP("storedProcedureName", parms, MSSQLConnection.ReturnTypes.Reader);
        /// myConn.closeConnection();
        /// </code>
        /// </example>
        /// <param name="sp">Stored Procedure Name</param>
        /// <param name="parameters">Stored Procedure Parameters</param>
        /// <param name="returns">The data structure used to return the query data. If no data is returned use a reader.</param>
        /// <returns>True: If the Stored Procedure could be executed.</returns>        
        public bool executeSP(string sp, SqlParameterCollection parameters, ReturnTypes returns) {
            bool booldev = false;
            if (openConnection()) {
                try
                {
                    _sqlCommand = new SqlCommand(sp, _sqlConn);
                    _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    foreach (SqlParameter parameter in parameters){
                        _sqlCommand.Parameters.Add(parameter.ParameterName, parameter.SqlDbType).Value = parameter.Value;
                    }
                    switch (returns)
                    {
                        case ReturnTypes.Reader:
                            _sqlReader = _sqlCommand.ExecuteReader();
                            booldev = true;
                            break;
                        case ReturnTypes.Dataset:
                            _sqlDataAdapter = new SqlDataAdapter();
                            _sqlDataAdapter.SelectCommand = _sqlCommand;
                            _rows = new DataSet();
                            _sqlDataAdapter.Fill(_rows);
                            closeConnection();
                            booldev = true;
                            break;
                    }
                }
                catch (SqlException sqlex) { _message = "SQL Exception. Message=" + sqlex.Message; }
                catch (Exception ex) { _message = "Exception. Message = " + ex.Message; }
            }
            else
            {
                _message = "Impossible to connect to Database. " + _message;
            }
            return booldev;
        }
    }
}
