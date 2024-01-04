using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Security.Principal;

namespace DAL.Entities
{

    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Role { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
        public Borrower? Borrower { get; set; }
        public Librarian? Librarian { get; set; }
        public Accounter? Accounter { get; set; }


        //private string _passwordHash;
        //private string _passwordSalt;

        //public void SetPassword(string password)
        //{
        //    // Generate a random salt
        //    byte[] salt = new byte[16];
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        rng.GetBytes(salt);
        //    }

        //    // Hash the password with the salt using bcrypt
        //    using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
        //    {
        //        byte[] hash = pbkdf2.GetBytes(20); // 20 bytes for the hash
        //        _passwordSalt = Convert.ToBase64String(salt);
        //        _passwordHash = Convert.ToBase64String(hash);
        //    }
        //}

        //public bool VerifyPassword(string password)
        //{
        //    // Verify the password by hashing the provided password with the stored salt
        //    using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(_passwordSalt), 10000))
        //    {
        //        byte[] hashToCheck = pbkdf2.GetBytes(20); // 20 bytes for the hash
        //        return Convert.ToBase64String(hashToCheck) == _passwordHash;
        //    }
        //}
    }
}
