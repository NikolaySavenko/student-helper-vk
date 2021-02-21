namespace VKGroupBot.Controllers {
	public class CarouselTry {
		/*
		 var msgObject = body.GetProperty("object").GetProperty("message");
					var message = JsonConvert.DeserializeObject<Message>(msgObject.ToString());
					var tmpl = new MessageTemplate {
						Type = TemplateType.Carousel,
						Elements = new List<CarouselElement>() {
							new CarouselElement {
								Title = message.Text + "_1",
								Description =  "Description 1",
								Action = new CarouselElementAction {
									Type = CarouselElementActionType.OpenLink,
									Link = new Uri("https://vk.com")
								},
								PhotoId = "-109837093_457242809",
								Buttons = new List<MessageKeyboardButton>() {
									new MessageKeyboardButton() {
										Action = new MessageKeyboardButtonAction() {
											Type = KeyboardButtonActionType.Text,
											Label = "lABEL"
										}
									}
								}
							},
							new CarouselElement {
								Title = message.Text + "_1",
								Description =  "Description 2",
								Action = new CarouselElementAction {
									Type = CarouselElementActionType.OpenLink,
									Link = new Uri("https://vk.com")
								},
								PhotoId = "-109837093_457242809",
								Buttons = new List<MessageKeyboardButton>() {
									new MessageKeyboardButton() {
										Action = new MessageKeyboardButtonAction() {
											Type = KeyboardButtonActionType.Text,
											Label = "lABEL"
										}
									}
								}
							}
						}
					};
					// Heroku dyno wake up for 10 secs and at this time vk make retry
					if (!Request.Headers.Keys.Contains("X-Retry-Counter"))
						_vkApi.Messages.Send(new MessagesSendParams {
							RandomId = new DateTime().Millisecond,
							PeerId = message.PeerId.Value,
							Message = message.Text,
							Template = tmpl
						});
		 */
	}
}