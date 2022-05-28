

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Models;
using Projeto.Repositories.Interfaces;
using Projeto.ViewModels;

namespace Projeto.Controllers {

    public class CarrinhoCompraController : Controller {

        private readonly IProdutoRepository _iProdutoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(IProdutoRepository iProdutoRepository, CarrinhoCompra carrinhoCompra) {

            _iProdutoRepository = iProdutoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index() {

            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel {

                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
                FreteTotal = _carrinhoCompra.GetFreteTotal(),
                ValorFinal = _carrinhoCompra.GetValorFinal()
            };

            return View(carrinhoCompraVM);
        }


        [Authorize]
        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int produtoId) {

            var produtoSelecionado = _iProdutoRepository.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produtoSelecionado != null) {

                // Verificar o estoque // Obs: não resolve o retorno ao estoque. A um problema caso a venda não seja realizada ou o tempo de session do carrinho acabe
                if (produtoSelecionado.EmEstoque == 0) {
                    return RedirectToAction("ErroEstoque"); //Redireciona para o método ErroEstoque.
                }
                produtoSelecionado.EmEstoque--;// Diminui o estoque


                _carrinhoCompra.AdicionarAoCarrinho(produtoSelecionado);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemDoCarrinhoCompra(int produtoId) {

            var produtoSelecionado = _iProdutoRepository.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produtoSelecionado != null) {

                _carrinhoCompra.RemoverDoCarrinho(produtoSelecionado);
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult ErroEstoque() {
            return View();
        }
    }
}