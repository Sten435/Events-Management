using BLEventService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEventService.Managers
{
	public class VisitorManager
	{

		private Dictionary<int, Visitor> _visitors = new();
		private int _idCount = 1;

		public VisitorManager()
		{
			_visitors.Add(_idCount, new Visitor(_idCount++, "John", DateTime.Parse("12/3/1975")));
			_visitors.Add(_idCount, new Visitor(_idCount++, "Jane", DateTime.Parse("18/7/1995")));
			_visitors.Add(_idCount, new Visitor(_idCount++, "David", DateTime.Parse("2/4/2001")));
			_visitors.Add(_idCount, new Visitor(_idCount++, "Cris", DateTime.Parse("12/9/1999")));
		}

		public IReadOnlyList<Visitor> GetAll()
		{
			return _visitors.Values.ToList().AsReadOnly();
		}

		public Visitor GetById(int id)
		{
			if (_visitors.ContainsKey(id))
				return _visitors[id];
			else
				throw new EventException("VisitorManager - GetById");
		}

		public void Unsubscribe(Visitor visitor)
		{
			if (visitor == null)
				throw new EventException("VisitorManager - Unsubscribe");
			if (!_visitors.ContainsKey(visitor.Id))
				throw new EventException("VisitorManager - Unsubscribe");
			_visitors.Remove(visitor.Id);
		}

		public Visitor RegisterVisitor(Visitor visitor)
		{
			visitor.SetId(_idCount++);
			return visitor;
		}

		public void SubscribeVisitor(Visitor visitor)
		{
			if (visitor == null)
				throw new EventException("VisitorManager - SubscribeVisitor");
			if (_visitors.ContainsKey(visitor.Id))
				throw new EventException("VisitorManager - SubscribeVisitor");
			_visitors.Add(visitor.Id, visitor);
		}

		public void UpdateVisitor(Visitor visitor)
		{
			if (visitor == null)
				throw new EventException("VisitorManager - UpdateVisitor");
			if (!_visitors.ContainsKey(visitor.Id))
				throw new EventException("VisitorManager - UpdateVisitor");
			if (_visitors[visitor.Id].IsSame(visitor))
				throw new EventException("VisitorManager - UpdateVisitor");
			_visitors[visitor.Id] = visitor;
		}
	}
}
