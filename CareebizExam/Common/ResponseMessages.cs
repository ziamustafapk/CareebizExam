using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CareebizExam.Common
{
    public static class ResponseMessages
    {
        public static BaseResponseModel ModelValidate(ModelStateDictionary modelState)
        {
            var errorsToAdd = new List<ErrorViewModel>();
            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {

                    foreach (var error in errors)
                    {
                        // split the message to get the validator key
                        var keyAndMessage = error.ErrorMessage.Split('|');

                        // if there's no validator key, just return the error message,
                        // otherwise add the validatorkey
                        if (keyAndMessage.Count() > 1)
                        {
                            errorsToAdd.Add(new ErrorViewModel(
                                keyAndMessage[1],
                                keyAndMessage[0]));
                        }
                        else
                        {
                            errorsToAdd.Add(new ErrorViewModel(
                                keyAndMessage[0], key));
                        }
                    }
                    //Add(key, errorsToAdd);
                }
            }

            return new BaseResponseModel()
            {
                HasErrors = "TRUE",
                ResultCode = "UnprocessableEntity",
                StatusCode = "422",
                ResultDescription = "Your request has been received successfully with errors. Thank You.",
                Error = errorsToAdd.ToList(),
                HasWarnings = "NO",
                ReferenceNumber = "NO",
                Trace = ""
            };

        }
        public static BaseResponseModel NotFound()
        {
            return new BaseResponseModel()
            {
                HasErrors = "FALSE",
                ResultCode = "NotFound",
                StatusCode = "404",
                ResultDescription = "Your request has been received successfully But data is not available. Thank You.",
                Error = null,
                HasWarnings = "NO",
                ReferenceNumber = "NO",
                Trace = ""
            };

        }

        public static BaseResponseModel BadRequest()
        {
            return new BaseResponseModel()
            {
                HasErrors = "TRUE",
                ResultCode = "BadRequest",
                StatusCode = "400",
                ResultDescription = "Your request has been received successfully But data is not available. Thank You.",
                Error = null,
                HasWarnings = "NO",
                ReferenceNumber = "NO",
                Trace = ""
            };
        }

        public static BaseResponseModel InternalServerError(string ex)
        {
            return new BaseResponseModel()
            {
                HasErrors = "TRUE",
                ResultCode = "InternalServerError",
                StatusCode = "500",
                ResultDescription = ex,
                Error = null,
                HasWarnings = "NO",
                ReferenceNumber = "NO",
                Trace = ""
            };
        }
        public static BaseResponseModel Success()
        {
            return new BaseResponseModel()
            {
                HasErrors = "FALSE",
                ResultCode = "SUCCESS",
                StatusCode = "200",
                ResultDescription = "Your request has been received successfully and sent for processing. Thank You.",
                Error = null,
                HasWarnings = "NO",
                ReferenceNumber = "NO",
                Trace = ""
            };
        }

        public static BaseResponseModel CreatedPDF(string message)
        {
            return new BaseResponseModel()
            {
                HasErrors = "FALSE",
                ResultCode = "SUCCESS",
                StatusCode = "200",
                ResultDescription = message,
                Error = null,
                HasWarnings = "NO",
                ReferenceNumber = "NO",
                Trace = ""
            };
        }
        public static BaseResponseModel Created()
        {
            return new BaseResponseModel()
            {
                HasErrors = "FALSE",
                ResultCode = "Created",
                StatusCode = "201",
                ResultDescription = "Your request has been received successfully and sent for processing. Thank You.",
                Error = null,
                HasWarnings = "NO",
                ReferenceNumber = "NO",
                Trace = ""
            };
        }

    }

}
