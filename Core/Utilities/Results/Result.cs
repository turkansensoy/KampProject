using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message):this(success)
        {
            Message = message;
            // Success = success; aşagıda kendini tekrar ettigi için yazılmıyor. onun yerine bu constructora this eklenir ve
            // diger constoctorda çalışır
        }
        //ProductManager'da  return new Results(true); degeri vermek için.message vermek istemiyorsak overload edilebilir.
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
