using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class EnderecoProdutoAgregEntity
    {
        int _ST_TP_ENDERECO;
        int _CD_LOGRADOURO;
        int _NU_RESIDENCIA;
        string _COMPL_RESIDENCIA;
        string _NU_BLOCO;
        string _NU_APARTAMENTO;
        string _DS_COMPLEMENTO;
        string _DS_PONTO_REF;
        int _CD_LOCAL_ENTREGA;
        string _NU_CEP;
        string _DS_ENDERECO_EXT;
        string _DS_BAIRRO_EXT;
        string _DS_MUNICIPIO_EXT;
        string _DS_UF_EXT;
        string _NU_CEP_EXT;

        public int ST_TP_ENDERECO
        {
            get { return _ST_TP_ENDERECO; }
            set { _ST_TP_ENDERECO = value; }
        }
        public int CD_LOGRADOURO
        {
            get { return _CD_LOGRADOURO; }
            set { _CD_LOGRADOURO = value; }
        }
        public int NU_RESIDENCIA
        {
            get { return _NU_RESIDENCIA; }
            set { _NU_RESIDENCIA = value; }
        }
        public string COMPL_RESIDENCIA
        {
            get { return _COMPL_RESIDENCIA; }
            set { _COMPL_RESIDENCIA = value; }
        }
        public string NU_BLOCO
        {
            get { return _NU_BLOCO; }
            set { _NU_BLOCO = value; }
        }
        public string NU_APARTAMENTO
        {
            get { return _NU_APARTAMENTO; }
            set { _NU_APARTAMENTO = value; }
        }
        public string DS_COMPLEMENTO
        {
            get { return _DS_COMPLEMENTO; }
            set { _DS_COMPLEMENTO = value; }
        }
        public string DS_PONTO_REF
        {
            get { return _DS_PONTO_REF; }
            set { _DS_PONTO_REF = value; }
        }
        public int CD_LOCAL_ENTREGA
        {
            get { return _CD_LOCAL_ENTREGA; }
            set { _CD_LOCAL_ENTREGA = value; }
        }
        public string NU_CEP
        {
            get { return _NU_CEP; }
            set { _NU_CEP = value; }
        }
        public string DS_ENDERECO_EXT
        {
            get { return _DS_ENDERECO_EXT; }
            set { _DS_ENDERECO_EXT = value; }
        }
        public string DS_BAIRRO_EXT
        {
            get { return _DS_BAIRRO_EXT; }
            set { _DS_BAIRRO_EXT = value; }
        }
        public string DS_MUNICIPIO_EXT
        {
            get { return _DS_MUNICIPIO_EXT; }
            set { _DS_MUNICIPIO_EXT = value; }
        }
        public string DS_UF_EXT
        {
            get { return _DS_UF_EXT; }
            set { _DS_UF_EXT = value; }
        }
        public string NU_CEP_EXT
        {
            get { return _NU_CEP_EXT; }
            set { _NU_CEP_EXT = value; }
        }

        
    }
}
