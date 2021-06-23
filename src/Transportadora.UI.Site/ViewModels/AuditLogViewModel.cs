using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class AuditLogViewModel
    {
        public long Id { get; set; }
        [DisplayName("Data")]
        public DateTime DateTime { get; set; }
        [DisplayName("Usuário")]
        public String Username { get; set; }
        [DisplayName("Tabela")]
        public String TableName { get; set; }
        [DisplayName("Tipo da Ação")]
        public String Action { get; set; }

        public String KeyValues { get; set; }
        [DisplayName("Antigo Registro")]
        public String OldValues { get; set; }
        [DisplayName("Novo Registro")]
        public String NewValues { get; set; }
    }
}
