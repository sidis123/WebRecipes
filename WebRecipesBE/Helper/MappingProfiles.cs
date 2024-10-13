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
            CreateMap<RecipeDto, Recipe>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
