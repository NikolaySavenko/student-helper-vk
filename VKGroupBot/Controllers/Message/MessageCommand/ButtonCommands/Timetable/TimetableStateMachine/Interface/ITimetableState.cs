using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public interface ITimetableState {
		public string Message { get; }
		public void Action(ButtonPayload buttonPayload);
		public MessageKeyboard BuildKeyboard();
	}
}