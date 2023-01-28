using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Project.DAL.Context;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Concretes
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly MyContext _db;

        public BaseRepository(MyContext db)
        {
            _db = db;
        }
        public DbSet<T> Table => _db.Set<T>();

        ////////////////////////////////////////////////////////////////////////

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        ////////////////////////////////////////////////////////////////////////

        public async Task AddAsync(T item)
        {
            await Table.AddAsync(item);
        }
        public void AddRangeAsync(List<T> list)
        {
            Table.AddRangeAsync(list);
        }

        ////////////////////////////////////////////////////////////////////////

        public void Update(T item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Modified;
            item.ModifiedDate = DateTime.Now;

            T toBeUpdated = Table.FirstOrDefault(data => data.ID == item.ID);
            Table.Entry(toBeUpdated).CurrentValues.SetValues(item);
        }
        public void UpdateRange(List<T> list)
        {
            foreach (var item in list)
            {
                Update(item);
            }
        }

        ////////////////////////////////////////////////////////////////////////

        public void Delete(T item)
        {
            item.Status = ENTITIES.Enums.DataStatus.Deleted;
            item.DeletedDate = DateTime.Now;
        }
        public void DeleteRange(List<T> list)
        {
            foreach (var item in list)
            {
                Delete(item);
            }
        }

        ////////////////////////////////////////////////////////////////////////

        public void Destroy(T item)
        {
            Table.Remove(item);
        }
        public void DestroyRange(List<T> list)
        {
            Table.RemoveRange(list);
        }

        ////////////////////////////////////////////////////////////////////////

        public IQueryable<T> GetAll()
        {
            return Table.AsQueryable();
        }
        public IQueryable<T> GetActives()
        {
            return Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted);
        }
        public IQueryable<T> GetModifieds()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Modified);
        }
        public IQueryable<T> GetPassives()
        {
            return Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted);
        }

        ////////////////////////////////////////////////////////////////////////

        public async Task<T> GetFirstData()
        {
            return await Table.OrderBy(x => x.CreatedDate).FirstOrDefaultAsync();
        }

        public async Task<T> GetLastData()
        {
            return await Table.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
        }

        ////////////////////////////////////////////////////////////////////////

        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return Table.Where(exp);
        }
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return await Table.FirstOrDefaultAsync(exp);
        }
        public async Task<bool> Any(Expression<Func<T, bool>> exp)
        {
            return await Table.AnyAsync(exp);
        }
        public object Select(Expression<Func<T, object>> exp)
        {
            return Table.Select(exp).ToList();
        }
        public IQueryable<X> SelectViaClass<X>(Expression<Func<T, X>> exp)
        {
            return Table.Select(exp);
        }

        ////////////////////////////////////////////////////////////////////////

        public async Task<T> Find(int id)
        {
            return await Table.FirstOrDefaultAsync(data => data.ID == id);
        }
    }
}
