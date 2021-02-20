using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public interface ITimeTableMachine {
		public ITimetableState State { get; set; }
		public void Action(ButtonPayload payload);
		public MessageKeyboard BuildKeyboard();
	}
}