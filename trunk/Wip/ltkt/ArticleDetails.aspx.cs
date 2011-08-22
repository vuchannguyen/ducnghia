﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using ltktDAO;

namespace ltkt
{
    public partial class ArticleDetails : System.Web.UI.Page
    {
        EventLog log = new EventLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["sec"] != null)
            {
                string sec = Request.QueryString["sec"];
                int id = Convert.ToInt32(Request.QueryString["id"]);
                if (checkDoOneTime(CommonConstants.LIKE, sec, id))
                {
                    btnLike.Visible = false;
                }
                if( checkDoOneTime(CommonConstants.DISLIKE, sec, id))
                {
                    btnDislike.Visible = false;
                }
                try
                {
                    switch (sec)
                    {
                        case "uni":
                            {
                                // do something
                                showContest(id);
                                break;
                            }
                        case "el":
                            {
                                // do something else
                                showEnglish(id);
                                break;
                            }
                        case "it":
                            {
                                //do something
                                showInformatic(id);
                                break;
                            }
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    tblUser user = (tblUser)Session["User"];
                    string username = CommonConstants.USER_GUEST;
                    if (user != null)
                    {
                        username = user.Username;
                    }

                    log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

                    Session[CommonConstants.CONST_SES_ERROR] = CommonConstants.COMMON_ERROR_TEXT;
                    Response.Redirect(CommonConstants.PAGE_ERROR);
                }

                if (txtPostedComment.Text == String.Empty)
                {
                    txtPostedComment.Visible = false;
                }
                else
                {
                    txtPostedComment.Visible = true;
                }

                if (Session["User"] != null)
                {
                    nonUserPanel.Visible = false;
                }
                else
                {
                    nonUserPanel.Visible = true;
                }
            }
            else
            {
                viewArticle.Visible = false;
                commentPanel.Visible = false;
                relativePanel.Visible = false;
                invalidArticle.Visible = true;
                liMessage.Text = "Bài viết này không có hoặc đã bị xóa!";
                liMessage.Text += "<br /><br /><a href=\"Home.aspx\">Quay về trang chủ</a>";
            }

        }

        private void showContest(int id)
        {
            try
            {
                tblContestForUniversity contest = ltktDAO.Contest.getContest(id);
                if (contest != null)
                {
                    liTitle.Text = contest.Title;

                    lblTitle.Text = contest.Title;
                    lblLiker.Text = contest.Point.ToString();

                    lblAuthor.Text = ltktDAO.Contest.getAuthor(id);
                    lblPostedDate.Text = convertDateToString(contest.Posted);
                    lblChecker.Text = contest.Checker;
                    lblType.Text = "<a href=\"./ContestUniversity.aspx\">Luyện Thi Đại Học</a>";

                    lblSubject.Text = contest.Subject;
                    lblBranch.Text = ltktDAO.Contest.getBranch(id);
                    lblYear.Text = Convert.ToString(contest.Year);

                    lblOverview.Text = contest.Contents.Replace("\n", "<br />");

                    hpkDownloadlink.Text = contest.Title;
                    hpkDownloadlink.NavigateUrl = contest.Location.Replace("\\", "/");

                    if (contest.Solving != null)
                    {
                        lblResolve.Text = "<a href=\"" + contest.Solving.Replace("\\", "/") + "\">Hướng dẫn giải</a>";
                    }

                    txtPostedComment.Text = contest.Comment;

                    IList<tblContestForUniversity> items = ltktDAO.Contest.getRelativeByYear(contest.Year, CommonConstants.NUMBER_RECORD_RELATIVE);
                    lblRelative.Text = "<ul>";
                    for (int i = 0; i < items.Count; i++)
                    {
                        lblRelative.Text += "<li>";
                        lblRelative.Text += "<a href='ArticleDetails.aspx?sec=uni&id=" + items[i].ID + "'>" + items[i].Title.Trim() + "</a>";
                        lblRelative.Text += "(" + items[i].Posted + ")";
                        lblRelative.Text += "</li>";
                    }
                    lblRelative.Text += "</ul>";

                    infoContest.Visible = true;
                    infoEnglish.Visible = false;
                    infoInformatic.Visible = false;
                }
                else
                {
                    viewArticle.Visible = false;
                    commentPanel.Visible = false;
                    relativePanel.Visible = false;
                    invalidArticle.Visible = true;
                    liMessage.Text = "Bài viết này không có hoặc đã bị xóa!";
                    liMessage.Text += "<br /><br /><a href=\"Home.aspx\">Quay về trang chủ</a>";
                }
            }
            catch (Exception ex)
            {
                tblUser user = (tblUser)Session["User"];
                string username = CommonConstants.USER_GUEST;
                if (user != null)
                {
                    username = user.Username;
                }

                log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

                Session[CommonConstants.CONST_SES_ERROR] = CommonConstants.COMMON_ERROR_TEXT;
                Response.Redirect(CommonConstants.PAGE_ERROR);
            }
        }

