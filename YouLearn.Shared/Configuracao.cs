using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Shared
{
    public static class Configuracao
    {
        //public static string ConnectionString = @"Server=tcp:johnatan.database.windows.net,1433;Initial Catalog=YouLearn;Persist Security Info=False;User ID=johnatan;Password=Johnbrow123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static string ConnectionString = @"Server=.\sqlexpress;Database=YouLearn;Trusted_Connection=True;";

    }
}
