using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserDatabase.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
        public string UserPassWord { get; set; }
        public DateTime CreatedDAte { get; private set; }
    }
}
