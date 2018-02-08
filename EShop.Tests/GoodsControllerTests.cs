using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using EShop.Areas.Login.Controllers;
using EShop.Areas.Login.Models;
using EShop.Controllers;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;
using EShop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcContrib.TestHelper;

namespace EShop.Tests
{
    [TestClass]
    public class GoodsControllerTests
    {
        [TestInitialize]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer("id",
                    new SessionStateItemCollection(),
                    new HttpStaticObjectsCollection(),
                    10,
                    true,
                    HttpCookieMode.AutoDetect,
                    SessionStateMode.InProc,
                    false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                    .GetConstructor(
                        BindingFlags.NonPublic | BindingFlags.Instance,
                        null,
                        CallingConventions.Standard,
                        new[] { typeof(HttpSessionStateContainer) },
                        null)
                    .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        [TestMethod]
        public void CanFilterGoods()
        {
            // Arrange
            var mock = new Mock<IEshopRepository>();
            mock.Setup(m=>m.Goods).Returns(new List<Goods>()
            {
                new Goods()
                {
                    Name = "Meizu M2 Note",
                    Description = "Mobile Phone from Meizu",
                    Category = new Category()
                    {
                        Name = "Mobile Phones"
                    },
                    AvailableCount = 5,
                    Price = 3000
                },
                new Goods()
                {
                    Name = "Hp DV6-6158er",
                    Category = new Category()
                    {
                        Name = "NoteBook"
                    },
                    Description = "Awesome notebook, developed by HP",
                    AvailableCount = 0,
                    Price = 10000
                }
            });
            GoodsController controller = new GoodsController(mock.Object);

            GoodsModel model = new GoodsModel()
            {
                PriceLow = 2000,
                PriceHight = 3500
            };

            int expected = 1;
            
            // Action
            var result = controller.Filters(model).Count;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReverseToModel_Test()
        {
            // Arrange
            var mock = new Mock<IEshopRepository>();
            GoodsController controller = new GoodsController(mock.Object);

            GoodsModel model = new GoodsModel() {Sort = new SortModel(){RevboolId = false}};

            var expected = true;

            // Action
            controller.ReverseToModel(model, "Id");
            var result = model.Sort.RevboolId;

            // Assert
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void ItemController_GetItemWithSetId()
        {
            // Arrange
            var mock = new Mock<IEshopRepository>();
            mock.Setup(m => m.Goods)
                .Returns(new List<Goods>() { new Goods() { Id = 13 }});

            var controller = new ItemController(mock.Object);

            // Act
            ViewResult result = controller.Index(13) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GoodsController_ListByName()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            // Arrange
            var mock = new Mock<IEshopRepository>();
            mock.Setup(m => m.Goods)
                .Returns(new List<Goods>
                {
                    new Goods() {Name = "ABCD", Category = new Category()},
                    new Goods() {Name = "XYZ", Category = new Category()},
                    new Goods() {Name = "KLMN", Category = new Category()}
                });
            var expexted = new GoodsModel()
            {
                Goods = new List<Goods>
                {
                    new Goods() {Name = "ABCD", Category = new Category()},
                    new Goods() {Name = "KLMN", Category = new Category()},
                    new Goods() {Name = "XYZ", Category = new Category()}
                },
                Sort = new SortModel() { RevboolName = true}
            };

            var controller = new GoodsController(mock.Object);
            builder.InitializeController(controller);

            // Action
            var result = controller.ListByName(expexted) as ViewResult;
            
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ComputeTotalValue_Test()
        {
            // Arrange
            Cart cart = new Cart(new List<CartLine>()
            {
                new CartLine(){Goods = new Goods(){Category = new Category(), Name = "Buubbo", Price = 5}, Quantity = 20},
                new CartLine(){Goods = new Goods(){Category = new Category(), Name = "Duuddo", Price = 10}, Quantity = 5},
            });
            int expected = 150;

            // Action 
            var result = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void Register_Validation_IncorrectData_Test1()
        {
            // Arrange
            RegisterController register = new RegisterController();
            RegisterModel model = new RegisterModel(){Email = "Abbcc", Name = "1234", Password = "", PasswordConfirm = ""};

            // Action
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            var result = Validator.TryValidateObject(model, ctx, validationResults, true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Register_Validation_IncorrectData_Test2()
        {
            // Arrange
            RegisterController register = new RegisterController();
            RegisterModel model = new RegisterModel() { Email = "Abbcc@gsdf.com", Name = "Bromat", Password = "1234567", PasswordConfirm = "123" };
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            // Action
            var result = Validator.TryValidateObject(model, ctx, validationResults, true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Register_Validation_CorrectData_Test1()
        {
            // Arrange
            RegisterController register = new RegisterController();
            RegisterModel model = new RegisterModel() { Email = "Abbcc@gsdf.com", Name = "Bromat", Password = "1234567", PasswordConfirm = "1234567" };
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);

            // Action
            var result = Validator.TryValidateObject(model, ctx, validationResults, true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Search_Goods_CorrectData_Test()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            // Arrange
            var mock = new Mock<IEshopRepository>();
            mock.Setup(m => m.Goods)
                .Returns(new List<Goods>
                {
                    new Goods(){Name = "Just a name"},
                    new Goods(){Name = "just Anozer name"},
                    new Goods(){Name = "Wrong name"}
                });

            int expected = 2;
            var controller = new GoodsController(mock.Object);
            builder.InitializeController(controller);
            var model = new GoodsModel(){Name = "just"};


            // Action
            ViewResult view = controller.Search(model) as ViewResult;
            int result = ((GoodsModel) view.Model).Goods.Count();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Search_Goods_IncorrectData_Test()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            // Arrange
            var mock = new Mock<IEshopRepository>();
            mock.Setup(m => m.Goods)
                .Returns(new List<Goods>
                {
                    new Goods(){Name = "Just a name"},
                    new Goods(){Name = "just Anozer name"},
                    new Goods(){Name = "Wrong name"}
                });

            int expected = 2;
            var controller = new GoodsController(mock.Object);
            builder.InitializeController(controller);
            var model = new GoodsModel() { Name = "Incorrect search querry" };


            // Action
            ViewResult view = controller.Search(model) as ViewResult;
            int result = ((GoodsModel)view.Model).Goods.Count();

            // Assert
            Assert.AreNotEqual(expected, result);
        }
    }
}
