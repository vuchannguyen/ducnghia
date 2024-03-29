﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ltktDAO;

namespace ltkt.Admin
{
    public partial class News : System.Web.UI.Page
    {
        EventLog log = new EventLog();
        ltktDAO.Control control = new ltktDAO.Control();
        ltktDAO.News newsDAO = new ltktDAO.News();
        private ltktDAO.Users userDAO = new ltktDAO.Users();

        public const int NoOfNewsPerPage = 10;
        public const string SelfLink = "<a href=\"News.aspx?page={0}\">{1}</a>";
        public const string DisplayNewsLink = "<a href=\"../../News.aspx?id={0}\" target=\"_blank\">{1}</a>";

        protected void Page_Load(object sender, EventArgs e)
        {
            tblUser user = (tblUser)Session[CommonConstants.SES_USER];
            if (user != null)
            {
                if (userDAO.isAllow(user.Permission, CommonConstants.P_A_NEWS)
                    || userDAO.isAllow(user.Permission, CommonConstants.P_A_FULL_CONTROL))
                {
                    ///DO WORK HERE ONLY//////////////////////////////
                    AdminMaster pageMaster = (AdminMaster)Master;
                    pageMaster.updateHeader(CommonConstants.PAGE_ADMIN_NEWS_NAME);

                    liTitle.Text = CommonConstants.PAGE_ADMIN_NEWS_NAME
                                   + CommonConstants.SPACE + CommonConstants.HLINE
                                   + CommonConstants.SPACE
                                   + control.getValueString(CommonConstants.CF_TITLE_ON_HEADER);
                    if (Page.IsPostBack)
                    {
                        return;
                    }
                    int page = 1;

                    if (Request.QueryString[CommonConstants.REQ_PAGE] != null)
                    {
                        viewPanel.Visible = true;
                        addPanel.Visible = false;

                        page = Convert.ToInt32(Request.QueryString[CommonConstants.REQ_PAGE]);
                        //btnSave.Text = "Thêm tin tức";
                        showNews(page);
                    }
                    else if (Request.QueryString[CommonConstants.REQ_ACTION] != null)
                    {
                        string action = Request.QueryString[CommonConstants.REQ_ACTION];
                        int newsID = Convert.ToInt32(Request.QueryString[CommonConstants.REQ_ID]);

                        if (action == CommonConstants.ACT_EDIT)
                        {
                            viewPanel.Visible = false;
                            editNews(newsID);
                        }
                        else if (action == CommonConstants.ACT_DELETE)
                        {
                            Boolean completeDelete = newsDAO.deleteNews(newsID);

                            if (completeDelete)
                            {
                                //Response.Write(CommonConstants.ALERT_DELETE_SUCCESSFUL);
                                Response.Redirect(CommonConstants.PAGE_ADMIN_NEWS
                                                  + CommonConstants.ADD_PARAMETER
                                                  + CommonConstants.REQ_PAGE
                                                  + CommonConstants.EQUAL + "1");

                            }
                            else
                            {
                                //Response.Write(CommonConstants.ALERT_DELETE_FAIL);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect(CommonConstants.PAGE_ADMIN_NEWS
                                                  + CommonConstants.ADD_PARAMETER
                                                  + CommonConstants.REQ_PAGE
                                                  + CommonConstants.EQUAL + "1");
                    }
                    //////////////////////////////////////////////////
                }
            }
            else
            {
                Session[CommonConstants.SES_ERROR] = CommonConstants.MSG_E_ACCESS_DENIED;
                //Response.Redirect(CommonConstants.DOT + CommonConstants.PAGE_ADMIN_LOGIN);
                Response.Redirect(CommonConstants.PAGE_ADMIN_LOGIN);
            }
        }
                
        private void showNews(int page)
        {
            int totalNews = newsDAO.countNews();
            // Computing total pages
            int totalPages;
            int mod = totalNews % NoOfNewsPerPage;

            IEnumerable<tblNew> lst = newsDAO.fetchNewsList(((page - 1) * NoOfNewsPerPage), NoOfNewsPerPage);

            if (mod == 0)
            {
                totalPages = totalNews / NoOfNewsPerPage;
            }
            else
            {
                totalPages = ((totalNews - mod) / NoOfNewsPerPage) + 1;
            }
            BaseServices bs = new BaseServices();
            for (int idx = 0; idx < lst.Count(); ++idx)
            {
                tblNew news = lst.ElementAt(idx);

                TableCell noCell = new TableCell();
                noCell.CssClass = "table-cell";
                noCell.Style["width"] = "20px";
                noCell.Text = Convert.ToString(idx + 1 + (page - 1) * NoOfNewsPerPage);


                TableCell titleCell = new TableCell();
                titleCell.CssClass = "table-cell";
                //titleCell.Style["width"] = "300px";
                titleCell.Text = String.Format(DisplayNewsLink, news.ID, news.Title);

                TableCell postedCell = new TableCell();
                postedCell.CssClass = "table-cell";
                postedCell.Style["width"] = "80px";
                postedCell.Text = bs.convertDateToString(news.Posted);

                TableCell authorCell = new TableCell();
                authorCell.CssClass = "table-cell";
                authorCell.Style["width"] = "80px";
                authorCell.Text = news.Author;

                TableCell actionCell = new TableCell();
                actionCell.CssClass = "table-cell";
                actionCell.Style["width"] = "40px";
                actionCell.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_DISPLAY_LINK,
                                                                     CommonConstants.PAGE_ADMIN_NEWS,
                                                                     CommonConstants.ACT_EDIT,
                                                                     Convert.ToString(news.ID),
                                                                     CommonConstants.HTML_EDIT_ADMIN);

                actionCell.Text += BaseServices.createMsgByTemplate(CommonConstants.TEMP_DISPLAY_LINK,
                                                                     CommonConstants.PAGE_ADMIN_NEWS,
                                                                     CommonConstants.ACT_DELETE,
                                                                     Convert.ToString(news.ID),
                                                                     CommonConstants.HTML_DELETE_ADMIN);

                TableRow newsRow = new TableRow();
                newsRow.Cells.Add(noCell);
                newsRow.Cells.Add(titleCell);
                newsRow.Cells.Add(postedCell);
                newsRow.Cells.Add(authorCell);
                newsRow.Cells.Add(actionCell);

                NewsTable.Rows.AddAt(2 + idx, newsRow);
                string totatRecord = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, totalNews.ToString());
                NumRecordLiteral.Text = BaseServices.createMsgByTemplate(CommonConstants.MSG_I_NUM_SEARCHED_RECORD, totatRecord);
            }

