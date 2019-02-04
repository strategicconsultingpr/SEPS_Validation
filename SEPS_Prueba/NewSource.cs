using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public class NewSource
{
    static internal string connectionString = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(System.Configuration.ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString)).Replace("?","");
    internal const int nr_dias_edicion_registros = 7;
    public DataTable getAll(string directory)
    {
        DataTable Dt = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand CMD = new SqlCommand(directory, conn))
                {
                    Dt = new DataTable();
                    CMD.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter Da = new SqlDataAdapter(CMD);
                    Da.Fill(Dt);
                    Da.Dispose();
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Dt = null;
            throw ex;
        }
        return Dt;
    }
    public void deleteFiltered(string directory, int filterValue, string filterName)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand CMD = new SqlCommand("SP_DEL_" + directory, conn))
                {
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.Add(filterName, SqlDbType.Int);
                    CMD.Parameters[filterName].Value = filterValue;
                    CMD.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void insertItem(string directory, string paramName1, int paramValue1, string paramName2, int paramValue2)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand CMD = new SqlCommand("SPC_" + directory, conn))
                {
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.Add(paramName1, SqlDbType.Int);
                    CMD.Parameters.Add(paramName2, SqlDbType.Int);
                    CMD.Parameters[paramName1].Value = paramValue1;
                    CMD.Parameters[paramName2].Value = paramValue2;
                    CMD.ExecuteNonQuery();
                }
                conn.Close();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable getRef(string directory, int UPK)
    {
        DataTable Dt = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand CMD = new SqlCommand(directory,conn))
                {
                    Dt = new DataTable();
                    CMD.CommandType = CommandType.StoredProcedure;
                    if (directory.Equals("SPR_Ref_Maltrato") || directory.Equals("SPR_Ref_ProbJusticia"))
                    {
                        CMD.Parameters.AddWithValue("@FK_Episodio", UPK).Direction = ParameterDirection.Input;
                    }
                    else
                    {
                        CMD.Parameters.AddWithValue("@FK_Perfil", UPK).Direction = ParameterDirection.Input;
                    }
                    SqlDataAdapter Da = new SqlDataAdapter(CMD);
                    Da.Fill(Dt);
                    Da.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Dt = null;
            throw ex;
        }
        return Dt;
    }
    public DataTable getNivel(string directory, int PK)
    {
        DataTable Dt = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand CMD = new SqlCommand(directory, conn))
                {
                    Dt = new DataTable();
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@PK_Programa", PK).Direction = ParameterDirection.Input;
                    SqlDataAdapter Da = new SqlDataAdapter(CMD);
                    Da.Fill(Dt);
                    Da.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Dt = null;
            throw ex;
        }
        return Dt;
    }
    public DataTable getNivelUnavailable(string sp, int FK_Persona, int PK_Episodio)
    {
        DataTable Dt = null;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand CMD = new SqlCommand(sp, conn))
                {
                    Dt = new DataTable();
                    CMD.CommandType = CommandType.StoredProcedure;
                    CMD.Parameters.AddWithValue("@PK_Episodio", PK_Episodio).Direction = ParameterDirection.Input;
                    CMD.Parameters.AddWithValue("@FK_Persona",FK_Persona).Direction = ParameterDirection.Input;
                    SqlDataAdapter Da = new SqlDataAdapter(CMD);
                    Da.Fill(Dt);
                    Da.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Dt = null;
            throw ex;
        }
        return Dt;
    }
}