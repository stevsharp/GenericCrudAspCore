using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IUnitOfWork _IUnitOfWork;
        public BlogController(IUnitOfWork IUnitOfWork)
        {
            _IUnitOfWork = IUnitOfWork;
        }

        [HttpGet]
        public ActionResult<List<Blog>> GetAll()
        {
            return _IUnitOfWork.Repository<Blog>()
                    .Query()
                    .Get()
                    .ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Blog>> GetAllWithFilter()
        {
            return _IUnitOfWork.Repository<Blog>()
                    .Query()
                    .Filter(x=>x.BlogId > 1)
                    .Get()
                    .OrderBy(x=>x.BlogId)
                    .ToList();
        }

        [HttpGet("{id}", Name = "GetBlog")]
        public ActionResult<Blog> GetById(int id)
        {
            Blog item = _IUnitOfWork.Repository<Blog>().FindById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(Blog item)
        {
            item.State = ObjectState.Added;

            _IUnitOfWork.Repository<Blog>().InsertGraph(item);
            _IUnitOfWork.Save();

            return CreatedAtRoute("GetBlog", new { id = item.BlogId }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Blog item)
        {
            Blog ediItem = _IUnitOfWork.Repository<Blog>().FindById(id);
            if (ediItem == null)
            {
                return NotFound();
            }

            ediItem.State = ObjectState.Modified;
            ediItem.Url = item.Url;

            _IUnitOfWork.Save();

            return NoContent();
        }


    }
}

