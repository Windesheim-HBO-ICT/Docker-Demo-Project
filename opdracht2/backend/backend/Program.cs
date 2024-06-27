using backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

// add dbcontext
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options => {
    // add cors policy to allow specific configuration
    options.AddPolicy("MyAllowSpecificOrigins",
        builder => {
            builder
                .WithOrigins("http://localhost:4080") // specifying the allowed origins
                .WithMethods("POST", "GET", "PUT", "DELETE", "OPTIONS") // defining the allowed HTTP methods
                .AllowAnyHeader() // allowing any header to be sent
                .AllowCredentials(); // allowing credentials
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope()) {
    IServiceProvider services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();

    // migrate & seed database
    await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors("MyAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
