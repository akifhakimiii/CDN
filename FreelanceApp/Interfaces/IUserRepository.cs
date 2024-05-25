using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.DTOs;
using FreelanceApp.Helpers;
using FreelanceApp.Models;

namespace FreelanceApp.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers(QueryObject query);
        User? GetUserById(int id);
        User CreateUser(User userDto);
        User? UpdateUser(int id, UpdateUserDto userDto);
        User? GetUserByEmail(string email);
        User? GetUserByUsername(string username);
        User? GetUserByPhoneNumber(string phoneNumber);
        User? DeleteUser(int id);
    }
}