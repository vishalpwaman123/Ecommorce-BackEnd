using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Helper
{
    public class SqlQueries
    {
        static IConfiguration queriesConfig = new ConfigurationBuilder().AddXmlFile("SqlQueries.xml", true, true).Build();
        
        public static string SignUp { get { return queriesConfig["SignUp"]; } }
        public static string Login { get { return queriesConfig["Login"]; } }
    }
}
