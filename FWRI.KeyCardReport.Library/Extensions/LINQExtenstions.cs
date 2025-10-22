using System.Linq.Expressions;

namespace System.Linq
{
    public static class LINQExtenstions
    {
        /// <summary>
        /// Extenstion Method to order a query by a string matching a column name.
        /// TODO: doesn't work with nested table columns.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="memberName"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByString<T>(this IQueryable<T> query, string memberName, string direction)
        {
            ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };

            System.Reflection.PropertyInfo pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    direction.ToUpper() == "ASC" ? "OrderBy" : "OrderByDescending",
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }
    }
}
