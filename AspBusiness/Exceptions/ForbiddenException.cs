using System.Net;

namespace AspBusiness.Exceptions
{
    public class ForbiddenException: BaseException
    {
        public ForbiddenException(string msg) : base(HttpStatusCode.Forbidden, msg)
        {
        }
    }
}