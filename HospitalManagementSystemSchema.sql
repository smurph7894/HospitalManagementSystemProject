create database HospitalManagementDB;

use HospitalManagementDB;

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

CREATE TABLE dbo.MedicalHistories (
	HistoryId INT IDENTITY PRIMARY KEY,
	PatientId INT NOT NULL,
	Condition NVARCHAR(200) NOT NULL,
	Description NVARCHAR(MAX) NULL,
	DateRecorded DATETIME NOT NULL DEFAULT GETDATE(),
	DocumentPath NVARCHAR(500) NUll,
	CONSTRAINT FK_MedicalHistories_Patients
		FOREIGN KEY(PatientId) REFERENCES dbo.Patients(PatientId)
);

CREATE TABLE dbo.Departments (
	DepartmentID INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL UNIQUE,
	Description NVARCHAR(500) Null
);

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
		FOREIGN KEY(DepartmentId) Reference dbo.Departments(DepartmentId)
);

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

CREATE TABLE dbo.Beds (
    BedId          INT             IDENTITY PRIMARY KEY,
    Ward           NVARCHAR(100)   NOT NULL,
    BedNumber      NVARCHAR(50)    NOT NULL,
    Status         NVARCHAR(20)    NOT NULL,  -- 'Available','Occupied','Maintenance'
    CONSTRAINT UQ_Beds_WardBed UNIQUE (Ward, BedNumber)
);

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

-- Insert sample data into Locations table
INSERT INTO Locations (CityName, Latitude, Longitude) VALUES ('New York', 40.7128, -74.0060);

{"CreatedAt": "2024-05-01T00:00:00Z", "UpdatedAt": "2024-05-01T00:00:00Z", "Permissions": "Admin", "Username": "username", "Password": "abc123", "firstName": "John", "LastName": "Smith", "DOB": "2024-05-01T00:00:00Z", "Position": "Admin"},
]