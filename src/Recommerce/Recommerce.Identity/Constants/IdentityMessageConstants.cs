namespace Recommerce.Identity.Constants;

internal static class IdentityMessageConstants
{
    public const string WrongOtpCodeErrorMessage = "کد وارد شده معتبر نمی باشد.";
    public const string UserNotFoundErrorMessage = "کاربر یافت نشد.";
    public const string WrongOtpTokenErrorMessage = "به نظر میرسد کد وارد شده در سیستم نیست.";
    public const string UserIsInActiveErrorMessage = "دسترسی ورود توسط مدیریت بسته شده است.";
    public const string OtpWasNotSentErrorMessage = "مشکلی در ارسال کدفعالسازی توسط سرور رخ داده است. لطفا با پشتیبانی تماس بگیرید";
    public const string OtpTimeLimitErrorMessage = "شما به تازگی درخواست ارسال کدفعالسازی داده اید. لطفا لحظاتی بعد تلاش کنید";
    public const string OtpWasUsedErrorMessage = "این کدفعالسازی قبلا توسط شما استفاده شده است و در حال حاضر منقضی است.";
}