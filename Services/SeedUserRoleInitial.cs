using Microsoft.AspNetCore.Identity;

namespace Projeto.Services {
    public class SeedUserRoleInitial : ISeedUserRoleInitial {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles() {

            // Verifica se o perfil ja existe
            // .Result porque o método é assíncrono
            if (!_roleManager.RoleExistsAsync("Member").Result) {

                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result; //IdentityResult é o resultado de identity
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result) {

                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers() {

            // Criando um usuario
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null) {

                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Senha#2022").Result;// Usuario e senha

                if (result.Succeeded) {
                    _userManager.AddToRoleAsync(user, "Member").Wait(); //.WaitAguarda o Task concluir a execução.
                }
            }

            // Criando um administrador
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null) {

                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Senha#2022").Result;// Usuario e senha

                if (result.Succeeded) {
                    _userManager.AddToRoleAsync(user, "Admin").Wait(); //.WaitAguarda o Task concluir a execução.
                }
            }
        }

    }
}