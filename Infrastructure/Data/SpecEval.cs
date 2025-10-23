using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class SpecEval<T> where T : Base
    {

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpec<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            if(spec.isDistinct)
            {
                query = query.Distinct();
            }

            return query;
        }

        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> inputQuery,
        ISpec<T, TResult> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            //using a projection here and returning TResult type
            var selectQuery = query as IQueryable<TResult>;
            if (spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);
            }
            
            if (spec.isDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }
            return selectQuery ?? query.Cast<TResult>();
        }
    }
}