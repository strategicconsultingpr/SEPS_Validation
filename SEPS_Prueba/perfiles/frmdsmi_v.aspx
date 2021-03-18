 <%@ Page Language="c#" Inherits="ASSMCA.Perfiles.frmDSMI_V" CodeBehind="frmdsmi_v.aspx.cs"  %>

<!DOCTYPE html>
<html lang="en">
 <head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Diagnósticos</title>
    <meta content="C#" name="CODE_LANGUAGE">
    <link href="../vendor/bootstrap-3.3.2-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../vendor/bootstrap-3.3.2-dist/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/global.min.css" rel="stylesheet">
    <script type='text/javascript' src="<%= Page.ResolveClientUrl("~/vendor/jquery-1.11.2.min.js") %>"></script>
    <script type='text/javascript' src="<%= Page.ResolveClientUrl("~/scripts/UIFlujo.js")%>"></script>
    <script type='text/javascript' src="<%= Page.ResolveClientUrl("~/scripts/UIVal.js") %>"></script>
</head>
<body style="overflow:hidden;" >
    <form id="Form1" method="post" runat="server" defaultbutton="btnSeleccionar" defaultfocus="txtFilter">
            <input id="txtDescripcion" type="hidden" name="Hidden2" runat="server"/>
            <input id="txtDescripcionHidden" type="hidden" name="Hidden1" runat="server"/>
            <input id="tipoDescripcion" type="hidden" name="Hidden3" runat="server"/>

         <%-- Anadi esta variable para recoger el tipo de filtro --%>
        <input id="txtFiltroTipo" type="hidden" name="Hidden2" runat="server"/>

            <input id="ClinHD" type="hidden" name="Hidden2" runat="server"/>
            <input id="ClinHD1" type="hidden" name="Hidden2" runat="server"/>
            <input id="ClinTxt1" type="hidden" name="Hidden2" runat="server"/>
            <input id="ClinHD2" type="hidden" name="Hidden2" runat="server"/>
            <input id="ClinTxt2" type="hidden" name="Hidden2" runat="server"/>
            
            <input id="RMHD" type="hidden" name="Hidden2" runat="server"/>
            <input id="RMHD1" type="hidden" name="Hidden2" runat="server"/>
            <input id="RMTxt1" type="hidden" name="Hidden2" runat="server"/>
            <input id="RMHD2" type="hidden" name="Hidden2" runat="server"/>
            <input id="RMTxt2" type="hidden" name="Hidden2" runat="server"/>
            <div class="row">
                <div class="col-xs-12" style="padding:10px 25px 5px 50px;">
                    <div class="input-group">
                        <asp:TextBox ID="txtFilter" name="txtFilter" CssClass="form-control" placeholder="Filtrar" aria-describedby="txtFilterIcon" autocomplete="off" runat="server"/>
                        <span class="input-group-addon">
                            <span class="glyphicon glyphicon-search" id="txtFilterIcon"/>
                        </span>
                    </div>
                </div>
                <div class="col-xs-12" style="padding:5px 25px 5px 50px">
                       <%-- Quite el data memeber y el source y lo inicialice en el code behind --%>
                     <asp:ListBox ID="lbxDSMV"  runat="server" CssClass="form-control" Height="175px" DataValueField="_PK_DSMV"/>
                </div>
                <div class="col-xs-12" style="text-align:center; padding:5px 25px 5px 50px">
                    <asp:Button id="btnSeleccionar" runat="server" CssClass="btn btn-default" Text="Seleccionar diagnóstico" OnClientClick="DSMV();"/>
                </div>
            </div>
        
            <script type="text/javascript">
                function DoListBoxFilter(listBoxSelector, filter, keys, values)
                {
                    var list = $(listBoxSelector);
                    var selectBase = '<option value="{0}">{1}</option>';
                    list.empty();
                    for (i = 0; i < values.length; ++i)
                    {
                        var value = values[i];
                        if (value == "" || value.toLowerCase().indexOf(filter.toLowerCase()) >= 0)
                        {
                            var temp = '<option value="' + keys[i] + '">' + value + '</option>';
                            list.append(temp);
                        }
                    }
                }
                var keys = [];
                var values = [];
                var options = $('#<% = lbxDSMV.ClientID %> option');
                $.each(options, function (index, item)
                {
                    keys.push(item.value);
                    values.push(item.innerHTML);
                });
                $('#<% = txtFilter.ClientID %>').keyup(function ()
                {
                    var filter = $(this).val();
                    DoListBoxFilter('#<% = lbxDSMV.ClientID %>', filter, keys, values);
                });
                $(window).resize(function () {
                    $('#<% = lbxDSMV.ClientID %>').css("height",175 + ($(window).height()-280));
                });
            </script>
    </form>
    <script type='text/javascript' src= "<%= Page.ResolveClientUrl("~/vendor/bootstrap-3.3.2-dist/js/bootstrap.min.js") %>"></script>
    <!--[if IE 10]>
        <script type='text/javascript' src= "<%= Page.ResolveClientUrl("~/vendor/ie10-viewport-bug-workaround.js") %>"></script>
    <![endif]-->ipt>
</body>
</hl>
