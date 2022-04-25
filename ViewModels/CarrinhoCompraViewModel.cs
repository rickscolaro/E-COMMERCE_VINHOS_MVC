using Projeto.Models;

namespace Projeto.ViewModels {

    // Representa a logica da View
    public class CarrinhoCompraViewModel {
        
        public CarrinhoCompra CarrinhoCompra { get; set; }
        
        public double CarrinhoCompraTotal { get; set; }

        public double FreteTotal { get; set; }

        public double ValorFinal { get; set; }
    }
}