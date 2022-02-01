using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

using System.Xml;
using System.Xml.Linq;
using System.IO;

using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text.RegularExpressions;

using System.Net;
using System.Web.UI;
using System.Configuration;

using System.Security.Cryptography;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using CWA.Venda.Entity;


namespace CWA.Util
{
    public class Utility
    {

        string pstrChave = "EAC1835CW@";

        public static void SetLogXML(string pstrProcesso, string pstrTipo, string pstrErro, bool pbolEnviarEmail = false, int pintTipoPath = 1)
        {
            //pstrTipo (Erro,Alert,Sucesso)
            try
            {

                string lstrPathLog = "";
                if (pintTipoPath == 0)
                {
                    lstrPathLog = AppDomain.CurrentDomain.BaseDirectory + @"\" + ConfigurationManager.AppSettings["PathLog"].ToString();
                }
                else
                {
                    lstrPathLog = ConfigurationManager.AppSettings["PathLog"].ToString();
                }

                if (!File.Exists(lstrPathLog))
                {
                    XmlWriter writer = XmlWriter.Create(lstrPathLog, null);
                    writer.WriteStartDocument();
                    writer.WriteStartElement("LogSys");
                    writer.Close();
                }
                XDocument document = XDocument.Load(lstrPathLog);
                document.Element("LogSys").Add(new XElement("DadosLog", new object[] { new XElement("Processo", pstrProcesso), new XElement("Tipo", pstrTipo), new XElement("Erro", pstrErro), new XElement("DataLog", DateTime.Now.ToString()) }));
                document.Save(lstrPathLog);

                if (pbolEnviarEmail == true)
                {
                    //Implementar rotina de envio de email
                }

            }
            catch (Exception)
            {
                //throw exception;
            }
        }

        ////BANCO

