using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebUI.CrossbrowserUI.Common;

namespace WebUI.CrossbrowserUI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            loadScreenValidations();
        }
        void loadScreenValidations() {
            System.Text.StringBuilder setOfInstructions = new System.Text.StringBuilder();
            clientScripts.loadJqueryScript(false);
            clientScripts.loadJqueryValidator(false, false);
            
            uiEnhacements obj = new uiEnhacements();
            if (!(Page.IsPostBack))
            {
                txtUser.Text = "example@example.com";
                setOfInstructions.AppendLine(obj.preFilledText(txtUser, "example@example.com"));
            }
            else {
                if (txtUser.Text != String.Empty) {
                    setOfInstructions.AppendLine(obj.preFilledText(txtUser, "example@example.com"));
                }
            }

            setOfInstructions.AppendLine("$('#" + this.form1.ClientID + "').validate({");
            setOfInstructions.AppendLine("rules:{");
            setOfInstructions.AppendLine(txtUser.UniqueID + ": {required:true,email:true},");
            setOfInstructions.AppendLine(txtPass.UniqueID + ": {required:true}");
            setOfInstructions.AppendLine("},");
            setOfInstructions.AppendLine("messages:{");
            setOfInstructions.AppendLine(txtUser.UniqueID + ": {required:\"Enter a Username.\", email:\"Username must be an email\"},");
            setOfInstructions.AppendLine(txtPass.UniqueID + ": {required:\"Enter your password\"}");
            setOfInstructions.AppendLine("}");
            setOfInstructions.AppendLine("});");
            clientScripts.loadJqueryInstructions_OnDocumentReady(setOfInstructions.ToString());            
        }

        protected void bntLogin_Click(object sender, EventArgs e)
        {
            SystemEngine.Objects.webUser wuser = new SystemEngine.Objects.webUser();
            int webUserId = 0;
            if (wuser.authenticate(txtUser.Text, txtPass.Text, ref webUserId))
            {
                Session["webUserId"] = webUserId;
                Response.Redirect("main.aspx");
            }
            else {
                lblMessages.Visible = true;
                lblMessages.Text = "Email account or password are invalid. Please try again.";
            }
        }
    }
}