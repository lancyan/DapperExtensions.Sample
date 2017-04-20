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
    public class GoodsBLL
    {
        GoodsDAL dal = new GoodsDAL();

        #region Insert
        public int Insert(Goods model)
        {
            return dal.Insert(model);
        }
        public void Insert(params Goods[] models)
        {
            dal.Insert(models);
        }
        #endregion

        #region UPDATE
        public bool Update(Goods entity)
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

        public bool Delete(Goods obj)
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

        public int Count(Expression<Func<Goods, bool>> predicate)
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
        public bool Exists(Expression<Func<Goods, bool>> predicate)
        {
            return dal.Exists(predicate);
        }
        #endregion

        #region GET
        public Goods Get(dynamic id)
        {
            return dal.Get(id);

        }
        public Goods Get(Expression<Func<Goods, bool>> predicate)
        {
            return dal.Get(predicate);
        }

        #endregion

        

        #region Where
        public IEnumerable<Goods> Where(Expression<Func<Goods, bool>> predicate, string orderBy = null)
        {
            return dal.Where(predicate, orderBy);
        }
        public IEnumerable<Goods> Where(Expression<Func<Goods, bool>> predicate, string orderBy, int pageIndex, int pageSize)
        {
            return dal.Where(predicate, orderBy, pageIndex, pageSize);
        }
        //public IEnumerable<Goods> Where(IPredicate predicate, IList<ISort> sort, int pageIndex, int pageSize)
        //{
        //    return dal.Where(predicate, sort, pageIndex, pageSize);
        //}

        //public IEnumerable<Goods> Where(IPredicate predicate = null, IList<ISort> sort = null)
        //{
        //    return dal.Where(predicate, sort);
        //}
        //public IEnumerable<Goods> Where(string where, string orderBy = null)
        //{
        //    return dal.Where(where, orderBy);
        //}
        //public IEnumerable<Goods> Where(string where, string orderBy, int pageIndex, int pageSize)
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
