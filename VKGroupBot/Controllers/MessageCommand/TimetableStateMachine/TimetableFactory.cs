using VKGroupBot.Controllers.TimetableStateMachine.States;
using VkNet.Model;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public class TimetableFactory: ITimetableFactory {
		public ITimeTableMachine MakeTimetable() => new TimetableMachine(new TimetableWeekState());
	}
}