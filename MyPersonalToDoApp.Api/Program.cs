using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyPersonalToDoApp.Api.StartupConfig;
using MyPersonalToDoApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
Services.Configure(builder);

var app = builder.Build();

Pipeline.Configure(app);
app.Run();


    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        var host = CreateHostBuilder(args).Build();

    //        host.Run();
    //    }

    //    public static IHostBuilder CreateHostBuilder(string[] args) =>
    //        Host.CreateDefaultBuilder(args)
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder.UseStartup<Startup>();
    //            });
    //}

