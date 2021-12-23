using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Selenium
{
    class Program
    {
        public static IWebDriver driver= new ChromeDriver();
        
        static void Main(string[] args)
        {
            driver.Navigate().GoToUrl("https://www.beymen.com/"); //Beymen'in sitesine gittik.
            driver.Manage().Window.Maximize(); // Ekranı Tam ekran boyutuna getirttik.
            HomePage();
            searchProduct();
            productDetail();
            driver.Quit();
            
         
        }
        public static void HomePage(){
            var account= driver.FindElement(By.XPath(".//*[@title='Hesabım']")).Text; 
            var favorite= driver.FindElement(By.XPath(".//*[@title='Favorilerim']")).Text;
            var basket= driver.FindElement(By.XPath(".//*[@title='Sepetim']")).Text;
            if(account=="Hesabım" && favorite=="Favorilerim" && basket=="Sepetim"){ // Hesabım,Favorilerim,Sepetim sekmelerinin kontrolü
                Console.WriteLine("Hesabım,Favorilerim ve Sepetim Alanları mevcut");
            } else Console.WriteLine("Alanlar Mevcut Değil");
           
        }
        public static void searchProduct() {
            Thread.Sleep(1000);
            driver.FindElement(By.ClassName("o-header__search--input")).SendKeys("Pantolon"+Keys.Enter); //Pantolon Kelimesini arattırdık.
            
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            
           driver.FindElement(By.XPath(".//*[@id='moreContentButton']")).Click(); //Daha Fazla Göster Butonuna Tıklama İşlemi Yaptık.
            Thread.Sleep(2000);
            driver.FindElement(By.XPath(".//*[@data-productid='781186']/div[1]/a")).Click(); //Rastgele Bir Ürünü Seçerek Detay Sayfasına Gittik.
        }
        public static void productDetail(){
            driver.FindElement(By.XPath(".//*[@class='m-variation']/span[2]")).Click(); //Beden Seçimi Yaptık.
            driver.FindElement(By.Id("addBasket")).Click(); // Sepete Ekleme İşlemini Yaptık.
            string getText = driver.FindElement(By.Id("priceNew")).Text; // Fiyatını Çektik 
            Thread.Sleep(2000);
            checkBasket(getText);
        }
         public static void checkBasket(string text){
             driver.FindElement(By.XPath(".//*[@class='container']/div/div[3]/div/a[3]")).Click(); //Sepetimize Gitme işlemini Gerçekleştirdik.
              string getText = driver.FindElement(By.ClassName("m-productPrice__salePrice")).Text; // Fiyatını Çektik 
              //Sepette ki fiyat ile ürünün sayfasında ki fiyat kontrolü
              if(getText==text){
                 Console.Write("Fiyatlar Eşit");
              } else Console.WriteLine("Fiyatlar Eşit Değil");

              driver.FindElement(By.XPath(".//*[@class='m-basket__quantity']/div/select/option[2]")).Click(); // Miktar Seçimini 2 yaptık.
              var getAmountText = driver.FindElement(By.Id("quantitySelect0")).Text; // Miktarın değerini aldık.
             //Miktar Kontrolü
              var amount=Convert.ToInt32(getAmountText);  
              if(amount==2){
                  Console.WriteLine("2 Adet Seçildi");
              }
              else Console.WriteLine("Adet Seçiminde Hata Var");
              driver.FindElement(By.Id("removeCartItemBtn0")).Click(); // Sepetteki ürünü kaldırdık.     
              var element=driver.FindElement(By.ClassName("m-empty__message")); //Boş sayfaya ait element çektik

              //Sepetin boş olup olmadığının kontrolünü gerçekleştirdik.
              if(element.Enabled==true)  
              Console.WriteLine("Sepet Boş");
              else Console.WriteLine("Silme İşlemi Başarısız");
            } 

    }
}
