

using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Models {

    [Table("PedidoDetalhes")]
    public class PedidoDetalhe {


        public int PedidoDetalheId { get; set; }

        public int PedidoId { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public double Preco { get; set; }

        public virtual Produto Produto { get; set; }

        public virtual Pedido Pedido { get; set; }

    }
}