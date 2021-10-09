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
                    "Server=65.108.55.209;User Id=vtbbook_db;Password=FNomoktY4vhXM88trNFCeKXaQm;Port=5432;Database=vtbbook_db;"
                }
            };

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(devConnectionStrings[Environment.Development]);

            return new DataContext(optionsBuilder.Options, settings);
        }
    }
}
