﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Registry.aspx.cs" Inherits="ltkt.Registry" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="SearchHeader" ContentPlaceHolderID="cphMasterHearder" runat="Server">
    <%--<title>Đăng ký thành viên | Website luyện thi kinh tế</title>--%>
    <title><asp:Literal ID="liTitle" runat="server"></asp:Literal></title>
</asp:Content>
<asp:Content ID="Register" ContentPlaceHolderID="cphContent" runat="Server">
    <div id="signupForm" class="block_text">
        <h2>
            Đăng ký thành viên</h2>
        <hr />
        <div id="divMessage" class="form_settings">
            <asp:Literal ID="liMessage" runat="server" Text="" Visible="False"></asp:Literal>
        </div>
        <asp:Panel ID="registerPanel" runat="server">
            <div class="form_settings">
                <p>
                    <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="true" HeaderText="Lỗi" />
                </p>
                <p>
                    <span>Tên đăng nhập:</span><asp:TextBox ID="txtboxLoginName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUsername" runat="server" ErrorMessage="Vui lòng nhập tên đăng nhập"
                        ControlToValidate="txtboxLoginName" Display="None">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExpUsername" runat="server" ErrorMessage="Tên đăng nhập từ 6 tới 20 kí tự và không được chứa khoảng trắng"
                        ControlToValidate="txtboxLoginName" Display="None" ValidationExpression="^[a-zA-Z'.\S]{6,20}$">
                    </asp:RegularExpressionValidator>
                </p>
                <p>
                    <span>Tên hiển thị:</span><asp:TextBox ID="txtboxDisplayName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqDisplayName" runat="server" ErrorMessage="Vui lòng nhập tên hiển thị"
                        ControlToValidate="txtboxDisplayName" Display="none">
                    </asp:RequiredFieldValidator>
                </p>
                <p>
                    <span>Giới tính:</span><asp:DropDownList ID="ddlSex" runat="server">
                        <asp:ListItem Text="Nam" Value="false"></asp:ListItem>
                        <asp:ListItem Text="Nữ" Value="true"></asp:ListItem>
                    </asp:DropDownList>
                </p>
                <p>
                    <span>Mật khẩu:</span><asp:TextBox ID="txtboxPassword" runat="server" TextMode="Password"
                        MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ErrorMessage="Vui lòng nhập mật khẩu"
                        ControlToValidate="txtboxPassword" Display="none">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExpPassword" runat="server" ErrorMessage="Mật khẩu phải có từ 6 ký tự trở lên"
                        ControlToValidate="txtboxPassword" Display="None" ValidationExpression="^.{6,30}$">
                    </asp:RegularExpressionValidator>
                </p>
                <p>
                    <span>Nhập lại mật khẩu:</span><asp:TextBox ID="txtboxConfirmPassword" runat="server"
                        TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="compPass" runat="server" ControlToValidate="txtboxConfirmPassword"
                        Operator="Equal" ControlToCompare="txtboxPassword" ErrorMessage="Mật khẩu được nhập lại không giống nhau."
                        Display="None" />
                    <asp:RequiredFieldValidator ID="reqConfirmPassword" runat="server" ErrorMessage="Vui lòng nhập xác nhận mật khẩu"
                        ControlToValidate="txtboxConfirmPassword" Display="none"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <span>Email:</span><asp:TextBox ID="txtboxEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="Vui lòng nhập email liên lạc"
                        ControlToValidate="txtboxEmail" Display="none">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="valEmail" runat="server" ControlToValidate="txtboxEmail"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Email không đúng định dạng"
                        Display="None" />
                </p>
                <div align="center" style="margin-top: 10px; margin-left: 100px">
                    <recaptcha:RecaptchaControl ID="recaptcha" runat="server" PublicKey="6Le4WccSAAAAAPNrmUGzjeAUMyH_iXso4kipQqrQ "
                        PrivateKey="6Le4WccSAAAAAPovqPf4ymPe2E4dI9k7JD3qhnan" />
                </div>
                <p style="padding-top: 15px">
                    <asp:Button ID="btnSubmitSignup" runat="server" Text="Đăng ký"
                        CssClass="submit" OnClick="btnRegistry_Click" /></p>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
