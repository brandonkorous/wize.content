using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using wize.common.tenancy.Interfaces;
using wize.content.data.v1.Models;
//using wize.common.use.Helpers;

namespace wize.content.data
{
    public class WizeContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;


        public WizeContext(DbContextOptions<WizeContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public override TEntity Find<TEntity>(params object[] keyValues)
        {
            var model = base.Find<TEntity>(keyValues);
            var tenantId = _tenantProvider.GetTenantId();
            var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
            if (!tenantId.HasValue || modelTenantId != tenantId.Value)
                return default;

            return model;
        }

        public override object Find(Type entityType, params object[] keyValues)
        {
            var model = base.Find(entityType, keyValues);
            var tenantId = _tenantProvider.GetTenantId();
            var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
            if (!tenantId.HasValue || modelTenantId != tenantId.Value)
                return default;

            return model;
        }

        public override ValueTask<object> FindAsync(Type entityType, params object[] keyValues)
        {
            var model = base.FindAsync(entityType, keyValues);
            var tenantId = _tenantProvider.GetTenantId();
            var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
            if (!tenantId.HasValue || modelTenantId != tenantId.Value)
                return default;

            return model;
        }

        public override ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues)
        {
            var model = base.FindAsync<TEntity>(keyValues);
            var tenantId = _tenantProvider.GetTenantId();
            var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
            if (!tenantId.HasValue || modelTenantId != tenantId.Value)
                return default;

            return model;
        }

        public override ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
        {
            var model = base.FindAsync<TEntity>(keyValues, cancellationToken);
            var tenantId = _tenantProvider.GetTenantId();
            var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
            if (!tenantId.HasValue || modelTenantId != tenantId.Value)
                return default;

            return model;
        }

        public override ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        {
            var model = base.FindAsync(entityType, keyValues, cancellationToken);
            var tenantId = _tenantProvider.GetTenantId();
            var modelTenantId = base.Entry(model).CurrentValues.GetValue<Guid>("TenantId");
            if (!tenantId.HasValue || modelTenantId != tenantId.Value)
                return default;

            return model;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentFile>().HasKey(cf => new { cf.ContentId, cf.FileId });
            modelBuilder = AddTenancy(modelBuilder);
            //modelBuilder = AddDeleted(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ApplyTenancy();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyTenancy();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTenancy();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyTenancy();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyTenancy()
        {
            var modified = ChangeTracker.Entries<ITenantModel>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entity in modified)
            {
                var property = entity.Property("TenantId");
                if (property != null)
                {
                    property.CurrentValue = _tenantProvider.GetTenantId();
                }
            }
        }

        private ModelBuilder AddTenancy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().Property<Guid>("TenantId");
            modelBuilder.Entity<FrontPage>().Property<Guid>("TenantId");
            modelBuilder.Entity<Thumbnail>().Property<Guid>("TenantId");
            modelBuilder.Entity<FileType>().Property<Guid>("TenantId");
            modelBuilder.Entity<Content>().Property<Guid>("TenantId");
            modelBuilder.Entity<ContentFile>().Property<Guid>("TenantId");
            modelBuilder.Entity<ContentType>().Property<Guid>("TenantId");
            modelBuilder.Entity<ContentArchive>().Property<Guid>("TenantId");
            modelBuilder.Entity<Link>().Property<Guid>("TenantId");
            modelBuilder.Entity<LinkGroup>().Property<Guid>("TenantId");

            return modelBuilder;
        }

        private ModelBuilder AddDeleted(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>().Property<bool>("IsDeleted");
            modelBuilder.Entity<File>().HasQueryFilter(f => EF.Property<bool>(f, "IsDeleted"));

            return modelBuilder;
        }


        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Thumbnail> Thumbnails { get; set; }
        public virtual DbSet<FileType> FileTypes { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentFile> ContentFiles { get; set; }
        public virtual DbSet<ContentArchive> ContentArchives { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }
        public virtual DbSet<FrontPage> FrontPages { get; set; }
        public virtual DbSet<Link> Links { get; set; }
        public virtual DbSet<LinkGroup> LinkGroups { get; set; }
    }
}
