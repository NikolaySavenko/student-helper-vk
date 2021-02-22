using System;
using VkNet.Abstractions;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace VKGroupBot.Controllers {
	public class MessageSender : IMessageSender {
		private readonly IVkApi _vkApi;

		public MessageSender(IVkApi vkApi) {
			_vkApi = vkApi;
		}

		public event EventHandler Sended = (sender, args) => { };

		public void Send(string text, long? peerId, long? chatId) {
			_vkApi.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = peerId,
				Message = text,
				ChatId = chatId
			});
			Sended(this, null);
		}

		public void Send(string text, MessageKeyboard keyboard, long? peerId, long? chatId) {
			_vkApi.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = peerId,
				Message = text,
				Keyboard = keyboard,
				ChatId = chatId
			});
			Sended(this, null);
		}
	}
}