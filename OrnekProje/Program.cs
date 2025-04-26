using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrnekProje.Data;
using OrnekProje.Interfaces;
using OrnekProje.Models.Entities;
using OrnekProje.Repositories;

namespace OrnekProje
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // DbContext tanýmlanmasý
            builder.Services.AddDbContext<OrnekProjeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

            // Id int olarak verildiginden tanýmlamasý
            //builder.Services.AddIdentity<User, IdentityRole<int>>()
            //    .AddEntityFrameworkStores<OrnekProjeDbContext>();

            builder.Services
    .AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<OrnekProjeDbContext>();


            // DI eklenmesi - GenericRepositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // DI eklenmesi -BookRepository için
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // Kullanýcý bilgisinin gitmesi için!
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Book}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
