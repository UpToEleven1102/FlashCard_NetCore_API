using FlashCard.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCard.Models
{
    public class CardSetEntity
    {
        public CardSetEntity() { }

        public CardSetEntity(CardSetRequest cardSet) {
            this.Title = cardSet.Title;
        }

        public int Id { get; set; }
        public string Title { get; set; }

    }
}
