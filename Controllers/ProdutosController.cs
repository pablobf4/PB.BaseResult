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
            new ProdutoDTO { codigo = 1, Nome = "Produto A", Marca = "Marca X" },
            new ProdutoDTO { codigo = 2, Nome = "Produto B", Marca = "Marca Y" },
            new ProdutoDTO { codigo = 3, Nome = "Produto C", Marca = "Marca X" },
            new ProdutoDTO { codigo = 4, Nome = "Produto D", Marca = "Marca Z" },
            new ProdutoDTO { codigo = 5, Nome = "Produto E", Marca = "Marca Y" }
        };

        [ProducesResponseType(typeof(Result<PagedResult<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("get-by-filter-async")]
        public IActionResult GetPagedProducts([FromQuery] PaginationFilterDTO paginationFilter)
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

                if (!pagedData.Any())
                {
                    var noDataResponse = new Result<string>(
                        new Message("Nenhum produto encontrado")
                    );
                    return NotFound(noDataResponse);
                }

                var response = new PagedResult<ProdutoDTO>(pagedData, totalPages, pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new Result<string>(
                    new Message($"Erro ao recuperar produtos: {ex.Message}")
                );
                return StatusCode(500, errorResponse);
            }
        }


        [HttpGet("{nome}")]
        public IActionResult GetProductByName(string nome)
        {
            try
            {
                var produto = _produtos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

                if (produto == null)
                {
                    var notFoundResponse = new Result<string>(
                        new Message($"Produto '{nome}' não encontrado")
                    );
                    return NotFound(notFoundResponse);
                }

                var successResponse = new Result<ProdutoDTO>(produto, new Message("Produto encontrado com sucesso"));



                return Ok(successResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = new Result<string>(
                    new Message($"Erro ao recuperar o produto: {ex.Message}")
                );
                return StatusCode(500, errorResponse);
            }
        }
    }



}
