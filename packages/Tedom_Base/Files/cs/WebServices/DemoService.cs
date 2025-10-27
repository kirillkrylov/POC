using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.SessionState;
using Terrasoft.Web.Common;

namespace Tedom_Base.WebServices{

	/// <summary>
	/// Provides demonstration web service methods for user information retrieval.
	/// </summary>
	/// <remarks>
	/// This service requires an authenticated user session and ASP.NET compatibility mode.
	/// </remarks>
	[ServiceContract]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class DemoService : BaseService, IReadOnlySessionState{

		/// <summary>
		/// Retrieves the current authenticated user's display name.
		/// </summary>
		/// <returns>The display name of the currently authenticated user.</returns>
		/// <exception cref="InvalidOperationException">Thrown when UserConnection is not properly initialized.</exception>
		[OperationContract]
		[WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare
			, ResponseFormat = WebMessageFormat.Json)]
		public string DemoGet() {
			
			if (UserConnection == null) {
				throw new InvalidOperationException("UserConnection is not initialized");
			}

			if (UserConnection.CurrentUser == null) {
				throw new InvalidOperationException("Current user is not authenticated");
			}

			return UserConnection.CurrentUser.Name;
		}
	}
}
