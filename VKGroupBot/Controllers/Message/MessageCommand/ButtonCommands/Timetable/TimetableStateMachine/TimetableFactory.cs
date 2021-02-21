using VKGroupBot.Controllers.TimetableStateMachine.States;
using VkNet.Model;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public class TimetableFactory: ITimetableFactory {
		public ITimeTableMachine MakeTimetable(ButtonPayload buttonPayload = null) {
			// TODO remake this
			ITimeTableMachine machine = new TimetableMachine(buttonPayload);
			return machine;
		}
	}
}