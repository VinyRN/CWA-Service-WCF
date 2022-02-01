using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class DadosPessoaEntity
    {

        private int _TP_PESSOA;
        private string _DS_NOME;
        private string _DS_EMAIL;
        private string _NU_CPF;
        private string _DT_NASC;
        private string _NU_DDDTEL;
        private string _NU_TEL;
        private string _NU_DDDCEL;
        private string _NU_CEL;
        private string _DS_SENHA;
        private string _DS_SENHA_DESCRIPT;

        private int _ST_AUTORIZA_EMAIL;
        private int _ST_IND_DIVULGACAO;
        private int _CD_REPR_VENDA;
        private int _CD_VENDEDOR;
        private int _CD_LOCAL_ENTREGA;
        private int _CD_GRUPO_SELECAO;

        private string _DS_SOBRE_NOME;

        private string _DS_NOME_EMPRESA;
        private string _DS_NOME_RESP;
        private string _NU_CPF_RESP;
        private string _NM_FANTASIA;
        private string _NU_CNPJ;
        private string _NU_IE;
        private string _NU_IM;
        private int _CD_RAMO_ATV;

        private int _ALTERAR_DADOS;

        private int _ST_TP_PARC_CARTAO;

        public int TP_PESSOA
        {
            get { return _TP_PESSOA; }
            set { _TP_PESSOA = value; }
        }

        public string DS_NOME
        {
            get { return _DS_NOME; }
            set { _DS_NOME = value; }
        }

        public string DS_EMAIL
        {
            get { return _DS_EMAIL; }
            set { _DS_EMAIL = value; }
        }

        public string NU_CPF
        {
            get { return _NU_CPF; }
            set { _NU_CPF = value; }
        }

        public string DT_NASC
        {
            get { return _DT_NASC; }
            set { _DT_NASC = value; }
        }

        public string NU_DDDTEL
        {
            get { return _NU_DDDTEL; }
            set { _NU_DDDTEL = value; }
        }

        public string NU_TEL
        {
            get { return _NU_TEL; }
            set { _NU_TEL = value; }
        }

        public string NU_DDDCEL
        {
            get { return _NU_DDDCEL; }
            set { _NU_DDDCEL = value; }
        }

        public string NU_CEL
        {
            get { return _NU_CEL; }
            set { _NU_CEL = value; }
        }

        public string DS_SENHA
        {
            get { return _DS_SENHA; }
            set { _DS_SENHA = value; }
        }

        public string DS_SENHA_DESCRIPT
        {
            get { return _DS_SENHA_DESCRIPT; }
            set { _DS_SENHA_DESCRIPT = value; }
        }

        public int ST_AUTORIZA_EMAIL
        {
            get { return _ST_AUTORIZA_EMAIL; }
            set { _ST_AUTORIZA_EMAIL = value; }
        }

        public int ST_IND_DIVULGACAO
        {
            get { return _ST_IND_DIVULGACAO; }
            set { _ST_IND_DIVULGACAO = value; }
        }

        public int CD_REPR_VENDA
        {
            get { return _CD_REPR_VENDA; }
            set { _CD_REPR_VENDA = value; }
        }

        public int CD_VENDEDOR
        {
            get { return _CD_VENDEDOR; }
            set { _CD_VENDEDOR = value; }
        }

        public int CD_LOCAL_ENTREGA
        {
            get { return _CD_LOCAL_ENTREGA; }
            set { _CD_LOCAL_ENTREGA = value; }
        }

        public int CD_GRUPO_SELECAO
        {
            get { return _CD_GRUPO_SELECAO; }
            set { _CD_GRUPO_SELECAO = value; }
        }

        public string DS_SOBRE_NOME
        {
            get { return _DS_SOBRE_NOME; }
            set { _DS_SOBRE_NOME = value; }
        }

        public string DS_NOME_EMPRESA
        {
            get { return _DS_NOME_EMPRESA; }
            set { _DS_NOME_EMPRESA = value; }
        }

        public string DS_NOME_RESP
        {
            get { return _DS_NOME_RESP; }
            set { _DS_NOME_RESP = value; }
        }

        public string NU_CPF_RESP
        {
            get { return _NU_CPF_RESP; }
            set { _NU_CPF_RESP = value; }
        }

        public string NM_FANTASIA
        {
            get { return _NM_FANTASIA; }
            set { _NM_FANTASIA = value; }
        }

        public string NU_CNPJ
        {
            get { return _NU_CNPJ; }
            set { _NU_CNPJ = value; }
        }

        public string NU_IE
        {
            get { return _NU_IE; }
            set { _NU_IE = value; }
        }

        public string NU_IM
        {
            get { return _NU_IM; }
            set { _NU_IM = value; }
        }

        public int CD_RAMO_ATV
        {
            get { return _CD_RAMO_ATV; }
            set { _CD_RAMO_ATV = value; }
        }


        public int ALTERAR_DADOS
        {
            get { return _ALTERAR_DADOS; }
            set { _ALTERAR_DADOS = value; }
        }

        public int TIPO_PARC_CARTAO
        {
            get { return _ST_TP_PARC_CARTAO; }
            set { _ST_TP_PARC_CARTAO = value; }
        }

    }
}
