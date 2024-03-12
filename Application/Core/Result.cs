// Result.cs

namespace Application.Core
{
    /* Jadi kita akan memberikannya tipe generik dan kami menentukan tipe generik dalam tanda kurung siku di Result ini */
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        /* Jadi ketika kita menggunakan tipe generik dengan cara ini, maka kita akan menentukan kapan kita membuat instance baru */
        public T Value { get; set; }
        public string Error { get; set; }

        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
    }
}
