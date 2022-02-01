using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Entity;
using CWA.Central.Entity;

namespace CWA.Venda.Data
{
    public class TipoEmailData
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

        public List<Central.Entity.TipoEmailAssinanteCentralEntity> GetLerTipoEmailAssinanteCentral()
        {
            //WSP_BUSCAREMAILSDOASSINANTE
            try
            {

                _erro = 0;
                _msgErro = "";

                if (DataContext.AbrirConexao())
                {
                    DbCommand myComando = DataContext.CriarComando(false);
                    myComando.CommandType = CommandType.StoredProcedure;
                    myComando.CommandText = "WSP_BUSCARTIPOEMAIL";

                    DbDataReader myReader = DataContext.ExecutarReader(myComando);

                    if (DataContext.Erro == 0)
                    {

                        if (myReader.HasRows)
                        {
                            List<TipoEmailAssinanteCentralEntity> lcolEnt = new List<TipoEmailAssinanteCentralEntity>();
                            TipoEmailAssinanteCentralEntity lEnt;

                            while (myReader.Read())
                            {
                                lEnt = new TipoEmailAssinanteCentralEntity();

                                lEnt.CD_TIPO_EMAIL = Int32.Parse(myReader[0].ToString());
                                lEnt.DS_TIPO_EMAIL = myReader.IsDBNull(1) ? "" : myReader[1].ToString();

                                lcolEnt.Add(lEnt);
                            }

                            return lcolEnt;

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
                        _msgErro = "GetLerTipoEmailAssinanteCentral (Select) - Erro ao fechar conexão com o banco de dados.";
                    }
                }
            }
        }
    }
}
