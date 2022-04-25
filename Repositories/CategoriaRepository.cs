
using Projeto.Data;
using Projeto.Models;
using Projeto.Repositories.Interfaces;

namespace Projeto.Repositories {

    public class CategoriaRepository : ICategoriaRepository {

        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context) {

            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;// Retornar uma coleção de Categorias
    }
}