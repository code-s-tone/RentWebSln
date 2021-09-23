using Backstage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backstage.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RentContext _rentContext;
        public OrderController(RentContext rentContext)
        {
            _rentContext = rentContext;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return _rentContext.Orders;
        }

        // GET api/<OrderController>/5
        [HttpGet("{OrderID}")]
        public ActionResult<Order> Get(int OrderID)
        {
            var result = _rentContext.Orders.Find(OrderID);
            if(result == null)
            {
                return NotFound("找不到資源");
            }

            return result;
        }

        // POST api/<OrderController>
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order value)
        {
            _rentContext.Orders.Add(value);
            _rentContext.SaveChanges();
            return CreatedAtAction(nameof(Get),new { id = value.OrderId},value);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{OrderID}")]
        public IActionResult Put(int OrderID, [FromBody] Order value)
        {
            if (OrderID != value.OrderId)
            {
                return BadRequest();
            }
            _rentContext.Entry(value).State = EntityState.Modified;

            try
            {
                _rentContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (_rentContext.Orders.Any(x=>x.OrderId== OrderID))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                }
            }
            return NoContent();//更新成功 不傳任何內容

        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{OrderID}")]
        public IActionResult Delete(int OrderID)
        {
            var delete = _rentContext.Orders.Find(OrderID);
            if(delete == null)
            {
                return NotFound();
            }
            _rentContext.Orders.Remove(delete);
            _rentContext.SaveChanges();
            return NotFound();
        }
    }
}
