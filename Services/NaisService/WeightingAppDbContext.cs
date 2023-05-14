using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NaisService.WeightingAppDataBaseModels;
using SharedLibrary.AppSettings;

namespace NaisService;

public partial class WeightingAppDbContext : DbContext
{
    public WeightingAppDbContext()
    {
    }

    public WeightingAppDbContext(DbContextOptions<WeightingAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditWeighing> AuditWeighings { get; set; }

    public virtual DbSet<AuditWeightingPhoto> AuditWeightingPhotos { get; set; }

    public virtual DbSet<ComDirInvoiceDirStorage> ComDirInvoiceDirStorages { get; set; }

    public virtual DbSet<ComDirTransportDriverTrailer> ComDirTransportDriverTrailers { get; set; }

    public virtual DbSet<ComDirUserGroupsAccessPermission> ComDirUserGroupsAccessPermissions { get; set; }

    public virtual DbSet<ComKfkh> ComKfkhs { get; set; }

    public virtual DbSet<DirAdditionalCharacteristic> DirAdditionalCharacteristics { get; set; }

    public virtual DbSet<DirAxleRealWeight> DirAxleRealWeights { get; set; }

    public virtual DbSet<DirCargo> DirCargos { get; set; }

    public virtual DbSet<DirCarrier> DirCarriers { get; set; }

    public virtual DbSet<DirCounterparty> DirCounterparties { get; set; }

    public virtual DbSet<DirDefaultSettingsForPrint> DirDefaultSettingsForPrints { get; set; }

    public virtual DbSet<DirDriver> DirDrivers { get; set; }

    public virtual DbSet<DirExportFile> DirExportFiles { get; set; }

    public virtual DbSet<DirFund> DirFunds { get; set; }

    public virtual DbSet<DirInvoice> DirInvoices { get; set; }

    public virtual DbSet<DirKfkhCombine> DirKfkhCombines { get; set; }

    public virtual DbSet<DirKfkhCombiner> DirKfkhCombiners { get; set; }

    public virtual DbSet<DirKfkhPlantation> DirKfkhPlantations { get; set; }

    public virtual DbSet<DirLoadPoint> DirLoadPoints { get; set; }

    public virtual DbSet<DirPlacement> DirPlacements { get; set; }

    public virtual DbSet<DirStorage> DirStorages { get; set; }

    public virtual DbSet<DirTrailer> DirTrailers { get; set; }

    public virtual DbSet<DirTrailerModelAxleCharacter> DirTrailerModelAxleCharacters { get; set; }

    public virtual DbSet<DirTransport> DirTransports { get; set; }

    public virtual DbSet<DirTransportModelAxleCharacter> DirTransportModelAxleCharacters { get; set; }

    public virtual DbSet<DirTypeOfCarSuspension> DirTypeOfCarSuspensions { get; set; }

    public virtual DbSet<DirTypeOfCargo> DirTypeOfCargos { get; set; }

    public virtual DbSet<DirTypeOfOperation> DirTypeOfOperations { get; set; }

    public virtual DbSet<DirTypeOfPack> DirTypeOfPacks { get; set; }

    public virtual DbSet<DirTypeOfTrailer> DirTypeOfTrailers { get; set; }

    public virtual DbSet<DirTypeOfTrailerModel> DirTypeOfTrailerModels { get; set; }

    public virtual DbSet<DirTypeOfTransport> DirTypeOfTransports { get; set; }

    public virtual DbSet<DirTypeOfTransportModel> DirTypeOfTransportModels { get; set; }

    public virtual DbSet<DirUploadPoint> DirUploadPoints { get; set; }

    public virtual DbSet<DirUser> DirUsers { get; set; }

    public virtual DbSet<DirUserGroup> DirUserGroups { get; set; }

    public virtual DbSet<DirWeightRoom> DirWeightRooms { get; set; }

    public virtual DbSet<InvoiceDocument> InvoiceDocuments { get; set; }

    public virtual DbSet<InvoiceTorg12Document> InvoiceTorg12Documents { get; set; }

    public virtual DbSet<LogStartTerminalInfo> LogStartTerminalInfos { get; set; }

    public virtual DbSet<ReferenceBooksTable> ReferenceBooksTables { get; set; }

    public virtual DbSet<RegWeight> RegWeights { get; set; }

    public virtual DbSet<RusNameField> RusNameFields { get; set; }

    public virtual DbSet<SettingBackup> SettingBackups { get; set; }

    public virtual DbSet<SettingsApp> SettingsApps { get; set; }

    public virtual DbSet<SettingsAuditLoad> SettingsAuditLoads { get; set; }

    public virtual DbSet<SettingsAxleLoad> SettingsAxleLoads { get; set; }

    public virtual DbSet<SettingsC1integrator> SettingsC1integrators { get; set; }

    public virtual DbSet<SettingsEmail> SettingsEmails { get; set; }

    public virtual DbSet<SettingsExport> SettingsExports { get; set; }

    public virtual DbSet<SettingsIntegration> SettingsIntegrations { get; set; }

    public virtual DbSet<SettingsKfkh> SettingsKfkhs { get; set; }

    public virtual DbSet<SettingsPreAxleReport> SettingsPreAxleReports { get; set; }

    public virtual DbSet<SettingsRecognitionModule> SettingsRecognitionModules { get; set; }

    public virtual DbSet<SettingsRecognitionMpixel> SettingsRecognitionMpixels { get; set; }

    public virtual DbSet<SettingsScoreboard> SettingsScoreboards { get; set; }

    public virtual DbSet<SettingsServerReport> SettingsServerReports { get; set; }

    public virtual DbSet<SettingsSkud> SettingsSkuds { get; set; }

    public virtual DbSet<SettingsSpeech> SettingsSpeeches { get; set; }

    public virtual DbSet<SettingsTerminal> SettingsTerminals { get; set; }

    public virtual DbSet<SettingsUsbkeyReader> SettingsUsbkeyReaders { get; set; }

    public virtual DbSet<SettingsVideoSurveillance> SettingsVideoSurveillances { get; set; }

    public virtual DbSet<SystemDirAccessPermission> SystemDirAccessPermissions { get; set; }

    public virtual DbSet<SystemDirDirectionTypeOperation> SystemDirDirectionTypeOperations { get; set; }

    public virtual DbSet<SystemDirIdentifierState> SystemDirIdentifierStates { get; set; }

    public virtual DbSet<SystemDirKindOfPacking> SystemDirKindOfPackings { get; set; }

    public virtual DbSet<SystemDirTypeOfCarriage> SystemDirTypeOfCarriages { get; set; }

    public virtual DbSet<SystemDirTypeOfCarriageDocument> SystemDirTypeOfCarriageDocuments { get; set; }

    public virtual DbSet<SystemDirTypeOfPass> SystemDirTypeOfPasses { get; set; }

    public virtual DbSet<SystemDirTypeOfRegweight> SystemDirTypeOfRegweights { get; set; }

    public virtual DbSet<Ttndocument> Ttndocuments { get; set; }

    public virtual DbSet<UnrecWeighing> UnrecWeighings { get; set; }

    public virtual DbSet<UnrecWeightingPhoto> UnrecWeightingPhotos { get; set; }

    public virtual DbSet<ViewComTrailerModelsCharacter> ViewComTrailerModelsCharacters { get; set; }

    public virtual DbSet<ViewComTransportModelsCharacter> ViewComTransportModelsCharacters { get; set; }

    public virtual DbSet<Weighing> Weighings { get; set; }

    public virtual DbSet<WeighingInfo> WeighingInfos { get; set; }

    public virtual DbSet<WeightingPhoto> WeightingPhotos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("AppSettings.json")
            .Build();

        var settings = config.GetSection("Settings").Get<Settings>();
        optionsBuilder
        .UseSqlServer(settings.NaisConnectionString)
        .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditWeighing>(entity =>
        {
            entity.ToTable("audit_weighing");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TansportNum).HasMaxLength(45);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.AuditWeighings)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditWeighing_DirUsers");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.AuditWeighings)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .HasConstraintName("FK_audit_WeightRoom");
        });

        modelBuilder.Entity<AuditWeightingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__audit_we__3213E83F09E81C92");

            entity.ToTable("audit_weighting_photo");

            entity.HasIndex(e => e.Id, "UQ__audit_we__3213E83E1493DAE9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdAudit).HasColumnName("id_audit");
            entity.Property(e => e.IdSettingsVideoSurveillance).HasColumnName("id_Settings_VideoSurveillance");

            entity.HasOne(d => d.IdAuditNavigation).WithMany(p => p.AuditWeightingPhotos)
                .HasForeignKey(d => d.IdAudit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_audit_photo_weighing");

            entity.HasOne(d => d.IdSettingsVideoSurveillanceNavigation).WithMany(p => p.AuditWeightingPhotos)
                .HasForeignKey(d => d.IdSettingsVideoSurveillance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_audit_weighting_photo_Settings_VideoSurveillance");
        });

        modelBuilder.Entity<ComDirInvoiceDirStorage>(entity =>
        {
            entity.ToTable("com_dir_invoice_dir_storage");

            entity.HasIndex(e => e.IdDirInvoice, "IX_com_dir_invoice_dir_storage_id_dir_invoice").HasFillFactor(80);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DocBrutto).HasColumnName("Doc_brutto");
            entity.Property(e => e.DocNetto).HasColumnName("Doc_netto");
            entity.Property(e => e.DocTara).HasColumnName("Doc_tara");
            entity.Property(e => e.IdDirCargo).HasColumnName("id_dir_cargo");
            entity.Property(e => e.IdDirInvoice).HasColumnName("id_dir_invoice");
            entity.Property(e => e.IdDirLoadPoint).HasColumnName("id_dir_loadPoint");
            entity.Property(e => e.IdDirPlacement).HasColumnName("id_dir_placement");
            entity.Property(e => e.IdDirPlacementRec).HasColumnName("id_dir_placement_rec");
            entity.Property(e => e.IdDirStrorage).HasColumnName("id_dir_strorage");
            entity.Property(e => e.IdDirStrorageRec).HasColumnName("id_dir_strorage_rec");
            entity.Property(e => e.IdDirTypeOfCargo).HasColumnName("id_dir_typeOfCargo");
            entity.Property(e => e.IdDirTypeOfPack).HasColumnName("id_dir_typeOfPack");
            entity.Property(e => e.IdDirUploadPoint).HasColumnName("id_dir_uploadPoint");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdDirCargoNavigation).WithMany(p => p.ComDirInvoiceDirStorages)
                .HasForeignKey(d => d.IdDirCargo)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_cargo");

            entity.HasOne(d => d.IdDirInvoiceNavigation).WithMany(p => p.ComDirInvoiceDirStorages)
                .HasForeignKey(d => d.IdDirInvoice)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_invoice");

            entity.HasOne(d => d.IdDirLoadPointNavigation).WithMany(p => p.ComDirInvoiceDirStorages)
                .HasForeignKey(d => d.IdDirLoadPoint)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_loadPoint");

            entity.HasOne(d => d.IdDirPlacementNavigation).WithMany(p => p.ComDirInvoiceDirStorageIdDirPlacementNavigations)
                .HasForeignKey(d => d.IdDirPlacement)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_placement");

            entity.HasOne(d => d.IdDirPlacementRecNavigation).WithMany(p => p.ComDirInvoiceDirStorageIdDirPlacementRecNavigations)
                .HasForeignKey(d => d.IdDirPlacementRec)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_placement_rec");

            entity.HasOne(d => d.IdDirStrorageNavigation).WithMany(p => p.ComDirInvoiceDirStorageIdDirStrorageNavigations)
                .HasForeignKey(d => d.IdDirStrorage)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_storage");

            entity.HasOne(d => d.IdDirStrorageRecNavigation).WithMany(p => p.ComDirInvoiceDirStorageIdDirStrorageRecNavigations)
                .HasForeignKey(d => d.IdDirStrorageRec)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_strorage_rec");

            entity.HasOne(d => d.IdDirTypeOfCargoNavigation).WithMany(p => p.ComDirInvoiceDirStorages)
                .HasForeignKey(d => d.IdDirTypeOfCargo)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_typeOfCargo");

            entity.HasOne(d => d.IdDirTypeOfPackNavigation).WithMany(p => p.ComDirInvoiceDirStorages)
                .HasForeignKey(d => d.IdDirTypeOfPack)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_typeOfPack");

            entity.HasOne(d => d.IdDirUploadPointNavigation).WithMany(p => p.ComDirInvoiceDirStorages)
                .HasForeignKey(d => d.IdDirUploadPoint)
                .HasConstraintName("FK_com_dir_invoice_dir_storage_dir_UploadPoint");
        });

        modelBuilder.Entity<ComDirTransportDriverTrailer>(entity =>
        {
            entity.ToTable("com_dir_transport_driver_trailer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirDriver).HasColumnName("id_dir_driver");
            entity.Property(e => e.IdDirTrailer).HasColumnName("id_dir_trailer");
            entity.Property(e => e.IdDirTransport).HasColumnName("id_dir_transport");
            entity.Property(e => e.IdDirTypeOfTrailer).HasColumnName("id_dir_typeOfTrailer");
            entity.Property(e => e.IdDirTypeOfTransport).HasColumnName("id_dir_typeOfTransport");
            entity.Property(e => e.IdTrailerModel).HasColumnName("id_TrailerModel");
            entity.Property(e => e.IdTransportModel).HasColumnName("id_TransportModel");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RecognTrailerNumFirstWeight).HasMaxLength(45);
            entity.Property(e => e.RecognTrailerNumSecondWeight).HasMaxLength(45);
            entity.Property(e => e.RecognTransportNumFirstWeight).HasMaxLength(45);
            entity.Property(e => e.RecognTransportNumSecondWeight).HasMaxLength(45);

            entity.HasOne(d => d.IdDirDriverNavigation).WithMany(p => p.ComDirTransportDriverTrailers)
                .HasForeignKey(d => d.IdDirDriver)
                .HasConstraintName("FK_com_dir_transport_driver_trailer_dir_drivers1");

            entity.HasOne(d => d.IdDirTrailerNavigation).WithMany(p => p.ComDirTransportDriverTrailers)
                .HasForeignKey(d => d.IdDirTrailer)
                .HasConstraintName("FK_com_dir_transport_driver_trailer_dir_trailer");

            entity.HasOne(d => d.IdDirTransportNavigation).WithMany(p => p.ComDirTransportDriverTrailers)
                .HasForeignKey(d => d.IdDirTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_com_dir_transport_driver_trailer_dir_transport");

            entity.HasOne(d => d.IdDirTypeOfTrailerNavigation).WithMany(p => p.ComDirTransportDriverTrailers)
                .HasForeignKey(d => d.IdDirTypeOfTrailer)
                .HasConstraintName("FK_com_dir_transport_driver_trailer_dir_typeOfTrailer");

            entity.HasOne(d => d.IdDirTypeOfTransportNavigation).WithMany(p => p.ComDirTransportDriverTrailers)
                .HasForeignKey(d => d.IdDirTypeOfTransport)
                .HasConstraintName("FK_com_dir_transport_driver_trailer_dir_typeOfTransport");

            entity.HasOne(d => d.IdTrailerModelNavigation).WithMany(p => p.ComDirTransportDriverTrailers)
                .HasForeignKey(d => d.IdTrailerModel)
                .HasConstraintName("FK_com_dir_transport_driver_trailer_dir_TypeOfTrailerModels");

            entity.HasOne(d => d.IdTransportModelNavigation).WithMany(p => p.ComDirTransportDriverTrailers)
                .HasForeignKey(d => d.IdTransportModel)
                .HasConstraintName("FK_com_dir_transport_driver_trailer_dir_TypeOfTransportModels");
        });

        modelBuilder.Entity<ComDirUserGroupsAccessPermission>(entity =>
        {
            entity.ToTable("com_dir_UserGroups_AccessPermissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDirUserGroup).HasColumnName("id_dir_UserGroup");
            entity.Property(e => e.IdSystemDirAccessPermission).HasColumnName("id_system_dir_AccessPermission");

            entity.HasOne(d => d.IdDirUserGroupNavigation).WithMany(p => p.ComDirUserGroupsAccessPermissions)
                .HasForeignKey(d => d.IdDirUserGroup)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_com_dir_UserGroups_AccessPermissions_dir_UserGroups");

            entity.HasOne(d => d.IdSystemDirAccessPermissionNavigation).WithMany(p => p.ComDirUserGroupsAccessPermissions)
                .HasForeignKey(d => d.IdSystemDirAccessPermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_com_dir_UserGroups_AccessPermissions_system_dir_AccessPermissions");
        });

        modelBuilder.Entity<ComKfkh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_kfkh");

            entity.ToTable("com_kfkh");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirCombine).HasColumnName("id_dir_combine");
            entity.Property(e => e.IdDirCombiner).HasColumnName("id_dir_combiner");
            entity.Property(e => e.IdDirPlantation).HasColumnName("id_dir_plantation");
            entity.Property(e => e.IdWeighigInfo).HasColumnName("id_weighig_info");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdDirCombineNavigation).WithMany(p => p.ComKfkhs)
                .HasForeignKey(d => d.IdDirCombine)
                .HasConstraintName("FK_dir_kfkh_dir_kfkh_combine1");

            entity.HasOne(d => d.IdDirCombinerNavigation).WithMany(p => p.ComKfkhs)
                .HasForeignKey(d => d.IdDirCombiner)
                .HasConstraintName("FK_dir_kfkh_dir_kfkh_combiner");

            entity.HasOne(d => d.IdDirPlantationNavigation).WithMany(p => p.ComKfkhs)
                .HasForeignKey(d => d.IdDirPlantation)
                .HasConstraintName("FK_dir_kfkh_dir_kfkh_plantation");

            entity.HasOne(d => d.IdWeighigInfoNavigation).WithMany(p => p.ComKfkhs)
                .HasForeignKey(d => d.IdWeighigInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_com_kfkh_weighing_info");
        });

        modelBuilder.Entity<DirAdditionalCharacteristic>(entity =>
        {
            entity.ToTable("dir_additional_characteristics");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ReasonOfMarriage).HasMaxLength(50);
        });

        modelBuilder.Entity<DirAxleRealWeight>(entity =>
        {
            entity.ToTable("dir_AxleRealWeights");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirTypeOfTrailerModels).HasColumnName("id_dir_typeOfTrailerModels");
            entity.Property(e => e.IdDirTypeOfTransportModels).HasColumnName("id_dir_typeOfTransportModels");
            entity.Property(e => e.IdWeighings).HasColumnName("id_weighings");
            entity.Property(e => e.Weight).HasMaxLength(150);
        });

        modelBuilder.Entity<DirCargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_cargo");

            entity.ToTable("dir_cargo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.PriceTonne).HasColumnName("Price_tonne");
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SellingPrice).HasColumnName("Selling_price");
        });

        modelBuilder.Entity<DirCarrier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Interpreters");

            entity.ToTable("dir_carrier");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Bik)
                .HasMaxLength(90)
                .HasColumnName("BIK");
            entity.Property(e => e.CheckingAccount).HasMaxLength(30);
            entity.Property(e => e.CorrespondentAccount).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DirPosition)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("dirPosition");
            entity.Property(e => e.FiochiefAccountant)
                .HasMaxLength(80)
                .HasColumnName("FIOChiefAccountant");
            entity.Property(e => e.Fiodirector)
                .HasMaxLength(80)
                .HasColumnName("FIODirector");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Inn)
                .HasMaxLength(90)
                .HasColumnName("INN");
            entity.Property(e => e.Kpp)
                .HasMaxLength(90)
                .HasColumnName("KPP");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Okpo)
                .HasMaxLength(90)
                .HasColumnName("OKPO");
            entity.Property(e => e.Phone).HasMaxLength(30);
        });

        modelBuilder.Entity<DirCounterparty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Counterparties");

            entity.ToTable("dir_counterparty");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualAddress).HasMaxLength(255);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AttorneyDate)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("attorneyDate");
            entity.Property(e => e.AttorneyFrom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("attorneyFrom");
            entity.Property(e => e.AttorneyNum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("attorneyNum");
            entity.Property(e => e.Bik)
                .HasMaxLength(90)
                .HasColumnName("BIK");
            entity.Property(e => e.CheckingAccount).HasMaxLength(30);
            entity.Property(e => e.CorrespondentAccount).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DirPosition)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("dirPosition");
            entity.Property(e => e.FiochiefAccountant)
                .HasMaxLength(80)
                .HasColumnName("FIOChiefAccountant");
            entity.Property(e => e.Fiodirector)
                .HasMaxLength(80)
                .HasColumnName("FIODirector");
            entity.Property(e => e.FiowarehouseManager)
                .HasMaxLength(80)
                .HasColumnName("FIOWarehouseManager");
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Inn)
                .HasMaxLength(90)
                .HasColumnName("INN");
            entity.Property(e => e.Kpp)
                .HasMaxLength(90)
                .HasColumnName("KPP");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Okpo)
                .HasMaxLength(90)
                .HasColumnName("OKPO");
            entity.Property(e => e.Phone).HasMaxLength(30);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<DirDefaultSettingsForPrint>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dir_defaultSettingsForPrint");

            entity.Property(e => e.FieldValue)
                .HasColumnType("text")
                .HasColumnName("fieldValue");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeOfField)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("typeOfField");
        });

        modelBuilder.Entity<DirDriver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_drivers");

            entity.ToTable("dir_driver");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DriverLicenseNumber).HasMaxLength(6);
            entity.Property(e => e.DriverLicenseSeries).HasMaxLength(4);
            entity.Property(e => e.ExpirationDate).HasColumnType("date");
            entity.Property(e => e.IdOfPassport).HasMaxLength(6);
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("identityNumber");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("INN");
            entity.Property(e => e.IssuedBy).HasMaxLength(255);
            entity.Property(e => e.IssuedWhen).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ResidenceAddress).HasMaxLength(255);
            entity.Property(e => e.SeriesOfPassport).HasMaxLength(4);
        });

        modelBuilder.Entity<DirExportFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_exportFileinf");

            entity.ToTable("dir_ExportFile");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BeginTime)
                .HasColumnType("datetime")
                .HasColumnName("beginTime");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.FileName).HasMaxLength(150);
            entity.Property(e => e.PatternName).HasMaxLength(150);
        });

        modelBuilder.Entity<DirFund>(entity =>
        {
            entity.ToTable("dir_fund");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<DirInvoice>(entity =>
        {
            entity.ToTable("dir_invoice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.IdDirCarrier).HasColumnName("id_dir_carrier");
            entity.Property(e => e.IdDirCounterparty).HasColumnName("id_dir_counterparty");
            entity.Property(e => e.IdDirTypeOfOperation).HasColumnName("id_dir_typeOfOperation");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Time).HasPrecision(0);

            entity.HasOne(d => d.IdDirCarrierNavigation).WithMany(p => p.DirInvoices)
                .HasForeignKey(d => d.IdDirCarrier)
                .HasConstraintName("FK_dir_invoice_dir_carrier");

            entity.HasOne(d => d.IdDirCounterpartyNavigation).WithMany(p => p.DirInvoices)
                .HasForeignKey(d => d.IdDirCounterparty)
                .HasConstraintName("FK_dir_invoice_dir_сounterparty");

            entity.HasOne(d => d.IdDirTypeOfOperationNavigation).WithMany(p => p.DirInvoices)
                .HasForeignKey(d => d.IdDirTypeOfOperation)
                .HasConstraintName("FK_dir_invoice_dir_typeOfOperation");
        });

        modelBuilder.Entity<DirKfkhCombine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_combine");

            entity.ToTable("dir_kfkh_combine");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Make).HasMaxLength(45);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<DirKfkhCombiner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_combineOperator");

            entity.ToTable("dir_kfkh_combiner");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DriverLicenseNumber).HasMaxLength(6);
            entity.Property(e => e.DriverLicenseSeries).HasMaxLength(4);
            entity.Property(e => e.ExpirationDate).HasColumnType("date");
            entity.Property(e => e.IdOfPassport).HasMaxLength(6);
            entity.Property(e => e.IssuedBy).HasMaxLength(255);
            entity.Property(e => e.IssuedWhen).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ResidenceAddress).HasMaxLength(255);
            entity.Property(e => e.SeriesOfPassport).HasMaxLength(4);
        });

        modelBuilder.Entity<DirKfkhPlantation>(entity =>
        {
            entity.ToTable("dir_kfkh_plantation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<DirLoadPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_bootPoint");

            entity.ToTable("dir_loadPoint");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DirPlacement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_placementRecipient");

            entity.ToTable("dir_placement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<DirStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_storageRecipient");

            entity.ToTable("dir_storage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Company).HasMaxLength(45);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SeniorStorekeeper).HasMaxLength(100);
        });

        modelBuilder.Entity<DirTrailer>(entity =>
        {
            entity.ToTable("dir_trailer");

            entity.HasIndex(e => e.Name, "IX_dir_trailer_Name");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<DirTrailerModelAxleCharacter>(entity =>
        {
            entity.ToTable("dir_TrailerModelAxleCharacters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdTypeOfTsmodels).HasColumnName("id_TypeOfTSModels");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdTypeOfTsmodelsNavigation).WithMany(p => p.DirTrailerModelAxleCharacters)
                .HasForeignKey(d => d.IdTypeOfTsmodels)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dir_TrailerModelAxleCharacters_dir_typeOfTrailerModels");
        });

        modelBuilder.Entity<DirTransport>(entity =>
        {
            entity.ToTable("dir_transport");

            entity.HasIndex(e => e.Name, "IX_dir_transport_Name");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<DirTransportModelAxleCharacter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_AxleModelCharacters");

            entity.ToTable("dir_TransportModelAxleCharacters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdTypeOfTsmodels).HasColumnName("id_TypeOfTSModels");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdTypeOfTsmodelsNavigation).WithMany(p => p.DirTransportModelAxleCharacters)
                .HasForeignKey(d => d.IdTypeOfTsmodels)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dir_TransportModelAxleCharacters_dir_typeOfTransportModels");
        });

        modelBuilder.Entity<DirTypeOfCarSuspension>(entity =>
        {
            entity.ToTable("dir_typeOfCarSuspension");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DirTypeOfCargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_typeOFCargo");

            entity.ToTable("dir_typeOfCargo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirCargo).HasColumnName("id_dir_cargo");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdDirCargoNavigation).WithMany(p => p.DirTypeOfCargos)
                .HasForeignKey(d => d.IdDirCargo)
                .HasConstraintName("FK_dir_typeOfCargo_dir_Cargo");
        });

        modelBuilder.Entity<DirTypeOfOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_businessTransaction");

            entity.ToTable("dir_typeOfOperation", tb => tb.HasTrigger("dir_typeOfOperation_Edit_Insert_Delete"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdSystemDirDirectionTypeOperation).HasColumnName("id_system_dir_DirectionTypeOperation");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(90);

            entity.HasOne(d => d.IdSystemDirDirectionTypeOperationNavigation).WithMany(p => p.DirTypeOfOperations)
                .HasForeignKey(d => d.IdSystemDirDirectionTypeOperation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dir_typeOfOperation_dir_DirectionTypeOperation");
        });

        modelBuilder.Entity<DirTypeOfPack>(entity =>
        {
            entity.ToTable("dir_typeOfPack");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<DirTypeOfTrailer>(entity =>
        {
            entity.ToTable("dir_typeOfTrailer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<DirTypeOfTrailerModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_TypeOfTrailerModels");

            entity.ToTable("dir_typeOfTrailerModels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirTypeOfCarSuspension).HasColumnName("id_dir_typeOfCarSuspension");
            entity.Property(e => e.IdDirTypeOfTrailer).HasColumnName("id_dir_TypeOfTrailer");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdDirTypeOfCarSuspensionNavigation).WithMany(p => p.DirTypeOfTrailerModels)
                .HasForeignKey(d => d.IdDirTypeOfCarSuspension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dir_typeOfTrailerModels_dir_typeOfCarSuspension");

            entity.HasOne(d => d.IdDirTypeOfTrailerNavigation).WithMany(p => p.DirTypeOfTrailerModels)
                .HasForeignKey(d => d.IdDirTypeOfTrailer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dir_typeOfTrailerModels_dir_typeOfTrailer");
        });

        modelBuilder.Entity<DirTypeOfTransport>(entity =>
        {
            entity.ToTable("dir_typeOfTransport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<DirTypeOfTransportModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dir_TypeOfTransportModels");

            entity.ToTable("dir_typeOfTransportModels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirTypeOfCarSuspension).HasColumnName("id_dir_typeOfCarSuspension");
            entity.Property(e => e.IdDirTypeOfTransport).HasColumnName("id_dir_TypeOfTransport");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdDirTypeOfCarSuspensionNavigation).WithMany(p => p.DirTypeOfTransportModels)
                .HasForeignKey(d => d.IdDirTypeOfCarSuspension)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dir_typeOfTransportModels_dir_typeOfCarSuspension");

            entity.HasOne(d => d.IdDirTypeOfTransportNavigation).WithMany(p => p.DirTypeOfTransportModels)
                .HasForeignKey(d => d.IdDirTypeOfTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_dir_typeOfTransportModels_dir_typeOfTransport");
        });

        modelBuilder.Entity<DirUploadPoint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_unloadingPoints");

            entity.ToTable("dir_uploadPoint");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DirUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__dir_User__3213E83F70917BB9");

            entity.ToTable("dir_Users");

            entity.HasIndex(e => e.Id, "UQ__dir_User__3213E83EDEB14632").IsUnique();

            entity.HasIndex(e => e.Name, "UQ_dir_Users_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirUserGroups).HasColumnName("id_dir_UserGroups");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdDirUserGroupsNavigation).WithMany(p => p.DirUsers)
                .HasForeignKey(d => d.IdDirUserGroups)
                .HasConstraintName("dir_Users_dir_UserGroups_FK");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.DirUsers)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("logonusers_dir_WeightRoom_FK");
        });

        modelBuilder.Entity<DirUserGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GroupOfUsers");

            entity.ToTable("dir_UserGroups", tb => tb.HasTrigger("dir_UserGroups_Edit_Insert_Delete"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<DirWeightRoom>(entity =>
        {
            entity.ToTable("dir_WeightRoom", tb => tb.HasTrigger("trigger_CreateWeightRoom"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<InvoiceDocument>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountantName).HasMaxLength(255);
            entity.Property(e => e.BusinessmanName).HasMaxLength(255);
            entity.Property(e => e.ChiefName).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.DocumentName).HasMaxLength(255);
            entity.Property(e => e.IdWeighing).HasColumnName("id_weighing");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(55);
            entity.Property(e => e.Nds).HasColumnName("NDS");
            entity.Property(e => e.RegIpcertificate)
                .HasMaxLength(255)
                .HasColumnName("RegIPCertificate");

            entity.HasOne(d => d.IdWeighingNavigation).WithMany(p => p.InvoiceDocuments)
                .HasForeignKey(d => d.IdWeighing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceDocuments_weighing");
        });

        modelBuilder.Entity<InvoiceTorg12Document>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountantName).HasMaxLength(255);
            entity.Property(e => e.CargoPickUpName).HasMaxLength(255);
            entity.Property(e => e.CargoPickUpWorkAs).HasMaxLength(255);
            entity.Property(e => e.CargoReleaseName).HasMaxLength(255);
            entity.Property(e => e.CargoReleaseWorkAs).HasMaxLength(255);
            entity.Property(e => e.ConsigneeName).HasMaxLength(255);
            entity.Property(e => e.ConsigneeWorkAs).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Foundation).HasMaxLength(255);
            entity.Property(e => e.IdWeighing).HasColumnName("id_weighing");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(55);
            entity.Property(e => e.Nds).HasColumnName("NDS");
            entity.Property(e => e.ProxyDate).HasColumnType("date");
            entity.Property(e => e.ProxyGivenBy).HasMaxLength(255);
            entity.Property(e => e.ProxyName).HasMaxLength(255);
            entity.Property(e => e.ShipperName).HasMaxLength(255);
            entity.Property(e => e.ShipperWorkAs).HasMaxLength(255);
            entity.Property(e => e.StructName).HasMaxLength(255);

            entity.HasOne(d => d.IdWeighingNavigation).WithMany(p => p.InvoiceTorg12Documents)
                .HasForeignKey(d => d.IdWeighing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceTorg12Documents_weighing");
        });

        modelBuilder.Entity<LogStartTerminalInfo>(entity =>
        {
            entity.ToTable("log_StartTerminalInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasMaxLength(10);
            entity.Property(e => e.DateVerificationProtocolFormation).HasMaxLength(10);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Time).HasMaxLength(5);
        });

        modelBuilder.Entity<ReferenceBooksTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ReferenceBooks");

            entity.ToTable("ReferenceBooksTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessTag)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.NameTable)
                .HasMaxLength(150)
                .HasColumnName("Name_Table");
        });

        modelBuilder.Entity<RegWeight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_reg_weight_id");

            entity.ToTable("reg_weight");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DatePassExpiry).HasColumnType("date");
            entity.Property(e => e.DocBrutto).HasColumnName("Doc_brutto");
            entity.Property(e => e.DocNetto).HasColumnName("Doc_netto");
            entity.Property(e => e.DocTara).HasColumnName("Doc_tara");
            entity.Property(e => e.DocumentId).HasMaxLength(100);
            entity.Property(e => e.IdDirAdditionalCharacteristics).HasColumnName("id_dir_additional_characteristics");
            entity.Property(e => e.IdDirCargo).HasColumnName("id_dir_cargo");
            entity.Property(e => e.IdDirCarrier).HasColumnName("id_dir_carrier");
            entity.Property(e => e.IdDirCounterparty).HasColumnName("id_dir_counterparty");
            entity.Property(e => e.IdDirDriver).HasColumnName("id_dir_driver");
            entity.Property(e => e.IdDirFund).HasColumnName("id_dir_fund");
            entity.Property(e => e.IdDirKfkhPlantation).HasColumnName("id_dir_kfkh_plantation");
            entity.Property(e => e.IdDirPlacement).HasColumnName("id_dir_placement");
            entity.Property(e => e.IdDirPlacementRec).HasColumnName("id_dir_placement_rec");
            entity.Property(e => e.IdDirStorage).HasColumnName("id_dir_storage");
            entity.Property(e => e.IdDirStorageRec).HasColumnName("id_dir_storage_rec");
            entity.Property(e => e.IdDirTrailer).HasColumnName("id_dir_trailer");
            entity.Property(e => e.IdDirTransport).HasColumnName("id_dir_transport");
            entity.Property(e => e.IdDirTypeOfCargo).HasColumnName("id_dir_typeOfCargo");
            entity.Property(e => e.IdDirTypeOfOperation).HasColumnName("id_dir_typeOfOperation");
            entity.Property(e => e.IdDirTypeOfTrailer).HasColumnName("id_dir_typeOfTrailer");
            entity.Property(e => e.IdDirTypeOfTrailerModel).HasColumnName("id_dirTypeOfTrailerModel");
            entity.Property(e => e.IdDirTypeOfTransport).HasColumnName("id_dir_typeOfTransport");
            entity.Property(e => e.IdDirTypeOfTransportModels).HasColumnName("id_dir_TypeOfTransportModels");
            entity.Property(e => e.IdSystemDirIdentifierState).HasColumnName("id_system_dir_identifierState");
            entity.Property(e => e.IdSystemDirTypeOfPass).HasColumnName("id_system_dir_typeOfPass");
            entity.Property(e => e.IdSystemDirTypeOfRegweight).HasColumnName("id_system_dir_typeOfRegweight");
            entity.Property(e => e.InvoiceName).HasMaxLength(45);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.PassEnabled)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.TimePassExpiry).HasPrecision(0);

            entity.HasOne(d => d.IdDirAdditionalCharacteristicsNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirAdditionalCharacteristics)
                .HasConstraintName("FK_reg_weight_dir_additional_characteristics");

            entity.HasOne(d => d.IdDirCargoNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirCargo)
                .HasConstraintName("FK_reg_weight_dir_cargo");

            entity.HasOne(d => d.IdDirCarrierNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirCarrier)
                .HasConstraintName("FK_reg_weight_dir_carrier");

            entity.HasOne(d => d.IdDirCounterpartyNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirCounterparty)
                .HasConstraintName("FK_reg_weight_dir_сounterparty");

            entity.HasOne(d => d.IdDirDriverNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirDriver)
                .HasConstraintName("FK_reg_weight_dir_driver");

            entity.HasOne(d => d.IdDirFundNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirFund)
                .HasConstraintName("FK_reg_weight_dir_fund");

            entity.HasOne(d => d.IdDirKfkhPlantationNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirKfkhPlantation)
                .HasConstraintName("FK_reg_weight_dir_kfkh_plantation");

            entity.HasOne(d => d.IdDirPlacementNavigation).WithMany(p => p.RegWeightIdDirPlacementNavigations)
                .HasForeignKey(d => d.IdDirPlacement)
                .HasConstraintName("FK_reg_weight_dir_placement");

            entity.HasOne(d => d.IdDirPlacementRecNavigation).WithMany(p => p.RegWeightIdDirPlacementRecNavigations)
                .HasForeignKey(d => d.IdDirPlacementRec)
                .HasConstraintName("FK_reg_weight_dir_placement_rec");

            entity.HasOne(d => d.IdDirStorageNavigation).WithMany(p => p.RegWeightIdDirStorageNavigations)
                .HasForeignKey(d => d.IdDirStorage)
                .HasConstraintName("FK_reg_weight_dir_storage");

            entity.HasOne(d => d.IdDirStorageRecNavigation).WithMany(p => p.RegWeightIdDirStorageRecNavigations)
                .HasForeignKey(d => d.IdDirStorageRec)
                .HasConstraintName("FK_reg_weight_dir_storage_rec");

            entity.HasOne(d => d.IdDirTrailerNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTrailer)
                .HasConstraintName("FK_reg_weight_dir_trailer");

            entity.HasOne(d => d.IdDirTransportNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTransport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reg_weight_dir_transport");

            entity.HasOne(d => d.IdDirTypeOfCargoNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTypeOfCargo)
                .HasConstraintName("FK_reg_weight_dir_typeOfCargo");

            entity.HasOne(d => d.IdDirTypeOfOperationNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTypeOfOperation)
                .HasConstraintName("FK_reg_weight_dir_typeOfOperation");

            entity.HasOne(d => d.IdDirTypeOfTrailerNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTypeOfTrailer)
                .HasConstraintName("FK_reg_weight_dir_typeOfTrailer");

            entity.HasOne(d => d.IdDirTypeOfTrailerModelNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTypeOfTrailerModel)
                .HasConstraintName("FK_reg_weight_dir_TypeOfTrailerModels");

            entity.HasOne(d => d.IdDirTypeOfTransportNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTypeOfTransport)
                .HasConstraintName("FK_reg_weight_dir_typeOfTransport");

            entity.HasOne(d => d.IdDirTypeOfTransportModelsNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdDirTypeOfTransportModels)
                .HasConstraintName("FK_reg_weight_dir_TypeOfTransportModels");

            entity.HasOne(d => d.IdSystemDirIdentifierStateNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdSystemDirIdentifierState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reg_weight_system_dir_identifierState");

            entity.HasOne(d => d.IdSystemDirTypeOfPassNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdSystemDirTypeOfPass)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reg_weight_system_dir_typeOfPass");

            entity.HasOne(d => d.IdSystemDirTypeOfRegweightNavigation).WithMany(p => p.RegWeights)
                .HasForeignKey(d => d.IdSystemDirTypeOfRegweight)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reg_weight_system_dir_typeOfRegweight");
        });

        modelBuilder.Entity<RusNameField>(entity =>
        {
            entity.ToTable("RusNameField");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Rusname)
                .HasMaxLength(400)
                .HasColumnName("RUSName");
        });

        modelBuilder.Entity<SettingBackup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.SettingBackup");

            entity.ToTable("Setting_Backup");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RestorePath).HasMaxLength(200);
            entity.Property(e => e.TimeBackup).HasColumnType("datetime");
        });

        modelBuilder.Entity<SettingsApp>(entity =>
        {
            entity.ToTable("Settings_App");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConditionalZero).HasDefaultValueSql("((0.5))");
            entity.Property(e => e.IdCompany).HasColumnName("id_company");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhotoDirectory).HasMaxLength(255);
            entity.Property(e => e.TsnumberMask)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("TSNumberMask");
            entity.Property(e => e.TsnumberMaskUse).HasColumnName("TSNumberMaskUse");
            entity.Property(e => e.UnionRecognizeSkud).HasColumnName("UnionRecognizeSKUD");

            entity.HasOne(d => d.IdCompanyNavigation).WithMany(p => p.SettingsApps)
                .HasForeignKey(d => d.IdCompany)
                .HasConstraintName("FK_Settings_App_dir_counterparty");
        });

        modelBuilder.Entity<SettingsAuditLoad>(entity =>
        {
            entity.ToTable("Settings_AuditLoad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.LimitOfStart).HasColumnName("limitOfStart");
            entity.Property(e => e.PhotoDirectory)
                .HasMaxLength(255)
                .IsFixedLength();

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsAuditLoads)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settings_AuditLoad_dir_WeightRoom");
        });

        modelBuilder.Entity<SettingsAxleLoad>(entity =>
        {
            entity.ToTable("Settings_AxleLoad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.LimitOfStart).HasColumnName("limitOfStart");
            entity.Property(e => e.PeriodDelay).HasColumnName("periodDelay");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Window).HasColumnName("window");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsAxleLoads)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settings_AxleLoad_dir_WeightRoom");
        });

        modelBuilder.Entity<SettingsC1integrator>(entity =>
        {
            entity.ToTable("Settings_C1Integrator");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("IP");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PathToDb)
                .HasMaxLength(50)
                .HasColumnName("PathToDB");
            entity.Property(e => e.PathToImage).HasMaxLength(50);
            entity.Property(e => e.Refr).HasMaxLength(50);
            entity.Property(e => e.Server).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<SettingsEmail>(entity =>
        {
            entity.ToTable("Settings_Email");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdresantMail).HasMaxLength(150);
            entity.Property(e => e.AdresantName).HasMaxLength(150);
            entity.Property(e => e.Message).HasMaxLength(200);
            entity.Property(e => e.MessageBody).HasMaxLength(200);
            entity.Property(e => e.PortGmail).HasColumnName("PortGMail");
            entity.Property(e => e.UserMail).HasMaxLength(150);
            entity.Property(e => e.UserPassword).HasMaxLength(150);
            entity.Property(e => e.UserSmtp)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.UserUseSsl).HasColumnName("UserUseSSL");
        });

        modelBuilder.Entity<SettingsExport>(entity =>
        {
            entity.ToTable("Settings_Export");

            entity.Property(e => e.FilePath).HasMaxLength(100);
            entity.Property(e => e.PeriodBeginning).HasColumnType("datetime");
            entity.Property(e => e.PeriodEnd).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SettingsIntegration>(entity =>
        {
            entity.ToTable("Settings_Integration");

            entity.Property(e => e.BeginTime)
                .HasColumnType("datetime")
                .HasColumnName("beginTime");
            entity.Property(e => e.DownloadFilePath).HasMaxLength(200);
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.PeriodBeginning).HasColumnType("datetime");
            entity.Property(e => e.PeriodEnd).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UploadFilePath).HasMaxLength(200);
            entity.Property(e => e.WithAdditionalCharachteristics)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.WithCargo)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.WithCounterparty)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.WithStorage)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.WithUsers)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.WithWeighing)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<SettingsKfkh>(entity =>
        {
            entity.ToTable("Settings_KFKH");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<SettingsPreAxleReport>(entity =>
        {
            entity.ToTable("Settings_PreAxleReport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CertificateFrom).HasColumnType("datetime");
            entity.Property(e => e.CertificateName).HasMaxLength(100);
            entity.Property(e => e.CertificateNumber).HasMaxLength(50);
            entity.Property(e => e.CertificateOrg).HasMaxLength(150);
            entity.Property(e => e.CertificateTo).HasColumnType("datetime");
            entity.Property(e => e.ControlPlace).HasMaxLength(150);
            entity.Property(e => e.PpvkNumber)
                .HasMaxLength(50)
                .HasColumnName("PPVK_Number");
            entity.Property(e => e.PpvkOwner)
                .HasMaxLength(150)
                .HasColumnName("PPVK_Owner");
            entity.Property(e => e.ScaleNamber).HasMaxLength(50);
            entity.Property(e => e.WeghingMode).HasMaxLength(50);
        });

        modelBuilder.Entity<SettingsRecognitionModule>(entity =>
        {
            entity.ToTable("Settings_RecognitionModule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.WorkMode).HasMaxLength(1);

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsRecognitionModules)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settings_RecognitionModule_dir_WeightRoom");
        });

        modelBuilder.Entity<SettingsRecognitionMpixel>(entity =>
        {
            entity.ToTable("Settings_RecognitionMPixel", tb => tb.HasComment("Таблица для настроек распознавания"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.IdSettingsVideoSurveillance).HasColumnName("id_Settings_VideoSurveillance");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.ZoneMode128x128).HasColumnName("ZoneMode_128x128");
            entity.Property(e => e.ZoneMode128x32).HasColumnName("ZoneMode_128x32");
            entity.Property(e => e.ZoneMode160x160).HasColumnName("ZoneMode_160x160");
            entity.Property(e => e.ZoneMode160x40).HasColumnName("ZoneMode_160x40");
            entity.Property(e => e.ZoneMode192x192).HasColumnName("ZoneMode_192x192");
            entity.Property(e => e.ZoneMode192x48).HasColumnName("ZoneMode_192x48");
            entity.Property(e => e.ZoneMode256x64).HasColumnName("ZoneMode_256x64");
            entity.Property(e => e.ZoneMode320x80).HasColumnName("ZoneMode_320x80");
            entity.Property(e => e.ZoneMode384x96).HasColumnName("ZoneMode_384x96");
            entity.Property(e => e.ZoneMode48x48).HasColumnName("ZoneMode_48x48");
            entity.Property(e => e.ZoneMode64x64).HasColumnName("ZoneMode_64x64");
            entity.Property(e => e.ZoneMode80x80).HasColumnName("ZoneMode_80x80");
            entity.Property(e => e.ZoneMode96x24).HasColumnName("ZoneMode_96x24");
            entity.Property(e => e.ZoneMode96x96).HasColumnName("ZoneMode_96x96");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsRecognitionMpixels)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settings_RecognitionMPixel_dir_WeightRoom");
        });

        modelBuilder.Entity<SettingsScoreboard>(entity =>
        {
            entity.ToTable("Settings_Scoreboard");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baudrate).HasMaxLength(15);
            entity.Property(e => e.ComName).HasMaxLength(7);
            entity.Property(e => e.Databits).HasMaxLength(1);
            entity.Property(e => e.Dtr).HasColumnName("DTR");
            entity.Property(e => e.Handshake)
                .HasMaxLength(30)
                .HasDefaultValueSql("('None')");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.InterfaceType)
                .HasMaxLength(15)
                .HasDefaultValueSql("('RS232')");
            entity.Property(e => e.Parity).HasMaxLength(15);
            entity.Property(e => e.Rts).HasColumnName("RTS");
            entity.Property(e => e.ScoreboardInterval)
                .HasMaxLength(4)
                .HasDefaultValueSql("((500))");
            entity.Property(e => e.ScoreboardModelValue).HasMaxLength(3);
            entity.Property(e => e.Stopbits).HasMaxLength(15);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsScoreboards)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Settings_Scoreboard_dir_WeightRoom_FK");
        });

        modelBuilder.Entity<SettingsServerReport>(entity =>
        {
            entity.ToTable("Settings_ServerReport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuditPhotoDirectory).HasMaxLength(150);
            entity.Property(e => e.ConnectionString).HasMaxLength(700);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.PhotoDirectory).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.WeighingName).HasMaxLength(100);
        });

        modelBuilder.Entity<SettingsSkud>(entity =>
        {
            entity.ToTable("Settings_SKUD");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BarrierInClose).HasMaxLength(2);
            entity.Property(e => e.BarrierInOpen).HasMaxLength(2);
            entity.Property(e => e.BarrierOutClose).HasMaxLength(2);
            entity.Property(e => e.BarrierOutOpen).HasMaxLength(2);
            entity.Property(e => e.BarrierWorkMode).HasMaxLength(1);
            entity.Property(e => e.Baudrate).HasMaxLength(15);
            entity.Property(e => e.ComName).HasMaxLength(7);
            entity.Property(e => e.ControlS1).HasColumnName("Control_S1");
            entity.Property(e => e.ControlS2).HasColumnName("Control_S2");
            entity.Property(e => e.ControlS3).HasColumnName("Control_S3");
            entity.Property(e => e.ControlS4).HasColumnName("Control_S4");
            entity.Property(e => e.ControlS5).HasColumnName("Control_S5");
            entity.Property(e => e.ControlS6).HasColumnName("Control_S6");
            entity.Property(e => e.Databits).HasMaxLength(1);
            entity.Property(e => e.EnableIrSensors).HasColumnName("EnableIR_Sensors");
            entity.Property(e => e.EnableSkud).HasColumnName("EnableSKUD");
            entity.Property(e => e.Handshake).HasMaxLength(30);
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.InterfaceType).HasMaxLength(15);
            entity.Property(e => e.InvertS1).HasColumnName("Invert_S1");
            entity.Property(e => e.InvertS2).HasColumnName("Invert_S2");
            entity.Property(e => e.InvertS3).HasColumnName("Invert_S3");
            entity.Property(e => e.InvertS4).HasColumnName("Invert_S4");
            entity.Property(e => e.InvertS5).HasColumnName("Invert_S5");
            entity.Property(e => e.InvertS6).HasColumnName("Invert_S6");
            entity.Property(e => e.NetworkAddress).HasMaxLength(2);
            entity.Property(e => e.Parity).HasMaxLength(15);
            entity.Property(e => e.ReaderCenter).HasMaxLength(2);
            entity.Property(e => e.ReaderIn).HasMaxLength(2);
            entity.Property(e => e.ReaderOut).HasMaxLength(2);
            entity.Property(e => e.ReaderViewMode).HasMaxLength(1);
            entity.Property(e => e.ReaderWorkMode).HasMaxLength(1);
            entity.Property(e => e.Stopbits).HasMaxLength(15);
            entity.Property(e => e.TrafficLightInFar).HasMaxLength(2);
            entity.Property(e => e.TrafficLightInNear).HasMaxLength(2);
            entity.Property(e => e.TrafficLightOutFar).HasMaxLength(2);
            entity.Property(e => e.TrafficLightOutNear).HasMaxLength(2);
            entity.Property(e => e.TrafficLightWorkMode).HasMaxLength(1);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsSkuds)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Settings_SKUD_dir_WeightRoom_FK");
        });

        modelBuilder.Entity<SettingsSpeech>(entity =>
        {
            entity.ToTable("Settings_Speech");

            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.MessageText).HasMaxLength(150);
            entity.Property(e => e.MessageTextEight).HasMaxLength(150);
            entity.Property(e => e.MessageTextFive).HasMaxLength(150);
            entity.Property(e => e.MessageTextFour).HasMaxLength(150);
            entity.Property(e => e.MessageTextSeven).HasMaxLength(150);
            entity.Property(e => e.MessageTextSix).HasMaxLength(150);
            entity.Property(e => e.MessageTextThree).HasMaxLength(150);
            entity.Property(e => e.MessageTextTwo).HasMaxLength(150);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsSpeeches)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Settings_Speech_dir_WeightRoom");
        });

        modelBuilder.Entity<SettingsTerminal>(entity =>
        {
            entity.ToTable("Settings_Terminal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baudrate).HasMaxLength(15);
            entity.Property(e => e.ComName).HasMaxLength(7);
            entity.Property(e => e.Databits).HasMaxLength(1);
            entity.Property(e => e.Dtr).HasColumnName("DTR");
            entity.Property(e => e.Handshake).HasMaxLength(30);
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.InterfaceType).HasMaxLength(15);
            entity.Property(e => e.NetworkAddress).HasMaxLength(2);
            entity.Property(e => e.Parity).HasMaxLength(15);
            entity.Property(e => e.Rts).HasColumnName("RTS");
            entity.Property(e => e.Stopbits).HasMaxLength(15);
            entity.Property(e => e.TerminalInterval)
                .HasMaxLength(4)
                .HasDefaultValueSql("((500))");
            entity.Property(e => e.TerminalModelValue).HasMaxLength(15);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsTerminals)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Settings_Terminal_dir_WeightRoom_FK");
        });

        modelBuilder.Entity<SettingsUsbkeyReader>(entity =>
        {
            entity.ToTable("Settings_USBKeyReader");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Baudrate).HasMaxLength(15);
            entity.Property(e => e.ComName).HasMaxLength(7);
            entity.Property(e => e.Databits).HasMaxLength(1);
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.Parity).HasMaxLength(15);
            entity.Property(e => e.Stopbits).HasMaxLength(15);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UsbkeyReaderInterval)
                .HasMaxLength(4)
                .HasDefaultValueSql("((500))")
                .HasColumnName("USBKeyReaderInterval");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsUsbkeyReaders)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Settings_USBKeyReader_dir_WeightRoom_FK");
        });

        modelBuilder.Entity<SettingsVideoSurveillance>(entity =>
        {
            entity.ToTable("Settings_VideoSurveillance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.Ip)
                .HasMaxLength(15)
                .HasColumnName("IP");
            entity.Property(e => e.Login).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(35);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.SettingsVideoSurveillances)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Settings_VideoSurveillance_dir_WeightRoom_FK");
        });

        modelBuilder.Entity<SystemDirAccessPermission>(entity =>
        {
            entity.ToTable("system_dir_AccessPermissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ControlTag).HasMaxLength(10);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasComment("Название разрешения");
        });

        modelBuilder.Entity<SystemDirDirectionTypeOperation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_system_dir_DirectionTypeOperation_id");

            entity.ToTable("system_dir_DirectionTypeOperation", tb => tb.HasTrigger("system_dir_DirectionTypeOperation_Edit_Insert_Delete"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<SystemDirIdentifierState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_system_dir_identifierState_id");

            entity.ToTable("system_dir_identifierState", tb => tb.HasTrigger("system_dir_identifierState_Edit_Insert_Delete"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<SystemDirKindOfPacking>(entity =>
        {
            entity.ToTable("system_dir_kindOfPacking", tb => tb.HasTrigger("system_dir_kindOfPacking_Edit_Insert_Delete"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<SystemDirTypeOfCarriage>(entity =>
        {
            entity.ToTable("system_dir_typeOfCarriage", tb => tb.HasTrigger("system_dir_typeOfCarriage_Edit_Insert_Delete"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<SystemDirTypeOfCarriageDocument>(entity =>
        {
            entity.ToTable("system_dir_typeOfCarriageDocument", tb => tb.HasTrigger("system_dir_typeOfCarriageDocument_Edit_Insert_Delete"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<SystemDirTypeOfPass>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_system_dir_typeOfPass_id");

            entity.ToTable("system_dir_typeOfPass", tb => tb.HasTrigger("system_dir_typeOfPass_Edit_Insert_Delete"));

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<SystemDirTypeOfRegweight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_system_dir_typeOfRegweight_id");

            entity.ToTable("system_dir_typeOfRegweight");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Ttndocument>(entity =>
        {
            entity.ToTable("TTNDocuments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountantName).HasMaxLength(255);
            entity.Property(e => e.CargoPickUpAdditionalInfo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CargoPickUpIdentity).HasMaxLength(255);
            entity.Property(e => e.CargoPickUpName).HasMaxLength(255);
            entity.Property(e => e.CargoPickUpWorkAs).HasMaxLength(255);
            entity.Property(e => e.CargoReleaseName).HasMaxLength(255);
            entity.Property(e => e.CargoReleaseWorkAs).HasMaxLength(255);
            entity.Property(e => e.CarriageDocumentNumber).HasMaxLength(45);
            entity.Property(e => e.CarrierDir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CarrierOrganization).HasMaxLength(400);
            entity.Property(e => e.CarrierPositionDir)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ConsigneeDir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ConsigneeName).HasMaxLength(255);
            entity.Property(e => e.ConsigneeOrganization).HasMaxLength(400);
            entity.Property(e => e.ConsigneePositionDir)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ConsigneeWorkAs).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CustomerDir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CustomerOrganization).HasMaxLength(400);
            entity.Property(e => e.CustomerPositionDir)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IdSystemDirKindOfPacking).HasColumnName("id_system_dir_kindOfPacking");
            entity.Property(e => e.IdSystemDirTypeOfCarriage).HasColumnName("id_system_dir_typeOfCarriage");
            entity.Property(e => e.IdSystemDirTypeOfCarriageDocument).HasColumnName("id_system_dir_typeOfCarriageDocument");
            entity.Property(e => e.IdWeighing).HasColumnName("id_weighing");
            entity.Property(e => e.LoadPoint).HasMaxLength(45);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OwnDir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OwnPositionDir)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PackSealNumber).HasMaxLength(45);
            entity.Property(e => e.PayerDir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PayerOrganization).HasMaxLength(400);
            entity.Property(e => e.PayerPositionDir)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ProxyDate).HasColumnType("date");
            entity.Property(e => e.ProxyGivenBy).HasMaxLength(255);
            entity.Property(e => e.ProxyName).HasMaxLength(255);
            entity.Property(e => e.ShipperDir)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShipperOrganization).HasMaxLength(400);
            entity.Property(e => e.ShipperPositionDir)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TrailerNumber).HasMaxLength(45);
            entity.Property(e => e.TypeOfOwn)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfTrailer).HasMaxLength(45);
            entity.Property(e => e.UploadPoint).HasMaxLength(45);

            entity.HasOne(d => d.IdSystemDirKindOfPackingNavigation).WithMany(p => p.Ttndocuments)
                .HasForeignKey(d => d.IdSystemDirKindOfPacking)
                .HasConstraintName("FK_TTNDocuments_system_dir_kindOfPacking");

            entity.HasOne(d => d.IdSystemDirTypeOfCarriageNavigation).WithMany(p => p.Ttndocuments)
                .HasForeignKey(d => d.IdSystemDirTypeOfCarriage)
                .HasConstraintName("FK_TTNDocuments_system_dir_typeOfCarriage");

            entity.HasOne(d => d.IdSystemDirTypeOfCarriageDocumentNavigation).WithMany(p => p.Ttndocuments)
                .HasForeignKey(d => d.IdSystemDirTypeOfCarriageDocument)
                .HasConstraintName("FK_TTNDocuments_system_dir_typeOfCarriageDocument");

            entity.HasOne(d => d.IdWeighingNavigation).WithMany(p => p.Ttndocuments)
                .HasForeignKey(d => d.IdWeighing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TTNDocuments_weighing");
        });

        modelBuilder.Entity<UnrecWeighing>(entity =>
        {
            entity.ToTable("unrec_weighing");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdDirWeightRoom).HasColumnName("id_dir_WeightRoom");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.UnrecWeighings)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnrecWeighing_DirUsers");

            entity.HasOne(d => d.IdDirWeightRoomNavigation).WithMany(p => p.UnrecWeighings)
                .HasForeignKey(d => d.IdDirWeightRoom)
                .HasConstraintName("FK_Unrec_WeightRoom");
        });

        modelBuilder.Entity<UnrecWeightingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__unrec_we__3213E83F655F4AEC");

            entity.ToTable("unrec_weighting_photo");

            entity.HasIndex(e => e.Id, "UQ__unrec_we__3213E83E38455557").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdSettingsVideoSurveillance).HasColumnName("id_Settings_VideoSurveillance");
            entity.Property(e => e.IdUnrecWeighing).HasColumnName("id_unrec_weighing");

            entity.HasOne(d => d.IdUnrecWeighingNavigation).WithMany(p => p.UnrecWeightingPhotos)
                .HasForeignKey(d => d.IdUnrecWeighing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_unrec_weighting_photo");
        });

        modelBuilder.Entity<ViewComTrailerModelsCharacter>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_com_TrailerModels_Characters");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ModelName).HasMaxLength(50);
            entity.Property(e => e.TrailerName).HasMaxLength(45);
        });

        modelBuilder.Entity<ViewComTransportModelsCharacter>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_com_TransportModels_Characters");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ModelName).HasMaxLength(50);
            entity.Property(e => e.TransportName).HasMaxLength(45);
        });

        modelBuilder.Entity<Weighing>(entity =>
        {
            entity.ToTable("weighing");

            entity.HasIndex(e => e.DateTimeFirstWeight, "IX_weighing_DateTimeFirstWeight");

            entity.HasIndex(e => e.DateTimeSecondWeight, "IX_weighing_DateTimeSecondWeight");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateTimeFirstWeight).HasColumnType("datetime");
            entity.Property(e => e.DateTimeSecondWeight).HasColumnType("datetime");
            entity.Property(e => e.IdFirstWeightUser).HasColumnName("id_firstWeight_user");
            entity.Property(e => e.IdRegWeight).HasColumnName("id_reg_weight");
            entity.Property(e => e.IdSecondWeightUser).HasColumnName("id_secondWeight_user");
            entity.Property(e => e.IdWeightingInfo).HasColumnName("id_weighting_info");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NumberOfWeight).HasColumnName("Number_of_weight");
            entity.Property(e => e.Ref).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.IdFirstWeightUserNavigation).WithMany(p => p.WeighingIdFirstWeightUserNavigations)
                .HasForeignKey(d => d.IdFirstWeightUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weighing_id_firstWeight_user");

            entity.HasOne(d => d.IdRegWeightNavigation).WithMany(p => p.Weighings)
                .HasForeignKey(d => d.IdRegWeight)
                .HasConstraintName("FK_weighing_reg_weight");

            entity.HasOne(d => d.IdSecondWeightUserNavigation).WithMany(p => p.WeighingIdSecondWeightUserNavigations)
                .HasForeignKey(d => d.IdSecondWeightUser)
                .HasConstraintName("FK_weighing_id_secondWeight_user");

            entity.HasOne(d => d.IdWeightingInfoNavigation).WithMany(p => p.Weighings)
                .HasForeignKey(d => d.IdWeightingInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weighing_weighing_info");
        });

        modelBuilder.Entity<WeighingInfo>(entity =>
        {
            entity.ToTable("weighing_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IdComTransportDriverTrailer).HasColumnName("id_Com_transport_driver_trailer");
            entity.Property(e => e.IdDirAdditionalCharacteristics).HasColumnName("id_dir_additional_characteristics");
            entity.Property(e => e.IdDirFund).HasColumnName("id_dir_fund");
            entity.Property(e => e.IdDirInvoice).HasColumnName("id_dir_invoice");
            entity.Property(e => e.IdDirKfkh).HasColumnName("id_dir_KFKH");
            entity.Property(e => e.IdDirLaboratory).HasColumnName("id_dir_Laboratory");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.IdComTransportDriverTrailerNavigation).WithMany(p => p.WeighingInfos)
                .HasForeignKey(d => d.IdComTransportDriverTrailer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weighing_info_com_dir_transport_driver_trailer");

            entity.HasOne(d => d.IdDirAdditionalCharacteristicsNavigation).WithMany(p => p.WeighingInfos)
                .HasForeignKey(d => d.IdDirAdditionalCharacteristics)
                .HasConstraintName("FK_weighing_info_dir_additional_characteristics");

            entity.HasOne(d => d.IdDirFundNavigation).WithMany(p => p.WeighingInfos)
                .HasForeignKey(d => d.IdDirFund)
                .HasConstraintName("FK_weighing_info_dir_fund");

            entity.HasOne(d => d.IdDirInvoiceNavigation).WithMany(p => p.WeighingInfos)
                .HasForeignKey(d => d.IdDirInvoice)
                .HasConstraintName("FK_weighing_info_dir_invoice");
        });

        modelBuilder.Entity<WeightingPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__weightin__3213E83F093F9B93");

            entity.ToTable("weighting_photo");

            entity.HasIndex(e => e.Id, "UQ__weightin__3213E83E3B30E0DE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IdSettingsVideoSurveillance).HasColumnName("id_Settings_VideoSurveillance");
            entity.Property(e => e.IdWeighing).HasColumnName("id_weighing");

            entity.HasOne(d => d.IdWeighingNavigation).WithMany(p => p.WeightingPhotos)
                .HasForeignKey(d => d.IdWeighing)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_weighting_photo_weighing");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
