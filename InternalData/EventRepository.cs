using Domein;

namespace InternalData
{
	public class EventRepository : IEventRepo
	{
		Dictionary<string, Event> _events;

		public EventRepository()
		{
			_events = new Dictionary<string, Event>();
		}

		public IEnumerable<Event> GetAllEvents()
		{
			throw new NotImplementedException();
		}

		public Event GetEventByDate(DateOnly datum)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Event> GetEventsByDate(DateOnly datum)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Event> GetEventsByLocation(string location)
		{
			throw new NotImplementedException();
		}

		public void AddEvent(Event _event)
		{
			throw new NotImplementedException();
		}

		public void RemoveEvent(Event _event)
		{
			throw new NotImplementedException();
		}

		public void UpdateEvent(Event _event)
		{
			throw new NotImplementedException();
		}

		public bool Exists(Event _event)
		{
			if (_events.ContainsKey(_event.Naam))
			{
				return true;
			}
			return false;
		}
	}
}
