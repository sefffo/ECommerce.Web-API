using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presistence.Identity.Models
{
    public class Address
    {


        [Key]
        public int Id { get; set; }


        //3shan lw el order hyb2a b asm meen mmokn agebha direct mn el id aw y7ot 7d mokhtlf 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;



        //relation 
        public ApplicationUser? User { get; set; }
        [ForeignKey("User")]
        public string userId { get; set; }

    }
}
