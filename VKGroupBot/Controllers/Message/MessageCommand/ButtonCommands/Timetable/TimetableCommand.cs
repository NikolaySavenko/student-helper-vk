using VKGroupBot.Controllers.TimetableStateMachine;
using VkNet.Model;

namespace VKGroupBot.Controllers {
	public class TimetableCommand : MessageCommandWithKeyboard {
		public const string CommandStart = "/timetable";
		private static ITimetableFactory _factory;

		public TimetableCommand(Message message, IMessageSender sender) : base(message, sender) {
			Type = MessageCommandType.Timetable;
			Params = Text.Replace($"{CommandStart} ", "");
		}

		public static ITimetableFactory Factory {
			get {
				if (_factory == null) _factory = new TimetableFactory();
				return _factory;
			}
		}

		public override void Execute() {
			var timetable = Factory.MakeTimetable();
			SendMessage(timetable.ToString(), timetable.BuildKeyboard());
		}
	}
}