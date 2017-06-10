using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core;
using SharpArch.Core.CommonValidator;

namespace FashionAde.Web.Common
{
    public class ValidatableView : IValidatable
    {
        public virtual bool IsValid()
        {
            return Validator.IsValid(this);
        }

        public virtual ICollection<IValidationResult> ValidationResults()
        {
            return Validator.ValidationResultsFor(this);
        }

        private IValidator Validator
        {
            get
            {
                return SafeServiceLocator<IValidator>.GetService();
            }
        }
    }
}
