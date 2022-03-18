using System.Data.Entity;

namespace GlobalLoggingWithSerilog.Data
{
    public class NomeContext : DbContext
    {

       // public NomeContext(DbContextOptions<NomeContext> options)
        //  : base(options) {
       //     DbInterception.Add(new LoggerEntityFrameworkInterceptor());
       // }

    }
}
