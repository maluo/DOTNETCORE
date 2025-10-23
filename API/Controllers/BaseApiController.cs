using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatedPagedResult<T>(IGenericRepository<T> repo,
        ISpec<T> spec, int pageIndex, int pageSize) where T : Base
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
            return Ok(pagination);
        }
        
    }
}