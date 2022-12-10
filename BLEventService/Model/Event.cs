using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEventService.Model
{
    public class Event
    {
        public string Name { get; private set; }
        public string Location { get; private set; }
        public int MaxVisitors { get; private set; }
        public DateTime Date { get; set; }
        private Dictionary<int, Visitor> _visitors = new Dictionary<int, Visitor>();
		public IReadOnlyList<Visitor> Visitors => _visitors.Values.ToList();

		public Event(string name, string location, int maxVisitors, DateTime date)
        {
            SetName(name);
            SetLocation(location);
            SetMaxVisitors(maxVisitors);
            Date = date;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EventException("Event - SetName");
            Name = name;
        }
        public void SetLocation(string loc)
        {
            if (string.IsNullOrWhiteSpace(loc)) throw new EventException("Event - SetLocation");
            Location = loc;
        }
        public void SetMaxVisitors(int max)
        {
            if (max <= 0) throw new EventException("Event - SetMaxVisitors");
            MaxVisitors = max;
        }
        public void SubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("Event - SubscribeVisitor");
            if (_visitors.Values.Count >= MaxVisitors) throw new EventException("Event - SubscribeVisitor");
            if (_visitors.ContainsKey(visitor.Id)) throw new EventException("Event - SubscribeVisitor");
            _visitors.Add(visitor.Id, visitor);
        }
        public void UnsubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("Event - UnsubscribeVisitor");
            if (!_visitors.ContainsKey(visitor.Id)) throw new EventException("Event - SubscribeVisitor");
            _visitors.Remove(visitor.Id);
        }

        public bool IsSame(Event secondEvent)
        {
			if (secondEvent == null)
				throw new EventException("Event - IsSame");
			if (Name != secondEvent.Name)
				return false;
			if (Location != secondEvent.Location)
				return false;
            if (MaxVisitors.Equals(secondEvent.Name))
                return false;
            if (Date.Equals(secondEvent.Date))
                return false;
			return true;

		}
    }
}
