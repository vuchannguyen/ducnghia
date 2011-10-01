﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ltktDAO;

namespace ltkt.Admin
{
    public partial class Informatics : System.Web.UI.Page
    {
        private ltktDAO.Users userDAO = new ltktDAO.Users();
        ltktDAO.Control control = new ltktDAO.Control();
        ltktDAO.BaseServices bs = new ltktDAO.BaseServices();
        ltktDAO.Informatics infDAO = new ltktDAO.Informatics();
        ltktDAO.EventLog log = new ltktDAO.EventLog();

        private const int NoOfInformacticsPerPage = 7;

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

                    liTableHeader.Text = CommonConstants.PAGE_ADMIN_INFORMATICS_NAME;

                    hpkShowAll.Text += "(" + infDAO.countInf() + ")";
                    hpkShowUncheck.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_UNCHECK) + ")";
                    hpkShowChecked.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_CHECKED) + ")";
                    hpkShowBad.Text += "(" + infDAO.countInfListByState(CommonConstants.STATE_BAD) + ")";

                    pageLoad(sender, e, user);
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
            try
            {
                int page = 1;
                string action = Request.QueryString[CommonConstants.REQ_ACTION];
                string sPage = Request.QueryString[CommonConstants.REQ_PAGE];
                if (BaseServices.isNullOrBlank(action))
                {
                    action = CommonConstants.ACT_SEARCH;
                }
                if (BaseServices.isNullOrBlank(sPage))
                {
                    sPage = CommonConstants.PAGE_NUMBER_FIRST;
                }

                page = Convert.ToInt32(sPage);


                //action is Search
                if (action == CommonConstants.ACT_SEARCH)
                {
                    viewPanel.Visible = true;
                    detailPanel.Visible = false;
                    messagePanel.Visible = false;
                    IEnumerable<tblInformatic> lst = null;
                    //page = Convert.ToInt32(Request.QueryString[CommonConstants.REQ_PAGE]);
                    string key = Request.QueryString[CommonConstants.REQ_KEY];
                    if (BaseServices.isNullOrBlank(key))
                    {
                        key = CommonConstants.ALL;
                    }
                    else
                    {
                        key = BaseServices.nullToBlank(key);
                    }
                    if (key == CommonConstants.ALL)
                    {
                        lst = infDAO.fetchInfList(((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                    }
                    else if (key == CommonConstants.STATE_UNCHECK.ToString()
                        || key == CommonConstants.STATE_CHECKED.ToString()
                        || key == CommonConstants.STATE_BAD.ToString())
                    {
                        lst = infDAO.fetchInfList(Convert.ToInt32(key), ((page - 1) * NoOfInformacticsPerPage), NoOfInformacticsPerPage);
                    }

                    // show data
                    bool isOK = false;
                    if (lst != null)
                    {
                        if (lst.Count() > 0)
                        {
                            showInformactics(lst, page, action, key);
                            isOK = true;
                        }

                    }
                    if (!isOK)
                    {
                        showErrorMessage(CommonConstants.MSG_E_RESOURCE_NOT_FOUND);
                        InformaticsTable.Visible = false;
                        return;
                    }
                }
                else if (action == CommonConstants.ACT_VIEW || action == CommonConstants.ACT_EDIT)
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
                    int _id = Convert.ToInt32(Request.QueryString[CommonConstants.REQ_ID]);
                    viewPanel.Visible = false;
                    detailPanel.Visible = true;
                    messagePanel.Visible = false;

                    if (action == CommonConstants.ACT_VIEW)
                    {
                        btnEdit.Visible = false;
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
                    int _id = Convert.ToInt32(Request.QueryString[CommonConstants.REQ_ID]);

                    bool isMatch = infDAO.isState(_id, CommonConstants.STATE_UNCHECK);
                    if (infDAO.deleteInf(_id, user.Username))
                    {
                        if (isMatch)
                        {
                            ltktDAO.Statistics statDAO = new ltktDAO.Statistics();
                            statDAO.add(CommonConstants.SF_NUM_ARTICLE_ON_IT, CommonConstants.CONST_ONE_NEGATIVE);
                        }
                        string mess = BaseServices.createMsgByTemplate(CommonConstants.MSG_I_ACTION_SUCCESSFUL, CommonConstants.ACT_DELETE);
                        
                        ltktDAO.Alert.Show(mess);
                        isDeleted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLog(DBHelper.strPathLogFile, user.Username, CommonConstants.MSG_LINK_ERROR);
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
                                              + "1");
            }
            if (isDeleted)
            {
                Response.Redirect(CommonConstants.PAGE_ADMIN_INFORMATICS);
            } 
        }

        private void showInformactics(IEnumerable<tblInformatic> lst, int page, string action, string key)
        {
            int totalInformactic = 0;
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
                noCell.Text = Convert.ToString(idx + 1);

                TableCell titleCell = new TableCell();
                titleCell.CssClass = "table-cell";
                titleCell.Style["width"] = "200px";
                titleCell.Text = BaseServices.createMsgByTemplate(CommonConstants.TEMP_DISPLAY_LINK,
                                                                  CommonConstants.PAGE_ADMIN_INFORMATICS,
                                                                  CommonConstants.ACT_VIEW,
                                                                  Convert.ToString(inf.ID),
                                                                  inf.Title);

                TableCell postedCell = new TableCell();
                postedCell.CssClass = "table-cell";
                postedCell.Style["width"] = "80px";
                postedCell.Text = bs.convertDateToString(inf.Posted);

                TableCell authorCell = new TableCell();
                authorCell.CssClass = "table-cell";
                authorCell.Style["width"] = "60px";
                authorCell.Text = infDAO.getAuthor(inf.ID);

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
                infRow.Cells.Add(authorCell);
                infRow.Cells.Add(stateCell);
                infRow.Cells.Add(actionCell);

                InformaticsTable.Rows.AddAt(2 + idx, infRow);
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
                                + key
                                + CommonConstants.AND
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

        private void showErrorMessage(string errorText)
        {
            liMessage.Text = errorText;
            messagePanel.Visible = true;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        { }
    }
}