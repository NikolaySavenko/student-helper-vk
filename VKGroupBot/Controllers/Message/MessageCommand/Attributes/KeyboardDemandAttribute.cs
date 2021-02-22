using System;

namespace VKGroupBot.Controllers.Attributes {
	[AttributeUsage(AttributeTargets.Class)]
	public class KeyboardDemandAttribute : Attribute {
		public bool Demand { get; }

		public KeyboardDemandAttribute(bool demand = false) {
			Demand = demand;
		}
	}
}