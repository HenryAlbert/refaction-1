using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Routing;


namespace refactor_me.CustomFilters
{
   
   public class ValidateGuidAttribute : ActionFilterAttribute
    {
        private bool IsValidGUID(string stringGuid)
        {
           
                Guid newGuid;
                if (Guid.TryParse(stringGuid, out newGuid))
                    return true;
                else return false;
           

        }
       
        public override void OnActionExecuting(HttpActionContext actionExecuingContext)
        {
            var objectContent = actionExecuingContext.Request.Content.ReadAsStringAsync().Result;
            if (objectContent != null && objectContent != "")
            {
               if(! IsValidGUID(objectContent))
                 {
                    actionExecuingContext.Response = actionExecuingContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionExecuingContext.ModelState);
                }
            }

        }



        //public override void OnActionExecuting(Act actionContext)
        //{

        //    actionContext.ro

        //    if (actionContext.ModelState.IsValid == false)
        //    {
        //        actionContext.Response = actionContext.Request.CreateErrorResponse(
        //            HttpStatusCode.BadRequest, actionContext.ModelState);
        //    }
        //} 
    }
}