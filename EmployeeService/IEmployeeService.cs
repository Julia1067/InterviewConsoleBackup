using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace EmployeeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetEmployeeById?id={id}",
            BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GetEmployeeById(int id);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetEmployees",
            BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Stream> GetEmployees();

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "EnableEmployee?id={id}&enable={enable}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Task EnableEmployee(int id, int enable);
    }

	
}