        private void showEnglish(int id)
        {
            try
            {
                tblEnglish english = ltktDAO.English.getEnglish(id);
                if (english != null)
                {
                    liTitle.Text = english.Title;

                    lblTitle.Text = english.Title;
                    lblLiker.Text = english.Point.ToString();

                    lblAuthor.Text = ltktDAO.English.getAuthor(id);
                    lblPostedDate.Text = convertDateToString(english.Posted);
                    lblChecker.Text = english.Checker;
                    lblType.Text = "<a href=\"./English.aspx\">Anh văn</a>";


                    lblOverview.Text = english.Contents.Replace("\n", "<br />");

                    hpkDownloadlink.Text = english.Title;
                    hpkDownloadlink.NavigateUrl = english.Location.Replace("\\", "/");

                    IList<tblEnglish> items = ltktDAO.English.getRelativeByType(english.Type, CommonConstants.NUMBER_RECORD_RELATIVE);
                    lblRelative.Text = "<ul>";
                    for (int i = 0; i < items.Count; i++)
                    {
                        lblRelative.Text += "<li>";
                        lblRelative.Text += "<a href='ArticleDetails.aspx?sec=el&id=" + items[i].ID + "'>" + items[i].Title.Trim() + "</a>";
                        lblRelative.Text += "(" + items[i].Posted + ")";
                        lblRelative.Text += "</li>";
                    }
                    lblRelative.Text += "</ul>";

                    txtPostedComment.Text = english.Comment;

                    infoContest.Visible = false;
                    infoEnglish.Visible = true;
                    infoInformatic.Visible = false;
                }
                else
                {
                    viewArticle.Visible = false;
                    commentPanel.Visible = false;
                    relativePanel.Visible = false;
                    invalidArticle.Visible = true;
                    liMessage.Text = "Bài viết này không có hoặc đã bị xóa!";
                    liMessage.Text += "<br /><br /><a href=\"Home.aspx\">Quay về trang chủ</a>";
                }
            }
            catch (Exception ex)
            {
                tblUser user = (tblUser)Session["User"];
                string username = CommonConstants.USER_GUEST;
                if (user != null)
                {
                    username = user.Username;
                }

                log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

                Session[CommonConstants.CONST_SES_ERROR] = CommonConstants.COMMON_ERROR_TEXT;
                Response.Redirect(CommonConstants.PAGE_ERROR);
            }
        }

