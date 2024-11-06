using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication_DatabaseFirst_Demo.Models;

namespace DbFirstApi.Models;

public partial class BikeStoresContext : DbContext
{
    public BikeStoresContext()
    {
    }

    public BikeStoresContext(DbContextOptions<BikeStoresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customer1> Customers1 { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Employee2> Employee2s { get; set; }

    public virtual DbSet<EmployeeAudit> EmployeeAudits { get; set; }

    public virtual DbSet<EmployeeDay2> EmployeeDay2s { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Order1> Orders1 { get; set; }

    public virtual DbSet<OrderAudit> OrderAudits { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<TblDepartment> TblDepartments { get; set; }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    public virtual DbSet<VWemployeeByDept> VWemployeeByDepts { get; set; }

    public virtual DbSet<VWemployeeCountByDept> VWemployeeCountByDepts { get; set; }

    public virtual DbSet<VwEmployeeDetail> VwEmployeeDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-2PAUJVTP\\MSSQLSERVER01;Database=BikeStores;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK_Bookings_73951ACD828CD426");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_BookingsRoomID_282DF8C2");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK_brands_5E5A8E2771C7BE10");

            entity.ToTable("brands", "production");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_categori_D54EE9B4F700468F");

            entity.ToTable("categories", "production");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_CUSTOMER_CD65CB85453D14E9");

            entity.ToTable("CUSTOMERS");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer1>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_customer_CD65CB85DA5B8C2D");

            entity.ToTable("customers", "sales", tb => tb.HasTrigger("preventsDeletionOfCustomer"));

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "idx_name");

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "idx_name1");

            entity.HasIndex(e => e.Email, "idx_unique_email").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Departme_3214EC07D83513A5");

            entity.ToTable("Department");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Employee");

            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Dept).WithMany()
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK_EmployeeDeptId_66603565");
        });

        modelBuilder.Entity<Employee2>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK_Employee_AF2DBA79FF26472D");

            entity.ToTable("Employee2", tb => tb.HasTrigger("trg_LogEmployeeChanges"));

            entity.Property(e => e.EmpId)
                .ValueGeneratedNever()
                .HasColumnName("EmpID");
            entity.Property(e => e.EmpName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK_Employee_A17F23B89555ED6E");

            entity.ToTable("Employee_Audit");

            entity.Property(e => e.AuditId).HasColumnName("AuditID");
            entity.Property(e => e.ChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ChangeType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EmpId).HasColumnName("EmpID");
        });

        modelBuilder.Entity<EmployeeDay2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Employee_3214EC07A90E7C11");

            entity.ToTable("EmployeeDay2");

            entity.HasIndex(e => e.Id, "ix_employee_id");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Dept)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_orders_46596229899A43C9");

            entity.ToTable("orders", tb =>
            {
                tb.HasTrigger("tgrAfterInsertOrder");
                tb.HasTrigger("tgrAfterUpdateOrder");
            });

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
        });

        modelBuilder.Entity<Order1>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_orders_46596229A6D081BA");

            entity.ToTable("orders", "sales", tb =>
            {
                tb.HasTrigger("UpdateStockTbl");
                tb.HasTrigger("UpdateStockTbl1");
                tb.HasTrigger("UpdateStockTbl2");
                tb.HasTrigger("UpdateStockTbl3");
            });

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.RequiredDate).HasColumnName("required_date");
            entity.Property(e => e.ShippedDate).HasColumnName("shipped_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orderscustomer_47DBAE45");

            entity.HasOne(d => d.Staff).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ordersstaff_id_49C3F6B7");

            entity.HasOne(d => d.Store).WithMany(p => p.Order1s)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_ordersstore_id_48CFD27E");
        });

        modelBuilder.Entity<OrderAudit>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK_order_au_5AF33E3332A7E745");

            entity.ToTable("order_audit");

            entity.Property(e => e.AuditId).HasColumnName("audit_id");
            entity.Property(e => e.AuditInfo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Audit_info");
            entity.Property(e => e.AuditTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("audit_timestamp");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ItemId }).HasName("PK_order_it_837942D45894DECE");

            entity.ToTable("order_items", "sales", tb => tb.HasTrigger("trg_UpdateStockOnNewOrder"));

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_order_iteorder_4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_order_iteprodu_4E88ABD4");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_products_47027DF5BEBF0091");

            entity.ToTable("products", "production");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            entity.Property(e => e.ModelYear).HasColumnName("model_year");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_productsbrand__3C69FB99");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_productscatego_3B75D760");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK_Room_328639191572EBB1");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("RoomID");
            entity.Property(e => e.RoomType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK_staffs_1963DD9C315D6152");

            entity.ToTable("staffs", "sales");

            entity.HasIndex(e => e.Email, "UQ_staffs_AB6E6164316A6DB3").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_staffsmanager__44FF419A");

            entity.HasOne(d => d.Store).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_staffsstore_id_440B1D61");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.ProductId }).HasName("PK_stocks_E68284D37CF9A72B");

            entity.ToTable("stocks", "production");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_stocksproduct__52593CB8");

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_stocksstore_id_5165187F");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK_stores_A2F2A30C02AE1F7C");

            entity.ToTable("stores", "sales");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("store_name");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<TblDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartId).HasName("PK_tblDepar_0E53E1D5AD55CA33");

            entity.ToTable("tblDepartment");

            entity.Property(e => e.DepartId)
                .ValueGeneratedNever()
                .HasColumnName("DepartID");
            entity.Property(e => e.DeptName).HasMaxLength(20);
        });

        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblEmplo_3214EC072D8394BD");

            entity.ToTable("tblEmployee");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<VWemployeeByDept>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vWEmployeeByDept");

            entity.Property(e => e.DeptName).HasMaxLength(20);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<VWemployeeCountByDept>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vWEmployeeCountByDept");

            entity.Property(e => e.DeptName).HasMaxLength(20);
        });

        modelBuilder.Entity<VwEmployeeDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwEmployeeDetails");

            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}