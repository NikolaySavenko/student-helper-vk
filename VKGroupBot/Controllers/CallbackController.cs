﻿using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;

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
			var type = body.GetProperty("type").GetString();

			switch (type) {
				case "confirmation":
					response = Environment.GetEnvironmentVariable("vk_response");
					break;
				case "message_new":
					var msgObject = body.GetProperty("object").GetProperty("message");
					var message = JsonConvert.DeserializeObject<Message>(msgObject.ToString());

					// Heroku dyno wake up for 10 secs and at this time vk make retry
					if (!Request.Headers.Keys.Contains("X-Retry-Counter")) {
						var command = commandFactory.CreateCommand(message);
						command.Execute();
					}
					break;
			}

			return Ok(response);
		}
	}
}