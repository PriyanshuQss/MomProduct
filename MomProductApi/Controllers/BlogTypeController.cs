using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MomProduct.Model;
using MomProductApi.Repositories;

namespace MomProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTypeController : ControllerBase
    {
        private readonly IBlogTypeRepository _blogTypeRepository;

        public BlogTypeController(IBlogTypeRepository blogTypeRepository)
        {
            _blogTypeRepository = blogTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogType()
        {
            var blogTypes = await _blogTypeRepository.GetAllAsync();
            return Ok(blogTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogTypeById(int id)
        {
            var blogType = await _blogTypeRepository.GetByIdAsync(id);
            if (blogType == null) return NotFound();
            return Ok(blogType);
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogType(BlogType blogType)
        {
            await _blogTypeRepository.AddAsync(blogType);
            return CreatedAtAction(nameof(GetBlogTypeById), new { id = blogType.Id }, blogType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogType(int id, BlogType blogType)
        {
            if (id != blogType.Id) return BadRequest();
            await _blogTypeRepository.UpdateAsync(blogType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogType(int id)
        {
            await _blogTypeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
