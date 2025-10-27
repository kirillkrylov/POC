using NSubstitute;
using System;
using System.Collections.Generic;
using NSubstitute.Core;
using Terrasoft.Common;
using Terrasoft.Configuration.Tests;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Factories;
using Terrasoft.UnitTest;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using NUnit.Framework;
using Terrasoft.Core;
using Terrasoft.Core.Configuration;
using Terrasoft.Web.Http.Abstractions;

namespace MarketplaceAppManagement.Tests
{


	#region Class: BaseMarketplaceTestFixture
	[TestFixture(Category = "UnitTests")]
	public class BaseMarketplaceTestFixture : BaseConfigurationTestFixture
	{
		
		public static bool ValidateInsertColumns(Insert q, Dictionary<string, object> expected, string schemaName){
			bool isError = q.Source.SchemaName == schemaName;
			int i = 0;
			while (!isError && i<= expected.Count) {
				foreach (KeyValuePair<string, object> kvp in expected) {
					ModifyQueryColumnValue column = q.ColumnValues.FirstOrDefault(cv =>
						cv.SourceColumnAlias == kvp.Key);
					object columnValue = column.ValueExpression.Parameter.Value;
					if (!columnValue.Equals(kvp.Value)) {
						isError = true;
					}
					i++;
				}
			}
			return isError;
		}
		
		public static bool ValidateUpdateColumns(Update q, Dictionary<string, object> expected, string schemaName){
			bool isError = q.Source.SchemaName == schemaName;
			int i = 0;
			while (!isError && i<= expected.Count) {
				foreach (KeyValuePair<string, object> kvp in expected) {
					ModifyQueryColumnValue column = q.ColumnValues.FirstOrDefault(cv =>
						cv.SourceColumnAlias == kvp.Key);
					object columnValue = column.ValueExpression.Parameter.Value;
					if (!columnValue.Equals(kvp.Value)) {
						isError = true;
					}
					i++;
				}
			}
			return isError;
		}
		

		#region Enum: Public

		/// <summary>
		/// Available DataValueType
		/// </summary>
		/// <remarks>
		/// See <see cref="Terrasoft.Core.DataValueTypeManager"/>
		/// </remarks>
		public enum DataValueType
		{

			ShortText,
			MediumText,
			LongText,
			MaxSizeText,
			Text,
			PhoneText,
			WebText,
			EmailText,
			RichText,
			SecureText,
			HashText,
			DBObjectName,
			MetaDataTextDataValueType,
			Integer,
			Float,
			Float1,
			Float2,
			Float3,
			Float4,
			Float8,
			Money,
			DateTime,
			Date,
			Time,
			Boolean,
			Image,
			ImageLookup,
			File,
			Color,
			Guid,
			Binary,
			Lookup,
			MultiLookup,
			ValueList,
			Object,
			LocalizableStringDataValueType,
			LocalizableImageDataValueType,
			UnitDataValueType,
			EntityDataValueType,
			EntityColumnMappingCollectionDataValueType,
			EntityCollectionDataValueType,
			LocalizableParameterValuesListDataValueType,
			ObjectList,
			CompositeObjectList,
			FileLocator

		}

		#endregion

		#region Methods: Protected

		/// <summary>
		/// Creates custom httpContext
		/// </summary>
		/// <param name="httpContext">Substitute.For&lt;HttpContext&gt;();</param>
		/// <param name="userConnection">TestUserConnection</param>
		/// <example>
		/// <code>
		/// HttpContext context = Substitute.For&lt;HttpContext&gt;();
		/// IHttpContextAccessor httpContextAccessor = CustomSetupHttpContextAccessor(context, UserConnection);
		/// ServiceUnderTest sut = new ServiceUnderTest() {
		///		HttpContextAccessor = httpContextAccessor
		/// };
		/// </code>
		/// </example>
		/// <returns></returns>
		protected static IHttpContextAccessor CustomSetupHttpContextAccessor(HttpContext httpContext,
			UserConnection userConnection = null) {
			if (userConnection != null) {
				httpContext.Session["UserConnection"] = userConnection;
			}
			if (userConnection?.AppConnection != null) {
				httpContext.Application["AppConnection"] = userConnection.AppConnection;
			}
			IHttpContextAccessor httpContextAccessor = Substitute.For<IHttpContextAccessor>();
			httpContextAccessor.GetInstance().Returns(httpContext);
			return httpContextAccessor;
		}

