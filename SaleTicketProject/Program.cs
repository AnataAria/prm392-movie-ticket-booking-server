using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using Services.Service;

var builder = WebApplication.CreateBuilder(args);

//Database
//Repository
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped<PromotionRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<SolvedTicketRepository>();
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<TransactionHistoryRepository>();
builder.Services.AddScoped<TransactionTypeRepository>();

//UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Service
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISolvedTicketService, SolvedTicketService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionHistoryService, TransactionHIstoryService>();
builder.Services.AddScoped<ITransactionTypeService, TransactionTypeService>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
