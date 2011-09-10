﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ContestUniversity.aspx.cs" Inherits="ltkt.ContestUniversity" %>

<asp:Content ID="ContestHeader" ContentPlaceHolderID="cphMasterHearder" runat="Server">
    <title>
        <asp:Literal ID="liTitle" runat="server"></asp:Literal></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            background-color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Contest" ContentPlaceHolderID="cphContent" runat="Server">
    <div id="content" class="block_text">
        <h2>
            <asp:Label ID="lblTitle" runat="server" Text="Luyện Thi Đại Học"></asp:Label></h2>
        <hr />
        <br />
        <div>
            <asp:ListView ID="productList" runat="server">
                <LayoutTemplate>
                    <ul class="articleList">
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li>
                        <img src="<%#Eval("Thumbnail")%>" alt="" width="120px" height="120px" />
                        <br />
                        <br />
                        <a href="ArticleDetails.aspx?sec=uni&id=<%#Eval("ID")%>">
                            <center>
                                <%#Eval("Title")%></center>
                        </a>
                        <div class="block_details_text">
                            <center>
                                <%#Eval("Year")%></center>
                        </div>
                    </li>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <div>
                        Hiện tại chưa có tài liệu nào
                    </div>
                </EmptyDataTemplate>
            </asp:ListView>
        </div>
        <div class="datapager">
            <br />
            <br />
            <hr />
            <asp:DataPager ID="pagerCons" OnPreRender="DataPagerArticles_PreRender" PagedControlID="productList"
                runat="server">
                <Fields>
                    <asp:NextPreviousPagerField ShowFirstPageButton="True" ShowNextPageButton="False"
                        PreviousPageText="<" FirstPageText="<<" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowLastPageButton="True" ShowPreviousPageButton="False"
                        LastPageText=">>" NextPageText=">" />
                </Fields>
            </asp:DataPager>
        </div>
        <hr />
        <br />
        <h4>
            Các đề liên quan</h4>
        <asp:Label ID="lblOlderLinks" runat="server"></asp:Label>
    </div>
</asp:Content>