        public Boolean DV_BANCOBRASIL(string Num_CC, string Num_DV)
        {
            int iTot, sD1;
            string sCC, sD2;

            string Base = "23456789";

            Num_CC = Num_CC.Trim();

            sCC = Convert.ToInt64(Num_CC).ToString("00000000");
            iTot = 0;

            for (int T = 0; T <= 7; T++)
            {
                iTot = iTot + (Convert.ToInt32(sCC[T].ToString()) * Convert.ToInt32(Base[T].ToString()));
            }

            sD1 = iTot % 11;
            sD2 = sD1.ToString();
            if (sD1 < 1)
            {
                sD2 = "0";
            }
            else if (sD1 == 10)
            {
                sD2 = "X";
            }

            if (sD2 == Num_DV.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean DV_CEF(string Nu_Agencia, string Num_CC, string Num_DV)
        {
            int iMlt = 2, iTot = 0, sD1;
            string sCC;

            Nu_Agencia = Convert.ToInt32(Nu_Agencia).ToString("0000");
            Num_CC = Convert.ToInt64(Num_CC).ToString("00000000000");
            sCC = Nu_Agencia + Num_CC;
            //sCC = Num_CC;

            //for(int i = 14; i >= 0; i--)
            for (int i = (sCC.Length - 1); i >= 0; i--)
            {
                iTot = iTot + (Convert.ToInt32(sCC[i].ToString()) * iMlt);
                if (iMlt == 9)
                {
                    iMlt = 2;
                }
                else
                {
                    iMlt++;
                }
            }

            if ((iTot % 11) <= 1)
            {
                sD1 = 0;
            }
            else
            {
                sD1 = 11 - (iTot % 11);
            }

            if (Num_DV == sD1.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean DV_BRADESCO(string Num_CC, string Num_DV)
        {
            int iTot = 0;
            string Base = "2765432", sD1;
            Num_CC = Convert.ToInt64(Num_CC).ToString("0000000");

            for (int i = 0; i <= (Num_CC.Length - 1); i++)
            {
                iTot = iTot + (Convert.ToInt32(Num_CC[i].ToString()) * Convert.ToInt32(Base[i].ToString()));
            }

            if ((iTot % 11) <= 1)
            {
                sD1 = "0";
            }
            else
            {
                sD1 = (11 - (iTot % 11)).ToString();
            }

            if (Num_DV.ToUpper() == "P")
            {
                Num_DV = "0";
            }

            if (sD1 == Num_DV)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean DV_ITAU(string Nu_Agencia, string Num_CC, string Num_DV)
        {
            int iTot = 0, i;
            string Base = "212121212", sD1;
            Nu_Agencia = Convert.ToInt64(Nu_Agencia).ToString("0000");
            Num_CC = Convert.ToInt64(Num_CC).ToString("00000");

            for (i = 0; i <= (Nu_Agencia.Length - 1); i++)
            {
                if ((Convert.ToInt32(Nu_Agencia[i].ToString()) * Convert.ToInt32(Base[i].ToString())) > 9)
                {
                    iTot = iTot + Convert.ToInt32((Convert.ToInt32(Nu_Agencia[i].ToString()) * Convert.ToInt32(Base[i].ToString())).ToString().Substring(0, 1)) + Convert.ToInt32((Convert.ToInt32(Nu_Agencia[i].ToString()) * Convert.ToInt32(Base[i].ToString())).ToString().Substring(1, 1));
                }
                else
                {
                    iTot = iTot + (Convert.ToInt32(Nu_Agencia[i].ToString()) * Convert.ToInt32(Base[i].ToString()));
                }
            }

            for (i = 0; i <= (Num_CC.Length - 1); i++)
            {
                if ((Convert.ToInt32(Num_CC[i].ToString()) * Convert.ToInt32(Base[i].ToString())) > 9)
                {
                    iTot = iTot + Convert.ToInt32((Convert.ToInt32(Num_CC[i].ToString()) * Convert.ToInt32(Base[i].ToString())).ToString().Substring(0, 1)) + Convert.ToInt32((Convert.ToInt32(Num_CC[i].ToString()) * Convert.ToInt32(Base[i].ToString())).ToString().Substring(1, 1));
                }
                else
                {
                    iTot = iTot + (Convert.ToInt32(Num_CC[i].ToString()) * Convert.ToInt32(Base[i].ToString()));
                }
            }

            if ((iTot % 10) < 1)
            {
                sD1 = "0";
            }
            else
            {
                sD1 = (10 - (iTot % 10)).ToString();
            }

            if (sD1 == Num_DV)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean DV_HSBC(string Num_CC, string Num_DV)
        {
            int iTot = 0, i;
            string Base = "8923456789", sD1;

            Num_CC = Convert.ToInt64(Num_CC).ToString("0000000000");

            for (i = 9; i >= 0; i--)
            {
                iTot = iTot + (Convert.ToInt32(Num_CC[i].ToString()) * Convert.ToInt32(Base[i].ToString()));
            }

            if ((iTot % 11) == 10)
            {
                sD1 = "0";
            }
            else
            {
                sD1 = (iTot % 11).ToString();
            }

            if (sD1 == Num_DV)
            { return true; }
            else
            { return false; }
        }

        public Boolean DV_AG_BANCOBRASIL(string Nu_Agencia, string Nu_Dv_Agencia)
        {
            int iTot = 0, i;
            string Base = "6789", sD1;

            Nu_Agencia = Convert.ToInt64(Nu_Agencia).ToString("0000");

            for (i = 0; i <= (Nu_Agencia.Length - 1); i++)
            {
                iTot = iTot + (Convert.ToInt32(Nu_Agencia[i].ToString()) * Convert.ToInt32(Base[i].ToString()));
            }

            sD1 = (iTot % 11).ToString();
            if (sD1 == "10")
            {
                sD1 = "X";
            }

            if (sD1 == Nu_Dv_Agencia.ToUpper())
            { return true; }
            else
            { return false; }
        }

        public Boolean DV_AG_BRADESCO(string Nu_Agencia, string Nu_Dv_Agencia)
        {
            int iTot = 0, i;
            string Base = "5432", sD1;

            Nu_Agencia = Convert.ToInt64(Nu_Agencia).ToString("0000");

            for (i = 0; i <= (Nu_Agencia.Length - 1); i++)
            {
                iTot = iTot + (Convert.ToInt32(Nu_Agencia[i].ToString()) * Convert.ToInt32(Base[i].ToString()));
            }
            sD1 = (iTot % 11).ToString();

            if ((11 - Convert.ToInt32(sD1)) == 10)
            { sD1 = "0"; }
            else
            { sD1 = (11 - Convert.ToInt32(sD1)).ToString(); }

            if (sD1 == Nu_Dv_Agencia.ToUpper())
            { return true; }
            else
            { return false; }
        }

        public Boolean CriticaAgencia(int pintBanco, string pstrAgencia, string pstrDvAgencia)
        {
            try
            {
                bool lbolOK = false;

                switch (pintBanco)
                {
                    case 1:
                        lbolOK = DV_AG_BANCOBRASIL(pstrAgencia, pstrDvAgencia);
                        break;
                    case 237: case 641:
                        lbolOK = DV_AG_BRADESCO(pstrAgencia, pstrDvAgencia);
                        break;
                    default:
                        lbolOK = true;
                        break;
                }

                return lbolOK;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public Boolean CriticaContaCorrente(int pintBanco, string pstrAgencia, string pstrConta, string pstrDvConta)
        {
            try
            {
                bool lbolOK = false;

                switch (pintBanco)
                {
                    case 1:   //BANCO DO BRASIL
                        lbolOK = DV_BANCOBRASIL(pstrConta, pstrDvConta);
                        break;
                    case 275:
                    case 356: //REAL
                        lbolOK = true; //DV_REAL(psAgencia & psConta & psDVConta);
                        break;
                    case 33: //BANESPA 
                        lbolOK = true; //DV_BANESPA(psAgencia & psConta & psDVConta);
                        break;
                    case 341: //ITAU
                        lbolOK = DV_ITAU(pstrAgencia, pstrConta, pstrDvConta);
                        break;
                    case 237: //BRADESCO
                        lbolOK = DV_BRADESCO(pstrConta, pstrDvConta);
                        break;
                    case 151: //NOSSA CAIXA
                        lbolOK = true; //DV_Nossa_Caixa(psAgencia & psConta & psDVConta);
                        break;
                    case 409: //UNIBANCO
                        lbolOK = true; //DV_UNIBANCO(psAgencia & psConta & psDVConta);
                        break;
                    case 399: //HSBC
                        lbolOK = DV_HSBC(pstrConta, pstrDvConta);
                        break;
                    case 104: //CEF
                        lbolOK = DV_CEF(pstrAgencia, pstrConta, pstrDvConta);
                        break;
                    case 41: //BANRISUL
                        lbolOK = true; //DV_BANRISUL(psAgencia & psConta & psDVConta);
                        break;
                    case 38: //BANESTADO
                        lbolOK = true; //DV_BANESTADO(psAgencia & psConta & psDVConta);
                        break;
                    case 641: //BBV
                        lbolOK = true; //DV_BBV(psAgencia & psConta & psDVConta); 
                        break;
                    case 24: //BANDEP
                        lbolOK = true; //DV_REAL(psAgencia & psConta & psDVConta); 
                        break;
                    default:
                        lbolOK = true;
                        break;
                }

                return lbolOK;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public string[] GetMaxLengthAgenciaConta(int pintBanco)
        {

            try
            {

                string[] lstrVetorMaxLength = null;

                switch (pintBanco)
                {
                    case 1:
                        lstrVetorMaxLength = new string[] { "4", "8" };
                        break;
                    case 275:
                        lstrVetorMaxLength = new string[] { "4", "8" };
                        break;
                    case 356:
                        lstrVetorMaxLength = new string[] { "4", "8" };
                        break;
                    case 33:
                        lstrVetorMaxLength = new string[] { "4", "8" };
                        break;
                    case 341:
                        lstrVetorMaxLength = new string[] { "4", "5" };
                        break;
                    case 237:
                        lstrVetorMaxLength = new string[] { "4", "7" };
                        break;
                    case 409:
                        lstrVetorMaxLength = new string[] { "4", "6" };
                        break;
                    case 399:
                        lstrVetorMaxLength = new string[] { "4", "6" };
                        break;
                    case 104:
                        lstrVetorMaxLength = new string[] { "4", "11" };
                        break;
                    case 41:
                        lstrVetorMaxLength = new string[] { "4", "9" };
                        break;
                    case 38:
                        lstrVetorMaxLength = new string[] { "4", "6" };
                        break;
                    case 641:
                        lstrVetorMaxLength = new string[] { "4", "9" };
                        break;
                    case 151:
                        lstrVetorMaxLength = new string[] { "4", "8" };
                        break;
                    default:
                        break;
                }

                return lstrVetorMaxLength;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public string GetAContaFormatada(int pintBanco, int pintConta)
        {

            try
            {
                string lstrContaFormat = "";

                switch (pintBanco)
                {
                    case 409: //UNIBANCO 
                        lstrContaFormat = pintConta.ToString("000000");
                        break;
                    case 275: //REAL 
                        lstrContaFormat = pintConta.ToString("00000000");
                        break;
                    case 356: //REAL 
                        lstrContaFormat = pintConta.ToString("00000000");
                        break;
                    case 21:  //BANESTES
                        lstrContaFormat = pintConta.ToString("00000000");
                        break;
                    case 399: //HSBC
                        lstrContaFormat = pintConta.ToString("00000");
                        break;
                    case 41:  //BANRISUL
                        lstrContaFormat = pintConta.ToString("000000000");
                        break;
                    case 1:   //BANCO DO BRASIL
                        lstrContaFormat = pintConta.ToString("00000000");
                        break;
                    case 104: //CAIXA ECONOMICA
                        lstrContaFormat = pintConta.ToString("00000000000");
                        break;
                    case 33:  //BANESPA
                        lstrContaFormat = pintConta.ToString("00000000");
                        break;
                    case 341: //ITAU
                        lstrContaFormat = pintConta.ToString("00000");
                        break;
                    case 237: //BRADESCO
                        lstrContaFormat = pintConta.ToString("0000000");
                        break;
                    case 151: //NOSSA CAIXA
                        lstrContaFormat = pintConta.ToString("00000000");
                        break;
                    case 38:  //BANESTADO
                        lstrContaFormat = pintConta.ToString("000000");
                        break;
                    case 641: //BBV 
                        lstrContaFormat = pintConta.ToString("000000000");
                        break;
                    case 24:  //BANDEPE 
                        lstrContaFormat = pintConta.ToString("0000000");
                        break;
                    default:  //OUTROS
                        lstrContaFormat = pintConta.ToString("0000000");
                        break;
                }

                return lstrContaFormat;

            }
            catch (Exception)
            {
                return "";
            }

        }

        ////CARTAO

        public int ValidarCartao(string lsNumCartao, int liBan)
        {

            try
            {
                int lintRetorno = 0;

                if (liBan != 6)
                {
                    lintRetorno = ValidarCartaoOUTROS(lsNumCartao, liBan);
                }
                else //HIPERCARD
                {
                    lintRetorno = ValidarCartaoHIPERCARD(lsNumCartao);
                }

                return lintRetorno;
            }
            catch (Exception ex)
            {

                return -1;

            }

        }

        private int ValidarCartaoOUTROS(string lsNumCartao, int liBan)
        {

            try
            {

                string lstrAMEX = "SOAM";
                string lstrCRED = "MCDI";
                string lstrVISA = "VS";
                string lstrHYPER = "HY";
                string lstrCREDS = "CS";


                string lsBandeira = "";
                string lsBytesInic = "";
                string lsEmpCart = "";
                string lsNumCart = "";
                string lsDV = "";
                string lsBin = "";

                string lstrCritica = "";

                int lnCodErro = 0;
                int lnLimiteInf = 0;
                int lnInd1 = 0;
                int lnInd2 = 0;

                lsBin = lsNumCartao.Substring(0, 6);

                //ACERTA BANDEIRA DE CARTÃO DINNERS PASSADO COM BANDEIRA MASTER
                if (liBan == 3)
                {
                    if (lsNumCartao.Trim().Length == 14)
                    {
                        liBan = 2;
                    }
                }

                lstrCritica = "";

                if (liBan == 1)
                {
                    lsBandeira = "AM";
                }
                else if (liBan == 2)
                {
                    lsBandeira = "DI";
                }
                else if (liBan == 3)
                {
                    lsBandeira = "MC";
                }
                else if (liBan == 4)
                {
                    lsBandeira = "SO";
                }
                else if (liBan == 5)
                {
                    lsBandeira = "VS";
                }
                else if (liBan == 6)
                {
                    lsBandeira = "HY";
                }
                else if (liBan == 8)
                {
                    lsBandeira = "CS";
                }
                else
                {
                    lsBandeira = "??";
                }

                lsNumCartao = lsNumCartao.PadLeft(18, '0');

                lnCodErro = 0;

                switch (lsBandeira)
                {

                    case "SO":
                        break;
                    case "AM":
                        break;
                    case "DI":
                        break;
                    case "MC":
                        if (lsNumCartao.Substring(2, 1) == "4")
                        {
                            lnCodErro = 7;
                        }
                        break;
                    case "VS":
                        if (lsNumCartao.Substring(2, 1) != "4")
                        {
                            lnCodErro = 7;
                        }
                        break;
                    case "CS":
                        break;
                    default:
                        break;
                }


                //primeiro
                if (lnCodErro == 0)
                {
                    # region primeiroIF

                    lsBytesInic = "";

                    switch (lsBandeira)
                    {
                        case "MC":
                            lsBytesInic = lsNumCartao.Substring(0, 2);
                            break;
                        case "VS":
                            lsBytesInic = lsNumCartao.Substring(0, 5);
                            break;
                        case "DI":
                            lsBytesInic = lsNumCartao.Substring(0, 4);
                            break;
                        case "AM":
                            lsBytesInic = lsNumCartao.Substring(0, 3);
                            break;
                        case "SO":
                            lsBytesInic = lsNumCartao.Substring(0, 3);
                            break;
                        case "CS":
                            lsBytesInic = lsNumCartao.Substring(0, 3);
                            break;
                        default:
                            break;
                    }

                    //VERIFICA BANDEIRA DO CARTÃO
                    switch (lsBandeira)
                    {
                        case "MC":
                        case "VS":
                            if (lsNumCartao.Substring(0, 2) != "00" || lsNumCartao.Substring(2, 1) == "0")
                            {
                                lnCodErro = 6;
                            }
                            break;
                        case "SO":
                            if ((lsNumCartao.Substring(0, 1) != "0") || (lsNumCartao.Substring(1, 1) == "0"))
                            {
                                lnCodErro = 6;
                            }
                            break;
                        case "AM":
                            if ((lsNumCartao.Substring(0, 3) != "000") || (lsNumCartao.Substring(3, 1) == "0"))
                            {
                                lnCodErro = 6;
                            }
                            break;
                        case "CS":
                            if ((lsNumCartao.Substring(0, 3) != "000") || (lsNumCartao.Substring(3, 1) == "0"))
                            {
                                lnCodErro = 6;
                            }
                            break;

                        default:
                            break;
                    }


                    //SEPARA CORPO DO NÚMERO DO CARTÃO
                    if (lnCodErro == 0)
                    {
                        #region segundoIf

                        if (lstrAMEX.IndexOf(lsBandeira) >= 0)
                        {
                            if (int.Parse(lsBytesInic) == 0)
                            {
                                lnLimiteInf = 8;
                                lsEmpCart = lsNumCartao.Substring(3, 4);
                                lsNumCart = lsNumCartao.Substring(7, 11);
                            }
                            else
                            {
                                lnLimiteInf = 6;
                                lsEmpCart = lsNumCartao.Substring(1, 4);
                                lsNumCart = lsNumCartao.Substring(5, 13);
                            }
                        }
                        else if (lstrCRED.IndexOf(lsBandeira) >= 0)
                        {

                            if (int.Parse(lsNumCartao.Substring(0, 4)) == 0)
                            {
                                lnLimiteInf = 9;
                                lsEmpCart = lsNumCartao.Substring(4, 4);
                                lsNumCart = lsNumCartao.Substring(8, 10);
                            }
                            else
                            {
                                lnLimiteInf = 7;
                                lsEmpCart = lsNumCartao.Substring(2, 4);
                                lsNumCart = lsNumCartao.Substring(6, 12);
                            }

                        }
                        else if (lstrVISA.IndexOf(lsBandeira) >= 0)
                        {
                            if (int.Parse(lsBytesInic) == 0)
                            {
                                lnLimiteInf = 10;
                                lsEmpCart = lsNumCartao.Substring(5, 4);
                                lsNumCart = lsNumCartao.Substring(9, 9);
                            }
                            else
                            {
                                lnLimiteInf = 7;
                                lsEmpCart = lsNumCartao.Substring(2, 4);
                                lsNumCart = lsNumCartao.Substring(6, 12);
                            }
                        }
                        else if (lstrHYPER.IndexOf(lsBandeira) >= 0)
                        {
                            if (int.Parse(lsBytesInic) == 0)
                            {
                                lnLimiteInf = 9;
                                lsEmpCart = lsNumCartao.Substring(0, 5);
                                lsNumCart = lsNumCartao.Substring(5, 13);
                            }

                        }
                        else if (lstrCREDS.IndexOf(lsBandeira) >= 0)
                        {
                            if (int.Parse(lsBytesInic) == 0)
                            {
                                lnLimiteInf = 8;
                                lsEmpCart = lsNumCartao.Substring(3, 4);
                                lsNumCart = lsNumCartao.Substring(7, 11);
                            }
                            else
                            {
                                lnLimiteInf = 6;
                                lsEmpCart = lsNumCartao.Substring(1, 4);
                                lsNumCart = lsNumCartao.Substring(5, 13);
                            }
                        }
                        else
                        {
                            lnCodErro = 9;
                        }


                        //terceiro if
                        if (lnCodErro == 0)
                        {
                            #region TerceiroIf
                            //CALCULA DV

                            switch (lsBandeira)
                            {
                                case "MC":
                                case "VS":
                                    if (lsNumCart.Substring(lsNumCart.Length - 1, 1) != CalcDvMod10(lsEmpCart + lsNumCart.Substring(0, (lsNumCart.Length - 1))))
                                    {
                                        lnCodErro = 5;
                                    }
                                    break;
                                case "DI":
                                    if (int.Parse(lsBytesInic) != 0)
                                    {
                                        lnCodErro = 6;
                                    }
                                    else if (lsNumCart.Substring(lsNumCart.Length - 1, 1) != CalcDvMod10(lsEmpCart + lsNumCart.Substring(0, (lsNumCart.Length - 1))))
                                    {
                                        lnCodErro = 5;
                                    }
                                    break;
                                case "SO":
                                case "AM":
                                    if (lsBandeira == "SO")
                                    {
                                        if (int.Parse(lsBytesInic.Substring(0, 1)) != 0)
                                        {
                                            lnCodErro = 6;
                                        }
                                        else if (int.Parse(lsBytesInic) != 0)
                                        {
                                            lnCodErro = 6;
                                        }
                                    }
                                    else if (lsNumCart.Substring(lsNumCart.Length - 1, 1) != CalcDvMod10Inv(lsEmpCart + lsNumCart.Substring(0, (lsNumCart.Length - 1))))
                                    {
                                        lnCodErro = 5;
                                    }
                                    break;
                                case "HY":
                                    if (int.Parse(lsBytesInic) != 0)
                                    {
                                        lnCodErro = 6;
                                    }
                                    else
                                    {
                                        if (lsNumCart.Substring(9, 1) != CalcDvMod11(lsNumCart.Substring(1, 8)))
                                        {
                                            lnCodErro = 5;
                                        }
                                        else if (lsNumCart.Substring(10, 1) != CalcDvMod11(lsNumCart.Substring(1, 9)))
                                        {
                                            lnCodErro = 5;
                                        }
                                        else if (lsNumCart.Substring(13, 1) != CalcDvMod11(lsNumCart.Substring(1, 8) + lsNumCart.Substring(11, 2)))
                                        {
                                            lnCodErro = 5;
                                        }
                                    }
                                    break;
                                case "CS":
                                    if (lsBandeira == "SO")
                                    {
                                        if (int.Parse(lsBytesInic.Substring(0, 1)) != 0)
                                        {
                                            lnCodErro = 6;
                                        }
                                        else if (int.Parse(lsBytesInic) != 0)
                                        {
                                            lnCodErro = 6;
                                        }
                                    }
                                    else if (lsNumCart.Substring(lsNumCart.Length - 1, 1) != CalcDvMod10Inv(lsEmpCart + lsNumCart.Substring(0, (lsNumCart.Length - 1))))
                                    {
                                        lnCodErro = 5;
                                    }
                                    break;
                                default:
                                    break;
                            }

                            #endregion terceiroif
                        }

                        #endregion segundoif
                    }

                    #endregion primeiroif
                }


                lstrCritica = "";

                switch (lnCodErro)
                {
                    case 0:
                        lstrCritica = "";
                        break;
                    case 5:
                        lstrCritica = "Dígito verificador do cartão inválido.";
                        break;
                    case 6:
                        lstrCritica = "Tamanho do número do cartão inválido.";
                        break;
                    case 7:
                        lstrCritica = "Cartão não é desta fonte.";
                        break;
                    case 8:
                        lstrCritica = "Companhia do cartão não prevista.";
                        break;
                    case 9:
                        lstrCritica = "Letra do cartão não prevista.";
                        break;
                    case 41:
                        lstrCritica = "Bin não previsto para a administradora.";
                        break;
                    default:
                        break;
                }

                return lnCodErro;
            }
            catch (Exception)
            {

                return -1;
            }



        }

        private int ValidarCartaoHIPERCARD(string lsCartao)
        {
            try
            {

                int liDig1;
                int liDig2;
                int liDig3;
                int liResto;

                int liDigCalc1;
                int liDigCalc2;
                int liDigCalc3;

                int ldSoma = 0;

                int liNumPos;
                int liParcSoma;
                int liResult = 0;

                string lstrCritica = "";

                if ((lsCartao.Length != 13) && (lsCartao.Length != 16))
                {
                    lstrCritica = "Tamanho do número do cartão inválido.";
                    return 6;
                }

                if (lsCartao.Length == 13)
                {
                    liDig1 = int.Parse(lsCartao.Substring(8, 1));
                    liDig2 = int.Parse(lsCartao.Substring(9, 1));
                    liDig3 = int.Parse(lsCartao.Substring(12, 1));


                    //Calcula primeiro digito
                    ldSoma = 0;
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(0, 1)) * 9);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(1, 1)) * 8);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(2, 1)) * 7);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(3, 1)) * 6);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(4, 1)) * 5);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(5, 1)) * 4);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(6, 1)) * 3);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(7, 1)) * 2);

