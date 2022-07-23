using BusinessObject.DataAccess;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.    
builder.Services.AddControllersWithViews();
//ADD DBCONTEXT
builder.Services.AddDbContext<FStoreContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("MyStockWeb")
    ));
//ADD SCOPED DBCONTEXT
builder.Services.AddScoped(typeof(FStoreContext));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddSession();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddMvc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
