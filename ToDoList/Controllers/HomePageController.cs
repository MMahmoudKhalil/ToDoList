using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.Model;

namespace ToDoList.Controllers
{
    public class HomePageController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult ToDoList()
        {
            var todo = dbContext.todos.ToList();
            return View(todo);
        }
        public IActionResult CreatePage()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult CreatePage(ToDo toDo, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension
                    (file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                toDo.File =  fileName;
            }
            if (toDo != null)
            {
                dbContext.todos.Add(new ToDo 
            {
                Name        = toDo.Name,
                Description = toDo.Description,
                Deadline    = toDo.Deadline,
                File        = toDo.File
            });
            dbContext.SaveChanges();
                TempData["SuccessMessage"] = " ✔️ Data added successfully !";
                return RedirectToAction("ToDoList");
            }
            return RedirectToAction("NotFund");            
            
        }
        public IActionResult Edit(int ToDoId)
        {
            var todos = dbContext.todos.FirstOrDefault(e => e.Id == ToDoId);
            if (todos == null)
            {
                return NotFound();
            }
            return View(todos);

        }
        [HttpPost]
        public IActionResult Edit(ToDo toDo, IFormFile file)
        {
            var toDoInDb = dbContext.todos.AsNoTracking().FirstOrDefault(e => e.Id == toDo.Id);
            if (toDoInDb != null && file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "www\\images", toDoInDb.File);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Exists(oldPath);
                }
                toDo.File = fileName;
            }
            else
            {
                toDo.File = toDoInDb?.File;
            }
            if (toDo != null)
            {
                dbContext.todos.Update(toDo);
                dbContext.SaveChanges();
                TempData["SuccessMessage"] = "✔️ Data has been modified successfully!";
                return RedirectToAction("ToDoList");
            }
            return RedirectToAction("NotFund");
        }

        public IActionResult Delete(int ToDoId)
        {
            var todos = dbContext.todos.FirstOrDefault(e => e.Id == ToDoId);
            if (todos != null)
            {
                dbContext.todos.Remove(todos);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(ToDoList));
            }
            return View("NotFund");
            
        }
        
    }
}