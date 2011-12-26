using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Jabbot.Models;

namespace Jabbot.Sprockets.Github
{
    public class Issues : RegexSprocket
    {
        public override Regex Pattern
        {
            get { return new Regex(@"^.*issues.*for\s+(the\s+)?(\w*)(\?|\.)?"); }
        }

        protected override void ProcessMatch(Match match, ChatMessage message, Bot bot)
        {
            NGitHub.GitHubClient client = new NGitHub.GitHubClient();
            client.Issues.GetIssuesAsync("nuget", match.Groups[2].Value, NGitHub.Models.State.Open,
                issues =>
                {
                    string issueCount = "";
                    if (issues.Any())
                    {
                        issueCount = "@{0}, {1} has {2} issues.";
                    }
                    else
                    {
                        issueCount = "@{0}, {1} has no issues, this has to be wrong. I know that code isn't perfect.";
                    }
                    bot.Say(string.Format(issueCount, message.FromUser, match.Groups[2], issues.Count()), message.Room);
                },
                e =>
                {
                    bot.Say(String.Format("I'm affriad I can't do that {0}. {1}", message.FromUser, e.ToString()), message.Room);
                });
        }
    }
}
