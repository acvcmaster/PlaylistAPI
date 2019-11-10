using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [ApiController]
    public abstract class BaseController<TModel, TBusiness> : ControllerBase
        where TModel : BaseModel
        where TBusiness : BaseBusiness<TModel>, new()
    {
        private readonly PlaylistContext _context;
        public TBusiness Business { get; set; }

        public BaseController(PlaylistContext context)
        {
            this._context = context;
            Business = new TBusiness();
            Business.Context = _context;
        }

        [HttpGet]
        public virtual IActionResult Get([FromQuery]int id)
        {
            try
            {
                var result = Business.Get(id);
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
            catch { return BadRequest($"Could not get {typeof(TModel).Name} by id."); }
        }

        [HttpPost]
        public virtual IActionResult Insert([FromBody]TModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            var result = Business.Insert(model);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not insert {typeof(TModel).Name} on database.");
        }

        [HttpPut]
        public virtual IActionResult Update([FromBody]TModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            var result = Business.Update(model);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not update {typeof(TModel).Name} on database.");
        }

        [HttpDelete]
        public virtual IActionResult Delete([FromQuery]int id)
        {
            var result = Business.Delete(id);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not delete {typeof(TModel).Name} from database.");
        }

        [HttpPatch]
        public virtual IActionResult Recover([FromQuery] int id)
        {
            var result = Business.Recover(id);
            if (result != null)
                return Ok(result);
            return BadRequest($"Could not recover {typeof(TModel).Name} from database.");
        }
    }
}