using Markdig;
using Microsoft.EntityFrameworkCore;
using PresentationApp.Data;
using PresentationApp.Hubs;
using PresentationApp.Services;


namespace PresentationApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSignalR();

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<SessionManager>();
            builder.Services.AddSingleton(new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());



            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();
                using var db = dbFactory.CreateDbContext();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapHub<PresentationHub>("/presentationhub");
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
