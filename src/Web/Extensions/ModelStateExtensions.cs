using ASE3040.Application.Common.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ASE3040.Web.Extensions;

public static class ModelStateExtensions
{
    public static void AddValidationResult(this ModelStateDictionary modelState, ValidationResult result)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
    
    public static void AddResult(this ModelStateDictionary modelState, Result result)
    {
        foreach (var error in result.Errors)     
        {
            modelState.AddModelError("", error);
        }
    }
}