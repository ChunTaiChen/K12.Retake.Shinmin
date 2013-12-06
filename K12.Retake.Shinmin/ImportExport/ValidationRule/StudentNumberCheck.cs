using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using FISCA.Data;

namespace K12.Retake.Shinmin
{
    public class StudentNumberCheck : IFieldValidator
    {
        private List<string> Names;
        private Task mTask;

        public StudentNumberCheck()
        {
            Names = new List<string>();
            mTask = Task.Factory.StartNew
            (() =>
            {
                QueryHelper Helper = new QueryHelper();

                DataTable Table = Helper.Select("select student_number from student where status in (1,2)");

                foreach (DataRow Row in Table.Rows)
                {
                    string Name = Row.Field<string>("student_number");

                    if (!Names.Contains(Name))
                        Names.Add(Name);
                }
            }
            );
        }

        #region IFieldValidator Members

        public string Correct(string Value)
        {
            return string.Empty;
        }

        public string ToString(string template)
        {
            return template;
        }

        public bool Validate(string Value)
        {
            mTask.Wait();
            return Names.Contains(Value);
        }

        #endregion
    }
}