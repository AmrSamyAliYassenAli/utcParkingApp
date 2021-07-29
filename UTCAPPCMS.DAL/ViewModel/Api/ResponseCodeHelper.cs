using System;
using System.Collections.Generic;
using System.Text;

namespace UTCAPPCMS.DAL.ViewModel.Api
{
   public class ResponseCodeHelper
    {
        public static int SuccessCode = 1;
        public static int NullObjectCode = 2;
        public static int ErrorCode = 0;
        public static int ExceptionCode = -1;

        public static string SuccessMessageEn = "Success Request";
        public static string SuccessMessageAr = "تم الطلب بنجاح";
        public static string SuccessgetDataMessageEn = "Success Request";
        public static string SuccessgetDataMessageAr = "تم الطلب بنجاح";

        public static string ExistMessageEn = "Already Exist";
        public static string ExistMessageAr = "موجود مسبقا";

        public static string NotExistMessageEn = "Not Exist";
        public static string NotExistMessageAr = "غير موجود";

        public static string NoSubMessageEn = "No Subscription";
        public static string NoSubMessageAr = "لا يوجد اشتراك";

        public static string SubMessageEn = "Active Subscription Available";
        public static string SubMessageAr = "يوجد اشتراك فعال";

        public static string ExceedMessageEn = "Vehicles Count Exceed";
        public static string ExceedMessageAr = "تجاوزت عدد المركبات";

        public static string ErrorMessageEn = "General error. try again";
        public static string ErrorMessageAr = "خطا عام .حاول مرة اخرى";
    }
}
