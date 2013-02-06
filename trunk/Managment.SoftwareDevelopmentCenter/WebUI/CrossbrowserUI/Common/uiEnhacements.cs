using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.CrossbrowserUI.Common
{
    public class uiEnhacements
    {
        public string preFilledText(System.Web.UI.WebControls.TextBox source, string defaultText)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.AppendLine("$('#" + source.ClientID + "').click(function () { if ($('#" + source.ClientID + "').val() == '" + defaultText + "') { $('#" + source.ClientID + "').val(''); } });");
            str.AppendLine("$('#" + source.ClientID + "').blur(function () {if ($('#" + source.ClientID + "').val() == '') $('#" + source.ClientID + "').val('" + defaultText + "');});");
            return str.ToString();
        }
    }
}