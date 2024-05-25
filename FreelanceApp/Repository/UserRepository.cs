using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceApp.DTOs;
using FreelanceApp.Helpers;
using FreelanceApp.Interfaces;
using FreelanceApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace FreelanceApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CdnMainContext _context;

        public UserRepository(CdnMainContext context)
        {
            _context = context;
        }

        public List<User> GetUsers(QueryObject query)
        {


                var userQuery = _context.Users.AsQueryable();


                if(!string.IsNullOrWhiteSpace(query.Email))
                {
                    userQuery = userQuery.Where(s => s.Email.Contains(query.Email));
                }

                if(!string.IsNullOrWhiteSpace(query.Username))
                {
                     userQuery = userQuery.Where(s => s.Username.Contains(query.Username));
                }
                if(!string.IsNullOrWhiteSpace(query.PhoneNumber))
                {
                     userQuery = userQuery.Where(s => s.PhoneNumber.Contains(query.PhoneNumber));
                }
                var users = userQuery
                    .OrderBy(user => user.Id)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .ToList();
                
                return users;
            }

        public User? GetUserById(int id)
        {
            var user = _context.Users.Find(id);

            return user;
        }

        public User CreateUser(User userDto)
        {

            _context.Users.Add(userDto);
            _context.SaveChanges();

            return userDto;
        }
        public User? UpdateUser(int id, UpdateUserDto userDto)
        {
            var userModel = _context.Users.FirstOrDefault(x => x.Id == id);
            if (userModel == null)
            {
                return userModel;
            }
            userModel.Username = userDto.Username;
            userModel.PhoneNumber = userDto.PhoneNumber;
            userModel.Email = userDto.Email;
            userModel.Hobby = userDto.Hobby;
            userModel.Skillsets = userDto.Skillsets;
            _context.SaveChanges();
            return userModel;
        }
        public User? DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return user;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return user;
        }
        public User? GetConflictingUser(UpdateUserDto userDto, int userId)
        {
            return _context.Users
                .FirstOrDefault(u => 
                    (u.Email == userDto.Email || u.Username == userDto.Username || u.PhoneNumber == userDto.PhoneNumber) && u.Id != userId);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User? GetUserByPhoneNumber(string phoneNumber)
        {
            return _context.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }

    }
}