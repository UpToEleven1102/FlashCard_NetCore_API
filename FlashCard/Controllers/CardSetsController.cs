using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashCard.Infrastructures;
using FlashCard.Models;

namespace FlashCard.Controllers
{
    [Produces("application/json")]
    [Route("api/CardSets")]
    public class CardSetsController : Controller
    {
        private readonly FlashCardContext _context;

        public CardSetsController(FlashCardContext context)
        {
            _context = context;
        }

        // GET: api/CardSet
        [HttpGet]
        public IEnumerable<CardSetEntity> GetCardSetEntity()
        {
            return _context.CardSetEntity;
        }

        // GET: api/CardSet/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardSetEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cardSetEntity = await _context.CardSetEntity.SingleOrDefaultAsync(m => m.Id == id);

            if (cardSetEntity == null)
            {
                return NotFound();
            }

            return Ok(cardSetEntity);
        }

        // PUT: api/CardSet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardSetEntity([FromRoute] int id, [FromBody] CardSetEntity cardSetEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardSetEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(cardSetEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardSetEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CardSet
        [HttpPost]
        public async Task<IActionResult> PostCardSetEntity([FromBody] CardSetEntity cardSetEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CardSetEntity.Add(cardSetEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardSetEntity", new { id = cardSetEntity.Id }, cardSetEntity);
        }

        // DELETE: api/CardSet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardSetEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cardSetEntity = await _context.CardSetEntity.SingleOrDefaultAsync(m => m.Id == id);
            if (cardSetEntity == null)
            {
                return NotFound();
            }

            _context.CardSetEntity.Remove(cardSetEntity);
            await _context.SaveChangesAsync();

            return Ok(cardSetEntity);
        }

        private bool CardSetEntityExists(int id)
        {
            return _context.CardSetEntity.Any(e => e.Id == id);
        }
    }
}