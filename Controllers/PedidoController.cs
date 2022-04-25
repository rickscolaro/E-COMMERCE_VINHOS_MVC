
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Models;
using Projeto.Repositories.Interfaces;

namespace Projeto.Controllers {

    public class PedidoController : Controller {

        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra) {

            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }


        [Authorize]
        [HttpGet]
        public IActionResult Checkout() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido) {

            double precoTotalPedido = 0;
            int totalItensPedido = 0;

            // Obter os itens do carrinho de compra do cliente
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();

            _carrinhoCompra.CarrinhoCompraItens = items;

            // Verifica se existem itens de pedidos
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0) {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um Produto...");
            }

            // Calcula o total de itens e o total do pedido
            foreach (var item in items) {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Produto.Preco * item.Quantidade);
            }

            // Atribui o total de itens do pedido
            pedido.TotalItensPedido = totalItensPedido;

            // Atribui o total do pedido ao pedido
            pedido.PedidoTotal = precoTotalPedido;


            // Validar os dados do pedido
            if (ModelState.IsValid) {
                _pedidoRepository.CriarPedido(pedido);

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :) ";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                // Adicionado
                ViewBag.FreteTotal = _carrinhoCompra.GetFreteTotal();
                ViewBag.ValorFinal = _carrinhoCompra.GetValorFinal();

                _carrinhoCompra.LimparCarrinho();
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);


        }

        public IActionResult CheckoutCompleto() {

            // ViewBag: passar dados entre controller e uma view

            ViewBag.Cliente = TempData["Cliente"];
            ViewBag.DataPedido = TempData["DataPedido"];
            ViewBag.NumeroPedido = TempData["NumeroPedido"];
            ViewBag.TotalPedido = TempData["TotalPedido"];
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :) ";

            ViewBag.FreteTotal = _carrinhoCompra.GetFreteTotal();
            ViewBag.ValorFinal = _carrinhoCompra.GetValorFinal();


            return View();
        }
    }
}