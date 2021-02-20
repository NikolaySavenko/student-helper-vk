namespace VKGroupBot.Controllers.TimetableStateMachine {
	public interface ITimetableFactory {
		public ITimeTableMachine MakeTimetable(ButtonPayload buttonPayload = null);
	}
}