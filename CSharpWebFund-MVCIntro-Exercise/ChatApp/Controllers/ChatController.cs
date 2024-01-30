using ChatApp.Models.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ChatApp.Controllers
{
	public class ChatController : Controller
	{

		private static readonly List<KeyValuePair<string, string>> messages = new List<KeyValuePair<string, string>>();

		public IActionResult Show()
		{
			if (messages.Count < 0)
			{
				return this.View(new ChatViewModel());
			}

			var chatViewModel = new ChatViewModel()
			{
				AllMessages = messages
				.Select(m => new MessageViewModel()
				{
					Sender = m.Key,
					MessageText = m.Value
				})
				.ToArray()
			};

			return this.View(chatViewModel);
		}

		[HttpPost]
		public IActionResult Send(ChatViewModel chat)
		{
			var newMessage = chat.CurrentMessage;

			messages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));

			return RedirectToAction("Show");
		}
	}
}
