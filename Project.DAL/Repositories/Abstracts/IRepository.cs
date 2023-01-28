using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstracts
{
    public interface IRepository<T> where T : IEntity
    {
        //List Commands
        IQueryable<T> GetAll();
        IQueryable<T> GetActives();
        IQueryable<T> GetPassives();
        IQueryable<T> GetModifieds();


        //Modify Commands
        Task AddAsync(T item);
        void AddRangeAsync(List<T> list);
        void Delete(T item);
        void DeleteRange(List<T> list);
        void Update(T item);
        void UpdateRange(List<T> list);
        void Destroy(T item);
        void DestroyRange(List<T> list);


        //Linq
        IQueryable<T> Where(Expression<Func<T, bool>> exp);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> exp);
        Task<bool> Any(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);
        IQueryable<X> SelectViaClass<X>(Expression<Func<T, X>> exp);


        //Find Command
        Task<T> Find(int id);


        //Last Data / First Data
        Task<T> GetLastData();
        Task<T> GetFirstData();

        //Save
        Task SaveAsync();
    }
}
