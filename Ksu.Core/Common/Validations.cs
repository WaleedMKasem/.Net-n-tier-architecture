using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Arabia.Core.Common
{
    public class ArabiaRequired : RequiredAttribute, IClientValidatable
    {
        public string RequiredMessage
        {
            get { return "تأكد من قيمة الحقل."; }
        }
        public ArabiaRequired()
        {
            this.ErrorMessage = RequiredMessage;
        }
        public ArabiaRequired(string s)
        {
            this.ErrorMessage = s;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "required"
                
            };
        }
    }
    public class ArabiaRequiredNumber : RequiredAttribute, IClientValidatable
    {
        public string RequiredMessage
        {
            get { return "تأكد من قيمة الحقل, يجب أن تكون قيمة رقمية مناسبة."; }
        }
        public ArabiaRequiredNumber()
        {
            this.ErrorMessage = RequiredMessage;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "required"
            };
        }
    }
    public class ArabiaNumber : ValidationAttribute, IClientValidatable
    {
        public ArabiaNumber()
        {
            this.ErrorMessage = "تأكد من قيمة الحقل, يجب أن تكون رقم.";
        }

        //public override bool IsValid(object value)
        //{
        //    int no;
        //    return int.TryParse(value.ToString(), out no);
        //} 
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            int retNum;

            return int.TryParse(Convert.ToString(value), out retNum);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,
                ValidationType = "number"
            };
            yield return rule;
        }
    }

    public class DataTypeAttributeAdapter : DataAnnotationsModelValidator<DataTypeAttribute>
    {
        public DataTypeAttributeAdapter(ModelMetadata metadata, ControllerContext context, DataTypeAttribute attribute)
            : base(metadata, context, attribute) { }

        public override System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            if (Attribute.DataType == DataType.Currency)
            {
                return new[] { new ModelClientValidationDateRule(Attribute.FormatErrorMessage(Metadata.GetDisplayName())) };
            }

            return base.GetClientValidationRules();
        }
    }
    public class ModelClientValidationDateRule : ModelClientValidationRule
    {
        public ModelClientValidationDateRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "number";
        }
    }
    public class ClientNumberValidatorProvider : ClientDataTypeModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata,
                                                               ControllerContext context)
        {
            bool isNumericField = base.GetValidators(metadata, context).Any();
            if (isNumericField)
                yield return new ClientSideNumberValidator(metadata, context);
        }
    }
    public class ClientSideNumberValidator : ModelValidator
    {
        public ClientSideNumberValidator(ModelMetadata metadata,
            ControllerContext controllerContext)
            : base(metadata, controllerContext) { }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            yield break; // Do nothing for server-side validation 
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationRule
            {
                ValidationType = "number",
                ErrorMessage = "تأكد من قيمة الحقل, يجب أن تكون قيمة رقمية مناسبة."
            };
        }
    }
}