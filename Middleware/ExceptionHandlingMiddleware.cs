using PB.BaseResult.Communication;

namespace PB.BaseResult.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResponse = Result<string>.Fail(
                    $"Erro interno do servidor: {ex.Message}",
                    MessageTypeEnum.ERROR
                );

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }

}
