using System.Linq;
using System.Text;
using VkNet.Model;

namespace VKGroupBot.Controllers {
	public class ZoomLinkCommand : MessageCommand {
		public const string CommandStart = "/zoom";
		public const string ParamLecture = "лекция";
		public const string ParamPractice = "практика";
		public const string ParamInfo = "предметы";

		public ZoomLinkCommand(Message message, IMessageSender sender) : base(message, sender) {
			Type = MessageCommandType.Zoom;
			Params = Text.Replace($"{CommandStart} ", "");
		}

		public void ShowSubjects() {
			using (var db = new PostgresContext()) {
				var links = db.Links.ToList();
				var sb = new StringBuilder();
				foreach (var link in links) sb.AppendLine(link.SubjectName);
				SendMessage(sb.ToString());
			}
		}

		public void ShowSubjectInfo(string type) {
			var subject = Params.Replace(type + " ", "");
			SendMessage($"Zoom links for: {Params}");
			using (var db = new PostgresContext()) {
				var links = db.Links.ToList();
				var chosenLinks = from link1 in links
					where link1.SubjectName == subject
					orderby link1.SubjectName
					select link1;

				foreach (var link in chosenLinks) {
					var sb = new StringBuilder();
					var connectionLink = string.Empty;
					var code = string.Empty;
					var pass = string.Empty;
					if (type == ParamLecture) {
						if (link.LecturesLink != null) {
							connectionLink = link.LecturesLink;
						}
						else {
							code = link.LecturesCode;
							pass = link.LecturesPassword;
						}
					}
					else if (type == ParamPractice) {
						if (link.PracticesLink != null) {
							connectionLink = link.PracticesLink;
						}
						else {
							code = link.PracticesCode;
							pass = link.PracticesPassword;
						}
					}
					else {
						sb.AppendLine("Ты запрашиваешь информацию, но делаешь это без уважения.");
						sb.AppendLine(
							"А ну-ка давай играй по правилам! Умник тут нашелся! Думаешь разраб не предугадал твой ход?");
					}

					if (connectionLink != string.Empty || code != string.Empty) {
						sb.AppendLine($"Предмет ({type}): {link.SubjectName}");
						if (connectionLink != string.Empty) {
							sb.AppendLine($"Ссылка: {connectionLink}");
						}
						else {
							sb.AppendLine($"Код: {code}");
							sb.AppendLine($"Пароль: {pass}");
						}
					}

					SendMessage(sb.ToString());
				}
			}
		}

		public override void Execute() {
			var type = Params.Split()[0];
			if (type == ParamInfo)
				ShowSubjects();
			else if (type == ParamLecture || type == ParamPractice) ShowSubjectInfo(type);
		}
	}
}