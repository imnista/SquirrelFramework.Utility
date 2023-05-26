using System.Linq.Expressions;

namespace SquirrelFramework.Utility.Coding
{
    /// <summary>
    /// Lambda Expression Helper
    /// </summary>
    public static class LambdaExpressionHelper
    {
        /// <summary>
        /// Retrieve the property name of a LambdaExpression
        /// </summary>
        /// <param name="expression">Expression, e.g. () => user.DisplayName </param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>The property name, e.g. "DisplayName"</returns>
        public static string RetrievePropertyNameByLambdaExpression<TSource, TField>(Expression<Func<TSource, TField>> expression)
        {
            // Refer to https://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression
            if (expression == null)
            {
                throw new NullReferenceException("Expression is required.");
            }

            MemberExpression expr = expression.Body switch
            {
                MemberExpression body => body,
                UnaryExpression unaryBody => (MemberExpression)unaryBody.Operand,
                _ => throw new ArgumentException($"Expression '{expression}' not supported.", nameof(expression)),
            };
            return expr.Member.Name;
        }

        /// <summary>
        /// Retrieve the property name of a LambdaExpression
        /// </summary>
        /// <param name="expression">Expression, e.g. () => user.DisplayName </param>
        /// <returns>The property name, e.g. "DisplayName"</returns>
        public static string RetrievePropertyNameByLambdaExpression(Expression<Func<object>> expression)
        {
            if (expression == null)
            {
                throw new NullReferenceException("Expression is required.");
            }

            MemberExpression expr = expression.Body switch
            {
                MemberExpression body => body,
                UnaryExpression unaryBody => (MemberExpression)unaryBody.Operand,
                _ => throw new ArgumentException($"Expression '{expression}' not supported.", nameof(expression)),
            };
            return expr.Member.Name;
        }
    }
}