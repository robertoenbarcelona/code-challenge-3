
namespace Challenge3.AppService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppRentService
    {
        OperationMessage RentProduct(string productId, string userID);

        OperationMessage ReturnProduct(string productId, string userID, DateTime dateTime);
    }
}
