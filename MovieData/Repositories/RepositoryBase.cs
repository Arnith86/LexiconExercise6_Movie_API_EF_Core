using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Data;

namespace MovieData.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
{
	protected DbSet<T> DbSet { get; }
	protected MovieApiContext _context { get; }

	public RepositoryBase(MovieApiContext context)
	{
		DbSet = context.Set<T>();
		_context = context;
	}

	public void Add(T entity) => DbSet.Add(entity);

	public void Remove(T entity) => DbSet.Remove(entity);

	public void Update(T entity) => DbSet.Update(entity);
}
