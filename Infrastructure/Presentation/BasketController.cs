using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet("{id}")] // get baseurl/api/Basket/value
        public async Task<ActionResult<BasketDTO>> Get(string id)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost] // post baseUrl/api/Basket
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDto)
        {
            var basket = await serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }
        [HttpDelete("{id}")] 
        public async Task<ActionResult> Delete(string id)
        {
             await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
