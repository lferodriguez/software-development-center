<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebUI.CrossbrowserUI.Default" %>
<%@ Register src="WebUserControls/ClientScriptsHandler.ascx" tagname="ClientScriptsHandler" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Software Development Center</title>    
    <link type="text/css" rel="Stylesheet" href="css/default.css" />            
</head>
<body>
    <form id="form1" runat="server">    
    <uc2:ClientScriptsHandler ID="clientScripts" runat="server" />
    <div>
    <table class="mainContainer" align="center">
        <tr><td style=" height:20px;"></td></tr>
        <tr>
        <td class="leftColumn">
            <table border="0">
                <tr><td><img src="images/img0000.jpg" alt="SDC Banner 001" width="600"/></td></tr>
                <tr><td class="bannerText">
                    <span style="color:#FFFFFF; font-size:16px;">Manage your Software Development Team Faster and Better!</span> <br />
                    <span style="color:#FFFFFF; font-size:14px;">Organize, Plan and Execute, simply with one click.</span>
                 </td></tr>
            </table>        
        </td>
        <td class="centerColumn">
          <table>
          <tr><td><img alt="loginTittle" src="images/img0004.png" /></td></tr>
          <tr><td style="height:20px;"></td></tr>
          <tr><td><asp:Label ID="lblMessages" runat="server" Text="" CssClass="messages" Visible="false"></asp:Label></td></tr>         
          <tr><td><span class="ho_label">Email Account</span></td></tr>
          <tr><td><asp:TextBox ID="txtUser" runat="server" CssClass="ho_textBox"></asp:TextBox></td></tr>
          <tr><td><span class="ho_label">Password</span></td></tr>
          <tr><td><asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="ho_textBox"></asp:TextBox></td></tr>
          <tr><td><asp:Button ID="bntLogin" runat="server" Text="Log In" CssClass="ho_button" onclick="bntLogin_Click"/></td></tr>                    
          </table>  
        </td>
        </tr>        
        <tr>
        <td colspan="2" style="border-top-width:1px; border-top-style:solid; border-top-color:#C3C3C3; height:10px;"></td>
        </tr>
        <tr>
        <td colspan="2"><span>&copy; lrodriguez 2013. All Rights Reserved.</span></td>
        </tr>
    </table>
    </div>
    </form>
</body>    
</html>
