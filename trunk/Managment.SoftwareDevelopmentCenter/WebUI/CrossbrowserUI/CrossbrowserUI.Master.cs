using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SystemEngine.Objects;

namespace WebUI.CrossbrowserUI
{
    public partial class CrossbrowserUI : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(this.Page.IsPostBack)){

                if (!(string.IsNullOrEmpty(Session["webUserId"].ToString()) == true))
                {
                    int webUserId = 0;
                    int.TryParse(Session["webUserId"].ToString(), out webUserId);
                    if (webUserId > 0)
                    {
                        webUser wUser = new webUser();
                        if (wUser.userInformation(webUserId))
                        {
                            lblUserName.Text = wUser.getResults().Tables[0].Rows[0]["CompleteName"].ToString() + " " +
                                           "(" + wUser.getResults().Tables[0].Rows[0]["rol"].ToString() + ")";
                        }
                    }
                }
                else {
                    Response.Redirect("default.aspx");
                }
            }
        }

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            int webUserId = 0;
            if (!(string.IsNullOrEmpty(Session["webUserId"].ToString()) == true)){ 
                int.TryParse(Session["webUserId"].ToString(), out webUserId);
                Session["webUserId"] = "";
                if (webUserId > 0) {                
                    webUser wUser = new webUser();
                    wUser.recordLogOut(webUserId);                
                }
            }            
            Response.Redirect("default.aspx");
        }
    }
}