using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Toolbox;

namespace ToolboxTests
{
	[TestFixture]
	public class NullCullExtensionTests
	{
		[Test]
		public static void NullCull_PrimitiveTypeAlreadyHasValue_IsIgnored()
		{
			var pt = new PrimitiveTypes{i = 5};
			pt.NullCull();
			Assert.AreNotEqual(pt.i, 0);
		}

		private class PrimitiveTypes
		{
			public int i { get; set; }
		}
	}
}
