using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Ninject;
using Ninject.Extensions.Conventions;
using Sau.Mackey.NR.Data;
using Sau.Mackey.NR.Services;
using Sau.Mackey.NR.Services.Interfaces;
using Sau.Mackey.NR.Services.Relationships;

namespace Sau.Mackey.NR.Console.Ninject
{
	public class Bootstrapper
	{
		public void Initialize(IKernel container)
		{
			container.Bind<IRelationshipFactory>().To<RelationshipFactory>().InSingletonScope();
			container.Bind<IRelationshipBuilder>().To<RelationshipBuilder>().InSingletonScope();

			var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
			var unescapedPath = Uri.UnescapeDataString(uri.Path);
			var path = Path.GetDirectoryName(unescapedPath);

			container.Bind(x => x
				.FromAssembliesInPath(path)
				.SelectAllClasses().InheritedFrom<IInterrogator>()
				.BindAllInterfaces()
				);

			container.Bind<IRepository>().To<JsonCardListRepository>()
				.InSingletonScope()
				.WithConstructorArgument("cardDirectoryInfo", new DirectoryInfo(ConfigurationManager.AppSettings["JsonCardPath"]))
				.WithConstructorArgument("lookupDirectoryInfo", new DirectoryInfo(ConfigurationManager.AppSettings["JsonLookupPath"]));

			container.Bind<ICardService>().To<CardService>().InSingletonScope();
		}
	}
}
