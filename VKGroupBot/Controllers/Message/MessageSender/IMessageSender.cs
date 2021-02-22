using System;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers {
	public interface IMessageSender {
		public void Send(string text, long? peerId, long? chatId);
		public void Send(string text, MessageKeyboard keyboard, long? peerId, long? chatId);
		public event EventHandler Sended;
	}
}