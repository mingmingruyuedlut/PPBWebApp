using PPB.Constant.Constants;
using PPB.PythonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Managers
{
    public class CommonManager
    {
        public static int GetMemberTypeLength(string memberType)
        {
            var startPos = memberType.IndexOf("[");
            var endPos = memberType.IndexOf("]");
            var typeLength = memberType.Substring(startPos + 1, endPos - startPos - 1);
            return Int32.Parse(typeLength);
        }

        public static int GetBoolArrayMemberTypeLength(string memberType)
        {
            var startPos = memberType.IndexOf("(");
            var endPos = memberType.IndexOf(")");
            var typeLength = memberType.Substring(startPos + 1, endPos - startPos - 1);
            return Int32.Parse(typeLength);
        }

        public static string GetMemberTypeWithoutLength(string memberType)
        {
            if (memberType.Contains("["))
            {
                var startPosition = memberType.IndexOf("[", StringComparison.OrdinalIgnoreCase);
                return memberType.Substring(0, startPosition);
            }
            return memberType;
        }

        public static string SupplementMemberValue(string memberValue, int memberLength)
        {
            if (memberValue.Length >= memberLength)
            {
                return memberValue;
            }

            for (var i = memberValue.Length; i < memberLength; i++)
            {
                memberValue += " "; //supplement with " "
            }
            return memberValue;
        }

        public static bool IsCommonDataType(string mtWithoutLength)
        {
            if (mtWithoutLength.Equals(DbMemberTypeConstant.MtInt) ||
                mtWithoutLength.Equals(DbMemberTypeConstant.MtBool) ||
                mtWithoutLength.Equals(DbMemberTypeConstant.MtDInt) ||
                mtWithoutLength.Equals(DbMemberTypeConstant.MtReal) ||
                mtWithoutLength.Equals(DbMemberTypeConstant.MtSInt) ||
                mtWithoutLength.Equals(DbMemberTypeConstant.MtString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetDownloadMemberName(string memberName, string baseTag)
        {
            var memberNameList = memberName.Split('.');
            if (memberNameList.Count() <= 0)
            {
                return string.Empty;
            }
            var memberNameFirstPart = memberNameList.First();
            var memberNameSecondPart = memberNameList.Last();
            if (memberNameFirstPart.Equals(memberNameSecondPart))
            {
                var finalMemberName = string.IsNullOrWhiteSpace(baseTag) ? memberNameFirstPart : baseTag + Form2Constant.Dot + memberNameFirstPart;
                return finalMemberName;
            }
            else
            {
                var finalMemberName = string.IsNullOrWhiteSpace(baseTag) ? memberName : baseTag + Form2Constant.Dot + memberName;
                return finalMemberName;
            }
        }

        public static string GetDownloadMemberValue(string memberValue)
        {
            if (memberValue.Equals(Form2Constant.True1))
            {
                return "1";
            }
            else if(memberValue.Equals(Form2Constant.False1))
            {
                return "0";
            }
            return memberValue;
        }

        public static void DownloadStringTypeMember(ref dynamic fileObj, string memberName, string memberValue)
        {
            try
            {
                var tagValueList = memberValue.ToList();
                var i = 0;
                foreach (var tagVal in tagValueList)
                {
                    var tagName = memberName + ".Data[" + i.ToString() + "]";
                    var tagValue = ((int)tagVal).ToString(); //tag value must be the char code number
                    LogixService.DownloadTag(ref fileObj, tagName, tagValue);
                    i++;
                }
            }
            catch (Exception ex)
            {
                //to-do
            }
        }

        public static void DownloadStringTypeLength(ref dynamic fileObj, string memberName, int memberLength)
        {
            try
            {
                var tagName = memberName + ".Len";
                var tagValue = memberLength.ToString();
                LogixService.DownloadTag(ref fileObj, tagName, tagValue);
            }
            catch (Exception ex)
            {
                //to-do
            }
        }
    }
}
