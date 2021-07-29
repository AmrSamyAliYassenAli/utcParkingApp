using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UTCAPPCMS.MVC.Helpers.Api
{
    public class ResponseCode
    {
        public static int SuccessCode = 1;
        public static int NullObjectCode = 2;
        public static int ErrorCode = 0;
        public static int ExceptionCode = -1;

        public static string SuccessMessageEn = "Success Request";
        public static string SuccessMessageAr = "تم الطلب بنجاح";

        public static string SuccessgetDataMessageEn = "Success Request";
        public static string SuccessgetDataMessageAr = "تم الطلب بنجاح";

        public static string successsavedataen = "successfully saved data";
        public static string successsavedataar = "تم حفظ البيانات بنجاح";

        public static string errorsaveddataen = "error save data. try again";
        public static string errorsaveddataar = "خطا حفظ البيانات . حاول مرة اخرى";

        public static string accountnotactivatedEn = "your account is not activated yet do you want resend activation link via email again";
        public static string accountnotactivatedAr = "لم يتم تفعيل حسابك حتى الان هل تريد ارسال رابط التفعيل الى البريد اللاكترونى مره اخرى";

        public static string successregisterEn = "your account registered success and  activation link sent via email";
        public static string successregisterAr = "تم التسجيل بنجاح وتم ارسال رابط التفعيل على البريد اللاكترونى";

        public static string SuccessActivationMessageEn = "Account Activated successfully";
        public static string SuccessActivationMessageAr = "تم تفعيل الحساب بنجاح";

        public static string NullObjecMessageEn = "Error Data Try Again";
        public static string NullObjecMessageAr = "تم الطلب بنجاح";

        public static string ErrorMessageEn = "Exception Data .Try Again ";
        public static string ErrorMessageAr = "خطا عام .حاول مرة اخرى";

        public static string ErrorMessageDataNotFoundEn = "data not found";
        public static string ErrorMessageDataNotFoundAr = "البيانات غير موجوده";

        public static string dataEn = "wrong data";
        public static string dataAr = " خطا فى البيانات ";

        public static string UserExistBeforeEn = "user exist before";
        public static string UserExistBeforeAr = "المستخدم  موجود من قبل";

        public static string UserNotExistBeforeEn = "user not exist before";
        public static string UserNotExistBeforeAr = "المستخدم غير موجود من قبل";

        public static string PackageExcedUsereEn = "your package not has privilage to add  more upgrad your package";
        public static string PackageExcedUsereAr = "اشتراكك لا يسمح لك باضافه المزيد";

        public static string DonthaveprivilageAr = "خطا";
        public static string DonthaveprivilageEn = "Error try again";

        public static string UserDataNotValidAr = "خطا";
        public static string UserDataNotValidEn = "Error try again";

        public static string ActivationCodeNotCoorectAr = "رقم التفعيل غير صحيح";
        public static string ActivationCodeNotCoorectEn = "Activation code not correct";

        public static string updateDoneAr = "نجح التحديث";
        public static string updateDoneEn = "Success Update";

        public static string updateErrorAr = "فشل التحديث";
        public static string updateErrorEn = "Failed Update";

        public static string noUpdateAr = "لا يوجد تحديث";
        public static string noUpdateEn = "No Found Update";

        public static string addDoneAr = "نجح الاضافة";
        public static string addDoneEn = "Success add";

        public static string addErrorAr = "فشل الاضافة";
        public static string addErrorEn = "Failed add";

        public static string deleteDoneAr = "نجح المسح";
        public static string deleteDoneEn = "Success delete";

        public static string objectnotfoundAr = "لا يوجد سجل";
        public static string objectnotfoundEn = "item not found";

        public static string deleteErrorAr = "فشل المسح";
        public static string deleteErrorEn = "Failed delete";

        public static string notallowedtounclockAr = "غير مسموح لك بفتح البيانات لقد تعديت الحد الاقى";
        public static string notallowedtounclockEn = "not allowed to view contact yo excceed package count";

        public static string getDoneAr = " نجح الحصول على المعلومات";
        public static string getDoneEn = "Success Get Data";

        public static string getErrorAr = "فشل الحصول على المعلومات";
        public static string getErrorEn = "Failed Get Data";

        public static string changePasswordDoneAr = "تم تعديل كلمة المرور";
        public static string changePasswordDoneEn = "Success Change Password";

        public static string changePasswordErrorAr = "خطأ تغيير كلمة المرور";
        public static string changePasswordErrorEn = "Failed Change Password";

        public static string ForgetPasswordDoneAr = "تم تغيير كلمة المرور";
        public static string ForgetPasswordDoneEn = "Success Change Password";

        public static string ReSendActivationLinkAr = "تم ارسال الرابط على البريد اللاكترونى";
        public static string ReSendActivationLinkEn = "activation link sent to email";

        public static string ForgetPasswordErrorAr = "خطأ تغيير كلمة المرور";
        public static string ForgetPasswordErrorEn = "Failed Change Password";

        public static string ActivationDoneAr = "تم تفعيل الحساب";
        public static string ActivationDoneEn = "Success Activation Account";

        public static string ActivationErrorAr = "لم يتم تفعيل الحساب";
        public static string ActivationErrorEn = "Failed Activation Account";

        public static string validationErrorAr = "خطا";
        public static string validationErrorEn = "Validation Error";

        public static string dataexistbeforear = "البيانات موجودة من قبل";
        public static string dataexistbeforeen = "data is exist before";
    }
}