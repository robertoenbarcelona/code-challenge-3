using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge3.AppService
{
    public interface IAppProductService
    {
        OperationMessage Register(string productId, string userID);
    }
}
