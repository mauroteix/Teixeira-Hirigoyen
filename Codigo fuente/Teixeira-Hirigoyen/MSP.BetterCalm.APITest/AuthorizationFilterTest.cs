using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSP.BetterCalm.API.Filters;
using MSP.BetterCalm.BusinessLogicInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.BetterCalm.APITest
{
    [TestClass]
    public class AuthorizationFilterTest
    {
        [TestMethod]
        public void TestAuthFilterWithoutHeader()
        {
            var logicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
            AuthorizationFilter authFilter = new AuthorizationFilter(logicMock.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(401, response.StatusCode);
        }

        


    }
}
