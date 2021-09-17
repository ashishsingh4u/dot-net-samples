using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.MVC.View;
using Simple.MVC.Model;

namespace Simple.MVC.Controller
{
    public class TopEmployeeController : ITopEmployeeController
    {
        private ITopEmployeeView _view;
        private Employee _employee;

        private bool _monitoring;

        public ITopEmployeeView View
        {
            get
            {
                return _view;
            }
        }

        public TopEmployeeController()
        {
            _employee = new Employee();
            _view = new TopEmployeeForm(this, _employee);
        }

        public void GetTopEmployee()
        {
            if (!_monitoring)
            {
                _monitoring = true;
                _employee.MonitorChanges();
            }
        }
    }
}
