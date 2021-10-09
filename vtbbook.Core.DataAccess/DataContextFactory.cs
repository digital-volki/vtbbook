using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using vtbbook.Core.Common;
using vtbbook.Core.Common.Environment;

namespace vtbbook.Core.DataAccess
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContextFactory()
        {
        }

        public DataContext CreateDbContext(string[] args)
        {
            var settings = new Settings();
            var devConnectionStrings = new Dictionary<Environment, string>
            {
                {
                    Environment.Development,
                    "Server=192.168.31.236;User Id=dev;Password=FNomoktY4vhXM88trNFCeKXaQm;Port=5435;Database=dev;"
                }
            };

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(devConnectionStrings[EnvironmentGetter.DevCurrentEnv]);

            return new DataContext(optionsBuilder.Options, settings);
        }
    }
}
