using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Toolbox
{
	public static class NullCullExtensions
	{

		public static T NullCull<T>(this T src) where T : class
		{
			try
			{
				var props =
					src.GetType()
						.GetProperties()
						.Where(p => !p.PropertyType.IsPrimitive && !p.PropertyType.IsValueType && p.GetValue(src, null) == null)
						.ToList();
				props.ForEach(prop =>
				{
					var value = Instantiate(prop.PropertyType);
					value.NullCull();
					prop.SetValue(src, value, null);
				});
				return src;
			}
			catch (TargetParameterCountException) //modifying indexable properties, should ignore these
			{
				return null;
			}
			catch (InvalidOperationException) //there are no constructors
			{
				return null;
			}
		}

		private static object Instantiate(Type arg)
		{
			if (arg == typeof (bool))
				return false;
			if (arg.IsValueType || arg.IsPrimitive)
				return 0;
			if (arg == typeof (string)) return string.Empty;
			if (arg.IsInterface)
			{
				arg = GetDescendantType(arg);
			}
			else if (arg.IsAbstract)
				throw new NotSupportedException("Abstract classes are not supported by current NullCull implementation");

			var ctor = arg.GetConstructors().First();
			var args = ctor.GetParameters().Select(a => Instantiate(a.ParameterType).NullCull()).ToArray();
			return ctor.Invoke(args);
		}

		private static Type GetDescendantType(Type type)
		{
			if (type.GetGenericArguments().Any())
			{
				if (type == typeof (IEnumerable<>).MakeGenericType(type.GetGenericArguments()))
				{
					var listType = typeof (List<>).MakeGenericType(type.GetGenericArguments());
					return listType;
				}
			}
			throw new NotImplementedException("Interface " + type.ToString() + " is not bound to a concrete class");
		}
	}
}
