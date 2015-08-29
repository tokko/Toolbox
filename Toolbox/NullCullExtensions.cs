using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolbox
{
	public static class NullCullExtensions
	{

		public static void NullCull<T>(this T src) where T : class
		{
			var props =
				typeof (T).GetProperties().Where(p => !p.PropertyType.IsPrimitive && !p.PropertyType.IsValueType).ToList();
			foreach (var propertyInfo in props)
			{
				if (propertyInfo.PropertyType == typeof (string) && propertyInfo.GetValue(src, null) == null)
				{
					propertyInfo.SetValue(src, string.Empty, null);
				}
			}
		}
	}
}
