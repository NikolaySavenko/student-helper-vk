using VKGroupBot.Controllers.TimetableStateMachine.States;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public class TimetableMachine : ITimeTableMachine {
		public TimetableMachine(ITimetableState state) {
			State = state;
		}

		public TimetableMachine(ButtonPayload buttonPayload) {
			if (buttonPayload != null)
				switch (buttonPayload.Stage) {
					case TimetableWeekState.name:
						State = new TimetableWeekState(this);
						break;
					case TimeTableDayState.name:
						State = new TimeTableDayState(this, buttonPayload.Action);
						break;
				}
			else
				State = new TimetableWeekState(this);
		}

		public void Action(ButtonPayload payload) {
			State.Action(payload);
		}

		public ITimetableState State { get; set; }

		public MessageKeyboard BuildKeyboard() => State.BuildKeyboard();

		public override string ToString() => State.Message;
	}
}