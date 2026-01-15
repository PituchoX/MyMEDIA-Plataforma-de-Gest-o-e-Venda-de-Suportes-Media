using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfulAPIPWeb.Data;
using RESTfulAPIPWeb.Dtos;
using RESTfulAPIPWeb.Repositories.Interfaces;

namespace RESTfulAPIPWeb.Controllers
{

    /// Controller para consulta de produtos da API MyMEDIA

    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repo;
        private readonly AppDbContext _context;

        public ProdutosController(IProdutoRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }


        /// Lista todos os produtos ATIVOS
   
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutos()
        {
            var produtos = await _context.Produtos
                .Where(p => p.Estado == "Ativo")
                .Include(p => p.Categoria)
                .Include(p => p.Subcategoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .ToListAsync();

            var result = produtos.Select(p => new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                PrecoBase = p.PrecoBase,
                Percentagem = p.Percentagem,
                PrecoFinal = p.PrecoFinal,
                Estado = p.Estado,
                Stock = p.Stock,
                Imagem = p.Imagem,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria?.Nome,
                SubcategoriaId = p.SubcategoriaId,
                SubcategoriaNome = p.Subcategoria?.Nome,
                ModoEntregaId = p.ModoEntregaId,
                ModoEntregaNome = p.ModoEntrega?.Nome,
                FornecedorId = p.FornecedorId,
                FornecedorNome = p.Fornecedor?.NomeEmpresa
            });

            return Ok(result);
        }

    
        /// Pesquisa produtos por nome ou categoria
   
        [HttpGet("pesquisa")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> PesquisarProdutos([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return Ok(new List<ProdutoDto>());

            var termoLower = q.ToLower();

            var produtos = await _context.Produtos
                .Where(p => p.Estado == "Ativo" && 
                    (p.Nome.ToLower().Contains(termoLower) || 
                     (p.Categoria != null && p.Categoria.Nome.ToLower().Contains(termoLower)) ||
                     (p.Subcategoria != null && p.Subcategoria.Nome.ToLower().Contains(termoLower))))
                .Include(p => p.Categoria)
                .Include(p => p.Subcategoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .ToListAsync();

            var result = produtos.Select(p => new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                PrecoBase = p.PrecoBase,
                Percentagem = p.Percentagem,
                PrecoFinal = p.PrecoFinal,
                Estado = p.Estado,
                Stock = p.Stock,
                Imagem = p.Imagem,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria?.Nome,
                SubcategoriaId = p.SubcategoriaId,
                SubcategoriaNome = p.Subcategoria?.Nome,
                ModoEntregaId = p.ModoEntregaId,
                ModoEntregaNome = p.ModoEntrega?.Nome,
                FornecedorId = p.FornecedorId,
                FornecedorNome = p.Fornecedor?.NomeEmpresa
            });

            return Ok(result);
        }

 
        /// Lista produtos por categoria

        [HttpGet("categoria/{categoriaId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutosPorCategoria(int categoriaId)
        {
            var produtos = await _context.Produtos
                .Where(p => p.CategoriaId == categoriaId && p.Estado == "Ativo")
                .Include(p => p.Categoria)
                .Include(p => p.Subcategoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .ToListAsync();

            var result = produtos.Select(p => new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                PrecoBase = p.PrecoBase,
                Percentagem = p.Percentagem,
                PrecoFinal = p.PrecoFinal,
                Estado = p.Estado,
                Stock = p.Stock,
                Imagem = p.Imagem,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria?.Nome,
                SubcategoriaId = p.SubcategoriaId,
                SubcategoriaNome = p.Subcategoria?.Nome,
                ModoEntregaId = p.ModoEntregaId,
                ModoEntregaNome = p.ModoEntrega?.Nome,
                FornecedorId = p.FornecedorId,
                FornecedorNome = p.Fornecedor?.NomeEmpresa
            });

            return Ok(result);
        }


        /// Lista produtos por subcategoria

        [HttpGet("subcategoria/{subcategoriaId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutosPorSubcategoria(int subcategoriaId)
        {
            var produtos = await _context.Produtos
                .Where(p => p.SubcategoriaId == subcategoriaId && p.Estado == "Ativo")
                .Include(p => p.Categoria)
                .Include(p => p.Subcategoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .ToListAsync();

            var result = produtos.Select(p => new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                PrecoBase = p.PrecoBase,
                Percentagem = p.Percentagem,
                PrecoFinal = p.PrecoFinal,
                Estado = p.Estado,
                Stock = p.Stock,
                Imagem = p.Imagem,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria?.Nome,
                SubcategoriaId = p.SubcategoriaId,
                SubcategoriaNome = p.Subcategoria?.Nome,
                ModoEntregaId = p.ModoEntregaId,
                ModoEntregaNome = p.ModoEntrega?.Nome,
                FornecedorId = p.FornecedorId,
                FornecedorNome = p.Fornecedor?.NomeEmpresa
            });

            return Ok(result);
        }

   
        /// Obtém produto em destaque (aleatório)
 
        [HttpGet("destaque")]
        [AllowAnonymous]
        public async Task<ActionResult<ProdutoDto>> GetProdutoDestaque()
        {
            var produtos = await _context.Produtos
                .Where(p => p.Estado == "Ativo" && p.Stock > 0)
                .Include(p => p.Categoria)
                .Include(p => p.Subcategoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .ToListAsync();

            if (!produtos.Any())
                return NotFound(new { Message = "Nenhum produto disponível." });

            var random = new Random();
            var p = produtos[random.Next(produtos.Count)];

            return Ok(new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                PrecoBase = p.PrecoBase,
                Percentagem = p.Percentagem,
                PrecoFinal = p.PrecoFinal,
                Estado = p.Estado,
                Stock = p.Stock,
                Imagem = p.Imagem,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria?.Nome,
                SubcategoriaId = p.SubcategoriaId,
                SubcategoriaNome = p.Subcategoria?.Nome,
                ModoEntregaId = p.ModoEntregaId,
                ModoEntregaNome = p.ModoEntrega?.Nome,
                FornecedorId = p.FornecedorId,
                FornecedorNome = p.Fornecedor?.NomeEmpresa
            });
        }


        /// Obtém um produto pelo ID
   
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProdutoDto>> GetProduto(int id)
        {
            var p = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Subcategoria)
                .Include(p => p.ModoEntrega)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null) return NotFound();

            if (p.Estado != "Ativo")
                return NotFound();

            return Ok(new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                PrecoBase = p.PrecoBase,
                Percentagem = p.Percentagem,
                PrecoFinal = p.PrecoFinal,
                Estado = p.Estado,
                Stock = p.Stock,
                Imagem = p.Imagem,
                CategoriaId = p.CategoriaId,
                CategoriaNome = p.Categoria?.Nome,
                SubcategoriaId = p.SubcategoriaId,
                SubcategoriaNome = p.Subcategoria?.Nome,
                ModoEntregaId = p.ModoEntregaId,
                ModoEntregaNome = p.ModoEntrega?.Nome,
                FornecedorId = p.FornecedorId,
                FornecedorNome = p.Fornecedor?.NomeEmpresa
            });
        }
    }
}
