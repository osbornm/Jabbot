using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Jabbot.Models;

namespace Jabbot.Sprockets.Github
{
    public class Issue : RegexSprocket
    {
        public override Regex Pattern
        {
            get { return new Regex(@"^.*issue\s+(#)?(\d+)\s(for|in)\s(\w*)(\.)?"); }
        }

        protected override void ProcessMatch(Match match, ChatMessage message, Bot bot)
        {
            NGitHub.GitHubClient client = new NGitHub.GitHubClient();
            client.Issues.GetIssueAsync("nuget", match.Groups[4].Value, match.Groups[2].Value,
                issue =>
                {
                    var response = "{0}, '{1}' is {2}";
                    bot.Say(string.Format(response, message.FromUser, issue.Title, issue.State), message.Room);
                },
                e =>
                {
                    bot.Say(String.Format("I'm affriad I can't do that {0}. {1}", message.FromUser, e.ToString()), message.Room);
                });
        }
    }
}
