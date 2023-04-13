using System.Collections;

namespace Warehouse.Models
{
    public class WaitingList : IReadOnlyList<Car>
    {
        private List<Car> _cars;

        public WaitingList(List<Car> cars)
        {
            _cars = cars;
        }

        public Car this[int index] => _cars[index];

        public int Count => _cars.Count;

        public IEnumerator<Car> GetEnumerator() => _cars.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _cars.GetEnumerator();
    }
}
