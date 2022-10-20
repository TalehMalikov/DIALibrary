namespace Library.WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string path = @"C:\Log\httplogs.txt";
                string content =
                    $"\n\nException Date: {DateTime.Now.ToString("g")}\nException Message: {ex.Message}\n\n--------------------------";
                File.AppendAllText(path,content);
            }
        }
    }
}