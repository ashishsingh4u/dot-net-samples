#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataObjects.LinqToSql
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Action")]
	public partial class ActionDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCategoryEntity(CategoryEntity instance);
    partial void UpdateCategoryEntity(CategoryEntity instance);
    partial void DeleteCategoryEntity(CategoryEntity instance);
    partial void InsertCustomerEntity(CustomerEntity instance);
    partial void UpdateCustomerEntity(CustomerEntity instance);
    partial void DeleteCustomerEntity(CustomerEntity instance);
    partial void InsertOrderEntity(OrderEntity instance);
    partial void UpdateOrderEntity(OrderEntity instance);
    partial void DeleteOrderEntity(OrderEntity instance);
    partial void InsertProductEntity(ProductEntity instance);
    partial void UpdateProductEntity(ProductEntity instance);
    partial void DeleteProductEntity(ProductEntity instance);
    partial void InsertOrderDetailEntity(OrderDetailEntity instance);
    partial void UpdateOrderDetailEntity(OrderDetailEntity instance);
    partial void DeleteOrderDetailEntity(OrderDetailEntity instance);
    #endregion
		
		public ActionDataContext() : 
				base(global::DataObjects.Properties.Settings.Default.ActionConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ActionDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ActionDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ActionDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ActionDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CategoryEntity> CategoryEntities
		{
			get
			{
				return this.GetTable<CategoryEntity>();
			}
		}
		
		public System.Data.Linq.Table<CustomerEntity> CustomerEntities
		{
			get
			{
				return this.GetTable<CustomerEntity>();
			}
		}
		
		public System.Data.Linq.Table<OrderEntity> OrderEntities
		{
			get
			{
				return this.GetTable<OrderEntity>();
			}
		}
		
		public System.Data.Linq.Table<ProductEntity> ProductEntities
		{
			get
			{
				return this.GetTable<ProductEntity>();
			}
		}
		
		public System.Data.Linq.Table<OrderDetailEntity> OrderDetailEntities
		{
			get
			{
				return this.GetTable<OrderDetailEntity>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Category")]
	public partial class CategoryEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CategoryId;
		
		private string _CategoryName;
		
		private string _Description;
		
		private System.Data.Linq.Binary _Version;
		
		private EntitySet<ProductEntity> _ProductEntities;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategoryIdChanging(int value);
    partial void OnCategoryIdChanged();
    partial void OnCategoryNameChanging(string value);
    partial void OnCategoryNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnVersionChanging(System.Data.Linq.Binary value);
    partial void OnVersionChanged();
    #endregion
		
		public CategoryEntity()
		{
			this._ProductEntities = new EntitySet<ProductEntity>(new Action<ProductEntity>(this.attach_ProductEntities), new Action<ProductEntity>(this.detach_ProductEntities));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int CategoryId
		{
			get
			{
				return this._CategoryId;
			}
			set
			{
				if ((this._CategoryId != value))
				{
					this.OnCategoryIdChanging(value);
					this.SendPropertyChanging();
					this._CategoryId = value;
					this.SendPropertyChanged("CategoryId");
					this.OnCategoryIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryName", DbType="VarChar(15) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string CategoryName
		{
			get
			{
				return this._CategoryName;
			}
			set
			{
				if ((this._CategoryName != value))
				{
					this.OnCategoryNameChanging(value);
					this.SendPropertyChanging();
					this._CategoryName = value;
					this.SendPropertyChanged("CategoryName");
					this.OnCategoryNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="VarChar(100) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Version", AutoSync=AutoSync.Always, DbType="rowversion NOT NULL", CanBeNull=false, IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this.OnVersionChanging(value);
					this.SendPropertyChanging();
					this._Version = value;
					this.SendPropertyChanged("Version");
					this.OnVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CategoryEntity_ProductEntity", Storage="_ProductEntities", ThisKey="CategoryId", OtherKey="CategoryId")]
		public EntitySet<ProductEntity> ProductEntities
		{
			get
			{
				return this._ProductEntities;
			}
			set
			{
				this._ProductEntities.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_ProductEntities(ProductEntity entity)
		{
			this.SendPropertyChanging();
			entity.CategoryEntity = this;
		}
		
		private void detach_ProductEntities(ProductEntity entity)
		{
			this.SendPropertyChanging();
			entity.CategoryEntity = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Customer")]
	public partial class CustomerEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CustomerId;
		
		private string _CompanyName;
		
		private string _City;
		
		private string _Country;
		
		private System.Data.Linq.Binary _Version;
		
		private EntitySet<OrderEntity> _OrderEntities;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCustomerIdChanging(int value);
    partial void OnCustomerIdChanged();
    partial void OnCompanyNameChanging(string value);
    partial void OnCompanyNameChanged();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnCountryChanging(string value);
    partial void OnCountryChanged();
    partial void OnVersionChanging(System.Data.Linq.Binary value);
    partial void OnVersionChanged();
    #endregion
		
		public CustomerEntity()
		{
			this._OrderEntities = new EntitySet<OrderEntity>(new Action<OrderEntity>(this.attach_OrderEntities), new Action<OrderEntity>(this.detach_OrderEntities));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int CustomerId
		{
			get
			{
				return this._CustomerId;
			}
			set
			{
				if ((this._CustomerId != value))
				{
					this.OnCustomerIdChanging(value);
					this.SendPropertyChanging();
					this._CustomerId = value;
					this.SendPropertyChanged("CustomerId");
					this.OnCustomerIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyName", DbType="VarChar(40) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string CompanyName
		{
			get
			{
				return this._CompanyName;
			}
			set
			{
				if ((this._CompanyName != value))
				{
					this.OnCompanyNameChanging(value);
					this.SendPropertyChanging();
					this._CompanyName = value;
					this.SendPropertyChanged("CompanyName");
					this.OnCompanyNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="VarChar(15) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				if ((this._City != value))
				{
					this.OnCityChanging(value);
					this.SendPropertyChanging();
					this._City = value;
					this.SendPropertyChanged("City");
					this.OnCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Country", DbType="VarChar(15) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				if ((this._Country != value))
				{
					this.OnCountryChanging(value);
					this.SendPropertyChanging();
					this._Country = value;
					this.SendPropertyChanged("Country");
					this.OnCountryChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Version", AutoSync=AutoSync.Always, DbType="rowversion NOT NULL", CanBeNull=false, IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this.OnVersionChanging(value);
					this.SendPropertyChanging();
					this._Version = value;
					this.SendPropertyChanged("Version");
					this.OnVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerEntity_OrderEntity", Storage="_OrderEntities", ThisKey="CustomerId", OtherKey="CustomerId")]
		public EntitySet<OrderEntity> OrderEntities
		{
			get
			{
				return this._OrderEntities;
			}
			set
			{
				this._OrderEntities.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_OrderEntities(OrderEntity entity)
		{
			this.SendPropertyChanging();
			entity.CustomerEntity = this;
		}
		
		private void detach_OrderEntities(OrderEntity entity)
		{
			this.SendPropertyChanging();
			entity.CustomerEntity = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[Order]")]
	public partial class OrderEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _OrderId;
		
		private int _CustomerId;
		
		private System.DateTime _OrderDate;
		
		private System.Nullable<System.DateTime> _RequiredDate;
		
		private System.Nullable<System.DateTime> _ShippedDate;
		
		private System.Nullable<decimal> _Freight;
		
		private System.Data.Linq.Binary _Version;
		
		private EntitySet<OrderDetailEntity> _OrderDetailEntities;
		
		private EntityRef<CustomerEntity> _CustomerEntity;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOrderIdChanging(int value);
    partial void OnOrderIdChanged();
    partial void OnCustomerIdChanging(int value);
    partial void OnCustomerIdChanged();
    partial void OnOrderDateChanging(System.DateTime value);
    partial void OnOrderDateChanged();
    partial void OnRequiredDateChanging(System.Nullable<System.DateTime> value);
    partial void OnRequiredDateChanged();
    partial void OnShippedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnShippedDateChanged();
    partial void OnFreightChanging(System.Nullable<decimal> value);
    partial void OnFreightChanged();
    partial void OnVersionChanging(System.Data.Linq.Binary value);
    partial void OnVersionChanged();
    #endregion
		
		public OrderEntity()
		{
			this._OrderDetailEntities = new EntitySet<OrderDetailEntity>(new Action<OrderDetailEntity>(this.attach_OrderDetailEntities), new Action<OrderDetailEntity>(this.detach_OrderDetailEntities));
			this._CustomerEntity = default(EntityRef<CustomerEntity>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int OrderId
		{
			get
			{
				return this._OrderId;
			}
			set
			{
				if ((this._OrderId != value))
				{
					this.OnOrderIdChanging(value);
					this.SendPropertyChanging();
					this._OrderId = value;
					this.SendPropertyChanged("OrderId");
					this.OnOrderIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerId", DbType="Int NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public int CustomerId
		{
			get
			{
				return this._CustomerId;
			}
			set
			{
				if ((this._CustomerId != value))
				{
					if (this._CustomerEntity.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCustomerIdChanging(value);
					this.SendPropertyChanging();
					this._CustomerId = value;
					this.SendPropertyChanged("CustomerId");
					this.OnCustomerIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderDate", DbType="DateTime NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public System.DateTime OrderDate
		{
			get
			{
				return this._OrderDate;
			}
			set
			{
				if ((this._OrderDate != value))
				{
					this.OnOrderDateChanging(value);
					this.SendPropertyChanging();
					this._OrderDate = value;
					this.SendPropertyChanged("OrderDate");
					this.OnOrderDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RequiredDate", DbType="DateTime", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.DateTime> RequiredDate
		{
			get
			{
				return this._RequiredDate;
			}
			set
			{
				if ((this._RequiredDate != value))
				{
					this.OnRequiredDateChanging(value);
					this.SendPropertyChanging();
					this._RequiredDate = value;
					this.SendPropertyChanged("RequiredDate");
					this.OnRequiredDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShippedDate", DbType="DateTime", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<System.DateTime> ShippedDate
		{
			get
			{
				return this._ShippedDate;
			}
			set
			{
				if ((this._ShippedDate != value))
				{
					this.OnShippedDateChanging(value);
					this.SendPropertyChanging();
					this._ShippedDate = value;
					this.SendPropertyChanged("ShippedDate");
					this.OnShippedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Freight", DbType="Money", UpdateCheck=UpdateCheck.Never)]
		public System.Nullable<decimal> Freight
		{
			get
			{
				return this._Freight;
			}
			set
			{
				if ((this._Freight != value))
				{
					this.OnFreightChanging(value);
					this.SendPropertyChanging();
					this._Freight = value;
					this.SendPropertyChanged("Freight");
					this.OnFreightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Version", AutoSync=AutoSync.Always, DbType="rowversion NOT NULL", CanBeNull=false, IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this.OnVersionChanging(value);
					this.SendPropertyChanging();
					this._Version = value;
					this.SendPropertyChanged("Version");
					this.OnVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="OrderEntity_OrderDetailEntity", Storage="_OrderDetailEntities", ThisKey="OrderId", OtherKey="OrderId")]
		public EntitySet<OrderDetailEntity> OrderDetailEntities
		{
			get
			{
				return this._OrderDetailEntities;
			}
			set
			{
				this._OrderDetailEntities.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerEntity_OrderEntity", Storage="_CustomerEntity", ThisKey="CustomerId", OtherKey="CustomerId", IsForeignKey=true)]
		public CustomerEntity CustomerEntity
		{
			get
			{
				return this._CustomerEntity.Entity;
			}
			set
			{
				CustomerEntity previousValue = this._CustomerEntity.Entity;
				if (((previousValue != value) 
							|| (this._CustomerEntity.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CustomerEntity.Entity = null;
						previousValue.OrderEntities.Remove(this);
					}
					this._CustomerEntity.Entity = value;
					if ((value != null))
					{
						value.OrderEntities.Add(this);
						this._CustomerId = value.CustomerId;
					}
					else
					{
						this._CustomerId = default(int);
					}
					this.SendPropertyChanged("CustomerEntity");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_OrderDetailEntities(OrderDetailEntity entity)
		{
			this.SendPropertyChanging();
			entity.OrderEntity = this;
		}
		
		private void detach_OrderDetailEntities(OrderDetailEntity entity)
		{
			this.SendPropertyChanging();
			entity.OrderEntity = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Product")]
	public partial class ProductEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ProductId;
		
		private int _CategoryId;
		
		private string _ProductName;
		
		private string _Weight;
		
		private decimal _UnitPrice;
		
		private int _UnitsInStock;
		
		private System.Data.Linq.Binary _Version;
		
		private EntitySet<OrderDetailEntity> _OrderDetailEntities;
		
		private EntityRef<CategoryEntity> _CategoryEntity;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnProductIdChanging(int value);
    partial void OnProductIdChanged();
    partial void OnCategoryIdChanging(int value);
    partial void OnCategoryIdChanged();
    partial void OnProductNameChanging(string value);
    partial void OnProductNameChanged();
    partial void OnWeightChanging(string value);
    partial void OnWeightChanged();
    partial void OnUnitPriceChanging(decimal value);
    partial void OnUnitPriceChanged();
    partial void OnUnitsInStockChanging(int value);
    partial void OnUnitsInStockChanged();
    partial void OnVersionChanging(System.Data.Linq.Binary value);
    partial void OnVersionChanged();
    #endregion
		
		public ProductEntity()
		{
			this._OrderDetailEntities = new EntitySet<OrderDetailEntity>(new Action<OrderDetailEntity>(this.attach_OrderDetailEntities), new Action<OrderDetailEntity>(this.detach_OrderDetailEntities));
			this._CategoryEntity = default(EntityRef<CategoryEntity>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				if ((this._ProductId != value))
				{
					this.OnProductIdChanging(value);
					this.SendPropertyChanging();
					this._ProductId = value;
					this.SendPropertyChanged("ProductId");
					this.OnProductIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryId", DbType="Int NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public int CategoryId
		{
			get
			{
				return this._CategoryId;
			}
			set
			{
				if ((this._CategoryId != value))
				{
					if (this._CategoryEntity.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCategoryIdChanging(value);
					this.SendPropertyChanging();
					this._CategoryId = value;
					this.SendPropertyChanged("CategoryId");
					this.OnCategoryIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductName", DbType="VarChar(40) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string ProductName
		{
			get
			{
				return this._ProductName;
			}
			set
			{
				if ((this._ProductName != value))
				{
					this.OnProductNameChanging(value);
					this.SendPropertyChanging();
					this._ProductName = value;
					this.SendPropertyChanged("ProductName");
					this.OnProductNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weight", DbType="VarChar(20) NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if ((this._Weight != value))
				{
					this.OnWeightChanging(value);
					this.SendPropertyChanging();
					this._Weight = value;
					this.SendPropertyChanged("Weight");
					this.OnWeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UnitPrice", DbType="Money NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public decimal UnitPrice
		{
			get
			{
				return this._UnitPrice;
			}
			set
			{
				if ((this._UnitPrice != value))
				{
					this.OnUnitPriceChanging(value);
					this.SendPropertyChanging();
					this._UnitPrice = value;
					this.SendPropertyChanged("UnitPrice");
					this.OnUnitPriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UnitsInStock", DbType="Int NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public int UnitsInStock
		{
			get
			{
				return this._UnitsInStock;
			}
			set
			{
				if ((this._UnitsInStock != value))
				{
					this.OnUnitsInStockChanging(value);
					this.SendPropertyChanging();
					this._UnitsInStock = value;
					this.SendPropertyChanged("UnitsInStock");
					this.OnUnitsInStockChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Version", AutoSync=AutoSync.Always, DbType="rowversion NOT NULL", CanBeNull=false, IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this.OnVersionChanging(value);
					this.SendPropertyChanging();
					this._Version = value;
					this.SendPropertyChanged("Version");
					this.OnVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ProductEntity_OrderDetailEntity", Storage="_OrderDetailEntities", ThisKey="ProductId", OtherKey="ProductId")]
		public EntitySet<OrderDetailEntity> OrderDetailEntities
		{
			get
			{
				return this._OrderDetailEntities;
			}
			set
			{
				this._OrderDetailEntities.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CategoryEntity_ProductEntity", Storage="_CategoryEntity", ThisKey="CategoryId", OtherKey="CategoryId", IsForeignKey=true)]
		public CategoryEntity CategoryEntity
		{
			get
			{
				return this._CategoryEntity.Entity;
			}
			set
			{
				CategoryEntity previousValue = this._CategoryEntity.Entity;
				if (((previousValue != value) 
							|| (this._CategoryEntity.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CategoryEntity.Entity = null;
						previousValue.ProductEntities.Remove(this);
					}
					this._CategoryEntity.Entity = value;
					if ((value != null))
					{
						value.ProductEntities.Add(this);
						this._CategoryId = value.CategoryId;
					}
					else
					{
						this._CategoryId = default(int);
					}
					this.SendPropertyChanged("CategoryEntity");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_OrderDetailEntities(OrderDetailEntity entity)
		{
			this.SendPropertyChanging();
			entity.ProductEntity = this;
		}
		
		private void detach_OrderDetailEntities(OrderDetailEntity entity)
		{
			this.SendPropertyChanging();
			entity.ProductEntity = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.OrderDetail")]
	public partial class OrderDetailEntity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _OrderId;
		
		private int _ProductId;
		
		private decimal _UnitPrice;
		
		private int _Quantity;
		
		private double _Discount;
		
		private System.Data.Linq.Binary _Version;
		
		private EntityRef<OrderEntity> _OrderEntity;
		
		private EntityRef<ProductEntity> _ProductEntity;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOrderIdChanging(int value);
    partial void OnOrderIdChanged();
    partial void OnProductIdChanging(int value);
    partial void OnProductIdChanged();
    partial void OnUnitPriceChanging(decimal value);
    partial void OnUnitPriceChanged();
    partial void OnQuantityChanging(int value);
    partial void OnQuantityChanged();
    partial void OnDiscountChanging(double value);
    partial void OnDiscountChanged();
    partial void OnVersionChanging(System.Data.Linq.Binary value);
    partial void OnVersionChanged();
    #endregion
		
		public OrderDetailEntity()
		{
			this._OrderEntity = default(EntityRef<OrderEntity>);
			this._ProductEntity = default(EntityRef<ProductEntity>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderId", DbType="Int NOT NULL", IsPrimaryKey=true, UpdateCheck=UpdateCheck.Never)]
		public int OrderId
		{
			get
			{
				return this._OrderId;
			}
			set
			{
				if ((this._OrderId != value))
				{
					if (this._OrderEntity.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOrderIdChanging(value);
					this.SendPropertyChanging();
					this._OrderId = value;
					this.SendPropertyChanged("OrderId");
					this.OnOrderIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProductId", DbType="Int NOT NULL", IsPrimaryKey=true, UpdateCheck=UpdateCheck.Never)]
		public int ProductId
		{
			get
			{
				return this._ProductId;
			}
			set
			{
				if ((this._ProductId != value))
				{
					if (this._ProductEntity.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnProductIdChanging(value);
					this.SendPropertyChanging();
					this._ProductId = value;
					this.SendPropertyChanged("ProductId");
					this.OnProductIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UnitPrice", DbType="Money NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public decimal UnitPrice
		{
			get
			{
				return this._UnitPrice;
			}
			set
			{
				if ((this._UnitPrice != value))
				{
					this.OnUnitPriceChanging(value);
					this.SendPropertyChanging();
					this._UnitPrice = value;
					this.SendPropertyChanged("UnitPrice");
					this.OnUnitPriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Quantity", DbType="Int NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				if ((this._Quantity != value))
				{
					this.OnQuantityChanging(value);
					this.SendPropertyChanging();
					this._Quantity = value;
					this.SendPropertyChanged("Quantity");
					this.OnQuantityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Discount", DbType="Float NOT NULL", UpdateCheck=UpdateCheck.Never)]
		public double Discount
		{
			get
			{
				return this._Discount;
			}
			set
			{
				if ((this._Discount != value))
				{
					this.OnDiscountChanging(value);
					this.SendPropertyChanging();
					this._Discount = value;
					this.SendPropertyChanged("Discount");
					this.OnDiscountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Version", AutoSync=AutoSync.Always, DbType="rowversion NOT NULL", CanBeNull=false, IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if ((this._Version != value))
				{
					this.OnVersionChanging(value);
					this.SendPropertyChanging();
					this._Version = value;
					this.SendPropertyChanged("Version");
					this.OnVersionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="OrderEntity_OrderDetailEntity", Storage="_OrderEntity", ThisKey="OrderId", OtherKey="OrderId", IsForeignKey=true)]
		public OrderEntity OrderEntity
		{
			get
			{
				return this._OrderEntity.Entity;
			}
			set
			{
				OrderEntity previousValue = this._OrderEntity.Entity;
				if (((previousValue != value) 
							|| (this._OrderEntity.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._OrderEntity.Entity = null;
						previousValue.OrderDetailEntities.Remove(this);
					}
					this._OrderEntity.Entity = value;
					if ((value != null))
					{
						value.OrderDetailEntities.Add(this);
						this._OrderId = value.OrderId;
					}
					else
					{
						this._OrderId = default(int);
					}
					this.SendPropertyChanged("OrderEntity");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="ProductEntity_OrderDetailEntity", Storage="_ProductEntity", ThisKey="ProductId", OtherKey="ProductId", IsForeignKey=true)]
		public ProductEntity ProductEntity
		{
			get
			{
				return this._ProductEntity.Entity;
			}
			set
			{
				ProductEntity previousValue = this._ProductEntity.Entity;
				if (((previousValue != value) 
							|| (this._ProductEntity.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ProductEntity.Entity = null;
						previousValue.OrderDetailEntities.Remove(this);
					}
					this._ProductEntity.Entity = value;
					if ((value != null))
					{
						value.OrderDetailEntities.Add(this);
						this._ProductId = value.ProductId;
					}
					else
					{
						this._ProductId = default(int);
					}
					this.SendPropertyChanged("ProductEntity");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
