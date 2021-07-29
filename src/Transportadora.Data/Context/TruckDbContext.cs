
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;


using Transportadora.Business.Extensions;
using Transportadora.Business.Models;

namespace Transportadora.Data.Context
{
    public class TruckDbContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        public TruckDbContext(DbContextOptions options,
            ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.loggerFactory = loggerFactory;
            this.httpContextAccessor = httpContextAccessor;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<ProfileT> Profiles { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Funcionality> Funcionalities { get; set; }
        public DbSet<ActionFuncionality> ActionFuncionalities { get; set; }
        public DbSet<ProfileActionFuncionality> ProfileActionFuncionalities { get; set; }

        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Fleet> Fleets { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
        public DbSet<InsuranceSituation> InsuranceSituations { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<RequisicaoCompra> RequisicaoCompra { get; set; }
        public DbSet<ItensRequisicaoCompra> ItensRequisicaoCompra { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<CostCenter> Costcenter { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<PaymentForm> PaymentForm { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoicePayment> InvoicePayment { get; set; }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<ExpensePayment> ExpensePayment { get; set; }
        public DbSet<ExpenseType> ExpenseType { get; set; }
        public DbSet<ExpenseSupplier> ExpenseSupplier { get; set; }

        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<FinancialSettlement> FinancialSettlement { get; set; }
        public DbSet<ExpenseFinancialSettlement> ExpenseFinancialSettlement { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<AuditLog> AuditLog { get; set; }

        public DbSet<Parameter> Parameter { get; set; }

        public DbSet<FluxoCaixa> FluxoCaixas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TruckDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var temoraryAuditEntities = await AuditNonTemporaryProperties();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await AuditTemporaryProperties(temoraryAuditEntities);
            return result;
        }


        async Task<IEnumerable<Tuple<EntityEntry, AuditLog>>> AuditNonTemporaryProperties()
        {
            ChangeTracker.DetectChanges();
            var entitiesToTrack = ChangeTracker.Entries().Where(e => !(e.Entity is AuditLog) && e.State != EntityState.Detached && e.State != EntityState.Unchanged);

            await AuditLog.AddRangeAsync(
                entitiesToTrack.Where(e => !e.Properties.Any(p => p.IsTemporary)).Select(e => new AuditLog()
                {
                    TableName = GetTableName(e.Entity.ToString()),
                    Action = Enum.GetName(typeof(EntityState), e.State),
                    DateTime = DateTime.Now,
                    Username = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name,
                    KeyValues = JsonConvert.SerializeObject(e.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                    NewValues = JsonConvert.SerializeObject(e.Properties.Where(p => e.State == EntityState.Added || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()),
                    OldValues = JsonConvert.SerializeObject(e.Properties.Where(p => e.State == EntityState.Deleted || e.State == EntityState.Modified).ToDictionary(p => p.Metadata.Name, p => p.OriginalValue).NullIfEmpty())
                }).ToList()
            );

            //Return list of pairs of EntityEntry and ToolAudit
            return entitiesToTrack.Where(e => e.Properties.Any(p => p.IsTemporary))
                 .Select(e => new Tuple<EntityEntry, AuditLog>(
                     e,
                 new AuditLog()
                 {
                     TableName = GetTableName(e.Entity.ToString()),
                     Action = Enum.GetName(typeof(EntityState), e.State),
                     DateTime = DateTime.Now,
                     Username = this.httpContextAccessor?.HttpContext?.User?.Identity?.Name,
                     NewValues = JsonConvert.SerializeObject(e.Properties.Where(p => !p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty())
                 }
                 )).ToList();
        }

        async Task AuditTemporaryProperties(IEnumerable<Tuple<EntityEntry, AuditLog>> temporatyEntities)
        {
            if (temporatyEntities != null && temporatyEntities.Any())
            {
                await AuditLog.AddRangeAsync(
                temporatyEntities.ForEach(t => t.Item2.KeyValues = JsonConvert.SerializeObject(t.Item1.Properties.Where(p => p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue).NullIfEmpty()))
                    .Select(t => t.Item2)
                );
                await SaveChangesAsync();
            }
            await Task.CompletedTask;
        }

        private string GetTableName(string name)
        {
            
            switch (name)
            {
                case "Transportadora.Business.Models.Address":
                    return "ENDERECO";
                case "Transportadora.Business.Models.Bank":
                    return "BANCO";
                case "Transportadora.Business.Models.RequisicaoCompra":
                    return "REQUISICAOCOMPRA";
                case "Transportadora.Business.Models.Estoque":
                    return "ESTOQUE";
                case "Transportadora.Business.Models.BankAccount":
                    return "CONTABANCO";
                case "Transportadora.Business.Models.City":
                    return "CIDADE";
                case "Transportadora.Business.Models.Company":
                    return "EMPRESA";
                case "Transportadora.Business.Models.Customer":
                    return "CLIENTE";
                case "Transportadora.Business.Models.CostCenter":
                    return "CENTROCUSTO";
                case "Transportadora.Business.Models.DocumentType":
                    return "TIPODOCUMENTO";
                case "Transportadora.Business.Models.Employee":
                    return "FUNCIONARIO";
                case "Transportadora.Business.Models.Expense":
                    return "DESPESA";
                case "Transportadora.Business.Models.ExpenseFinancialSettlement":
                    return "ACERTODESPESA";
                case "Transportadora.Business.Models.ExpensePayment":
                    return "PAGAMENTODESPESA";
                case "Transportadora.Business.Models.ExpenseSupplier":
                    return "DESPESAFORNECEDOR";
                case "Transportadora.Business.Models.FinancialSettlement":
                    return "ACERTO";
                case "Transportadora.Business.Models.Fleet":
                    return "FROTA";
                case "Transportadora.Business.Models.Funcionality":
                    return "FUNCIONALIDADE";
                case "Transportadora.Business.Models.Insurance":
                    return "SEGURO";
                case "Transportadora.Business.Models.InsuranceCompany":
                    return "SEGURADORA";
                case "Transportadora.Business.Models.InsuranceSituation":
                    return "SITUACAOSEGURO";
                case "Transportadora.Business.Models.Invoice":
                    return "RECEITA";
                case "Transportadora.Business.Models.InvoicePayment":
                    return "RECEITAPAGAMENTO";
                case "Transportadora.Business.Models.Module":
                    return "MODULO";
                case "Transportadora.Business.Models.PaymentForm":
                    return "FORMAPAGAMENTO";
                case "Transportadora.Business.Models.PaymentMethod":
                    return "METODOPAGAMENTO";
                case "Transportadora.Business.Models.ProfileActionFuncionality":
                    return "PERFILACAOFUNCIONALIDADE";
                case "Transportadora.Business.Models.ProfileT":
                    return "PERFIL";
                case "Transportadora.Business.Models.Responsibility":
                    return "CARGO";
                case "Transportadora.Business.Models.State":
                    return "ESTADO";
                case "Transportadora.Business.Models.Supplier":
                    return "FORNECEDOR";
                case "Transportadora.Business.Models.User":
                    return "USUARIO";
                case "Transportadora.Business.Models.Vehicle":
                    return "VEICULO";
                case "Transportadora.Business.Models.VehicleType":
                    return "TIPOVEICULO";
                case "Transportadora.Business.Models.Parameter":
                    return "PARÂMETROS";
                default:
                    return name;
            }
        }
    }
}
