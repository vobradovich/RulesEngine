//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Xunit;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using RulesEngine.Core;
//using RulesEngine.Controllers;
//using RulesEngine.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace RulesEngine.Tests
//{
//    public class ModelControllerTests
//    {



//        [Fact]
//        public async Task ModelController_GetModels()
//        {
//            // Arrange
//            var db = new PolicyContext();
//            db.PolicyModels.Add(new PolicyModel { Id = 0, Name = "JObject" });
//            await db.SaveChangesAsync();

//            var controller = new PolicyModelController(db);

//            // Act
//            var result = await controller.GetModels();

//            // Assert
//            Assert.IsType<List<PolicyModel>>(result);
//        }

//        [Fact]
//        public async Task ModelController_GetModel()
//        {
//            // Arrange
//            var db = new PolicyContext();
//            db.PolicyModels.Add(new PolicyModel { Id = 0, Name = "JObject" });
//            await db.SaveChangesAsync();

//            var controller = new PolicyModelController(db);

//            // Act
//            var result = await controller.GetModel("JObject");

//            // Assert
//            var objectResult = Assert.IsType<OkObjectResult>(result);
//            var model = Assert.IsAssignableFrom<PolicyModel>(objectResult.Value);
//            Assert.Equal("JObject", model.Name);
//        }

//        [Fact]
//        public async Task ModelController_Post()
//        {
//            // Arrange
//            var db = new PolicyContext();
//            var controller = new PolicyModelController(db);

//            // Act
//            var result = await controller.Post(new PolicyModel { Id = 0, Name = "JObject2" });

//            // Assert
//            var objectResult = Assert.IsType<CreatedAtRouteResult>(result);
//            var model = Assert.IsAssignableFrom<PolicyModel>(objectResult.Value);
//            Assert.Equal("JObject2", model.Name);
//        }

//        [Fact]
//        public async Task ModelController_Delete()
//        {
//            // Arrange
//            var db = new PolicyContext();
//            db.PolicyModels.Add(new PolicyModel { Id = 0, Name = "JObject" });
//            await db.SaveChangesAsync();
//            var controller = new PolicyModelController(db);

//            // Act
//            var result = await controller.Delete(1);

//            // Assert
//            var objectResult = Assert.IsType<OkObjectResult>(result);
//            var model = Assert.IsAssignableFrom<PolicyModel>(objectResult.Value);
//            Assert.Equal(1, model.Id);
//        }
//    }
//}
