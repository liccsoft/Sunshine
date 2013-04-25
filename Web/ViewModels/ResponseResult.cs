using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunshine.ViewModels
{
    public class ResponseResult
    {
        public ResponseResultStatus Status { get; set; }
        public string Message { get; set; }
        public object ReturnValue { get; set; } 

        public ResponseResult()
        {}
        public ResponseResult(ResponseResultStatus status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }

    public enum ResponseResultStatus
    {
        Unkown,
        Sucess,
        Failed,
        NotAllowed
    }
}