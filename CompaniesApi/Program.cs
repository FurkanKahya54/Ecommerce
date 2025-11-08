using CompaniesApi.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContextFactory<AppDb_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContextFactory<AppDb2_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db2Connection")));



builder.Services.AddDbContextFactory<AppDb3_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db3Connection")));

builder.Services.AddDbContextFactory<AppDb4_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db4Connection")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
