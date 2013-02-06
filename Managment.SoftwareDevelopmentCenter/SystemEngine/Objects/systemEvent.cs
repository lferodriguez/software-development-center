using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemEngine.Common;
namespace SystemEngine.Objects
{
    public class systemEvent
    {
        private string connectionStringId;
        private string message;
        private System.Data.DataSet results;
        private MSSQLConnection dbCon;
        public enum SystemEventLevels : int { Information = 1, Warning = 2, Error = 3 };
        public systemEvent()
        {
            connectionStringId = "SoftwareDevelopmentCenter_ConnString";
            results = new System.Data.DataSet();
        }

        public string getMessage() { return message; }
        public System.Data.DataSet getResults() { return results; }

        public bool addSystemEvent (int webUser,string eventTitle, string eventDescription, SystemEventLevels systemLevel)
        {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@webUser", System.Data.SqlDbType.Int).Value = webUser;
            parms.Add("@eventTitle", System.Data.SqlDbType.VarChar).Value = eventTitle;
            parms.Add("@eventDescription", System.Data.SqlDbType.VarChar).Value = eventDescription;
            parms.Add("@sysEventLevel", System.Data.SqlDbType.Int).Value = systemLevel;
            if (dbCon.executeSP("SystemEngine_AddSystemEvent", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
    }
}
