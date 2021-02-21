using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public override string Message {
			get {
				string message;
				using (var dbContext = new PostgresContext()) {
					var code = DaysWithCodes[_day];
					// DB IS NOT MINE
					var chosen = from couple in dbContext.Couples
						where (_even ? couple.NameCoupleEven != null : couple.NameCoupleOdd != null) &&
						      couple.CoupleUi.StartsWith(code)
						orderby couple.CoupleUi
						select couple;
					message = GetTextByCouples(chosen);
				}

				return message;
			}
		}

		private ButtonPayload Payload =>
			new() {
				CommandController = TimetableCommand.CommandStart,
				Stage = ToString()
			};

		public string GetTextByCouples(IOrderedQueryable<Couple> couples) {
			var sb = new StringBuilder();
			sb.AppendLine($"Расписание на {TimetableWeekState.RuDays[_day]}");
			sb.AppendLine($"Четная неделя: {_even}");
			sb.AppendLine();

			foreach (var couple in couples) {
				sb.AppendLine($"Пара: №{couple.CoupleUi.Remove(0, 3)}");
				var cName = _even ? couple.NameCoupleEven : couple.NameCoupleOdd;
				sb.AppendLine($" {cName}");
				sb.AppendLine($" Время: {couple.TimeStart} - {couple.TimeEnd}");
				sb.AppendLine($" Перерыв: {couple.TimeBreak} минут");
				sb.AppendLine();
			}

			return sb.ToString();
		}

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
			var changeEven = Payload;
			changeEven.Action = _day.ToString();
			changeEven.Params = (!_even).ToString();

			var evenAction = new MessageKeyboardButtonAction {
				Type = KeyboardButtonActionType.Callback,
				Label = "Изменить четность",
				Payload = changeEven.ToString()
			};
			builder.AddButton(evenAction, KeyboardButtonColor.Default);
			builder.AddLine();

			var goBackData = Payload;
			goBackData.Action = TimetableWeekState.Name;
			goBackData.Params = _even.ToString();

			var backAction = new MessageKeyboardButtonAction {
				Type = KeyboardButtonActionType.Callback,
				Label = "Назад",
				Payload = goBackData.ToString()
			};
			builder.AddButton(backAction, KeyboardButtonColor.Primary);

			return builder.Build();
		}
	}
}