		protected EntityColumnValueCollection CreateDummyEntityColumnValueCollection() {
			return new EntityColumnValueCollection(UserConnection) {
				new EntityColumnValue(UserConnection) {
					Name = "TestColumnName"
				},
				new EntityColumnValue(UserConnection) {
					Name = "TestColumnName2"
				}
			};
		}

		protected Entity CreateEmptyEntity(string entitySchemaName) {
			EntitySchema entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName(entitySchemaName);
			Entity entity = entitySchema.CreateEntity(UserConnection);
			return entity;
		}

		protected virtual Entity CreateEntity(string entitySchemaName, IDictionary<string, object> columns,
			bool withSave = false) {
			EntitySchema schema = UserConnection.EntitySchemaManager.FindInstanceByName(entitySchemaName);
			Entity entity = schema.CreateEntity(UserConnection);
			foreach (KeyValuePair<string, object> column in columns) {
				entity.SetColumnValue(column.Key, column.Value);
			}
			if (withSave) {
				entity.Save(false);
			}
			return entity;
		}

		protected void MockEmptyEntitySchema(string entitySchemaName) {
			TestEntitySchemaManager entitySchemaManager = UserConnection.EntitySchemaManager as TestEntitySchemaManager;
			EntitySchema testEntitySchema =
				entitySchemaManager.CreateEntitySchemaMock(entitySchemaName, new Dictionary<string, string>());
			entitySchemaManager.AddSchema(testEntitySchema);
		}

		protected void MockEntitySchemaWithColumns(string entitySchemaName, Dictionary<string, string> columns = null,
			Dictionary<string, string> lookupColumns = null) {
			TestEntitySchemaManager entitySchemaManager = UserConnection.EntitySchemaManager as TestEntitySchemaManager;
			EntitySchema testEntitySchema =
				entitySchemaManager.CreateEntitySchemaMock(entitySchemaName, columns, lookupColumns);
			entitySchemaManager.AddSchema(testEntitySchema);
		}

		protected void MockEntitySchemaWithColumns(string entitySchemaName, Dictionary<string, DataValueType> columns,
			Dictionary<string, string> lookupColumns = null) {
			TestEntitySchemaManager entitySchemaManager = UserConnection.EntitySchemaManager as TestEntitySchemaManager;
			Dictionary<string, string> stringDictionary = columns.ToDictionary(
				pair => pair.Key,
				pair => pair.Value.ToString()
			);
			EntitySchema testEntitySchema =
				entitySchemaManager.CreateEntitySchemaMock(entitySchemaName, stringDictionary, lookupColumns);
			entitySchemaManager.AddSchema(testEntitySchema);
		}

		protected void MockEntitySchemaWithParentUId(string entitySchemaName, Dictionary<string, string> columns,
			Dictionary<string, string> lookupColumns, Guid baseSchemaUId, Guid uid) {
			TestEntitySchemaManager entitySchemaManager = UserConnection.EntitySchemaManager as TestEntitySchemaManager;
			EntitySchema testEntitySchema =
				entitySchemaManager.CreateEntitySchemaMock(entitySchemaName, columns, lookupColumns, baseSchemaUId,
					uid);
			entitySchemaManager.AddSchema(testEntitySchema);
		}

