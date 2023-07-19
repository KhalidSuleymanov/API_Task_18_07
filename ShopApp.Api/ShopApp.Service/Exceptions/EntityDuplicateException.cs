using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Service.Exceptions
{
    public class EntityDuplicateException:Exception
    {
        public EntityDuplicateException()
        {

        }

        public EntityDuplicateException(string message):base(message)
        {

        }
    }
}
