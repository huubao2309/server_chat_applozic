using System;
using Microsoft.AspNetCore.Mvc;
using ServerChatOmnicasa.Entities;

namespace ServerChatOmnicasa.Base
{
    public class BaseControllerHandler : ControllerBase
    {
        public BaseResponse<object> Result(object data)
        {
            return new BaseResponse<object>
            {
                Code = 200,
                Data = data,
                Message = "Successful",
            };
        }

        public BaseResponse<object> ResultException(Exception ex, int code = 520)
        {
            return new BaseResponse<object>
            {
                Code = code,
                Data = null,
                Message = ex?.Message,
                Error = ex?.StackTrace
            };
        }
    }
}
