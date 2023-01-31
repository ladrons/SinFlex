using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstracts;
using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        protected IRepository<T> _iRep;

        public BaseManager(IRepository<T> iRep)
        {
            _iRep = iRep;
        }

        public async Task SaveAsync()
        {
            await _iRep.SaveAsync();
        }

        ////////////////////////////////////////////////////////////////////////
        
        public async Task AddAsync(T item)
        {
            await _iRep.AddAsync(item);
        }
        public void AddRangeAsync(List<T> list)
        {
            _iRep.AddRangeAsync(list);
        }

        ////////////////////////////////////////////////////////////////////////
        
        public void Update(T item)
        {
            _iRep.Update(item);
        }
        public void UpdateRange(List<T> list)
        {
            _iRep.UpdateRange(list);
        }

        ////////////////////////////////////////////////////////////////////////
        
        public void Delete(T item)
        {
            _iRep.Delete(item);
        }
        public void DeleteRange(List<T> list)
        {
            _iRep.DeleteRange(list);
        }

        ////////////////////////////////////////////////////////////////////////
        
        public void Destroy(T item)
        {
            _iRep.Destroy(item);
        }
        public void DestroyRange(List<T> list)
        {
            _iRep.DestroyRange(list);
        }

        ////////////////////////////////////////////////////////////////////////
        
        public IQueryable<T> GetAll()
        {
            return _iRep.GetAll();
        }
        public IQueryable<T> GetActives()
        {
            return _iRep.GetActives();
        }
        public IQueryable<T> GetModifieds()
        {
            return _iRep.GetModifieds();
        }
        public IQueryable<T> GetPassives()
        {
            return _iRep.GetPassives();
        }

        ////////////////////////////////////////////////////////////////////////
        
        public async Task<T> GetFirstData()
        {
            return await _iRep.GetFirstData();
        }
        public async Task<T> GetLastData()
        {
            return await _iRep.GetLastData();
        }

        ////////////////////////////////////////////////////////////////////////
        
        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return _iRep.Where(exp);
        }
        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> exp)
        {
            return await _iRep.FirstOrDefault(exp);
        }
        public async Task<bool> Any(Expression<Func<T, bool>> exp)
        {
            return await _iRep.Any(exp);
        }
        public object Select(Expression<Func<T, object>> exp)
        {
            return _iRep.Select(exp);
        }
        public IQueryable<X> SelectViaClass<X>(Expression<Func<T, X>> exp)
        {
            return _iRep.SelectViaClass(exp);
        }

        ////////////////////////////////////////////////////////////////////////
        
        public async Task<T> Find(int id)
        {
            return await _iRep.Find(id);
        }
    }
}
