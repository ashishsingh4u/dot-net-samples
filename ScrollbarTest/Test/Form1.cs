using ExpressTreeList.Columns;
using ExpressTreeList.Nodes;

namespace Test
{
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        #region Fields

        readonly ResourceManager _resourceManager;

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
            _resourceManager = new ResourceManager("Test.Culture.Test", GetType().Assembly);
            //myTreeList1.NodeHeight = 20;
            //myTreeList1.ColumnHeight = 20;
            //myTreeList1.ColumnFont = Font;
            myTreeList1.ColumnGap = 0;
            myTreeList1.AddColumn(new ExpressColumn("Hello1") {Text = "Hello1"});
            myTreeList1.AddColumn(new ExpressColumn("Hello2") { Text = "Hello2" });
            myTreeList1.AddColumn(new ExpressColumn("Hello3") { Text = "Hello3" });
            myTreeList1.AddColumn(new ExpressColumn("Hello4") { Text = "Hello4" });
            myTreeList1.AddColumn(new ExpressColumn("Hello5") { Text = "Hello5" });
            for (int i = 0; i < 4000000; i++)
            {
                myTreeList1.AddNode(new ExpressNode(new[]
                                                        {
                                                            new NodeData("Node"+ i + ":Col1"), new NodeData("Node"+ i + ":Col2"),
                                                            new NodeData("Node"+ i + ":Col3"), new NodeData("Node"+ i + ":Col4"),
                                                            new NodeData("Node"+ i + ":Col5")
                                                        }));
            }
            //myTreeList1.AddNodes(new[]
            //                         {
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node1:Col1"), new NodeData("Node1:Col2"),
            //                                                     new NodeData("Node1:Col3"), new NodeData("Node1:Col4"),
            //                                                     new NodeData("Node1:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node2:Col1"), new NodeData("Node2:Col2"),
            //                                                     new NodeData("Node2:Col3"), new NodeData("Node2:Col4"),
            //                                                     new NodeData("Node2:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node3:Col1"), new NodeData("Node3:Col2"),
            //                                                     new NodeData("Node3:Col3"), new NodeData("Node3:Col4"),
            //                                                     new NodeData("Node3:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node4:Col1"), new NodeData("Node4:Col2"),
            //                                                     new NodeData("Node4:Col3"), new NodeData("Node4:Col4"),
            //                                                     new NodeData("Node4:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node5:Col1"), new NodeData("Node5:Col2"),
            //                                                     new NodeData("Node5:Col3"), new NodeData("Node5:Col4"),
            //                                                     new NodeData("Node5:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node6:Col1"), new NodeData("Node6:Col2"),
            //                                                     new NodeData("Node6:Col3"), new NodeData("Node6:Col4"),
            //                                                     new NodeData("Node6:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node7:Col1"), new NodeData("Node7:Col2"),
            //                                                     new NodeData("Node7:Col3"), new NodeData("Node7:Col4"),
            //                                                     new NodeData("Node7:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node8:Col1"), new NodeData("Node8:Col2"),
            //                                                     new NodeData("Node8:Col3"), new NodeData("Node8:Col4"),
            //                                                     new NodeData("Node8:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node9:Col1"), new NodeData("Node9:Col2"),
            //                                                     new NodeData("Node9:Col3"), new NodeData("Node9:Col4"),
            //                                                     new NodeData("Node9:Col5")
            //                                                 }),
            //                             new ExpressNode(new[]
            //                                                 {
            //                                                     new NodeData("Node10:Col1"), new NodeData("Node10:Col2"),
            //                                                     new NodeData("Node10:Col3"), new NodeData("Node10:Col4"),
            //                                                     new NodeData("Node10:Col5")
            //                                                 }),
            //                         });
        }

        #endregion Constructors

        #region Methods

        private void Button1Click(object sender, System.EventArgs e)
        {
            var delegatesEvents = new DelegatesEvents();
            delegatesEvents._mathematicsResult += DelegatesEventsMathematicsResult;
            delegatesEvents._mathematicsResult +=DelegatesEventsMathematicsResult1;
            delegatesEvents._mathematics(20,30);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("hi-IN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("hi-IN");
            Text = _resourceManager.GetString("Hello");
        }

        static void DelegatesEventsMathematicsResult(int result)
        {
        }

        static void DelegatesEventsMathematicsResult1(int result)
        {
        }

        private void Form1Load(object sender, System.EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            Text = _resourceManager.GetString("Hello");
        }

        #endregion Methods
    }
}