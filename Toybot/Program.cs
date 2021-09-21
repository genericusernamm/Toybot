using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Nefarius.DSharpPlus.CommandsNext.Extensions.Hosting;
using Nefarius.DSharpPlus.Extensions.Hosting;
using Nefarius.DSharpPlus.Interactivity.Extensions.Hosting;
using Nefarius.DSharpPlus.SlashCommands.Extensions.Hosting;
using OpenTracing;
using OpenTracing.Mock;
using Toybot.Commands;
using Toybot.Data;
using Toybot.Services;
using Toybot.ApplicationCommands;

namespace Toybot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //Adding DbContext
                    services.AddDbContext<ApplicationDbContext>(options => 
                        options.UseNpgsql(
                            hostContext.Configuration.GetConnectionString("DefaultConnection"))
                    );
                    
                    //Adding our database services.
                    services.AddScoped<ITagService, TagService>()
                        .AddScoped<IRoleConfigService, RoleConfigService>();

                    //The hosting extension needs a tracer so we are providing a mock tracer.
                    services.AddSingleton<ITracer>(provider => new MockTracer());
                    
                    
                    //Creating the Discord Client instance.
                    services.AddDiscord(options =>
                    {
                        options.Token = hostContext.Configuration["DiscordToken"];
                    });
                    
                    //Adding Discord as a hosted service (Automatically added at startup)
                    services.AddDiscordHostedService();
                    
                    //Setting up Commands and registering our modules
                    services.AddDiscordCommandsNext(options =>
                        {
                            options.StringPrefixes = new[] {"!"};
                        }, extension =>
                        {
                            extension.RegisterCommands<PingModule>();
                            extension.RegisterCommands<TagModule>();
                        }
                    );
                    
                    //Setting up SlashCommands and Context Menus.
                    services.AddDiscordSlashCommands(options =>
                    {
                        options.Services = services.BuildServiceProvider();
                    }, extension =>
                    {
                        extension.RegisterCommands<SlashCommandsTagModule>(719334790129647720);
                        extension.RegisterCommands<SlashCommandsConfigModule>(719334790129647720);
                        extension.RegisterCommands<SlashCommandsGravelModule>(719334790129647720);
                        extension.RegisterCommands<SlashCommandsTagModule>(783751280132227083);
                        extension.RegisterCommands<SlashCommandsConfigModule>(783751280132227083);
                        extension.RegisterCommands<SlashCommandsGravelModule>(783751280132227083);
                    });

                    services.AddDiscordInteractivity(options =>
                    {
                    }, extension =>
                    {
             
                    });


                });
    }
}