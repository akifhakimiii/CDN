using System.Diagnostics;
using AutoMapper;
using FreelanceApp.DTOs;
using FreelanceApp.Helpers;
using FreelanceApp.Interfaces;
using FreelanceApp.Mappers;
using FreelanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FreelanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery]QueryObject query)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var users = _mapper.Map<List<User>>(_userRepository.GetUsers(query));

                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = _mapper.Map<User>(_userRepository.GetUserById(id));
                
                if(user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto userDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_userRepository.GetUserByEmail(userDto.Email) != null)
                {
                    return BadRequest("Email already exists.");
                }

                if (_userRepository.GetUserByUsername(userDto.Username) != null)
                {
                    return BadRequest("Username already exists.");
                }

                if (_userRepository.GetUserByPhoneNumber(userDto.PhoneNumber) != null)
                {
                    return BadRequest("Phone number already exists.");
                }

                var userModel = userDto.ToUserFromCreateDto();
                var user = _userRepository.CreateUser(userModel);

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto userDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingUser = _userRepository.GetUserById(id);

                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                if (_userRepository.GetUserByEmail(userDto.Email) != null)
                {
                    return BadRequest("Email already exists.");
                }

                if (_userRepository.GetUserByUsername(userDto.Username) != null)
                {
                    return BadRequest("Username already exists.");
                }

                if (_userRepository.GetUserByPhoneNumber(userDto.PhoneNumber) != null)
                {
                    return BadRequest("Phone number already exists.");
                }
;
                var userModel = _userRepository.UpdateUser(id, userDto);
                if (userModel == null)
                    return NotFound();
                return Ok(userModel.ToUserDto());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            try
            {
                var existingUser = _userRepository.GetUserById(id);

                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                var user = _mapper.Map<User>(_userRepository.DeleteUser(id));

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
