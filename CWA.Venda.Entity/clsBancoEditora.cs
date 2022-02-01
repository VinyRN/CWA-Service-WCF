using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Venda.Entity
{
    public class BancoEditoraEntity
    {

        private int _CODIGO_BANCO;
        private string _NU_AGENCIA_BOL_ED;
        private string _NU_DV_AGENCIA;

        private string _NU_CONTA_BOL_ED;
        private string _NU_DV_CONTA;

        private int _NU_LAYOUT_BOL_ED;
        private string _NU_CARTEIRA_ED;
        private string _NU_CONVENIO_BOL_ED;
        private string _DS_DIR_ENVIO_BOL_ED;

        private string _DS_ASS_BOLETO_GERAL_BOL_ED;
        private string _DS_ASS_BOLETO_MSG1_BOL_ED;
        private string _DS_ASS_BOLETO_MSG2_BOL_ED;
        private string _DS_ASS_BOLETO_MSG3_BOL_ED;
        private string _DS_ASS_BOLETO_MSG4_BOL_ED;
        private string _DS_ASS_BOLETO_MSG5_BOL_ED;
        private string _DS_ASS_BOLETO_MSG6_BOL_ED;
        private string _DS_ASS_BOLETO_MSG7_BOL_ED;

        private int _NU_BOL_TIPO_REGISTRO_ED;
        private string _NM_BENEFICIARIO;
        private string _NU_CNPJ_BENEFICIARIO;

        private string _NM_LOCAL_PAGAMENTO;

        private string _DS_ENDERECO;

        public int CODIGO_BANCO
        {
            get { return _CODIGO_BANCO; }
            set { _CODIGO_BANCO = value; }
        }

        public string NU_AGENCIA_BOL_ED
        {
            get { return _NU_AGENCIA_BOL_ED; }
            set { _NU_AGENCIA_BOL_ED = value; }
        }

        public string NU_DV_AGENCIA
        {
            get { return _NU_DV_AGENCIA; }
            set { _NU_DV_AGENCIA = value; }
        }
        
        public string NU_CONTA_BOL_ED
        {
            get { return _NU_CONTA_BOL_ED; }
            set { _NU_CONTA_BOL_ED = value; }
        }

        public string NU_DV_CONTA
        {
            get { return _NU_DV_CONTA; }
            set { _NU_DV_CONTA = value; }
        }

        public int NU_LAYOUT_BOL_ED
        {
            get { return _NU_LAYOUT_BOL_ED; }
            set { _NU_LAYOUT_BOL_ED = value; }
        }

        public string NU_CARTEIRA_ED
        {
            get { return _NU_CARTEIRA_ED; }
            set { _NU_CARTEIRA_ED = value; }
        }

        public string NU_CONVENIO_BOL_ED
        {
            get { return _NU_CONVENIO_BOL_ED; }
            set { _NU_CONVENIO_BOL_ED = value; }
        }

        public string DS_DIR_ENVIO_BOL_ED
        {
            get { return _DS_DIR_ENVIO_BOL_ED; }
            set { _DS_DIR_ENVIO_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_GERAL_BOL_ED
        {
            get { return _DS_ASS_BOLETO_GERAL_BOL_ED; }
            set { _DS_ASS_BOLETO_GERAL_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_MSG1_BOL_ED
        {
            get { return _DS_ASS_BOLETO_MSG1_BOL_ED; }
            set { _DS_ASS_BOLETO_MSG1_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_MSG2_BOL_ED
        {
            get { return _DS_ASS_BOLETO_MSG2_BOL_ED; }
            set { _DS_ASS_BOLETO_MSG2_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_MSG3_BOL_ED
        {
            get { return _DS_ASS_BOLETO_MSG3_BOL_ED; }
            set { _DS_ASS_BOLETO_MSG3_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_MSG4_BOL_ED
        {
            get { return _DS_ASS_BOLETO_MSG4_BOL_ED; }
            set { _DS_ASS_BOLETO_MSG4_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_MSG5_BOL_ED
        {
            get { return _DS_ASS_BOLETO_MSG5_BOL_ED; }
            set { _DS_ASS_BOLETO_MSG5_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_MSG6_BOL_ED
        {
            get { return _DS_ASS_BOLETO_MSG6_BOL_ED; }
            set { _DS_ASS_BOLETO_MSG6_BOL_ED = value; }
        }

        public string DS_ASS_BOLETO_MSG7_BOL_ED
        {
            get { return _DS_ASS_BOLETO_MSG7_BOL_ED; }
            set { _DS_ASS_BOLETO_MSG7_BOL_ED = value; }
        }

        public int NU_BOL_TIPO_REGISTRO_ED
        {
            get { return _NU_BOL_TIPO_REGISTRO_ED; }
            set { _NU_BOL_TIPO_REGISTRO_ED = value; }
        }

        public string NM_BENEFICIARIO
        {
            get { return _NM_BENEFICIARIO; }
            set { _NM_BENEFICIARIO = value; }
        }

        public string NU_CNPJ_BENEFICIARIO
        {
            get { return _NU_CNPJ_BENEFICIARIO; }
            set { _NU_CNPJ_BENEFICIARIO = value; }
        }

        public string NM_LOCAL_PAGAMENTO
        {
            get { return _NM_LOCAL_PAGAMENTO; }
            set { _NM_LOCAL_PAGAMENTO = value; }
        }

        public string DS_ENDERECO
        {
            get { return _DS_ENDERECO; }
            set { _DS_ENDERECO = value; }
        }
        
    }
}
