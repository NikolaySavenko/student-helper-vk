using System;
using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using static System.DayOfWeek;

namespace VKGroupBot.Controllers.TimetableStateMachine.States {
	public class TimeTableDayState : TimetableState {
		public const string name = "DayState";

		private static readonly Dictionary<DayOfWeek, string> daysWithCodes = new() {
			{Monday, "mon"},
			{Tuesday, "tue"},
			{Wednesday, "wed"},
			{Thursday, "thu"},
			{Friday, "fri"},
			{Saturday, "sat"}
		};

		public readonly DayOfWeek _day;

		public TimeTableDayState(ITimeTableMachine machine, DayOfWeek day) : base(machine) {
			_day = day;
		}

		public TimeTableDayState(ITimeTableMachine machine, string day) : base(machine) {
			var result = Enum.TryParse(day, out _day);
			if (!result) _day = Sunday;
			// ERROR
		}

		public override string Message => "DAY_" + _day;

		private ButtonPayload Payload =>
			new() {
				CommandController = TimetableCommand.CommandStart,
				Stage = ToString()
			};

		public override void Action(ButtonPayload buttonPayload) {
			_machine.State = new TimetableWeekState(_machine);
		}

		public override string ToString() => name;

		public override MessageKeyboard BuildKeyboard() {
			var builder = new KeyboardBuilder(false);
			builder.SetInline();
			var data = Payload;
			data.Action = null;
			var action = new MessageKeyboardButtonAction {
				Type = KeyboardButtonActionType.Callback,
				Label = "fuck go back",
				Payload = data.ToString()
			};
			builder.AddButton(action, KeyboardButtonColor.Primary);
			return builder.Build();
		}
	}
}