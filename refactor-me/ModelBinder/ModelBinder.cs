using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Models;
using System.Collections.Concurrent;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;
using Newtonsoft.Json;
using System.Reflection;


namespace refactor_me.ModelBinder
{
    public class ProductModelBinder : IModelBinder
    {
           /***************************************************
           // Follow the same pattern for ProductOption also
           /***************************************************/
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(Models.Product))
                return false;
        
            var jContent = actionContext.Request.Content.ReadAsStringAsync().Result;
            if (jContent == null) return false;
            if(string.IsNullOrEmpty(jContent))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid value");
                return false;

            }

            //TO be add null checking for missing input fields

            var result = JsonConvert.DeserializeObject<Models.Product>(jContent);
            bindingContext.Model = result;
            return true;

        }

       


    }
}