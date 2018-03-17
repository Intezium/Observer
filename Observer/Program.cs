using System;
using System.Collections.Generic;

namespace Observer
{
    interface IObserver
    {
        void Update(object args);
    }
    interface IObserved
    {
        void AddObserver(IObserver observer);

        void RemoveObserver(IObserver observer);

        void NotifyObservers();
    }

    class Observer : IObserver
    {
        public Action<int> onChange = null;

        public void Update(object args)
        {
            if (onChange != null)
                onChange((int)args);
        }
    }
    class Observed : IObserved
    {
        List<IObserver> observers = new List<IObserver>();

        public int number = 0;

        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (number != value)
                {
                    number = value;
                    NotifyObservers();
                }
            }
        }
        
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            for (int i = 0; i < observers.Count; i++)
                observers[i].Update(number);
        }
    }

    class Program
    {         
        static void Main(string[] args)
        {
            Observer observer = new Observer();
            Observed observed = new Observed();   

            observed.AddObserver(observer);

            observer.onChange += (value) => { Console.WriteLine("Значение изменилось на {0}", value); };

            observed.Number = 10;
            observed.Number = 20;
            observed.Number = 30;

            Console.ReadKey();
        }
    }
}
