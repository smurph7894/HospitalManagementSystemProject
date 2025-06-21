-- Create Database
CREATE DATABASE HospitalManagementDB;
Go

-- Use the created database
USE HospitalManagementDB;
Go

CREATE TABLE dbo.Patients (
    PatientId INT PRIMARY KEY IDENTITY,
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
GO

CREATE TABLE dbo.CarePlans (
	CarePlanId INT IDENTITY PRIMARY KEY,
	PatientId INT NOT NULL,
	Condition NVARCHAR(200) NOT NULL,
	Description NVARCHAR(MAX) NULL,
	DiagnosisDate DATETIME NOT NULL DEFAULT GETDATE(),
	DateResolved DATETIME NULL,

	CONSTRAINT FK_CarePlan_Patients
		FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId)
);
GO

CREATE TABLE dbo.CarePlanUpdates (
	CarePlanUpdateId INT IDENTITY PRIMARY KEY,
	CarePlanId INT NOT NULL,
	AppointmentId INT NOT NULL,
	Notes NVARCHAR(MAX) NOT NULL,

	CONSTRAINT FK_CarePlanUpdates_CarePlans 
		FOREIGN KEY(CarePlanId) REFERENCES dbo.CarePlans(CarePlanId),
	CONSTRAINT FK_CarePlanUpdates_Appointments
		FOREIGN KEY(AppointmentId) REFERENCES dbo.Appointments(AppointmentId)
);
GO

CREATE TABLE dbo.Departments (
	DepartmentID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL UNIQUE,
	Description NVARCHAR(500) Null
);
GO

CREATE TABLE dbo.Staff(
	StaffId INT IDENTITY PRIMARY KEY,
	UserRef VARCHAR(24) NULL,
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
GO

CREATE TABLE dbo.Appointments (
    AppointmentId      INT             IDENTITY PRIMARY KEY,
    PatientId          INT             NOT NULL,
    StaffId            INT             NOT NULL,
    ScheduledAt        DATETIME        NOT NULL,
    DurationMinutes    INT             NULL,
    Status             NVARCHAR(50)    NOT NULL,  -- e.g. 'Scheduled','Completed','Cancelled'
    Reason             NVARCHAR(500)   NULL,
    CreatedBy          INT             NOT NULL,  -- who created the appt
    CreatedAt          DATETIME        NOT NULL DEFAULT GETDATE(),
    UpdatedAt          DATETIME        NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Appointments_Patients
        FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId),
    CONSTRAINT FK_Appointments_Staff
        FOREIGN KEY(StaffId) REFERENCES dbo.Staff(StaffId),
    CONSTRAINT FK_Appointments_CreatedBy
        FOREIGN KEY(CreatedBy) REFERENCES dbo.Staff(StaffId)
);
GO

CREATE TABLE dbo.InventoryItems (
    ItemId             INT             IDENTITY PRIMARY KEY,
    Name               NVARCHAR(200)   NOT NULL,
    Description        NVARCHAR(500)   NULL,
    QuantityInStock    INT             NOT NULL DEFAULT 0,
    UnitOfMeasure      NVARCHAR(50)    NULL,
    ReorderLevel       INT             NOT NULL DEFAULT 0,
    Location           NVARCHAR(100)   NULL,
    CreatedAt          DATETIME        NOT NULL DEFAULT GETDATE(),
    UpdatedAt          DATETIME        NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE dbo.InventoryTransactions (
    TransactionId      INT             IDENTITY PRIMARY KEY,
    ItemId             INT             NOT NULL,
    ChangeQuantity     INT             NOT NULL,
    TransactionType    NVARCHAR(50)    NOT NULL,  -- 'Received','Dispensed','Adjustment'
    PerformedBy        INT             NOT NULL,
    TransactionDateTime DATETIME       NOT NULL DEFAULT GETDATE(),
    Remarks            NVARCHAR(500)   NULL,
    CONSTRAINT FK_InvTrans_Items
        FOREIGN KEY(ItemId) REFERENCES dbo.InventoryItems(ItemId),
    CONSTRAINT FK_InvTrans_Staff
        FOREIGN KEY(PerformedBy) REFERENCES dbo.Staff(StaffId)
);
GO

CREATE TABLE dbo.Beds (
    BedId          INT             IDENTITY PRIMARY KEY,
    Ward           NVARCHAR(100)   NOT NULL,
    BedNumber      NVARCHAR(50)    NOT NULL,
    Status         NVARCHAR(20)    NOT NULL,  -- 'Available','Occupied','Maintenance'
    CONSTRAINT UQ_Beds_WardBed UNIQUE (Ward, BedNumber)
);
GO

CREATE TABLE dbo.Admissions (
    AdmissionId    INT             IDENTITY PRIMARY KEY,
    PatientId      INT             NOT NULL,
    BedId          INT             NOT NULL,
    AdmittedAt     DATETIME        NOT NULL DEFAULT GETDATE(),
    DischargedAt   DATETIME        NULL,
    AdmitBy        INT             NOT NULL,
    DischargeBy    INT             NULL,
    CONSTRAINT FK_Admissions_Patients
        FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId),
    CONSTRAINT FK_Admissions_Beds
        FOREIGN KEY(BedId) REFERENCES dbo.Beds(BedId),
    CONSTRAINT FK_Admissions_AdmitBy
        FOREIGN KEY(AdmitBy) REFERENCES dbo.Staff(StaffId),
    CONSTRAINT FK_Admissions_DischargeBy
        FOREIGN KEY(DischargeBy) REFERENCES dbo.Staff(StaffId)
);
GO

CREATE TABLE dbo.Vitals (
    VitalId        INT             IDENTITY PRIMARY KEY,
    PatientId      INT             NOT NULL,
    VitalType      NVARCHAR(50)    NOT NULL,  -- 'Heart Rate', 'Blood Pressure', etc.
    Value          NVARCHAR(50)    NOT NULL,
    Unit           NVARCHAR(20)    NULL,
    RecordedAt     DATETIME        NOT NULL DEFAULT GETDATE(),
    RecordedBy     INT             NOT NULL,
    CONSTRAINT FK_Vitals_Patients
        FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId),
    CONSTRAINT FK_Vitals_Staff
        FOREIGN KEY(RecordedBy) REFERENCES dbo.Staff(StaffId)
);
GO

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
GO

-- Insert dummy data into the Products table
--INSERT INTO Products (ProductName, Price) VALUES ('Smartphone', 499.99);
--INSERT INTO Products (ProductName, Price) VALUES ('Laptop', 899.99);
--INSERT INTO Products (ProductName, Price) VALUES ('Headphones', 99.99);
--INSERT INTO Products (ProductName, Price) VALUES ('Tablet', 299.99);
GO
