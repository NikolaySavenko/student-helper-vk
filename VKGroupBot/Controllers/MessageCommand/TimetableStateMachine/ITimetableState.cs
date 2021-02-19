namespace VKGroupBot.Controllers.TimetableStateMachine {
	public interface ITimetableState {
		public void GoNext();
		public void GoBack();
	}
}