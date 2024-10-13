using Microsoft.EntityFrameworkCore;
using WebRecipesBE.Data;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;

namespace WebRecipesBE.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CommentExists(int id)
        {
            return _context.Comments.Any(r => r.id_Komentaras == id);
        }

        public bool CreateComment(Comment comment)
        {
            _context.Add(comment);
            return Save();
        }

        public bool DeleteComment(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }

        public ICollection<Comment> GetAllComments()
        {
            return _context.Comments.OrderBy(c => c.id_Komentaras).ToList();
        }

        public Comment GetComment(int id)
        {
            return _context.Comments.Where(c => c.id_Komentaras == id).FirstOrDefault();
        }

        public int GetRecipeIDForComment(int commentId)
        {
            return _context.Comments.Where(c => c.id_Komentaras == commentId).Select(c => c.Recipeid_Receptas).FirstOrDefault();
        }

        public int GetUserIDForComment(int commentId)
        {
            return _context.Comments.Where(c => c.id_Komentaras == commentId).Select(c => c.Userid_Vartotojas).FirstOrDefault();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateComment(Comment comment)
        {
            _context.Update(comment);
            return Save();
        }
    }
}
