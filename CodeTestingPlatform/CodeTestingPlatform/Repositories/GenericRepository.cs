using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using CodeTestingPlatform.DatabaseEntities.Local;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CodeTestingPlatform.Repositories.Interfaces;
using System.Threading;
using CodeTestingPlatform.Models;
using Microsoft.EntityFrameworkCore.Query;
using CodeTestingPlatform.Models.Extensions;
using System.Reflection;

namespace CodeTestingPlatform.Repositories {
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class {
        protected internal readonly CTP_TESTContext _context;
        protected internal readonly DbSet<TEntity> _dbSet;

        public GenericRepository(CTP_TESTContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }


        public virtual IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false) {

            IQueryable<TEntity> query = _dbSet;
            if (disableTracking) {
                query = query.AsNoTracking();
            }

            if (include != null) {
                query = include(query);
            }

            if (predicate != null) {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters) {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null) {
                return orderBy(query);
            }
            else {
                return query;
            }
        }

        public IQueryable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false) {

            var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

            return query.Select(selector);
        }


        public virtual async Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false) {

            IQueryable<TEntity> query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

            return await query.ToListAsync();
        }

        public virtual async Task<IList<TResult>> GetAllAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false) {

            var query = GetAll(selector, predicate, orderBy, include, disableTracking, ignoreQueryFilters);

            return await query.ToListAsync();
        }

        public virtual Task<IPagedList<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false) {

            var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

            return query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        public virtual Task<IPagedList<TResult>> GetListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken),
            bool ignoreQueryFilters = false) {

            var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

            return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false) {

            var query = GetAll(predicate, orderBy, include, disableTracking, ignoreQueryFilters);

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TResult> FindOneAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false) {

            var query = GetAll<TResult>(selector, predicate, orderBy, include, disableTracking, ignoreQueryFilters);

            return await query.FirstOrDefaultAsync();
        }

        public virtual void Add(TEntity entity) {
            _dbSet.Add(entity);
        }

        public virtual async Task DeleteAsync(object id) {
            TEntity entity = await FindAsync(id);
            _dbSet.Remove(entity);
        }

        public virtual async Task DeleteAsync(TEntity entity) {
            await Task.FromResult(_dbSet.Remove(entity));
        }


        public virtual void Update(TEntity entityToUpdate) {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null) {
            if (predicate == null) {
                return await _dbSet.CountAsync();
            }
            else {
                return await _dbSet.CountAsync(predicate);
            }
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual void Attach(TEntity entity) {
            var entry = _dbSet.Attach(entity);
            entry.State = EntityState.Added;
        }

        public virtual bool AddRange(params TEntity[] entities) {
            _dbSet.AddRange(entities);
            return true;
        }

        public virtual bool UpdateRange(params TEntity[] entities) {
            _dbSet.UpdateRange(entities);
            return true;
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task ClearAsync() {
            var allEntities = await _dbSet.ToListAsync();
            _dbSet.RemoveRange(allEntities);
        }

        protected async Task RemoveManyToManyRelationship(string joinEntityName, string ownerIdKey, string ownedIdKey, long ownerEntityId, List<long> idsToIgnore) {
            DbSet<Dictionary<string, object>> dbset = _context.Set<Dictionary<string, object>>(joinEntityName);

            var manyToManyData = await dbset
                .Where(joinPropertyBag => joinPropertyBag[ownerIdKey].Equals(ownerEntityId))
                .ToListAsync();

            var filteredManyToManyData = manyToManyData
                .Where(joinPropertyBag => !idsToIgnore.Any(idToIgnore => joinPropertyBag[ownedIdKey].Equals(idToIgnore)))
                .ToList();

            dbset.RemoveRange(filteredManyToManyData);
        }

        public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        public virtual ValueTask<TEntity> FindAsync(params object[] keyValues) => _dbSet.FindAsync(keyValues);

        public virtual ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => _dbSet.FindAsync(keyValues, cancellationToken);

        /// <summary>
        /// Gets the async max based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        ///  /// <param name="selector"></param>
        /// <returns>decimal</returns>
        public virtual async Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null) {
            if (predicate == null)
                return await _dbSet.MaxAsync(selector);
            else
                return await _dbSet.Where(predicate).MaxAsync(selector);
        }

        /// <summary>
        /// Gets the async min based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        ///  /// <param name="selector"></param>
        /// <returns>decimal</returns>
        public virtual async Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null) {
            if (predicate == null)
                return await _dbSet.MinAsync(selector);
            else
                return await _dbSet.Where(predicate).MinAsync(selector);
        }

        /// <summary>
        /// Gets the async average based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        ///  /// <param name="selector"></param>
        /// <returns>decimal</returns>
        public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null) {
            if (predicate == null)
                return await _dbSet.AverageAsync(selector);
            else
                return await _dbSet.Where(predicate).AverageAsync(selector);
        }

        /// <summary>
        /// Gets the async sum based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        ///  /// <param name="selector"></param>
        /// <returns>decimal</returns>
        public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null) {
            if (predicate == null)
                return await _dbSet.SumAsync(selector);
            else
                return await _dbSet.Where(predicate).SumAsync(selector);
        }

        public void Dispose() {
            _context?.Dispose();
        }
    }
}