using Microsoft.AspNetCore.Mvc;
using server.Classes;
using System.Collections.Generic;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {


        private readonly List<Recipe> _recipes = new List<Recipe> {
        new Recipe(
        "Chocolate Chip Cookies",
            1, // Baking category ID (replace with actual ID)
        30,
            2, // Medium difficulty level
            DateTime.Now,
        new List<string>() { "2 cups all-purpose flour", "1 teaspoon baking soda", "1/2 teaspoon salt", "1 cup (2 sticks) unsalted butter, softened", "1 cup granulated sugar", "1 cup packed light brown sugar", "2 large eggs", "2 teaspoons pure vanilla extract", "2 cups semisweet chocolate chips" },
        new List<string>() { "Preheat oven to 375 degrees F (190 degrees C).", "... (remaining instructions)" },
        userId: 1, // Replace with actual user ID
        imagePath: "images/chocolate_chip_cookies.jpg",
        description: "Classic and delicious homemade chocolate chip cookies...",1,"114"
    )};
// GET: api/recipes - קבלת כל המתכונים
    [HttpGet]
        public ActionResult<IEnumerable<Recipe>> Get()
        {
            return Ok(_recipes);
        }

        // GET: api/recipes/{id} - קבלת מתכון לפי מזהה
        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(int id)
        {
            var recipe = _recipes.Find(r => r.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        // POST: api/recipes - הוספת מתכון חדש
        [HttpPost]
        public ActionResult<Recipe> Post([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _recipes.Add(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.RecipeId }, recipe);
        }

        // PUT: api/recipes/{id} - עדכון מתכון קיים
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Recipe updatedRecipe)
        {
            if (id != updatedRecipe.RecipeId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = _recipes.Find(r => r.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            recipe.RecipeName = updatedRecipe.RecipeName;
            recipe.CategoryId = updatedRecipe.CategoryId;
            recipe.PreparationTimeInMinutes = updatedRecipe.PreparationTimeInMinutes;
            recipe.DifficultyLevel = updatedRecipe.DifficultyLevel;
            recipe.Ingredients = updatedRecipe.Ingredients;
            recipe.Instructions = updatedRecipe.Instructions;
            recipe.UserId = updatedRecipe.UserId;
            recipe.ImagePath = updatedRecipe.ImagePath;

            return NoContent();
        }

        // DELETE: api/recipes/{id} - מחיקת מתכון
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var recipe = _recipes.Find(r => r.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            _recipes.Remove(recipe);
            return NoContent();
        }
    }
}
