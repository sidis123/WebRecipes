using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebRecipesBE.DTO;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;
using WebRecipesBE.Repository;
using WebRecipesBE.Security;


namespace WebRecipesBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public UserController(AuthService authService, TokenService tokenService, IUserRepository userRepository , IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _authService = authService;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            Console.WriteLine("LOGINAS PASPAUSTAS VAZIUOJAM BLE");
            // Step 1: Authenticate the user
            var user = _userRepository.Authenticate(loginDto.Email, loginDto.Password);// Assuming you have an Authenticate method
            Console.WriteLine("cia musu useris :");
            Console.WriteLine(user);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Step 2: Generate tokens using AuthService
            var accessToken = _authService.GenerateAccessToken(user);
            var refreshToken = _authService.GenerateRefreshToken();

            // Step 3: Save the refresh token in the database
            var refreshTokenEntry = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.id_Vartotojas,
                Expiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };
            _tokenService.SaveRefreshToken(refreshTokenEntry);

            // Step 4: Return both tokens to the client
            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = new
                {
                    user.id_Vartotojas,
                    user.email,
                    user.vardas,
                    user.pavarde,
                    user.telefonas,
                    user.role,
                }
            });
            }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var result = _tokenService.RevokeRefreshToken(refreshTokenDto.Token);
            if (!result)
            {
                return BadRequest("Invalid refresh token.");
            }
            return NoContent(); // Successfully logged out
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            // Step 1: Validate the refresh token
            var existingToken = _tokenService.GetRefreshToken(refreshTokenDto.Token);
            if (existingToken == null || existingToken.Expiration < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token");
            }

            // Step 2: Get the associated user
            var user = _userRepository.GetUser(existingToken.UserId);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            // Step 3: Generate a new access token and a new refresh token
            var newAccessToken = _authService.GenerateAccessToken(user);
            var newRefreshToken = _authService.GenerateRefreshToken();

            // Step 4: Update the refresh token in the database
            _tokenService.UpdateRefreshToken(existingToken, newRefreshToken, _jwtSettings.RefreshTokenExpirationDays);

            // Step 5: Return the new tokens to the client
            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }



        /// <summary>
        /// Retrieves all the users
        /// </summary>
        /// <returns>A list of users</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetAllUsers()
        {
            Console.WriteLine("Opa getinam visus dawajjjjjjjjjjjjjjj");
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

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("admin-only-endpoint")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("This is an admin-only endpoint.");
        }

        [Authorize(Policy = "UserOrAdmin")]
        [HttpGet("user-or-admin-endpoint")]
        public IActionResult UserOrAdminEndpoint()
        {
            return Ok("This endpoint is accessible to users and admins.");
        }

        [Authorize(Policy = "GuestOrHigher")]
        [HttpGet("guest-or-higher-endpoint")]
        public IActionResult GuestOrHigherEndpoint()
        {
            return Ok("This endpoint is accessible to guests, users, and admins.");
        }




    }
}
