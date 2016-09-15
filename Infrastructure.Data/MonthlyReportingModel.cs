using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Validation;
using System.Linq;
using Domain.Entities;

namespace Infrastructure.Data
{
    public partial class MonthlyReportingModel : DbContext
    {
        public MonthlyReportingModel()
            : base("name=MonthlyReportingModel")
        {
        }

        public virtual DbSet<RootObject> RootObjects { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<ApiEndpoint> ApiEndpoints { get; set; }
        public virtual DbSet<ApiKeyType> ApiKeyTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RootObject>()
                .ToTable("RootObject");
            modelBuilder.Entity<RootObject>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<RootObject>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Result>()
                .ToTable("Result");
            modelBuilder.Entity<Result>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Result>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Content>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Content>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Event>()
                .ToTable("Event");
            modelBuilder.Entity<Event>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Event>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Event>()
               .Ignore(t => t.IsDuplicate);
            modelBuilder.Entity<Event>().Property(t => t.HashedByteValue)
               .HasMaxLength(450);
            modelBuilder.Entity<Event>()
                .Property(e => e.appName)
                .IsUnicode(false)
                .HasMaxLength(100);
            modelBuilder.Entity<Event>()
                .Property(e => e.host)
                .IsUnicode(false)
                .HasMaxLength(100);
            modelBuilder.Entity<Event>()
                .Property(e => e.name)
                .IsUnicode(false)
                .HasMaxLength(100);
            modelBuilder.Entity<Event>()
                .Property(e => e.tripId)
                .IsUnicode(false)
                .HasMaxLength(100);
            modelBuilder.Entity<Event>()
                .Property(e => e.errorType)
                .IsUnicode(false)
                .HasMaxLength(100);
            modelBuilder.Entity<Event>()
                .Property(e => e.errorMessage)
                .IsUnicode(false)
                .HasMaxLength(1024);
           
            modelBuilder
                .Entity<Event>()
                .Property(t => t.HashedByteValue)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
            modelBuilder.Entity<Metadata>()
                .ToTable("Metadata");
            modelBuilder.Entity<Metadata>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Metadata>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.eventType)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.beginTime)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.endTime)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.rawSince)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.rawUntil)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.rawCompareWith)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.guid)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<Metadata>()
                .Property(e => e.routerGuid)
                .IsUnicode(false)
                .HasMaxLength(512);
            modelBuilder.Entity<PerformanceStats>()
                .ToTable("PerformanceStats");
            modelBuilder.Entity<PerformanceStats>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<PerformanceStats>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Order>()
                .ToTable("Order");
            modelBuilder.Entity<Order>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Order>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Order>()
                .Property(e => e.column)
                .IsUnicode(false)
                .HasMaxLength(100);
            modelBuilder.Entity<Order>()
                .Property(e => e.descending)
                .IsRequired();
            modelBuilder.Entity<RootObject>()
                .HasMany(e => e.results)
                .WithRequired(e => e.RootObject);

            modelBuilder.Entity<ApiEndpoint>()
                .ToTable("ApiEndpoint");

            modelBuilder.Entity<ApiEndpoint>()
                .Property(e => e.ApiKey)
                .IsUnicode(false)
                .HasMaxLength(512)
                .IsRequired();

            modelBuilder.Entity<ApiEndpoint>()
                .Property(e => e.Title)
                .IsUnicode(false)
                .HasMaxLength(80);

            modelBuilder.Entity<ApiEndpoint>()
                .Property(e => e.Endpoint)
                .IsUnicode(false)
                .HasMaxLength(2048)
                .IsRequired();

            modelBuilder.Entity<ApiEndpoint>()
                .Property(e => e.Curl)
                .IsUnicode(false)
                .HasMaxLength(4096);

            modelBuilder.Entity<ApiEndpoint>()
                .Property(e => e.NRSQLSyntax)
                .IsUnicode(false)
                .HasMaxLength(2049);

            modelBuilder.Entity<ApiKeyType>()
                .ToTable("ApiKeyType");

            modelBuilder.Entity<ApiKeyType>()
                .Property(e => e.EndpointType)
                .IsUnicode(false)
                .HasMaxLength(80)
                .IsRequired();

            modelBuilder.Entity<ApiKeyType>()
                .Property(e => e.Description)
                .IsUnicode(false)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<Content>()
                .ToTable("Content");

            modelBuilder.Entity<Content>()
                .Property(e => e.function)
                .HasMaxLength(100)
                .IsUnicode(false);

            modelBuilder.Entity<Content>()
                .Property(e => e.limit);

        }


        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

    }
}
