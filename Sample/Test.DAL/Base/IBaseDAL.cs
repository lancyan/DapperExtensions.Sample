using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace Test.DAL.Base
{
    public interface IBaseDAL<T>
    {
        #region Count
        int Count(Expression<Func<T, bool>> predicate);

        int Count(IPredicate predicate);

        int Count(string sql = null, string where = null);
        #endregion

        #region Delete

        bool Delete(IPredicate predicate);

        bool Delete(T entity);

        bool DeleteById(object id);

        #endregion

        #region Exists
        bool Exists(Expression<Func<T, bool>> predicate);

        bool Exists(IPredicate predicate);

        bool Exists(string sql = null, string where = null);

        #endregion

        #region
        dynamic Insert(T obj);

        void Insert(params T[] objs);

        #endregion

        #region
        bool Update(dynamic updateDict, dynamic keyDict);

        bool Update(T entity);

        #endregion

        #region Query

        T Get(dynamic id);

        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<dynamic> Query(string sql, int? timeout = null, bool buffered = true);

        IEnumerable<dynamic> Query(string sql, string orderBy, int pageIndex, int pageSize, int? timeout = null, bool buffered = true);

        IEnumerable<K> Query<K>(string sql, int? timeout = null, bool buffered = true) where K : class;

        IEnumerable<K> Query<K>(string sql, string orderBy, int pageIndex, int pageSize, int? timeout = null, bool buffered = true) where K : class;

        IEnumerable<T> Where(IPredicate predicate = null, IList<ISort> sort = null);

        IEnumerable<T> Where(IPredicate predicate, IList<ISort> sort, int pageIndex, int pageSize);

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate, string orderBy = null);

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate, string orderBy, int pageIndex, int pageSize);

        IEnumerable<T> Where(string where, string orderBy = null);

        IEnumerable<T> Where(string where, string orderBy, int pageIndex, int pageSize);

        #endregion

    }
}
