using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

using CWA.Venda.Data;
using CWA.Venda.Entity;
using CWA.Util;

namespace CWA.Venda.Business
{
    public class CuboRecibojcBusiness
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

        public List<CuboReciboJCEntity> GetCuboReciboJCList(string psDtInicioInic = "", string psDtInicioFim = "", Int32 psCdcontabil = 0)
        {

            try
            {

                if (!string.IsNullOrEmpty(psDtInicioInic))

                {

                    if (Utility.GetValidarData(psDtInicioInic.Trim()) == false)
                    {
                        _erro = -99;
                        _msgErro = "Data Inicial inválida";

                        return null;
                    }

                }

                if (!string.IsNullOrEmpty(psDtInicioInic))

                {

                    if (Utility.GetValidarData(psDtInicioFim.Trim()) == false)
                    {
                        _erro = -99;
                        _msgErro = "Data Final inválida";

                        return null;
                    }
                }

                CuboReciboJCData ObjData = new CuboReciboJCData();

                List<CuboReciboJCEntity> lcolEnt = new List<CuboReciboJCEntity>();

                lcolEnt = ObjData.GetCuboReciboJCEntityList(psDtInicioInic, psDtInicioFim, psCdcontabil);

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
      
    }
}