                    liResto = ldSoma % 11;

                    if ((liResto == 0) || (liResto == 1))
                    {
                        liDigCalc1 = 0;
                    }
                    else
                    {
                        liDigCalc1 = 11 - liResto;
                    }

                    //Calcula segundo digito
                    ldSoma = 0;
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(0, 1)) * 2);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(1, 1)) * 9);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(2, 1)) * 8);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(3, 1)) * 7);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(4, 1)) * 6);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(5, 1)) * 5);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(6, 1)) * 4);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(7, 1)) * 3);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(8, 1)) * 2);

                    liResto = ldSoma % 11;

                    if ((liResto == 0) || (liResto == 1))
                    {
                        liDigCalc2 = 0;
                    }
                    else
                    {
                        liDigCalc2 = 11 - liResto;
                    }


                    //Calcula terceiro digito
                    ldSoma = 0;
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(0, 1)) * 3);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(1, 1)) * 2);

                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(2, 1)) * 9);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(3, 1)) * 8);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(4, 1)) * 7);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(5, 1)) * 6);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(6, 1)) * 5);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(7, 1)) * 4);

                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(10, 1)) * 3);
                    ldSoma = ldSoma + (int.Parse(lsCartao.Substring(11, 1)) * 2);

                    liResto = ldSoma % 11;

                    if ((liResto == 0) || (liResto == 1))
                    {
                        liDigCalc3 = 0;
                    }
                    else
                    {
                        liDigCalc3 = 11 - liResto;
                    }

                    if ((liDig1 != liDigCalc1) || (liDig2 != liDigCalc2) || (liDig3 != liDigCalc3))
                    {
                        lstrCritica = "Dígito verificador do cartão inválido.";
                        return 5;
                    }

                    return 0;

                }
                else //16 digitos
                {

                    ldSoma = 0;

                    for (int i = 0; i < lsCartao.Length - 1; i++)
                    {
                        liNumPos = int.Parse(lsCartao.Substring(i, 1));

                        if (((i + 1) % 2) == 0)
                        {
                            ldSoma = ldSoma + (liNumPos * 1);
                        }
                        else
                        {
                            liResult = 0;

                            if ((liNumPos * 2).ToString().Length == 1)
                            {
                                ldSoma = ldSoma + (liNumPos * 2);
                            }
                            else
                            {
                                liParcSoma = liNumPos * 2;

                                for (int x = 0; x < liParcSoma.ToString().Length; x++)
                                {
                                    liResult = liResult + int.Parse((liParcSoma.ToString().Substring(x, 1)));
                                }

                                ldSoma = ldSoma + liResult;
                                liResult = 0;
                            }
                        }
                    }

                    liResto = ldSoma % 10;

                    int DV;

                    if (liResto == 0)
                    {
                        DV = 0;
                    }
                    else
                    {
                        DV = 10 - liResto;
                    }

                    if (int.Parse(lsCartao.Substring(15, 1)) != DV)
                    {
                        lstrCritica = "Digito verificador inválido!";
                        return 5;
                    }
                    else
                    {
                        lstrCritica = "";
                        return 0;
                    }

                }

            }
            catch (Exception ex)
            {

                return -1;

            }

        }

        private string CalcDvMod10(string wRecAux)
        {

            int X;
            int wSoma = 0;
            int wResto;
            int wDigCalc;

            if (wRecAux == "")
            {
                return "";
            }

            for (int i = 0; i < wRecAux.Length; i++)
            {

                if ((i + 1) % 2 == 0)
                {
                    wSoma = wSoma + int.Parse(wRecAux.Substring(i, 1));
                }
                else
                {

                    if ((int.Parse(wRecAux.Substring(i, 1)) * 2) >= 10)
                    {
                        wSoma = wSoma + ((int.Parse(wRecAux.Substring(i, 1)) * 2) - 9);
                    }
                    else
                    {
                        wSoma = wSoma + (int.Parse(wRecAux.Substring(i, 1)) * 2);
                    }
                }

            }

            wResto = wSoma % 10;

            if (wResto == 0)
            {
                wDigCalc = 0;
            }
            else
            {
                wDigCalc = 10 - wResto;
            }

            return wDigCalc.ToString();
        }

        private string CalcDvMod10Inv(string wRecAux)
        {

            int X;
            int wSoma = 0;
            int wResto;
            int wDigCalc;

            if (wRecAux == "")
            {
                return "";
            }

            for (int i = 0; i < wRecAux.Length; i++)
            {

                if ((i + 1) % 2 == 0)
                {
                    if ((int.Parse(wRecAux.Substring(i, 1)) * 2) >= 10)
                    {
                        wSoma = wSoma + ((int.Parse(wRecAux.Substring(i, 1)) * 2) - 9);
                    }
                    else
                    {
                        wSoma = wSoma + (int.Parse(wRecAux.Substring(i, 1)) * 2);
                    }
                }
                else
                {
                    wSoma = wSoma + int.Parse(wRecAux.Substring(i, 1));

                }

            }

            wResto = wSoma % 10;

            if (wResto == 0)
            {
                wDigCalc = 0;
            }
            else
            {
                wDigCalc = 10 - wResto;
            }

            return wDigCalc.ToString();
        }

        private string CalcDvMod11(string wRecAux)
        {
            int[] intPesos = { 2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5, 6, 7, 8, 9 };

            if (wRecAux.Length > 16)
                throw new Exception("Número não suportado pela função!");

            int intSoma = 0;
            int intIdx = 0;
            for (int intPos = wRecAux.Length - 1; intPos >= 0; intPos--)
            {
                intSoma += Convert.ToInt32(wRecAux[intPos].ToString()) * intPesos[intIdx];
                intIdx++;
            }
            int intResto = (intSoma * 10) % 11;
            int intDigito = intResto;
            if (intDigito >= 10)
                intDigito = 0;

            return intDigito.ToString();
        }

        public string ValidarValidadeCartao(string pstrMes, string pstrAno)
        {

            try
            {

                if (pstrMes.Trim() == "")
                {
                    return "Favor informar o mês.";
                }

                if (pstrAno.Trim() == "")
                {
                    return "Favor informar o ano.";
                }


                string lstrData = DateTime.Now.ToString("dd/MM/yyyy");

                int lintMesAtual = int.Parse(lstrData.Substring(3, 2));
                int lintAnoAtual = int.Parse(lstrData.Substring(6, 4));


                if (int.Parse(pstrMes) <= lintMesAtual)
                {
                    if (int.Parse(pstrAno) <= lintAnoAtual)
                    {
                        return "Vencimento do cartão deve ser maior que o mês atual.";
                    }
                }


                return "";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string GetErroCartaoTratado(int lnCodErro)
        {

            try
            {

                string lstrCritica = "";

                switch (lnCodErro)
                {
                    case 0:
                        lstrCritica = "";
                        break;
                    case 5:
                        lstrCritica = "Dígito verificador do cartão inválido.";
                        break;
                    case 6:
                        lstrCritica = "Tamanho do número do cartão inválido.";
                        break;
                    case 7:
                        lstrCritica = "Cartão não é desta fonte.";
                        break;
                    case 8:
                        lstrCritica = "Companhia do cartão não prevista.";
                        break;
                    case 9:
                        lstrCritica = "Letra do cartão não prevista.";
                        break;
                    case 41:
                        lstrCritica = "Bin não previsto para a administradora.";
                        break;
                    default:
                        break;
                }

                return lstrCritica;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        ////OUTROS
        public string GetGuid()
        {
            try
            {
                return Guid.NewGuid().ToString();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string SetUpLoadoFile(HtmlInputFile ObjUpLoad, string pstrIDImagem, string pstrPathSave)
        {

            try
            {

                ObjUpLoad.PostedFile.SaveAs(pstrPathSave + pstrIDImagem);

                if (!File.Exists(pstrPathSave + pstrIDImagem))
                {
                    return "Arquivo não localizado no servidor";
                }

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }


        //SEGURANCA
        public string GetSenhaCIR2000(string pstrSenha)
        {
            try
            {

                string lstrSenha = pstrSenha;
                string lstrCript = "";

                int lintContSTRSenha = lstrSenha.Length;

                for (int lintIndex = lstrSenha.Length - 1; lintIndex >= 0; lintIndex--)
                {
                    int lintASC = (int)System.Convert.ToChar(lstrSenha.Substring(lintIndex, 1));
                    lstrCript = lstrCript.Trim() + SetCripCaractCIR2000(lintASC, lintContSTRSenha);

                    lintContSTRSenha--;
                }

                return lstrCript;
            }
            catch (Exception)
            {

                return "";
            }

        }

        private string SetCripCaractCIR2000(int pintNumero, int pintPosicao)
        {
            try
            {
                string lstrCaracter;
                int lintwNTemp;

                lintwNTemp = (120 - pintNumero + ((pintPosicao * 2) + 8));

                if ((lintwNTemp == 34) || (lintwNTemp == 39) || (lintwNTemp == 94) || (lintwNTemp == 96))
                {
                    lintwNTemp = lintwNTemp + 1;
                }

                lstrCaracter = ((Char)lintwNTemp).ToString();

                return lstrCaracter;

            }
            catch (Exception)
            {

                return "";
            }
        }

        public string SetCript(string pstrTextoCrip)
        {
            byte[] bytValue = { };
            byte[] bytKey = { };
            byte[] bytEncoded = { };
            byte[] bytIV = { 121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62 };
            int intLength = 0;
            int intRemaining = 0;
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream;
            RijndaelManaged objRijndaelManaged = new RijndaelManaged();

            //Utilidade ObjUtil = new Utilidade();

            pstrTextoCrip = TirarCaracterNull(pstrTextoCrip);

            bytValue = Encoding.ASCII.GetBytes(pstrTextoCrip.ToCharArray());
            intLength = pstrChave.Length;

            if (intLength >= 32)
            {
                pstrChave = pstrChave.Substring(0, 32);
            }
            else
            {
                intLength = pstrChave.Length;
                intRemaining = 32 - intLength;
            }

            bytKey = Encoding.ASCII.GetBytes(pstrChave.ToCharArray());

            try
            {


                objCryptoStream = new CryptoStream(objMemoryStream, objRijndaelManaged.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write);
                objCryptoStream.Write(bytValue, 0, bytValue.Length);
                objCryptoStream.FlushFinalBlock();

                bytEncoded = objMemoryStream.ToArray();
                objMemoryStream.Close();
                objCryptoStream.Close();

            }
            catch (Exception)
            {
                //colocar rotina de log    
                throw;
            }

            return Convert.ToBase64String(bytEncoded);

        }

        public string GetDesCript(string pstrTextoDescripty)
        {
            byte[] bytDataToBeDecrypted = { };
            byte[] bytTemp = { };
            byte[] bytIV = { 121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62 };

            int intLength = 0;
            int intRemaining = 0;
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream;
            RijndaelManaged objRijndaelManaged = new RijndaelManaged();

            byte[] bytDecryptionKey = { };
            string strReturnString = string.Empty;

            //Utilidade ObjUtil = new Utilidade();

            bytDataToBeDecrypted = Convert.FromBase64String(pstrTextoDescripty);

            intLength = pstrTextoDescripty.Length;

            if (intLength >= 32)
            {
                pstrTextoDescripty = pstrTextoDescripty.Substring(0, 32);

            }
            else
            {
                intLength = pstrChave.Length;
                intRemaining = 32 - intLength;
                pstrTextoDescripty = pstrTextoDescripty + new string('X', intRemaining);
            }

            bytDecryptionKey = Encoding.ASCII.GetBytes(pstrChave.ToCharArray());
            bytTemp = new byte[bytDataToBeDecrypted.Length];
            objMemoryStream = new MemoryStream(bytDataToBeDecrypted);

            try
            {
                objCryptoStream = new CryptoStream(objMemoryStream, objRijndaelManaged.CreateDecryptor(bytDecryptionKey, bytIV), CryptoStreamMode.Read);
                objCryptoStream.Read(bytTemp, 0, bytTemp.Length);
                //objCryptoStream.FlushFinalBlock(); 

                objMemoryStream.Close();
                objCryptoStream.Close();

            }
            catch (Exception)
            {
                //colocar rotina de log    
                throw;
            }

            return TirarCaracterNull(Encoding.ASCII.GetString(bytTemp));

        }

        public string TirarCaracterNull(string pstrTexto)
        {
            int length = 1;
            string str = pstrTexto;

            //do
            //{
            //    length = str.IndexOf(" ");
            //    if (length > 0)
            //    {
            //        str = str.Substring(0, length) + str.Substring(length + 1, str.Length - (length + 1));
            //    }
            //}
            //while (length > 0);

            length = 1;
            do
            {
                length = str.IndexOf("\0");
                if (length > 0)
                {
                    str = str.Substring(0, length) + str.Substring(length + 1, str.Length - (length + 1));
                }
            }
            while (length > 0);
            return str;
        }

        public string GetObjetoToStringJSON(Object pObjT)
        {
            try
            {
                string lstrResult = JsonConvert.SerializeObject(pObjT).ToString();
                return lstrResult;

            }
            catch (Exception)
            {
                return null;

            }
        }

        public Object GetStringJsonToObject(string pstrJson, System.Type pobjTipo)
        {
            try
            {
                Object lobjResult = JsonConvert.DeserializeObject(pstrJson, pobjTipo);
                return lobjResult;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void SetLogTXT(string pstrPathLog, string pstrTextoLog)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(pstrPathLog, true))
                {
                    writer.WriteLine(pstrTextoLog);
                }

            }
            catch (Exception)
            {
                //throw exception;
            }
        }

        public string SetMascaraCartao(string pstrCartao)
        {

            try
            {
                if (pstrCartao.Trim() == "")
                {
                    return pstrCartao;
                }
                //-----------------------
                // 0123 4567 8901 2345
                //-----------------------
                // 4551 8100 0160 2686
                //-----------------------

                string lstrRet = "";
                string lstrMask = "";

                lstrRet = pstrCartao.Substring(0, 4);

                for (int i = 0; i < (pstrCartao.Length - 8); i++)
                {
                    lstrMask = lstrMask + "*";
                }

                lstrRet = lstrRet + lstrMask + pstrCartao.Substring((pstrCartao.Length - 4), 4);

                return lstrRet;
            }
            catch (Exception)
            {
                return pstrCartao;
            }


        }

        public string GetCriptCartaoCIR2000()
        {
            return "#MestreByms#CW@1835";
        }

        public static bool GetValidarData(string pstrData)
        {
            try
            {
                DateTime date;
                if (DateTime.TryParseExact(pstrData, "dd/MM/yyyy",
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out date))
                {
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {
                return false;
            }
        }

        public string[] SetSenhaUserCentral()
        {
            try
            {
                string NovaSenha = string.Empty;
                string NovaSenhaCript = string.Empty;

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);


                NovaSenha = finalString;

                if (NovaSenha.Trim() != "")
                {
                    NovaSenhaCript = CriptLogin.Encriptar(NovaSenha);
                }
                else
                {
                    NovaSenhaCript = NovaSenha;
                }
                return new string[] { "OK", NovaSenha, NovaSenhaCript };
            }
            catch (Exception)
            {
                return new string[] { "ERRO", "", "" };
            }
        }

        public string[] GetSenhaUserCentral(string pstrSenhaCript)
        {
            try
            {

                if (string.IsNullOrEmpty(pstrSenhaCript))
                {
                    return new string[] { "ERRO", "", "" };
                }

                string lstrSenhaDescript = string.Empty;

                lstrSenhaDescript = CriptLogin.Desencriptar(pstrSenhaCript);

                if (!string.IsNullOrEmpty(lstrSenhaDescript))
                {
                    return new string[] { "OK", lstrSenhaDescript, "" };
                }
                else
                {
                    return new string[] { "ERRO", "", "" };
                }

            }
            catch (Exception)
            {
                return new string[] { "ERRO", "", "" };
            }
        }

        public static bool GetValidarNumero(string valor)
        {
            try
            {
                Int32 numero = Int32.Parse(valor);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static string GetRetornoJSON(string pstrIDRetorno, string pstrDescricaoRetorno, string pstrDadosRetorno)
        {
            RetornoJSON ObjRetJSON = new RetornoJSON();

            try
            {
                ObjRetJSON.CodigoRetorno = pstrIDRetorno;
                ObjRetJSON.DescricaoRetorno = pstrDescricaoRetorno;
                ObjRetJSON.DadosRetorno = pstrDadosRetorno;

                string lstrRet = JsonConvert.SerializeObject(ObjRetJSON, Newtonsoft.Json.Formatting.None);

                return lstrRet;

            }
            catch (Exception ex)
            {
                //Newtonsoft.Json

                ObjRetJSON.CodigoRetorno = "-99";
                ObjRetJSON.DescricaoRetorno = "Erro no processo de geração JSON de retorno";
                ObjRetJSON.DadosRetorno = "";

                string lstrRet = JsonConvert.SerializeObject(ObjRetJSON, Newtonsoft.Json.Formatting.None);

                return lstrRet;
            }

        }

        public static string GetRetornoAPIRest(int pintStatus, string pstrDescricao, string pstrMensagem)
        {
            RetornoAPIRest ObjRestAPI = new RetornoAPIRest();
            MensagemAPI ObjMensagem = new MensagemAPI();

            try
            {
                ObjMensagem.retorno = pstrMensagem;

                ObjRestAPI.status = pintStatus;
                ObjRestAPI.descricao = pstrDescricao;
                ObjRestAPI.mensagem = ObjMensagem;          
                    
                string lstrRet = JsonConvert.SerializeObject(ObjRestAPI, Newtonsoft.Json.Formatting.None);

                return lstrRet;

            }
            catch (Exception ex)
            {

                ObjMensagem.retorno = "";

                ObjRestAPI.status = -99;
                ObjRestAPI.descricao = ex.Message;
                ObjRestAPI.mensagem = ObjMensagem;

                string lstrRet = JsonConvert.SerializeObject(ObjRestAPI, Newtonsoft.Json.Formatting.None);

                return lstrRet;
            }

        }

        public static bool GetEmailValido(string pstrEmail)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(pstrEmail))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static string SetEnCryptURL(string pstrValor)
        {
            try
            {
                CriptURL ObjCriptURL = new CriptURL();

                string lstrCript = ObjCriptURL.ActionEncrypt(pstrValor);

                return lstrCript;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string SetDesCryptURL(string pstrValor)
        {
            try
            {
                CriptURL ObjCriptURL = new CriptURL();

                string lstrCript = ObjCriptURL.ActionDecrypt(pstrValor);

                return lstrCript;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static bool IsNumeric(string pstrValor)
        {
            try
            {
                float output;
                return float.TryParse(pstrValor, out output);
            }
            catch (Exception)
            {
                return false;
            }
        }

        //SSO JC 
        public static string GetSSOJC(string pstrCodeToken)
        {
            try
            {

               string lstrURLPost = ConfigurationManager.AppSettings["UrlPostLoginIntegrado"].ToString();

                var request = (HttpWebRequest)WebRequest.Create(lstrURLPost);

                var postData = "code=" + Uri.EscapeDataString(pstrCodeToken);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString.ToString();
            }
            catch (Exception ex)
            {

                return "";
            }
        }

        public static string GetAssinanteSSOJC(RetornoSSOJC ObjEnt)
        {

            try
            {
                string lstrURLPost = ConfigurationManager.AppSettings["UrlPostInfoUserLoginIntegrado"].ToString();

                var request = (HttpWebRequest)WebRequest.Create(lstrURLPost);

                //var postData = "code=" + Uri.EscapeDataString(code);
                var postData = "access_token=" + Uri.EscapeDataString(ObjEnt.access_token) + "&refresh_token=" + Uri.EscapeDataString(ObjEnt.refresh_token);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;

            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static string SetLogOffSSOJC(string pstrCodeToken, string pstrRefreshToken)
        {
            try
            {

                string lstrURLPost = ConfigurationManager.AppSettings["UrlLogOffIntegrado"].ToString();

                var request = (HttpWebRequest)WebRequest.Create(lstrURLPost);

                var postData = "access_token=" + Uri.EscapeDataString(pstrCodeToken) + "&refresh_token=" + Uri.EscapeDataString(pstrRefreshToken);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString.ToString();
            }
            catch (Exception ex)
            {

                return "";
            }
        }

    }
}
