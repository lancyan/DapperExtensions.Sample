/******************************************
*
* 模块名称：
* 当前版本：1.0
* 开发人员：lancyan
* 完成时间：2017/4/12
* 版本历史：
* 
******************************************/

using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq.Expressions;
using Test.Entity;
using Test.DAL;
using Test.BLL.Base;
using DapperExtensions;
using Dapper;

namespace Test.BLL
{
    public class UsersBLL
    {
        UsersDAL dal = new UsersDAL();

        #region Insert
        public int Insert(Users model)
        {
            return dal.Insert(model);
        }
        public void Insert(params Users[] models)
        {
            dal.Insert(models);
        }
        #endregion

        #region UPDATE
        public bool Update(Users entity)
        {
            return dal.Update(entity);
        }
        public bool Update(dynamic updateDict, dynamic keyDict)
        {
            return dal.Update(updateDict, keyDict);
        }
        #endregion

        #region Delete
      

        public bool Delete(Users obj)
        {
            return dal.Delete(obj);
        }

        public bool Delete(int id)
        {
            var predicate = Predicates.Field<Users>(f => f.ID, Operator.Eq, id);
            var re = dal.Delete(predicate);
            return re;
        }

        public bool Delete(dynamic id)
        {
            return dal.DeleteById(id);
        }

        #endregion

        #region Count
        //public int Count(IPredicate predicate)
        //{
        //    return dal.Count(predicate);
        //}
        public int Count(string sql = null, string where = null)
        {
            return dal.Count(sql, where);
        }

        public int Count(Expression<Func<Users, bool>> predicate)
        {
            return dal.Count(predicate);
        }
        #endregion

        #region Exists
        //public bool Exists(IPredicate predicate)
        //{
        //    return dal.Exists(predicate);
        //}
        public bool Exists(string sql = null, string where = null)
        {
            return dal.Exists(sql, where);
        }
        public bool Exists(Expression<Func<Users, bool>> predicate)
        {
            return dal.Exists(predicate);
        }
        #endregion

        #region GET
        public Users Get(dynamic id)
        {
            return dal.Get(id);

        }
        public Users Get(Expression<Func<Users, bool>> predicate)
        {
            return dal.Get(predicate);
        }

        #endregion

        #region Where
        public IEnumerable<Users> Where(Expression<Func<Users, bool>> predicate, string orderBy = null)
        {
            return dal.Where(predicate, orderBy);
        }
        public IEnumerable<Users> Where(Expression<Func<Users, bool>> predicate, string orderBy, int pageIndex, int pageSize)
        {
            return dal.Where(predicate, orderBy, pageIndex, pageSize);
        }
        //public IEnumerable<Users> Where(IPredicate predicate, IList<ISort> sort, int pageIndex, int pageSize)
        //{
        //    return dal.Where(predicate, sort, pageIndex, pageSize);
        //}

        //public IEnumerable<Users> Where(IPredicate predicate = null, IList<ISort> sort = null)
        //{
        //    return dal.Where(predicate, sort);
        //}
        public IEnumerable<Users> Where(string where, string orderBy = null)
        {
            return dal.Where(where, orderBy);
        }
        public IEnumerable<Users> Where(string where, string orderBy, int pageIndex, int pageSize)
        {
            return dal.Where(where, orderBy, pageIndex, pageSize);
        }
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

        public IEnumerable<Users> GetUsers()
        {
            return dal.Where();
        }

        public IEnumerable<Goods> GetGoodsByUser(string userId)
        {
            return dal.Query<Goods>(string.Format("select * from Goods where ID in(select GoodID from Bills where UserId={0})", userId));
        }

        public IEnumerable<dynamic> GetBillsByUser(string userId)
        {
            return dal.Query<dynamic>(string.Format("select a.UserName,a.NickName,a.Mobile,b.Count,b.PriceSum,c.Name from Users a inner join Bills b on a.ID=b.UserID inner join Goods c on b.GoodID=c.ID and a.ID={0} and a.status<>0", userId));
        }
        #endregion


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <returns></returns>
        public dynamic UserLogin(String userName, String password)
        {
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@UserName", userName);
            paras.Add("@Password", password);
            //paras.Add("@res", ParameterDirection.Output);
            var entity = dal.Execute<UserData>("Pro_UserLogin", paras);
            return entity;
        }


    }

    public class UserData
    {
        public int Id { get; set; }
        public int[] Roles { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }

    }
}
