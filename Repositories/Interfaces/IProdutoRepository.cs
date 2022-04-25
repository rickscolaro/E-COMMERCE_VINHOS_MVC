


using Projeto.Models;

namespace Projeto.Repositories.Interfaces {

    public interface IProdutoRepository {

        IEnumerable<Produto> Produtos { get; }

        Produto GetProdutoById(int ProdutoId);
    }
}