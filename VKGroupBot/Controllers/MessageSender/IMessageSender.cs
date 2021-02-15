using System;

namespace VKGroupBot.Controllers {
	public interface IMessageSender {
		public void Send(string text, long? peerId);
		public event EventHandler Sended;
	}
}