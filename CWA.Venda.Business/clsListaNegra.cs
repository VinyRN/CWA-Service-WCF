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
    public class ListaNegraBusiness
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

        public string[] GetValidarListaNegra(ListaNegraEntity pObjEnt)
        {

            try
            {

                string lstrJSONListaNegra = "";
                lstrJSONListaNegra = JsonConvert.SerializeObject(pObjEnt, Formatting.None);  

                if (lstrJSONListaNegra == "")
                {
                    _erro = -1;
                    _msgErro = "Erro ao serializar objeto da lista negra";

                    return null;
                }



                string lstrParametroMetros = "";
                string lstrRet = "";
                string[] lstrVetRet = null;

                GerenciaInterface ObjInterface = new GerenciaInterface();

                lstrParametroMetros = "CWA.EngineServices.InterfaceLoja,GetValidarListaNegra;" + lstrJSONListaNegra;

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
                    string[] lcolEnt = new string[] {}; 

                    lcolEnt = (string[])JsonConvert.DeserializeObject(lstrRet, lcolEnt.GetType());

                    if (lcolEnt != null)
                    {
                           bool lbolErro = false;

                            if (lcolEnt != null)
                            {

                                if (lcolEnt[0].Trim() == "1")
                                {
                                    Util.Utility.SetLogXML("ListaNegra", "ERRO", "Cartão na lista negra", false);
                                    lbolErro = true;
                                }

                                if (lcolEnt[1].Trim() == "1")
                                {
                                    Util.Utility.SetLogXML("ListaNegra", "ERRO", "Conta na lista negra", false);
                                    lbolErro = true;
                                }

                                if (lcolEnt[2].Trim() == "1")
                                {
                                    Util.Utility.SetLogXML("ListaNegra", "ERRO", "CPF/CNPJ na lista negra", false);
                                    lbolErro = true;
                                }

                                if (lcolEnt[3].Trim() == "1")
                                {
                                    Util.Utility.SetLogXML("ListaNegra", "ERRO", "Endereço na lista negra", false);
                                    lbolErro = true;
                                }

                            }

                            if (lbolErro == true)
                            {
                                return lcolEnt;
                            }
                            else
                            {
                                return null;                        
                            }
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
                _erro = -99;
                _msgErro = ex.Message;

                return null;
            }

        }

        public string GetValidarListaNegraService(ListaNegraEntity pObjEnt)
        {

            try
            {

                ListaNegraData ObjData = new ListaNegraData();

                string[] lcolEnt = ObjData.GetValidarListaNegra(pObjEnt);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;

                    return null;
                }
                else
                {
                    bool lbolErro = false;

                    if (lcolEnt != null)
                    {

                        if (lcolEnt[0].Trim() == "1")
                        {
                            Util.Utility.SetLogXML("ListaNegra", "ERRO", "Cartão na lista negra", false);
                            lbolErro = true;
                        }

                        if (lcolEnt[1].Trim() == "1")
                        {
                            Util.Utility.SetLogXML("ListaNegra", "ERRO", "Conta na lista negra", false);
                            lbolErro = true;
                        }

                        if (lcolEnt[2].Trim() == "1")
                        {
                            Util.Utility.SetLogXML("ListaNegra", "ERRO", "CPF/CNPJ na lista negra", false);
                            lbolErro = true;
                        }

                        if (lcolEnt[3].Trim() == "1")
                        {
                            Util.Utility.SetLogXML("ListaNegra", "ERRO", "Endereço na lista negra", false);
                            lbolErro = true;
                        }

                    }

                    if (lbolErro == false)
                    {
                        string lstrRet = JsonConvert.SerializeObject(lcolEnt, Formatting.None);
                        return lstrRet;
                    }
                    else
                    {
                        return null;
                    }

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
