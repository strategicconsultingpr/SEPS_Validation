<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="SEPS.dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBodyContent" runat="server">
    <h1>Dashboard</h1>
    <br />
    <div>
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#tab1" aria-controls="tab1" role="tab" data-toggle="tab">Tab1</a></li>
            <li role="presentation"><a href="#tab3" aria-controls="tab3" role="tab" data-toggle="tab">Tab3</a></li>
        </ul>
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="tab1">
                <br />
                <iframe width="100%" height="1060" src="https://app.powerbi.com/view?r=eyJrIjoiNWQ3M2FkODYtOTA5Mi00NjVmLTg1OTctNmE4NjMyMDhhNDE2IiwidCI6IjBkZmE1ZGMwLTAzNmYtNDYxNS05OWU0LTk0YWY4MjJmMmI4NCIsImMiOjF9&embedImagePlaceholder=true" frameborder="0" allowFullScreen="true"></iframe>                <br />
            </div>
            <div role="tabpanel" class="tab-pane" id="tab2">
                <br />
                <iframe  src="https://app.powerbi.com/view?r=eyJrIjoiYjBkYzIyZjAtMmUyNi00NzZlLTllZTAtM2RiOGU1MmI2NWI1IiwidCI6IjBkZmE1ZGMwLTAzNmYtNDYxNS05OWU0LTk0YWY4MjJmMmI4NCIsImMiOjF9" frameborder="0" allowFullScreen="true"></iframe>
            </div>
        </div>

    </div>
</asp:Content>
