namespace VKGroupBot.Controllers.TimetableStateMachine {
	 public abstract class TimetableState : ITimetableState {
		private TimetableMachine _machine;

		TimetableState(TimetableMachine machine) {
			_machine = machine;
		}

		public void GoNext() {
			throw new System.NotImplementedException();
		}

		public void GoBack() {
			throw new System.NotImplementedException();
		}
	 }
}