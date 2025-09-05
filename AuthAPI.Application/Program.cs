using AuthAPI.Application.UoWs;
using AuthAPI.Domain.Commons;
using AuthAPI.Domain.Contracts;
using AuthAPI.Domain.Repository;
using AuthAPI.Domain.Services;
using AuthAPI.Infrastructure.ACL;
using AuthAPI.Infrastructure.Contexts;
using AuthAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Zack.Commons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MySqlDbContext>(p =>
{
    string? ConnStr = builder.Configuration.GetConnectionString("ConnStr");
    p.UseSqlServer(ConnStr);
});
builder.Services.AddMediatR(p =>
{
    p.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg4NTY2NDAwIiwiaWF0IjoiMTc1NzAzOTc0MSIsImFjY291bnRfaWQiOiIwMTk5MTdiOWI3MjQ3ZmNkYjVkMmIxMmQ0YTk0NjMwOCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazRidm5qdGdrcnlzcHc0OTYzYTJoYXltIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.dYI-Ptur0IC2nYUG0IcPEOqtGfLWokqx_mm3cxMrDs2EH3xMH4D1nXIdTNetkIRuN_LwOPdrATUS349l6vdnk1lCyv5dyBvyIuJGRWc4UdEuqTRXyrhE04ueT73JkEJf9CSn2T4AQtCooIMvrBtbL9J8FkHLoeK3kfPKpDBQnOu4aDzOY4meULcDt8cjpF58qgRxGkpTJZCIJEVn6MSE4TGu6xlOx1xeqWSgyUwvEZ5eUjGwwJazlI-hGgrAMtJs3yZE4xy3SaVHN1vOjx1XHgm2WRhfh-DcPg3qTaRwD2k3XhUedOGTEqFhR4lli4uCghdqU41lv188VZOKbgXIyA";
    p.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
});
builder.Services.AddScoped<IHashHelper, HashService>();
builder.Services.AddScoped<IUserAccessFail, AccessFailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserDomainService>();
builder.Services.AddScoped<ISmsCodeSender, SmsCodeSender>();
builder.Services.AddScoped<IUserDomainRepository, UserDomainRepository>();
builder.Services.Configure<MvcOptions>(p =>
{
    p.Filters.Add<DbFilter>();
});
builder.Services.AddCors(p =>
{
    p.AddDefaultPolicy(x =>
    {
        x.AllowAnyOrigin().
        AllowAnyMethod()
        .AllowAnyHeader();
	});
});
builder.Services.AddStackExchangeRedisCache(p =>
{
    p.InstanceName = "Auth_";
    p.Configuration = "localhost";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
