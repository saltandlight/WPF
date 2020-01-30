using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace chapter8
{
    public class JpgValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //string filename = value.ToString();

            ////존재하지 않는 파일들은 거절한다.
            //if (!File.Exists(filename))
            //    return new ValidationResult(false, "Value is not a valid file.");
            ////.jpg로 끝나지 않는 파일들을 거절한다.
            //if (!filename.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase))
            //    return new ValidationResult(false, "Value is not a .jpg file");
            ////입력된 내용을 시험하고 결과를 반환한다.

            int result;
            
            return new ValidationResult(int.TryParse(value.ToString(), out result), null);
        }
    }
}
