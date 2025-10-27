#region

using System;
using FluentAssertions;
using MarketplaceAppManagement.Tests;
using NSubstitute;
using NUnit.Framework;
using Tedom_Base.WebServices;
using Terrasoft.Core.Configuration;
using Terrasoft.Web.Common;
using Terrasoft.Web.Http.Abstractions;

#endregion

namespace TedomBase.Tests.WebServices{

	[TestFixture(Category = "WebServices")]
	[TestFixture(Category = "Unit")]
	public class DemoServiceTestFixture : BaseMarketplaceTestFixture{
		#region Fields: Private

		private HttpContext _context;
		private IHttpContextAccessor _httpContextAccessor;

		private DemoService _sut;

		#endregion

		#region Methods: Public

		[Test]
		[Description("Should return the current user's contact name from DemoGet.")]
		public void DemoGet_Returns_ContactName() {
			// Act
			string actualContactName = _sut.DemoGet();

			// Assert
			actualContactName.Should().Be(UserConnection.CurrentUser.Name,
				"Contact name should be the current user's name.");
		}


		[Test]
		[Description("Should throw NullReferenceException when HttpContextAccessor is null.")]
		public void DemoGet_WithNullHttpContextAccessor_ThrowsNullReferenceException() {
			// Arrange
			DemoService sut = new DemoService {
				HttpContextAccessor = null
			};

			// Act
			Action act = () => sut.DemoGet();

			// Assert
			act.Should().Throw<NullReferenceException>(
				"HttpContextAccessor is null and causes failure in BaseService");
		}

		[Test]
		[Description("Should throw InvalidOperationException when UserConnection is null.")]
		public void DemoGet_WithNullUserConnection_ThrowsInvalidOperationException() {
			// Arrange
			HttpContext context = Substitute.For<HttpContext>();
			IHttpContextAccessor httpContextAccessor = CustomSetupHttpContextAccessor(context);
			DemoService sut = new DemoService {
				HttpContextAccessor = httpContextAccessor
			};

			// Act
			Action act = () => sut.DemoGet();

			// Assert
			act.Should().Throw<InvalidOperationException>()
			   .WithMessage("UserConnection is not initialized");
		}

		[Test]
		[Description("Should throw InvalidOperationException when UserConnection.CurrentUser is null.")]
		public void DemoGet_WithNullCurrentUser_ThrowsInvalidOperationException() {
			// Arrange
			UserConnection.SetCurrentUser(null);
			HttpContext context = Substitute.For<HttpContext>();
			IHttpContextAccessor httpContextAccessor = CustomSetupHttpContextAccessor(context, UserConnection);
			DemoService sut = new DemoService {
				HttpContextAccessor = httpContextAccessor
			};
			
			// Act
			Action act = () => sut.DemoGet();

			// Assert
			act.Should().Throw<InvalidOperationException>()
			   .WithMessage("Current user is not authenticated");
		}
		
		[Test]
		[Description("Should return empty string when UserConnection.CurrentUser.Name is null.")]
		public void DemoGet_WithEmptyName_ReturnsEotyString() {
			// Arrange
			UserConnection.SetCurrentUser(new SysUserInfo(UserConnection){Name = null});
			HttpContext context = Substitute.For<HttpContext>();
			IHttpContextAccessor httpContextAccessor = CustomSetupHttpContextAccessor(context, UserConnection);
			DemoService sut = new DemoService {
				HttpContextAccessor = httpContextAccessor
			};
			
			// Act
			string actual = sut.DemoGet();

			// Assert
			actual.Should().Be(string.Empty, "Null user name should return empty string.");
		}
		
		
		

		[Test]
		[Description("DemoService should properly inherit from BaseService.")]
		public void DemoService_InheritsFromBaseService() {
			// Assert
			_sut.Should().BeAssignableTo<BaseService>(
				"DemoService should inherit from BaseService");
		}

		[SetUp]
		public void SetUpTest() {
			_context = Substitute.For<HttpContext>();
			_httpContextAccessor = CustomSetupHttpContextAccessor(_context, UserConnection);
			_sut = new DemoService {
				HttpContextAccessor = _httpContextAccessor
			};
		}

		#endregion
	}

}
