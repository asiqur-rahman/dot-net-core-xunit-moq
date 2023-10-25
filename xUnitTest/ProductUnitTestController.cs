using Moq;
using UnitTestMoq.Controllers;
using UnitTestMoq.Interfaces;
using UnitTestMoq.Models;

namespace xUnitTest
{
    public class ProductUnitTestController
    {
        private readonly Mock<IProductService> productService;
        public ProductUnitTestController()
        {
            productService = new Mock<IProductService>();
        }

        [Fact]
        public void GetProductList_ProductList()
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductList())
                .Returns(productList);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.ProductList();
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(GetProductsData().Count(), productResult.Count());
            Assert.Equal(GetProductsData().ToString(), productResult.ToString());
            Assert.True(productList.Equals(productResult));
        }

        [Fact]
        public void GetProductByID_Product()
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductById(2))
                .Returns(productList[1]);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.GetProductById(2);
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(productList[1].Id, productResult.Id);
            Assert.True(productList[1].Id == productResult.Id);
        }

        [Theory]
        [InlineData("IPhone")]
        public void CheckProductExistOrNotByProductName_Product(string productName)
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.GetProductList())
                .Returns(productList);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.ProductList();
            var expectedProductName = productResult.ToList()[0].Name;
            //assert
            Assert.Equal(productName, expectedProductName);
        }

        [Fact]
        public void AddProduct_Product()
        {
            //arrange
            var productList = GetProductsData();
            productService.Setup(x => x.AddProduct(productList[2]))
                .Returns(productList[2]);
            var productController = new ProductController(productService.Object);
            //act
            var productResult = productController.AddProduct(productList[2]);
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(productList[2].Id, productResult.Id);
            Assert.True(productList[2].Id == productResult.Id);
        }

        private List<Product> GetProductsData()
        {
            List<Product> productsData = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "IPhone",
                Description = "IPhone 12",
                Price = 55000,
                Stock = 10
            },
             new Product
            {
                Id = 2,
                Name = "Laptop",
                Description = "HP Pavilion",
                Price = 100000,
                Stock = 20
            },
             new Product
            {
                Id = 3,
                Name = "TV",
                Description = "Samsung Smart TV",
                Price = 35000,
                Stock = 30
            },
        };
            return productsData;
        }
    }
}