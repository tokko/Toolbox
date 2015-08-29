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
		public static void NullCull_StringIsNull_IsSetToEmptyString()
		{
			var st = new StringTypes();
			st.NullCull();
			Assert.That(st.S, Is.EqualTo(string.Empty));
		}

		[Test]
		public static void NullCull_ComplexTypeInObjectVariable_InitializedCorrectly()
		{
			object o = Activator.CreateInstance(typeof(ComplexTypes));
			o.NullCull();
			var ct = (ComplexTypes) o;
			Assert.NotNull(ct.PrimitiveTypes);
		}
		[Test]
		public static void NullCull_ComplexType_InitializedRecursively()
		{
			var ct = new ComplexStringTypes();
			ct.NullCull();
			Assert.NotNull(ct.StringTypes);
			Assert.NotNull(ct.StringTypes.S);
		}

		[Test]
		public static void NullCull_ComplextType_IsSetToDefaultOfObject()
		{
			var ct = new ComplexTypes();
			ct.NullCull();
			Assert.NotNull(ct.PrimitiveTypes);
		}
		[Test]
		public static void NullCull_StringAlreadyHasValue_IsIgnored()
		{
			var st = new StringTypes {S = "test"};
			st.NullCull();
			Assert.That(st.S, Is.EqualTo("test"));
		}

		[Test]
		public static void NullCull_ComplexTypeAlreadyHasValue_IsIgnored()
		{
			var ct = new ComplexTypes{PrimitiveTypes = new PrimitiveTypes{i = 5}};
			ct.NullCull();
			Assert.NotNull(ct.PrimitiveTypes);
			Assert.That(ct.PrimitiveTypes.i, Is.EqualTo(5));
		}

		[Test]
		public static void NullCull_ListType_InitialzedCorrectly()
		{
			var lt = new ListTypes();
			lt.NullCull();
			Assert.NotNull(lt.List);
			Assert.That(lt.List.GetType(), Is.EqualTo(typeof(List<string>)));
		}

		[Test]
		public static void NullCull_GenericPrimitive_IsInitialized()
		{
			var gt = new GenericClass<string>();
			gt.NullCull();
			Assert.That(gt.t, Is.EqualTo(string.Empty));
		}

		[Test]
		public static void NullCull_GenericComplex_IsInitialized()
		{
			var gt = new GenericClass<ComplexStringTypes>();
			gt.NullCull();
			Assert.NotNull(gt.t);
			Assert.True(gt.t.StringTypes.S == string.Empty);
		}

		[Test]
		public static void NullCull_IEnumerable_IsInitializedToList()
		{
			var et = new EnumerableTypes();
			et.NullCull();
			Assert.NotNull(et.Enumerable);
			Assert.True(typeof(List<string>) == et.Enumerable.GetType());
		}

		private abstract class AbstractType
		{
			public string S { get; set; }
		}
		private class EnumerableTypes
		{
			public IEnumerable<string> Enumerable { get; set; }
		}
		private class GenericClass<T>
		{
			public T t { get; set; }
		}
		private class ListTypes
		{
			public List<string> List { get; set; }
		}

		private class StringTypes
		{
			public string S { get; set; }
		}

		private class ComplexStringTypes
		{
			public StringTypes StringTypes { get; set; }
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
