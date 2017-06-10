using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NHibernate.Validator.Engine;
using xVal.RuleProviders;
using xVal.Rules;
using NHibernate.Validator.Constraints;

namespace FashionAde.Utils.Validators
{
    public class StringRangeAttribute : ValidationAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public StringRangeAttribute(int minLength, int maxLength)
        {
            this.MinLength = (minLength < 0) ? 0 : minLength;
            this.MaxLength = (maxLength < 0) ? 0 : maxLength;
        }

        public override bool IsValid(object value)
        {
            //null or empty is <em>not</em> invalid
            var str = (string)value;
            if (string.IsNullOrEmpty(str))
                return true;

            return (str.Length >= this.MinLength && str.Length <= this.MaxLength);
        }
    }

    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute()
            : base(@"^[\w-\.]{1,}\@([\w]{1,}\.){1,}[a-z]{2,4}$")
        {
            
        }
    }

    public class EmailMatchAttribute : PropertiesMatchAttribute
    {
        public EmailMatchAttribute(String firstPropertyName, String secondPropertyName)
            : base(firstPropertyName, secondPropertyName)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PropertiesMatchAttribute : ValidationAttribute
    { 
        public String FirstPropertyName { get; set; }
        public String SecondPropertyName { get; set; } 

        public PropertiesMatchAttribute(String firstPropertyName, String secondPropertyName )
        {
          FirstPropertyName = firstPropertyName;
          SecondPropertyName = secondPropertyName ;
        }

        public override Boolean IsValid(Object value)
        {
            Type objectType = value.GetType();

            PropertyInfo[] neededProperties =
                objectType.GetProperties()
                .Where(propertyInfo => propertyInfo.Name == FirstPropertyName || propertyInfo.Name == SecondPropertyName)
                .ToArray();

            if(neededProperties.Count() != 2)
                throw new ApplicationException("PropertiesMatchAttribute error on " + objectType.Name);

            Boolean isValid = true;

            if(!Convert.ToString(neededProperties[0].GetValue(value, null)).Equals(Convert.ToString(neededProperties[1].GetValue(value, null))))
                isValid = false;

            return isValid;
        }
    }

    #region xVal Validation Extensions

    // EXTENDING: http://www.aminemami.com/blog/2010/01/custom-validation-in-xval/

    [Serializable]
    public class DateAttribute : ValidationAttribute, ICustomRule
    {
        public DateAttribute()
        {
            ErrorMessage = "Date format is invalid.";
        }

        public CustomRule ToCustomRule()
        {
            return new CustomRule(
              "ValidateDate", // JavaScript function name
              null, // Params for JavaScript function
              ErrorMessage // Message if invalid
              );
        }

        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var parse = DateTime.TryParse((string)value, out dateTime);
            return parse;
        }
    }

      public class ValidatorRulesProvider : CachingRulesProvider
        {
            private readonly RuleEmitterList<IRuleArgs> _ruleEmitters;

            public ValidatorRulesProvider()
            {
                _ruleEmitters = new RuleEmitterList<IRuleArgs>();
                _ruleEmitters.AddSingle<LengthAttribute>(x => new StringLengthRule(x.Min, x.Max));
                _ruleEmitters.AddSingle<MinAttribute>(x => new RangeRule(x.Value, null));
                _ruleEmitters.AddSingle<MaxAttribute>(x => new RangeRule(null, x.Value));
                _ruleEmitters.AddSingle<NHibernate.Validator.Constraints.RangeAttribute>(x => new RangeRule(x.Min, x.Max));
                _ruleEmitters.AddSingle<NotEmptyAttribute>(x => new RequiredRule());
                _ruleEmitters.AddSingle<NotNullNotEmptyAttribute>(x => new RequiredRule());
                _ruleEmitters.AddSingle<NotNullAttribute>(x => new RequiredRule());
                _ruleEmitters.AddSingle<PatternAttribute>(x => new RegularExpressionRule(x.Regex, x.Flags));
                _ruleEmitters.AddSingle<NHibernate.Validator.Constraints.EmailAttribute>(x => new DataTypeRule(DataTypeRule.DataType.EmailAddress));
                _ruleEmitters.AddSingle<DigitsAttribute>(MakeDigitsRule);
            }

            protected override RuleSet GetRulesFromTypeCore(Type type)
            {
                var classMapping = new ValidatorEngine().GetClassValidator(type);

                var rules = from member in type.GetMembers()
                            where member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property
                            from constraint in classMapping.GetMemberConstraints(member.Name).OfType<IRuleArgs>()

                            // All NHibernate Validation validators attributes must implement this interface
                            from rule in ConvertToXValRules(constraint)
                            where rule != null
                            select new { MemberName = member.Name, Rule = rule };

                return new RuleSet(rules.ToLookup(x => x.MemberName, x => x.Rule));
            }

            private IEnumerable<Rule> ConvertToXValRules(IRuleArgs ruleArgs)
            {
                foreach (var rule in _ruleEmitters.EmitRules(ruleArgs))
                {
                    if (rule != null)
                    {
                        rule.ErrorMessage = MessageIfSpecified(ruleArgs.Message);
                        yield return rule;
                    }
                }
            }

            private static RegularExpressionRule MakeDigitsRule(DigitsAttribute att)
            {
                if (att == null) throw new ArgumentNullException("att");

                string pattern;
                if (att.FractionalDigits < 1)
                    pattern = string.Format(@"\d{{0,{0}}}", att.IntegerDigits);
                else
                    pattern = string.Format(@"\d{{0,{0}}}(\.\d{{1,{1}}})?", att.IntegerDigits, att.FractionalDigits);

                return new RegularExpressionRule(pattern);
            }

            private static string MessageIfSpecified(string message)
            {
                // We don't want to display the default {validator.*} messages
                if ((message != null) && !message.StartsWith("{validator."))
                    return message;

                return null;
            }
        }
      #endregion
  }

