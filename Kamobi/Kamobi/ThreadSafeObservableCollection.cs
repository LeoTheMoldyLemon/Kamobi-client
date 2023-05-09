using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;

namespace Kamobi
{


    public class ThreadSafeObservableCollection<T> : ObservableCollection<T>
    {
        private readonly object _locker = new object();

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            lock (_locker)
            {
                base.OnCollectionChanged(e);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            lock (_locker)
            {
                base.OnPropertyChanged(e);
            }
        }

        protected override void ClearItems()
        {
            lock (_locker)
            {
                base.ClearItems();
            }
        }

        protected override void InsertItem(int index, T item)
        {
            lock (_locker)
            {
                base.InsertItem(index, item);
            }
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            lock (_locker)
            {
                base.MoveItem(oldIndex, newIndex);
            }
        }

        protected override void RemoveItem(int index)
        {
            lock (_locker)
            {
                base.RemoveItem(index);
            }
        }

        protected override void SetItem(int index, T item)
        {
            lock (_locker)
            {
                base.SetItem(index, item);
            }
        }
    }
}
