using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCard.Models.Request
{
    public class CardRequest
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int CardSetId { get; set; }
    }
}
