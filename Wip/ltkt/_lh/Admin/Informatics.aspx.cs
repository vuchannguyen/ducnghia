﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ltktDAO;
using System.Collections;
using System.IO;

namespace ltkt.Admin
{
    public partial class Informatics : System.Web.UI.Page
    {
        private ltktDAO.Users userDAO = new ltktDAO.Users();
        private ltktDAO.Control control = new ltktDAO.Control();
        private ltktDAO.BaseServices bs = new ltktDAO.BaseServices();
        private ltktDAO.Informatics infDAO = new ltktDAO.Informatics();
        private ltktDAO.EventLog log = new ltktDAO.EventLog();
        private ltktDAO.Statistics statDAO = new ltktDAO.Statistics();

        private const int NoOfInformacticsPerPage = 8;

        protected void Page_Load(object sender, EventArgs e)
        {
            tblUser user = (tblUser)Session[CommonConstants.SES_USER];
            if (user != null)
            {
                if (userDAO.isAllow(user.Permission, CommonConstants.P_A_INFORMATICS)
                    || userDAO.isAllow(user.Permission, CommonConstants.P_A_FULL_CONTROL))
                {
                    ///DO WORK HERE ONLY//////////////////////////////
                    AdminMaster page = (AdminMaster)Master;
                    page.updateHeader(CommonConstants.PAGE_ADMIN_INFORMATICS_NAME);

                    liTitle.Text = CommonConstants.PAGE_ADMIN_INFORMATICS_NAME
                                   + CommonConstants.SPACE + CommonConstants.HLINE
                                   + CommonConstants.SPACE
                                   + control.getValueString(CommonConstants.CF_TITLE_ON_HEADER);

                    liTableHeader.Text = CommonConstants.TXT_LIST_ARTICLE;

                    //hpkShowAll.Text += "(" + infDAO.countInf() + ")";
                    // hpkShowUncheck.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_UNCHECK) + ")";
                    //hpkShowChecked.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_CHECKED) + ")";
                    //hpkShowBad.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_BAD) + ")";
                    int numDeletedFile = infDAO.countDeletedArticles();
                    if (numDeletedFile > 0)
                    {
                        btnClear.Text = CommonConstants.TXT_CLEAR_DATA;
                        btnClear.Text += CommonConstants.SPACE;
                        btnClear.Text += "(" + numDeletedFile + ")";
                        btnClear.Visible = true;
                    }
                    else
                    {
                        btnClear.Visible = false;
                    }
                    pageLoad(sender, e, user);
                    string inform = (string)Session[CommonConstants.SES_INFORM];
                    if (!BaseServices.isNullOrBlank(inform))
                    {
                        showErrorMessage(inform);
                        Session[CommonConstants.SES_INFORM] = null;
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

        private void pageLoad(object sender, EventArgs e, tblUser user)
        {
            bool isDeleted = false;
            bool isEditError = false;
            try
            {
                int page = 1;
                string action = Request.QueryString[CommonConstants.REQ_ACTION];
                string sPage = Request.QueryString[CommonConstants.REQ_PAGE];
                string state = Request.QueryString[CommonConstants.REQ_STATE];
                if (BaseServices.isNullOrBlank(action))
                {
                    action = CommonConstants.ACT_SEARCH;
                }
                if (BaseServices.isNullOrBlank(sPage))
                {
                    sPage = CommonConstants.PAGE_NUMBER_FIRST;
                }
                if (BaseServices.isNullOrBlank(state))
                {
                    state = CommonConstants.ALL;
                }
                page = Convert.ToInt32(sPage);


                //action is Search
                if (action == CommonConstants.ACT_SEARCH)
                {
                    if (Page.IsPostBack)
                    {
                        return;
                    }

                    showCountingArticle();
                    viewPanel.Visible = true;
                    detailPanel.Visible = false;
                    messagePanel.Visible = false;
                    IEnumerable<tblInformatic> lst = null;
                    //page = Convert.ToInt32(Request.QueryString[CommonConstants.REQ_PAGE]);
                    string key = Request.QueryString[CommonConstants.REQ_KEY];
                    int totalRecord = 0;
                    if (BaseServices.isNullOrBlank(key))
                    {
                        key = CommonConstants.ALL;
                    }
                    else
                    {
                        key = BaseServices.nullToBlank(key);
                    }
                    changeViewState(key);
                    if (key == CommonConstants.ALL)
                    {
                        //lst = infDAO.fetchInfList(((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                        if (state == CommonConstants.ALL)// key = ALL and state = ALL
                        {
                            lst = infDAO.fetchInfList(((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countInf();
                            string txt = hpkShowAllState.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowAllState.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                            hpkShowUncheck.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_UNCHECK) + ")";
                            hpkShowChecked.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_CHECKED) + ")";
                            hpkShowBad.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_BAD) + ")";
                            statDAO.setValue(CommonConstants.SF_NUM_ARTICLE_ON_IT, totalRecord.ToString());
                        }
                        else if (state == CommonConstants.STATE_UNCHECK.ToString())// key = ALL and state = UNCHECk
                        {
                            lst = infDAO.fetchInfList(CommonConstants.STATE_UNCHECK, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countInfListByState(CommonConstants.STATE_UNCHECK);

                            hpkShowAllState.Text += "(" + infDAO.countInf() + ")";
                            string txt = hpkShowUncheck.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowUncheck.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                            hpkShowChecked.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_CHECKED) + ")";
                            hpkShowBad.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_BAD) + ")";
                        }
                        else if (state == CommonConstants.STATE_CHECKED.ToString())// key = ALL and state = CHECKED
                        {
                            lst = infDAO.fetchInfList(CommonConstants.STATE_CHECKED, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countInfListByState(CommonConstants.STATE_CHECKED);

                            hpkShowAllState.Text += "(" + infDAO.countInf() + ")";
                            hpkShowUncheck.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_UNCHECK) + ")";
                            string txt = hpkShowChecked.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowChecked.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                            hpkShowBad.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_BAD) + ")";
                        }
                        else if (state == CommonConstants.STATE_BAD.ToString())// key = ALL and state = BAD
                        {
                            lst = infDAO.fetchInfList(CommonConstants.STATE_BAD, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countInfListByState(CommonConstants.STATE_BAD);

                            hpkShowAllState.Text += "(" + infDAO.countInf() + ")";
                            hpkShowUncheck.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_UNCHECK) + ")";
                            hpkShowChecked.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_CHECKED) + ")";
                            string txt = hpkShowBad.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowBad.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                        }
                    }
                    else if (key == CommonConstants.TXT_STICKY.ToLower())
                    {
                        changeStateLink(key);
                        if (state == CommonConstants.ALL)// key = ALL and state = ALL
                        {
                            lst = infDAO.getStickyArticles(((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countStickyInfArticle();
                            string txt = hpkShowAllState.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowAllState.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                            hpkShowUncheck.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_UNCHECK) + ")";
                            hpkShowChecked.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_CHECKED) + ")";
                            hpkShowBad.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_BAD) + ")";
                        }
                        else if (state == CommonConstants.STATE_UNCHECK.ToString())// key = ALL and state = UNCHECk
                        {
                            lst = infDAO.getStickyArticlesByState(CommonConstants.STATE_UNCHECK, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countStickyInfArticleByState(CommonConstants.STATE_UNCHECK);

                            hpkShowAllState.Text += "(" + infDAO.countStickyInfArticle() + ")";
                            string txt = hpkShowUncheck.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowUncheck.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                            hpkShowChecked.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_CHECKED) + ")";
                            hpkShowBad.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_BAD) + ")";
                        }
                        else if (state == CommonConstants.STATE_CHECKED.ToString())// key = ALL and state = CHECKED
                        {
                            lst = infDAO.getStickyArticlesByState(CommonConstants.STATE_CHECKED, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countInfListByState(CommonConstants.STATE_CHECKED);

                            hpkShowAllState.Text += "(" + infDAO.countStickyInfArticle() + ")";
                            hpkShowUncheck.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_UNCHECK) + ")";
                            string txt = hpkShowChecked.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowChecked.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                            hpkShowBad.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_BAD) + ")";
                        }
                        else if (state == CommonConstants.STATE_BAD.ToString())// key = ALL and state = BAD
                        {
                            lst = infDAO.getStickyArticlesByState(CommonConstants.STATE_BAD, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countStickyInfArticleByState(CommonConstants.STATE_BAD);

                            hpkShowAllState.Text += "(" + infDAO.countStickyInfArticle() + ")";
                            hpkShowUncheck.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_UNCHECK) + ")";
                            hpkShowChecked.Text += "(" + infDAO.countStickyInfArticleByState(CommonConstants.STATE_CHECKED) + ")";
                            string txt = hpkShowBad.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowBad.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                        }
                    }
                    else//key != ALL
                    {
                        //lst = infDAO.fetchInfList(Convert.ToInt32(key), ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                        int lei = -1;
                        if (!BaseServices.isNumeric(key))
                        {
                            lei = CommonConstants.AT_IT_OFFICE_WORD;
                        }
                        else
                        {
                            lei = BaseServices.convertStringToInt(key.Trim());
                        }
                        //change state link
                        changeStateLink(key);

                        if (state == CommonConstants.ALL)//key != ALL and state = ALL
                        {
                            lst = infDAO.fetchInfListWithLeitmotif(lei, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);

                            totalRecord = infDAO.countInfByLeitmotif(lei);
                            hpkShowUncheck.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_UNCHECK) + ")";
                            hpkShowChecked.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_CHECKED) + ")";
                            hpkShowBad.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_BAD) + ")";
                            string txt = hpkShowAllState.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowAllState.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                        }
                        else if (state == CommonConstants.STATE_UNCHECK.ToString())//key != ALL and state = UNCHECK
                        {
                            lst = infDAO.fetchInfList(lei, CommonConstants.STATE_UNCHECK, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);

                            totalRecord = infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_UNCHECK);
                            hpkShowAllState.Text += "(" + infDAO.countInfByLeitmotif(lei) + ")";
                            hpkShowChecked.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_CHECKED) + ")";
                            hpkShowBad.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_BAD) + ")";
                            string txt = hpkShowUncheck.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowUncheck.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                        }
                        else if (state == CommonConstants.STATE_CHECKED.ToString())//key != ALL and state = CHECKED
                        {
                            lst = infDAO.fetchInfList(lei, CommonConstants.STATE_CHECKED, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_CHECKED);
                            hpkShowAllState.Text += "(" + infDAO.countInfByLeitmotif(lei) + ")";
                            hpkShowUncheck.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_UNCHECK) + ")";
                            string txt = hpkShowChecked.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowChecked.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                            hpkShowBad.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_BAD) + ")";
                        }
                        else if (state == CommonConstants.STATE_BAD.ToString())//key != ALL and state = BAD
                        {
                            lst = infDAO.fetchInfList(lei, CommonConstants.STATE_BAD, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                            totalRecord = infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_BAD);
                            hpkShowAllState.Text += "(" + infDAO.countInfByLeitmotif(lei) + ")";
                            hpkShowUncheck.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_UNCHECK) + ")";
                            hpkShowChecked.Text += "(" + infDAO.countInfListByStateAndLeimotif(lei, CommonConstants.STATE_CHECKED) + ")";
                            string txt = hpkShowBad.Text;
                            txt += "(" + totalRecord + ")";
                            hpkShowBad.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, txt);
                        }

                    }

                    // show data
                    bool isOK = false;
                    if (lst != null)
                    {
                        if (lst.Count() > 0)
                        {
                            showInformactics(lst, totalRecord, page, action, key, state);
                            isOK = true;
                        }

                    }
                    if (!isOK)
                    {
                        showInfoMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);
                        InformaticsTable.Visible = false;
                        return;
                    }
                }
                else if (action == CommonConstants.ACT_VIEW || action == CommonConstants.ACT_EDIT)
                {
                    if (Page.IsPostBack)
                    {
                        return;
                    }
                    
                    Session[CommonConstants.SES_OLD_PAGE] = Request.UrlReferrer.ToString();

                    if (Request.QueryString[CommonConstants.REQ_ID] == null)
                    {
                        showErrorMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);
                        return;
                    }
                    if (!BaseServices.isNumeric(Request.QueryString[CommonConstants.REQ_ID]))
                    {
                        showErrorMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);
                        return;
                    }

                    
                    int _id = Convert.ToInt32(Request.QueryString[CommonConstants.REQ_ID]);
                    viewPanel.Visible = false;
                    detailPanel.Visible = true;
                    messagePanel.Visible = false;

                    tblInformatic article = infDAO.getInformatic(_id);
                    if (article != null)
                    {
                        //initilize combobox
                        initial();
                        Session[CommonConstants.SES_ID] = _id;
                        txtTitle.Text = BaseServices.nullToBlank(article.Title);
                        txtChapeau.Text = BaseServices.nullToBlank(article.Contents);
                        txtAuthor.Text = BaseServices.nullToBlank(article.Author);
                        txtPosted.Text = BaseServices.formatDateTimeString(article.Posted);
                        txtTag.Text = BaseServices.nullToBlank(article.Tag);
                        txtPoint.Text = article.Point.ToString();
                        txtChecker.Text = BaseServices.nullToBlank(article.Checker);
                        txtHtmlEmbbed.Text = BaseServices.nullToBlank(article.HtmlPreview);
                        txtHtmlPreviewLink.Text = BaseServices.nullToBlank(article.HtmlEmbedLink);
                        txtLocation.Text = BaseServices.nullToBlank(article.Location);
                        txtChecker.Text = BaseServices.nullToBlank(article.Checker);
                        txtThumbnail.Text = BaseServices.nullToBlank(article.Thumbnail);
                        txtComment.Text = BaseServices.nullToBlank(article.Comment);
                        txtFolderId.Text = BaseServices.nullToBlank(article.FolderID);
                        showFileContent(article.Location);
                        showThumbnail(article.Thumbnail);
                        ddlState.SelectedValue = article.State.ToString();
                        if (article.StickyFlg)
                        {
                            ddlSticky.SelectedValue = CommonConstants.CONST_ONE;
                        }
                        ddlType.SelectedValue = article.Type.ToString();
                        ddlLeitmotif.SelectedValue = article.Leitmotif.ToString();
                        ddlScore.SelectedValue = article.Score.ToString();

                    }
                    else
                    {
                        isEditError = true;
                    }

                    if (action == CommonConstants.ACT_VIEW)
                    {
                        btnEdit.Visible = false;
                        changeState(false);
                        liContent.Text += "&nbsp;&nbsp;<input type=\"button\" value=\"Tải tập tin nội dung\" class=\"formbutton\" onclick=\"uploadContent()\" />";
                        liThumbnail.Text += "&nbsp;&nbsp;<input type=\"button\" value=\"Tải hình thu nhỏ\" class=\"formbutton\" onclick=\"uploadThumbnail()\" />";
                    }
                    else
                    {
                        changeState(true);
                        liContent.Text += "&nbsp;&nbsp;<input type=\"button\" value=\"Tải tập tin nội dung\" class=\"formbutton\" onclick=\"uploadContent()\" />";
                        liThumbnail.Text += "&nbsp;&nbsp;<input type=\"button\" value=\"Tải hình thu nhỏ\" class=\"formbutton\" onclick=\"uploadThumbnail()\" />";
                    }
                    
                }
                else if (action == CommonConstants.ACT_DELETE)
                {
                    if (Request.QueryString[CommonConstants.REQ_ID] == null)
                    {
                        showErrorMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);
                        return;
                    }
                    if (!BaseServices.isNumeric(Request.QueryString[CommonConstants.REQ_ID]))
                    {
                        showErrorMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);
                        return;
                    }
                    int _id = BaseServices.convertStringToInt(Request.QueryString[CommonConstants.REQ_ID]);

                    bool isMatch = infDAO.isState(_id, CommonConstants.STATE_UNCHECK);
                    if (infDAO.setDeleteFlag(_id, user.Username))
                    {
                        if (isMatch)
                        {
                            ltktDAO.Statistics statDAO = new ltktDAO.Statistics();
                            statDAO.add(CommonConstants.SF_NUM_ARTICLE_ON_IT, CommonConstants.CONST_ONE_NEGATIVE);
                        }
                        string mess = BaseServices.createMsgByTemplate(CommonConstants.MSG_I_ACTION_SUCCESSFUL, CommonConstants.ACT_DELETE);

                        //ltktDAO.Alert.Show(mess);
                        isDeleted = true;
                        Session[CommonConstants.SES_INFORM] = BaseServices.createMsgByTemplate(CommonConstants.MSG_I_ACTION_SUCCESSFUL, CommonConstants.ACT_DELETE);
                    }
                    else
                    {
                        Session[CommonConstants.SES_INFORM] = BaseServices.createMsgByTemplate(CommonConstants.MSG_E_ACTION_FAILED, CommonConstants.ACT_DELETE);
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLog(DBHelper.strPathLogFile, user.Username, CommonConstants.MSG_E_LINK_INVALID);
                log.writeLog(DBHelper.strPathLogFile, user.Username, ex.Message
                                                        + CommonConstants.NEWLINE
                                                        + ex.Source
                                                        + CommonConstants.NEWLINE
                                                        + ex.StackTrace
                                                        + CommonConstants.NEWLINE
                                                        + ex.HelpLink);

                Response.Redirect(CommonConstants.PAGE_ADMIN_INFORMATICS
                                              + CommonConstants.ADD_PARAMETER
                                              + CommonConstants.REQ_PAGE
                                              + CommonConstants.EQUAL
                                              + CommonConstants.PAGE_NUMBER_FIRST);
            }
            if (isDeleted)
            {
                Response.Redirect(CommonConstants.PAGE_ADMIN_INFORMATICS);
            }
            if (isEditError)
            {
                detailPanel.Visible = false;
                viewPanel.Visible = false;
                showErrorMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);

            }
        }

        private void showInformactics(IEnumerable<tblInformatic> lst, int totalInformactic, int page, string action, string key, string state)
        {
            //int totalInformactic = 0;
            // Computing total pages
            int totalPages;
            int mod = totalInformactic % NoOfInformacticsPerPage;

            if (mod == 0)
            {
                totalPages = totalInformactic / NoOfInformacticsPerPage;
            }
            else
            {
                totalPages = ((totalInformactic - mod) / NoOfInformacticsPerPage) + 1;
            }

            for (int idx = 0; idx < lst.Count(); ++idx)
            {
                tblInformatic inf = lst.ElementAt(idx);

                TableCell noCell = new TableCell();
                noCell.CssClass = "table-cell";
                noCell.Style["width"] = "10px";
                noCell.Text = Convert.ToString(idx + 1 + (page - 1) * NoOfInformacticsPerPage);

                TableCell titleCell = new TableCell();
                titleCell.CssClass = "table-cell";
                titleCell.Style["width"] = "200px";
                titleCell.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_DISPLAY_LINK,
                                                                  CommonConstants.PAGE_ADMIN_INFORMATICS,
                                                                  CommonConstants.ACT_VIEW,
                                                                  Convert.ToString(inf.ID),
                                                                  inf.Title);
                TableCell leimotifCell = new TableCell();
                leimotifCell.CssClass = "table-cell";
                leimotifCell.Style["width"] = "80px";
                leimotifCell.Text = infDAO.getName(inf.Leitmotif);

                TableCell postedCell = new TableCell();
                postedCell.CssClass = "table-cell";
                postedCell.Style["width"] = "80px";
                postedCell.Text = bs.convertDateToString(inf.Posted);

                TableCell authorCell = new TableCell();
                authorCell.CssClass = "table-cell";
                authorCell.Style["width"] = "60px";
                authorCell.Text = inf.Author;

                TableCell stateCell = new TableCell();
                stateCell.CssClass = "table-cell";
                stateCell.Style["width"] = "40px";
                stateCell.Text = infDAO.getState(inf.ID);


                TableCell actionCell = new TableCell();
                actionCell.CssClass = "table-cell";
                actionCell.Style["width"] = "40px";
                actionCell.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_DISPLAY_LINK,
                                                                     CommonConstants.PAGE_ADMIN_INFORMATICS,
                                                                     CommonConstants.ACT_EDIT,
                                                                     Convert.ToString(inf.ID),
                                                                     CommonConstants.HTML_EDIT_ADMIN);

                actionCell.Text += BaseServices.createMsgByTemplate(CommonConstants.TEMP_DISPLAY_LINK,
                                                                     CommonConstants.PAGE_ADMIN_INFORMATICS,
                                                                     CommonConstants.ACT_DELETE,
                                                                     Convert.ToString(inf.ID),
                                                                     CommonConstants.HTML_DELETE_ADMIN);


                TableRow infRow = new TableRow();
                infRow.Cells.Add(noCell);
                infRow.Cells.Add(titleCell);
                infRow.Cells.Add(postedCell);
                infRow.Cells.Add(leimotifCell);
                infRow.Cells.Add(authorCell);
                infRow.Cells.Add(stateCell);
                infRow.Cells.Add(actionCell);

                InformaticsTable.Rows.AddAt(2 + idx, infRow);
                string totatRecord = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, totalInformactic.ToString());
                NumRecordLiteral.Text = BaseServices.createMsgByTemplate(CommonConstants.MSG_I_NUM_SEARCHED_RECORD, totatRecord);
            }

            // Creating links to previous and next pages
            if (totalPages > 1)
            {
                string param = CommonConstants.REQ_ACTION
                                + CommonConstants.EQUAL
                                + action
                                + CommonConstants.AND
                                + CommonConstants.REQ_KEY
                                + CommonConstants.EQUAL
                                + key;
                if (!BaseServices.isNullOrBlank(state))
                {
                    param += CommonConstants.AND
                               + CommonConstants.REQ_STATE
                               + CommonConstants.EQUAL
                               + state;
                }
                param += CommonConstants.AND
                       + CommonConstants.REQ_PAGE
                       + CommonConstants.EQUAL;


                if (page > 1)
                {

                    PreviousPageLiteral.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_PAGING_LINK,
                                                                                CommonConstants.PAGE_ADMIN_INFORMATICS,
                                                                                param + (page - 1).ToString(),
                                                                                CommonConstants.TXT_PREVIOUS_PAGE);
                }
                if (page > 0 && page < totalPages)
                {
                    NextPageLiteral.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_PAGING_LINK,
                                                                             CommonConstants.PAGE_ADMIN_INFORMATICS,
                                                                             param + (page + 1).ToString(),
                                                                             CommonConstants.TXT_NEXT_PAGE);
                }
            }
        }
        private void resetDefaultLabel()
        {
            hpkShowAll.Text = CommonConstants.TXT_ALL;
            hpkShowSticky.Text = CommonConstants.TXT_STICKY + CommonConstants.SPACE;
            hpkShowAllState.Text = CommonConstants.TXT_ALL + CommonConstants.SPACE;
            hpkShowBad.Text = CommonConstants.STATE_BAD_NAME + CommonConstants.SPACE;
            hpkShowChecked.Text = CommonConstants.STATE_CHECKED_NAME + CommonConstants.SPACE;
            hpkShowUncheck.Text = CommonConstants.STATE_UNCHECK_NAME + CommonConstants.SPACE;
            hpkShowWord.Text = CommonConstants.AT_IT_OFFICE_WORD_NAME + CommonConstants.SPACE;
            hpkShowAccess.Text = CommonConstants.AT_IT_OFFICE_ACCESS_NAME + CommonConstants.SPACE + CommonConstants.SPACE;
            hpkShowExcel.Text = CommonConstants.AT_IT_OFFICE_EXCEL_NAME + CommonConstants.SPACE;
            hpkShowPP.Text = CommonConstants.AT_IT_OFFICE_POWERPOINT_NAME + CommonConstants.SPACE;
            hpkShowAdvTip.Text = CommonConstants.AT_IT_ADVANCE_TIP_NAME + CommonConstants.SPACE;
            hpkShowSimTip.Text = CommonConstants.AT_IT_SIMPLE_TIP_NAME + CommonConstants.SPACE;

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Session[CommonConstants.SES_ID] == null)
            {
                string message = CommonConstants.MSG_E_RESOURCE_NOT_FOUND;
                message += CommonConstants.TEMP_BR_TAG;
                message += BaseServices.createMsgByTemplate(CommonConstants.MSG_E_ACTION_FAILED, CommonConstants.ACT_EDIT);
                showErrorMessage(message);
                return;
            }
            string sError = validateForm();
            if (!BaseServices.isNullOrBlank(sError))
            {
                showErrorMessage(sError);
                return;
            }
            tblInformatic item = new tblInformatic();
            item.Contents = txtChapeau.Text.Trim();
            item.Posted = BaseServices.getDateTimeFromString(txtPosted.Text);
            item.Title = txtTitle.Text.Trim();
            item.Tag = txtTag.Text.Trim();
            item.Location = txtLocation.Text.Trim();
            item.Author = txtAuthor.Text;
            int score = BaseServices.convertStringToInt(ddlScore.SelectedValue);
            item.Score = score;
            //get checker when score > 0.
            if (score != 0)
            {
                if (BaseServices.isNullOrBlank(txtChecker.Text))
                {
                    item.Checker = getCurrentUser();
                }
                else
                {
                    item.Checker = txtChecker.Text.Trim();
                }
            }
            else
            {
                item.Checker = null;
            }
            item.Comment = txtComment.Text.Trim();
            item.Thumbnail = txtThumbnail.Text.Trim();
            item.HtmlEmbedLink = txtHtmlEmbbed.Text.Trim();
            item.HtmlPreview = txtHtmlPreviewLink.Text.Trim();
            int type = BaseServices.convertStringToInt(ddlType.SelectedValue.ToString());
            item.Type = type;
            item.Leitmotif = BaseServices.convertStringToInt(ddlLeitmotif.SelectedValue.ToString());
            item.FolderID = txtFolderId.Text.Trim();
            //sticky item
            if (ddlSticky.SelectedValue.ToString() == CommonConstants.CONST_ZERO)
            {
                item.StickyFlg = false;
            }
            else
            {
                int stickied = infDAO.countStickyInfArticle();
                int noOfInfoOnPage = control.getValueByInt(CommonConstants.CF_NUM_ARTICLE_ON_IT);
                if (stickied > noOfInfoOnPage / 2)
                {
                    showErrorMessage(BaseServices.createMsgByTemplate(CommonConstants.MSG_E_OVER_NUMBER,
                        CommonConstants.TXT_STICKY,
                        stickied.ToString(),
                        CommonConstants.TXT_ONE_HALF +
                        CommonConstants.SPACE +
                        CommonConstants.CF_NUM_ARTICLE_ON_IT_NAME,
                        noOfInfoOnPage.ToString()));
                    return;
                }
                item.StickyFlg = true;
            }
            //state
            string sState = ddlState.SelectedValue.ToString();
            int iState = BaseServices.convertStringToInt(sState);
            if (iState == CommonConstants.STATE_CHECKED && item.Leitmotif == CommonConstants.AT_UNCLASSIFIED)
            {
                string message = BaseServices.createMsgByTemplate(CommonConstants.MSG_E_PLEASE_INPUT_DATA, CommonConstants.TXT_SUBJECT);
                showErrorMessage(message);
                return;
            }
            item.State = iState;
            int id = (Int32)Session[CommonConstants.SES_ID];
            //upload file
            string _fileContentSave = BaseServices.nullToBlank(txtLocation.Text);
            string _fileThumbnailSave = BaseServices.nullToBlank(txtThumbnail.Text);
            string _fileContent = CommonConstants.BLANK;
            string _fileThumbnail = CommonConstants.BLANK;
            string _folderID = txtFolderId.Text.Trim();
            bool _fileContentGood = false;
            bool _fileThumbnailGood = false;
            //root folder
            string rootFolder = Server.MapPath("~") + "/" + CommonConstants.FOLDER_IT + _folderID;
            //file type allow
            string fileTypes = control.getValueString(CommonConstants.CF_FILE_TYPE_ALLOW);

            //size in MB
            int fileSizeMax = control.getValueByInt(CommonConstants.CF_MAX_FILE_SIZE); ;
            fileSizeMax = 1024 * 1024 * fileSizeMax;

            //upload file content
            if (fileContent.HasFile)
            {
                //check file existed: keep both
                _fileContent = bs.fileNameToSave(rootFolder + "/" + fileContent.FileName);
                //filename = newFileName;

                //check filetype
                if (!bs.checkFileType(fileContent.FileName, fileTypes))
                {
                    showErrorMessage(CommonConstants.MSG_E_FILE_SIZE_IS_NOT_ALLOW);
                    return;
                }
                //check filesize max (MB)
                if (fileContent.PostedFile.ContentLength > fileSizeMax)
                {
                    showErrorMessage(CommonConstants.MSG_E_FILE_SIZE_IS_TOO_LARGE);
                    return;
                }
                _fileContentSave = _fileContent.Substring(_fileContent.LastIndexOf(CommonConstants.FOLDER_DATA));
                _fileContentGood = true;
                item.Location = _fileContentSave;
            }
            else
            {
                if (_fileContentSave.LastIndexOf("/") > 0)
                    _fileContent = rootFolder + _fileContentSave.Substring(_fileContentSave.LastIndexOf("/"));
                else
                    _fileContent = rootFolder + _fileContentSave.Substring(_fileContentSave.LastIndexOf("\\"));

                if (!File.Exists(_fileContent))
                {
                    string src = Server.MapPath("~") + "/" + _fileContentSave;
                    File.Copy(src, _fileContent);
                    File.Delete(src);
                    _fileContentSave = _fileContent.Substring(_fileContent.LastIndexOf(CommonConstants.FOLDER_DATA));
                    item.Location = _fileContentSave;
                }
            }

            //upload thumbnail
            if (fileThumbnail.HasFile)
            {
                //check file existed: keep both
                _fileThumbnail = Path.GetFileNameWithoutExtension(_fileContentSave) +
                            "_thumbnail" + Path.GetExtension(fileThumbnail.FileName);

                _fileThumbnail = bs.fileNameToSave(rootFolder + "/" + _fileThumbnail);
                //filename = rootFolder + newFileName;

                //check filetype
                fileTypes = control.getValueString(CommonConstants.CF_IMG_FILE_TYPE_ALLOW);
                if (!bs.checkFileType(fileThumbnail.FileName, fileTypes))
                {
                    showErrorMessage(CommonConstants.MSG_E_FILE_SIZE_IS_NOT_ALLOW);
                    return;
                }
                //check filesize max (KB)
                fileSizeMax = control.getValueByInt(CommonConstants.CF_IMG_FILE_SIZE_MAX);
                fileSizeMax = 1024 * fileSizeMax;
                if (fileThumbnail.PostedFile.ContentLength > fileSizeMax)
                {
                    showErrorMessage(CommonConstants.MSG_E_FILE_SIZE_IS_TOO_LARGE);
                    return;
                }
                _fileThumbnailSave = _fileThumbnail.Substring(_fileContent.LastIndexOf(CommonConstants.FOLDER_DATA));
                _fileThumbnailGood = true;
                item.Thumbnail = _fileThumbnailSave;
            }
            else if (_fileThumbnailSave != CommonConstants.BLANK)
            {
                if (!_fileThumbnailSave.Contains(CommonConstants.FOLDER_DEFAULT_IMG))
                {
                    if (_fileThumbnailSave.LastIndexOf("/") > 0)
                        _fileThumbnail = rootFolder + _fileThumbnailSave.Substring(_fileThumbnailSave.LastIndexOf("/"));
                    else
                        _fileThumbnail = rootFolder + _fileThumbnailSave.Substring(_fileThumbnailSave.LastIndexOf("\\"));

                    if (!File.Exists(_fileThumbnail))
                    {
                        string src = Server.MapPath("~") + "/" + _fileThumbnailSave;
                        File.Copy(src, _fileThumbnail);
                        File.Delete(src);
                        _fileThumbnailSave = _fileThumbnail.Substring(_fileThumbnail.LastIndexOf(CommonConstants.FOLDER_DATA));
                        item.Thumbnail = _fileThumbnailSave;
                    }
                }
            }

            bool isOk = false;
            try
            {
                isOk = infDAO.updateInformatic(id, item, getCurrentUser());
            }
            catch (Exception ex)
            {
                writeException(ex);
            }
            if (isOk)
            {
                if (_fileContentGood)
                {
                    string folder = Path.GetDirectoryName(_fileContent);
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    fileContent.SaveAs(_fileContent);
                }
                if (_fileThumbnailGood)
                {
                    string folder = Path.GetDirectoryName(_fileThumbnail);
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    fileThumbnail.SaveAs(_fileThumbnail);
                }


                int stickiedArticle = infDAO.countStickyInfArticle();
                statDAO.setValue(CommonConstants.SF_NUM_STICKED_ON_IT, stickiedArticle.ToString());
                Session[CommonConstants.SES_INFORM] = BaseServices.createMsgByTemplate(CommonConstants.MSG_I_ACTION_SUCCESSFUL, CommonConstants.ACT_EDIT);
            }
            else
            {
                Session[CommonConstants.SES_INFORM] = BaseServices.createMsgByTemplate(CommonConstants.MSG_E_ACTION_FAILED, CommonConstants.ACT_EDIT);
            }
            Response.Redirect(CommonConstants.PAGE_ADMIN_INFORMATICS);
            
        }
        private void showFileContent(string location)
        {
            if (File.Exists(DBHelper.strCurrentPath + location))
            {
                liContent.Text = "&nbsp;&nbsp;<br/><input type=\"button\" value=\"Mở\" class=\"formbutton\" onclick=\"openFile('../../" + location.Replace("\\", "/") + "')\"/>";
            }
            else
                liContent.Text = CommonConstants.MSG_E_RESOURCE_NOT_FOUND;
        }
        private void showThumbnail(string location)
        {
            if (File.Exists(DBHelper.strCurrentPath + location))
            {
                liThumbnail.Text = "&nbsp;&nbsp;<br /><input type=\"button\" value=\"Mở\" class=\"formbutton\" onclick=\"DisplayFullImage('../../" + location.Replace("\\", "/") + "')\"/>";
            }
            else
                liThumbnail.Text = CommonConstants.MSG_E_RESOURCE_NOT_FOUND;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            IEnumerable<tblInformatic> lst = infDAO.getDeletedArticle();
            string path = CommonConstants.BLANK;
            bool fileDeleted = false;
            int totalFileDeleted = 0;
            bool deleteSuccessful = false;

            //build list path to delete
            System.Collections.ArrayList strPathList = new ArrayList();
            foreach (var item in lst)
            {
                strPathList.Add(item.FolderID.Trim());
            }
            try
            {
                if (lst != null)
                {
                    deleteSuccessful = infDAO.deleteInf(getCurrentUser());
                    if (deleteSuccessful)
                    {
                        foreach (var item in strPathList)
                        {
                            path = DBHelper.strCurrentPath;
                            path += CommonConstants.FOLDER_IT;
                            path += item;
                            fileDeleted = BaseServices.deleteFolder(path);
                            if (fileDeleted)
                            {
                                totalFileDeleted++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                writeException(ex);
            }
            if (deleteSuccessful)
            {
                string message = BaseServices.createMsgByTemplate(CommonConstants.MSG_I_ACTION_SUCCESSFUL,
                    CommonConstants.ACT_DELETE);
                message += CommonConstants.TEMP_BR_TAG;
                message += BaseServices.createMsgByTemplate(CommonConstants.MSG_I_ACTION_DETAIL,
                                                            CommonConstants.ACT_DELETE,
                                                            totalFileDeleted.ToString(),
                                                            CommonConstants.TXT_ARTICLE_NAME);
                InformaticsTable.Visible = false;
                showErrorMessage(message);
                return;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text;
            int page = 1;
            if (!BaseServices.isNullOrBlank(keyword))
            {
                int totalRecord = infDAO.countArticles(keyword);

                IEnumerable<tblInformatic> lst = infDAO.searchArticles(keyword, ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                if (lst.Count() > 0)
                {
                    InformaticsTable.Visible = true;
                    messagePanel.Visible = false;
                    showInformactics(lst, totalRecord, page, CommonConstants.ACT_SEARCH_FREE, keyword, CommonConstants.ALL);
                    resetDefaultLabel();
                    showCountingArticle();
                    showCountingState();
                }
                else
                {
                    //Session[CommonConstants.SES_INFORM] = CommonConstants.MSG_E_RESOURCE_NOT_FOUND;
                    //Response.Redirect(CommonConstants.PAGE_ADMIN_ENGLISH);
                    showInfoMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);
                    InformaticsTable.Visible = false;
                    return;
                }
            }
            else
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
        /// <summary>
        /// validate input
        /// </summary>
        /// <returns></returns>
        private string validateForm()
        {
            string sError = CommonConstants.BLANK;
            if (BaseServices.isNullOrBlank(txtTitle.Text))
            {
                sError += BaseServices.createMsgByTemplate(CommonConstants.MSG_E_PLEASE_INPUT_DATA, CommonConstants.TXT_TITLE);
                sError += CommonConstants.TEMP_BR_TAG;
            }
            else
            {
                if (txtTitle.Text.Trim().Length > 254)
                {
                    sError += BaseServices.createMsgByTemplate(CommonConstants.MSG_E_MAX_LENGTH,
                        CommonConstants.TXT_TITLE, CommonConstants.BLANK + 254);
                    sError += CommonConstants.TEMP_BR_TAG;
                }
            }
            if (BaseServices.isNullOrBlank(txtLocation.Text))
            {
                sError += BaseServices.createMsgByTemplate(CommonConstants.MSG_E_PLEASE_INPUT_DATA, CommonConstants.TXT_LOCATION);
                sError += CommonConstants.TEMP_BR_TAG;
            }
            if (txtThumbnail.Text.Trim().Length > 254)
            {
                sError += BaseServices.createMsgByTemplate(CommonConstants.MSG_E_MAX_LENGTH,
                        CommonConstants.TXT_THUMBNAIL, CommonConstants.BLANK + 254);
                sError += CommonConstants.TEMP_BR_TAG;
            }
            if (txtTag.Text.Trim().Length > 254)
            {
                sError += BaseServices.createMsgByTemplate(CommonConstants.MSG_E_MAX_LENGTH,
                        CommonConstants.TXT_TAG, CommonConstants.BLANK + 254);
                sError += CommonConstants.TEMP_BR_TAG;
            }
            if (txtLocation.Text.Trim().Length > 200)
            {
                sError += BaseServices.createMsgByTemplate(CommonConstants.MSG_E_MAX_LENGTH,
                        CommonConstants.TXT_LOCATION, CommonConstants.BLANK + 200);
                sError += CommonConstants.TEMP_BR_TAG;
            }
            return sError;
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
        /// <summary>
        /// count by state
        /// </summary>
        private void showCountingState()
        {
            hpkShowAllState.Text += "(" + infDAO.countInf() + ")";
            hpkShowBad.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_BAD) + ")";
            hpkShowChecked.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_CHECKED) + ")";
            hpkShowUncheck.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_UNCHECK) + ")";

        }
        private void initial()
        {
            ddlState.Items.Add(new ListItem(CommonConstants.STATE_UNCHECK_NAME, CommonConstants.STATE_UNCHECK.ToString()));
            ddlState.Items.Add(new ListItem(CommonConstants.STATE_CHECKED_NAME, CommonConstants.STATE_CHECKED.ToString()));
            ddlState.Items.Add(new ListItem(CommonConstants.STATE_BAD_NAME, CommonConstants.STATE_BAD.ToString()));
            //Sticky
            ddlSticky.Items.Add(new ListItem(CommonConstants.TXT_UNSTICKY, CommonConstants.CONST_ZERO));
            ddlSticky.Items.Add(new ListItem(CommonConstants.TXT_STICKY, CommonConstants.CONST_ONE));
            //Type
            ddlType.Items.Add(new ListItem(CommonConstants.TXT_PLEASE_SELECT, CommonConstants.CONST_ONE_NEGATIVE));
            ddlType.Items.Add(new ListItem(CommonConstants.AT_LECTURE_NAME.ToString(), CommonConstants.AT_LECTURE.ToString()));
            ddlType.Items.Add(new ListItem(CommonConstants.AT_PRACTISE_NAME, CommonConstants.AT_PRACTISE.ToString()));
            ddlType.Items.Add(new ListItem(CommonConstants.AT_EXAM_NAME, CommonConstants.AT_EXAM.ToString()));
            //Score
            ddlScore.Items.Add(new ListItem(CommonConstants.TXT_PLEASE_SELECT, CommonConstants.CONST_ZERO));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_ONE, CommonConstants.CONST_ONE));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_TWO, CommonConstants.CONST_TWO));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_THREE, CommonConstants.CONST_THREE));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_FOUR, CommonConstants.CONST_FOUR));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_FIVE, CommonConstants.CONST_FIVE));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_SIX, CommonConstants.CONST_SIX));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_SEVEN, CommonConstants.CONST_SEVEN));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_EIGHT, CommonConstants.CONST_EIGHT));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_NINE, CommonConstants.CONST_NINE));
            ddlScore.Items.Add(new ListItem(CommonConstants.CONST_TEN, CommonConstants.CONST_TEN));
            //Class
            ddlLeitmotif.Items.Add(new ListItem(CommonConstants.AT_UNCLASSIFIED_NAME, CommonConstants.AT_UNCLASSIFIED.ToString()));
            ddlLeitmotif.Items.Add(new ListItem(CommonConstants.AT_IT_OFFICE_WORD_NAME, CommonConstants.AT_IT_OFFICE_WORD.ToString()));
            ddlLeitmotif.Items.Add(new ListItem(CommonConstants.AT_IT_OFFICE_EXCEL_NAME, CommonConstants.AT_IT_OFFICE_EXCEL.ToString()));
            ddlLeitmotif.Items.Add(new ListItem(CommonConstants.AT_IT_OFFICE_POWERPOINT_NAME, CommonConstants.AT_IT_OFFICE_POWERPOINT.ToString()));
            ddlLeitmotif.Items.Add(new ListItem(CommonConstants.AT_IT_OFFICE_ACCESS_NAME, CommonConstants.AT_IT_OFFICE_ACCESS.ToString()));
            ddlLeitmotif.Items.Add(new ListItem(CommonConstants.AT_IT_SIMPLE_TIP_NAME, CommonConstants.AT_IT_SIMPLE_TIP.ToString()));
            ddlLeitmotif.Items.Add(new ListItem(CommonConstants.AT_IT_ADVANCE_TIP_NAME, CommonConstants.AT_IT_ADVANCE_TIP.ToString()));
        }
        /// <summary>
        /// use to show message information on mode SEARCH, DELETE
        /// </summary>
        /// <param name="errorText"></param>
        private void showInfoMessage(string infoText)
        {
            liMessage.Text = infoText;
            messagePanel.Visible = true;
        }
        /// <summary>
        /// use to show message error on mode EDIT, VIEW
        /// </summary>
        /// <param name="errorText"></param>
        private void showErrorMessage(string errorText)
        {
            liErrorMessage.Text = errorText;
            ErrorMessagePanel.Visible = true;
        }
        private void changeStateLink(string key)
        {
            hpkShowAllState.NavigateUrl = BaseServices.createMsgByTemplate(CommonConstants.TEMP_ADMIN_IT_URL,
                                                                                       CommonConstants.ACT_SEARCH,
                                                                                       key,
                                                                                       CommonConstants.ALL,
                                                                                       CommonConstants.CONST_ONE);
            hpkShowChecked.NavigateUrl = BaseServices.createMsgByTemplate(CommonConstants.TEMP_ADMIN_IT_URL,
                                                                           CommonConstants.ACT_SEARCH,
                                                                           key,
                                                                           CommonConstants.STATE_CHECKED.ToString(),
                                                                           CommonConstants.CONST_ONE);
            hpkShowUncheck.NavigateUrl = BaseServices.createMsgByTemplate(CommonConstants.TEMP_ADMIN_IT_URL,
                                                                           CommonConstants.ACT_SEARCH,
                                                                           key,
                                                                           CommonConstants.STATE_UNCHECK.ToString(),
                                                                           CommonConstants.CONST_ONE);
            hpkShowBad.NavigateUrl = BaseServices.createMsgByTemplate(CommonConstants.TEMP_ADMIN_IT_URL,
                                                                           CommonConstants.ACT_SEARCH,
                                                                           key,
                                                                           CommonConstants.STATE_BAD.ToString(),
                                                                           CommonConstants.CONST_ONE);
        }
        private void showCountingArticle()
        {
            try
            {
                hpkShowAccess.Text += "(" + infDAO.countInfByLeitmotif(CommonConstants.AT_IT_OFFICE_ACCESS) + ")";
                hpkShowAdvTip.Text += "(" + infDAO.countInfByLeitmotif(CommonConstants.AT_IT_ADVANCE_TIP) + ")";
                hpkShowAll.Text += "(" + infDAO.countInf() + ")";
                hpkShowSimTip.Text += "(" + infDAO.countInfByLeitmotif(CommonConstants.AT_IT_SIMPLE_TIP) + ")";
                hpkShowExcel.Text += "(" + infDAO.countInfByLeitmotif(CommonConstants.AT_IT_OFFICE_EXCEL) + ")";
                hpkShowPP.Text += "(" + infDAO.countInfByLeitmotif(CommonConstants.AT_IT_OFFICE_POWERPOINT) + ")";
                hpkShowWord.Text += "(" + infDAO.countInfByLeitmotif(CommonConstants.AT_IT_OFFICE_WORD) + ")";
                hpkShowSticky.Text += "(" + infDAO.countStickyInfArticle() + ")";
            }
            catch (Exception e)
            {
                writeException(e);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session[CommonConstants.SES_OLD_PAGE] != null)
            {
                Response.Redirect((string)Session[CommonConstants.SES_OLD_PAGE]);
            }
            else
            {
                Response.Redirect(CommonConstants.PAGE_ADMIN_ENGLISH);
            }
        }
        private void changeViewState(string linkID)
        {
            if (linkID == CommonConstants.ALL)
            {
                hpkShowAll.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowAll.Text);
            }
            else if (linkID == CommonConstants.TXT_STICKY.ToLower())
            {
                hpkShowSticky.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowSticky.Text);
            }
            else
            {
                int lei = -1;
                if (!BaseServices.isNumeric(linkID))
                {
                    lei = CommonConstants.AT_IT_OFFICE_WORD;

                }
                else
                {
                    lei = BaseServices.convertStringToInt(linkID.Trim());
                }
                switch (lei)
                {
                    case CommonConstants.AT_IT_OFFICE_WORD:
                        {
                            hpkShowWord.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowWord.Text);
                            break;
                        }
                    case CommonConstants.AT_IT_OFFICE_POWERPOINT:
                        {
                            hpkShowPP.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowPP.Text);
                            break;
                        }
                    case CommonConstants.AT_IT_OFFICE_EXCEL:
                        {
                            hpkShowExcel.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowExcel.Text);
                            break;
                        }
                    case CommonConstants.AT_IT_OFFICE_ACCESS:
                        {
                            hpkShowAccess.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowAccess.Text);
                            break;
                        }
                    case CommonConstants.AT_IT_ADVANCE_TIP:
                        {
                            hpkShowAdvTip.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowAdvTip.Text);
                            break;
                        }
                    case CommonConstants.AT_IT_SIMPLE_TIP:
                        {
                            hpkShowSimTip.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_B_TAG, hpkShowSimTip.Text);
                            break;
                        }
                }
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
        /// change state of control
        /// </summary>
        /// <param name="state"></param>
        private void changeState(bool state)
        {
            txtTitle.Enabled = state;
            txtChapeau.Enabled = state;
            txtTag.Enabled = state;
            txtChecker.Enabled = state;
            ddlState.Enabled = state;
            ddlSticky.Enabled = state;
            ddlType.Enabled = state;
            ddlLeitmotif.Enabled = state;
            ddlScore.Enabled = state;
            txtHtmlEmbbed.Enabled = state;
            txtHtmlPreviewLink.Enabled = state;
            txtChecker.Enabled = state;
        }
    }
}