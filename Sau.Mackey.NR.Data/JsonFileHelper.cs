using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json;

namespace Sau.Mackey.NR.Data
{
	/// <summary>
	/// Helper for reading Json files in a directory into a list of entries
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static class JsonFileHelper
	{
		/// <summary>
		/// Loads the json in the specified directory information.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="directoryInfo">The directory information.</param>
		/// <returns></returns>
		public static List<T> Load<T>(DirectoryInfo directoryInfo) where T: class, IMackeyEntity
		{
			var result = new List<T>();

			foreach (var fileInfo in directoryInfo.EnumerateFiles("*.json"))
			{
				using (var streamReader = fileInfo.OpenText())
				{
					var json = streamReader.ReadToEnd();
					var entities = (List<T>)JsonConvert.DeserializeObject(json, typeof(List<T>));
					foreach (var entity in entities)
						entity.DbId = Guid.NewGuid();

					result.AddRange(entities);
				}
			}

			return result;
		}
	}
}