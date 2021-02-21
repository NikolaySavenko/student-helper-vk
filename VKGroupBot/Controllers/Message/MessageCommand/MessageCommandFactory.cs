using VkNet.Abstractions;
using VkNet.Model;

namespace VKGroupBot.Controllers {
	public class MessageCommandFactory {
		private readonly IMessageSender _sender;
		private readonly IVkApi _vkApi;

		public MessageCommandFactory(IVkApi api) {
			_vkApi = api;
			_sender = new MessageSender(_vkApi);
		}

		public ICommand CreateCommand(Message message) {
			switch (message.Text.Split()[0]) {
				case ZoomLinkCommand.CommandStart:
					return new ZoomLinkCommand(message, _sender);
				case TimetableCommand.CommandStart:
					return new TimetableCommand(message, _sender);
				default:
					return new EmptyMessageCommand(message, _sender);
			}
		}
	}
}