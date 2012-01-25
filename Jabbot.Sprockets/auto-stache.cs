using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Jabbot.Sprockets
{
    public class auto_stache: RegexSprocket
    {
        public override System.Text.RegularExpressions.Regex Pattern
        {
            get { return new Regex(@"^.*stache\s+<a.*>(https?:\/\/[^ #]+\.(?:png|jpg|jpeg))(?:[#]\.png)?</a>"); }
        }

        protected override void ProcessMatch(System.Text.RegularExpressions.Match match, Models.ChatMessage message, Bot bot)
        {
            bot.Say("{0} http://mustachify.me/?src={1}", message.Room, message.FromUser, HttpUtility.UrlEncode(match.Groups[1].Value));
        }
    }
}
