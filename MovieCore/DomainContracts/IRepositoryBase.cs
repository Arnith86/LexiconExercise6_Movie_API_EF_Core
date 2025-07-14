using MovieCore.Models.Entities;

namespace MovieCore.DomainContracts;

public interface IRepositoryBase<T> where T : EntityBase
{
	//Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false);
	//Task<T> GetAsync(Expression<Func<T, bool>> expression, bool trackChanges = false);
	//Task<bool> AnyAsync(int id);
	//void Add(T entity);
	//void Update(T entity);
	//void Remove(T entity);
}
