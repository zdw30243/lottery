<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reg.aspx.cs" Inherits="bocai_001.User.Reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <!--#include file="/head1.html"-->
    <title>老伟的网站</title>
    <!--#include file="/head2.html"-->
   </head>
    <body><div class="main">
        <form id="form1" runat="server" action="Reg.aspx">
            <asp:MultiView ID="MultiView1" runat="server" >
                <asp:View ID="View1" runat="server">
                    网站昵称:<br />
                    <asp:TextBox ID="tbNetname" runat="server"></asp:TextBox>
                    <br />
                    手机号码:(重要,收款会通知)<br/>
                    <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
                    <br />登陆密码:<br/>
                    <asp:TextBox ID="tbPwd" runat="server"></asp:TextBox>
                    <br />确认密码:<br/>
                    <asp:TextBox ID="tbPwd1" runat="server"></asp:TextBox>
                    <br />
                    <asp:Button ID="regist" runat="server" Text="立即注册" CssClass="bottom" />
                    <br/><hr/><a href="Login.Aspx?Source=&amp;v=1">返回会员登陆</a><br/>
                    <span class="ddd-qs"><a href="/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">【返回伟哥首页】</a></span><br/>

                  </asp:View>
        <asp:View ID="View2" runat="server">
            <%=_remind %>
            <br /><hr/><a href="Reg.Aspx?v=1&Ac=Reg">返回重新注册</a><br/>
            <span class="ddd-qs"><a href="/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">【返回伟哥首页】</a></span><br/>
        </asp:View>
                <asp:View ID="View3" runat="server">
            <%=_remind %>
            <br/><hr/><a href="Login.Aspx?Source=&amp;v=1">返回会员登陆</a><br/>
             <span class="ddd-qs"><a href="/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">【返回伟哥首页】</a></span><br/>
                </asp:View>
      </asp:MultiView> 
        </form>
        <!--#include file="../bottom.html"-->
         时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
        </body></html>
