namespace Cal.Middleware
{
    public class PathCorrectionMiddleware
    {
        private readonly RequestDelegate _next;

        public PathCorrectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Проверяем, был ли получен ответ "404 Not Found"
            if (context.Response.StatusCode == 404)
            {
                // Получаем путь, который привел к 404 ошибке
                string path = context.Request.Path;

                // Далее можно выполнить необходимые действия с этим путем,
                // например, записать его в журнал или обработать по своему усмотрению.

                // Выводим информацию о несуществующем пути в консоль
                Console.WriteLine($"404 Error: Path '{path}' was not found.");
            }

            // Передаем запрос следующему Middleware в цепочке
            await _next(context);
        }
    }

    public static class UsePathCorrectionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePathCorrectionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PathCorrectionMiddleware>();
        }
    }
}
