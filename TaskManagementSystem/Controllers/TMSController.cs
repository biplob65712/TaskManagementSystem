using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using TaskManagementSystem.Models;
using TaskManagementSystem.Models.ViewModels;

namespace TaskManagementSystem.Controllers
{
    public class TMSController : Controller
    {
        TMSDbContext db;
        public TMSController()
        {
            db = new TMSDbContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginVM model)
        {
            var aaa = db.UserTB.Where(c => c.UserName == model.UserName && c.Password == model.Password).FirstOrDefault();

            if (aaa != null)
            {
                HttpContext.Session.SetString("UserSession", aaa.UserName);
                HttpContext.Session.SetString("roleSession", aaa.Role);
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.Message = "Login failed !";
            }

            return View(model);
        }

        public IActionResult Dashboard(DashboardVM model)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                ViewBag.MyRoleSession = HttpContext.Session.GetString("roleSession").ToString();

                if (ViewBag.MyRoleSession == "1")
                {
                    ViewBag.roleShow = "Admin";
                }
                else
                {
                    ViewBag.roleShow = "User";
                }

               
                
            }
            else
            {
                return RedirectToAction("Login");
            }

            ICollection<User> allUser = new List<User>();

            allUser = db.UserTB.ToList();

            model.UserList = allUser;

            return View(model);
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }

            return View();
        }

        public  IActionResult AllTaskList(TaskIndexVM model)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                ViewBag.MyRoleSession = HttpContext.Session.GetString("roleSession").ToString();

                if (ViewBag.MyRoleSession == "1")
                {
                    ViewBag.roleShow = "Admin";
                }
                else
                {
                    ViewBag.roleShow = "User";
                }



            }
            else
            {
                return RedirectToAction("Login");
            }

            ICollection<TaskModel> allTask = new List<TaskModel>();

            allTask = db.TaskTB.ToList();

            model.TaskList =  allTask.Select(c => new TaskListItemVM()
            {
                ID = c.ID,
                TaskTile = c.TaskTile,
                TaskDesk = c.TaskDesk,
                CreatedDate = c.CreatedDate,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                Status = c.Status,
                UserID = c.UserID
            }).ToList();

            //model.ProductList = products.Select(c => new ProductListItem()
            //{
            //    Id = c.Id,
            //    Name = c.Name,
            //    Description = c.Description,
            //    SalesPrice = c.SalesPrice,
            //    BrandName = c.Brand?.Name
            //}).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult TaskCreate()
        {
            //var brands = _brandService.GetAll();
            //var categories = _categoryService.GetAll();

            //var model = new ProductCreateVM();
            //model.Brands = brands;
            //model.Categories = categories;

            //return View(model);

            var users = db.UserTB.ToList();

            var model = new TaskCreateVM()
            {
                Users = users,
            };

            return View(model);

            
           
        }

        //public virtual ICollection<T> GetAll()
        //{
        //    return Table.ToList();
        //}

        [HttpPost]
        public IActionResult TaskCreate(TaskCreateVM model)
        {


            model.Users = db.UserTB.ToList();

            
            
                var taskM = new TaskModel()
                {
                    TaskTile = model.TaskTile,
                    TaskDesk = model.TaskDesk,
                    CreatedDate = DateTime.Now,
                    StartDate= model.StartDate,
                    EndDate= model.EndDate,
                    Status = model.Status,
                    UserID= model.UserID
                };

                db.TaskTB.Add(taskM);

                int rowAffected = db.SaveChanges();

                if (rowAffected > 0)
                {
                    return RedirectToAction("AllTaskList");
                }
                else
                {
                    return ViewBag.ErrorMSG = "save failed";
                }
            

            return View(model);

            

        }

        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                ViewBag.MyRoleSession = HttpContext.Session.GetString("roleSession").ToString();

                if (ViewBag.MyRoleSession == "1")
                {
                    ViewBag.roleShow = "Admin";
                }
                else
                {
                    ViewBag.roleShow = "User";
                }



            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public TaskModel GetByTaskID(int id)
        {
            var existingTask = db.TaskTB.FirstOrDefault(c => c.ID == id);

            return existingTask;
        }

        [HttpGet]
        public IActionResult TaskUpdate(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = "Id is not set for this Task !";
                return View("_ApplicationError");
            }
            var existing = GetByTaskID((int)id);

            if (existing == null)
            {
                ViewBag.ErrorMessage = $"Did not find any Task with Id {id}";

                return View("_ApplicationError");
            }

            var taskUpdateVM = new TaskUpdateVM()
            {
                ID = existing.ID,
                TaskTile = existing.TaskTile,
                TaskDesk = existing.TaskDesk,
                CreatedDate = existing.CreatedDate,
                StartDate = existing.StartDate,
                EndDate = existing.EndDate,
                Status = existing.Status,
                UserID = existing.UserID,
            };

            return View(taskUpdateVM);


        }

        [HttpPost]
        public IActionResult TaskUpdate(TaskUpdateVM model)
        {
            var taskM = new TaskModel()
            {
                ID = model.ID,
                TaskTile = model.TaskTile,
                TaskDesk = model.TaskDesk,
                CreatedDate = model.CreatedDate,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Status = model.Status,
                UserID = model.UserID

            };

            db.TaskTB.Update(taskM);

            int rowAffected = db.SaveChanges();

            if (rowAffected > 0)
            {
                return RedirectToAction("AllTaskList");
            }

            return View(model); 
        }
    }
}
