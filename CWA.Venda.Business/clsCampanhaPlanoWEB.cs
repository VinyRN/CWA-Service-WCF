using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Venda.Business
{
    public class CampanhaPlanoWEBBusiness
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

        public void GravaCampanhaPlanoWEB(CampanhaPlanoWEBEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                DataContext.AbrirConexao();

                ObjData.SetCampanhaPlanoWEB(pObjEnt, true, true);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }

        public void AtualizaCampanhaPlanoWEB(CampanhaPlanoWEBEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                DataContext.AbrirConexao();

                ObjData.UpdCampanhaPlanoWEB(pObjEnt, true, true);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }

        public void DeleteCampanhaPlanoWEB(CampanhaPlanoWEBEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                DataContext.AbrirConexao();

                ObjData.DelCampanhaPlanoWEB(pObjEnt, true, true);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }

        public List<CampanhaPlanoWEBEntity> GetCampanhaPlanoWEB(int pintIDCampanha)
        {
            try
            {

                if (pintIDCampanha == 0)
                {
                    _erro = -1;
                    _msgErro = "Campanha não pode ser 0";

                    return null;
                }


                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetCampanhaPlanoWEBList;" + pintIDCampanha.ToString();

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    List<CampanhaPlanoWEBEntity> lcolEnt = new List<CampanhaPlanoWEBEntity>();

                    lcolEnt = (List<CampanhaPlanoWEBEntity>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

                    return lcolEnt;

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao retornar JSON vazio";

                    return null;
                }


            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }

            //try
            //{
            //    CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

            //    List<CampanhaPlanoWEBEntity> lcolEnt = new List<CampanhaPlanoWEBEntity>();

            //    lcolEnt = ObjData.GetCampanhaPlanoWEB(pintIDCampanha);

            //    if (ObjData.Erro != 0)
            //    {
            //        _erro = ObjData.Erro;
            //        _msgErro = ObjData.MsgErro;

            //        return null;
            //    }
            //    else
            //    {
            //        return lcolEnt;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    _erro = -99;
            //    _msgErro = ex.Message;

            //    return null;            
            //}
        }

        public CampanhaPlanoWEBEntity GetCampanhaPlanoWEB(int pintIDCampanha, int pintIDPlano)
        {
            try
            {
                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                CampanhaPlanoWEBEntity lEnt = new CampanhaPlanoWEBEntity();

                lEnt = ObjData.GetCampanhaPlanoWEB(pintIDCampanha, pintIDPlano);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return lEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }

        public List<CampanhaPlanoWEBEntity> GetCampanhaPlanoWEBConteudo(int pintDestaque)
        {

            try
            {
                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                List<CampanhaPlanoWEBEntity> lcolEnt = new List<CampanhaPlanoWEBEntity>();

                lcolEnt = ObjData.GetCampanhaPlanoWEBConteudo(pintDestaque);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    return lcolEnt;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        
        }

        public int GetTotalDestaque()
        {

            try
            {
                _erro = 0;
                _msgErro = "";


                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetTotalDestaque;";

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        //Tratar Retorno e setar as variaveis da classe.
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    string[] lstrVetorRet = lstrRet.Split(';');

                    if (lstrVetorRet != null)
                    {
                        return int.Parse(lstrRet);
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = "Erro no vetor de retorno - WCF/Engine";

                        return 0;

                    }

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao GetTotalDestaqueService - WCF/Engine";

                    return 0;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return 0;
            }
        }

        public string GetCampanhaPlanoWEBService(int pintIDCampanha)
        {
            try
            {
                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                List<CampanhaPlanoWEBEntity> lcolEnt = new List<CampanhaPlanoWEBEntity>();

                lcolEnt = ObjData.GetCampanhaPlanoWEB(pintIDCampanha);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                    return lstrRet;
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }
        }

        public void GravaCampanhaPlanoWEBService(CampanhaPlanoWEBEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (pObjEnt == null)
                {
                    _erro = -1;
                    _msgErro = "Objeto de Dados da Campanha/Plano não pode ser nulo";

                    return;
                }

                string lstrDados = JsonConvert.SerializeObject(pObjEnt, Formatting.None);

                Utility ObjUtil = new Utility();

                string lstrDadosCript = ObjUtil.SetCript(lstrDados);

                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                //Fazer a Chamada da EngineService passando os JSON Criptografado.

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GravaCampanhaPlanoWEB;" + lstrDadosCript;

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        //Tratar Retorno e setar as variaveis da classe.
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    string[] lstrVetorRet = lstrRet.Split(';');

                    if (lstrVetorRet != null)
                    {
                        _erro = int.Parse(lstrVetorRet[0]);
                        _msgErro = lstrVetorRet[1];
                    }

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao GravaCampanhaPlanoWEB - WCF/Engine";

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }

        public void AtualizaCampanhaPlanoWEBService(CampanhaPlanoWEBEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (pObjEnt == null)
                {
                    _erro = -1;
                    _msgErro = "Objeto de Dados da Campanha/Plano não pode ser nulo";

                    return;
                }

                string lstrDados = JsonConvert.SerializeObject(pObjEnt, Formatting.None);

                Utility ObjUtil = new Utility();

                string lstrDadosCript = ObjUtil.SetCript(lstrDados);

                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                //Fazer a Chamada da EngineService passando os JSON Criptografado.

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,AtualizaCampanhaPlanoWEB;" + lstrDadosCript;

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        //Tratar Retorno e setar as variaveis da classe.
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    string[] lstrVetorRet = lstrRet.Split(';');

                    if (lstrVetorRet != null)
                    {
                        _erro = int.Parse(lstrVetorRet[0]);
                        _msgErro = lstrVetorRet[1];
                    }

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao AtualizaCampanhaPlanoWEB - WCF/Engine";

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }

        public void DeleteCampanhaPlanoWEBService(CampanhaPlanoWEBEntity pObjEnt)
        {

            try
            {
                _erro = 0;
                _msgErro = "";

                if (pObjEnt == null)
                {
                    _erro = -1;
                    _msgErro = "Objeto de Dados da Campanha/Plano não pode ser nulo";

                    return;
                }

                string lstrDados = JsonConvert.SerializeObject(pObjEnt, Formatting.None);

                Utility ObjUtil = new Utility();

                string lstrDadosCript = ObjUtil.SetCript(lstrDados);

                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                //Fazer a Chamada da EngineService passando os JSON Criptografado.

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,DeleteCampanhaPlanoWEB;" + lstrDadosCript;

                ObjInterface.ExecutarInterface("", "", lstrParametroMetros, "WS");

                lstrVetRet = ObjInterface.VertorRetorno;

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        //Tratar Retorno e setar as variaveis da classe.
                        lstrRet = lstrVetRet[2];
                    }

                }

                if (lstrRet != "")
                {
                    string[] lstrVetorRet = lstrRet.Split(';');

                    if (lstrVetorRet != null)
                    {
                        _erro = int.Parse(lstrVetorRet[0]);
                        _msgErro = lstrVetorRet[1];
                    }

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro ao DeleteCampanhaPlanoWEB - WCF/Engine";

                    return;
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
            }

        }

        public string GetTotalDestaqueService()
        {
            try
            {

                CampanhaPlanoWEBData ObjData = new CampanhaPlanoWEBData();

                Int32 lintQtdDestaque = ObjData.GetTotalDestaque();

                if (lintQtdDestaque == 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;
                }

                return lintQtdDestaque.ToString();
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }



        }

    }
}
