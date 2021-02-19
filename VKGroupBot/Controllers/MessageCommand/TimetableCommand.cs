using VKGroupBot.Controllers.TimetableStateMachine;
using VkNet.Model;


namespace VKGroupBot.Controllers {
	public class TimetableCommand : MessageCommandWithKeyboard {
		public const string CommandStart = "/timetable";

		private ITimetableFactory _factory;

		public TimetableCommand(Message message, IMessageSender sender) : base(message, sender) {
			Type = MessageCommandType.Timetable;
			Params = Text.Replace($"{CommandStart} ", "");
			// ? maybe in future i will put Params into factory
			_factory = new TimetableFactory();
		}

		public override void Execute() {
			var timetable = _factory.MakeTimetable();
			SendMessage(timetable.ToString(), timetable.BuildKeyboard());
		}
	}
}