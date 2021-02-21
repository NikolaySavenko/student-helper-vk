using System;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public abstract class TimetableState : ITimetableState {
		protected ITimeTableMachine _machine;

		public TimetableState(ITimeTableMachine machine) {
			_machine = machine;
		}

		public abstract void Action(ButtonPayload buttonPayload);

		public abstract string Message { get; }

		public abstract MessageKeyboard BuildKeyboard();

		public override string ToString() => throw new InvalidProgramException("Please override ToString()");
	}
}