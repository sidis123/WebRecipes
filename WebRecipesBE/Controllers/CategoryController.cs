using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebRecipesBE.DTO;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;

namespace WebRecipesBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository , IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all Categories 
        /// </summary>
        /// <returns>All categories as a list.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]//jeigu lista grazinam ar daug tu dalyku grazinam tai reikia krc IEnumerable daryt
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetAllCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }


        /// <summary>
        /// Retrieves a specific category by ID
        /// </summary>
        /// <param name="Catid"> The ID of the category to retrieve </param>
        /// <returns>The retrieved category</returns>
        [HttpGet("{Catid}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategory(int Catid)
        {
            if (!_categoryRepository.CategoryExists(Catid))
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(Catid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }


        /// <summary>
        /// Creates a category
        /// </summary>
        /// <param> The body of a category in .json form </param>
        /// <returns>201 if created , other error code if something is wrong</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)//reikia FromBody , nes jis paims data is jsono
        {
            if (categoryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.GetAllCategories().Where(c => c.pavadinimas.Trim().ToUpper() == categoryCreate.pavadinimas.TrimEnd().ToUpper()).FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201,"Successfully created");
        }


        /// <summary>
        /// Updates a specific category by ID
        /// </summary>
        /// <param name="categoryId"> The ID of the category to update </param>
        /// <returns>No content</returns>
        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest(ModelState);
            }
            if (categoryId != updatedCategory.id_Kategorija)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categoryMap = _mapper.Map<Category>(updatedCategory);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        /// <summary>
        /// Deletes a specific category by ID
        /// </summary>
        /// <param name="categoryId"> The ID of the category to delete </param>
        /// <returns>No content</returns>
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var categorytoDelete = _categoryRepository.GetCategory(categoryId);

            //jeigu yra prirista kazkokia data prie to ka mes deletinam reiketu ir ja deletint arba nors prachekint ar yra surista
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.DeleteCategory(categorytoDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Returns a category with its recipes
        /// </summary>
        /// <param name="Catid"></param>
        /// <returns></returns>
        [HttpGet("{Catid}/receptai")]
        [ProducesResponseType(200, Type = typeof(CategoryWithRecipesDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategoryWithRecipes(int Catid)
        {
            if (!_categoryRepository.CategoryExists(Catid))
            {
                return NotFound();
            }

            var category = _categoryRepository.GetCategoryWithRecipes(Catid);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryWithRecipesDto = _mapper.Map<CategoryWithRecipesDto>(category);

            return Ok(categoryWithRecipesDto);
        }

    }
}
