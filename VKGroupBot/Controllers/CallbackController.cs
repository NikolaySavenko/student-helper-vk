using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.GroupUpdate;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VKGroupBot.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class CallbackController : ControllerBase {
		private readonly IConfiguration _configuration;
		private readonly IVkApi _vkApi;
		private readonly MessageCommandFactory commandFactory;

		public CallbackController(IVkApi vkApi, IConfiguration configuration) {
			_vkApi = vkApi;
			_configuration = configuration;
			commandFactory = new MessageCommandFactory(vkApi);
		}

		[HttpPost]
		public IActionResult Callback([FromBody] JsonElement body) {
			var response = "ok";

			// Heroku dyno wake up for 10 secs and at this time vk make retry
			if (!Request.Headers.Keys.Contains("X-Retry-Counter")) {
				var jToken = JToken.Parse(body.ToString());
				var vkResponse = new VkResponse(jToken);
				var update = GroupUpdate.FromJson(vkResponse);
				if (update.Type == GroupUpdateType.Confirmation) {
					response = Environment.GetEnvironmentVariable("vk_response");
				}
				else if (update.Type == GroupUpdateType.MessageNew) {
					var messageNew = update.MessageNew;
					var command = commandFactory.CreateCommand(messageNew);
					command.Execute();
				}
				else if (update.Type == GroupUpdateType.MessageEvent) {
					var messageEvent = update.MessageEvent;
					var payload = JsonConvert.DeserializeObject<ButtonPayload>(messageEvent.Payload);
					if (payload.CommandController == TimetableCommand.CommandStart) {
						var timetable = TimetableCommand.Factory.MakeTimetable(payload);
						timetable.Action(payload);
						_vkApi.Messages.Edit(new MessageEditParams {
							PeerId = messageEvent.PeerId.Value,
							Message = timetable.ToString(),
							ConversationMessageId = messageEvent.ConversationMessageId,
							Keyboard = timetable.BuildKeyboard()
						});
					}
				}
			}

			return Ok(response);
		}
	}
}