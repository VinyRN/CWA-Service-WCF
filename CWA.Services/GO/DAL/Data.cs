using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace CWA.Services
{
    public class InterfacesWSGOVA_Data
    {

        //private string lsSQL;
        //public SqlConnection conectar;
        private SqlCommand Comando = new SqlCommand();
        private SqlCommand Comando2 = new SqlCommand();
        public SqlTransaction goTransaction;
        //public SqlDataReader Reader;

        public SqlConnection Conectar()
        {
            string string_conexao = string.Empty;

            string_conexao = "Data Source=" + VarBanco.Servidor + ";Initial Catalog=" + VarBanco.Banco + ";Persist Security Info=True;User ID=" + VarBanco.Usuario + ";Password=VSSMJOSCWA";
            
            try
            {

                if (VarBanco.conectar == null)
                {
                    VarBanco.conectar = new SqlConnection(string_conexao);
                    VarBanco.conectar.Open();    
                }
                

                return VarBanco.conectar;
            }
             catch(Exception ex)  
            {
                string erro = ex.Message; 
                return VarBanco.conectar;
            }
        }

        public SqlConnection Conectar2()
        {
            string string_conexao = string.Empty;

            string_conexao = "Data Source=" + VarBanco.Servidor + ";Initial Catalog=" + VarBanco.Banco + ";Persist Security Info=True;User ID=" + VarBanco.Usuario + ";Password=VSSMJOSCWA";

            
            try
            {
                if (VarBanco.conectar2 == null)
                {
                    VarBanco.conectar2 = new SqlConnection(string_conexao);
                    VarBanco.conectar2.Open();
                }

                return VarBanco.conectar2;
            }
            catch
            {
                return VarBanco.conectar2;
            }
        }

        public Boolean RetornaSQL()
        {
            try
            {

                Comando.Connection = VarBanco.conectar;
                Comando.Transaction = VarBanco.transaction;
                Comando.CommandText = VarBanco.lsSQL;

                if (VarBanco.Reader != null)
                {
                    VarBanco.Reader.Close();
                }

                VarBanco.Reader = Comando.ExecuteReader();

                if (VarBanco.Reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                VarBanco.ErroSQL = ex.Message;
                return false;
            }

        }

        public Boolean RetornaSQL2()
        {
            try
            {

                Comando2.Connection = VarBanco.conectar2;
                Comando2.Transaction = VarBanco.transaction;
                Comando2.CommandText = VarBanco.lsSQL;

                if (VarBanco.Reader2 != null)
                {
                    VarBanco.Reader2.Close();
                }

                VarBanco.Reader2 = Comando2.ExecuteReader();

                if (VarBanco.Reader2.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                VarBanco.ErroSQL = ex.Message;
                return false;
            }

        }

        public Boolean ExecutaSQL()
        {
            try
            {
                Comando.Connection = VarBanco.conectar;
                Comando.Transaction = VarBanco.transaction;
                Comando.CommandText = VarBanco.lsSQL;

                if (VarBanco.Reader != null)
                {
                    VarBanco.Reader.Close();
                }

                Comando.ExecuteNonQuery();
                return true;

            }
            catch
            {
                //VarBanco.conectar.Close();
                return false;
            }
        }

        public Boolean ExecutaSQL2()
        {
            try
            {
                Comando2.Connection = VarBanco.conectar2;
                Comando2.Transaction = VarBanco.transaction;
                Comando2.CommandText = VarBanco.lsSQL;

                if (VarBanco.Reader2 != null)
                {
                    VarBanco.Reader2.Close();
                }

                Comando2.ExecuteNonQuery();
                return true;

            }
            catch
            {
                //VarBanco.conectar.Close();
                return false;
            }
        }

        public Boolean Close_Conection()
        {
            try
            {
                VarBanco.conectar.Close();
                VarBanco.conectar = null;
                return true;
            }
            catch
            {
                VarBanco.conectar.Close();
                VarBanco.conectar = null;
                return false;
            }
        }

        public Boolean Close_Conection2()
        {
            try
            {
                VarBanco.conectar2.Close();
                VarBanco.conectar2 = null;
                return true;
            }
            catch
            {
                VarBanco.conectar2.Close();
                VarBanco.conectar2 = null;
                return false;
            }
        }

    }
}
