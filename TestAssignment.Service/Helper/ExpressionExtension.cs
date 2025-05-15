using System.Linq.Expressions;

namespace TestAssignment.Service.Helper;

public static class ExpressionExtension
{
    public static Expression<Func<T, bool>> AndAlso<T>(
    this Expression<Func<T, bool>> expr1,
    Expression<Func<T, bool>> expr2
)
    {
        var param = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(Expression.Invoke(expr1, param), Expression.Invoke(expr2, param));

        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}
