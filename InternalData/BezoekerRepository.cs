using Domein;

namespace InternalData
{
	public class BezoekerRepository : IBezoekerRepo
	{
		private Dictionary<int, Bezoeker> _bezoekers;

		public BezoekerRepository()
		{
			_bezoekers = new Dictionary<int, Bezoeker>();
		}
		
		public IEnumerable<Bezoeker> GetAllBezoekers()
		{
			return _bezoekers.Values;
		}

		public Bezoeker GetBezoekerById(int id)
		{
			return _bezoekers[id];
		}

		public void RegistreerBezoeker(Bezoeker bezoeker)
		{
			_bezoekers.Add(bezoeker.Id, bezoeker);
		}

		public void UpdateBezoeker(Bezoeker bezoeker)
		{
			_bezoekers[bezoeker.Id] = bezoeker;
		}

		public void VerwijderBezoeker(Bezoeker bezoeker)
		{
			_bezoekers.Remove(bezoeker.Id);
		}
	}
}
