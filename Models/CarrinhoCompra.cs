

using Microsoft.EntityFrameworkCore;
using Projeto.Data;

namespace Projeto.Models {

    public class CarrinhoCompra {

        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context) {

            _context = context;
        }

        //Define as propriedades do Carrinho : Id e os Itens
        public string CarrinhoCompraId { get; set; }

        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }



        // Obter Carrinho da sessão
        public static CarrinhoCompra GetCarrinho(IServiceProvider services) {

            //define uma sessão
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;// Operador elvis?. Se for diferente de null obtem Session e retorna

            //Obtem um serviço do tipo do nosso contexto 
            var context = services.GetService<AppDbContext>();

            //Obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();// Operador de coalescência nula. Se for diferente de null retorna valor se não gera um novo id

            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context) {
                CarrinhoCompraId = carrinhoId
            };
        }



        public void AdicionarAoCarrinho(Produto produto) {

            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                        s => s.Produto.ProdutoId == produto.ProdutoId && s.CarrinhoCompraId == CarrinhoCompraId);

            // Se o item não existe se adiciona se não só incrementa a quantidade
            if (carrinhoCompraItem == null) {

                carrinhoCompraItem = new CarrinhoCompraItem {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Produto = produto,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);

            } else {

                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }





        public int RemoverDoCarrinho(Produto produto) {

            // Verificar se o produto existe
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                        s => s.Produto.ProdutoId == produto.ProdutoId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null) {

                // Verificar a quantidade 
                if (carrinhoCompraItem.Quantidade > 1) {

                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;

                } else {

                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }


        public List<CarrinhoCompraItem> GetCarrinhoCompraItens() {

            // se não existir vai obter todos os itens do carrinho
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = _context.CarrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(s => s.Produto)
                    .ToList());
        }

        public void LimparCarrinho() {

            var carrinhoItens = _context.CarrinhoCompraItens
                    .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }


        // Total da soma de todos os itens de um carrinho de compra
        public double GetCarrinhoCompraTotal() {

            var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Select(c => c.Produto.Preco * c.Quantidade).Sum();

            return total;
        }

        public double GetFreteTotal() {

            var distancia = 250;

            var frete = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Select(c => c.Produto.Peso * c.Quantidade).Sum();

            if (distancia > 100) {
                frete = ((frete * 5) * distancia) / 100;
            } else {
                frete = frete * 5;
            }
            return frete;
        }

        public double GetValorFinal() {

            var valorFinal = GetCarrinhoCompraTotal() + GetFreteTotal();

            return valorFinal;
        }
    }
}