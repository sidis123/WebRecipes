using WebRecipesBE.Data;
using WebRecipesBE.Models;

namespace WebRecipesBE
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Recipes.Any())
            {
                // Create sample categories
                var categories = new List<Category>()
            {
                new Category { pavadinimas = "Dessert" },
                new Category { pavadinimas = "Main Course" },
                new Category { pavadinimas = "Appetizer" }
            };

                // Create sample users
                var users = new List<User>()
            {
                new User { vardas = "John", pavarde = "Doe", email = "john.doe@example.com", password = "password123", telefonas = "123456789", role = 1 },
                new User { vardas = "Jane", pavarde = "Smith", email = "jane.smith@example.com", password = "password123", telefonas = "987654321", role = 1 },
                new User { vardas = "Emily", pavarde = "Johnson", email = "emily.johnson@example.com", password = "password123", telefonas = "456123789", role = 2 }
            };

                // Create sample recipes with comments and categories
                var recipes = new List<Recipe>()
            {
                new Recipe
                {
                    pavadinimas = "Chocolate Cake",
                    tekstas = "A delicious chocolate cake recipe.",
                    instrukcija = "1. Preheat the oven. 2. Mix ingredients. 3. Bake for 30 minutes.",
                    User = users[0], // John Doe
                    Comments = new List<Comment>()
                    {
                        new Comment { tekstas = "Amazing recipe!", patiktukai = 10, sukurimo_data = DateTime.Now.AddDays(-10), User = users[1] }, // Jane Smith
                        new Comment { tekstas = "My family loved it!", patiktukai = 8, sukurimo_data = DateTime.Now.AddDays(-5), User = users[2] }  // Emily Johnson
                    },
                    ReceptuKategorijos = new List<RecipeCategory>()
                    {
                        new RecipeCategory { Category = categories[0] } // Dessert
                    }
                },
                new Recipe
                {
                    pavadinimas = "Grilled Chicken",
                    tekstas = "A simple and tasty grilled chicken recipe.",
                    instrukcija = "1. Season the chicken. 2. Grill for 20 minutes.",
                    User = users[1], // Jane Smith
                    Comments = new List<Comment>()
                    {
                        new Comment { tekstas = "The best chicken I've ever made!", patiktukai = 15, sukurimo_data = DateTime.Now.AddDays(-7), User = users[0] }, // John Doe
                        new Comment { tekstas = "It turned out great, thanks!", patiktukai = 12, sukurimo_data = DateTime.Now.AddDays(-2), User = users[2] }  // Emily Johnson
                    },
                    ReceptuKategorijos = new List<RecipeCategory>()
                    {
                        new RecipeCategory { Category = categories[1] } // Main Course
                    }
                },
                new Recipe
                {
                    pavadinimas = "Bruschetta",
                    tekstas = "A fresh and easy appetizer.",
                    instrukcija = "1. Toast the bread. 2. Top with tomato mixture.",
                    User = users[2], // Emily Johnson
                    Comments = new List<Comment>()
                    {
                        new Comment { tekstas = "Great for parties!", patiktukai = 20, sukurimo_data = DateTime.Now.AddDays(-3), User = users[0] }, // John Doe
                        new Comment { tekstas = "So easy and delicious!", patiktukai = 18, sukurimo_data = DateTime.Now.AddDays(-1), User = users[1] }  // Jane Smith
                    },
                    ReceptuKategorijos = new List<RecipeCategory>()
                    {
                        new RecipeCategory { Category = categories[2] } // Appetizer
                    }
                }
            };

                // Add the categories to the database
                dataContext.Categories.AddRange(categories);
                // Add the users to the database
                dataContext.Users.AddRange(users);
                // Add the recipes to the database
                dataContext.Recipes.AddRange(recipes);

                // Save changes to the database
                dataContext.SaveChanges();
            }
        }
    }
}
