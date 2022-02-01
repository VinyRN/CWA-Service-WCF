using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;


namespace CWA.Util
{
    public class Email
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

        public void Enviar(string pstrMsg, 
                           string pstrDestinatario, 
                           CWA.Venda.Entity.BoletoParametroEntity ObjParamBoleto = null, 
                           string pstrCaminhoAnexo = "",
                           string[] pstrVetorEmailVB6 = null,
                           int pintRecuperaSenha = 0)
        {

            _erro = 0;
            _msgErro = "";


            try
            {

                Utility ObjUtil = new Utility();

                string lstrHost = "";
                string lstrPorta = "";
                string lstrSsl = "";
                string lstrSubject = "";
                string lstrPathAnexo = "";
                string lstrEmailRemetente = "";
                string lstrPathTemplateCorpoMSG  = "";
                string lstrCorpoEmail = "";

                string lstrDestinatario = "";

                int lintPorta = 0;
                bool lbSsl = false;

                //Login para Exchange 
                string lstrUser = "";
                string lstrPwd = "";
                string lstrPwdDesc = "";

                int lintAutentica = 0;

                if (pstrVetorEmailVB6 != null)
                {

                    //pstrHost, pstrPorta, pstrSsl, pstrSubject, pstrEmailDestinatario, pstrPathAnexo, 
                    //pstrEmailRemetente, pstrPathTemplateCorpoMSG, pstrCorpoEmail,  
                    //pstrUserLogin, pstrPwdLogin 

                    lstrHost = pstrVetorEmailVB6[0];
                    lstrPorta = pstrVetorEmailVB6[1];
                    lstrSsl = pstrVetorEmailVB6[2];
                    lstrSubject = pstrVetorEmailVB6[3];

                    lstrDestinatario = pstrVetorEmailVB6[4];

                    lstrPathAnexo = pstrVetorEmailVB6[5];
                    lstrEmailRemetente = pstrVetorEmailVB6[6];
                    lstrPathTemplateCorpoMSG = pstrVetorEmailVB6[7];
                    lstrCorpoEmail = pstrVetorEmailVB6[8];

                    //Login para Exchange 
                    lstrUser = pstrVetorEmailVB6[9];
                    lstrPwdDesc = pstrVetorEmailVB6[10];

                    lintAutentica = int.Parse(pstrVetorEmailVB6[13]);


                    //No CIR2000 não tem esse parametro, por isso é sempre true 
                    //lbSsl = true;

                    if (!string.IsNullOrEmpty(lstrPathTemplateCorpoMSG))
                    {
                        using (StreamReader streamReader = new StreamReader(lstrPathTemplateCorpoMSG, Encoding.UTF8))
                        {
                            lstrCorpoEmail = streamReader.ReadToEnd();
                        }
                    }

                    lstrCorpoEmail = lstrCorpoEmail.Replace("##", ";");

                    if (lstrSsl == "")
                    {
                        lbSsl = false;
                    }
                    else
                    {
                        lbSsl = bool.Parse(lstrSsl);
                    }

                    if (lstrUser.Trim() == "")
                    {
                        lstrUser = lstrEmailRemetente;
                    }

                }
                else if (ObjParamBoleto != null) //Quando for diferente de NULL é a Rotina de Boleto chamando
                {

                    lstrDestinatario = pstrDestinatario; 

                    //Trata SSL
                    if (!string.IsNullOrEmpty(ObjParamBoleto.ST_EMAIL_SSL))
                    {
                        if (ObjParamBoleto.ST_EMAIL_SSL.Trim() == "0" )
                        {
                            lbSsl = false;
                        }
                        else
                        {
                            lbSsl = true;
                        }
                        
                    }


                    lintAutentica = ObjParamBoleto.ST_REQ_AUTENT_EMAIL_BOL;


                    if (!string.IsNullOrEmpty(ObjParamBoleto.DS_EMAIL_PADRAO_BOL_WEB))
                    {
                        lstrEmailRemetente = ObjParamBoleto.DS_EMAIL_PADRAO_BOL_WEB;
                    }

                    if (!string.IsNullOrEmpty(ObjParamBoleto.DS_ASSUNTO_EMAIL_BOL))
                    {
                        lstrSubject = ObjParamBoleto.DS_ASSUNTO_EMAIL_BOL;
                    }

                    if (!string.IsNullOrEmpty(ObjParamBoleto.DS_SERV_SMTP_BOL_WEB))
                    {
                        lstrHost = ObjParamBoleto.DS_SERV_SMTP_BOL_WEB;
                    }

                    if (!string.IsNullOrEmpty(ObjParamBoleto.DS_EMAIL_LOGIN_BOL_WEB))
                    {
                        lstrUser = ObjParamBoleto.DS_EMAIL_LOGIN_BOL_WEB;
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = "User mail não parametrizado.";
                        return;
                    }

                    if (!string.IsNullOrEmpty(ObjParamBoleto.DS_SENHA_EMAIL_PADRAO_BOL_WEB))
                    {
                        lstrPwdDesc = ObjParamBoleto.DS_SENHA_EMAIL_PADRAO_BOL_WEB;
                    }

                    if (!string.IsNullOrEmpty(ObjParamBoleto.NU_PORTA_SMTP_BOL_WEB))
                    {
                        lstrPorta = ObjParamBoleto.NU_PORTA_SMTP_BOL_WEB;
                    }

                    if (!string.IsNullOrEmpty(ObjParamBoleto.DS_CAMINHO_MENSAGEM_EMAIL_BOL))
                    {
                        lstrPathTemplateCorpoMSG = ObjParamBoleto.DS_CAMINHO_MENSAGEM_EMAIL_BOL;

                        if (!File.Exists(lstrPathTemplateCorpoMSG))
                        {
                            _erro = -1;
                            _msgErro = "Arquivo de mensagem do email não localizado - " + lstrPathTemplateCorpoMSG;
                            return;
                        }

                        using (StreamReader streamReader = new StreamReader(ObjParamBoleto.DS_CAMINHO_MENSAGEM_EMAIL_BOL, Encoding.UTF8))
                        {
                            lstrCorpoEmail = streamReader.ReadToEnd();
                        }

                        if (lstrCorpoEmail.Trim() != "" )
                        {

                            if (ObjParamBoleto.DS_CAMINHO_IMAGEM_EMAIL_BOL != "")
                            {
                                lstrCorpoEmail = lstrCorpoEmail.Replace("<logo>", ObjParamBoleto.DS_CAMINHO_IMAGEM_EMAIL_BOL);
                            }
                           
                        }
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = "Caminho da mensagem do Email não parametrizado.";
                        return;
                    }


                }
                else if (pintRecuperaSenha == 1)
                {

                    Utility.SetLogXML("EnviarEmail", "ERRO", "Lendo paramentros", false);

                    lstrHost = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["HostMail"].ToString());
                    lstrPorta = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["PortaMail"].ToString());
                    lstrSsl = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["SslMail"].ToString());

