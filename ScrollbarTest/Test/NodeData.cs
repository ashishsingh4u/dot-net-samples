using ExpressTreeList.Nodes;

namespace Test
{
    public class NodeData:ICellValue
    {
        public NodeData(string data)
        {
            CellText = data;
        }

        public string CellText
        {
            get;
            set;
        }
    }
}