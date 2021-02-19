using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers.TimetableStateMachine {
	public class TimetableMachine : ITimeTableMachine {
		private ITimetableState _state;

		private Dictionary<string, string> ru2enWeekDay = new Dictionary<string, string>() {
			{"Пн","mon"},
			{"Вт","tue"},
			{"Ср","wed"},
			{"Чт","thu"},
			{"Пт","fri"},
			{"Сб","sat"}
		};

		public TimetableMachine(ITimetableState state) {
			_state = state;
		}

		public MessageKeyboard BuildKeyboard() {
			var builder = new KeyboardBuilder(false);
			builder.SetInline();

			int i = 0;
			foreach (var day in ru2enWeekDay.Keys) {
				var action = new MessageKeyboardButtonAction {
					Type = KeyboardButtonActionType.Callback,
					Label = day,
					Payload = $"{{\"action\": \"getDay_{ru2enWeekDay[day]}\"}}"
				};
				builder.AddButton(action, KeyboardButtonColor.Positive);
				if (i % 2 == 1)
					builder.AddLine();
				i++;
			}
			return builder.Build();
		}

		public override string ToString() => "OH SHIT ITS A TIMETABLE";
	}
}