using System;

namespace UnityEngine
{
	[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class ShowOrHide : PropertyAttribute
	{
		public readonly bool enabledBool;

		public ShowOrHide (bool positionalBool)
		{
			this.enabledBool = positionalBool;
		}
	}
}