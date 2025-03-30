using Serilog.Context;

namespace Api.GRRInnovations.TaskQueue.Processor.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationHeader = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(CorrelationHeader, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers[CorrelationHeader] = correlationId;
            }

            context.Items[CorrelationHeader] = correlationId;
            context.Response.Headers[CorrelationHeader] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }
    }
}
