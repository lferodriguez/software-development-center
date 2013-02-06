using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace WebUI.CrossbrowserUI.WebUserControls
{
    public partial class ClientScriptsHandler : System.Web.UI.UserControl
    {
        System.Web.UI.HtmlControls.HtmlGenericControl _hgControl;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void loadJqueryScript(bool includeCustomJquery){                
            _hgControl = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            _hgControl.Attributes.Add("type", "text/javascript");
            _hgControl.Attributes.Add("language", "javascript");
            _hgControl.Attributes.Add("src", ResolveUrl("~/CrossbrowserUI/js/jquery-1.9.0.js"));
            Page.Header.Controls.Add(_hgControl);

            if (includeCustomJquery) { 
                _hgControl = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
                _hgControl.Attributes.Add("type", "text/javascript");
                _hgControl.Attributes.Add("language", "javascript");
                _hgControl.Attributes.Add("src", ResolveUrl("~/CrossbrowserUI/js/jquery-ui-1.10.0.custom.min.js"));
                Page.Header.Controls.Add(_hgControl);
            }        
        }
        public void loadJqueryValidator(bool includeMetaDataScript, bool includeAdditionalMethods) {
            _hgControl = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            _hgControl.Attributes.Add("type", "text/javascript");
            _hgControl.Attributes.Add("language", "javascript");
            _hgControl.Attributes.Add("src", ResolveUrl("~/CrossbrowserUI/js/jquery.validate.js"));
            Page.Header.Controls.Add(_hgControl);
            if (includeMetaDataScript)
            {
                _hgControl = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
                _hgControl.Attributes.Add("type", "text/javascript");
                _hgControl.Attributes.Add("language", "javascript");
                _hgControl.Attributes.Add("src", ResolveUrl("~/CrossbrowserUI/js/jquery.metadata.js"));
                Page.Header.Controls.Add(_hgControl);
            }
            if (includeAdditionalMethods)
            {
                _hgControl = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
                _hgControl.Attributes.Add("type", "text/javascript");
                _hgControl.Attributes.Add("language", "javascript");
                _hgControl.Attributes.Add("src", ResolveUrl("~/CrossbrowserUI/js/additional-methods.js"));
                Page.Header.Controls.Add(_hgControl);
            }        
        }
        public void loadJqueryInstructions_OnDocumentReady(string setOfInstructions) {
            _hgControl = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
            _hgControl.Attributes.Add("type", "text/javascript");
            _hgControl.Attributes.Add("language", "javascript");
            StringBuilder strHtml = new StringBuilder();
            strHtml.AppendLine("$(document).ready(function(){");
            strHtml.AppendLine(setOfInstructions);
            strHtml.AppendLine("});");
            _hgControl.InnerHtml = strHtml.ToString();
            Page.Header.Controls.Add(_hgControl);
        }
    }
}