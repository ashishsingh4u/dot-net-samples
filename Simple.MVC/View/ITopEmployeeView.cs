using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.MVC.Model;

namespace Simple.MVC.View
{
    public interface ITopEmployeeView
    {
        string TopEmployeeName { get; set;  }
        void UpdateView();
    }
}
