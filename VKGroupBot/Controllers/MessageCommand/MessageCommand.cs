using VkNet.Model;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers {
	public abstract class MessageCommand: ICommand {
		protected MessageCommand(Message message, IMessageSender sender) {
			_sender = sender;
			PeerId = message.PeerId;
			Text = message.Text;
		}

		public abstract void Execute();

		protected void SendMessage(string text) {
			_sender.Send(text, PeerId);
		}

		protected void SendMessage(string text, MessageKeyboard keyboard) {
			_sender.Send(text, keyboard, PeerId);
		}

		public MessageCommandType Type { get; protected set; }
		public long? PeerId { get; private set; }
		public string Text { get; private set; }
		public string Params { get; protected set; }
		protected readonly IMessageSender _sender;
	}
}