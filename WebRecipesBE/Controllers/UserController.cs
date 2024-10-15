using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebRecipesBE.DTO;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;
using WebRecipesBE.Repository;

namespace WebRecipesBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Retrieves all the users
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAllUsers()
        {
            
            var recipes = _mapper.Map<List<UserDto>>(_userRepository.GetAllUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipes);
        }

        /// <summary>
        /// Retrieves a specific user by ID
        /// </summary>
        /// <param name="id"> The ID of the user to retrieve </param>
        /// <returns>200 with the body of the retrieved user in .json format</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }
            var recipe = _mapper.Map<UserDto>(_userRepository.GetUser(id));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipe);
        }

        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="userCreate"> The body of the user entity in .json format </param>
        /// <returns>201 if the user was created succesfully, other code if something went wrong</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreate)//reikia FromBody , nes jis paims data is jsono
        {
            if (userCreate == null)
            {
                return BadRequest(ModelState);
            }
            var user = _userRepository.GetAllUsers().Where(c => c.email.Trim() == userCreate.email.TrimEnd()).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User with that email allready exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(userCreate);
            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201,"Successfully created");
        }

        /// <summary>
        /// Updates a specific user by ID
        /// </summary>
        /// <param name="userId"> The ID of the user to update </param>
        /// <returns>No content</returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest(ModelState);
            }
            if (userId != updatedUser.id_Vartotojas)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        /// <summary>
        /// Deletes a specific user by ID
        /// </summary>
        /// <param name="userId"> The ID of the user to delete </param>
        /// <returns>No content</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var usertoDelete = _userRepository.GetUser(userId);

            //jeigu yra prirista kazkokia data prie to ka mes deletinam reiketu ir ja deletint arba nors prachekint ar yra surista
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_userRepository.DeleteUser(usertoDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
