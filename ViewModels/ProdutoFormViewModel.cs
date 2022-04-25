using Projeto.Models;

namespace Projeto.ViewModels
{
    public class ProdutoFormViewModel
    {
        public Produto Produto { get; set; }

        public ICollection<Categoria> Categorias  { get; set; }= new List<Categoria>();
    }
}