using System.Text;
using VKGroupBot.Controllers.Attributes;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.GroupUpdate;

namespace VKGroupBot.Controllers {
	public class MessageCommandFactory {
		private readonly IMessageSender _sender;
		private readonly IVkApi _vkApi;

		public MessageCommandFactory(IVkApi api) {
			_vkApi = api;
			_sender = new MessageSender(_vkApi);
		}

		public ICommand CreateCommand(MessageNew messageNew) {
			var message = messageNew.Message;
			ICommand command;
			switch (message.Text.Split()[0]) {
				case ZoomLinkCommand.CommandStart:
					command = new ZoomLinkCommand(message, _sender);
					break;
				case TimetableCommand.CommandStart:
					command = new TimetableCommand(message, _sender);
					break;
				default:
					command = new EmptyMessageCommand(message, _sender);
					break;
			}

			if (GetNeedKeyboard(command) && !messageNew.ClientInfo.InlineKeyboard) {
				var sb = new StringBuilder();
				sb.AppendLine("Your client not support inline keyboard!");
				sb.AppendLine("Please use TG bot: https://t.me/leti_group_0310_bot");
				return new ErrorMessageCommand(message, _sender, sb.ToString());
			}
			return command;
		}

		public bool GetNeedKeyboard(ICommand command) {
			foreach (var attr in command.GetType().GetCustomAttributes(true)) {
				if (attr.GetType() == typeof(KeyboardDemandAttribute)) {
					return ((KeyboardDemandAttribute) attr).Demand;
				}
			}

			return false;
		}
	}
}