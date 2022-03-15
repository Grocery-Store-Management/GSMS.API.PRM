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
    [Route("api/import-orders")]
    [ApiController]
    [Authorize]
    public class ImportOrdersController : ControllerBase
    {
        private readonly GsmsContext _context;

        public ImportOrdersController(GsmsContext context)
        {
            _context = context;
        }

        // GET: api/ImportOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportOrder>>> GetImportOrders()
        {
            return await _context.ImportOrders
                .Include(io => io.Store)
                .Where(io => !io.IsDeleted)
                .OrderByDescending(io => io.CreatedDate)
                .ToListAsync();
        }

        // GET: api/ImportOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportOrder>> GetImportOrder(string id)
        {
            var importOrder = await _context.ImportOrders.FindAsync(id);

            if (importOrder == null)
            {
                return NotFound();
            }

            return importOrder;
        }

        // PUT: api/ImportOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportOrder(string id, ImportOrder importOrder)
        {
            if (id != importOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(importOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportOrderExists(id))
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

        // POST: api/ImportOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImportOrder>> PostImportOrder(ImportOrder importOrder)
        {
            importOrder.Id = Guid.NewGuid().ToString();
            importOrder.CreatedDate = DateTime.Now;
            importOrder.IsDeleted = false;

            if (importOrder.ImportOrderDetails != null && importOrder.ImportOrderDetails.Any())
            {
                foreach (ImportOrderDetail importOrderDetail in importOrder.ImportOrderDetails)
                {
                    importOrderDetail.Id = Guid.NewGuid().ToString();
                    importOrderDetail.IsDeleted = false;
                }
            }
            _context.ImportOrders.Add(importOrder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImportOrderExists(importOrder.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImportOrder", new { id = importOrder.Id }, importOrder);
        }

        // DELETE: api/ImportOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportOrder(string id)
        {
            var importOrder = await _context.ImportOrders.FindAsync(id);
            if (importOrder == null)
            {
                return NotFound();
            }
            importOrder.IsDeleted = true;
            _context.ImportOrders.Update(importOrder);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ImportOrderExists(string id)
        {
            return _context.ImportOrders.Any(e => e.Id == id);
        }
    }
}
