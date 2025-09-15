using Serilog;

namespace AutoGo.Api.Extentions
{
    public static class ApiExtentintions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //عشان نتبع ال endpoint والوقت بتاعها 
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;
                options.TrackConnectionOpenClose = true;
            }).AddEntityFramework();


            //استخدام ال serilog عشان نعمل logging 
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console() // عرض في الكونسول
                        .WriteTo.Seq("http://localhost:5341") // رابط الـ Seq
                        .Enrich.FromLogContext()
                        .Enrich.WithEnvironmentName()
                        .Enrich.WithMachineName()
                        .Enrich.WithThreadId()
                        .CreateLogger();


            //cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            // segnalr
            //services.AddSignalR();
            return services;
        }

    }
}
