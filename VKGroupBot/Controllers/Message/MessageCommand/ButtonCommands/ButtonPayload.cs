using Newtonsoft.Json;

namespace VKGroupBot.Controllers {
	public class ButtonPayload {
		public string CommandController { get; set; }
		public string Stage { get; set; }
		public string Action { get; set; }

		public string Params { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this);
	}
}