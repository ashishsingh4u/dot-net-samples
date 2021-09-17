using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var numbers = new[] {2,2,2,2,3,3,4,4,4,4,1};
            var list = new List<int>(numbers);
            var dict = list.Aggregate(new Dictionary<int, int>(), (accumulate, item) =>
                                                                                       {
                                                                                           if (accumulate.ContainsKey(item))
                                                                                               accumulate[item]++;
                                                                                           else
                                                                                               accumulate[item] = 1;
                                                                                           return accumulate;
                                                                                       },
                                                       accumulate =>
                                                           {
                                                               var ret = new Dictionary<int, int>();
                                                               var max = accumulate.Max(o => o.Value);
                                                               foreach (var item in
                                                                   accumulate.Where(item => item.Value >= max))
                                                               {
                                                                   ret[item.Key] = item.Value;
                                                               }
                                                               return ret;
                                                           });
            var result = dict.Keys.ToArray();
            list.ForEach(o =>
            {
            });
            var group = list.GroupBy(o => o);
            foreach(var item in group)
            {
                var i = item.Count();
            }
        }
    }
}
