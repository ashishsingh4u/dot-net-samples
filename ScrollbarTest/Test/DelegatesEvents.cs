namespace Test
{
    #region Delegates

    public delegate void Mathematics(int firstNum, int secondNum);

    public delegate void MathematicsResult(int result);

    #endregion Delegates

    public class DelegatesEvents
    {
        #region Fields

        public Mathematics _mathematics;

        #endregion Fields

        #region Constructors

        public DelegatesEvents()
        {
            _mathematics = new Mathematics(Sum);
        }

        #endregion Constructors

        #region Events

        public event MathematicsResult _mathematicsResult;

        #endregion Events

        #region Methods

        private void Sum(int firstNum, int secondNum)
        {
            int total = firstNum + secondNum;
            if (_mathematicsResult != null)
                _mathematicsResult(total);
        }

        #endregion Methods
    }
}