using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Collections.Specialized;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace CWA.Venda.Business
{
    public class UsuarioLoginBusiness
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


        public string[] GetLoginUsuario(string pstrUsuario,string pstrSenha) 
        {
            try
            {
                string lstrSenhaCript = new Utility().GetSenhaCIR2000(pstrSenha);

                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetLoginUsuario;" + pstrUsuario + ";" + lstrSenhaCript + ";";

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
                    string[] lstrVetorUser = null;
                    string lstrJasonRet = "";

                    lstrJasonRet = JsonConvert.DeserializeObject(lstrRet).ToString();

                    if (!string.IsNullOrEmpty(lstrJasonRet))
                    {
                        lstrVetorUser = lstrJasonRet.Split(',');
                        return lstrVetorUser;
                    }
                    else
                    {
                        return null;
                    }

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
                return new string[] { "-99", ex.Message, "0", "" };
            }
        
        }
        public string GetLoginUsuarioService(string pstrUsuario, string pstrSenha)
        {
            try
            {
                //string lstrSenhaCript = new Utility().GetSenhaCIR2000(pstrSenha);

                if (pstrSenha == "")
                {
                    _erro = -99;
                    _msgErro = "Error Cript CWA";

                    return null; //new string[] { "-99", "Error Cript CWA", "0", "" };
                }

                UsuarioLoginData ObjData = new UsuarioLoginData();

                UsuarioLoginEntity ObjEnt = ObjData.GetLoginUsuario(pstrUsuario, pstrSenha);

                if (ObjEnt == null)
                {
                    if (ObjData.Erro == 0)
                    {
                        _erro = -99;
                        _msgErro = "Usuário ou Senha inválido";

                        return null; //new string[] { "-99", "Usuário ou Senha inválido", "0", "" };
                    }
                    else
                    {
                        _erro = -99;
                        _msgErro = "Usuário ou Senha inválido";

                        return null; //new string[] { "-99", ObjData.MsgErro, "0", "" };
                    }

                }
                else
                {
                    return JsonConvert.SerializeObject("0,," + ObjEnt.CD_USUARIO.ToString() + "," + ObjEnt.DS_LOGIN);
                }

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;  //new string[] { "-99", ex.Message, "0", "" };
            }

        }
        public string GetLoginUsuarioCentralJSON (string pstrLogin, string pstrSenha)
        {
            try
            {

                UsuarioLoginData ObjData = new UsuarioLoginData();
                List<UsuarioLoginEntity> ObjListEnt = new List<UsuarioLoginEntity>();

                ObjListEnt = ObjData.GetLoginUsuarioCentral(pstrLogin, pstrSenha);

                if (ObjListEnt != null)
                {

                    int lintCentral = 1;
                    int lintPortal = 1;
                    int lintTipoProduto = 0;
                    int lintCampanha = 0;
                    int lintPlano = 0;
                    int lintSituacaoAtual = 0;
                    string lstrHashUser = "";
                    string lstrNome = "";


                    ContratoAssinanteBusiness ObjCTRBusiness = new ContratoAssinanteBusiness();

                    foreach (UsuarioLoginEntity ObjItem in ObjListEnt)
                    {
                        string[] lstrVetorRet;

                        lstrVetorRet = ObjCTRBusiness.GetStatusAplicacao(int.Parse(ObjItem.NU_CTR),
                                                                         int.Parse(ObjItem.NU_DV_CTR),
                                                                         ObjItem.NU_SERIE_CTR,
                                                                         ObjItem.CD_USUARIO);
                        if (lstrVetorRet != null)
                        {

                            //####################################################################
                            //ORGANIZAÇÃO da MATRIZ POR POSIÇÃO.
                            //####################################################################
                            // 0 = Status se executou ou não com sucesso (0 = Sucesso).
                            // 1 = Mensagem de erro caso a posição 0 seja diferente de sucesso.
                            // 2 = HASH contendo o User e Login do assinante
                            // 3 = Nome do assinante 
                            // 4 = Tipo de Assinantura ( 0 = IMPRESSO / 1 = DIGITAL / 2 = COMBO )
                            // 5 = CAMPANHA
                            // 6 = PLANO
                            //####################################################################

                            if (lstrVetorRet[0] == "S")
                            {                               
                                if ((lstrVetorRet[1] == "0") && (lstrVetorRet[2] == "0"))     //POSSUI ACESSO AO PORTAL E A CENTRAL
                                {
                                    //return Utility.GetRetornoJSON("0", "OK", "0;0;" + lstrHashUser);
                                    lintCentral = 0;
                                    lintPortal = 0;
                                    lstrNome = lstrVetorRet[3];
                                    //lintTipoProduto = int.Parse(lstrVetorRet[4]);
                                }
                                else if ((lstrVetorRet[1] == "1") && (lstrVetorRet[2] == "1")) //NÃO POSSUI ACESSO AO PORTAL E A CENTRAL
                                {
                                    lintCentral = 1;
                                    lintPortal = 1;
                                    lstrNome = lstrVetorRet[3];
                                    //lintTipoProduto = int.Parse(lstrVetorRet[4]);

                                }
                                else if ((lstrVetorRet[1] == "0") && (lstrVetorRet[2] == "1")) //POSSUI ACESSO AO PORTAL E NÃO A CENTRAL
                                {
                                    if ((lintPortal == 1))
                                    {
                                        lintPortal = 0;
                                    }
                                }
                                else if ((lstrVetorRet[1] == "1") && (lstrVetorRet[2] == "0")) //NÃO POSSUI ACESSO AO PORTAL E SIM A CENTRAL
                                {
                                    if ((lintCentral == 1))
                                    {
                                        lintCentral = 0;
                                    }
                                }

                                if (lstrHashUser.Trim() == "")
                                {
                                    lstrHashUser = Utility.SetEnCryptURL(ObjItem.DS_LOGIN + "^" + ObjItem.DS_SENHA);
                                }

                                if ((lstrVetorRet[3] != ""))
                                {
                                    if (lstrNome.Trim() == "")
                                    {
                                        lstrNome = lstrVetorRet[3];
                                    }
                                }

                                if ((lintTipoProduto == 0))
                                {
                                    lintTipoProduto = int.Parse(lstrVetorRet[4]); //IMPRESSO
                                }

                                if ((lintTipoProduto != 0) && (int.Parse(lstrVetorRet[4]) == 1))
                                {
                                    lintTipoProduto = int.Parse(lstrVetorRet[4]); //DIGITAL
                                }

                                if ((lintTipoProduto != 0) && (int.Parse(lstrVetorRet[4]) == 2))
                                {
                                    lintTipoProduto = int.Parse(lstrVetorRet[4]); //COMBO
                                }

                                if (ObjListEnt.Count == 1)
                                {
                                    lintCampanha = int.Parse(lstrVetorRet[5]);
                                    lintPlano = int.Parse(lstrVetorRet[6]);
                                    lintSituacaoAtual = ObjItem.ST_ESTADO_ATUAL;
                                }
                                else
                                {
                                    if (ObjItem.ST_ESTADO_ATUAL == 1)
                                    {
                                        lintCampanha = int.Parse(lstrVetorRet[5]);
                                        lintPlano = int.Parse(lstrVetorRet[6]);
                                    }
                                    else
                                    {
                                        if ((lintCampanha == 0) && (lintPlano == 0))
                                        {
                                            if ((lstrVetorRet[5]) != "" && (lstrVetorRet[6]) != "")
                                            {
                                                lintCampanha = int.Parse(lstrVetorRet[5]);
                                                lintPlano = int.Parse(lstrVetorRet[6]);
                                            }
                                        }
                                    }

                                    lintSituacaoAtual = ObjItem.ST_ESTADO_ATUAL;
                                }

                            }
                        }
                    }

                    return Utility.GetRetornoJSON("0", "OK", lintPortal.ToString() + ";" + lintCentral.ToString() + ";" + lstrHashUser + ";" + lstrNome + ";" + lintTipoProduto.ToString() + ";" + lintCampanha.ToString() + ";" + lintPlano.ToString() + ";" + lintSituacaoAtual.ToString());

                }
                else
                {
                    return Utility.GetRetornoJSON("-1", "usuario não encontrado para o login e senha informado.", "");
                }

                
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message ;

                return Utility.GetRetornoJSON("-99", _msgErro,"");
            }
        }

        public string GetLoginUsuarioCentralFolha(string pstrCPFCNPJ, string pstrEmail)
        {
            _erro = 0;
            _msgErro = "";

            try
            {

                if (pstrCPFCNPJ == "")
                {
                    _erro = -1;
                    _msgErro = "CPF/CNPJ não pode ser vazio";
                    return null;
                }

                if (pstrEmail == "")
                {
                    _erro = -1;
                    _msgErro = "E-mail não pode ser vazio";
                    return null;
                }

                string lstrHashUser = "";
                string[] VetMotivoSusp = new string[] { "36,1077,1076,1031,1030" };

                string lstrListIntegracaoAcesso = ConfigurationManager.AppSettings["ListIntegracaoAcesso"];
                string[] lstrVetorStatusAcesso = lstrListIntegracaoAcesso.Split('|');

                UsuarioLoginData ObjData = new UsuarioLoginData();
                List<UsuarioLoginEntity> ObjListUser = new List<UsuarioLoginEntity>();

                ObjListUser = ObjData.GetLoginUsuarioCentralLondrina(pstrCPFCNPJ, pstrEmail);

                //Portal|Central|Codigo Assinante
                string lstrRet = "-1|-1|-1";

                foreach (UsuarioLoginEntity ObjItem in ObjListUser)
                {
                    if (ObjItem.ST_ESTADO_ATUAL == 2)
                    {
                        foreach (string item in VetMotivoSusp)
                        {
                            if (item == ObjItem.CD_MOTIVO.ToString())
                            {
                                lstrHashUser = Utility.SetEnCryptURL(ObjItem.CD_USUARIO + "^" + ObjItem.NU_SERIE_CTR  + "^" + ObjItem.NU_CTR.ToString() + "^" + ObjItem.NU_DV_CTR.ToString());
                                lstrRet = "0|0|" + lstrHashUser;
                                return lstrRet;
                            }
                        }
                    }
                    else if (ObjItem.ST_ESTADO_ATUAL == 14)
                    {
                        ContratoAssinanteData ObjDataCTR = new ContratoAssinanteData();
                        StatusContratoEntity lEntCTR = new StatusContratoEntity();
                        lEntCTR = ObjDataCTR.GetStatusContrato(int.Parse(ObjItem.NU_CTR), int.Parse(ObjItem.NU_DV_CTR), ObjItem.NU_SERIE_CTR, ObjItem.CD_USUARIO);

                        if (lEntCTR != null)
                        {
                            int lintDiaAcessoBV = int.Parse(ConfigurationManager.AppSettings["TempoAcessoBV"]);

                            DateTime ldtDataDia = DateTime.Now;
                            DateTime ldtDatavenda = lEntCTR.DT_VENDA;

                            TimeSpan lintCalcDias = (ldtDataDia - ldtDatavenda);

                            int lintDifDays = lintCalcDias.Days;

                            if (lintDifDays <= lintDiaAcessoBV)
                            {
                                lstrHashUser = Utility.SetEnCryptURL(ObjItem.CD_USUARIO + "^" + ObjItem.NU_SERIE_CTR + "^" + ObjItem.NU_CTR.ToString() + "^" + ObjItem.NU_DV_CTR.ToString());
                                lstrRet = "0|0|" + lstrHashUser;
                                return lstrRet;
                            }
                        }
                    }
                    else
                    {
                        //DIF CANCELADO / ATENDIDO
                        if ((ObjItem.ST_ESTADO_ATUAL != 3) && (ObjItem.ST_ESTADO_ATUAL != 8))
                        {
                            lstrHashUser = Utility.SetEnCryptURL(ObjItem.CD_USUARIO + "^" + ObjItem.NU_SERIE_CTR + "^" + ObjItem.NU_CTR.ToString() + "^" + ObjItem.NU_DV_CTR.ToString());
                            lstrRet = "0|0|" + lstrHashUser;
                            return lstrRet;

                        }
                    }
                }

                return lstrRet;
            }
            catch (Exception ex)
            {
                _erro = 0;
                _msgErro = ex.Message + " - " + ex.InnerException;

                return null;
            }
        }

    }
}
