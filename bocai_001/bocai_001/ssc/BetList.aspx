<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BetList.aspx.cs" Inherits="bocai_001.ssc.BetList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<!--#include file="/head1.html"-->
    <title>老伟的网站</title>
    <!--#include file="/head2.html"-->
</head>
<body>
    <div class="szc">昵称:<strong><%=_netNameLink %></strong>❀<a href="/View/View.Aspx?id=25&amp;Cid=<%=_Uid %>&amp;v=1">[充值]</a>
        ❀<a href="/User/Apply.Aspx?Cid=<%=_Uid %>&amp;v=1">[提款]</a><br/>
        ✿ 余额:<strong><%=_money %>金币</strong>❀<a href="/User/Xin_Add.Aspx?id=1&amp;Cid=<%=_Uid %>&amp;v=1">[充值客服]</a>
        ❀<a href="/User/Xin_Add.Aspx?id=2&amp;Cid=<%=_Uid %>&amp;v=1">[提款客服]</a></div>
    
        <%=_result %>
        <a href="/ssc/BetList.Aspx?&amp;Cid=<%=_Uid %>&amp;v=1&amp;page=<%=_pageNext %>">[下页]</a><%=_previousPage %><br />共有<%=_totalPiece %>条记录,当前<%=_currentPage %>/<%=_totalPage %>页<br/> 
    <form id="form2" runat="server" method="get" action="/ssc/BetList.Aspx">
        <asp:TextBox ID="page" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="Cid" type="hidden" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextBox3" type="hidden" runat="server"></asp:TextBox>
         <input type="submit" class="submit" value="跳到此页"/></form><br/>
    <!--#include file="../bottom.html"-->
         时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
    </form>
</body>
</html>
