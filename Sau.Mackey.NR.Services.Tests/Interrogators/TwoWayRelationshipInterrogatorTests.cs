using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sau.Mackey.NR.Services.Interfaces;
using Sau.Mackey.NR.Services.Interrogators;

namespace Sau.Mackey.NR.Services.Tests.Interrogators
{
	[TestClass]
	[ExcludeFromCodeCoverage]
	public class TwoWayRelationshipInterrogatorTests
	{
		[TestClass]
		public class TwoWayRelationshipInterrogator_Ctor
		{
			[TestMethod]
			public void GivenNullRelationshipBuilder_ThenThrowsArgumentNullException()
			{
				// arrange & act
				try
				{
					var mockTwoWayRelationshipInterrogator =
						new Mock<TwoWayRelationshipInterrogator>(null) { CallBase = true };
					// ReSharper disable once UnusedVariable
					var result = mockTwoWayRelationshipInterrogator.Object;
				}
				catch (Exception ex)
				{
					// assert
					Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
				}
			}
		}

		[TestClass]
		public class TwoWayRelationshipInterrogator_Interrogate
		{
			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GivenNullCardQueryable_ThenThrowsArgumentNullException()
			{
				// arrange
				var mockRelationshipBuilder = new Mock<IRelationshipBuilder>(MockBehavior.Strict);
				var mockTwoWayRelationshipInterrogator =
					new Mock<TwoWayRelationshipInterrogator>(mockRelationshipBuilder.Object) { CallBase = true };

				// act
				mockTwoWayRelationshipInterrogator.Object.Interrogate(null);

				// assert
				Assert.Fail("ArgumentNullException should have been thrown.");
			}
		}
	}
}
