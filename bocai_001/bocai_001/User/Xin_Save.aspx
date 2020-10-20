<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xin_Save.aspx.cs" Inherits="bocai_001.User.Xin_Save" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <!--#include file="/head1.html"-->
        <title>发送消息</title>
        <!--#include file="/head2.html"-->
    </head><body>
    <form id="form1" runat="server">
        <%=_send %>
    </form>
        <hr /><a href="/User/Xin_Add.Aspx?Sid=<%=_idReceive %>&amp;Cid=<%=_Uid %>&amp;v=1">返回来源页</a><br/>
        <span class="ddd-qs"><a href="/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">[返回首页]</a></span><br />
        <!--#include file="../bottom.html"-->
          时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
</body>
</html>