        private void showInformatic(int id)
        {
            try
            {
                tblInformatic informatic = ltktDAO.Informatics.getInformatic(id);
                if (informatic != null)
                {
                    liTitle.Text = informatic.Title;

                    lblTitle.Text = informatic.Title;
                    lblLiker.Text = informatic.Point.ToString();

                    lblAuthor.Text = ltktDAO.Informatics.getAuthor(id);
                    lblPostedDate.Text = convertDateToString(informatic.Posted);
                    lblChecker.Text = informatic.Checker;
                    lblType.Text = "<a href=\"./Informatics.aspx\">Tin học</a>";

                    lblOverview.Text = informatic.Contents.Replace("\n", "<br />");

                    hpkDownloadlink.Text = informatic.Title;
                    hpkDownloadlink.NavigateUrl = informatic.Location.Replace("\\", "/");

                    IList<tblInformatic> items = ltktDAO.Informatics.getRelativeByType(informatic.Type, CommonConstants.NUMBER_RECORD_RELATIVE);
                    lblRelative.Text = "<ul>";
                    for (int i = 0; i < items.Count; i++)
                    {
                        lblRelative.Text += "<li>";
                        lblRelative.Text += "<a href='ArticleDetails.aspx?sec=it&id=" + items[i].ID + "'>" + items[i].Title.Trim() + "</a>";
                        lblRelative.Text += "(" + items[i].Posted + ")";
                        lblRelative.Text += "</li>";
                    }
                    lblRelative.Text += "</ul>";


                    txtPostedComment.Text = informatic.Comment;

                    infoContest.Visible = false;
                    infoEnglish.Visible = false;
                    infoInformatic.Visible = true;
                }
                else
                {
                    viewArticle.Visible = false;
                    commentPanel.Visible = false;
                    relativePanel.Visible = false;
                    invalidArticle.Visible = true;
                    liMessage.Text = "Bài viết này không có hoặc đã bị xóa!";
                    liMessage.Text += "<br /><br /><a href=\"Home.aspx\">Quay về trang chủ</a>";
                }
            }
            catch (Exception ex)
            {
                tblUser user = (tblUser)Session["User"];
                string username = CommonConstants.USER_GUEST;
                if (user != null)
                {
                    username = user.Username;
                }

                log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

                Session[CommonConstants.CONST_SES_ERROR] = CommonConstants.COMMON_ERROR_TEXT;
                Response.Redirect(CommonConstants.PAGE_ERROR);
            }
        }