                    lstrEmailRemetente = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["UserMailSeg"].ToString());
                    lstrPwd = ConfigurationManager.AppSettings["PwdMailSeg"].ToString();
                    lstrSubject = ConfigurationManager.AppSettings["SubjectMailSeg"].ToString();
                    lintAutentica = int.Parse(ConfigurationManager.AppSettings["RequerAutenticacao"].ToString());

                    lstrCorpoEmail = pstrMsg;
                    lstrDestinatario = pstrDestinatario;

                    //Para a loja o email remetente é o mesmo que  o login
                    lstrUser = lstrEmailRemetente;

                    if (lstrPwd == "")
                    {
                        _erro = -1;
                        _msgErro = "PwdMail não parametrizado.";
                        return;
                    }
                    else
                    {
                        lstrPwdDesc = ObjUtil.GetDesCript(lstrPwd);
                    }


                    if (lstrSsl == "")
                    {
                        _erro = -1;
                        _msgErro = "SSL não parametrizado.";
                        return;
                    }
                    else
                    {
                        lbSsl = bool.Parse(lstrSsl);
                    }

                } 
                else
                {
                    //Rotina do Site de venda chamando

                    lstrHost = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["HostMail"].ToString());
                    lstrPorta = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["PortaMail"].ToString());
                    lstrSsl = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["SslMail"].ToString());
                    lstrEmailRemetente = ObjUtil.GetDesCript(ConfigurationManager.AppSettings["UserMail"].ToString());
                    lstrPwd = ConfigurationManager.AppSettings["PwdMail"].ToString();
                    lstrSubject = ConfigurationManager.AppSettings["SubjectMail"].ToString();

                    lintAutentica = int.Parse(ConfigurationManager.AppSettings["RequerAutenticacao"].ToString());

                    lstrCorpoEmail = pstrMsg;
                    lstrDestinatario = pstrDestinatario; 

                    //Para a loja o email remetente é o mesmo que  o login
                    lstrUser = lstrEmailRemetente;

                    lintAutentica = int.Parse(ConfigurationManager.AppSettings["RequerAutenticacao"].ToString());

                    if (lstrPwd == "")
                    {
                        _erro = -1;
                        _msgErro = "PwdMail não parametrizado.";
                        return;
                    }
                    else
                    {
                        lstrPwdDesc = ObjUtil.GetDesCript(lstrPwd);
                    }


                    if (lstrSsl == "")
                    {
                        _erro = -1;
                        _msgErro = "SSL não parametrizado.";
                        return;
                    }
                    else
                    {
                        lbSsl = bool.Parse(lstrSsl);
                    }


                }

                //==================================================================
                //Validações 
                //==================================================================

                if (lstrHost == "")
                {
                    _erro = -1;
                    _msgErro = "Host não parametrizado.";
                    return;
                }

                if (lstrPorta == "")
                {
                    _erro = -1;
                    _msgErro = "Porta não parametrizada.";
                    return;
                }
                else
                {
                    lintPorta = int.Parse(lstrPorta);
                }

                if (lstrEmailRemetente == "")
                {
                    _erro = -1;
                    _msgErro = "Remetente não informado.";
                    return;
                }

                if (lstrCorpoEmail == "")
                {
                    _erro = -1;
                    _msgErro = "Corpo do e-mail não informado.";
                    return;
                }

                if (lstrSubject == "")
                {
                    _erro = -1;
                    _msgErro = "Assunto não informado.";
                    return;
                }

                if (lstrDestinatario == "")
                {
                    _erro = -1;
                    _msgErro = "Destinatário não informado.";
                    return;
                }

                if (lstrPwdDesc == "")
                {
                    _erro = -1;
                    _msgErro = "Senha de login não informado.";
                    return;
                }


                if (pstrCaminhoAnexo.Trim()  != "")
                {
                    lstrPathAnexo = pstrCaminhoAnexo;

                    if (!File.Exists(lstrPathAnexo))
                    {
                        _erro = -1;
                        _msgErro = "Arquivo informado para anexo não existe";
                        return;
                    }
                }


                //==================================================================

                Utility.SetLogXML("EnviarEmail", "ERRO", "Preparando Envio", false);

                SmtpClient client = new SmtpClient();

                client.Host = lstrHost;
                client.EnableSsl = lbSsl;
                client.Port = lintPorta;

                if (lintAutentica == 1)
                {
                    client.Credentials = new System.Net.NetworkCredential(lstrUser, lstrPwdDesc);
                }
                

                MailMessage mail = new MailMessage();

                mail.Sender = new System.Net.Mail.MailAddress(lstrEmailRemetente, "");
                mail.From = new MailAddress(lstrEmailRemetente, "");
                mail.To.Add(new MailAddress(lstrDestinatario));

                mail.Subject = lstrSubject;
                mail.Body = lstrCorpoEmail;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                
                if (lstrPathAnexo.Trim() != "" )
                {
                    mail.Attachments.Add(new Attachment(lstrPathAnexo));
                }

                client.Send(mail);
                
                mail.Dispose();
                client.Dispose(); 

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                Utility.SetLogXML("EmailConfirmação", "ERRO", ex.Message + " - " + ex.InnerException, false);
            }
        
        }



        public void EnviarAPI(string pstrMsg,
                           string pstrDestinatario,
                           CWA.Venda.Entity.BoletoParametroEntity ObjParamBoleto = null,
                           string pstrCaminhoAnexo = "",
                           int pintRecuperaSenha = 0)
        {

            _erro = 0;
            _msgErro = "";


            try
            {

                if (ObjParamBoleto == null)
                {
                    _erro = -1;
                    _msgErro = "Dados de copnfiguraão de envio de e-mail não esta correto.";

                    return; 
                }

                string lstrUrlBasePOST = "";
                string lstrAPIKey = "";
                string lstrDomain = "";
                string lstrFromMail = "";
                string lstrToMail = "";
                string lstrSubject = "";
                string lstrText = "";
                string lstrPath = "";
                string lstrFileAnexo = "";
                string lstrPathFileAnexo = "";
                int lintTipoCorpoEmail = 0;

                string lstrEndPointCWA = "";
                string lstrTipoEndPointCWA = "";
                string[] lstrVetorListParms;

                if (!string.IsNullOrEmpty(ObjParamBoleto.NM_LISTA_PARMS))
                {
                    lstrVetorListParms = ObjParamBoleto.NM_LISTA_PARMS.Split(';');

                    if (lstrVetorListParms != null)
                    {
                        //=====================================================================================
                        //domaim        remetente           destinatario   assunto   corpo        formato 
                        //  0               1                    2            3         4            5
                        //=====================================================================================  
                        //mg.jc.com.br; noreply @jc.com.br; @DESTINATARIO; @ASSUNTO; @CORPOEMAIL; html
                        //=====================================================================================

                        lstrDomain = lstrVetorListParms[0];
                        lstrFromMail = lstrVetorListParms[1];


                        if (!string.IsNullOrEmpty(pstrDestinatario))
                        {
                            lstrToMail = pstrDestinatario;
                        }
                        else
                        {
                            lstrToMail = lstrVetorListParms[2];
                        }

                        if (!string.IsNullOrEmpty(ObjParamBoleto.DS_ASSUNTO_EMAIL_BOL))
                        {
                            lstrSubject = ObjParamBoleto.DS_ASSUNTO_EMAIL_BOL;
                        }
                        else
                        {
                            lstrSubject = lstrVetorListParms[3];
                        }

                        //Utility.SetLogXML("EnviarEmail-API", "Log", "Iniciando Leitura do corpo - " + ObjParamBoleto.DS_CAMINHO_MENSAGEM_EMAIL_BOL, false);

                        if (!string.IsNullOrEmpty(ObjParamBoleto.DS_CAMINHO_MENSAGEM_EMAIL_BOL))
                        {
                            if (File.Exists(ObjParamBoleto.DS_CAMINHO_MENSAGEM_EMAIL_BOL))
                            {
                                using (StreamReader streamReader = new StreamReader(ObjParamBoleto.DS_CAMINHO_MENSAGEM_EMAIL_BOL, Encoding.UTF8))
                                {
                                    lstrText = streamReader.ReadToEnd();
                                }
                            }

                        }
                        else
                        {
                            lstrText = lstrVetorListParms[4];
                        }


                        if (lstrVetorListParms[5].Trim() != "")
                        {
                            if (lstrVetorListParms[5].ToUpper() == "HTML")
                            {
                                lintTipoCorpoEmail = 1;
                            }
                            else
                            {
                                lintTipoCorpoEmail = 2;
                            }

                        }
                        else
                        {
                            _erro = -1;
                            _msgErro = "Tipo do Corpo do e-mail não informado.";
                            return;
                        }

                    }
                }


                lstrTipoEndPointCWA = ObjParamBoleto.NM_TP_ENDPOINT_CWA;
                lstrEndPointCWA = ObjParamBoleto.NM_ENDPOINT_CWA;


                if (string.IsNullOrEmpty(ObjParamBoleto.NM_ENDPOINT_CLIENT))
                {
                    _erro = -1;
                    _msgErro = "Url post não informada.";

                    return;
                }
                else
                {
                    lstrUrlBasePOST = ObjParamBoleto.NM_ENDPOINT_CLIENT;
                }

                if (string.IsNullOrEmpty(ObjParamBoleto.NM_MERCHANTKEY_ENDPOINT_CLIENT))
                {
                    _erro = -1;
                    _msgErro = "Key da API não informado.";

                    return;
                }
                else
                {
                    lstrAPIKey = ObjParamBoleto.NM_MERCHANTKEY_ENDPOINT_CLIENT;
                }

                if (lstrDomain == "")
                {
                    _erro = -1;
                    _msgErro = "Domain não informado.";

                    return;
                }

                if (lstrFromMail == "")
                {
                    _erro = -1;
                    _msgErro = "Remetente não informado.";

                    return;
                }

                if (lstrToMail == "")
                {
                    _erro = -1;
                    _msgErro = "Destinatário não informado.";

                    return;
                }


                if (lstrSubject == "")
                {
                    _erro = -1;
                    _msgErro = "Assunto não informado.";

                    return;
                }

                if (lstrText == "")
                {
                    _erro = -1;
                    _msgErro = "Corpo do e-mail não informado.";

                    return;
                }

                
                if (! string.IsNullOrEmpty(pstrCaminhoAnexo))
                {

                    lstrPathFileAnexo = pstrCaminhoAnexo;

                    if (!File.Exists(lstrPathFileAnexo))
                    {
                        _erro = -1;
                        _msgErro = "Arquivo informado para o anexo não localizado.";
                        return;
                    }

                    FileInfo ObjFile = new FileInfo(lstrPathFileAnexo);

                    lstrFileAnexo = ObjFile.Name;
                    lstrPath = ObjFile.DirectoryName; 

                }

                //Utility.SetLogXML("EnviarEmail-API", "Log", "EndPoint CWA - " + lstrTipoEndPointCWA, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", "EndPoint CWA - " + lstrEndPointCWA, false);

                CWAMailAPIService.IServiceCWA ObjService = ServiceClientCWAAPIMail.GetServiceClintEndPoint(lstrTipoEndPointCWA, lstrEndPointCWA);

                if (ObjService == null)
                {
                    _erro = ServiceClientCWAAPIMail.Erro;
                    _msgErro = ServiceClientCWAAPIMail.MsgErro;

                    return;

                }

                
                //Utility.SetLogXML("EnviarEmail-API", "Log", "Chamdno WS API", false);

                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrUrlBasePOST, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrAPIKey, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrDomain, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrFromMail, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrToMail, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrSubject, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrText, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lintTipoCorpoEmail.ToString(), false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrPath, false);
                //Utility.SetLogXML("EnviarEmail-API", "Log", lstrFileAnexo, false);

                string lstrRet = ObjService.SendMailAPI(lstrUrlBasePOST,
                                                        lstrAPIKey,
                                                        lstrDomain,
                                                        lstrFromMail,
                                                        lstrToMail,
                                                        lstrSubject,
                                                        lstrText,
                                                        lintTipoCorpoEmail,
                                                        lstrPath,
                                                        lstrFileAnexo);



                if (lstrRet.Trim() == "")
                {
                    _erro = -1;
                    _msgErro = "EnviarMailAPI - Vetor de retonor vazio";
                    return;
                }

                Utility.SetLogXML("EnviarEmail-API", "Log", lstrRet, false);

                string[] lstrVetRet;
                lstrVetRet = lstrRet.Split(';');

                if (lstrVetRet != null)
                {
                    if (lstrVetRet[0] == "0")
                    {
                        if (lstrVetRet[2] == "OK")
                        {
                            _erro = 0;
                            _msgErro = "OK";
                        }
                        else
                        {
                            _erro = -1;
                            _msgErro = lstrVetRet[3];
                        }
                    }
                    else
                    {
                        _erro = -1;
                        _msgErro = lstrVetRet[1];
                    }

                }
                else
                {
                    _erro = -1;
                    _msgErro = "Erro interno da API";
                }

                return;

            }
            catch (Exception ex)
            {
                _erro = -1;
                _msgErro = ex.Message;

                Utility.SetLogXML("EnviarMailAPI", "ERRO", ex.Message + " - " + ex.InnerException, false);
            }

        }

    }
}
