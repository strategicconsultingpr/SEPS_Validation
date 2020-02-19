<%@ Page language="c#" Inherits="ASSMCA.frmHome" Codebehind="frmHome.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <div style="max-width:600px;margin:auto">
        <h1 class="hidden-print">Sistema Electr�nico de Perfiles Sociodemogr�ficos (SEPS - 2G)</h1>
        <p style="text-indent:10px">Los perfiles sociodemogr�ficos de Admisi�n, Evaluaci�n de Progreso y Alta ayudan a describir, establecer comparaciones e identificar tendencias de las caracter�sticas de las personas admitidas a tratamiento de abuso y dependencia de sustancias y de salud mental.  Se espera que esta informaci�n permita a la Agencia describir la poblaci�n atendida, c�mo y cu�ndo la atiende.  Finalmente, esta informaci�n permitir� al p�blico conocer los servicios ofrecidos por la Agencia y facilitar� la planificaci�n de actividades que ayuden a cumplir los objetivos y metas trazados racionalmente a base de informaci�n recopilada adecuadamente.</p>
        <p style="text-indent:10px">Como parte de su menester, la Agencia debe producir datos v�lidos a nivel nacional que sean comparables con los datos de otros estados de los Estados Unidos.  Por lo tanto, la recopilaci�n de datos debe hacerse de una manera uniforme y adecuada que permita planificar y evaluar eficientemente la prestaci�n de servicios en las �reas de Salud Mental y Drogas y Alcohol.</p>
        <br />
        <img class="center-block img-responsive" src="images/graphic_home.png" />
    </div> 


<script src="https://cdn.jsdelivr.net/npm/sweetalert2@9"></script>

    
    <script type="text/javascript">

        $(document).ready(function () {
           
            var now = new Date();
            var limit = new Date(2020, 02, 25);
            if (now <= limit) {
                Swal.fire(
                    'Bienvenido a la nueva version SEPS 2G!',
                    '<div>'+
  '<p>Se realizaron cambios para poder mejorar la entrada de datos. De ocurrir alg�n inconveniente favor documentar el problema y comunicarse con la oficina de planificaci�n.'+
'<br/><br/> Contactos: <br/><br/> &nbsp&nbsp&nbsp&nbsp <strong>Carlos Morel - ext. 1130 </strong>'+
'<br/> &nbsp&nbsp&nbsp&nbsp <strong>Vimarys Gonz�lez - ext. 1215</strong></p></div>'+
                    '<br/> &nbsp&nbsp&nbsp&nbsp <strong>CARMEN HERN�NDEZ - ext. 1210</strong>'+
                    '<br/> &nbsp&nbsp&nbsp&nbsp <strong>DAISY GONZ�LEZ - ext. 1214</strong></p></div>',
                    'info'
                )
            }
        });
    </script>
</asp:Content>