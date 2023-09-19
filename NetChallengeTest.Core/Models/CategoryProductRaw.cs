using CsvHelper.Configuration.Attributes;

namespace NetChallengeTest.Core.Models
{
    public class CategoryProductRaw
    {
        [Name("PRODUCT_CODE")]
        public string ProductCode { get; set; }
        [Name("PRODUCT_NAME")]
        public string ProductName { get; set; }
        [Name("CATEGORY_CODE")]
        public int CategoryCode { get; set; }
        [Name("CATEGORY_NAME")]
        public string CategoryName { get; set; }
    }
}
