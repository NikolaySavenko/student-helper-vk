using System;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers {
	public interface IMessageSender {
		public void Send(string text, long? peerId);
		public void Send(string text, MessageKeyboard keyboard, long? peerId);
		public event EventHandler Sended;
	}
}