using DataAccess.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Realisations.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly VotingApplicationContext _context;

        public RepositoryBase(VotingApplicationContext context)
        {
            _context = context;
        }
        public IQueryable<T>? GetQuery(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> data = _context.Set<T>();
            if (include is not null)
                data = include(data);
            if (filter is not null)
                data = data.Where(filter);
            return data;
        }
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return this.GetQuery(expression, include);
        }
        public T? GetFirstByCondition(Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            return this.GetQuery(expression, include)?.FirstOrDefault();
        }
        public decimal GetSumByCondition(Expression<Func<T, bool>> expression, Expression<Func<T, decimal>> sum,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            return this.GetQuery(expression, include).Sum(sum);
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Include(includes[0]);
            if (query is null)
            {
                return _context.Set<T>();
            }
            for (int i = 001; i < includes.Length; i++)
            {
                query = query.Include(includes[i]);
            }
            return query;
        }

    }
}
