using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.HTTP
{
    
    public enum EHttpStatusMessages
    {
        // Using https://techketo.com/quick-tip-enum-to-description-in-csharp/
        // for how to create 'Descriptions' to use strings for enumerations.

        // Client error responses.
        [Description("400 Bad Request")]
        BadRequest400 = 400,
        [Description("404 Not Found")]
        NotFound404 = 404,

        // Server error responses
        [Description("500 Internal Server Error")]
        InternalServerError500 = 500,

        // Success response.
        [Description("200 OK")]
        OK200 = 200
    }

    public class HttpStatusMessageConverter
    {
        /*public static EHttpStatusMessages FromStringToIdentifier(string description)
        {
            foreach (var status in Enum.GetValues(typeof(EHttpStatusMessages)))
            {
                if(status.ToString() == description)
                {
                    return (EHttpStatusMessages)status;
                }
            }

            return EHttpStatusMessages.BadRequest400;
        }*/

        public static string GetPlaceholderStatusCode(int code)
        {
            string statusCode = "";
            switch(code)
            {
                case 200:
                    {
                        statusCode = EHttpStatusMessages.OK200.GetDescription();
                        break;
                    }
                case 400:
                    {
                        statusCode = EHttpStatusMessages.BadRequest400.GetDescription();
                        break;
                    }
                case 404:
                    {
                        statusCode = EHttpStatusMessages.NotFound404.GetDescription();
                        break;
                    }
                case 500:
                    {
                        statusCode = EHttpStatusMessages.InternalServerError500.GetDescription();
                        break;
                    }
            }
            return statusCode;
        }
    }
    
}
