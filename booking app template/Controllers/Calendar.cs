using Microsoft.AspNetCore.Mvc;

namespace booking_app_template.Controllers
{
    public class Calendar : Controller
    {
        public IActionResult Index()
        {
            DateTime[] d = new DateTime[]
            {
                    new DateTime(2024,8,15),
                    new DateTime(2024,8,16),
                    new DateTime(2024,8,22),
                    new DateTime(2024,8,24)
            };

            return View(d);
        }

        
    }
}