		protected void MockLczValues(string cultureName, string resourceSchemaName,
			IReadOnlyDictionary<string, string> resources) {
			GeneralResourceStorage.SetCurentCulture(new CultureInfo(cultureName), true);
			IResourceStorage resourceStorageMock = Substitute.For<IResourceStorage>();
			IResourceManager rm = Substitute.For<IResourceManager>();
			SysWorkspace workSpaceMock = UserConnection.SetupSysWorkspace();
			workSpaceMock.ResourceStorage = resourceStorageMock;

			foreach (KeyValuePair<string, string> resource in resources) {
				rm.GetStringWithCultureFallback(
						$"LocalizableStrings.{resource.Key}.Value",
						Arg.Is<CultureInfo>(ci => ci.Name == cultureName))
					.Returns($"{resource.Value}");
			}
			workSpaceMock.ResourceStorage.GetManager(resourceSchemaName).Returns(rm);
		}

		protected void SetUpTestData(string entitySchemaName, params Dictionary<string, object>[] items) {
			SelectData selectData = new SelectData(UserConnection, entitySchemaName);
			items.ForEach(values => selectData.AddRow(values));
			selectData.MockUp();
		}

		protected void SetUpTestData(string entitySchemaName, Action<SelectData> filterAction,
			params Dictionary<string, object>[] items) {
			SelectData selectData = new SelectData(UserConnection, entitySchemaName);
			items.ForEach(values => selectData.AddRow(values));
			filterAction.Invoke(selectData);
			selectData.MockUp();
		}

		protected Entity SubstituteEmptyEntity(string entitySchemaName) {
			EntitySchema entitySchema = UserConnection.EntitySchemaManager.GetInstanceByName(entitySchemaName);
			Entity entity = Substitute.ForPartsOf<Entity>(UserConnection);
			entity.Schema = entitySchema;
			return entity;
		}

		#endregion

		#region Methods: Internal

		internal static IReadOnlyDictionary<string, string> GetLocalizableResourcesFromFile(string localization,
			string resourceDirectoryPath) {
			string resourcesDirectory = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName,
				resourceDirectoryPath);
			FileInfo[] files = new DirectoryInfo(resourcesDirectory).GetFiles();
			string requiredFileName = $"resource.{localization}.xml";

			Assert.That(files.Any(f => f.Name == requiredFileName), Is.True,
				$"Resource file {requiredFileName} not found");

			FileInfo resourceFile = files.First(f => f.Name == requiredFileName);

