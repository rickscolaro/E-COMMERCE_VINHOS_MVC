using Microsoft.AspNetCore.Mvc;
using Projeto.Models;
using Projeto.Repositories.Interfaces;
using Projeto.ViewModels;

namespace Projeto.Controllers {

    public class ProdutoController : Controller {


        private readonly IProdutoRepository _iProdutoRepository;

        public ProdutoController(IProdutoRepository iProdutoRepository) {

            _iProdutoRepository = iProdutoRepository;
        }



        public IActionResult List(string categoria) {

            IEnumerable<Produto> produtos;
            string categoriaAtual = string.Empty;

            // É vazio ou nullo?
            if (string.IsNullOrEmpty(categoria)) {

                produtos = _iProdutoRepository.Produtos.OrderBy(p => p.ProdutoId);
                categoriaAtual = "Todos os produtos";

            } else {

                produtos = _iProdutoRepository.Produtos
                           .Where(p => p.Categoria.CategoriaNome.Equals(categoria))
                           .OrderBy(p => p.Nome);

                categoriaAtual = categoria;
            }

            var produtoListViewModel = new ProdutoListViewModel {

                Produtos = produtos,
                CategoriaAtual = categoriaAtual
            };

            return View(produtoListViewModel);
        }





        public ViewResult Details(int produtoId) {

            var produto = _iProdutoRepository.Produtos.FirstOrDefault(d => d.ProdutoId == produtoId);

            if (produto == null) {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(produto);
        }



        // Campo para procurar um produto
        public ViewResult Search(string searchString) {

            string _searchString = searchString;

            IEnumerable<Produto> produtos;

            string currentCategory = string.Empty;



            if (string.IsNullOrEmpty(_searchString)) {

                produtos = _iProdutoRepository.Produtos.OrderBy(p => p.ProdutoId);

                currentCategory = "Todos os produtos";

            } else {

                produtos = _iProdutoRepository.Produtos.Where(p => p.Nome.ToLower().Contains(_searchString.ToLower()));

                // Se nenhum produto foi encontrado
                if (produtos.Any()) {

                    currentCategory = "Produtos";
                } else {
                    currentCategory = "Nenhum Produto foi encontrado";
                }
            }

            return View("~/Views/Produto/List.cshtml", new ProdutoListViewModel {

                Produtos = produtos,
                CategoriaAtual = currentCategory
            });

        }
    }
}