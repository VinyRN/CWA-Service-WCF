using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWA.Central.Entity
{
    public class UsuarioCentralEntity
    {
        public virtual string NM_PESSOA { get; set; }
        public virtual string DS_EMAIL { get; set; }
        public virtual string DS_SENHA { get; set; }
        public virtual int? CD_CONTABIL_PESSOA { get; set; }
        public virtual long? NU_CPF { get; set; }
        public virtual long? NU_CNPJ { get; set; }

        public virtual string DS_SERV_SMTP_BOL_WEB { get; set; }
        public virtual string DS_EMAIL_PADRAO_BOL_WEB { get; set; }
        public virtual string DS_SENHA_EMAIL_PADRAO_BOL_WEB { get; set; }
        public virtual int? NU_PORTA_SMTP_BOL_WEB { get; set; }
        public virtual string DS_EMAIL_LOGIN_BOL_WEB { get; set; }

        public virtual string NU_SERIE_CTR { get; set; }
        public virtual int? NU_CTR { get; set; }
        public virtual int? NU_DV_CTR { get; set; }
        public virtual Int16 CD_PRODUTO { get; set; }
        public virtual Int16 NU_PERIODO_ATUAL { get; set; }

        public virtual string NU_TEL_CONTATO { get; set; }
        public virtual string DS_MSG_ERRO { get; set; }
        public virtual string DS_MSG_SUCESSO { get; set; }

        public virtual int ST_IND_RENOVA { get; set; }

        public virtual byte CD_TP_PAGAMENTO { get; set; }

        public virtual int CD_LOCAL_PAG { get; set; }
        public virtual int ST_TP_PARC_CARTAO { get; set; }

        public virtual int ST_ESTADO_ATUAL { get; set; }

        public virtual int CD_TP_PAGAMENTO_PLANO { get; set; }

        public virtual int ST_STATUS_ACESSO { get; set; }
    }
}
