using System.Collections.ObjectModel;

namespace Robot
{
    public class ObservablePairCollection<TKey, TValue> : ObservableCollection<Pair<TKey, TValue>>
    {
        public void Add(TKey key, TValue value)
        {
            Add(new Pair<TKey, TValue>(key, value));
        }
    }
}
