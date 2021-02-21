namespace VKGroupBot.Controllers.TimetableStateMachine {
	public class TimetableFactory : ITimetableFactory {
		public ITimeTableMachine MakeTimetable(ButtonPayload buttonPayload = null) {
			// TODO remake this
			ITimeTableMachine machine = new TimetableMachine(buttonPayload);
			return machine;
		}
	}
}