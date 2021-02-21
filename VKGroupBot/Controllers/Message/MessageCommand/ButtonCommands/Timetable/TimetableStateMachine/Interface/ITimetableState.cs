using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public interface ITimetableState {
		public void Action(ButtonPayload buttonPayload);
		public string Message { get; }
		public MessageKeyboard BuildKeyboard();
	}
}