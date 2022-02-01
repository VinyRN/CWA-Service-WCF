using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class BoletoParametroEntity
    {

        //#########################################################
        //Campos de envio de email .NETMAIL
        //#########################################################
        private string _DS_EMAIL_PADRAO_BOL_WEB;
        private string _DS_SERV_SMTP_BOL_WEB;
        private string _DS_SENHA_EMAIL_PADRAO_BOL_WEB;
        private string _DS_EMAIL_LOGIN_BOL_WEB;
        private string _NU_PORTA_SMTP_BOL_WEB;
        private string _DS_CAMINHO_BOLETO;
        private string _DS_CAMINHO_BOLETO_EMAIL;
        private string _DS_ASSUNTO_EMAIL_BOL;
        private string _DS_CAMINHO_IMAGEM_EMAIL_BOL;
        private string _DS_CAMINHO_MENSAGEM_EMAIL_BOL;
        private string _ST_EMAIL_SSL;
        //#########################################################

        //#########################################################
        //Campos de envio de email .API
        //#########################################################

        private string _NM_INTERFACE_CWA;
        private string _NM_METODO_INTEFACE_CWA;
        private string _NM_ENDPOINT_CWA;
        private string _NM_TP_ENDPOINT_CWA;
        private string _NM_ENDPOINT_CLIENT;
        private string _NM_TP_ENDPOINT_CLIENT;
        private string _NM_METODO_ENDPOINT_CLIENT;
        private string _NM_AMBIENTE_ENDPOINT_CLIENT;
        private string _NM_CONTENTTYPE_ENDPOINT_CLIENT;
        private string _NM_MERCHANTID_ENDPOINT_CLIENT;
        private string _NM_MERCHANTKEY_ENDPOINT_CLIENT;
        private string _NM_LISTA_PARMS;

        private int _ST_REQ_AUTENT_EMAIL_BOL;
        private int _ST_REQ_AUTENT_EMAIL_WF;

        //#########################################################

        public string DS_CAMINHO_MENSAGEM_EMAIL_BOL
        {
            get { return _DS_CAMINHO_MENSAGEM_EMAIL_BOL; }
            set { _DS_CAMINHO_MENSAGEM_EMAIL_BOL = value; }
        }

        public string DS_CAMINHO_IMAGEM_EMAIL_BOL
        {
            get { return _DS_CAMINHO_IMAGEM_EMAIL_BOL; }
            set { _DS_CAMINHO_IMAGEM_EMAIL_BOL = value; }
        }

        public string DS_ASSUNTO_EMAIL_BOL
        {
            get { return _DS_ASSUNTO_EMAIL_BOL; }
            set { _DS_ASSUNTO_EMAIL_BOL = value; }
        }

        public string DS_CAMINHO_BOLETO_EMAIL
        {
            get { return _DS_CAMINHO_BOLETO_EMAIL; }
            set { _DS_CAMINHO_BOLETO_EMAIL = value; }
        }

        public string DS_CAMINHO_BOLETO
        {
            get { return _DS_CAMINHO_BOLETO; }
            set { _DS_CAMINHO_BOLETO = value; }
        }

        public string NU_PORTA_SMTP_BOL_WEB
        {
            get { return _NU_PORTA_SMTP_BOL_WEB; }
            set { _NU_PORTA_SMTP_BOL_WEB = value; }
        }

        public string DS_EMAIL_LOGIN_BOL_WEB
        {
            get { return _DS_EMAIL_LOGIN_BOL_WEB; }
            set { _DS_EMAIL_LOGIN_BOL_WEB = value; }
        }

        public string DS_SENHA_EMAIL_PADRAO_BOL_WEB
        {
            get { return _DS_SENHA_EMAIL_PADRAO_BOL_WEB; }
            set { _DS_SENHA_EMAIL_PADRAO_BOL_WEB = value; }
        }

        public string DS_EMAIL_PADRAO_BOL_WEB
        {
            get { return _DS_EMAIL_PADRAO_BOL_WEB; }
            set { _DS_EMAIL_PADRAO_BOL_WEB = value; }
        }

        public string DS_SERV_SMTP_BOL_WEB
        {
            get { return _DS_SERV_SMTP_BOL_WEB; }
            set { _DS_SERV_SMTP_BOL_WEB = value; }
        }

        public string ST_EMAIL_SSL
        {
            get { return _ST_EMAIL_SSL; }
            set { _ST_EMAIL_SSL = value; }
        }


        public string NM_INTERFACE_CWA
        {
            get { return _NM_INTERFACE_CWA; }
            set { _NM_INTERFACE_CWA = value; }
        }

        public string NM_METODO_INTEFACE_CWA
        {
            get { return _NM_METODO_INTEFACE_CWA; }
            set { _NM_METODO_INTEFACE_CWA = value; }
        }

        public string NM_ENDPOINT_CWA
        {
            get { return _NM_ENDPOINT_CWA; }
            set { _NM_ENDPOINT_CWA = value; }
        }

        public string NM_TP_ENDPOINT_CWA
        {
            get { return _NM_TP_ENDPOINT_CWA; }
            set { _NM_TP_ENDPOINT_CWA = value; }
        }

        public string NM_ENDPOINT_CLIENT
        {
            get { return _NM_ENDPOINT_CLIENT; }
            set { _NM_ENDPOINT_CLIENT = value; }
        }

        public string NM_TP_ENDPOINT_CLIENT
        {
            get { return _NM_TP_ENDPOINT_CLIENT; }
            set { _NM_TP_ENDPOINT_CLIENT = value; }
        }

        public string NM_METODO_ENDPOINT_CLIENT
        {
            get { return _NM_METODO_ENDPOINT_CLIENT; }
            set { _NM_METODO_ENDPOINT_CLIENT = value; }
        }

        public string NM_AMBIENTE_ENDPOINT_CLIENT
        {
            get { return _NM_AMBIENTE_ENDPOINT_CLIENT; }
            set { _NM_AMBIENTE_ENDPOINT_CLIENT = value; }
        }

        public string NM_CONTENTTYPE_ENDPOINT_CLIENT
        {
            get { return _NM_CONTENTTYPE_ENDPOINT_CLIENT; }
            set { _NM_CONTENTTYPE_ENDPOINT_CLIENT = value; }
        }

        public string NM_MERCHANTID_ENDPOINT_CLIENT
        {
            get { return _NM_MERCHANTID_ENDPOINT_CLIENT; }
            set { _NM_MERCHANTID_ENDPOINT_CLIENT = value; }
        }

        public string NM_MERCHANTKEY_ENDPOINT_CLIENT
        {
            get { return _NM_MERCHANTKEY_ENDPOINT_CLIENT; }
            set { _NM_MERCHANTKEY_ENDPOINT_CLIENT = value; }
        }

        public string NM_LISTA_PARMS
        {
            get { return _NM_LISTA_PARMS; }
            set { _NM_LISTA_PARMS = value; }
        }

        public int ST_REQ_AUTENT_EMAIL_BOL
        {
            get { return _ST_REQ_AUTENT_EMAIL_BOL; }
            set { _ST_REQ_AUTENT_EMAIL_BOL = value; }
        }

        public int ST_REQ_AUTENT_EMAIL_WF
        {
            get { return _ST_REQ_AUTENT_EMAIL_WF; }
            set { _ST_REQ_AUTENT_EMAIL_WF = value; }
        }



    }
}
