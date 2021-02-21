using System;
using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using static System.DayOfWeek;

namespace VKGroupBot.Controllers.TimetableStateMachine.States {
	public class TimeTableDayState : TimetableState {
		public const string Name = "DayState";

		private static readonly Dictionary<DayOfWeek, string> DaysWithCodes = new() {
			{Monday, "mon"},
			{Tuesday, "tue"},
			{Wednesday, "wed"},
			{Thursday, "thu"},
			{Friday, "fri"},
			{Saturday, "sat"}
		};

		private readonly DayOfWeek _day;
		private readonly bool _even;

		public TimeTableDayState(ITimeTableMachine machine, DayOfWeek day, bool even) : base(machine) {
			_day = day;
			_even = even;
		}

		public TimeTableDayState(ITimeTableMachine machine, string day, bool even) : base(machine) {
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
				case Name:
					var even = bool.Parse(buttonPayload.Params);
					_machine.State = new TimeTableDayState(_machine, _day, even);
					break;
				case TimetableWeekState.Name:
					_machine.State = new TimetableWeekState(_machine);
					break;
			}
		}

		public override string ToString() => Name;

		public override MessageKeyboard BuildKeyboard() {
			var builder = new KeyboardBuilder(false);
			builder.SetInline();
			var goBackData = Payload;
			goBackData.Action = ToString();

			var backAction = new MessageKeyboardButtonAction {
				Type = KeyboardButtonActionType.Callback,
				Label = "fuck go back",
				Payload = goBackData.ToString()
			};
			builder.AddButton(backAction, KeyboardButtonColor.Primary);
			builder.AddLine();

			var changeEven = Payload;
			changeEven.Action = _day.ToString();
			changeEven.Params = (!_even).ToString();

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