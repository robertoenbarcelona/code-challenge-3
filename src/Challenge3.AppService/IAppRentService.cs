
namespace Challenge3.AppService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAppRentService
    {
        OperationMessage Hire(string bookId, string userID);
    }
}
