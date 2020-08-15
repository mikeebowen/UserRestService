using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDatabase.Models;
using UserDatabase;

namespace UserRepository.Models
{
    public class UserDTO
    {
        public UserDTO()
        {
            CreatedDate = DateTime.Now;
        }
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserPassWord { get; set; }
        public DateTime CreatedDate { get; private set; }
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>().ReverseMap());
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
            if (user.CreatedDate == null)
            {
                user.CreatedDate = DateTime.UtcNow;
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
    }
}
