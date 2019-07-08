using System;
using System.Text;
using System.Linq;
using Chloe;
using Chloe.SqlServer;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using common.mdl;

namespace ctest
{
    class Program
    {
        private static IConfiguration appConfiguration;
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            new HostBuilder().ConfigureAppConfiguration((context, builder) => 
            {
                var hostingEnviroment = context.HostingEnvironment;
                appConfiguration = AppConfigurations.Get(hostingEnviroment.ContentRootPath, hostingEnviroment.EnvironmentName);
            })
            .Build();

            Console.Read();
        }

        static void LetMeKK(string mixstr)
        {
            Encoding[] encodings = new Encoding[] { Encoding.Default, Encoding.UTF32, Encoding.UTF7, Encoding.UTF8, Encoding.Unicode, Encoding.GetEncoding("gb2312") };
            foreach (var encoding in encodings)
            {
                foreach (var encoding2 in encodings)
                {
                    Console.WriteLine(encoding.EncodingName + " get bytes, " + encoding2.EncodingName + " GetString\n" + encoding2.GetString(encoding.GetBytes(mixstr)));
                }
                Console.WriteLine();
            }
        }

        static async void SqlServerTableDefineToHTML()
        {
            var htmlTask = GetHtmlDocOriginAsync();

            var connectString = appConfiguration.GetConnectionString("mssql");
            var dbcontext = new MsSqlContext(new DefaultDbConnectionFactory(connectString));
            //数据库名
            var dbname = dbcontext.SqlQuery<string>("select db_name()").First();
            
            var html = await htmlTask;
        }

        static async Task<string> GetHtmlDocOriginAsync()
        {
            var path_index = System.IO.Directory.GetCurrentDirectory();
            return await File.ReadAllTextAsync(Path.Combine(path_index, "template.html"));
        }
    }
}
