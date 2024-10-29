using DataAccessLayers.UnitOfWork;
using DataAccessLayers;
using Services.Interface;
using Services.Service;

using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BusinessObjects;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repository
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped<PromotionRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<SolvedTicketRepository>();
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<TransactionHistoryRepository>();
builder.Services.AddScoped<TransactionTypeRepository>();
builder.Services.AddScoped<ShowTimeRepository>();
builder.Services.AddScoped<SeatRepository>();
builder.Services.AddScoped<CinemaRoomRepository>();

//UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add Db Context

builder.Services.AddDbContext<Prn221projectContext> ();

//Service
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISolvedTicketService, SolvedTicketService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionHistoryService, TransactionHIstoryService>();
builder.Services.AddScoped<ITransactionTypeService, TransactionTypeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IShowTimeService, ShowTimeService>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<ICinemaRoomService, CinemaRoomService>();


// Add Jwt Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<Prn221projectContext>();
    context.Database.Migrate();
}

app.Run();
