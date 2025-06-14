namespace FastkartAPI.Middlewares
{
    public class ErrorHandlingMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddlware> _logger;

        public ErrorHandlingMiddlware(RequestDelegate next, ILogger<ErrorHandlingMiddlware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.Redirect("/page/404");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при обработке запроса");

                // Обработка 500 ошибок
                context.Response.Redirect("/page/504");
            }
        }
    }
}
