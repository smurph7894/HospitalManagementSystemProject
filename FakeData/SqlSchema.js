module.exports = `
--create DB if missing
IF DB_ID('HospitalManagementDB') IS NULL
    CREATE DATABASE HospitalManagementDB;
GO

USE HospitalManagementDB;
GO

IF OBJECT_ID('dbo.CarePlans', 'U') IS NULL
BEGIN
CREATE TABLE dbo.CarePlans (
	CarePlanId INT IDENTITY PRIMARY KEY,
	PatientId INT NOT NULL,
	Condition NVARCHAR(200) NOT NULL,
	Description NVARCHAR(MAX) NULL,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
	DateResolved DATETIME NULL,
	DiagnosisDate DATETIME NULL,

	CONSTRAINT FK_CarePlan_Patients
		FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId)
		ON DELETE CASCADE
);
END
GO

IF OBJECT_ID('dbo.CarePlanUpdates', 'U') IS NULL
BEGIN
CREATE TABLE dbo.CarePlanUpdates (
	CarePlanUpdateId INT IDENTITY PRIMARY KEY,
	CarePlanId INT NOT NULL,
	AppointmentId INT NOT NULL,
	Notes NVARCHAR(MAX) NOT NULL,

	CONSTRAINT FK_CarePlanUpdates_CarePlans 
		FOREIGN KEY(CarePlanId) REFERENCES dbo.CarePlans(CarePlanId)
		ON DELETE CASCADE,
	CONSTRAINT FK_CarePlanUpdates_Appointments
		FOREIGN KEY(AppointmentId) REFERENCES dbo.Appointments(AppointmentId)
);
END
GO

IF OBJECT_ID('dbo.Patients', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Patients (
    PatientId INT PRIMARY KEY IDENTITY,
	PatientOrgId NVARCHAR(500) NOT NULL, 
    FirstName NVARCHAR(100) NOT NULL,
	LastName NVARCHAR(100) NOT NULL,
    DOB DATE NOT NULL,
	Gender CHAR(1) NOT NULL,
	Phone NVARCHAR(20) NULL,
	Email NVARCHAR(255) NULL,
	Address NVARCHAR(500) NULL,
	EmergencyContactName NVARCHAR(200) NULL,
	EmergencyContactPhone NVARCHAR(20) NULL,
	InsuranceProvider NVARCHAR(200) NULL,
	InsurancePolicyNumber NVARCHAR(100) NULL,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
	UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
END
GO

IF OBJECT_ID('dbo.Departments', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Departments (
	DepartmentID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL UNIQUE,
	Description NVARCHAR(500) Null
);
END
GO

IF OBJECT_ID('dbo.Staff', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Staff(
	StaffId INT IDENTITY PRIMARY KEY,
	UserRef VARCHAR(24) NULL,
	Name VARCHAR(100) NULL,
	StaffType VARCHAR(50) NOT NULL,
	Specialization VARCHAR(100) NULL,
	DepartmentId INT NOT NULL,
	HireDate DATE NULL,
	Phone VARCHAR(20) NULL,
	Email VARCHAR(255) NULL,
	CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
	UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT FK_Staff_Departments
		FOREIGN KEY(DepartmentId) REFERENCES dbo.Departments(DepartmentId)
);
END
GO

IF OBJECT_ID('dbo.Appointments', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Appointments (
    AppointmentId      INT             IDENTITY PRIMARY KEY,
    PatientId          INT             NOT NULL,
    StaffId            INT             NULL,
    ScheduledAt        DATETIME        NOT NULL,
    DurationMinutes    INT             NULL,
    Status             NVARCHAR(50)    NOT NULL, 
    Reason             NVARCHAR(500)   NULL,
    CreatedAt          DATETIME        NOT NULL DEFAULT GETDATE(),
    UpdatedAt          DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Appointments_Patients
        FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId)
        ON DELETE CASCADE,
    CONSTRAINT FK_Appointments_Staff
        FOREIGN KEY(StaffId) REFERENCES dbo.Staff(StaffId),
);
END
GO

IF OBJECT_ID('dbo.InventoryItems', 'U') IS NULL
BEGIN
CREATE TABLE dbo.InventoryItems (
    ItemId             INT             IDENTITY PRIMARY KEY,
    Name               NVARCHAR(200)   NOT NULL,
    Description        NVARCHAR(500)   NULL,
    QuantityInStock    INT             NOT NULL DEFAULT 0,
    UnitOfMeasure      NVARCHAR(50)    NULL,
    ReorderLevel       INT             NOT NULL DEFAULT 0,
    Location           NVARCHAR(100)   NULL,
    isMedicine		   BIT			   NOT NULL, --0 is false, 1 is true
	TotalHospitalUsage INT			   NOT NULL DEFAULT 0,
    CreatedAt          DATETIME        NOT NULL DEFAULT GETDATE(),
    UpdatedAt          DATETIME        NOT NULL DEFAULT GETDATE()
);
END
GO

IF OBJECT_ID('dbo.InventoryTransactions', 'U') IS NULL
BEGIN
CREATE TABLE dbo.InventoryTransactions (
    TransactionId      INT             IDENTITY PRIMARY KEY,
    ItemId             INT             NOT NULL,
    ChangeQuantity     INT             NOT NULL,
    TransactionType    NVARCHAR(50)    NOT NULL, 
    PerformedBy        INT             NOT NULL,
    TransactionDateTime DATETIME       NOT NULL DEFAULT GETDATE(),
    Remarks            NVARCHAR(500)   NULL,
    CONSTRAINT FK_InvTrans_Items
        FOREIGN KEY(ItemId) REFERENCES dbo.InventoryItems(ItemId),
    CONSTRAINT FK_InvTrans_Staff
        FOREIGN KEY(PerformedBy) REFERENCES dbo.Staff(StaffId)
);
END
GO

IF OBJECT_ID('dbo.Beds', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Beds (
    BedId          INT             IDENTITY PRIMARY KEY,
    Ward           NVARCHAR(100)   NOT NULL,
    BedNumber      NVARCHAR(50)    NOT NULL,
    Status         NVARCHAR(20)    NOT NULL, 
    CONSTRAINT UQ_Beds_WardBed UNIQUE (Ward, BedNumber)
);
END
GO

IF OBJECT_ID('dbo.Admissions', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Admissions (
    AdmissionId    INT             IDENTITY PRIMARY KEY,
    PatientId      INT             NOT NULL,
    BedId          INT             NOT NULL,
    AdmittedAt     DATETIME        NOT NULL DEFAULT GETDATE(),
    DischargedAt   DATETIME        NULL,
    AdmitBy        INT             NOT NULL,
    DischargeBy    INT             NULL,
    CONSTRAINT FK_Admissions_Patients
        FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId)
        ON DELETE CASCADE,
    CONSTRAINT FK_Admissions_Beds
        FOREIGN KEY(BedId) REFERENCES dbo.Beds(BedId),
    CONSTRAINT FK_Admissions_AdmitBy
        FOREIGN KEY(AdmitBy) REFERENCES dbo.Staff(StaffId),
    CONSTRAINT FK_Admissions_DischargeBy
        FOREIGN KEY(DischargeBy) REFERENCES dbo.Staff(StaffId)
);
END
GO

IF OBJECT_ID('dbo.Vitals', 'U') IS NULL
BEGIN
CREATE TABLE dbo.Vitals (
    VitalId        INT             IDENTITY PRIMARY KEY,
    PatientId      INT             NOT NULL,
    VitalType      NVARCHAR(50)    NOT NULL,  
    Value          NVARCHAR(50)    NOT NULL,
    Unit           NVARCHAR(20)    NOT NULL,
    RecordedAt     DATETIME        NOT NULL DEFAULT GETDATE(),
    RecordedBy     INT             NOT NULL,
    CONSTRAINT FK_Vitals_Patients
        FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId)
        ON DELETE CASCADE,
    CONSTRAINT FK_Vitals_Staff
        FOREIGN KEY(RecordedBy) REFERENCES dbo.Staff(StaffId)
);
END
GO

IF OBJECT_ID('dbo.ReportsHistory', 'U') IS NULL
BEGIN
CREATE TABLE dbo.ReportsHistory (
    ReportId       INT             IDENTITY PRIMARY KEY,
    ReportType     NVARCHAR(100)   NOT NULL,
    Parameters     NVARCHAR(MAX)   NULL,
    GeneratedAt    DATETIME        NOT NULL DEFAULT GETDATE(),
    GeneratedBy    INT             NOT NULL,
    FilePath       NVARCHAR(500)   NULL,
    CONSTRAINT FK_Reports_Staff
        FOREIGN KEY(GeneratedBy) REFERENCES dbo.Staff(StaffId)
);
END
GO
`
