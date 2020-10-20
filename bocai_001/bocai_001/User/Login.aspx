<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="bocai_001.User.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<!--#include file="/head1.html"-->
    <title></title>
    <!--#include file="/head2.html"-->
</head>
<body><div class="main">
    <form id="form1" runat="server" method="post">
        <div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
             <a href="Reg.Aspx?Source=&amp;v=1">没有账号？快速注册</a><br/>
            手机号码:<br />
            <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
            <br />
            登陆密码:<br />
            <asp:TextBox ID="tbPwd" runat="server"></asp:TextBox>
            <br />
             <asp:Button ID="btLogin" runat="server" Text="立刻登陆" CssClass="bottom"  />
              </asp:View>
                
                <asp:View ID="View2" runat="server">
                    <%=_stt %><br />
                    <hr/><a href="Login.Aspx?Source=&amp;v=1">返回重新登陆</a><br/>
                    <span class="ddd-qs"><a href="/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">【返回伟哥首页】</a></span><br/>
                </asp:View>

                <asp:View ID="View3" runat="server">
                    <%=_stt %><br />
                    <span class="ddd-qs"><a href="/Default.Aspx?Cid=<%=_Uid %>&amp;v=1"">【返回伟哥首页】</a></span><br/>
                </asp:View>
               </asp:MultiView>
        </div>
    </form>
    <br /><hr />
    <!--#include file="/bottom.html"-->
         时间:<% DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");%> <p class="link"></p>
        <!--#include file="/bottom1.html"-->
</body>
</html>
