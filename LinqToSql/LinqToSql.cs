using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace LinqToSql
{
    class LinqToSql
    {
        private const string connection = @"Data Source=manishsingh-pc\sqlexpress2008;Initial Catalog=Test;User ID=sa;Password=ashu12";

        public void TestDatabase()
        {
            StudentEntryContext context = new StudentEntryContext(connection);
            Table<StudentEntry> data = context.GetTable<StudentEntry>();
            //DataClasses1DataContext context = new DataClasses1DataContext(connection);
            //Table<Student> data = context.GetTable<Student>();
            data.InsertOnSubmit(new StudentEntry() { StudentName = "Ramesh", StudentRoll = 6, StudentCity = "Delhi" });
            data.Context.SubmitChanges();
        }
    }

    [Table(Name="Student")]
    public class StudentEntry
    {
        [Column(Name="Name",DbType="nvarchar(50)")]
        public string StudentName;
        [Column(Name = "Roll", DbType = "numeric(18,0) not null",IsPrimaryKey = true)]
        public int StudentRoll;
        [Column(Name = "City", DbType = "nvarchar(50)")]
        public string StudentCity;
    }

    public partial class StudentEntryContext : DataContext
    {
        public StudentEntryContext(string connection)
            : base(connection)
        {
        }
    }
}
