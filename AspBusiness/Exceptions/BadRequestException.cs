using System.Net;

namespace AspBusiness.Exceptions
{
    public class BadRequestException: BaseException
    {
        public BadRequestException(string msg) : base(HttpStatusCode.BadRequest, msg)
        {
        }
    }
}