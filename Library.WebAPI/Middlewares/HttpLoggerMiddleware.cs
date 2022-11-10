using Microsoft.IO;
using System.Diagnostics;

namespace Library.WebAPI.Middlewares
{
    public class HttpLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpLoggerMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public HttpLoggerMiddleware(RequestDelegate next, ILogger<HttpLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);

            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using (var requestStream = _recyclableMemoryStreamManager.GetStream())
            {
                await context.Request.Body.CopyToAsync(requestStream);

                string requestBody = ReadStreamInChunks(requestStream);

                _logger.LogInformation("request received. {RequestUrl} {RequestBody} {QueryString}", context.Request.Path, requestBody, context.Request.QueryString);

                context.Request.Body.Position = 0;
            }
        }

        private async Task LogResponse(HttpContext context)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var originalBodyStream = context.Response.Body;

            await using (var responseBodyStream = _recyclableMemoryStreamManager.GetStream())
            {
                context.Response.Body = responseBodyStream;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);

                string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation("response replied {ResponseBody} {TotalMilliseconds}", responseBody, sw.Elapsed.TotalMilliseconds);

                await responseBodyStream.CopyToAsync(originalBodyStream);
            }
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using (var textWriter = new StringWriter())
            {
                using (var reader = new StreamReader(stream))
                {
                    var readChunk = new char[readChunkBufferLength];
                    int readChunkLength;

                    do
                    {
                        readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                        textWriter.Write(readChunk, 0, readChunkLength);
                    } while (readChunkLength > 0);

                    return textWriter.ToString();
                }
            }
        }
    }
}
