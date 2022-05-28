using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Models {
    
    [Table("CarrinhoCompraItens")]
    public class CarrinhoCompraItem {

        public int CarrinhoCompraItemId { get; set; }

        public Produto Produto { get; set; }
        
        public int Quantidade { get; set; }

        //[StringLength(100)]
        public string CarrinhoCompraId { get; set; }// string GUID

    }
}