using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public interface ITimeTableMachine {
		public MessageKeyboard BuildKeyboard();
	}
}