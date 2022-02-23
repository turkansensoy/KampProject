using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    // IResult:Temel voidler için başlangıç 
    public interface IResult
    {
        bool Success { get; } // get sadece okuyabilir,set yazmak için
        string Message { get; }
    }
}
