using Candy_Shop.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(
  options => options.UseSqlite("Data Source=db.db")
);

var connectionStringBuilder = new SqliteConnectionStringBuilder() {
  DataSource = "db.db"
};

// using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString)) {
//   connection.Open();
//
//   string sql = "INSERT INTO Czekoladki VALUES(1, 'test', 'mleczna', 'laskowe', 'truskawka', 'zajebiste', '69zł', 21.37)";
//   var Command = connection.CreateCommand();
//   Command.CommandText = sql;
//   Command.ExecuteNonQuery();
//   
//   connection.Close();
// };

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

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
