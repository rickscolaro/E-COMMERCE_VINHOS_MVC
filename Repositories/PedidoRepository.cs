


using Projeto.Data;
using Projeto.Models;
using Projeto.Repositories.Interfaces;

namespace Projeto.Repositories {

    // O pedido é montado com os itens do carrinho
    public class PedidoRepository : IPedidoRepository {

        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra) {

            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }



        // Criar o Pedido para que o Id do pedido seja criado
        public void CriarPedido(Pedido pedido) {

            pedido.PedidoEnviado = DateTime.Now;
            pedido.PedidoEntregueEm = DateTime.Now;

            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            // Para cada item do carrinho representa-se um pedido-detalhe
            foreach (var carrinhoItem in carrinhoCompraItens) {

                var pedidoDetail = new PedidoDetalhe() {
                    Quantidade = carrinhoItem.Quantidade,
                    ProdutoId = carrinhoItem.Produto.ProdutoId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Produto.Preco
                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }
            _appDbContext.SaveChanges();
        }



        public Pedido GetPedidoById(int pedidoId) {
            throw new NotImplementedException();
        }

        public List<Pedido> GetPedidos() {
            throw new NotImplementedException();
        }
    }
}