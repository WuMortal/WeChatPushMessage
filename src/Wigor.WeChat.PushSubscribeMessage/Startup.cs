using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wigor.WeChat.PushSubscribeMessage.Core;
using Wigor.WeChat.PushSubscribeMessage.Filter;
using Wigor.WeChat.PushSubscribeMessage.Models;

namespace Wigor.WeChat.PushSubscribeMessage
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region >> 注入全局异常过滤器
            services.AddMvc(option => { option.Filters.Add(typeof(GlobalExceptionFilter)); });
            #endregion

            #region >> 注入 WeChatSettings
            services.Configure<WeChatSettings>(Configuration.GetSection("WeChatSettings"));
            #endregion

            #region >> 注入 WeChatHttpClient
            services.AddHttpClient<IWeChatHttpClient, WeChatHttpClient>();
            #endregion

            #region >> 注入 WeChatHelper
            services.AddSingleton<WeChatHelper>();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            #region >> 生命周期

            lifetime.ApplicationStarted.Register(() =>
            {
                _logger.LogDebug("application started");
                _logger.LogError("application started");
                _logger.LogInformation("application started");
                _logger.LogWarning("application started");
                _logger.LogCritical("application started");
            });
            lifetime.ApplicationStopping.Register(() =>
            {
                _logger.LogCritical("application stopping");
            });
            lifetime.ApplicationStopped.Register(() =>
            {
                _logger.LogCritical("application stopped");
            });

            #endregion

            app.UseMvc();
        }
    }
}
