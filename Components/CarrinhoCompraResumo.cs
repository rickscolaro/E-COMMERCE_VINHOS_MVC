using Microsoft.AspNetCore.Mvc;
using Projeto.Models;
using Projeto.ViewModels;

namespace Projeto.Components {

    // ViewComponent permite criar funcionalidades semelhantes a um método Action de um Controlador independente de um controlador

    public class CarrinhoCompraResumo : ViewComponent {

        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra) {

            _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke() {

            var items = _carrinhoCompra.GetCarrinhoCompraItens();

            var carrinhoCompraVM = new CarrinhoCompraViewModel {

                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
                FreteTotal = _carrinhoCompra.GetFreteTotal(),
                ValorFinal =_carrinhoCompra.GetValorFinal()
            };
            return View(carrinhoCompraVM);
        }
    }
}