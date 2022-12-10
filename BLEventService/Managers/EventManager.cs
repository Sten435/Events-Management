using BLEventService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEventService.Managers
{
	public class EventManager
	{

		
		public Dictionary<string, Event> _events = new();

		public EventManager()
		{
			_events.Add("a", new Event("a", "location a", 5, DateTime.Parse("24/10/2022")));
			_events.Add("b", new Event("b", "location b", 5, DateTime.Parse("25/10/2022")));
			_events.Add("c", new Event("c", "location c", 5, DateTime.Parse("22/10/2022")));
			_events.Add("d", new Event("d", "location d", 5, DateTime.Parse("20/10/2022")));
		}

		public IReadOnlyList<Event> GetAll()
		{
			return _events.Values.ToList().AsReadOnly();
		}

		public IReadOnlyList<Event> GetByLocation(string location)
		{
			if (string.IsNullOrWhiteSpace(location))
				throw new EventException("EventManager - GetByLocation");
			return _events.Values.Where(e => e.Location.ToLower() == location.ToLower()).ToList().AsReadOnly();
		}

		public IReadOnlyList<Event> GetByDate(DateTime date)
		{
			return _events.Values.Where(d => d.Date.Date == date.Date).ToList().AsReadOnly();
		}

		public void AddEvent(Event newEvent)
		{
			if (newEvent == null)
				throw new EventException("EventManager - AddEvent");
			if (_events.ContainsKey(newEvent.Name))
				throw new EventException("EventManager - AddEvent"); //TODO: Wat met events die al gepaseerd zijn
			_events.Add(newEvent.Name, newEvent);
		}

		public Event GetByName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EventException("EventManager - GetByName - name is empty");
			if (!_events.ContainsKey(name))
				throw new EventException("EventManager - GetByName - name does not exist");
			return _events[name];
		}

		public void SubscribeVisitor(Event _event, Visitor visitor)
		{
			if (_event == null)
				throw new EventException("EventManager - SubscribeVisitor");
			if (visitor == null)
				throw new EventException("EventManager - SubscribeVisitor");
			if (!_events.ContainsKey(_event.Name))
				throw new EventException("EventManager - SubscribeVisitor");
			_event.SubscribeVisitor(visitor);
		}

		public void RemoveEvent(Event _event)
		{
			if (_event == null)
				throw new EventException("EventManager - RemoveEvent");
			if (!_events.ContainsKey(_event.Name))
				throw new EventException("EventManager - RemoveEvent");
			_events.Remove(_event.Name);
		}

		public void UnSubscribeVisitor(Event _event, Visitor visitor)
		{
			if (_event == null)
				throw new EventException("EventManager - UnSubscribeVisitor");
			if (visitor == null)
				throw new EventException("EventManager - UnSubscribeVisitor");
			if (!_events.ContainsKey(_event.Name))
				throw new EventException("EventManager - UnSubscribeVisitor");
			_event.UnsubscribeVisitor(visitor);
		}

		public void UpdateEvent(Event _event)
		{
			if (_event == null)
				throw new EventException("EventManager - UpdateEvent");
			if (!_events.ContainsKey(_event.Name))
				throw new EventException("EventManager - UpdateEvent");
			if (_events[_event.Name].IsSame(_event))
				throw new EventException("VisitorManager - UpdateVisitor");
			//TODO: Wat met Visitors
			_events[_event.Name] = _event;
		}

		public bool ExistEvent(Event ev)
		{
			return ExistEvent(ev.Name);
		}

		public bool ExistEvent(string name)
		{
			return _events.ContainsKey(name);
		}
	}
}
