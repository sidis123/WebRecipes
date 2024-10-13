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


        [HttpPost]
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
            return Ok("Successfully created");
        }

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
