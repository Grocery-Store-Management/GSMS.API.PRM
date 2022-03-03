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
    [Route("api/receipts")]
    [ApiController]
    [Authorize]
    public class ReceiptsController : ControllerBase
    {
        private readonly GsmsContext _context;

        public ReceiptsController(GsmsContext context)
        {
            _context = context;
        }

        // GET: api/Receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipts()
        {
            return await _context.Receipts.ToListAsync();
        }

        // GET: api/Receipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(string id)
        {
            var receipt = await _context.Receipts.FindAsync(id);

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }

        // PUT: api/Receipts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceipt(string id, Receipt receipt)
        {
            if (id != receipt.Id)
            {
                return BadRequest();
            }

            _context.Entry(receipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // POST: api/Receipts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receipt>> PostReceipt(Receipt receipt)
        {
            receipt.Id = Guid.NewGuid().ToString();
            receipt.CreatedDate = DateTime.Now;
            receipt.IsDeleted = false;

            if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Any())
            {
                foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
                {
                    receiptDetail.Id = Guid.NewGuid().ToString();
                    receiptDetail.CreatedDate = DateTime.Now;
                }
            }
            _context.Receipts.Add(receipt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReceiptExists(receipt.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReceipt", new { id = receipt.Id }, receipt);
        }

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(string id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            receipt.IsDeleted = true;
            _context.Receipts.Update(receipt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptExists(string id)
        {
            return _context.Receipts.Any(e => e.Id == id);
        }
    }
}
