using WebRecipesBE.Models;

namespace WebRecipesBE.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetComment(int id);
        bool CreateComment(Comment comment);
        bool UpdateComment(Comment comment);
        bool DeleteComment(Comment comment);
        ICollection<Comment> GetAllComments();
        bool Save();
    }
}
