using BLEventService.Managers;
using BLEventService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;

namespace RESTEventService.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class VisitorController : ControllerBase
	{
		private VisitorManager _visitorManager;

		public VisitorController(VisitorManager visitorManager)
		{
			_visitorManager = visitorManager;
		}

		[HttpGet]
		public ActionResult<List<Visitor>> GetAll()
		{
			try
			{
				return Ok(_visitorManager.GetAll());
			} catch (Exception error)
			{
				return BadRequest(error.Message);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<Visitor> GetVisitorById(int id)
		{
			try
			{
				return Ok(_visitorManager.GetById(id));
			} catch (Exception error)
			{
				return BadRequest(error.Message);
			}
		}

		[HttpPost]
		public ActionResult<Visitor> RegisterVisitor([FromBody] Visitor visitor)
		{
			try
			{
				if (visitor == null)
					return BadRequest("visitor not valid");
				Visitor newVisitor = _visitorManager.RegisterVisitor(visitor);
				_visitorManager.SubscribeVisitor(newVisitor);
				return CreatedAtAction(nameof(GetVisitorById), new { Id = visitor.Id }, visitor);
			} catch (Exception error)
			{
				return BadRequest(error.Message);
			}
		}

		[HttpPut("{id}")]
		public ActionResult<Visitor> UpdateVisitor(int id, [FromBody] Visitor visitor)
		{
			try
			{
				if (visitor == null)
					return BadRequest("visitor not valid");
				if (visitor.Id != id)
					return BadRequest("id not valid");
				_visitorManager.UpdateVisitor(visitor);
				return NoContent();
			} catch (Exception error)
			{
				return BadRequest(error.Message);
			}
		}

		[HttpDelete("{id}")]
		public ActionResult<Visitor> UpdateVisitor(int id)
		{
			try
			{
				Visitor visitor = _visitorManager.GetById(id);
				_visitorManager.Unsubscribe(visitor);
				return NoContent();
			} catch (Exception error)
			{
				return BadRequest(error.Message);
			}
		}
	}
}
