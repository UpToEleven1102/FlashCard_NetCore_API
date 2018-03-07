using FlashCard.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCard.Models
{
    public class CardEntity
    {
        public CardEntity() { }

        public CardEntity(CardRequest newCard) {
            this.Question = newCard.Question;
            this.Answer = newCard.Answer;
            this.CardSetId = newCard.CardSetId;
        }

        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int CardSetId { get; set; }
    }
}
