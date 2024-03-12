// AppException.cs

namespace Application.Core
{
    public class AppException
    {
        /* Kita akan memberikan nilai awal NULL di dalamnya dan hanya akan mengisinya
         jika kita berada di mode Developer, jadi kita perlu mengembalikan pengecualian
         ini dari middleware secara efektif */
        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
