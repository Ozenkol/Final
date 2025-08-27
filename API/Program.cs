using Application.Services;
using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;   
using DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;   
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme   
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement    
    {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
    Array.Empty<string>()
    }
    });
}); 

builder.Services.AddControllers();   
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<FinalDbContext>(options =>

    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(FinalDbContext)))
);

builder.Services.AddIdentity<UserEntity, IdentityRole>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

})
    .AddEntityFrameworkStores<FinalDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {   
        options.TokenValidationParameters = new TokenValidationParameters   
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration["JWT:SigningKey"]))   
        };
    }
);


builder.Services.AddAuthorization();


builder.Services.AddScoped<IInventoriesRepository, InventoriesRepository>();
builder.Services.AddScoped<IInventoriesService, InventoriesService>();
builder.Services.AddScoped<IFieldsRepository, FieldsRepository>();
builder.Services.AddScoped<IFieldsService, FieldsService>();
builder.Services.AddScoped<IValuesRepository, ValuesRepository>();
builder.Services.AddScoped<IValuesService, ValuesService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();  

app.Run();