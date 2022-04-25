

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Models {


    [Table("Produtos")]
    public class Produto {

        public Produto() {

        }

        public Produto(string nome, string descricaoCurta, string descritaDetalhada, double preco, int emEstoque, string imagem, double peso, Categoria categoria) {
            Nome = nome;
            DescricaoCurta = descricaoCurta;
            DescritaDetalhada = descritaDetalhada;
            Preco = preco;
            EmEstoque = emEstoque;
            Imagem = imagem;
            Peso = peso;
            Categoria = categoria;
        }

        [Key]
        public int ProdutoId { get; set; }

        // [Required(ErrorMessage = "O nome do produto deve ser informado")]
        [Display(Name = "Nome do Produto")]
        // [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        public string Nome { get; set; }

        // [Required(ErrorMessage = "A descrição do produto deve ser informada")]
        [Display(Name = "Descrição do Produto")]
        // [MinLength(20, ErrorMessage = "Descrição deve ter no mínimo {1} caracteres")]
        // [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
        public string DescricaoCurta { get; set; }


        // [Required(ErrorMessage = "O descrição detalhada do produto deve ser informada")]
        [Display(Name = "Descrição detalhada do Produto")]
        
        // [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no mínimo {1} caracteres")]
        // [MaxLength(200, ErrorMessage = "Descrição detalhada pode exceder {1} caracteres")]
        public string DescritaDetalhada { get; set; }

        // [Required(ErrorMessage = "Informe o preço do produto")]
        [Display(Name = "Preço")]
        // [Column(TypeName = "double(18,2)")]
        // [Range(1, 99.99, ErrorMessage = "O preço deve estar entre 1 e 99,99")]
        public double Preco { get; set; }

        [Display(Name = "Estoque em numero de garrafas")]
        public int EmEstoque { get; set; }


        [Display(Name = "Imagem do Produto ")]
        public string Imagem { get; set; }

        [Display(Name = "Peso do Produto ")]
        public double Peso { get; set; }

        [Display(Name = "Categoria")]
        public Categoria Categoria { get; set; }


        public int CategoriaId { get; set; }





    }
}