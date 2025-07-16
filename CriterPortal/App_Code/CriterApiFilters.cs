using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Text;

namespace CriterAPI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //if (actionContext.ModelState.IsValid == false)
            //{
            //    actionContext.Response = actionContext.Request.CreateErrorResponse(
            //        HttpStatusCode.BadRequest, actionContext.ModelState);
            //}

            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var state in modelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                var response = new { errors = errors };

                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.BadRequest, response, JsonMediaTypeFormatter.DefaultMediaType);
            }



        }

        

    }
}