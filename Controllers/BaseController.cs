using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [ApiController]
    public abstract class BaseController<TModel> : ControllerBase where TModel : BaseModel
    {
        private readonly PlaylistContext context;
        private readonly BaseBusiness<TModel> business;

        public BaseController(PlaylistContext context)
        {
            this.context = context;
            business = new BaseBusiness<TModel>(context);
        }

        [HttpGet]
        public virtual IActionResult Get([FromQuery]int id)
        {
            try
            {
                var result = business.Get(id);
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
            catch { return BadRequest($"Could not get {typeof(TModel)} by id."); }
        }

        [HttpPost]
        public virtual IActionResult Insert([FromBody]TModel model)
        {
            var result = business.Insert(model);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not insert {typeof(TModel)} on database.");
        }

        [HttpPut]
        public virtual IActionResult Update([FromBody]TModel model)
        {
            var result = business.Update(model);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not update {typeof(TModel)} on database.");
        }

        [HttpDelete]
        public virtual IActionResult Delete([FromQuery]int id)
        {
            var result = business.Delete(id);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not delete {typeof(TModel)} from database.");
        }

        [HttpPatch]
        public virtual IActionResult Recover([FromQuery] int id)
        {
            var result = business.Recover(id);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not recover {typeof(TModel)} from database.");
        }
    }
}