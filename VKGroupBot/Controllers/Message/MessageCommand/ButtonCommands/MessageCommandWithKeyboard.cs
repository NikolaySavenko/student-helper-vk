using VkNet.Model;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers {
	public abstract class MessageCommandWithKeyboard : MessageCommand  {
		public MessageCommandWithKeyboard(Message message, IMessageSender sender) : base(message, sender) { }
		// TODO remove if unused
	}
}