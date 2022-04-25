
using Projeto.Models;

namespace Projeto.ViewModels {

    public class PedidoProdutoViewModel {
        
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }
    }
}