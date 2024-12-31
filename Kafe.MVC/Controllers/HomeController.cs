using Kafe.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace Kafe.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //Amaç: Uygulama hatalarýný, uyarýlarý veya diðer log bilgilerini kaydetmek için ILogger arayüzü kullanýlýr. Bu sayede hata ayýklama, izleme ve raporlama iþlemleri yapýlabilir.

        public HomeController(ILogger<HomeController> logger)
        //ILogger nesnesi controller'ýn bir parçasý haline gelir ve loglama iþlemleri yapýlabilir. Bu, controller’ýn oluþturulmasý sýrasýnda gerekli olan baðýmlýlýðýn enjekte edilmesini saðlar.(Dependency Injection) 
        {
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        //Amaç: Ana sayfa (index) sayfasýna karþýlýk gelir. View() metodu, Index isminde bir View (görünüm) döndürecektir.
        {


            return View();
            //Kullanýcýya, Index sayfasý görselini (HTML içeriðini) göstermek amacýyla bu komut kullanýlýr.

        }


        public IActionResult Privacy() //Kullanýcýyý gizlilik politikasý sayfasýna yönlendiren aksiyondur
        {
            return View(); //Kullanýcýya gizlilik politikasýyla ilgili içeriði (HTML) gösterir.
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // Bu, Error metodunun önbelleðe alýnmamasý için kullanýlan bir filtre özelliðidir. Bu, sayfa her zaman yeniden yüklenmesini saðlar. Amaç: Hata sayfasý için önbelleklemeyi engeller, yani her hata oluþtuðunda bu sayfa her zaman yeniden oluþturulacaktýr.Duration = 0 ve NoStore = true parametreleri, sayfanýn tarayýcý veya baþka bir yer tarafýndan önbelleðe alýnmasýný engeller.
        public IActionResult Error()  //Hata sayfasýný kullanýcýya göstermek.
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //Hata sayfasý ile birlikte, hata hakkýnda bilgi sunar (örneðin, istek ID’si ile).
        }
    }
}