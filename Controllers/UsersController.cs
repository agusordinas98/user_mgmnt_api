using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserMgmntAPI.Models;
using UserMgmntAPI.Repositories;

namespace UserMgmntAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere token válido
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers() => Ok(_userRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return NotFound(new { message = $"Usuario con ID {id} no encontrado" });

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.Add(newUser))
                return Conflict(new { message = "Ya existe un usuario con ese ID" });

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            if (!_userRepository.Update(updatedUser))
                return NotFound(new { message = $"Usuario con ID {id} no encontrado" });

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (!_userRepository.Delete(id))
                return NotFound(new { message = $"Usuario con ID {id} no encontrado" });

            return Ok(new { message = $"Usuario con ID {id} eliminado" });
        }
    }
}
