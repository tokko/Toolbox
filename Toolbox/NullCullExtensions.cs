using System;
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
			var props =
				typeof(T).GetProperties().Where(p => !p.PropertyType.IsPrimitive && !p.PropertyType.IsValueType && p.GetValue(src, null) == null).ToList();
			props.ForEach(prop => prop.SetValue(src, Instantiate(prop.PropertyType), null));
			return src;
		}

		private static object Instantiate(Type arg)
		{
			if (arg == typeof (bool))
				return false;
			if (arg.IsValueType || arg.IsPrimitive)
				return 0;
			if (arg == typeof (string)) return string.Empty;
			var ctor = arg.GetConstructors().First();
			var args = ctor.GetParameters().Select(a => Instantiate(a.ParameterType)).ToArray();
			return ctor.Invoke(args);
		}
	}
}
