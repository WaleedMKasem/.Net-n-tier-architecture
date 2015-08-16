using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arabia.Web.Common
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RangeYearToCurrent : RangeAttribute, IClientValidatable
    {
        public RangeYearToCurrent(int from) : base(from, 1500) { }

        #region IClientValidatable Members

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rules = new ModelClientValidationRangeRule(this.ErrorMessage, this.Minimum, this.Maximum);
            yield return rules;
        }

        #endregion
    }
}