using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CWA.Venda.Data;
using CWA.Central.Entity;
using CWA.Central.Domain.BusinessEntities;
using CWA.Util;
using CWA.EngineServices;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CWA.Central.Business
{
    public class ContratoAssinanteCentralBusiness
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



        public List<CWA.Central.Domain.BusinessEntities.ContratoAssinante> GetLerContratosCentral(int pintCD_CONTABIL_PESSOA,
                                                                            string pstrDS_EMAIL,
                                                                            Int64 plngNU_CPF,
                                                                            Int64 plngNU_CNPJ)
        {

            try
            {

                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetLerContratosCentralJSON((int)pintCD_CONTABIL_PESSOA, pstrDS_EMAIL, plngNU_CPF, plngNU_CNPJ);

                if (lstrRet != "")
                {
                    List<CWA.Central.Domain.BusinessEntities.ContratoAssinante> lcolEnt = new List<CWA.Central.Domain.BusinessEntities.ContratoAssinante>();

                    lcolEnt = (List<CWA.Central.Domain.BusinessEntities.ContratoAssinante>)JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

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


        }
        public string GetLerContratosCentralService(int pintCD_CONTABIL_PESSOA,
                                                    string pstrDS_EMAIL,
                                                    Int64 plngNU_CPF,
                                                    Int64 plngNU_CNPJ)
        {
            try
            {

                ContratoAssinanteData ObjData = new ContratoAssinanteData();
                List<ContratoAssinanteCentralEntity> lcolEnt = new List<ContratoAssinanteCentralEntity>();

                lcolEnt = ObjData.GetLerContratosCentral(pintCD_CONTABIL_PESSOA, pstrDS_EMAIL, plngNU_CPF, plngNU_CNPJ);

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

        public CWA.Central.Domain.BusinessEntities.ContratoAssinanteImpresso GetImprimirContratosCentral(int? pintCD_CONTABIL_PESSOA,
                                                                    string pstrNU_SERIE_CTR,
                                                                    int pintNU_CTR,
                                                                    byte pbyteNU_DV_CTR)
        {

            try
            {


                CWA.EngineServices.CWAService.IServiceCWA ObjService = (CWA.EngineServices.CWAService.IServiceCWA)ServiceClientCWA.GetServiceClintEndPoint();

                string lstrRet = ObjService.GetImprimirContratosCentralJSON((int)pintCD_CONTABIL_PESSOA, pstrNU_SERIE_CTR, pintNU_CTR, pbyteNU_DV_CTR);

                if (lstrRet != "")
                {
                    CWA.Central.Domain.BusinessEntities.ContratoAssinanteImpresso ObjEnt = new CWA.Central.Domain.BusinessEntities.ContratoAssinanteImpresso();

                    ObjEnt = (CWA.Central.Domain.BusinessEntities.ContratoAssinanteImpresso)JsonConvert.DeserializeObject(lstrRet, ObjEnt.GetType());

                    return ObjEnt;

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


        }
        public string GetImprimirContratosCentralService(int pintCD_CONTABIL_PESSOA,
                                                        string pstrNU_SERIE_CTR,
                                                        int pintNU_CTR,
                                                        byte pbyteNU_DV_CTR)
        {
            try
            {

                ContratoAssinanteData ObjData = new ContratoAssinanteData();
                ContratoAssinanteCentralEntity ObjEnt = new ContratoAssinanteCentralEntity();

                ObjEnt = ObjData.GetImpressaoContrato(pintCD_CONTABIL_PESSOA, pstrNU_SERIE_CTR, pintNU_CTR, pbyteNU_DV_CTR);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(ObjEnt, Formatting.None);
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

    }
}
