using System;
using VkNet.Abstractions;
using VkNet.Model.RequestParams;

namespace VKGroupBot.Controllers {
	public class MessageSender: IMessageSender {
		private readonly IVkApi _vkApi;
		public event EventHandler Sended = (sender, args) => { };
		public MessageSender(IVkApi vkApi) {
			_vkApi = vkApi;
		}

		public void Send(string text, long? peerId) {
			_vkApi.Messages.Send(new MessagesSendParams {
				RandomId = new DateTime().Millisecond,
				PeerId = peerId,
				Message = text
			});
			Sended(this, null);
		}
	}
}