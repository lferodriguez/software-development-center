using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemEngine.Common;

namespace SystemEngine.Objects
{
    public class webUser
    {
        
        private string connectionStringId;
        private string message;
        private System.Data.DataSet results;
        private MSSQLConnection dbCon;

        public webUser()
        {
            connectionStringId = "SoftwareDevelopmentCenter_ConnString";
            results = new System.Data.DataSet();
        }

        public string getMessage() { return message; }
        public System.Data.DataSet getResults() { return results; }

        private bool updateLastLogin(int webUserId) {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@webUserId", System.Data.SqlDbType.VarChar).Value = webUserId;
            if (dbCon.executeSP("SystemEngine_webUserUpdateLastLogin", parms, MSSQLConnection.ReturnTypes.Dataset))
            {booldev = true;}
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        private void recordLogin(int webUserId)
        {
            SystemEngine.Objects.systemEvent sysEvt = new systemEvent();
            sysEvt.addSystemEvent(webUserId, "Successful Login", "User has logged in to the system.", systemEvent.SystemEventLevels.Information);
        }
        public void recordLogOut(int webUserId) {
            SystemEngine.Objects.systemEvent sysEvt = new systemEvent();
            sysEvt.addSystemEvent(webUserId, "Successful LogOut", "User has logged out from the system.", systemEvent.SystemEventLevels.Information);
        }

        public bool authenticate(string webUserEmail, string password, ref int webUserId) {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);            
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@webUserEmail", System.Data.SqlDbType.VarChar).Value = webUserEmail;
            parms.Add("@webUserPassword", System.Data.SqlDbType.VarChar).Value = password;            
            if (dbCon.executeSP("SystemEngine_webUserAuthenticate", parms, MSSQLConnection.ReturnTypes.Dataset))
            {
                if (dbCon.getDataset().Tables[0].Rows.Count > 0) {
                    booldev = true;
                    int.TryParse(dbCon.getDataset().Tables[0].Rows[0]["webUser"].ToString(),out webUserId);
                    updateLastLogin(webUserId);
                    recordLogin(webUserId);
                }                             
            }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        public bool userInformation(int webUserId) {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@webUserId", System.Data.SqlDbType.Int).Value = webUserId;            
            if (dbCon.executeSP("SystemEngine_webUserInformation", parms, MSSQLConnection.ReturnTypes.Dataset))
            {booldev = true;results = dbCon.getDataset();}
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
    }
}
