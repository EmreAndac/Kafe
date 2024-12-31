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
        //Ama�: Uygulama hatalar�n�, uyar�lar� veya di�er log bilgilerini kaydetmek i�in ILogger aray�z� kullan�l�r. Bu sayede hata ay�klama, izleme ve raporlama i�lemleri yap�labilir.

        public HomeController(ILogger<HomeController> logger)
        //ILogger nesnesi controller'�n bir par�as� haline gelir ve loglama i�lemleri yap�labilir. Bu, controller��n olu�turulmas� s�ras�nda gerekli olan ba��ml�l���n enjekte edilmesini sa�lar.(Dependency Injection) 
        {
            _logger = logger;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        //Ama�: Ana sayfa (index) sayfas�na kar��l�k gelir. View() metodu, Index isminde bir View (g�r�n�m) d�nd�recektir.
        {


            return View();
            //Kullan�c�ya, Index sayfas� g�rselini (HTML i�eri�ini) g�stermek amac�yla bu komut kullan�l�r.

        }


        public IActionResult Privacy() //Kullan�c�y� gizlilik politikas� sayfas�na y�nlendiren aksiyondur
        {
            return View(); //Kullan�c�ya gizlilik politikas�yla ilgili i�eri�i (HTML) g�sterir.
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // Bu, Error metodunun �nbelle�e al�nmamas� i�in kullan�lan bir filtre �zelli�idir. Bu, sayfa her zaman yeniden y�klenmesini sa�lar. Ama�: Hata sayfas� i�in �nbelleklemeyi engeller, yani her hata olu�tu�unda bu sayfa her zaman yeniden olu�turulacakt�r.Duration = 0 ve NoStore = true parametreleri, sayfan�n taray�c� veya ba�ka bir yer taraf�ndan �nbelle�e al�nmas�n� engeller.
        public IActionResult Error()  //Hata sayfas�n� kullan�c�ya g�stermek.
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //Hata sayfas� ile birlikte, hata hakk�nda bilgi sunar (�rne�in, istek ID�si ile).
        }
    }
}