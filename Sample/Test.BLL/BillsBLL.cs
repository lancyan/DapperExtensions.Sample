using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Test.Entity;
using Test.DAL;
using Test.BLL.Base;
using DapperExtensions;
namespace Test.BLL
{
    public class BillsBLL
    {
        BillsDAL dal = new BillsDAL();

        #region Insert
        public int Insert(Bills model)
        {
            return dal.Insert(model);
        }
        public void Insert(params Bills[] models)
        {
            dal.Insert(models);
        }
        #endregion

        #region UPDATE
        public bool Update(Bills entity)
        {
            return dal.Update(entity);
        }
        public bool Update(dynamic updateDict, dynamic keyDict)
        {
            return dal.Update(updateDict, keyDict);
        }
        #endregion

        #region Delete
        public bool Delete(IPredicate obj)
        {
            return dal.Delete(obj);
        }

        public bool Delete(Bills obj)
        {
            return dal.Delete(obj);
        }

        public bool Delete(dynamic id)
        {
            return dal.DeleteById(id);
        }

        #endregion

        #region Count
        public int Count(IPredicate predicate)
        {
            return dal.Count(predicate);
        }
        public int Count(string sql = null, string where = null)
        {
            return dal.Count(sql, where);
        }

        public int Count(Expression<Func<Bills, bool>> predicate)
        {
            return dal.Count(predicate);
        }
        #endregion

        #region Exists
        public bool Exists(IPredicate predicate)
        {
            return dal.Exists(predicate);
        }
        public bool Exists(string sql = null, string where = null)
        {
            return dal.Exists(sql, where);
        }
        public bool Exists(Expression<Func<Bills, bool>> predicate)
        {
            return dal.Exists(predicate);
        }
        #endregion

        #region GET
        public Bills Get(dynamic id)
        {
            return dal.Get(id);
        }
        public Bills Get(Expression<Func<Bills, bool>> predicate)
        {
            return dal.Get(predicate);
        }

        #endregion

       

        #region Where
        public IEnumerable<Bills> Where(Expression<Func<Bills, bool>> predicate, string orderBy = null)
        {
            return dal.Where(predicate, orderBy);
        }
        public IEnumerable<Bills> Where(Expression<Func<Bills, bool>> predicate, string orderBy, int pageIndex, int pageSize)
        {
            return dal.Where(predicate, orderBy, pageIndex, pageSize);
        }
        //public IEnumerable<Bills> Where(IPredicate predicate, IList<ISort> sort, int pageIndex, int pageSize)
        //{
        //    return dal.Where(predicate, sort, pageIndex, pageSize);
        //}
        //public IEnumerable<Bills> Where(IPredicate predicate = null, IList<ISort> sort = null)
        //{
        //    return dal.Where(predicate, sort);
        //}
        //public IEnumerable<Bills> Where(string where, string orderBy = null)
        //{
        //    return dal.Where(where, orderBy);
        //}
        //public IEnumerable<Bills> Where(string where, string orderBy, int pageIndex, int pageSize)
        //{
        //    return dal.Where(where, orderBy, pageIndex, pageSize);
        //}
        #endregion

        #region SQL Query 针对多表联合查询

        public IEnumerable<K> Query<K>(string sql, int? timeout = null, bool buffered = true) where K : class
        {
            return dal.Query<K>(sql, timeout, buffered);
        }
        public IEnumerable<K> Query<K>(string sql, string orderBy, int pageIndex, int pageSize, int? timeout = null, bool buffered = true) where K : class
        {
            return dal.Query<K>(sql, orderBy, pageIndex, pageSize, timeout, buffered);
        }
        public IEnumerable<dynamic> Query(string sql, int? timeout = null, bool buffered = true)
        {
            return dal.Query(sql, timeout, buffered);
        }
        public IEnumerable<dynamic> Query(string sql, string orderBy, int pageIndex, int pageSize, int? timeout = null, bool buffered = true)
        {
            return dal.Query(sql, orderBy, pageIndex, pageSize, timeout, buffered);
        }
        #endregion
    }
}
