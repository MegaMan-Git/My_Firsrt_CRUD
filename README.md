<h1 align="center">My First CRUD</h1>

<p align="center">
  <b>ASP.NET Core MVC CRUD Practice Project</b><br/>
  تمرین عملی معماری MVC و Entity Framework Core
</p>

<hr/>

<h2>📌 Overview | معرفی پروژه</h2>

<p>
This is a beginner-friendly ASP.NET Core MVC project built to practice CRUD operations 
using Entity Framework Core and SQL Server.
</p>

<p dir="rtl">
این پروژه یک تمرین عملی برای پیاده‌سازی عملیات CRUD با استفاده از 
ASP.NET Core MVC و Entity Framework Core و پایگاه داده SQL Server است.
</p>

<hr/>

<h2>🏗 Architecture | معماری پروژه</h2>

<h3>🔹 Web Layer (MVC)</h3>
<ul>
  <li><code>My_Firsrt_CRUD/Program.cs</code></li>
  <li><code>Controllers/</code></li>
  <li><code>Views/</code></li>
  <li><code>wwwroot/</code></li>
</ul>

<h3>🔹 Data Layer</h3>
<ul>
  <li><code>Data_Context/Data/MyDbContext.cs</code></li>
  <li><code>Data_Context/FluentConfing/</code></li>
  <li><code>Data_Context/Migrations/</code></li>
</ul>

<h3>🔹 Models Layer</h3>
<ul>
  <li><code>ModelAss/Models/</code></li>
  <li><code>ModelAss/ViewList/</code></li>
  <li><code>ModelAss/ColorOptions_ForDetail/ColorEnum.cs</code></li>
</ul>

<p dir="rtl">
پروژه به صورت چند لایه طراحی شده است:
لایه وب (MVC)، لایه دیتا (EF Core)، و لایه مدل‌ها.
</p>

<hr/>

<h2>⚙ Startup Configuration | تنظیمات شروع برنامه</h2>

<p>
The application registers MVC and configures Entity Framework Core with SQL Server.
</p>

<pre><code>
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext&lt;MyDbContext&gt;(options =>
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:DefaultConnection"]));
</code></pre>

<p><b>Middleware Pipeline:</b></p>

<ul>
  <li>UseExceptionHandler("/Product/Error") (Development)</li>
  <li>UseHsts()</li>
  <li>UseHttpsRedirection()</li>
  <li>UseRouting()</li>
  <li>UseAuthorization()</li>
  <li>MapStaticAssets()</li>
</ul>

<hr/>

<h2>🛣 Routing Configuration | مسیردهی</h2>

<p>Default Route:</p>

<pre><code>
{ controller = "Account", action = "SignUpLogin" }
</code></pre>

<p dir="rtl">
مسیر پیش‌فرض برنامه به کنترلر Account و اکشن SignUpLogin اشاره می‌کند.
</p>

<hr/>

<h2>🚀 Features | قابلیت‌ها</h2>

<ul>
  <li>ASP.NET Core MVC architecture</li>
  <li>Entity Framework Core integration</li>
  <li>SQL Server connection</li>
  <li>Layered architecture</li>
  <li>Exception handling middleware</li>
</ul>

<p dir="rtl">
✔ معماری چندلایه  
✔ اتصال به SQL Server  
✔ مدیریت خطا  
✔ ساختار تمیز MVC  
</p>

<hr/>

<h2>🛠 Technologies Used | تکنولوژی‌ها</h2>

<ul>
  <li>.NET (ASP.NET Core MVC)</li>
  <li>Entity Framework Core</li>
  <li>SQL Server</li>
  <li>Bootstrap</li>
  <li>jQuery</li>
</ul>

<hr/>

<h2>▶ Getting Started | نحوه اجرا</h2>

<ol>
  <li>Clone the repository</li>
  <li>Configure connection string in <code>appsettings.json</code></li>
  <li>Run EF Core migrations (if needed)</li>
  <li>Run the project</li>
</ol>

<p dir="rtl">
۱. پروژه را Clone کنید  
۲. ConnectionString را در appsettings تنظیم کنید  
۳. در صورت نیاز Migration اجرا کنید  
۴. پروژه را اجرا کنید  
</p>

<hr/>

<h2>📚 Educational Purpose | هدف آموزشی</h2>

<p>
This project is created for learning and practicing ASP.NET Core MVC and EF Core.
</p>

<p dir="rtl">
این پروژه صرفاً با هدف آموزش و تمرین توسعه داده شده است.
</p>

<hr/>

<p align="center">
  Made with ❤️ for learning ASP.NET Core
</p>
