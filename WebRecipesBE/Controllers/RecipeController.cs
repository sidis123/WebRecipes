using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebRecipesBE.DTO;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;

namespace WebRecipesBE.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
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


    }
}
