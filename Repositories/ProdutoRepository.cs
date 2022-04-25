using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Models;
using Projeto.Repositories.Interfaces;

namespace Projeto.Repositories {

    public class ProdutoRepository : IProdutoRepository {

        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) {
            _context = context;
        }


        public IEnumerable<Produto> Produtos => _context.Produtos.Include(c => c.Categoria);// Obtendo os produtos e suas categorias


        public Produto GetProdutoById(int produtoId) {

            return _context.Produtos.FirstOrDefault(P => P.ProdutoId == produtoId);
        }
    }
}