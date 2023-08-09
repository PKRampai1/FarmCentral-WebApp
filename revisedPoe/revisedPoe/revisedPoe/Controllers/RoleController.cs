using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace revisedPoe.Controllers
{
    //Adapted from: C-sharp corner
    //Author:Munib Butt
    //Author Profile:https://www.c-sharpcorner.com/members/munib-butt
    //Date:6 May 2020
    //Link:https://www.c-sharpcorner.com/article/adding-role-authorization-to-a-asp-net-mvc-core-application/

    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [Authorize(Roles ="Employee")]
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}