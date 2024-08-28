using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using TDTU_Chat.Data;
using TDTU_Chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký SignalR services
builder.Services.AddSignalR();
// Đăng ký DbContext xài Sql Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Đăng ký Defaultidentity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
// Đăng ký MVC helper
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình routing SignalR (nằm trong /chatHub)
app.MapHub<ChatHub>("/chatHub");

// Cấu hình middlewarre
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Cho phép truy cập các file tĩnh
app.UseStaticFiles();

// Kích hoạt định tuyến
app.UseRouting();

app.UseAuthentication();  // Thêm middleware xác thực
app.UseAuthorization();   // Thêm middleware phân quyền

//Route mặc định trang chủ ở Home/Index.cshtml
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Kích hoạt Razor Pages nếu cần
app.MapRazorPages(); 

app.Run();
