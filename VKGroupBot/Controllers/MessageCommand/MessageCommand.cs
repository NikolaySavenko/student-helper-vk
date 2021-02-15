using VkNet.Model;

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

		public MessageCommandType Type { get; protected set; }
		public long? PeerId { get; private set; }
		public string Text { get; private set; }
		public string Params { get; protected set; }
		private readonly IMessageSender _sender;
	}
}