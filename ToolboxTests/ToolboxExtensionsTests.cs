using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Toolbox;

namespace ToolboxTests
{
	[TestFixture]
	public class ToolboxExtensionsTests
	{
		[Test]
		public void DeepEquals_AllPropertiesOneMissingProperty_NotEquals()
		{
			var a = new A {
				a = "ga",
				b = 123
			};
			var b = new B { a = "ga" };
			var r = a.DeepEquals(b, true);
			Assert.That(r, Is.False);
		}

		[Test]
		public void DeepEquals_AllPropertiesAreNotEqual_NotEquals()
		{
			var a = new A {
				a = "ga",
				b = 123
			};
			var c = new C { a = a.a, b = 123456 };
			var r = a.DeepEquals(c, true);
			Assert.That(r, Is.False);
		}

		[Test]
		public void DeepEquals_AllPropertiesAreEqual_Equals()
		{
			var a = new A {
				a = "ga",
				b = 123
			};
			var c = new C { a = a.a, b = a.b };
			var r = a.DeepEquals(c, true);
			Assert.That(r, Is.True);
		}
		[Test]
		public void DeepEquals_IntersectionAreEqual_Equals()
		{
			var a = new A {
				a = "ga",
				b = 123
			};
			var b = new B { a = a.a };
			var r = a.DeepEquals(b);
			Assert.That(r, Is.True);
		}

		[Test]
		public void DeepEquals_IntersectionAreNotEqual_NotEquals()
		{
			var a = new A {
				a = "ga",
				b = 123
			};
			var b = new B { a = "fwaga" };
			var r = a.DeepEquals(b);
			Assert.That(r, Is.False);
		}

		[Test]
		public void DeepEquals_ObjectGraphAreEqual_Equals()
		{
			var d = new D {A = new A {a = "gf", b = 0}};
			var d1 = new D{A = d.A};
			var r = d.DeepEquals(d1);
			Assert.That(r, Is.True);
		}
		
		[Test]
		public void DeepEquals_ObjectGraphAreEqual_NotEquals()
		{
			var d = new D {A = new A {a = "gf", b = 0}};
			var d1 = new D{A = new A{a = "fa", b = 1}};
			var r = d.DeepEquals(d1);
			Assert.That(r, Is.False);
		}

		[Test]
		public void DeepEquals_WithListOfObjectsAreEqual_Equals()
		{
			var e = new E<B> {List = new List<B> {new B {a = "gafa"}}};
			var e1 = new E<B> { List = new List<B> { new B { a = "gafa" } } };
			var r = e.DeepEquals(e1);
			Assert.That(r, Is.True);
		}

		[Test]
		public void DeepEquals_WithListOfObjectsAreNotEqual_NotEquals()
		{
			var e = new E<B> { List = new List<B> { new B { a = "gafa" } } };
			var e1 = new E<B> { List = new List<B> { new B { a = "ga" } } };
			var r = e.DeepEquals(e1);
			Assert.That(r, Is.False);
		}
		[Test]
		public void DeepEquals_WithListOfObjectsOfNotEqualLength_NotEquals()
		{
			var e = new E<B> { List = new List<B> { new B { a = "gafa" } } };
			var e1 = new E<B> { List = new List<B> { new B { a = "gafa" }, new B { a = "ga" } } };
			var r = e.DeepEquals(e1);
			Assert.That(r, Is.False);
		}

		[Test]
		public void DeepEquals_WithListOfObjectsOfNotEqualType_NotEquals()
		{
			var e = new E<A> { List = new List<A> { new A { a = "gafa" } } };
			var e1 = new E<B> { List = new List<B> { new B { a = "gafa" }, new B { a = "ga" } } };
			var r = e.DeepEquals(e1);
			Assert.That(r, Is.False);
		}

		
	}

	public class A
	{
		public string a { get; set; }
		public int b { get; set; }
	}

	public class B
	{
		public string a { get; set; }
	}

	public class C : A
	{
		
	}

	public class D
	{
		public A A { get; set; }
	}

	public class E<T>
	{
		public List<T> List { get; set; }
	}
}
