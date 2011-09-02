﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ltktDAO;
using System.Web.Mail;
using ltktDAO;
using System.Text;

public partial class Contact : System.Web.UI.Page
{
    EventLog log = new EventLog();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[CommonConstants.SES_USER] != null)
        {
            tblUser user = (tblUser)Session[CommonConstants.SES_USER];
            txtboxContactName.Text = user.DisplayName;
            txtboxContactEmail.Text = user.Email.Trim();
        }
    }

    protected void btnSubmitContact_Click(object sender, EventArgs e)
    {
        string strContactName = txtboxContactName.Text;
        string strContactEmail = txtboxContactEmail.Text;
        string strContactTitle = txtboxContactTitle.Text;
        string strContactMessage = txtboxContactMessage.Text;
        if (!recaptcha.IsValid)
        {
            return;
        }
        try
        {
            MailMessage message = new MailMessage();
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            //Use 0 for anonymous
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "trungtamducnghia@gmail.com");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "123456987");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
            message.To = "trungtamducnghia@gmail.com";
            message.From = strContactEmail;
            message.Subject = strContactTitle;
            message.BodyFormat = MailFormat.Text;
            message.Body = strContactEmail + ":\n " + strContactMessage;
            message.BodyEncoding = Encoding.UTF8;
            SmtpMail.SmtpServer = "smtp.gmail.com:465";

            SmtpMail.Send(message);
            
            liMessage.Visible = true;
            contactPanel.Visible = false;
            liMessage.Text = CommonConstants.MSG_REPLY_SUCCESSFUL;

            Boolean isOK = ltktDAO.Contact.insertEmail(strContactEmail, "trungtamducnghia@gmail.com",
                strContactTitle, message.Body, DateTime.Now);
        }
        catch (Exception ex)
        {
            liMessage.Visible = true;
            liMessage.Text = CommonConstants.MSG_REPLY_FAILED;
            contactPanel.Visible = false;

            //Write to log
            tblUser user = (tblUser)Session[CommonConstants.SES_USER];
            string username = CommonConstants.USER_GUEST;
            if (user != null)
            {
                username = user.Username;
            }

            log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

            Session[CommonConstants.SES_ERROR] = CommonConstants.MSG_COMMON_ERROR_TEXT;
            Response.Redirect(CommonConstants.PAGE_ERROR);
        }

    }
}
