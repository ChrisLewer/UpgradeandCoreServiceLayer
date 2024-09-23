using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Models
{
    public class ModelStateWrapper : IValidationDictionary
    {

        //Want to decouple the service needing modelstate so create a IValidationDictionary
        // when using an mvc controller to call the service we create the ModelStateWrapper but
        // could use another wrapper ie a diffrent front end say windows app
        
        private ModelStateDictionary _modelState;

        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        #region IValidationDictionary Members

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }

        public bool IsValid
        {
            get { return _modelState.IsValid; }
        }

        #endregion
    }
}