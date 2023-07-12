using ISysDataBaseBack.DBContext;
using ISysWebAppBack.Services;
using Microsoft.EntityFrameworkCore;

namespace ISysWebAppBack.DBContext
{
    /// <summary>
    /// child class for delivering db context
    /// </summary>
    public class DBContextWeb : DataBaseContext
    {
        public static readonly ILoggerFactory loggerFactory =
        LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });
        public DBContextWeb() : base() { }
        public DBContextWeb(DbContextOptions<DataBaseContext> options)
                : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseNpgsql(Config.ConnectionString);
        }
    }
}
