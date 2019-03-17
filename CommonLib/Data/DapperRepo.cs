using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CommonLib.Data
{
    public class DapperRepo<T> where T : class, new()
    {
        private readonly SqlConnection _connection;

        protected DapperRepo(string connstr)
        {
            _connection = new SqlConnection(connstr);
        }

        protected SqlConnection GetConnection()
        {
            return _connection;
        }

        public dynamic Insert(T entity)
        {
            dynamic result = null;

            try
            {
                _connection.Open();
                result = _connection.Insert(entity);
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public bool InsertRange(T[] entities)
        {
            bool result = false;
            try
            {
                _connection.Open();
                using (SqlTransaction trans = _connection.BeginTransaction())
                {
                    try
                    {
                        _connection.Insert(entities, transaction: trans);
                        trans.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public bool Update(T entity)
        {
            bool result = false;

            try
            {
                _connection.Open();
                result = _connection.Update(entity);
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public bool Delete(T entity)
        {
            bool result = false;

            try
            {
                _connection.Open();
                result = _connection.Delete(entity);
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public List<T> GetAll()
        {
            List<T> result = new List<T>();
            try
            {
                _connection.Open();
                result = _connection.GetList<T>().ToList();
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public T Get(int id)
        {
            T result = new T();
            try
            {
                _connection.Open();
                result = _connection.Get<T>(id);
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public T Get(Guid id)
        {
            T result = new T();

            try
            {
                _connection.Open();
                return _connection.Get<T>(id);
            }
            catch (Exception ex)
            {
                string exstr = ex.Message;
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public T ItemQuery(string query)
        {
            var result = new T();
            try
            {
                _connection.Open();
                result = _connection.Query<T>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public List<T> ListQuery(string query)
        {
            var result = new List<T>();
            try
            {
                _connection.Open();
                result = _connection.Query<T>(query).ToList();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public int CountQuery(string query)
        {
            int result = 0;
            try
            {
                _connection.Open();
                result = _connection.Query<int>(query).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            return result;
        }
    }
}
