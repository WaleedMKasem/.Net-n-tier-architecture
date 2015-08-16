using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arabia.Web.Common
{
    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value.AttemptedValue != "")
            {
                DateTime hijry;
                if (DateTime.TryParse(value.AttemptedValue, CultureInfo.CurrentCulture, DateTimeStyles.None, out hijry))
                {
                    return hijry;
                }
                else
                {
                    DateTime dt = DateTime.Parse(value.AttemptedValue, new CultureInfo("ar-EG"));
                    return DateTime.Parse(dt.ToString(), CultureInfo.CurrentCulture);
                }
            }
            return null;
        }
    }



}
