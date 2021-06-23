using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transportadora.Business.Models
{
    [Table("AuditLog")]
    public class AuditLog : Entity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }
        public DateTime DateTime { get; set; }
        public String Username { get; set; }
        [Required]
        [MaxLength(128)]
        public String TableName { get; set; }
        [Required]
        [MaxLength(50)]
        public String Action { get; set; }

        public String KeyValues { get; set; }
        public String OldValues { get; set; }
        public String NewValues { get; set; }
    }
}
