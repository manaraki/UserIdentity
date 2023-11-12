using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Infra.Data.Contexts;
using UserIdentity.Infra.Data.Interfaces;

namespace UserIdentity.Infra.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
    
        private DataContext _db;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(DataContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> where=null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderby=null, string includes="") 
        {
            IQueryable<TEntity> query = _dbSet;
            
            if (where != null)
            {
                query = query.Where(where);
            }
            if (orderby != null)
            {
                query=orderby(query);
            }
            if (includes != "") 
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }
            return query.ToList();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where=null)
        {
            IQueryable<TEntity> query = _dbSet;            

            if (where != null)
            {
                query = query.Where(where);
            }
            return await query.FirstOrDefaultAsync();
            
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task InsertAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            
        }
        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;            
        }        

        public void Delete(TEntity entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            var entity = GetByIdAsync(id);
            Delete(entity);
        }       

        
    }
}
