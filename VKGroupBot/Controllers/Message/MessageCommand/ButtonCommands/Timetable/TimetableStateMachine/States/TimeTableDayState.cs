using System;
using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using static System.DayOfWeek;

namespace VKGroupBot.Controllers.TimetableStateMachine.States {
	public class TimeTableDayState : TimetableState {
		public const string name = "DayState";

		public const string backActionName = "week_back";
		public const string evenChangeActionName = "even_change";

		private static readonly Dictionary<DayOfWeek, string> daysWithCodes = new() {
			{Monday, "mon"},
			{Tuesday, "tue"},
			{Wednesday, "wed"},
			{Thursday, "thu"},
			{Friday, "fri"},
			{Saturday, "sat"}
		};

		public readonly DayOfWeek _day;
		public readonly bool _even;

		public TimeTableDayState(ITimeTableMachine machine, DayOfWeek day, bool even = false) : base(machine) {
			_day = day;
			_even = even;
		}

		public TimeTableDayState(ITimeTableMachine machine, string day, bool even = false) : base(machine) {
			_even = even;
			var result = Enum.TryParse(day, out _day);
			if (!result) _day = Sunday;
			// ERROR
		}

		public override string Message => $"DAY: {_day}; even: {_even}";

		private ButtonPayload Payload =>
			new() {
				CommandController = TimetableCommand.CommandStart,
				Stage = ToString()
			};

		public override void Action(ButtonPayload buttonPayload) {
			switch (buttonPayload.Action) {
				case evenChangeActionName:
					_machine.State = new TimeTableDayState(_machine, _day, !_even);
					break;
				case backActionName:
					_machine.State = new TimetableWeekState(_machine);
					break;
			}
		}

		public override string ToString() => name;

		public override MessageKeyboard BuildKeyboard() {
			var builder = new KeyboardBuilder(false);
			builder.SetInline();
			var goBackData = Payload;
			goBackData.Action = backActionName;
			var backAction = new MessageKeyboardButtonAction {
				Type = KeyboardButtonActionType.Callback,
				Label = "fuck go back",
				Payload = goBackData.ToString()
			};
			builder.AddButton(backAction, KeyboardButtonColor.Primary);
			builder.AddLine();

			var changeEven = Payload;
			changeEven.Action = evenChangeActionName;
			var evenAction = new MessageKeyboardButtonAction {
				Type = KeyboardButtonActionType.Callback,
				Label = "Change Even",
				Payload = changeEven.ToString()
			};
			builder.AddButton(evenAction, KeyboardButtonColor.Default);

			return builder.Build();
		}
	}
}