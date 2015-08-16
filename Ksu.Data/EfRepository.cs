using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq; 
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration; 

using Arabia.Core;
using Arabia.Core.Data;
using Arabia.Data.Model;

namespace Arabia.Data
{
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual bool Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
           
            catch (DbUpdateException ex)
            {
                if (null == ex.InnerException || null == ex.InnerException.InnerException)
                {
                    throw new Exception(ex.Message);
                    return false;
                }

                var sqlEx = ex.InnerException.InnerException
                                       as System.Data.SqlClient.SqlException;
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    throw new ArabiaDuplicateException(sqlEx.Message);
                    return false;
                }
               
                else
                {
                    throw new Exception(sqlEx.Message);
                    return false;
                }
                return false;
            }
        }

        public virtual bool Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
        
            catch (DbUpdateException ex)
            {
                if (null == ex.InnerException || null == ex.InnerException.InnerException)
                {
                    throw new Exception(ex.Message);
                    return false;
                }

                var sqlEx = ex.InnerException.InnerException
                                       as System.Data.SqlClient.SqlException;
                if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                {
                    throw new ArabiaDuplicateException(sqlEx.Message);
                    return false;
                }

                else
                {
                    throw new Exception(sqlEx.Message);
                    return false;
                }
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine +
                               string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                                   validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
                return false;
            }
            catch (Exception ex)
            {
                var sqlEx = ex.InnerException.InnerException
                                       as System.Data.SqlClient.SqlException;
                if (sqlEx != null && (sqlEx.Number == 547))
                {
                    throw new ArabiaDeleteException(sqlEx.Message);
                    return false;
                }
                throw ex;
            }
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }
    }
}
