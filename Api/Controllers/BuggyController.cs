using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundResult()
        {
            var product = this.context.Product.Find(10001);
            if (product is null) { return NotFound(new ApiResponse(404)); }

            return Ok(product);
        }

        
        [HttpGet("servererror")]
        public IActionResult GetServerErrorResult()
        {
            var product = this.context.Product.Find(10001);
           var result=product.ToString();
            return Ok();
        }

        
        [HttpGet("badrequest")]
        public IActionResult GetBadRequestResult()
        {
            var product = this.context.Product.Find(10001);
           var result=product.ToString();
            return Ok();
        }



    }
}