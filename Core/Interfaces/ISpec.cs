using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface ISpec<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        //add ordering
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDesc { get; }
        bool isDistinct { get; }
    }

    //change the projection to other entities
    public interface ISpec<T, TResult> : ISpec<T>
    {
        Expression<Func<T, TResult>>? Select { get; }
    }
}