			string xmlContent = File.ReadAllText(resourceFile.FullName);
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlContent);
			XmlNodeList nodeList = xmlDoc.SelectNodes("/Resources/Group/Items/Item");
			Dictionary<string, string> resources = new Dictionary<string, string>();
			foreach (XmlNode node in nodeList) {
				string nameAttr = node.Attributes["Name"].Value;
				if (nameAttr.StartsWith("LocalizableStrings")) {
					string valueAttr = node.Attributes["Value"].Value;
					string[] arr = nameAttr.Split('.');
					ArraySegment<string> arrParsed = new ArraySegment<string>(arr, 1, arr.Length - 2);
					string lczKey = string.Join(".", arrParsed);
					resources.Add(lczKey, valueAttr);
				}
			}
			return resources;
		}

		#endregion

		protected override void SetUp(){
			base.SetUp();
			ClassFactory.RebindWithFactoryMethod(()=>(UserConnection)UserConnection);
		}

		private protected T EvalCall<T>(CallInfo call) where T : class{
			if (typeof(T) == typeof(EvalCallEsqResult)) {
				return EvalEsqCall(call) as T;
			}
			if (typeof(T) == typeof(EvalCallProcessResult)) {
				return EvalStartProcessCall(call) as T;
			}
			return default;
		}
		
		private static EvalCallEsqResult EvalEsqCall(CallInfo call){
			QueryParameterCollection collection = call[1] as QueryParameterCollection;
			Dictionary<string, object> columns = new Dictionary<string, object>();

			if (collection[0].ParentParametrizedQuery is Update) {
				for (int i = 0; i < collection.Count; i++) {
					QueryParameter queryParameter = collection[i];
					object value = queryParameter.Value;
					//string columName = (queryParameter.ParentParametrizedQuery as Update).ColumnValues.FirstOrDefault()?.SourceColumnAlias;
					columns.Add(queryParameter.Name, value);
				}
			}

			if (collection[0].ParentParametrizedQuery is Insert) {
				for (int i = 0; i < collection.Count; i++) {
					ModifyQueryColumnValueCollection mqcc
						= ((dynamic)collection[i].ParentParametrizedQuery).ColumnValues as
						ModifyQueryColumnValueCollection;
					string columName = mqcc[i].SourceColumnAlias;
					object value = collection[i].Value;
					columns.Add(columName, value);
				}
			}

			string schemaName = string.Empty;
			QueryType qt = QueryType.Undefined;
			if (collection[0].ParentParametrizedQuery is Insert insert) {
				schemaName = insert.Source.SchemaName;
				qt = QueryType.Insert;
			}
			if (collection[0].ParentParametrizedQuery is Update update) {
				schemaName = update.Source.SchemaName;
				qt = QueryType.Update;
			}
			if (collection[0].ParentParametrizedQuery is Delete delete) {
				schemaName = delete.Source.SchemaName;
				qt = QueryType.Delete;
				IEnumerable<KeyValuePair<string, object>> kvp
					= delete.Parameters.Select(i => new KeyValuePair<string, object>(i.Name, i.Value));
				foreach (KeyValuePair<string, object> pair in kvp) {
					columns.Add(pair.Key, pair.Value);
				}
				delete.BuildParametersAsValue = true;
				string sqlText = delete.GetSqlText();
				return new EvalCallEsqResult(qt, columns, schemaName, sqlText);
			}
			if (collection[0].ParentParametrizedQuery is Select select) {
				schemaName = "";
				qt = QueryType.Select;
				IEnumerable<KeyValuePair<string, object>> kvp
					= select.Parameters.Select(i => new KeyValuePair<string, object>(i.Name, i.Value));
				foreach (KeyValuePair<string, object> pair in kvp) {
					columns.Add(pair.Key, pair.Value);
				}
				select.BuildParametersAsValue = true;
				string sqlText = select.GetSqlText();
				return new EvalCallEsqResult(qt, columns, schemaName, sqlText);
			}

			return new EvalCallEsqResult(qt, columns, schemaName, call[0] as string);
		}

		private static EvalCallProcessResult EvalStartProcessCall(CallInfo call){
			string processName = call[0] as string;
			IReadOnlyDictionary<string, string> inputParameters = call[1] as IReadOnlyDictionary<string, string>;
			IEnumerable<string> resultParameters = call[2] as IEnumerable<string>;
			return new EvalCallProcessResult(processName, inputParameters, resultParameters);
		}
	}

	#endregion
	
	
	
	public enum QueryType {

		Insert,
		Update,
		Select,
		Delete,
		Undefined

	}
	internal class EvalCallEsqResult
	{

		public EvalCallEsqResult(QueryType queryType, IReadOnlyDictionary<string, object> columns, string schemaName, string sqlText){
			QueryType = queryType;
			Columns = columns;
			SchemaName = schemaName;
			SqlText = sqlText;
			
		}
		
		public QueryType QueryType {get; private set;}
		public IReadOnlyDictionary<string, object> Columns { get; private set; }
		public string SchemaName { get; private set; }
		public string SqlText { get; private set; }
	}


	internal class EvalCallProcessResult {
		public EvalCallProcessResult(string processName, IReadOnlyDictionary<string,string> inputParameters, IEnumerable<string> resultParameters){
			ProcessName = processName;
			InputParameters = inputParameters;
			ResultParameters = resultParameters;
		}

		public string ProcessName { get; private set; }
		public IReadOnlyDictionary<string,string> InputParameters { get; private set; }
		public IEnumerable<string> ResultParameters { get; private set; }
	}
}