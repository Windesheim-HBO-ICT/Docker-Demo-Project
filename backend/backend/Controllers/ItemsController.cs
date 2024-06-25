using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {
        private readonly Context _context;

        public ItemsController(Context context) {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems() {
            return await _context.Items.OrderBy(i => i.Id).ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id) {
            var item = await _context.Items.FindAsync(id);

            if (item == null) {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id) {
            try {
                Item? oldItem = await _context.Items.FindAsync(id);

                oldItem.IsChecked = !oldItem.IsChecked;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!ItemExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return Ok(new { deleted = "success", id = id });
        }

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item) {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id) {
            var item = await _context.Items.FindAsync(id);
            if (item == null) {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { deleted = "success", id = id });
        }

        private bool ItemExists(int id) {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
