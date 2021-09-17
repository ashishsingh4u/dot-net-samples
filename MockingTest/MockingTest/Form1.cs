using System;
using System.Windows.Forms;
using NMock2;
using NUnit.Framework;

namespace MockingTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            TestGreetPositive();
        }
        [Test]
        public void TestGreetPositive()
        {
            //--i. Creating a mockery object.
            var personMock = new Mockery();
            //--ii. Mocking the IPerson interface using Mockery object.
            var p = personMock.NewMock<IPerson>();
            //--iii. Setting the mocked return value.
            const string returnValue = "Pradeep";
            //--iv. Mocking the result for method call IPerson.GetName() method.
            Expect.Once.On(p).Method("GetName").WithNoArguments().Will(Return.Value(returnValue));
            //--v. Creating the target class object.
            var h = new Hello(p);
            //--vi. Invoking the target test method.
            Assert.AreEqual("Hello Pradeep", h.Greet());
        }

    }
    public interface IPerson
    {
        string GetName();
    }
    class Hello
    {
        readonly IPerson _person;
        //--This method invokes the interface method.
        public Hello(IPerson person)
        {
            _person = person;
        }
        public String Greet()
        {
            return "Hello " + _person.GetName();
            //If you were to write a ut for this method (without nmocks), you should have created an
            //actual object of person and your inputs should be capable of navigating
            //thru' GetName() of Person class
        }
    }

    public class Person : IPerson
    {
        public string GetName()
        {
            return "Pramod";
        }
    }
}
