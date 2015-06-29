using System.Collections;
using System.Linq;

namespace Toolbox
{
	public static class ToolboxExtensions
	{
		public static bool DeepEquals<T1, T2>(this T1 t1, T2 t2, bool allProperties = false)
			where T1 : class
			where T2 : class
		{
			if (t2 == null) return false;
			var t1Props = t1.GetType().GetProperties();
			var t2Props = t2.GetType().GetProperties();
			if (allProperties && t1Props.Count() != t2Props.Count())
				return false;
			foreach (var propertyInfo in t1Props) {
				var t2prop =
					t2Props.SingleOrDefault(prop => prop.Name.Equals(propertyInfo.Name) && prop.GetType() == propertyInfo.GetType());
				if(t2prop == null) continue;
				var v1 = propertyInfo.GetValue(t1);
				var v2 = t2prop.GetValue(t2);
				if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsPrimitive ||
				    propertyInfo.PropertyType == typeof(string))
					if (!v1.Equals(v2))
						return false;
					else continue;
				var interfaces = propertyInfo.PropertyType.GetInterfaces();
				if (interfaces.Contains(typeof (IEnumerable))) //object is enumerable
				{
					var e1 = ((IEnumerable) v1).GetEnumerator();
					var e2 = ((IEnumerable) v2).GetEnumerator();
					while(e1.MoveNext() && e2.MoveNext())
							if(!e1.Current.DeepEquals(e2.Current, allProperties))
								return false;
					if (e1.MoveNext() || e2.MoveNext()) return false; //lists are not of equal length
				}
				else if (!v1.DeepEquals(v2, allProperties))
					return false;
			}
			return true;
		}
	}
}
