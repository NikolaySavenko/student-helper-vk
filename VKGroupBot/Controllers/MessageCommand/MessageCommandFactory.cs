﻿using VkNet.Abstractions;
using VkNet.Model;

namespace VKGroupBot.Controllers {
	public class MessageCommandFactory {
		private readonly IVkApi _vkApi;
		private readonly IMessageSender _sender;
		public MessageCommandFactory(IVkApi api) {
			_vkApi = api;
			_sender = new MessageSender(_vkApi);
		}

		public ICommand CreateCommand(Message message) {
			switch (message.Text.Split()[0]) {
				case "/zoom":
					return new ZoomLinkCommand(message, _sender);
				default:
					return new EmptyMessageCommand(message, _sender);
			}
		}
	}
}