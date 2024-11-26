//using CRUD_Operations.Data;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using RepositoryPatternWithUOW.Core.Models;
//public class ProductController : ControllerBase
//{
//    private readonly ApplicationDbContext _context;
//    private readonly HttpClient _httpClient;

//    public ProductController(ApplicationDbContext context, HttpClient httpClient)
//    {
//        _context = context;
//        _httpClient = httpClient;
//    }

//    [HttpGet("api/product/{barcode}")]
//    public async Task<IActionResult> CheckAllergyByBarcode(string barcode)
//    {
//        // البحث عن المنتج في قاعدة البيانات باستخدام الباركود
//        var product = await _context.ProductInfos
//            .Include(p => p.Ingredients)
//            .FirstOrDefaultAsync(p => p.Barcode == barcode);

//        // إذا لم يتم العثور على المنتج في قاعدة البيانات، استخدم Open Food Facts API
//        if (product == null)
//        {
//            product = await GetProductFromOpenFoodFacts(barcode);
//            if (product == null)
//            {
//                return NotFound("Product not found.");
//            }
//        }

//        // احصل على حساسيات المستخدم
//        var currentUserId = GetCurrentUserId(); // افترض أنك ستحصل على معرف المستخدم الحالي
//        var userAllergies = await _context.UserAllergies
//            .Where(ua => ua.UserId == currentUserId)
            
//            .ToListAsync();

//        // تحقق مما إذا كانت المكونات تحتوي على الحساسيات
//        var allergicIngredients = product.Ingredients
        
//            .Select(ingredient => ingredient.Name);

//        if (allergicIngredients.Any())
//        {
//            return Ok($"Warning: Contains {string.Join(", ", allergicIngredients)}");
//        }

//        return Ok("No allergies detected.");
//    }

//    private async Task<ProductDetails> GetProductFromOpenFoodFacts(string barcode)
//    {
//        // استدعاء API Open Food Facts للحصول على تفاصيل المنتج باستخدام الباركود
//        string apiUrl = $"https://world.openfoodfacts.org/api/v0/product/{barcode}.json";
//        var response = await _httpClient.GetStringAsync(apiUrl);

//        // إذا كانت الاستجابة فارغة أو غير صالحة
//        if (string.IsNullOrEmpty(response))
//        {
//            return null;
//        }

//        // تحويل الاستجابة إلى كائن
//        var productResponse = JsonConvert.DeserializeObject<ProductResponse>(response);

//        // التحقق إذا كان المنتج موجودًا في الاستجابة
//        if (productResponse?.ProductInfos == null)
//        {
//            return null;
//        }

//        // إرجاع بيانات المنتج المستخرجة من Open Food Facts
//        return new ProductDetails
//        {
//            Name = productResponse.ProductInfos.Name,
//            Ingredients = productResponse.ProductInfos.Ingredients,
           
//        };
//    }

//    // هذه دالة للحصول على معرف المستخدم الحالي (تحتاج إلى تعديلها لتناسب تطبيقك)
//    private string GetCurrentUserId()
//    {
//        // من المفترض أن يتم جلب معرف المستخدم الحالي من الجلسة أو الـ JWT أو أي آلية أخرى
//        return "some-user-id";
//    }
//}

