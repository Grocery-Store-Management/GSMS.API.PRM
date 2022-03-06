using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GSMS.API.PRM.Models;
using Microsoft.AspNetCore.Authorization;

namespace GSMS.API.PRM.Controllers
{
    [Route("api/receipt-details")]
    [ApiController]
    [Authorize]
    public class ReceiptDetailsController : ControllerBase
    {
        private readonly GsmsContext _context;

        public ReceiptDetailsController(GsmsContext context)
        {
            _context = context;
        }

        // GET: api/ReceiptDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptDetail>>> GetReceiptDetails([FromQuery] string receiptId)
        {
            if (!string.IsNullOrEmpty(receiptId))
            {
                return await _context.ReceiptDetails.Where(rd => rd.ReceiptId.Equals(receiptId)).ToListAsync();
            }
            return await _context.ReceiptDetails.ToListAsync();
        }

        // GET: api/ReceiptDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiptDetail>> GetReceiptDetail(string id)
        {
            var receiptDetail = await _context.ReceiptDetails.FindAsync(id);

            if (receiptDetail == null)
            {
                return NotFound();
            }

            return receiptDetail;
        }

        // PUT: api/ReceiptDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceiptDetail(string id, ReceiptDetail receiptDetail)
        {
            if (id != receiptDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(receiptDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptDetailExists(id))
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

        // POST: api/ReceiptDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceiptDetail>> PostReceiptDetail(ReceiptDetail receiptDetail)
        {
            receiptDetail.Id = Guid.NewGuid().ToString();
            receiptDetail.CreatedDate = DateTime.Now;

            _context.ReceiptDetails.Add(receiptDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReceiptDetailExists(receiptDetail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReceiptDetail", new { id = receiptDetail.Id }, receiptDetail);
        }

        // DELETE: api/ReceiptDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceiptDetail(string id)
        {
            var receiptDetail = await _context.ReceiptDetails.FindAsync(id);
            if (receiptDetail == null)
            {
                return NotFound();
            }

            _context.ReceiptDetails.Remove(receiptDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptDetailExists(string id)
        {
            return _context.ReceiptDetails.Any(e => e.Id == id);
        }
    }
}
