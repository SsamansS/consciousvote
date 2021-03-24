using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot_10._02.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int LawId { get; set; }
        public Law Law { get; set; }
        public int DeputyId { get; set; }
        public Deputy Deputy { get; set; }
        public string Decision { get; set; }
    }
}
