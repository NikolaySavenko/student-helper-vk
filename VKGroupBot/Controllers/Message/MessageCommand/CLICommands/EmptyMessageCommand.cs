using VkNet.Model;

namespace VKGroupBot.Controllers {
	// NullObjectPattern?
	public class EmptyMessageCommand: MessageCommand {
		public EmptyMessageCommand(Message message, IMessageSender sender) : base(message, sender) { }
		public override void Execute() { }
	}
}