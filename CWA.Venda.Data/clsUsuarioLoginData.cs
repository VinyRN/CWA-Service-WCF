using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;


namespace CWA.Venda.Data
{
    public class UsuarioLoginData
    {

        private System.Int32 _erro;
        private string _msgErro;

        public System.Int32 Erro
        {
            get { return _erro; }
        }

        public string MsgErro
        {
            get { return _msgErro; }
        }


        public UsuarioLoginEntity GetLoginUsuario(string pstrUsuario, string pstrSenha)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_LOGIN_SENHA_OPERADOR";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_USUARIO";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrUsuario;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "SP_SENHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrSenha;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        UsuarioLoginEntity lEnt = new UsuarioLoginEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.CD_USUARIO = int.Parse(myReader[0].ToString());
                            lEnt.DS_LOGIN = myReader[1].ToString();
                            lEnt.DS_SENHA = myReader[2].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetLoginUsuario (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public List<UsuarioLoginEntity> GetLoginUsuarioCentral(string pstrlogin, string pstrSenha)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ACESSOCENTRALAUTENTICAR_INTEGREADO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrlogin;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_SENHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrSenha;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        if (myReader.HasRows)
                        {
                            UsuarioLoginEntity lEnt = new UsuarioLoginEntity();
                            List<UsuarioLoginEntity> ObjList = new List<UsuarioLoginEntity>();  

                            while (myReader.Read())
                            {
                                lEnt = new UsuarioLoginEntity();

                                lEnt.CD_USUARIO = int.Parse(myReader[0].ToString());
                                lEnt.DS_LOGIN = myReader[1].ToString();
                                lEnt.DS_SENHA = myReader[2].ToString();

                                lEnt.NU_SERIE_CTR = myReader[3].ToString();
                                lEnt.NU_CTR = myReader[4].ToString();
                                lEnt.NU_DV_CTR = myReader[5].ToString();

                                lEnt.ST_ESTADO_ATUAL = int.Parse(myReader[6].ToString());

                                if (!myReader.IsDBNull(7))
                                {
                                    lEnt.CD_MOTIVO = int.Parse(myReader[7].ToString());
                                }
                                

                                ObjList.Add(lEnt);
                            }

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return ObjList;

                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetLoginUsuarioCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }

        public Central.Entity.UsuarioCentralEntity GetAcessoProcedure(string pstrDS_EMAIL)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ACESSOCENTRAL";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrDS_EMAIL;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.UsuarioCentralEntity lEnt = new Central.Entity.UsuarioCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_EMAIL = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.CD_CONTABIL_PESSOA = myReader.IsDBNull(1) ? 0 : int.Parse(myReader[1].ToString());
                            lEnt.NU_CPF = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());
                            lEnt.NU_CNPJ = myReader.IsDBNull(3) ? 0 : int.Parse(myReader[3].ToString());
                            lEnt.NM_PESSOA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetAcessoProcedure (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public Central.Entity.UsuarioCentralEntity GetAcessoSenhaProcedure(string pstrDS_EMAIL, string pstrDS_SENHA)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ACESSOCENTRALAUTENTICAR";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrDS_EMAIL;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_SENHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrDS_SENHA;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.UsuarioCentralEntity lEnt = new Central.Entity.UsuarioCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_EMAIL = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.CD_CONTABIL_PESSOA = myReader.IsDBNull(1) ? 0 : int.Parse(myReader[1].ToString());
                            lEnt.NU_CPF = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());
                            lEnt.NU_CNPJ = myReader.IsDBNull(3) ? 0 : int.Parse(myReader[3].ToString());
                            lEnt.NM_PESSOA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.DS_SENHA = myReader.IsDBNull(5) ? "" : myReader[5].ToString();
                           
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetAcessoSenhaProcedure (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public Central.Entity.UsuarioCentralEntity GetEsqueciSenhaProcedure(string pstrDS_EMAIL, string pstrNU_CPF, string pstrNU_CNPJ)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARLEMBRETESENHA";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrDS_EMAIL;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CPF";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrNU_CPF;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CNPJ";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrNU_CNPJ;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.UsuarioCentralEntity lEnt = new Central.Entity.UsuarioCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_EMAIL = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.DS_SENHA = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.CD_CONTABIL_PESSOA = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());
                            lEnt.NU_CPF = myReader.IsDBNull(3) ? 0 : int.Parse(myReader[3].ToString());
                            lEnt.NU_CNPJ = myReader.IsDBNull(4) ? 0 : int.Parse(myReader[4].ToString());

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetEsqueciSenhaProcedure (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public void SetAlterarSenhaProcedure(string pstrDS_EMAIL, 
                                             string pstrDS_SENHA, 
                                             string pstrNU_CPF, 
                                             string pstrNU_CNPJ,
                                             Boolean pbolAbrirTransacao,
                                             Boolean pbolFecharConexao)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {

                    DbCommand myComando = DataContext.CriarComando(pbolAbrirTransacao);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ALTERARSENHA";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (pstrDS_EMAIL != null ? pstrDS_EMAIL : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_SENHA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = (pstrDS_SENHA != null ? pstrDS_SENHA : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CPF";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (pstrNU_CPF != null ? pstrNU_CPF : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CNPJ";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = (pstrNU_CNPJ != null ? pstrNU_CNPJ : (object)DBNull.Value);

                    myComando.Parameters.Add(myParametro);
                  

                    DataContext.ExecutarComando(myComando);

                    if (DataContext.Erro != 0)
                    {
                        if (pbolAbrirTransacao)
                        {
                            DataContext.RollbackTransaction();
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;
                    }
                    else
                    {
                        DataContext.CommitTransaction();
                    }
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "SetAlterarSenhaProcedure (UpDate) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public Central.Entity.UsuarioCentralEntity GetPesquisaParametroEmail()
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARPARAMETROEMAIL";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.UsuarioCentralEntity lEnt = new Central.Entity.UsuarioCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_SERV_SMTP_BOL_WEB = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.DS_EMAIL_PADRAO_BOL_WEB = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.DS_SENHA_EMAIL_PADRAO_BOL_WEB = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.NU_PORTA_SMTP_BOL_WEB = myReader.IsDBNull(3) ? 0 : int.Parse(myReader[3].ToString());
                            lEnt.DS_EMAIL_LOGIN_BOL_WEB = myReader.IsDBNull(4) ? "" : myReader[4].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetPesquisaParametroEmail (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
        public Central.Entity.UsuarioCentralEntity GetAcessoIntegrado(int pintCD_CONTABIL_PESSOA, 
                                                                      string pstrNU_SERIE_CTR, 
                                                                      int pintNU_CTR, 
                                                                      int pintNU_DV_CTR)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ACESSOCENTRALINTEGRADO";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_SERIE_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrNU_SERIE_CTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintNU_CTR;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_DV_CTR";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintNU_DV_CTR;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.UsuarioCentralEntity lEnt = new Central.Entity.UsuarioCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_EMAIL = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.CD_CONTABIL_PESSOA = myReader.IsDBNull(1) ? 0 : int.Parse(myReader[1].ToString());
                            lEnt.NU_CPF = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());
                            lEnt.NU_CNPJ = myReader.IsDBNull(3) ? 0 : int.Parse(myReader[3].ToString());
                            lEnt.NM_PESSOA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.DS_SENHA = myReader.IsDBNull(5) ? "" : myReader[5].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetAcessoProcedure (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
        public Central.Entity.UsuarioCentralEntity GetPesquisaParametroGlobalWeb()
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WS_VENDA_BUSCA_PARAMETRO_GLOBAL_WEB";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.UsuarioCentralEntity lEnt = new Central.Entity.UsuarioCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.NU_TEL_CONTATO = myReader.IsDBNull(1) ? "" : myReader[1].ToString();
                            lEnt.DS_MSG_ERRO = myReader.IsDBNull(2) ? "" : myReader[2].ToString();
                            lEnt.DS_MSG_SUCESSO = myReader.IsDBNull(2) ? "" : myReader[2].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetPesquisaParametroGlobalWeb (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }

        public Central.Entity.UsuarioCentralEntity GetAcessoIntegradoSSOJC(int pintCD_CONTABIL_PESSOA, 
                                                                           string pstrNU_DOC, 
                                                                           string pstrDS_EMAIL)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ACESSOCENTRALINTEGRADOSSOJC";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "CD_CONTABIL_PESSOA";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = pintCD_CONTABIL_PESSOA;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_DOC";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrNU_DOC;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrDS_EMAIL;

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        Central.Entity.UsuarioCentralEntity lEnt = new Central.Entity.UsuarioCentralEntity();

                        if (myReader.HasRows)
                        {

                            myReader.Read();

                            lEnt.DS_EMAIL = myReader.IsDBNull(0) ? "" : myReader[0].ToString();
                            lEnt.CD_CONTABIL_PESSOA = myReader.IsDBNull(1) ? 0 : int.Parse(myReader[1].ToString());
                            lEnt.NU_CPF = myReader.IsDBNull(2) ? 0 : int.Parse(myReader[2].ToString());
                            lEnt.NU_CNPJ = myReader.IsDBNull(3) ? 0 : int.Parse(myReader[3].ToString());
                            lEnt.NM_PESSOA = myReader.IsDBNull(4) ? "" : myReader[4].ToString();
                            lEnt.DS_SENHA = myReader.IsDBNull(5) ? "" : myReader[5].ToString();

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return lEnt;
                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetAcessoProcedure (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
        public List<UsuarioLoginEntity> GetLoginUsuarioCentralLondrina(string pstrCPFCNPJ, string pstrEmail)
        {
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_ACESSOCENTRALAUTENTICAR_INTEGREADO_V2";

                    DbParameter myParametro;

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "DS_EMAIL";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.String;
                    myParametro.Value = pstrEmail;

                    myComando.Parameters.Add(myParametro);

                    myParametro = myComando.CreateParameter();
                    myParametro.ParameterName = "NU_CPF_CNPJ";
                    myParametro.Direction = ParameterDirection.Input;
                    myParametro.DbType = DbType.Int32;
                    myParametro.Value = Int32.Parse(pstrCPFCNPJ);

                    myComando.Parameters.Add(myParametro);

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {
                        if (myReader.HasRows)
                        {
                            UsuarioLoginEntity lEnt = new UsuarioLoginEntity();
                            List<UsuarioLoginEntity> ObjList = new List<UsuarioLoginEntity>();

                            while (myReader.Read())
                            {
                                lEnt = new UsuarioLoginEntity();

                                lEnt.CD_USUARIO = int.Parse(myReader[0].ToString());

                                lEnt.NU_SERIE_CTR = myReader[1].ToString();
                                lEnt.NU_CTR = myReader[2].ToString();
                                lEnt.NU_DV_CTR = myReader[3].ToString();

                                lEnt.ST_ESTADO_ATUAL = int.Parse(myReader[4].ToString());

                                if (!myReader.IsDBNull(5))
                                {
                                    lEnt.CD_MOTIVO = int.Parse(myReader[5].ToString());
                                }

                                lEnt.TIPO = int.Parse(myReader[6].ToString());

                                ObjList.Add(lEnt);
                            }

                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return ObjList;

                        }
                        else
                        {
                            if (myReader != null)
                            {
                                if (!myReader.IsClosed)
                                {
                                    myReader.Close();
                                }
                            }

                            return null;
                        }
                    }
                    else
                    {
                        if (myReader != null)
                        {
                            if (!myReader.IsClosed)
                            {
                                myReader.Close();
                            }
                        }

                        _erro = DataContext.Erro;
                        _msgErro = DataContext.MsgErro;

                        return null;
                    }
                }
                else
                {
                    _erro = DataContext.Erro;
                    _msgErro = DataContext.MsgErro;

                    return null;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
            finally
            {
                if (DataContext.ConexaoAberta())
                {
                    if (!DataContext.FecharConexao())
                    {
                        _erro = -99;
                        _msgErro = "GetLoginUsuarioCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }

        }
    }
}