            // Creating links to previous and next pages
            if (totalPages > 1)
            {
                if (page > 1)
                    PreviousPageLiteral.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_SELF_LINK,
                                                                                CommonConstants.PAGE_ADMIN_NEWS, 
                                                                                (page - 1).ToString(),
                                                                                CommonConstants.TXT_PREVIOUS_PAGE);

                if (page > 0 && page < totalPages)
                    NextPageLiteral.Text = BaseServices.createMsgByTemplate (CommonConstants.TEMP_SELF_LINK,
                                                                             CommonConstants.PAGE_ADMIN_NEWS,
                                                                             (page + 1).ToString(),
                                                                             CommonConstants.TXT_NEXT_PAGE);
            }
        }

        protected void btnAddNews_Click(object sender, EventArgs e)
        {
            viewPanel.Visible = false;
            addPanel.Visible = true;
            btnAddNews.Text = "Thêm bài viết";
            btnEdit.Visible = false;
            btnSave.Visible = true;
            btnSticky.Visible = false;
            btnCancel.Visible = false;
            Session[CommonConstants.SES_EDIT_NEWS] = null;
        }

        private void editNews (int newsID)
        {
            if (Session[CommonConstants.SES_EDIT_NEWS] == null)
            {
                tblNew editNews = newsDAO.getNews(newsID);
                Session[CommonConstants.SES_EDIT_NEWS] = editNews;

                btnSave.Visible = false;
                btnEdit.Visible = true;
                btnSticky.Visible = true;
                addPanel.Visible = true;

                txtTitle.Text = editNews.Title;
                txtChapeau.Text = editNews.Chapaeu;
                txtContent.Text = editNews.Contents;
            }
        }
        private tblNew getData(object sender, EventArgs e)
        {
            tblNew item = new tblNew();
            item.Chapaeu = txtChapeau.Text;
            item.Contents = txtContent.Text;
            
            return item;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string strTitlte = txtTitle.Text;
            string strChapeau = txtChapeau.Text;
            string strContent = Server.HtmlDecode(txtContent.Text);
            tblNew editNews = (tblNew)Session[CommonConstants.SES_EDIT_NEWS];
            if (editNews != null)
            {
                try
                {
                    newsDAO.updateNews(editNews.ID, editNews.Author, strTitlte, strChapeau, strContent);
                    Session[CommonConstants.SES_EDIT_NEWS] = null;
                    Response.Redirect(CommonConstants.PAGE_ADMIN_NEWS
                                              + CommonConstants.ADD_PARAMETER
                                              + CommonConstants.REQ_PAGE
                                              + CommonConstants.EQUAL + "1");
                }
                catch (Exception ex)
                {
                    writeException(ex);
                }
            }
            else
            {
                liMessage.Text = CommonConstants.MSG_E_COMMON_ERROR_TEXT;
                liMessage.Visible = true;
            }
        }
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strTitlte = txtTitle.Text;
            string strChapeau = txtChapeau.Text;
            string strContent = Server.HtmlDecode(txtContent.Text);
            bool isOk = false;
            try
            {
                isOk = newsDAO.insertNews(getCurrentUser(), strTitlte, strChapeau, strContent);
            }
            catch (Exception ex)
            {
                liMessage.Text = CommonConstants.MSG_E_COMMON_ERROR_TEXT;
                liMessage.Visible = true;

                tblUser user = (tblUser)Session[CommonConstants.SES_USER];
                writeException(ex);

                Session[CommonConstants.SES_ERROR] = CommonConstants.MSG_E_COMMON_ERROR_TEXT;
                Response.Redirect(CommonConstants.PAGE_ERROR);
            }
            if (isOk)
            {
                Response.Redirect(CommonConstants.PAGE_ADMIN_NEWS
                                                 + CommonConstants.ADD_PARAMETER
                                                 + CommonConstants.REQ_PAGE
                                                 + CommonConstants.EQUAL + CommonConstants.PAGE_NUMBER_FIRST);
            }
        }
        /// <summary>
        /// write exception to log file
        /// </summary>
        /// <param name="ex"></param>
        private void writeException(Exception ex)
        {
            string username = getCurrentUser();
            if (username == CommonConstants.BLANK)
                username = CommonConstants.USER_GUEST;
            log.writeLog(DBHelper.strPathLogFile, username, ex.Message
                                                    + CommonConstants.NEWLINE
                                                    + ex.Source
                                                    + CommonConstants.NEWLINE
                                                    + ex.StackTrace
                                                    + CommonConstants.NEWLINE
                                                    + ex.HelpLink);
            return;
        }
        /// <summary>
        /// get current username
        /// </summary>
        /// <returns></returns>
        private string getCurrentUser()
        {
            if (Session[CommonConstants.SES_USER] != null)
            {
                tblUser user = (tblUser)Session[CommonConstants.SES_USER];
                return user.Username;
            }
            return CommonConstants.BLANK;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            tblNew editNews = (tblNew)Session[CommonConstants.SES_EDIT_NEWS];
            if (editNews == null)//thêm mới
            {
                txtTitle.Text = CommonConstants.BLANK;
                txtChapeau.Text = CommonConstants.BLANK;
                txtContent.Text = Server.HtmlEncode(CommonConstants.BLANK);
            }
            else//edit
            {
                Session[CommonConstants.SES_EDIT_NEWS] = null;
            }
            

            addPanel.Visible = false;
            Response.Redirect(CommonConstants.PAGE_ADMIN_NEWS
                                                  + CommonConstants.ADD_PARAMETER
                                                  + CommonConstants.REQ_PAGE
                                                  + CommonConstants.EQUAL + "1");
        }
    }
}