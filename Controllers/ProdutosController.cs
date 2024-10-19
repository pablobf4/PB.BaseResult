using Microsoft.AspNetCore.Mvc;
using PB.BaseResult.Communication;
using PB.BaseResult.DTO;

namespace PB.BaseResult.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly List<ProdutoDTO> _produtos = new()
        {
            new ProdutoDTO { Codigo = 1, Nome = "Produto A", Marca = "Marca X" },
            new ProdutoDTO { Codigo = 2, Nome = "Produto B", Marca = "Marca Y" },
            new ProdutoDTO { Codigo = 3, Nome = "Produto C", Marca = "Marca X" },
            new ProdutoDTO { Codigo = 4, Nome = "Produto D", Marca = "Marca Z" },
            new ProdutoDTO { Codigo = 5, Nome = "Produto E", Marca = "Marca Y" }
        };

        [ProducesResponseType(typeof(Result<PagedResult<ProdutoDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("get-by-filter-async")]
        public IActionResult GetPagedProducts([FromQuery] PaginatedResult paginationFilter)
        {
            try
            {
                var pageNumber = paginationFilter?.PageNumber > 0 ? paginationFilter.PageNumber : 1;
                var pageSize = paginationFilter?.PageSize > 0 ? paginationFilter.PageSize : 10;

                var totalRecords = _produtos.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                var pagedData = _produtos
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var pagedResult = new PagedResult<ProdutoDTO>(pagedData, totalPages, pageNumber, pageSize);
                if (!pagedResult.HasData)
                {
                    return Ok(Result<PagedResult<ProdutoDTO>>.Fail(
                        "Nenhum produto encontrado.",
                        MessageTypeEnum.INFO
                    ));
                }

                return Ok(Result<PagedResult<ProdutoDTO>>.Success(
                    pagedResult,
                    "Produtos recuperados com sucesso."
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Result<string>.Fail(
                        $"Erro ao recuperar produtos: {ex.Message}",
                        MessageTypeEnum.ERROR
                    )
                );
            }
        }


        [HttpGet("{nome}")]
        public IActionResult GetProductByName(string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return BadRequest(Result<string>.Fail("O nome do produto não pode ser vazio ou nulo.", MessageTypeEnum.WARNING));

                var produto = _produtos.FirstOrDefault(p =>
                    p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

                if (produto == null)
                    return NotFound(Result<string>.Fail($"Produto '{nome}' não encontrado.", MessageTypeEnum.INFO));

                var produtoDTO = new ProdutoDTO
                {
                    Codigo = produto.Codigo,
                    Nome = produto.Nome,
                    Marca = produto.Marca
                };

                return Ok(Result<ProdutoDTO>.Success("Produto encontrado com sucesso."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    Result<string>.Fail($"Erro ao recuperar o produto: {ex.Message}", MessageTypeEnum.ERROR));
            }
        }

    }
}
