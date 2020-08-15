using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace UserDatabase.Models
{
    public class User
    {
        byte[] salt;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [EmailAddress]
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassWord { get; set; }
        [Required]
        private byte[] Salt { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
