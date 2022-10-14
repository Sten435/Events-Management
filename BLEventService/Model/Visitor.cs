namespace BLEventService.Model
{
	public class Visitor
	{
		public Visitor(string name, DateTime birthDay)
		{
			SetName(name);
			BirthDay = birthDay;
		}

		public Visitor(int id, string name, DateTime birthDay)
		{
			SetId(id);
			SetName(name);
			BirthDay = birthDay;
		}

		public Visitor()
		{
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime BirthDay { get; set; }
		public void SetId(int id)
		{
			if (id <= 0)
				throw new EventException("Visitor - SetId");
			Id = id;
		}
		public void SetName(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new EventException("Visitor - SetName");
			Name = name;
		}

		public bool IsSame(Visitor secondVisitor)
		{
			if (secondVisitor == null)
				throw new EventException("Visitor - IsSame");
			if (Id != secondVisitor.Id)
				return false;
			if (BirthDay != secondVisitor.BirthDay)
				return false;
			if (!Name.Equals(secondVisitor.Name))
				return false;
			return true;
		}

		public override bool Equals(object? obj)
		{
			return obj is Visitor visitor &&
				   Id == visitor.Id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id);
		}
	}
}