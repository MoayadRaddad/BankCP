﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessObjects.Models
{
    public class CustomValidation
    {
        public class GreaterThanAttribute : ValidationAttribute, IClientValidatable
        {
            private readonly string _comparisonProperty;

            public GreaterThanAttribute(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (string.IsNullOrEmpty(ErrorMessageString))
                {
                    ErrorMessage = ErrorMessageString;
                }

                if (value.GetType() == typeof(IComparable))
                {
                    throw new ArgumentException("value has not implemented IComparable interface");
                }

                var currentValue = (IComparable)value;

                var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

                if (property == null)
                {
                    throw new ArgumentException("Comparison property with this name not found");
                }

                var comparisonValue = property.GetValue(validationContext.ObjectInstance);

                if (comparisonValue.GetType() == typeof(IComparable))
                {
                    throw new ArgumentException("Comparison property has not implemented IComparable interface");
                }

                if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                {
                    throw new ArgumentException("The properties types must be the same");
                }

                if (currentValue.CompareTo((IComparable)comparisonValue) < 0)
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = GlobalResource.Resources.LangText.GreatThanMin,
                    ValidationType = "maximumservicetime"
                };
                rule.ValidationParameters.Add("minimumservicetime", _comparisonProperty);
                yield return rule;
            }
        }


        public class LessThanAttribute : ValidationAttribute, IClientValidatable
        {
            private readonly string _comparisonProperty;

            public LessThanAttribute(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (string.IsNullOrEmpty(ErrorMessageString))
                {
                    ErrorMessage = ErrorMessageString;
                }

                if (value.GetType() == typeof(IComparable))
                {
                    throw new ArgumentException("value has not implemented IComparable interface");
                }

                var currentValue = (IComparable)value;

                var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

                if (property == null)
                {
                    throw new ArgumentException("Comparison property with this name not found");
                }

                var comparisonValue = property.GetValue(validationContext.ObjectInstance);

                if (comparisonValue.GetType() == typeof(IComparable))
                {
                    throw new ArgumentException("Comparison property has not implemented IComparable interface");
                }

                if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                {
                    throw new ArgumentException("The properties types must be the same");
                }

                if (currentValue.CompareTo((IComparable)comparisonValue) >= 0)
                {
                    return new ValidationResult(ErrorMessage);
                }

                return ValidationResult.Success;
            }
            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = GlobalResource.Resources.LangText.LessThanMax,
                    ValidationType = "minimumservicetime"
                };
                rule.ValidationParameters.Add("maximumservicetime", _comparisonProperty);
                yield return rule;
            }
        }
    }
}