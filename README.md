# 🎭 Trendyol Seller Information With Playwright
This project was designed as a side project because Trendyol seller information could not be obtained via API. We developed it using the C# language and the playwright library. Our aim is to learn and have fun while developing the project. We hope you have fun contributing to it as well.

We also published the project as a Nuget package. You can start using it by installing the package from your Nuget package manager. [@Metehan Bas](https://github.com/MetehanBass) has prepared an Npm package in case it is useful for someone. We will update this when we publish the package. Please check back occasionally.

Looking for Playwright for  [.NET](https://playwright.dev/dotnet/docs/intro)

## 🎈 Installation

To set up, we first add our Nuget package to our project.
```
dotnet add package Ledbim.TrendyolStoreInfo 
```

To use the package, we inject our service into Startup.cs as follows.
```cs
services.AddTrendyolStoreService();
```

Once you have injected the service, you can use it in a structure such as a controller like this. We call the method that gets the Seller information based on the Seller Id parameter.
```cs
[ApiController]
[Route("api/[controller]")]
public class TrendyolStoreController : BaseController
{

    private readonly ITrendyolStoreService _service;
    public TrendyolStoreController(ITrendyolStoreService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string SellerId)
    { 
        var response = await _service.GetStore(SellerId);
        return Ok(response);
    }
}
```

## Let's Examine a Sample Trendyol Store 
You can see a screenshot of an example store information from Trendyol. Please review the json output we obtained by using the package

![image](https://user-images.githubusercontent.com/10067173/205509748-acc4bf6d-062e-4a3b-9f25-dccb77dc1264.png)


```json
{
  "SellerId": "132292",
  "StoreName": "FTMODA",
  "StoreImage": "https://cdn.dsmcdn.com/seller-store/uploads/132292/6c86d410-6b36-4ae1-9d78-39c341e1a4ea.jpeg",
  "Score": "9.4",
  "RegisterTime": "2 Yıl",
  "Location": "İzmir",
  "ProductCount": "200+",
  "DeliveryTimeToCargo": "1 Gün",
  "QuestionAnswerRate": "%33",
  "StoreUrl": "https://www.trendyol.com/magaza/x-m-132292"
}
```

## ✍️ Authors <a name = "authors"></a>

- [@Yasin ilkalp Arabacı](https://github.com/yasinilkalp) - (Nuget Package)
- [@Metehan Bas](https://github.com/MetehanBass) - (Npm Package) (Coming soon)
