using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Simple.MVC.Model
{
    public class Employee
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
                FirePropertyChange();
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
                FirePropertyChange();
            }
        }

        

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public Employee()
        {
        }

        internal void SetEmployeeTo(Employee employee)
        {
            _firstName = employee.FirstName;
            _lastName = employee.LastName;
            FirePropertyChange();
        }

        

        public event Action OnPropertyChange;

        private void FirePropertyChange()
        {
            var propChange = OnPropertyChange;
            if (propChange != null)
            {
                OnPropertyChange();
            }
        }

        internal void MonitorChanges()
        {
            var thread = new Thread(new ThreadStart(StartMonitoringChanges));
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        private void StartMonitoringChanges()
        {
            while (true)
            {
                SetEmployeeTo(
                    firstNames[rand.Next(firstNames.Length)],
                    lastNames[rand.Next(lastNames.Length)]);

                Thread.Sleep(DELAY);
            }
        }

        internal void SetEmployeeTo(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            FirePropertyChange();
        }

        private static readonly string[] firstNames = new[] { "Jane", "Jack", "Joe" };
        private static readonly string[] lastNames = new[] { "Doe", "Black", "White" };
        private static readonly Random rand = new Random();

        private const int DELAY = 1000;

    }
}
