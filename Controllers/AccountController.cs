

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projeto.ViewModels;

namespace Projeto.Controllers {

    public class AccountController : Controller {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        // Instancias para fazer login
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        //Implementar login, registro e logout
        [HttpGet]
        public IActionResult Login(string returnUrl) {

            var actualUrl = HttpContext.Request.Path;
            string ReturnUrl = HttpContext.Request.Body.ToString();

            return View(new LoginViewModel() {

                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM) {

            // Se não estiver válido retorna para a pagina de login com os erros
            if (!ModelState.IsValid)
                return View(loginVM);

            // Se estiver válido tenta localizar o usuário pelo nome
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            // verificar se o usuário existe
            if (user != null) {

                // Se ele existe eu tento fazer o login
                // Os dois últimos false são persistência do cookie e se o login falhar vai bloquear o usuário
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                // Verifica se o login foi feito com sucesso
                if (result.Succeeded) {

                    if (string.IsNullOrEmpty(loginVM.ReturnUrl)) {
                        return RedirectToAction("Index", "Home");
                    } else {
                        this.ModelState.AddModelError("Login", "Falha ao realizar o login, verifique o Usuário/Senha ");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Usuário/Senha inválidos ou não localizados!!");
            return View(loginVM);
        }




        
        public IActionResult Register() {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM) {

            if (ModelState.IsValid) {

                // Definir usuário como uma nova instancia do Identityuser
                var user = new IdentityUser() { UserName = registroVM.UserName };
                // Criar o usuário
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded) {
                    //// Adiciona o usuário padrão ao perfil Member
                    //await _userManager.AddToRoleAsync(user, "Member");
                    //await _signInManager.SignInAsync(user, isPersistent: false);

                    // Atribui ao perfil Member
                    await _userManager.AddToRoleAsync(user, "Member");

                    return RedirectToAction("Login", "Account");
                } else {
                    this.ModelState.AddModelError("Registro", "Falha ao realizar o registro, verifique o usuário/senha usados");
                }

            }
            return View(registroVM);
        }

        // [AllowAnonymous]
        // public ViewResult LoggedIn() => View();

        [HttpPost]
        public async Task<IActionResult> Logout() {

            // Linpar todos os valores da Session
            HttpContext.Session.Clear();
            // Zerando o usuário
            HttpContext.User = null;

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        // Acesso negado
        // O método AccessDenied é definido na politica dentro dentro de startup 
        public IActionResult AccessDenied(){

            return View();
        }



    }
}