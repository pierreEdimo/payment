using System.Reflection;
using payment.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using payment.Repositories.WalletRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddTransient<IWalletRepository, WalletRepository>();
builder.Services.AddDbContext<PayMentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("database")));
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Houlala Payment",
        Description = "Houlala Payment is an api built in order to solve a digital payment Issue" +
                      "for the Houlala ecosystem. It will alllow users to pay with all the Payment provider possible, such as " +
                      "Orange Money, Mtn Mobile Money etc... Inclusive the self developped Houlala Wallet",
        License = new OpenApiLicense()
        {
            Name = "MIT"
        },
        Contact = new OpenApiContact()
        {
            Name = "Pierre Patrice Emmanuel Edimo Nkoe",
            Email = "pierredimo@live.com",
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();

app.UseResponseCaching();

app.MapControllers();

app.Run();
