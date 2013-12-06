using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;
using System.Threading.Tasks;
using FISCA.Data;
using System.Data;

namespace K12.Retake.Shinmin
{
    public class DepartmentNameCheck : IFieldValidator
    {
        private List<string> Names;
        private Task mTask;

        public DepartmentNameCheck()
        {
            Names = new List<string>();
            mTask = Task.Factory.StartNew
            (() =>
            {
                

                QueryHelper Helper = new QueryHelper();

                DataTable Table = Helper.Select("select name from dept");

                foreach (DataRow Row in Table.Rows)
                {
                    string Name = Row.Field<string>("name");

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