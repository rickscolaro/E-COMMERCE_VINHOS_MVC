
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Models {

    [Table("Categorias")]
    public class Categoria {


        public Categoria() {

        }

        public Categoria(string categoriaNome, string descricao) {
            CategoriaNome = categoriaNome;
            Descricao = descricao;
        }

        [Key]
        public int CategoriaId { get; set; }

        [Display(Name = "Nome da Categoria")]
        // [Required]
        // [StringLength(100)]
        public string CategoriaNome { get; set; }

        // [Required]
        // [StringLength(200)]
        [Display(Name = "Descrição da Categoria")]
        public string Descricao { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();




    }
}