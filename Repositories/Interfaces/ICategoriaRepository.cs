
using Projeto.Models;

namespace Projeto.Repositories.Interfaces {

    public interface ICategoriaRepository {

        IEnumerable<Categoria> Categorias { get; }
    }
}