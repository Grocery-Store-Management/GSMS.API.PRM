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
    [Route("api/import-order-details")]
    [ApiController]
    [Authorize]
    public class ImportOrderDetailsController : ControllerBase
    {
        private readonly GsmsContext _context;

        public ImportOrderDetailsController(GsmsContext context)
        {
            _context = context;
        }

        // GET: api/ImportOrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportOrderDetail>>> GetImportOrderDetails([FromQuery] string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                return await _context.ImportOrderDetails.Where(io => io.OrderId.Equals(orderId)).ToListAsync();
            }
            return await _context.ImportOrderDetails.ToListAsync();
        }

        // GET: api/ImportOrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportOrderDetail>> GetImportOrderDetail(string id)
        {
            var importOrderDetail = await _context.ImportOrderDetails.FindAsync(id);

            if (importOrderDetail == null)
            {
                return NotFound();
            }

            return importOrderDetail;
        }

        // PUT: api/ImportOrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportOrderDetail(string id, ImportOrderDetail importOrderDetail)
        {
            if (id != importOrderDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(importOrderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportOrderDetailExists(id))
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

        // POST: api/ImportOrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImportOrderDetail>> PostImportOrderDetail(ImportOrderDetail importOrderDetail)
        {
            importOrderDetail.Id = Guid.NewGuid().ToString();
            importOrderDetail.IsDeleted = false;

            _context.ImportOrderDetails.Add(importOrderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImportOrderDetailExists(importOrderDetail.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImportOrderDetail", new { id = importOrderDetail.Id }, importOrderDetail);
        }

        // DELETE: api/ImportOrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportOrderDetail(string id)
        {
            var importOrderDetail = await _context.ImportOrderDetails.FindAsync(id);
            if (importOrderDetail == null)
            {
                return NotFound();
            }

            //_context.ImportOrderDetails.Remove(importOrderDetail);
            importOrderDetail.IsDeleted = true;
            _context.ImportOrderDetails.Update(importOrderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportOrderDetailExists(string id)
        {
            return _context.ImportOrderDetails.Any(e => e.Id == id);
        }
    }
}
