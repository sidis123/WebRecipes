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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetAllRecipes()
        {
            //auto map
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetAllRecipes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipes);
        }


        /// <summary>
        /// retrieves a specific recipe by ID
        /// </summary>
        /// <param name="id"> The ID of the recipe to retrieve </param>
        /// <returns>The retrieved recipe in a .json format</returns>
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
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromQuery] int categoryId,[FromQuery] int userId, [FromBody] RecipeDto recipeCreate)//reikia FromBody , nes jis paims data is jsono
        {
            if (recipeCreate == null)
            {
                return BadRequest(ModelState);
            }
            var recipe = _recipeRepository.GetAllRecipes().Where(c => c.id_Receptas == recipeCreate.id_Receptas).FirstOrDefault();

            if (recipe != null)
            {
                ModelState.AddModelError("", "receptas jau sukurtas");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);
            recipeMap.User = _userRepository.GetUser(userId);
            

            if (!_recipeRepository.CreateRecipe(categoryId,userId,recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201,"Successfully created");
        }


        /// <summary>
        /// Updates a specific recipe by ID
        /// </summary>
        /// <param name="recipeId"> The ID of the recipe to update </param>
        /// <returns>No content</returns>
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
            if (recipeId != updatedRecipe.id_Receptas)
            {
                return BadRequest(ModelState);
            }
            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var recipeMap = _mapper.Map<Recipe>(updatedRecipe);

            recipeMap.User = _userRepository.GetUser(_recipeRepository.GetUserIdFromRecipe(recipeMap));
            
            if (!_recipeRepository.UpdateRecipe(recipeMap))
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
