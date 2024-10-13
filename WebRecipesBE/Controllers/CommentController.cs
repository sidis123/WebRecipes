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
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, IMapper mapper, IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public IActionResult GetAllComments()
        {
            //auto map
            var recipes = _mapper.Map<List<CommentDto>>(_commentRepository.GetAllComments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int id)
        {
            if (!_commentRepository.CommentExists(id))
            {
                return NotFound();
            }
            var recipe = _mapper.Map<CommentDto>(_commentRepository.GetComment(id));
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(recipe);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComment([FromQuery] int userId, [FromQuery] int recipeId ,[FromBody] CommentDto commentCreate)//reikia FromBody , nes jis paims data is jsono
        {
            if (commentCreate == null)
            {
                return BadRequest(ModelState);
            }
            var comment = _commentRepository.GetAllComments().Where(c => c.id_Komentaras == commentCreate.id_Komentaras).FirstOrDefault();

            if (comment != null)
            {
                ModelState.AddModelError("", "Komentaras jau sukurtas");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentMap = _mapper.Map<Comment>(commentCreate);

            commentMap.User = _userRepository.GetUser(userId);
            commentMap.Userid_Vartotojas = userId;
            commentMap.Recipe= _recipeRepository.GetRecipe(recipeId);
            commentMap.Recipeid_Receptas= recipeId;

            if (!_commentRepository.CreateComment(commentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{commentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateComment(int commentId, [FromBody] CommentDto updatedComment)
        {
            if (updatedComment == null)
            {
                return BadRequest(ModelState);
            }
            if (commentId != updatedComment.id_Komentaras)
            {
                return BadRequest(ModelState);
            }
            if (!_commentRepository.CommentExists(commentId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commentMap = _mapper.Map<Comment>(updatedComment);

            commentMap.Userid_Vartotojas = _commentRepository.GetUserIDForComment(commentId);
            commentMap.Recipeid_Receptas =_commentRepository.GetRecipeIDForComment(commentId);
            if (!_commentRepository.UpdateComment(commentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating comment");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{commentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteComment(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
            {
                return NotFound();
            }
            var commenttoDelete = _commentRepository.GetComment(commentId);

            //jeigu yra prirista kazkokia data prie to ka mes deletinam reiketu ir ja deletint arba nors prachekint ar yra surista
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_commentRepository.DeleteComment(commenttoDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting comment");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
