using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using App1.Model.Logic;

namespace App1
{
    public static class ResponseFactory
    {

        public static StResponse createErrorResponse(string errorDescription)
        {
            StResponse response = new StResponse();
            response.IsSuccess = false;
            response.ErrorDetails = errorDescription;
            return response;
        }

        public static StResponse createErrorResponse()
        {
            StResponse response = new StResponse();
            response.IsSuccess = false;
            response.ErrorDetails = "Internal Server Error. Please contact your administrator.";

            return response;
        }

        public static StResponse createSuccessResponse(object responseData)
        {
            StResponse response = new StResponse();
            response.IsSuccess = true;
            response.Response = responseData;
            return response;
        }

        public static StResponse createSuccessResponse()
        {
            StResponse response = new StResponse();
            response.IsSuccess = true;

            return response;
        }

    }
}
