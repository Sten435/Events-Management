using BLEventService.Managers;
using BLEventService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using System.Xml.Linq;

namespace RESTEventService.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EventController : ControllerBase
	{
		private EventManager _eventManager;
		private VisitorManager _visitorManager;

		public EventController(EventManager eventManager, VisitorManager visitorManager)
		{
			_eventManager = eventManager;
			_visitorManager = visitorManager;
		}

		[HttpGet("{name}")]
		public ActionResult<Event> GetEvent(string name)
		{
			try
			{
				return Ok(_eventManager.GetByName(name));
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("/events")]
		public ActionResult<List<Event>> GetAll()
		{
			try
			{
				return Ok(_eventManager.GetAll());
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		public ActionResult<Event> PostEvent([FromBody] Event ev)
		{
			try
			{
				if (ev == null)
					return BadRequest("event is incorrect");
				_eventManager.AddEvent(ev);
				return CreatedAtAction(nameof(GetEvent), new { ev.Name }, ev);
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{name}")]
		public IActionResult PutEvent(string name, [FromBody] Event ev)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(name))
					return BadRequest("name is empty");
				if (ev == null)
					return BadRequest("event is incorrect");
				if (!name.Equals(ev.Name))
					return BadRequest("name does not match with event");
				if (!_eventManager.ExistEvent(ev))
					return NotFound(name);
				_eventManager.UpdateEvent(ev);
				return NoContent();
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{name}")]
		public IActionResult DeleteEvent(string name)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(name))
					return BadRequest("name is empty");
				if (!_eventManager.ExistEvent(name))
					return BadRequest("event does not exist");
				
				Event ev = _eventManager.GetByName(name);
				_eventManager.RemoveEvent(ev);
				
				return NoContent();
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost]
		[Route("{name}/visitors")]
		public ActionResult<Event> SubscribeVisitor(string name, [FromBody] int id)
		{
			try
			{
				Event ev = _eventManager.GetByName(name);
				Visitor visitor = _visitorManager.GetById(id);

				_eventManager.SubscribeVisitor(ev, visitor);
				return CreatedAtAction(nameof(GetEvent), new { ev.Name }, ev);
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		[Route("{name}/visitors/{id}")]
		public ActionResult<Event> UnsubscribeVisitor(string name, int id)
		{
			try
			{
				Event ev = _eventManager.GetByName(name);
				Visitor visitor = _visitorManager.GetById(id);

				_eventManager.UnSubscribeVisitor(ev, visitor);
				return CreatedAtAction(nameof(GetEvent), new { ev.Name }, ev);
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("location/{location}")]
		public ActionResult<List<Event>> GetEventByLocation(string location)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(location))
					return BadRequest("location is empty");

				return Ok(_eventManager.GetByLocation(location));
			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("date/{inputDate}")]
		public ActionResult<List<Event>> GetEventByDate(string inputDate)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(inputDate))
					return BadRequest("location is empty");

				bool isValid = DateTime.TryParse(inputDate, out DateTime date);

				if (isValid)
					return Ok(_eventManager.GetByDate(date));
				else
					return BadRequest("date is not valid");

			} catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
