using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDatabase.Models;

namespace UserRepository.Models
{
    public class UserDTO
    {
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
    }
}
