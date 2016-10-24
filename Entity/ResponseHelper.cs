namespace Entity
{
    /// <summary>
    /// Response辅助工具类
    /// </summary>
    public class ResponseHelper
    {
        public static T CreateSuccess<T>() where T : IResponse, new()
        {
            T response = new T();
            response.Code = ResponseCode.Ok;
            response.Message = ResponseCode.OkMsg;
            return response;
        }

        public static T Create<T>(string code, string msg) where T : IResponse, new()
        {
            T response = new T();
            response.Code = code;
            response.Message = msg;
            return response;
        }

        public static T InvalidParameter<T>() where T : IResponse, new()
        {
            return Create<T>(ResponseCode.InvalidParam, ResponseCode.InvalidParamMsg);
        }

        public static void InvalidParameter<T>(T t) where T : IResponse, new()
        {
            t.Code = ResponseCode.InvalidParam;
        }

        public static T Exception<T>() where T : IResponse, new()
        {
            return Create<T>(ResponseCode.Exception, ResponseCode.ExceptionMsg);
        }
    }
}
