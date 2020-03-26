using System.Collections.Generic;

namespace AddressBookApp.Utilities
{
    public class ApiResponse<T>
    {
        public List<ApiError> Errors {get; set;}
        public ApiResponse(){}
        public ApiResponse(List<ApiError> errors)
            => Errors = errors;

        public T Data {get; set;}
    }

    public class ApiError
    {
        public string ErrorCode {get;}
        public string Message {get;}
        public ApiError(string errorCode, string errorMessage)
            => (ErrorCode, Message) = (errorCode, errorMessage);
    }
}