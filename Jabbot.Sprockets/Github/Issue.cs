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
            bot.Say(
                string.Format("Here you go @{0}, http://github.com/Nuget/{1}/issues/{2}", message.FromUser, match.Groups[4].Value, match.Groups[2].Value),
                message.Room);
        }
    }
}
