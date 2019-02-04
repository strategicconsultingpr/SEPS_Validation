<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Documentos.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainBodyContent" runat="server">
    <h1>Adiestramiento de Perfiles:</h1>
        <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">:</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12"><a href="pdf/Perfiles_Adiestramiento.pdf"><span>Adiestramiento de Perfiles</span><img src="<%=ResolveClientUrl("~/images/file_pdf.png")%>" alt="Download" /></a></div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Manual del usuario:</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12"><a href="pdf/Manual.pdf"><span>Manual del usuario</span><img src="<%=ResolveClientUrl("~/images/file_pdf.png")%>" alt="Download" /></a></div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Perfiles:</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-4"><a href="pdf/Perfil_Admisión.pdf"><span>Admisión</span><img src="<%=ResolveClientUrl("~/images/file_pdf.png")%>" alt="Download" /></a></div>
                <div class="col-sm-4"><a href="pdf/Perfil_Evaluación.pdf"><span>Evaluación</span><img src="<%=ResolveClientUrl("~/images/file_pdf.png")%>" alt="Download" /></a></div>
                <div class="col-sm-4"><a href="pdf/Perfil_Alta.pdf"><span>Alta</span><img src="<%=ResolveClientUrl("~/images/file_pdf.png")%>" alt="Download" /></a></div>
            </div>
        </div>
    </div>
</asp:Content>
