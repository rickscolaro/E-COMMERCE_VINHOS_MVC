using Microsoft.AspNetCore.Mvc;

namespace Projeto.Controllers {

    public class ContatoController : Controller {

        public IActionResult Index() {
            
            return View();
        }
    }
}