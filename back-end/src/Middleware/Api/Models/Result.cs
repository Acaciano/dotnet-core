using System;
using Newtonsoft.Json;

namespace Middleware.Api.Models
{
    public class ResultData
    {
        [JsonProperty(PropertyName = "success")]
        public bool Success;

        [JsonProperty(PropertyName = "messageResult")]
        public string MessageResult;

        public static ResultData Error(Exception exception)
        {
            return new ResultData
            {
                Success = false,
                MessageResult = "Erro: " + exception.Message
            };
        }

        public static ResultData Error(string message)
        {
            return new ResultData
            {
                Success = false,
                MessageResult = "Erro: " + message
            };
        }
    }

    public class ResultData<T> : ResultData
    {
        [JsonProperty(PropertyName = "result")]
        public T Result;
    }

    public class ResultDataError<T> : ResultData
    {
        [JsonProperty(PropertyName = "result")]
        public T Result;

        public static ResultDataError<T> Error(T result)
        {
           return new ResultDataError<T>
            {
                Success = false,
                MessageResult = "Ocorreu um erro.",
                Result = result
           };
        }
    }

    public class ResultDataSuccess<T> : ResultData
    {
        [JsonProperty(PropertyName = "result")]
        public T Result;

        public static ResultData<T> Ok(T result , string message = null)
        {
            return new ResultData<T>
            {
                Success = true,
                MessageResult = message ?? "Operação executada com sucesso.",
                Result = result
            };
        }
    }
}