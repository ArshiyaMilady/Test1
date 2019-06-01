using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace MvcFamilyMeetings.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "تعداد اعضای خانواده")]
        public int Numbers { get; set; }

        [Display(Name = "آدرس منزل")]
        public string Address_Home { get; set; }

        [Required]
        [Display(Name = "شماره همراه")]
        public string Mobile_No { get; set; }

        [Display(Name = "تلفن منزل")]
        public string Phone_Home { get; set; }

        [Display(Name = "آدرس محل کار")]
        public string Address_Work { get; set; }

        [Display(Name = "تلفن محل کار")]
        public string Phone_Work { get; set; }

        [Display(Name = "ایمیل")]
        public string E_Mail { get; set; }

        [Required]
        [Display(Name = "نام کاربری جهت ورود به سیستم")]
        public string LoginName { get; set; }

        [Required]
        public string Login_Password { get; set; }
        public string Login_Password_Hint { get; set; } // کمک برای یادآوری رمز ورود

        [Display(Name = "تاریخ شروع عضویت")]
        //[DataType(DataType.Date)]
        public string Date_Enter_Meetings { get; set; } // تاریخ عضو شدن

        //[DataType(DataType.Date)]
        public string Date_Exit_Meetings { get; set; }  // تاریخ خارج شدن

        public int Level { get; set; }    // سطح دسترسی
        public string Comment { get; set; }

        [Display(Name = "موجودی پس انداز")]
        public long Saving_Cash { get; set; }  // موجودی پس انداز

        [Display(Name = "مبلغ آخرین وام")]
        public long Last_Loan { get; set; }  // مبلغ آخرین وام گرفته شده

        [Display(Name = "باقیماده آخرین وام")]
        public long Last_Loan_Remainig { get; set; }  // مبلغ باقیمانده از آخرین وام گرفته شده

        [Display(Name = "تاریخ آخرین وام")]
        public string Last_Loan_Date { get; set; }  // تاریخ آخرین وام گرفته شده

        [Display(Name = "نوبت جلسه")]
        public int Meeting_Turn { get; set; }  

        public string M_C1 { get; set; }
        public string M_C2 { get; set; }
        public string M_C3 { get; set; }
        public string M_C4 { get; set; }
        public int M_I1 { get; set; }
        public int M_I2 { get; set; }
        public int M_I3 { get; set; }
        public int M_I4 { get; set; }
    }

    // جلسات
    public class Meeting
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="شناسه عضو برگزار کننده جلسه")]
        public int Member_Id { get; set; }  // شخصی که جلسه را گرفته

        [Display(Name = "نام عضو برگزار کننده جلسه")]
        public string Member_Name { get; set; }  // YYYY/MM/DD

        [Required]
        [Display(Name = "تاریخ برگزاری جلسه")]
        public string Login_Date { get; set; }  // YYYY/MM/DD

        [Display(Name = "شرح")]
        public string Comment { get; set; }

        public string Mt_C1 { get; set; }
        public string Mt_C2 { get; set; }

        //[Editable(allowEdit: true, AllowInitialValue = true)]
        public int Mt_I1 { get; set; }
        public int Mt_I2 { get; set; }


    }

    // تاریخچه ورودهای به برنامه  : ورودهای به برنامه را ثبت می کند
    public class History_Login
    {
        public long Id { get; set; }
        public int Member_Id { get; set; }
        public string Login_DateTime { get; set; }  // YYYY/MM/DD-HH:MM
        public string Comment { get; set; }

        public string HL_C1 { get; set; }
        public string HL_C2 { get; set; }
        public int HL_I1 { get; set; }
        public int HL_I2 { get; set; }
    }

    // تاریخچه فعالیتهای انجام شده
    public class History_Action
    {
        public long Id { get; set; }
        // ورود انجام شده
        public long Login_Id { get; set; }
        public string Description { get; set; }
        public string HA_C1 { get; set; }
        public string HA_C2 { get; set; }
        public int HA_I1 { get; set; }
        public int HA_I2 { get; set; }
    }

    // پس انداز
    public class Saving
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "شناسه کاربر وارد شده")]
        public int Member_Id { get; set; }

        [Required]
        [Display(Name = "تاریخ واریز")]
        public string Date { get; set; }  

        [Required]
        [Display(Name = "مقدار واریز به صندوق - ریال")]
        public long Payment { get; set; }   

        public string S_C1 { get; set; }
        public string S_C2 { get; set; }
        public int S_I1 { get; set; }
        public int S_I2 { get; set; }
    }

    // قسطها
    public class Loan
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "شناسه کاربر")]
        public int Member_Id { get; set; }

        [Required]
        [Display(Name = "مبلغ پرداختی")]
        public long Payment { get; set; }

        [Required]
        [Display(Name = "تاریخ پرداخت")]
        public string Date { get; set; }    

        [Required]
        [Display(Name = "باقیمانده پس از پرداخت این قسط")]
        public long Remaining { get; set; }


        // دو پارامتر زیر از جدول اعضا پر می شوند
        [Display(Name = "اصل وام")]
        public long Loan_Amount { get; set; }

        [Display(Name = "تاریخ دریافت وام")]
        public string Loan_Date { get; set; }


        [Display(Name = "توضیحات")]
        public string Comment { get; set; }

        public string I_C1 { get; set; }
        public string I_C2 { get; set; }
        public int I_I1 { get; set; }
        public int I_I2 { get; set; }
    }

    public class Photoes
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        // آیا مشاهده این تصویر محدود به کاربرانی خاص می شود؟
        public bool Limitted { get; set; }  
        public string Date_Time { get; set; }   // YYYY/MM/DD-HH:MM زمان گرفتن عکس
        public string Location { get; set; }    // مکان گرفتن عکس
        public string Description { get; set; } // توضیحات : مناسبتی یا غیره

        public string P_C1 { get; set; }
        public string P_C2 { get; set; }
        public int P_I1 { get; set; }
        public int P_I2 { get; set; }
    }

    // چه کاربرانی می توانند چه تصاویری را ببیند؟
    public class Photo_Member
    {
        public int Id { get; set; }
        public int Member_Id { get; set; }  // شناسه کابر
        public int Photo_Id { get; set; }   // شناسه عکس

        public string PM_C1 { get; set; }
        public string PM_C2 { get; set; }
        public int PM_I1 { get; set; }
        public int PM_I2 { get; set; }
    }


}
