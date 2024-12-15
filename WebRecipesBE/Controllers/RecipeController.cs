using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebRecipesBE.DTO;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;
using WebRecipesBE.Repository;

namespace WebRecipesBE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper , IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }



        /// <summary>
        /// Retrieves all Recipes
        /// </summary>
        /// <returns>A list of recipes</returns>
        [Authorize(Policy = "GuestOrHigher")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<object>))]
        public IActionResult GetAllRecipes()
        {
            var recipes = _recipeRepository.GetAllRecipes();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Manually map the properties, including id_Receptas
            var recipeList = recipes.Select(r => new
            {
                id = r.id_Receptas,
                pavadinimas = r.pavadinimas,
                tekstas = r.tekstas,
                instrukcija = r.instrukcija,
                pictureUrl = r.PictureUrl,
                kurejas = r.Userid_Vartotojas,
            }).ToList();

            return Ok(recipeList);
        }



        /// <summary>
        /// retrieves a specific recipe by ID
        /// </summary>
        /// <param name="id"> The ID of the recipe to retrieve </param>
        /// <returns>The retrieved recipe in a .json format</returns>
        [Authorize(Policy = "GuestOrHigher")]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int id)
        {
            if (!_recipeRepository.RecipeExists(id))
            {
                return NotFound();
            }
            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(id));
            //manual map
            //var recipeDto = new RecipeDto
            //{
            //    id_Receptas = recipe.id_Receptas,
            //    pavadinimas = recipe.pavadinimas,
            //    tekstas = recipe.tekstas,
            //    instrukcija = recipe.instrukcija
            //};
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipe);
        }


        /// <summary>
        /// Creates a recipe
        /// </summary>
        /// <param name="categoryId"> The category that will specify the recipe </param>
        /// <param name="userId"> The ID of the user who is creating the recipe </param>
        /// <param name="recipeCreate"> The body of the recipe in a .json format </param>
        /// <returns>201 if created , other error if something went wrong</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateRecipe([FromQuery] int categoryId, [FromQuery] int userId, [FromBody] RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
            {
                return BadRequest("Invalid recipe data");
            }

            // Check if a recipe with the same "instrukcija" already exists
            var existingRecipe = _recipeRepository.GetAllRecipes()
                .FirstOrDefault(c => c.instrukcija == recipeCreate.instrukcija);
            if (existingRecipe != null)
            {
                ModelState.AddModelError("", "Recipe already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Recipe
            var recipeMap = _mapper.Map<Recipe>(recipeCreate);
            recipeMap.User = _userRepository.GetUser(userId);

            // Call the repository to insert the recipe (ID is auto-generated)
            if (!_recipeRepository.CreateRecipe(categoryId, userId, recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Successfully created");
        }




        /// <summary>
        /// Updates a specific recipe by ID
        /// </summary>
        /// <param name="recipeId"> The ID of the recipe to update </param>
        /// <returns>No content</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpPut("{recipeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRecipe(int recipeId, [FromBody] RecipeDto updatedRecipe)
        {
            if (updatedRecipe == null)
            {
                return BadRequest(ModelState);
            }

            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Retrieve the existing recipe from the database
            var existingRecipe = _recipeRepository.GetRecipe(recipeId);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            // Map updated properties from DTO to the existing recipe
            _mapper.Map(updatedRecipe, existingRecipe);

            // Ensure the User object is valid
            var userId = _recipeRepository.GetUserIdFromRecipe(existingRecipe);
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                ModelState.AddModelError("", "The user associated with this recipe does not exist.");
                return BadRequest(ModelState);
            }

            existingRecipe.User = user;

            // Attempt to update the recipe
            if (!_recipeRepository.UpdateRecipe(existingRecipe))
            {
                ModelState.AddModelError("", "Something went wrong updating recipe");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific recipe by ID
        /// </summary>
        /// <param name="recipeId"> The ID of the recipe to delete </param>
        /// <returns>No content</returns>
        [Authorize(Policy = "UserOrAdmin")]
        [HttpDelete("{recipeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }
            var recipetoDelete = _recipeRepository.GetRecipe(recipeId);

            //jeigu yra prirista kazkokia data prie to ka mes deletinam reiketu ir ja deletint arba nors prachekint ar yra surista
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_recipeRepository.DeleteRecipe(recipetoDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting comment");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }




    }
}
