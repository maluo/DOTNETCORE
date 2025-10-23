using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specs
{
    public class BaseSepc<T> : ISpec<T>
    {
        protected BaseSepc() { }//no need to get exposed to called by outside

        //base spec like brand and types
        private readonly Expression<Func<T, bool>>? _criteria;
        public Expression<Func<T, bool>>? Criteria => _criteria;//only get accessor allowed here 
        public BaseSepc(Expression<Func<T, bool>>? criteria)
        {
            _criteria = criteria;
        }

        //order by spec here
        public Expression<Func<T, object>>? OrderBy { get; private set; }

        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByExpression)
        {
            OrderByDesc = orderByExpression;
        }

        //distinct spec here
        public bool isDistinct { get; private set; }

        protected void ApplyDistinct()
        {
            isDistinct = true;
        }
    }
    
    public class BaseSepc<T, TResult>(Expression<Func<T,bool>> criteria) : BaseSepc<T>(criteria), ISpec<T, TResult>
    {
        protected BaseSepc() : this(null!) { }
        public Expression<Func<T, TResult>>? Select { get; private set; }
        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }
}