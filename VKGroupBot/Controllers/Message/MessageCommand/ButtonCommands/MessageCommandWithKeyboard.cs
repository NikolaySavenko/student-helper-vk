using VKGroupBot.Controllers.Attributes;
using VkNet.Model;

namespace VKGroupBot.Controllers {
	[KeyboardDemand(true)]
	public abstract class MessageCommandWithKeyboard : MessageCommand {
		public MessageCommandWithKeyboard(Message message, IMessageSender sender) : base(message, sender) { }
		// TODO remove if unused
	}
}