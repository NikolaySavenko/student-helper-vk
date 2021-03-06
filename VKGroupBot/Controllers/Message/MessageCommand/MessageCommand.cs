﻿using VKGroupBot.Controllers.Attributes;
using VkNet.Model;
using VkNet.Model.Keyboard;

namespace VKGroupBot.Controllers {
	[KeyboardDemand]
	public abstract class MessageCommand : ICommand {
		protected readonly IMessageSender _sender;

		protected MessageCommand(Message message, IMessageSender sender) {
			_sender = sender;
			PeerId = message.PeerId;
			Text = message.Text;
			ChatId = message.ChatId;
		}

		public MessageCommandType Type { get; protected set; }
		public long? PeerId { get; }
		public long? ChatId { get; }
		public string Text { get; }
		public string Params { get; protected set; }

		public abstract void Execute();

		protected void SendMessage(string text) {
			_sender.Send(text, PeerId, ChatId);
		}

		protected void SendMessage(string text, MessageKeyboard keyboard) {
			_sender.Send(text, keyboard, PeerId, ChatId);
		}
	}
}