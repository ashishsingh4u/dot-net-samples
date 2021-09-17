using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simple.MVC.Controller;
using Simple.MVC.Model;

namespace Simple.MVC.View
{
    public partial class TopEmployeeForm : Form, ITopEmployeeView
    {
        private ITopEmployeeController _controller;
        private Employee _model;

        public string TopEmployeeName
        {
            get { return TopEmployeeNameLabel.Text; }
            set { TopEmployeeNameLabel.Text = value; }
        }

        public TopEmployeeForm(ITopEmployeeController controller, Employee model)
        {
            _controller = controller;
            _model = model;

            //Let the model know that this view is interested if the model change
            _model.OnPropertyChange += new Action(UpdateView);

            InitializeComponent();
        }

        // view
        public void UpdateView()
        {
            // to do with threading
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateView));
            }
            else
            {
                TopEmployeeName = _model.FullName;
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            _controller.GetTopEmployee();
        }

    }
}
