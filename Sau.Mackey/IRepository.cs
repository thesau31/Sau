using System;
using System.Linq;

namespace Sau.Mackey
{
	/// <summary>
	/// Interface for the repository pattern
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Gets a queryable of the entities
		/// </summary>
		/// <returns></returns>
		IQueryable<T> GetAll<T>() where T : class, IMackeyEntity;

		/// <summary>
		/// Gets an entity by its identifier.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		T GetById<T>(Guid id) where T : class, IMackeyEntity;

		/// <summary>
		/// Saves the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		Guid Save<T>(T entity) where T : class, IMackeyEntity;

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="entity">The entity.</param>
		void Delete<T>(T entity) where T : class, IMackeyEntity;
	}
}