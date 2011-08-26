﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AdContact.aspx.cs" Inherits="AdContact" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="AdContactHeader" ContentPlaceHolderID="cphMasterHearder" runat="Server">
    <title>Liên hệ quảng cáo | Website luyện thi kinh tế</title>

    <script type="text/javascript" src="js/jquery.dynDateTime.js"></script>

    <script type="text/javascript" src="js/lang/calendar-en.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="css/calendar-blue.css" />

    <script type="text/javascript">


        $(document).ready(function() {
            $(".calendar").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });


        }); 
    </script>

</asp:Content>
<asp:Content ID="AdContact" ContentPlaceHolderID="cphContent" runat="Server">
    <%-- <form action="AdContact.aspx" method="post">--%>
    <div id="Advertisement" class="block_text">
        <h2>
            Liên hệ quảng cáo</h2>
        <hr />
        <asp:Panel ID="panel2" Visible="false" runat="server">
            <div id="divMessage" class="form_settings">
                <asp:Literal ID="liMessage" runat="server" Text=""></asp:Literal>
            </div>
        </asp:Panel>
        <asp:Panel ID="contactPanel" runat="server">
            <div class="form_settings">
                <p>
                    <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="true" HeaderText="Lỗi" />
                </p>
                <p>
                    <span>Tên Công ty(*):</span><asp:TextBox ID="txtboxCompanyName" runat="server"></asp:TextBox>
                </p>
                <asp:RequiredFieldValidator ID="reqComName" runat="server" ErrorMessage="Vui lòng nhập tên công ty"
                    ControlToValidate="txtboxCompanyName" Display="None">
                </asp:RequiredFieldValidator>
                <%-- Address--%>
                <p>
                    <span>Địa chỉ(*):</span><asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"
                        Rows="2"></asp:TextBox>
                </p>
                <asp:RequiredFieldValidator ID="reqAddress" runat="server" ErrorMessage="Vui lòng nhập địa chỉ công ty"
                    ControlToValidate="txtAddress" Display="None">
                </asp:RequiredFieldValidator>
                <%--Email--%>
                <p>
                    <span>Email(*):</span><asp:TextBox ID="txtboxContactEmail" runat="server"></asp:TextBox>
                </p>
                <asp:RequiredFieldValidator ID="reqContactEmail" runat="server" ErrorMessage="Vui lòng nhập email liên lạc"
                    ControlToValidate="txtboxContactEmail" Display="none">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="valEmail" runat="server" ControlToValidate="txtboxContactEmail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Email không đúng định dạng"
                    Display="None" />
                <%--Phone--%>
                <p>
                    <span>Phone(*):</span><asp:TextBox ID="txtboxFone" runat="server"></asp:TextBox>
                </p>
                <asp:RequiredFieldValidator ID="reqContactTitle" runat="server" ErrorMessage="Vui lòng nhập số điện thoại"
                    ControlToValidate="txtboxFone" Display="None">
                </asp:RequiredFieldValidator>
                <%--From date - To Date--%>
                <p>
                    <span>Từ ngày(*):</span><asp:TextBox ID="txtFromDate" runat="server" CssClass="calendar"></asp:TextBox>
                </p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập ngày bắt đầu"
                    ControlToValidate="txtFromDate" Display="None">
                </asp:RequiredFieldValidator>
                <p>
                    <span>Đến ngày(*):</span><asp:TextBox ID="txtToDate" runat="server" CssClass="calendar"></asp:TextBox>
                </p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập ngày kêt thúc"
                    ControlToValidate="txtToDate" Display="None">
                </asp:RequiredFieldValidator>
                <p>
                    <span>Vị trí:</span> <span id="location" class="checkboxlist">
                        <input runat="server" type="checkbox" id="chkTop" value="0" class="checkbox" checked="checked" />
                        Top Banner<br />
                        <input runat="server" type="checkbox" id="chkRight" value="1" class="checkbox" />
                        Right Banner<br />
                        <input runat="server" type="checkbox" id="chkLeft" value="2" class="checkbox" />
                        Left Banner<br />
                        <input runat="server" type="checkbox" id="chkBot" value="3" class="checkbox" />
                        Bottom Banner<br />
                    </span>
                </p>
                <div align="center" style="margin-top: 10px; margin-left: 100px">
                    <recaptcha:RecaptchaControl ID="recaptcha" runat="server" PublicKey="6Le4WccSAAAAAPNrmUGzjeAUMyH_iXso4kipQqrQ "
                        PrivateKey="6Le4WccSAAAAAPovqPf4ymPe2E4dI9k7JD3qhnan" />
                </div>
                <p style="padding-top: 15px">
                    <span>&nbsp;</span><asp:Button ID="btnSubmitContact" runat="server" Text="Gửi" CssClass="submit"
                        OnClick="btnSubmitContact_Click" /></p>
            </div>
        </asp:Panel>
    </div>
    <%--    </form>--%>
</asp:Content>
