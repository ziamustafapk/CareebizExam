using System.Collections.Generic;

namespace CareebizExam.Common
{
    public class BaseResponseModel
    {
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public string StatusCode { get; set; }
        public string HasErrors { get; set; }
        public string HasWarnings { get; set; }
        public string ReferenceNumber { get; set; }
        public string Trace { get; set; }
        public List<ErrorViewModel> Error { get; set; }
    }

    public class ErrorViewModel
    {
        public string ValidatorKey { get; private set; }
        public string Message { get; private set; }

        public ErrorViewModel(string message, string validatorKey = "")
        {
            ValidatorKey = validatorKey;
            Message = message;
        }
    }

    

    public interface ISingleResponse<TModel> 
    {
        TModel Model { get; set; }
    }

    public interface IListResponse<TModel> 
    {
        IEnumerable<TModel> Model { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public SingleResponse()
        {
            Messages = new BaseResponseModel();
            
        }
        public BaseResponseModel Messages { get; set; }
        public TModel Model { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public ListResponse()
        {
            Messages = new BaseResponseModel();

        }
        public BaseResponseModel Messages { get; set; }
        public IEnumerable<TModel> Model { get; set; }
    }
}
