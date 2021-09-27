using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.Stock;

namespace WebApplication1.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Account_Sheet> Account_Sheet { get; set; }
        public DbSet<Account_Sheet_Detail> Account_Sheet_Details { get; set; }
        public DbSet<SpResult> SpResult { get; set; }
        public DbSet<SpAddNewSheetPrams> SpAddNewSheetPrams { get; set; }
        public DbSet<Bransh> Bransh { get; set; }
        public DbSet<users> users { get; set; }
        public DbSet<userPermCustom> userPermCustom { get; set; }
        public DbSet<users_perm> users_perm { set; get; }
        public DbSet<Programs> Programs { set; get; }
        public DbSet<forms> forms { set; get; }
        public DbSet<program_proprties> program_Proprties { set; get; }
        public DbSet<Account_Sheet_File> Account_Sheet_File { get; set; }
        public DbSet<AccountTree> AccountTree { get; set; }
        public DbSet<DetailsTrans> DetailsTrans { get; set; }
        public DbSet<TrailBalance> TrailBalance { get; set; }
        public DbSet<MizanBalance> MizanBalance { get; set; }
        public DbSet<AccountFinalBalance> AccountFinalBalance { get; set; }
        public DbSet<payments_types> payments_types { get; set; }

        public DbSet<Stock_InvType> Stock_InvType { get; set; }
        public DbSet<Stock_items> Stock_items { get; set; }
        public DbSet<Stock_ItemsGroup> Stock_ItemsGroup { get; set; }
        public DbSet<Stock_ItemsGroup_Details> Stock_ItemsGroup_Details { get; set; }
        public DbSet<Stock_Units> Stock_Units { get; set; }
        public DbSet<Stock_ShopCard> Stock_ShopCard { get; set; }
        public DbSet<Stock_InvMain> Stock_InvMain { get; set; }
        public DbSet<Stock_Settings> Stock_Settings { get; set; }
      //  public DbSet<Client> clients { get; set; }
        public DbSet<Stock_Items_Barcode> Stock_Items_Barcode { get; set; }
        public DbSet<Stock_ItemsAndUnits> Stock_ItemsAndUnits { get; set; }
        public DbSet<Stock_InvTransSearchByBarcode> Stock_InvTransSearchByBarcode { get; set; }
        public DbSet<Stock_InvMainDetails> Stock_InvMainDetails { get; set; }
        public DbSet<StockRep_ItemMovement> StockRep_ItemMovement { get; set; }
        public DbSet<StockRep_ItemsStock> StockRep_ItemsStock { get; set; }
        public DbSet<StockRep_RevAllInv> StockRep_RevAllInv { get; set; }
        public DbSet<StockRep_RevAllInv_Group> StockRep_RevAllInv_Group { get; set; }
        public DbSet<StockRep_RevAllInv_Group2> StockRep_RevAllInv_Group2 { get; set; }
        public DbSet<Stock_itemsSub> Stock_itemsSub { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<users_perm>()
                .HasKey(c => new { c.id, c.prog_id, c.form_id, c.perm_type_id });
        }
    }
}
