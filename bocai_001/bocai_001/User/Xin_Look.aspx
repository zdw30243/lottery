<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xin_Look.aspx.cs" Inherits="bocai_001.User.Xin_Look" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="/head1.html"-->
        <title>查看消息</title>
        <!--#include file="/head2.html"-->
    </head><body>
    <form id="form1" runat="server">
        消息来源:<%=_source %><br />
        <%=_time %><br />
        <%=_result %>
    </form>
     <hr /><span class="ddd-qs"><a href="Default.Aspx?Cid=<%=_Uid %>&amp;v=1">[返回首页]</a></span><br />
        <!--#include file="../bottom.html"-->
          时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
</body>
</html>
