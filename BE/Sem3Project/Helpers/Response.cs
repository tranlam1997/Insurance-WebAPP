using System.Collections.Generic;

namespace Sem3Project.Helpers
{
    public class Response<T>
    {
        public Response() { }

        public Response(List<T> data)
        {
            Message = string.Empty;
            Data = data;
        }

        public List<T> Data { get; set; }

        public string Message { get; set; }

        public object Metadata { get; set; }
    }
}
