using Campus.DocumentValidator;

namespace K12.Retake.Shinmin
{
    /// <summary>
    /// 用來產生排課系統所需的自訂驗證規則
    /// </summary>
    public class FieldValidatorFactory : IFieldValidatorFactory
    {
        #region IFieldValidatorFactory 成員

        /// <summary>
        /// 根據typeName建立對應的FieldValidator
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="validatorDescription"></param>
        /// <returns></returns>
        public IFieldValidator CreateFieldValidator(string typeName, System.Xml.XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                case "STUDENTNUMBERCHECK":
                    return new StudentNumberCheck();
                case "DEPARTMENTNAMECHECK":
                    return new DepartmentNameCheck();
                case "TEACHERNAMECHECK":
                    return new TeacherNameCheck();
                default:
                    return null;
            }
        }

        #endregion
    }
}