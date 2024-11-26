using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class ProductInfo
    { 
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
    }

        public class Ingredient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ProductId { get; set; }
            public ProductInfo Product { get; set; }
        }

        public class UserAllergy
        {
            public int Id { get; set; }
            public string AllergyName { get; set; }
            public string UserId { get; set; }
            public User User { get; set; }
        }
    public class User
    {
        public string Id { get; set; } // معرّف المستخدم، يمكن أن يكون مثلاً GUID أو ID
        public string Name { get; set; } // اسم المستخدم
        public string Email { get; set; } // بريد المستخدم الإلكتروني

        // العلاقة مع الحساسية (UserAllergy)
        public ICollection<UserAllergy> Allergies { get; set; }
    }
    public class User2
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
