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
        bool CommentExists(int id);
        int GetUserIDForComment(int commentId);
        int GetRecipeIDForComment(int commentId);

        bool Save();
    }
}
