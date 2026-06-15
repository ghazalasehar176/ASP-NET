using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class14
{
  internal class DbConnection
    {

        //private readonly string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LoginForm;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0";

        public string GetConnection() {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LoginForm;Integrated Security=True;";
        }
    }
}
