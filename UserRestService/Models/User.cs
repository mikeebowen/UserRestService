﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRepository.Models;

namespace UserRestService.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public DateTime CreatedDate { get; set; }
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
        private static UserDTO convertToUserDTO(User user)
        {
            return mapper.Map<User, UserDTO>(user);
        }
        public static async Task<int> Create(JObject jObject)
        {
            UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(jObject.ToString());
            int id = await UserDTO.Create(userDTO);
            return id;
        }
        public static void Update(JObject jObject, int id)
        {
            UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(jObject.ToString());
            userDTO.UserID = id;
            UserDTO.Update(userDTO);
        }
        public static async Task<bool> DeleteUser(string id)
        {
            if (int.TryParse(id, out int intId))
            {
                return await UserDTO.DeleteUser(intId);
            }
            return false;
        }
        public static bool CheckPassword(string password, string userEmail)
        {
            return UserDTO.CheckPassword(password, userEmail);
        }
    }
}
