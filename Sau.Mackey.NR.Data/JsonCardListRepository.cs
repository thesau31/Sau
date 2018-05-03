using System.Collections;
using Sau.Mackey.NR.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sau.Mackey.NR.Data
{
	public class JsonCardListRepository : IRepository
	{
		internal readonly Dictionary<Type, IList> Data;

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonCardListRepository" /> class.
		/// </summary>
		/// <param name="cardDirectoryInfo">The directory information.</param>
		/// <param name="lookupDirectoryInfo"></param>
		public JsonCardListRepository(DirectoryInfo cardDirectoryInfo, DirectoryInfo lookupDirectoryInfo)
		{
			if (cardDirectoryInfo == null) throw new ArgumentNullException("cardDirectoryInfo");
			if (lookupDirectoryInfo == null) throw new ArgumentNullException("lookupDirectoryInfo");

			Data = new Dictionary<Type, IList>();
			Data[typeof(Card)] = JsonFileHelper.Load<Card>(cardDirectoryInfo);
			Data[typeof(Lookup)] = JsonFileHelper.Load<Lookup>(lookupDirectoryInfo);
		}

		/// <summary>
		/// Gets a queryable of the entities
		/// </summary>
		/// <returns></returns>
		public IQueryable<T> GetAll<T>() where T : class, IMackeyEntity
		{
			return Data[typeof(T)].AsQueryable().Cast<T>();
		}

		/// <summary>
		/// Gets an entity by its identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns></returns>
		public T GetById<T>(Guid id) where T : class, IMackeyEntity
		{
			return Data[typeof(T)].Cast<T>().FirstOrDefault(x => x.DbId == id);
		}

		/// <summary>
		/// Creates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns></returns>
		public Guid Save<T>(T entity) where T : class, IMackeyEntity
		{
			if (entity == null) throw new ArgumentNullException("entity");

			var old = Data[typeof(T)].Cast<T>().FirstOrDefault(x => x.DbId == entity.DbId);
			if (old == null)
			{
				entity.DbId = Guid.NewGuid();
				Data[typeof(T)].Add(entity);
				return entity.DbId;
			}

			Data[typeof(T)].Remove(old);
			Data[typeof(T)].Add(entity);

			return old.DbId;
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Delete<T>(T entity) where T : class, IMackeyEntity
		{
			if (entity == null) throw new ArgumentNullException("entity");
			if (Data[typeof(T)].Cast<T>().All(x => x.DbId != entity.DbId)) throw new InvalidOperationException();

			var old = Data[typeof(T)].Cast<T>().Single(x => x.DbId == entity.DbId);
			Data[typeof(T)].Remove(old);
		}
	}
}