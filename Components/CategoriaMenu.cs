using Microsoft.AspNetCore.Mvc;
using Projeto.Repositories.Interfaces;

namespace Projeto.Components {

    public class CategoriaMenu : ViewComponent {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository) {

            _categoriaRepository = categoriaRepository;
        }

        // Ordenando as categorias por nome e retornando
        public IViewComponentResult Invoke() {
            var categorias = _categoriaRepository.Categorias.OrderBy(p => p.CategoriaNome);
            return View(categorias);
        }
    }
}