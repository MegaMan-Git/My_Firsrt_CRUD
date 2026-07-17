<h1 align="center">My First CRUD + Identity</h1>
<p align="center">پروژه تمرینی ASP.NET Core MVC و EF Core</p>

<hr>

<h2>معرفی پروژه</h2>
<p>
    این پروژه یک برنامه تمرینی برای یادگیری ASP.NET Core MVC است که با استفاده از
    Entity Framework Core و ASP.NET Core Identity پیاده‌ سازی شده است.
    در این پروژه مدیریت کاربران، احراز هویت و عملیات CRUD برای بخش محصولات انجام می‌ شود.
</p>

<h2>قابلیت‌ها</h2>
<ul>
    <li>ثبت‌ نام، ورود و خروج کاربران</li>
    <li>بازیابی و تغییر رمز عبور</li>
    <li>ارسال ایمیل برای تأیید حساب و بازیابی رمز</li>
    <li>مدیریت محصولات، دسته‌ بندی ها و جزئیات</li>
    <li>استفاده از Identity با پیام‌ های خطای فارسی</li>
    <li>استفاده از SQL Server برای ذخیره اطلاعات</li>
</ul>

<h2>تکنولوژی‌های استفاده شده</h2>
<ul>
    <li>ASP.NET Core MVC</li>
    <li>Entity Framework Core</li>
    <li>ASP.NET Core Identity</li>
    <li>SQL Server</li>
    <li>Bootstrap</li>
    <li>jQuery</li>
</ul>

<h2>ساختار کلی پروژه</h2>
<ul>
    <li><code>My_Firsrt_CRUD</code> : بخش اصلی وب و کنترلرها</li>
    <li><code>Data_Context</code> : مدیریت دیتابیس و Migrationها</li>
    <li><code>ModelAss</code> : مدل‌ها، ویومدل‌ها و کلاس‌های مربوط به Identity</li>
</ul>

<h2>نحوه اجرا</h2>
<ol>
    <li>ابتدا Connection String را در فایل <code>appsettings.json</code> تنظیم کنید.</li>
    <li>سپس Migrationها را روی دیتابیس اعمال کنید.</li>
    <li>بعد پروژه را اجرا کنید.</li>
    <li>صفحه اصلی برنامه از مسیر <code>/Account/SignUpLogin</code> در دسترس است.</li>
</ol>

<h2>هدف پروژه</h2>
<p>
    هدف این پروژه تمرین مفاهیم اصلی توسعه وب با ASP.NET Core شامل معماری MVC،
    کار با دیتابیس، احراز هویت کاربران و مدیریت اطلاعات در یک پروژه واقعی و ساده است.
</p>

<hr>

<p align="center">Made with ❤️ for learning ASP.NET Core</p>
