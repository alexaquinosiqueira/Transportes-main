using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transportadora.UI.Site.ViewModels
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }
        public Guid[] ActionIds { get; set; }
        virtual public IEnumerable<UserViewModel> Users { get; set; }
        public virtual ICollection<ProfileActionFuncionalityViewModel> ProfileActionFuncionalities { get; set; }

    }
}
