using VKGroupBot.Controllers.TimetableStateMachine;
using VkNet.Model;


namespace VKGroupBot.Controllers {
	public class TimetableCommand : MessageCommandWithKeyboard {
		private static ITimetableFactory _factory;
		public const string CommandStart = "/timetable";

		public TimetableCommand(Message message, IMessageSender sender) : base(message, sender) {
			Type = MessageCommandType.Timetable;
			Params = Text.Replace($"{CommandStart} ", "");
		}

		public override void Execute() {
			var timetable = Factory.MakeTimetable();
			SendMessage(timetable.ToString(), timetable.BuildKeyboard());
		}

		public static ITimetableFactory Factory {
			get {
				if (_factory == null) _factory = new TimetableFactory();
				return _factory;
			}
		}
	}
}