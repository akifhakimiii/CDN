using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.DTOs;
using FreelanceApp.Models;
using Npgsql.Replication;

namespace FreelanceApp.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User UserModel)
        {
            return new UserDto
            {
                Id = UserModel.Id,
                Username = UserModel.Username,
                Email = UserModel.Email,
                PhoneNumber = UserModel.PhoneNumber,
                Skillsets = UserModel.Skillsets,
                Hobby = UserModel.Hobby
            };
        }

        public static User ToUserFromCreateDto(this CreateUserDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Skillsets = userDto.Skillsets,
                Hobby = userDto.Hobby

            };
        }
    }

}