using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Classes;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category> {


        new Category("Baking", "images/baking.png"),
        new Category("Main Courses", "images/main_courses.png"),
        new Category("Salads", "images/salads.png"),
        new Category("Desserts", "images/desserts.png"),
        new Category("Drinks", "images/drinks.png") };

        // GET: api/Category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return categories;
        }

        // GET: api/Category/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> Get(int id)
        {
            var category = categories.Find(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        // POST: api/Category
        [HttpPost]
        public ActionResult<Category> Post([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            categories.Add(category);
            return CreatedAtAction(nameof(Get), new { id = category.CategoryId }, category);
        }

        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category updatedCategory)
        {
            if (id != updatedCategory.CategoryId)
            {
                return BadRequest();
            }
            var category = categories.Find(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;
            category.IconPath = updatedCategory.IconPath;
            return NoContent();
        }

        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = categories.Find(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            categories.Remove(category);
            return NoContent();
        }
    }
}
