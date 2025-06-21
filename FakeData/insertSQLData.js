const sql = require('mssql');
const config = require('./dbConfig');

async function ensureSchema() {
  try{
    const pool = await sql.connect(config);
  }
  catch (error){
    console.error('SQL error', error);
  }
};

// Insert into Patients
async function insertPatient(
  patientOrgId, firstName, lastName, dob, gender,
  phone, email, address,
  emergencyContactName, emergencyContactPhone,
  insuranceProvider, insurancePolicyNumber
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('PatientOrgId', sql.NVarChar(500), patientOrgId)
    .input('FirstName', sql.NVarChar(100), firstName)
    .input('LastName', sql.NVarChar(100), lastName)
    .input('DOB', sql.Date, dob)
    .input('Gender', sql.Char(1), gender)
    .input('Phone', sql.NVarChar(20), phone)
    .input('Email', sql.NVarChar(255), email)
    .input('Address', sql.NVarChar(500), address)
    .input('EmergencyContactName', sql.NVarChar(200), emergencyContactName)
    .input('EmergencyContactPhone', sql.NVarChar(20), emergencyContactPhone)
    .input('InsuranceProvider', sql.NVarChar(200), insuranceProvider)
    .input('InsurancePolicyNumber', sql.NVarChar(100), insurancePolicyNumber)
    .query(
      `INSERT INTO dbo.Patients (
         PatientOrgId, FirstName, LastName, DOB, Gender,
         Phone, Email, Address,
         EmergencyContactName, EmergencyContactPhone,
         InsuranceProvider, InsurancePolicyNumber
       ) VALUES (
         @PatientOrgId, @FirstName, @LastName, @DOB, @Gender,
         @Phone, @Email, @Address,
         @EmergencyContactName, @EmergencyContactPhone,
         @InsuranceProvider, @InsurancePolicyNumber
       );`
    );
  console.log('Patient inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into Departments
async function insertDepartment(name, description) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('Name', sql.NVarChar(100), name)
    .input('Description', sql.NVarChar(500), description)
    .query(
      `INSERT INTO dbo.Departments (Name, Description)
       VALUES (@Name, @Description);`
    );
  console.log('Department inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into CarePlans
async function insertCarePlan(patientId, condition, description) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('PatientId', sql.Int, patientId)
    .input('Condition', sql.NVarChar(200), condition)
    .input('Description', sql.NVarChar(sql.MAX), description)
    .query(
      `INSERT INTO dbo.CarePlans (PatientId, Condition, Description)
       VALUES (@PatientId, @Condition, @Description);`
    );
  console.log('CarePlan inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into CarePlanUpdates
async function insertCarePlanUpdate(carePlanId, appointmentId, notes) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('CarePlanId', sql.Int, carePlanId)
    .input('AppointmentId', sql.Int, appointmentId)
    .input('Notes', sql.NVarChar(sql.MAX), notes)
    .query(
      `INSERT INTO dbo.CarePlanUpdates (CarePlanId, AppointmentId, Notes)
       VALUES (@CarePlanId, @AppointmentId, @Notes);`
    );
  console.log('CarePlanUpdate inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into Staff
async function insertStaff(
  userRef, name, staffType, specialization, departmentId,
  hireDate, phone, email
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('UserRef', sql.VarChar(24), userRef)
    .input('Name', sql.Varchar(100), name)
    .input('StaffType', sql.Varchar(50), staffType)
    .input('Specialization', sql.Varchar(100), specialization)
    .input('DepartmentId', sql.Int, departmentId)
    .input('HireDate', sql.Date, hireDate)
    .input('Phone', sql.Varchar(20), phone)
    .input('Email', sql.NVarChar(255), email)
    .query(
      `INSERT INTO dbo.Staff (
         UserRef, Name, StaffType, Specialization, DepartmentId,
         HireDate, Phone, Email
       ) VALUES (
         @UserRef, @Name, @StaffType, @Specialization, @DepartmentId,
         @HireDate, @Phone, @Email
       );`
    );
  console.log('Staff inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into Appointments
async function insertAppointment(
  patientId, staffId, scheduledAt, durationMinutes,
  status, reason, createdBy
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('PatientId', sql.Int, patientId)
    .input('StaffId', sql.Int, staffId)
    .input('ScheduledAt', sql.DateTime, scheduledAt)
    .input('DurationMinutes', sql.Int, durationMinutes)
    .input('Status', sql.NVarChar(50), status)
    .input('Reason', sql.NVarChar(500), reason)
    .input('CreatedBy', sql.Int, createdBy)
    .query(
      `INSERT INTO dbo.Appointments (
         PatientId, StaffId, ScheduledAt, DurationMinutes,
         Status, Reason, CreatedBy
       ) VALUES (
         @PatientId, @StaffId, @ScheduledAt, @DurationMinutes,
         @Status, @Reason, @CreatedBy
       );`
    );
  console.log('Appointment inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into InventoryItems
async function insertInventoryItem(
  name, description, quantityInStock, unitOfMeasure,
  reorderLevel, location
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('Name', sql.NVarChar(200), name)
    .input('Description', sql.NVarChar(500), description)
    .input('QuantityInStock', sql.Int, quantityInStock)
    .input('UnitOfMeasure', sql.NVarChar(50), unitOfMeasure)
    .input('ReorderLevel', sql.Int, reorderLevel)
    .input('Location', sql.NVarChar(100), location)
    .query(
      `INSERT INTO dbo.InventoryItems (
         Name, Description, QuantityInStock, UnitOfMeasure,
         ReorderLevel, Location
       ) VALUES (
         @Name, @Description, @QuantityInStock, @UnitOfMeasure,
         @ReorderLevel, @Location
       );`
    );
  console.log('InventoryItem inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into InventoryTransactions
async function insertInventoryTransaction(
  itemId, changeQuantity, transactionType, performedBy, remarks
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('ItemId', sql.Int, itemId)
    .input('ChangeQuantity', sql.Int, changeQuantity)
    .input('TransactionType', sql.NVarChar(50), transactionType)
    .input('PerformedBy', sql.Int, performedBy)
    .input('Remarks', sql.NVarChar(500), remarks)
    .query(
      `INSERT INTO dbo.InventoryTransactions (
         ItemId, ChangeQuantity, TransactionType, PerformedBy, Remarks
       ) VALUES (
         @ItemId, @ChangeQuantity, @TransactionType, @PerformedBy, @Remarks
       );`
    );
  console.log('InventoryTransaction inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into Beds
async function insertBed(ward, bedNumber, status) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('Ward', sql.NVarChar(100), ward)
    .input('BedNumber', sql.NVarChar(50), bedNumber)
    .input('Status', sql.NVarChar(20), status)
    .query(
      `INSERT INTO dbo.Beds (Ward, BedNumber, Status)
       VALUES (@Ward, @BedNumber, @Status);`
    );
  console.log('Bed inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into Admissions
async function insertAdmission(
  patientId, bedId, dischargedAt, admitBy, dischargeBy
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('PatientId', sql.Int, patientId)
    .input('BedId', sql.Int, bedId)
    .input('DischargedAt', sql.DateTime, dischargedAt)
    .input('AdmitBy', sql.Int, admitBy)
    .input('DischargeBy', sql.Int, dischargeBy)
    .query(
      `INSERT INTO dbo.Admissions (
         PatientId, BedId, DischargedAt, AdmitBy, DischargeBy
       ) VALUES (
         @PatientId, @BedId, @DischargedAt, @AdmitBy, @DischargeBy
       );`
    );
  console.log('Admission inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into Vitals
async function insertVital(
  patientId, vitalType, value, unit, recordedBy
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('PatientId', sql.Int, patientId)
    .input('VitalType', sql.NVarChar(50), vitalType)
    .input('Value', sql.NVarChar(50), value)
    .input('Unit', sql.NVarChar(20), unit)
    .input('RecordedBy', sql.Int, recordedBy)
    .query(
      `INSERT INTO dbo.Vitals (
         PatientId, VitalType, Value, Unit, RecordedBy
       ) VALUES (
         @PatientId, @VitalType, @Value, @Unit, @RecordedBy
       );`
    );
  console.log('Vital inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

// Insert into ReportsHistory
async function insertReportsHistory(
  reportType, parameters, generatedBy, filePath
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('ReportType', sql.NVarChar(100), reportType)
    .input('Parameters', sql.NVarChar(sql.MAX), parameters)
    .input('GeneratedBy', sql.Int, generatedBy)
    .input('FilePath', sql.NVarChar(500), filePath)
    .query(
      `INSERT INTO dbo.ReportsHistory (
         ReportType, Parameters, GeneratedBy, FilePath
       ) VALUES (
         @ReportType, @Parameters, @GeneratedBy, @FilePath
       );`
    );
  console.log('ReportsHistory inserted, rowsAffected:', result.rowsAffected[0]);
  await pool.close();
}

(async () => {
  try {
    // 1) Create DB + tables if missing
    await ensureSchema();

    // 2) Now that HospitalManagementDB exists, add required tables needed for data
    // await insertCarePlan(
    //   123, 
    //   'Hypertension follow-up', 
    //   'Check blood pressure and adjust medication'
    // );

    // await insertPatient(
    //   'abc123456', 
    //   'jane', 
    //   'smith', 
    //   '1992-01-01', 
    //   'X',
    //   'phone', 
    //   'email', 
    //   'address',
    //   'emergencyContactName',
    //   'emergencyContactPhone',
    //   'insuranceProvider', 
    //   'insurancePolicyNumber'
    // )
  } catch (err) {
    console.error('Error during setup/insert:', err);
  }
})();

/*
RUN IN TERMINAL -
  node insertSQLData.js

SQL EXPECTED OUTPUT
  Rows inserted: # 
  (ex. Rows inserted: 1)
*/