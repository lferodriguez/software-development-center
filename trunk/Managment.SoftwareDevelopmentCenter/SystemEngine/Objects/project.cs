using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SystemEngine.Common; 

namespace SystemEngine.Objects
{
    public class project
    {
        public enum projectTypes : int { Project = 1, Ticket = 2 };
        public enum softwareDevelopmentLifeCycles : int { Definition = 1, Analysis = 2, Design = 3, Development = 4, Tests = 5, Implementation = 6, Finalize = 7 }

        private MSSQLConnection dbCon;

        private string connectionStringId;
        private string message;
        private System.Data.DataSet results;
        
        public project()
        {
            connectionStringId = "SoftwareDevelopmentCenter_ConnString";
            results = new System.Data.DataSet();
        }

        public string getMessage() { return message; }
        public System.Data.DataSet getResults() { return results; }

        public bool getProjectTypes() {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            if (dbCon.executeSP("webManagment_getProjectTypes", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; results = dbCon.getDataset(); }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        
        public bool getSDLCs(){
          bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            if (dbCon.executeSP("webManagement_getSDLC", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; results = dbCon.getDataset(); }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        public bool getProjectSituations() {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            if (dbCon.executeSP("webManagement_getProjectSituation", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; results = dbCon.getDataset(); }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        public bool getProjectsByType(projectTypes projectType)
        {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@projectTypeId", System.Data.SqlDbType.Int).Value = projectType;
            if (dbCon.executeSP("webManagment_getProjectsByType", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; results = dbCon.getDataset(); }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        public bool getProject()
        {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;            
            if (dbCon.executeSP("webManagment_getProjects", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; results = dbCon.getDataset(); }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        public bool getProject(int projectId)
        {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@pProjectId", System.Data.SqlDbType.Int).Value = projectId;
            if (dbCon.executeSP("webManagment_getProjects", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; results = dbCon.getDataset(); }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }

        public bool queryProjects(int typeid, int sdlcId, int situation) {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@pSituation", System.Data.SqlDbType.Int).Value = situation;
            parms.Add("@pSdlc", System.Data.SqlDbType.Int).Value = sdlcId;
            parms.Add("@ptype", System.Data.SqlDbType.Int).Value = typeid;
            if (dbCon.executeSP("webManagment_queryProjects", parms, MSSQLConnection.ReturnTypes.Dataset))
            { booldev = true; results = dbCon.getDataset(); }
            else { message = "Query was not executed. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        public bool addProject(string identifier, string name, int projectType,int  sdlc, int situation, ref int projectId, 
                               string estimatedStartDate, string estimatedEndDate,
                               string realStartDate, string realEndDate, int estimatedResources) {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@pIdentifier", System.Data.SqlDbType.VarChar).Value = identifier ;
            parms.Add("@pName", System.Data.SqlDbType.VarChar).Value = name;
            parms.Add("@pType", System.Data.SqlDbType.Int).Value = projectType;
            parms.Add("@pSDLCid", System.Data.SqlDbType.SmallInt).Value = sdlc;
            parms.Add("@pSituationId", System.Data.SqlDbType.SmallInt).Value = situation;
            if (!(estimatedStartDate == string.Empty)) parms.Add("@pEstimatedStartDate", System.Data.SqlDbType.DateTime).Value = estimatedStartDate;
            if (!(estimatedEndDate == string.Empty)) parms.Add("@pEstimatedEndDate", System.Data.SqlDbType.DateTime).Value = estimatedEndDate;
            if (!(realStartDate == string.Empty)) parms.Add("@pRealStartDate", System.Data.SqlDbType.DateTime).Value = realStartDate;
            if (!(realEndDate == string.Empty)) parms.Add("@pRealEndDate", System.Data.SqlDbType.DateTime).Value = realEndDate;
            if (!(estimatedResources == 0)) parms.Add("@pEstimatedResources", System.Data.SqlDbType.SmallInt).Value = estimatedResources;
            if (dbCon.executeSP("webManagment_addProject", parms, MSSQLConnection.ReturnTypes.Dataset))
            {
                if (dbCon.getDataset().Tables[0].Rows.Count > 0)
                {
                    projectId = int.Parse(dbCon.getDataset().Tables[0].Rows[0]["projectId"].ToString());
                    if (projectId > -1) { booldev = true; }
                    else { message = "Record could not be inserted into database."; }
                }
                else { message = "Record could not be inserted into database."; }
            }
            else { message = "Record could not be inserted into database. Message = " + dbCon.getMessage(); }
            return booldev;
        }
        public bool updateProject(string identifier, string name, int projectType, int sdlc,int situation, int projectId,
                                    string estimatedStartDate, string estimatedEndDate,
                                    string realStartDate, string realEndDate, int estimatedResources)
        {
            bool booldev = false;
            message = "";
            dbCon = new MSSQLConnection(connectionStringId);
            System.Data.SqlClient.SqlParameterCollection parms = new System.Data.SqlClient.SqlCommand().Parameters;
            parms.Add("@pIdentifier", System.Data.SqlDbType.VarChar).Value = identifier;
            parms.Add("@pName", System.Data.SqlDbType.VarChar).Value = name;
            parms.Add("@pType", System.Data.SqlDbType.Int ).Value = projectType;
            parms.Add("@pSDLCid", System.Data.SqlDbType.SmallInt).Value = sdlc;
            parms.Add("@pSituationId", System.Data.SqlDbType.SmallInt).Value = situation;
            parms.Add("@pProjectId", System.Data.SqlDbType.Int).Value = projectId;
            if (!(estimatedStartDate == string.Empty)) parms.Add("@pEstimatedStartDate", System.Data.SqlDbType.DateTime).Value = estimatedStartDate;
            if (!(estimatedEndDate == string.Empty)) parms.Add("@pEstimatedEndDate", System.Data.SqlDbType.DateTime).Value = estimatedEndDate;
            if (!(realStartDate == string.Empty)) parms.Add("@pRealStartDate", System.Data.SqlDbType.DateTime).Value = realStartDate;
            if (!(realEndDate == string.Empty)) parms.Add("@pRealEndDate", System.Data.SqlDbType.DateTime).Value = realEndDate;
            if (!(estimatedResources == 0)) parms.Add("@pEstimatedResources", System.Data.SqlDbType.SmallInt).Value = estimatedResources;
            if (dbCon.executeSP("webManagment_updateProject", parms, MSSQLConnection.ReturnTypes.Dataset))
            {
               booldev = true; 
            }
            else { message = "Record could not be updated into database. Message = " + dbCon.getMessage(); }
            return booldev;
        }

    }
}