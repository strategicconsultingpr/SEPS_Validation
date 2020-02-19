<%@ Page language="c#" Inherits="ASSMCA.frmHome" Codebehind="frmHome.aspx.cs" MasterPageFile="~/Main.Master" %>
<asp:Content ID="mainC" runat="server" ContentPlaceHolderID="mainBodyContent">
    <div style="max-width:600px;margin:auto">
        <h1 class="hidden-print">Sistema Electrónico de Perfiles Sociodemográficos (SEPS - 2G)</h1>
        <p style="text-indent:10px">Los perfiles sociodemográficos de Admisión, Evaluación de Progreso y Alta ayudan a describir, establecer comparaciones e identificar tendencias de las características de las personas admitidas a tratamiento de abuso y dependencia de sustancias y de salud mental.  Se espera que esta información permita a la Agencia describir la población atendida, cómo y cuándo la atiende.  Finalmente, esta información permitirá al público conocer los servicios ofrecidos por la Agencia y facilitará la planificación de actividades que ayuden a cumplir los objetivos y metas trazados racionalmente a base de información recopilada adecuadamente.</p>
        <p style="text-indent:10px">Como parte de su menester, la Agencia debe producir datos válidos a nivel nacional que sean comparables con los datos de otros estados de los Estados Unidos.  Por lo tanto, la recopilación de datos debe hacerse de una manera uniforme y adecuada que permita planificar y evaluar eficientemente la prestación de servicios en las áreas de Salud Mental y Drogas y Alcohol.</p>
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
  '<p>Se realizaron cambios para poder mejorar la entrada de datos. De ocurrir algún inconveniente favor documentar el problema y comunicarse con la oficina de planificación.'+
'<br/><br/> Contactos: <br/><br/> &nbsp&nbsp&nbsp&nbsp <strong>Carlos Morel - ext. 1130 </strong>'+
'<br/> &nbsp&nbsp&nbsp&nbsp <strong>Vimarys González - ext. 1215</strong></p></div>'+
                    '<br/> &nbsp&nbsp&nbsp&nbsp <strong>CARMEN HERNÁNDEZ - ext. 1210</strong>'+
                    '<br/> &nbsp&nbsp&nbsp&nbsp <strong>DAISY GONZÁLEZ - ext. 1214</strong></p></div>',
                    'info'
                )
            }
        });
    </script>
</asp:Content>