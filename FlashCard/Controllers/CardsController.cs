using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashCard.Infrastructures;
using FlashCard.Models;
using FlashCard.Models.Request;

namespace FlashCard.Controllers
{
    [Produces("application/json")]
    [Route("api/Cards")]
    public class CardsController : Controller
    {
        private readonly FlashCardContext _context;

        public CardsController(FlashCardContext context)
        {
            _context = context;
        }

        // GET: api/CardEntities
        [HttpGet]
        public IEnumerable<CardEntity> GetCardEntity()
        {
            return _context.CardEntity;
        }

        [HttpGet("set/{setId}")]
        public IEnumerable<CardEntity> GetCardEntitiesBySetId([FromRoute] int setId) {
            return _context.CardEntity.Where(card => card.CardSetId == setId);
        }

        // GET: api/CardEntities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCardEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cardEntity = await _context.CardEntity.SingleOrDefaultAsync(m => m.ID == id);

            if (cardEntity == null)
            {
                return NotFound();
            }

            return Ok(cardEntity);
        }

        // PUT: api/CardEntities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardEntity([FromRoute] int id, [FromBody] CardEntity cardEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardEntity.ID)
            {
                return BadRequest();
            }

            _context.Entry(cardEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardEntityExists(id))
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

        // POST: api/CardEntities
        [HttpPost]
        public async Task<IActionResult> PostCardEntity([FromBody] CardRequest cardRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cardEntity = new CardEntity(cardRequest);
            _context.CardEntity.Add(cardEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardEntity", new { id = cardEntity.ID }, cardEntity);
        }

        // DELETE: api/CardEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cardEntity = await _context.CardEntity.SingleOrDefaultAsync(m => m.ID == id);
            if (cardEntity == null)
            {
                return NotFound();
            }

            _context.CardEntity.Remove(cardEntity);
            await _context.SaveChangesAsync();

            return Ok(cardEntity);
        }

        private bool CardEntityExists(int id)
        {
            return _context.CardEntity.Any(e => e.ID == id);
        }
    }
}