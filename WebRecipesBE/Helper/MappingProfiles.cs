using AutoMapper;
using WebRecipesBE.DTO;
using WebRecipesBE.Models;

namespace WebRecipesBE.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<User, UserDto>();
        }
    }
}
