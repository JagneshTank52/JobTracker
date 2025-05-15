using Microsoft.AspNetCore.Mvc.Filters;

namespace TestAssignment.Service.Helper;

public class TrimFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var parameters = filterContext.ActionArguments;
        foreach (var param in parameters)
        {
            TrimStringProperties(param.Value!);
        }

        base.OnActionExecuting(filterContext);
    }
    private void TrimStringProperties(object obj)
    {
        if (obj == null) return;

        foreach (var prop in obj.GetType().GetProperties())
        {
            if (prop.PropertyType == typeof(string) && prop.CanWrite)
            {
                string currentValue = (string)prop.GetValue(obj)!;
                if (currentValue != null)
                {
                    prop.SetValue(obj, currentValue.Trim());
                }
            }
        }
    }
}
