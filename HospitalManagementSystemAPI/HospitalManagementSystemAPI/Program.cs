using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Data;

namespace HospitalManagementSystemAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();

            // *** SQL CONNECTIONS *** //
            // EF SQL Connection
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer_EF")));
            //ADO.NET Connection
            builder.Services.AddTransient<IDbConnection>(sp => 
                new SqlConnection(builder.Configuration.GetConnectionString("SqlServerADO")));

            // *** MONGO CONNECTIONS *** //
            // MongoDB Connection
            builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("Mongo"));
            builder.Services.AddSingleton<IMongoClient>(sp =>
                new MongoClient(sp.GetRequiredService<IOptions<MongoSettings>>().Value.ConnectionString));
            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IMongoClient>()
                    .GetDatabase(sp.GetRequiredService<IOptions<MongoSettings>>().Value.DatabaseName));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}
