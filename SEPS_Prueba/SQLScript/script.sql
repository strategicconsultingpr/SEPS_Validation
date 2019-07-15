
/*
      Campo Episodios previos al tratamiento (Salud Mental).
            Agregar opción ‘No Aplica’
*/
UPDATE [dbo].[SA_LKP_TEDS_EPISODIO_PREVIO]
set [Active]=1
where [PK_EpisodiosPrevios]=99

go

/*
      Campo Frecuencia de uso.
            Actualizar las opciones en el campo de "Frecuencia de Uso" para poder igualar las opciones en el formulario de papel.
*/


update SA_LKP_TEDS_FRECUENCIA
set Active =0, Ordered=null;


update SA_LKP_TEDS_FRECUENCIA
set Active =1
where PK_Frecuencia in (1, 2,3,4,5, 95,96,99)


update SA_LKP_TEDS_FRECUENCIA
set Ordered=1 where PK_Frecuencia =1
 
update SA_LKP_TEDS_FRECUENCIA
set Ordered=2 where PK_Frecuencia =2

update SA_LKP_TEDS_FRECUENCIA
set Ordered=3 where PK_Frecuencia =3

update SA_LKP_TEDS_FRECUENCIA
set Ordered=4 where PK_Frecuencia =4

update SA_LKP_TEDS_FRECUENCIA
set Ordered=5 where PK_Frecuencia =5

update SA_LKP_TEDS_FRECUENCIA
set Ordered=6 where PK_Frecuencia =95

update SA_LKP_TEDS_FRECUENCIA
set Ordered=7 where PK_Frecuencia =96

update SA_LKP_TEDS_FRECUENCIA
set Ordered=8 where PK_Frecuencia =99


GO

/*
*************************************************************************************************************************
importante:
despues de ejecutar el script se debe reiniciar la instancia del iis que corresponda
*************************************************************************************************************************
*/