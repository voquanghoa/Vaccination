using System.Net;

namespace AspBusiness.Exceptions
{
    public class NotFoundException: BaseException
    {
        public NotFoundException(string msg) : base(HttpStatusCode.NotFound, msg)
        {
        }
    }
}