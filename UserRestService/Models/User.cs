using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UserRepository.Models;

namespace UserRestService.Models
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
        private static MapperConfiguration config = new MapperConfiguration(c => c.CreateMap<User, UserDTO>().ReverseMap());
        private static IMapper mapper = config.CreateMapper();
        public static IEnumerable<User> GetAll()
        {
            List<UserDTO> userDTOs = UserDTO.GetAll();
            return userDTOs.Select(userDTO => convertToUser(userDTO));
        }
        public static User GetUser(int id)
        {
            UserDTO userDTO = UserDTO.GetUserDTO(id);
            return convertToUser(userDTO);
        }
        private static User convertToUser(UserDTO userDTO)
        {
            return mapper.Map<UserDTO, User>(userDTO);
        }
    }
}
