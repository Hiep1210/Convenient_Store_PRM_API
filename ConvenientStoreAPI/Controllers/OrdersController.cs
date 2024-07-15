using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConvenientStoreAPI.Models;
using Microsoft.AspNetCore.OData.Query;
using ConvenientStoreAPI.Common;
using ConvenientStoreAPI.Mapper.DTO;
using AutoMapper;

namespace ConvenientStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ConvenientStoreContext _context;
        private IMapper mapper;

        public OrdersController(ConvenientStoreContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.Include(x => x.User)
                .Include(x => x.Orderdetails)
                .ThenInclude(detail => detail.Product).ThenInclude(p => p.Supplier)
                .Include(x => x.Orderdetails)
                .ThenInclude(detail => detail.Product.Image)
                .Include(x => x.Orderdetails)
                .ThenInclude(detail => detail.Product.Cat)
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.Include(x => x.Orderdetails).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }
            Order order = mapper.Map<Order>(request);

            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        [HttpGet("Process/{id}")]
        public async Task<IActionResult> ProcessOrder(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NoContent();
            }
            order.IsProcess = true;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderRequest request)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'ConvenientStoreContext.Orders'  is null.");
          }
            Order order = mapper.Map<Order>(request);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
