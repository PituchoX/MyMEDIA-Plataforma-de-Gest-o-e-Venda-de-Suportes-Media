namespace GestaoLoja.Entities
{
    
    public class Subcategoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Imagem { get; set; }

        // Ligação à categoria pai
        public int CategoriaId { get; set; }
        public Categorias? Categoria { get; set; }

        // Produtos desta subcategoria
        public ICollection<Produtos>? Produtos { get; set; }
    }
}
