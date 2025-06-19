using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Data;


namespace HospitalManagementSystemAPI
{
    public class MongoSettings { public string ConnectionString { get; set; } public string DatabaseName { get; set; } };
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();


            // *** SQL CONNECTIONS *** //
            // EF SQL Connection - connection strings are stored in appsettings.json
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer_EFConnection")));

            //ADO.NET Connection
            builder.Services.AddTransient<IDbConnection>(sp => 
                new SqlConnection(builder.Configuration.GetConnectionString("SqlServer_ADOConnection")));

            // *** MONGO CONNECTIONS *** //
            // MongoDB Connection - connection strings are stored in appsettings.json

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
