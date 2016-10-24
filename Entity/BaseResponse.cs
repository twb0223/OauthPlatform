using System;

namespace Entity
{
    public abstract class BaseResponse<T>: IResponse
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseResponse()
        {
            Code = ResponseCode.Ok;
        }

        public bool IsOk()
        {
            return Code == ResponseCode.Ok;
        }
    }

    public class BaseResponse : IResponse
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 接口授权码
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseResponse()
        {
            Code = ResponseCode.Ok;
            Message = ResponseCode.OkMsg;
        }

        public BaseResponse(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StringResponse : BaseResponse<string>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class Int64Response : BaseResponse<Int64>
    {

    }
    public class DateTimeResponse : BaseResponse<DateTime> { }

    [Serializable]
    public abstract class PageResponse<T> : BaseResponse<T>
    {


        /// <summary>
        /// 总页数 
        /// </summary>
        public int Pages { get; set; }
        /// <summary>
        /// 总行数 
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageResponse()
        {
            Code = ResponseCode.Ok;
        }

    }
}
