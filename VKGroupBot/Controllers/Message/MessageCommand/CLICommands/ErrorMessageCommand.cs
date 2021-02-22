using VkNet.Model;

namespace VKGroupBot.Controllers {
	public class ErrorMessageCommand : MessageCommand {
		public string ErrorText { get; private set; }

		public ErrorMessageCommand(Message message, IMessageSender sender, string errorText) : base(message, sender) {
			ErrorText = errorText;
		}

		public override void Execute() {
			_sender.Send(ErrorText, PeerId, ChatId);
		}
	}
}