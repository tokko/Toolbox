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
			Assert.That(pt.i, Is.EqualTo(5));
		}

		[Test]
		public static void NullCull_StringAlreadyHasValue_IsIgnored()
		{
			var st = new StringTypes {S = "test"};
			st.NullCull();
			Assert.That(st.S, Is.EqualTo("test"));
		}

		[Test]
		public static void NullCull_ComplextTypeAlreadyHasValue_IsIgnored()
		{
			var ct = new ComplexTypes{PrimitiveTypes = new PrimitiveTypes{i = 5}};
			ct.NullCull();
			Assert.NotNull(ct.PrimitiveTypes);
			Assert.That(ct.PrimitiveTypes.i, Is.EqualTo(5));
		}

		private class StringTypes
		{
			public string S { get; set; }
		}
		private class ComplexTypes
		{
			public PrimitiveTypes PrimitiveTypes { get; set; }
		}
		private class PrimitiveTypes
		{
			public int i { get; set; }
		}
	}
}
