using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Jabbot.Sprockets.Github
{
    public class Members:RegexSprocket
    {
        public override System.Text.RegularExpressions.Regex Pattern
        {
            get { return new Regex(@".*(members|people).*"); }
        }

        protected override void ProcessMatch(System.Text.RegularExpressions.Match match, Models.ChatMessage message, Bot bot)
        {
            var client = new NGitHub.GitHubClient();
            var org = new NGitHub.Services.OrganizationService(client);

            org.GetMembersAsync("NuGet", 
                members => {
                    bot.Say(string.Format("@{0}, Here are the people listed on github as part of the NuGet team. {1}. Why not get involved yourself? http://docs.nuget.org/docs/contribute/contributing-to-nuget", 
                        message.FromUser, String.Join(", ", members.Select(m => String.Format("{0} ({1})", m.Name, m.Login)))), message.Room);
                },
                e =>
                {
                    bot.Say(String.Format("I'm affriad I can't do that {0}. {1}", message.FromUser, e.ToString()), message.Room);
                });
        }
    }
}
