using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDatabase.Models;
using UserDatabase;
using UserDatabase.Migrations;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace UserRepository.Models
{
    public class UserDTO
    {
        private string password;
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPassWord
        {
            get
            {
                return password;
            }
            set
            {
                Salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(Salt);
                }
                password = hashPassword(value, Salt);
            }
        }
        public byte[] Salt { get; set; }
        public DateTime CreatedDate { get; private set; }
        //private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>().ReverseMap());
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>().ReverseMap().ForAllMembers(opts => opts.Condition(c => c != null)));

        private static IMapper mapper = config.CreateMapper();

        public static List<UserDTO> GetAll()
        {
            List<User> users = DatabaseManager.Instance.User.ToListAsync().Result;
            return users.Select(u => convertToUserDTO(u)).ToList();
        }
        public static UserDTO GetUserDTO(int id)
        {
            User user = DatabaseManager.Instance.User.FindAsync(id).Result;
            return convertToUserDTO(user);
        }
        private static UserDTO convertToUserDTO(User user)
        {
            return mapper.Map<User, UserDTO>(user);
        }
        private static User convertToUser(UserDTO userDTO)
        {
            User user = mapper.Map<UserDTO, User>(userDTO);
            if (user.CreatedDate == DateTime.MinValue)
            {
                user.CreatedDate = DateTime.Now;
            }
            return user;
        }
        public static async Task<int> Create(UserDTO userDTO)
        {
            if (!DatabaseManager.Instance.User.Any(u => u.UserEmail == userDTO.UserEmail))
            {
                var user = DatabaseManager.Instance.User.Add(convertToUser(userDTO));
                await DatabaseManager.Instance.SaveChangesAsync();
                return user.Entity.UserID;
            }
            return 0;
        }
        public static void Update(UserDTO userDTO)
        {
            User user = DatabaseManager.Instance.User.Find(userDTO.UserID);
            DatabaseManager.Instance.Entry(user).CurrentValues.SetValues(userDTO);
        }
        public static bool CheckPassword(string password, string userName)
        {
            var user = DatabaseManager.Instance.User.Where(u => u.UserEmail == userName).First();
            if (user == null)
            {
                return false;
            }
            return hashPassword(password, user.Salt) == user.UserPassWord;
        }
        private static string hashPassword(string pw, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: pw,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
