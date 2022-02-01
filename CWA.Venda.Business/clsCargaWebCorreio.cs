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
    public class CargaWebCorreioBusiness
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

        public List<CargaWebCorreioEntity> GetCargaWebCorreio(int pintIDProduto,
                                                              int pintIDCampanha,
                                                              int pintStIndCortesia)
        {
            try
            {
                _erro = 0;
                _msgErro = "";


                clsCargaWebCorreioData ObjData = new clsCargaWebCorreioData();

                List<CargaWebCorreioEntity> ObjList = new List<CargaWebCorreioEntity>();

                ObjList = ObjData.GetCargaWebCorreio(pintIDProduto, pintIDCampanha, pintStIndCortesia);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;
                    return null;
                    
                }
                else
                {
                    return ObjList;
                }                
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }

        public List<CargaWebCorreioEntity> GetCargaWebCorpCorreio(int pintIDProduto,
                                                                  int pintIDCampanha,
                                                                  int pintStIndCortesia)
        {
            try
            {
                _erro = 0;
                _msgErro = "";


                clsCargaWebCorreioData ObjData = new clsCargaWebCorreioData();

                List<CargaWebCorreioEntity> ObjList = new List<CargaWebCorreioEntity>();

                ObjList = ObjData.GetCargaWebCorpCorreio(pintIDProduto, pintIDCampanha, pintStIndCortesia);

                if (ObjData.Erro != 0)
                {
                    _erro = ObjData.Erro;
                    _msgErro = ObjData.MsgErro;
                    return null;

                }
                else
                {
                    return ObjList;
                }
            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = ex.Message;
                return null;
            }
        }
        public string GetCargaWebCorreioService(int pintIDProduto,
                                                                     int pintIDCampanha,
                                                                     int pintStIndCortesia)
        {
            try
            {
                _erro = 0;
                _msgErro = "";


                //CargaWebCorreioBusiness ObjBLL = new CargaWebCorreioBusiness();

                List<CargaWebCorreioEntity> ObjList = new List<CargaWebCorreioEntity>();

                ObjList = this.GetCargaWebCorreio(pintIDProduto, pintIDCampanha, pintStIndCortesia);

                if (_erro != 0)
                {
                    //_erro = ObjBLL.Erro;
                    //_msgErro = ObjBLL.MsgErro;
                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(ObjList, Formatting.None);
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

        public string GetCargaWebCorpCorreioService(int pintIDProduto,
                                                                         int pintIDCampanha,
                                                                         int pintStIndCortesia)
        {
            try
            {
                _erro = 0;
                _msgErro = "";


                //CargaWebCorreioBusiness ObjBLL = new CargaWebCorreioBusiness();

                List<CargaWebCorreioEntity> ObjList = new List<CargaWebCorreioEntity>();

                ObjList = this.GetCargaWebCorpCorreio(pintIDProduto, pintIDCampanha, pintStIndCortesia);

                if (_erro != 0)
                {
                    //_erro = ObjBLL.Erro;
                    //_msgErro = ObjBLL.MsgErro;
                    return null;
                }
                else
                {
                    string lstrRet = JsonConvert.SerializeObject(ObjList, Formatting.None);
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
