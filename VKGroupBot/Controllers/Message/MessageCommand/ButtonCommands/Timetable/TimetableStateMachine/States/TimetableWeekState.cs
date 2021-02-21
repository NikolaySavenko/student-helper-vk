using System;
using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using static System.DayOfWeek;

namespace VKGroupBot.Controllers.TimetableStateMachine.States {
	public class TimetableWeekState : TimetableState {
		public const string name = "WeekState";

		private static readonly Dictionary<DayOfWeek, string> ruDays = new() {
			{Monday, "Пн"},
			{Tuesday, "Вт"},
			{Wednesday, "Ср"},
			{Thursday, "Чт"},
			{Friday, "Пт"},
			{Saturday, "Сб"}
		};

		public TimetableWeekState(ITimeTableMachine machine) : base(machine) { }

		private ButtonPayload Payload =>
			new() {
				CommandController = TimetableCommand.CommandStart,
				Stage = ToString(),
				Params = false.ToString()
			};

		public override string Message => "ITS A FUKN WEEK";

		public override void Action(ButtonPayload buttonPayload) {
			var even = bool.Parse(buttonPayload.Params);
			var dayState = new TimeTableDayState(_machine, buttonPayload.Action, even);
			_machine.State = dayState;
		}

		public override MessageKeyboard BuildKeyboard() {
			var builder = new KeyboardBuilder(false);
			builder.SetInline();

			var i = 0;
			foreach (var day in ruDays) {
				var data = Payload;
				data.Action = day.Key.ToString();
				var action = new MessageKeyboardButtonAction {
					Type = KeyboardButtonActionType.Callback,
					Label = day.Value,
					Payload = data.ToString()
				};
				// TODO make color such as high load at day
				builder.AddButton(action, KeyboardButtonColor.Positive);
				if (i % 2 == 1)
					builder.AddLine();
				i++;
			}

			return builder.Build();
		}

		public override string ToString() => name;
	}
}