using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CWA.Services
{
    public static class VarBanco
    {
        private static string m_Servidor = string.Empty;
        public static string Servidor
        {
            get { return m_Servidor; }
            set { m_Servidor = value; }
        }

        private static string m_Banco = string.Empty;
        public static string Banco
        {
            get { return m_Banco; }
            set { m_Banco = value; }
        }

        private static string m_Usuario = string.Empty;
        public static string Usuario
        {
            get { return m_Usuario; }
            set { m_Usuario = value; }
        }

        private static string m_lsSQL = string.Empty;
        public static string lsSQL
        {
            get { return m_lsSQL; }
            set { m_lsSQL = value; }
        }

        private static SqlConnection m_conectar;
        public static SqlConnection conectar
        {
            get { return m_conectar; }
            set { m_conectar = value; }
        }

        private static SqlConnection m_conectar2;
        public static SqlConnection conectar2
        {
            get { return m_conectar2; }
            set { m_conectar2 = value; }
        }

        private static SqlDataReader m_Reader;
        public static SqlDataReader Reader
        {
            get { return m_Reader; }
            set { m_Reader = value; }
        }

        private static SqlDataReader m_Reader2;
        public static SqlDataReader Reader2
        {
            get { return m_Reader2; }
            set { m_Reader2 = value; }
        }

        private static SqlTransaction m_transaction;
        public static SqlTransaction transaction
        {
            get { return m_transaction; }
            set { m_transaction = value; }
        }


        private static string m_ErroSQL;
        public static string ErroSQL
        {
            get { return m_ErroSQL; }
            set { m_ErroSQL = value; }
        }


    }
}
