namespace SoftoneStudentManagmentSystem.Middleware
{
    public class GlobalExceptionHandlerMiddleware:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                //handle exception
                throw new Exception("Error encountered in " +ex.Message, ex);
            }
        }
    }
}