        protected void hpkDownloadLink_PreRender(object sender, EventArgs e)
        {
            
        }

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            string author = "";
            string date = "";
            string comment = "";
            string newComment = "";
            try
            {
                if (Session["User"] != null)
                {
                    tblUser user = (tblUser)Session["User"];
                    author = user.DisplayName;
                }
                else
                {
                    author = txtName.Text;
                }

                date = convertDateToString(DateTime.Now);
                comment = txtContent.Text.Replace("\n", "<br />");

                newComment += "<span>";
                newComment += "<b>" + author + "</b>" + " (" + date + ")";
                newComment += "<br />";
                newComment += comment;
                newComment += "</span>";

                // Write comment to db
                string sec = Request.QueryString["sec"];
                int id = Convert.ToInt32(Request.QueryString["id"]);

                switch (sec)
                {
                    case "uni":
                        {
                            ltktDAO.Contest.insertComment(id, newComment);
                            break;
                        }
                    case "el":
                        {
                            ltktDAO.English.insertComment(id, newComment);
                            break;
                        }
                    case "it":
                        {
                            ltktDAO.Informatics.insertComment(id, newComment);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                tblUser user = (tblUser)Session["User"];
                string username = CommonConstants.USER_GUEST;
                if (user != null)
                {
                    username = user.Username;
                }

                log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

                Session[CommonConstants.CONST_SES_ERROR] = CommonConstants.COMMON_ERROR_TEXT ;
                Response.Redirect(CommonConstants.PAGE_ERROR);
            }
            txtName.Text = "";
            txtEmail.Text = "";
            txtContent.Text = "";

            Page_Load(sender, e);
        }

        private string convertDateToString(DateTime date)
        {
            string strDate = "";
            if (date != null)
            {
                strDate += date.ToShortTimeString();
                strDate += " ngày ";
                strDate += Convert.ToString(date.Day);
                strDate += "/";
                strDate += Convert.ToString(date.Month);
                strDate += "/";
                strDate += Convert.ToString(date.Year);
            }
            return strDate;
        }

        protected void btnLike_Click(object sender, EventArgs e)
        {
            try
            {
                //// Write comment to db
                string sec = Request.QueryString["sec"];
                int id = Convert.ToInt32(Request.QueryString["id"]);

                //Check article haven't liked before
                if (!checkDoOneTime(CommonConstants.LIKE, sec, id))
                {
                    switch (sec)
                    {
                        case "uni":
                            {
                                ltktDAO.Contest.Like(id);
                                updateCookie(CommonConstants.LIKE, sec, id);
                                btnLike.Visible = false;
                                break;
                            }
                        case "el":
                            {
                                ltktDAO.English.Like(id);
                                updateCookie(CommonConstants.LIKE, sec, id);
                                btnLike.Visible = false;
                                break;
                            }
                        case "it":
                            {
                                ltktDAO.Informatics.Like(id);
                                updateCookie(CommonConstants.LIKE, sec, id);
                                btnLike.Visible = false;
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    btnLike.Visible = false;
                }
            }
            catch (Exception ex)
            {
                tblUser user = (tblUser)Session["User"];
                string username = CommonConstants.USER_GUEST;
                if (user != null)
                {
                    username = user.Username;
                }

                log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

                Session[CommonConstants.CONST_SES_ERROR] = CommonConstants.COMMON_ERROR_TEXT;
                Response.Redirect(CommonConstants.PAGE_ERROR);
            }
            Page_Load(sender, e);
        }

        private bool checkDoOneTime(string type, string sec, int id)
        {
            //uni:1,2,3,4
            //it:1,2,3,4
            //el:1,2,3,4
            string str = readCookie(type + sec);
            switch(sec)
            {
                case "uni":
                    {
                        if (Request.Cookies[type + sec] != null && str != null)
                        {
                            if(str.Contains(id.ToString()))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                        break;
                    }
                case "el":
                    {
                        if (Request.Cookies[type + sec] != null && str != null)
                        {
                            if (str.Contains(id.ToString()))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                        break;
                    }
                case "it":
                    {
                        if (Request.Cookies[type + sec] != null && str != null)
                        {
                            if (str.Contains(id.ToString()))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }
                        break;
                    }
            }
            return false;
        }
        private void updateCookie(string type, string sec, int id)
        {
            //update
            if (Request.Cookies[ type + sec] != null)
            {
                string str = readCookie(type + sec);
                str += id.ToString()  + ",";
                writeCookie(type + sec, str);
            }
            else //write new
            {
                writeCookie(type + sec, id.ToString());
            }
        }

        //Do not use this method directly
        private string readCookie(string name)
        {
            if (Request.Cookies[name] != null)
            {
                return Server.HtmlEncode(Request.Cookies[name].Value);
            }
            return "";
        }
        //Do not use this method directly
        private void writeCookie(string name, string value)
        {
            Response.Cookies[name].Value = value;
            //Response.Cookies[name].Domain = "www.luyenthikinhte.com";
            Response.Cookies[name].Expires = DateTime.Now.AddDays(14);
        }

        protected void btnDislike_Click(object sender, EventArgs e)
        {
            // Write comment to db
            try
            {
                string sec = Request.QueryString["sec"];
                int id = Convert.ToInt32(Request.QueryString["id"]);

                //Check article haven't disliked before
                if (!checkDoOneTime(CommonConstants.DISLIKE, sec, id))
                {
                    switch (sec)
                    {
                        case "uni":
                            {
                                ltktDAO.Contest.Dislike(id);
                                updateCookie(CommonConstants.DISLIKE, sec, id);
                                btnDislike.Visible = false;
                                break;
                            }
                        case "el":
                            {
                                ltktDAO.English.Dislike(id);
                                updateCookie(CommonConstants.DISLIKE, sec, id);
                                btnDislike.Visible = false;
                                break;
                            }
                        case "it":
                            {
                                ltktDAO.Informatics.Dislike(id);
                                updateCookie(CommonConstants.DISLIKE, sec, id);
                                btnDislike.Visible = false;
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    btnDislike.Visible = false;
                }
            }
            catch (Exception ex)
            {
                tblUser user = (tblUser)Session["User"];
                string username = CommonConstants.USER_GUEST;
                if (user != null)
                {
                    username = user.Username;
                }

                log.writeLog(Server.MapPath(CommonConstants.LOG_FILE_PATH), username, ex.Message);

                Session[CommonConstants.CONST_SES_ERROR] = CommonConstants.COMMON_ERROR_TEXT;
                Response.Redirect(CommonConstants.PAGE_ERROR);
            }
            Page_Load(sender, e);
        }
    }
}