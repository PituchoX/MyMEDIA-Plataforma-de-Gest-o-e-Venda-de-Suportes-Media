namespace GestaoLoja.Entities
{
    public class LinhasVenda
    {
        public int Id { get; set; }

        public int? VendaId { get; set; }      
        public Vendas? Venda { get; set; }

        public int? ProdutoId { get; set; }   
        public Produtos? Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal Preco { get; set; }
    }
}
