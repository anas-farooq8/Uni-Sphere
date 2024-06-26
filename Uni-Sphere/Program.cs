using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Uni_Sphere.DataAccess.Data;
using Uni_Sphere.Repositories;
using Uni_Sphere.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Injecting the DbContext into the services container
builder.Services.AddDbContext<UniSphereDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UniSphereConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UniSphereAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

// Password Config
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Shared/Account/Login";
    options.LogoutPath = $"/Shared/Account/Logout";
    options.AccessDeniedPath = "/Shared/Account/AccessDenied";
});

// Repository Injection
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ITeacherFunctionality, TeacherFunctionality>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<ITeacherAttendanceRepository, TeacherAttendanceRepository>();
builder.Services.AddScoped<IStudentFunctionality, StudentFunctionality>();
builder.Services.AddScoped<IStudentCoursesRepository, StudentCoursesRepository>();


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Shared}/{controller=Home}/{action=Index}/{id?}");

app.Run();
