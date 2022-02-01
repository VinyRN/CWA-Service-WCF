using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using CWA.Venda.Entity;

namespace CWA.Venda.Data
{
    public class SexoData
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

        public List<SexoEntity> GetSexoEntityList()
        {
            try
            {
                _erro = 0;
                _msgErro = "";

                string lstrVetorSexo = ConfigurationManager.AppSettings["ListSexo"];

                if (lstrVetorSexo == "")
                {
                    return null;
                }

                string[] VetorSexo = lstrVetorSexo.Split('|');

                List<SexoEntity> lcolEnt = new List<SexoEntity>();
                SexoEntity ObjEnt;

                foreach (var itemSexo in VetorSexo)
                {

                    if (itemSexo != "")
                    {
                        ObjEnt = new SexoEntity();

                        string[] VetorItem = itemSexo.Split(';');

                        if (VetorItem[0] != "")
                        {

                            ObjEnt.ID_SEXO = int.Parse(VetorItem[0]);
                            ObjEnt.DS_SEXO = VetorItem[1];

                            lcolEnt.Add(ObjEnt);
                        }

                    }

                }

                return lcolEnt;

            }
            catch (Exception ex)
            {
                _erro = -99;
                _msgErro = "SexoData (Lista) - " + ex.Message;

                return null;
            }
        }
        
    }
}
