using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DefaultValue("getutcdate()")]
        public DateTime CreatedDate { get; set; }
    }
}
