using System.ComponentModel.DataAnnotations;

namespace AdminDashBoardV1._0._0.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Name IS Requierd")]
        [StringLength(256, ErrorMessage = "Name Max Length Is 256 Char")]
        public string Name { get; set; }
    }
}
