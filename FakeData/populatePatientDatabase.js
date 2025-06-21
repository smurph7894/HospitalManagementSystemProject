const sql = require('mssql');
const { connectToDatabase } = require('./insertSQLData');

// public enum TransactionType
// {
//     Received,
//     Dispensed,
//     Adjustment
// }

// public enum Type
// {
//     AppointmentChanged,
//     AppointmentCancelled,
//     AppointmentCreated,
//     AdmissionCreated,
//     AdmissionUpdated,
//     AdmissionCancelled,
//     BedAssigned,
//     BedReleased,
//     InventoryItemAdded,
//     InventoryItemUpdated,
//     InventoryItemRemoved,
//     MedicalHistoryUpdated,
//     ReportAdded,
//     ReportUpdated,
//     StaffAdded,
//     StaffUpdated,
//     StaffRemoved,
//     PatientAdded,
//     PatientUpdated,
//     PatientRemoved,
//     VitalSignsUpdated,
//     GeneralNotification,
//     Message
// }

// public enum Permission
// {
//     ScheduleApp,
//     ViewMedicalHistory,
//     ManageInventory,
//     AccessReports,
//     ManageStaff,
//     ViewVitals,
//     ManageAdmissions,
//     ManageAppointments,
//     ManageBeds,
//     ManageDepartments,
//     ManageUsers,
// }

// public enum VitalType
// {
//     HeartRate,
//     BloodPressure,
//     Temperature,
//     RespiratoryRate,
//     OxygenSaturation,
//     BloodGlucose
// }


// Fake data generators
class FakeDataGenerator {
    constructor() {
        this.firstNames = [
            'James', 'Mary', 'John', 'Patricia', 'Robert', 'Jennifer', 'Michael', 'Linda',
            'William', 'Elizabeth', 'David', 'Barbara', 'Richard', 'Susan', 'Joseph', 'Jessica',
            'Thomas', 'Sarah', 'Christopher', 'Karen', 'Charles', 'Nancy', 'Daniel', 'Lisa',
            'Matthew', 'Betty', 'Anthony', 'Helen', 'Mark', 'Sandra', 'Donald', 'Donna',
            'Steven', 'Carol', 'Paul', 'Ruth', 'Andrew', 'Sharon', 'Joshua', 'Michelle',
            'Kenneth', 'Laura', 'Kevin', 'Sarah', 'Brian', 'Kimberly', 'George', 'Deborah',
            'Edward', 'Dorothy', 'Ronald', 'Lisa', 'Timothy', 'Nancy', 'Jason', 'Karen'
        ];

        this.lastNames = [
            'Smith', 'Johnson', 'Williams', 'Brown', 'Jones', 'Garcia', 'Miller', 'Davis',
            'Rodriguez', 'Martinez', 'Hernandez', 'Lopez', 'Gonzalez', 'Wilson', 'Anderson', 'Thomas',
            'Taylor', 'Moore', 'Jackson', 'Martin', 'Lee', 'Perez', 'Thompson', 'White',
            'Harris', 'Sanchez', 'Clark', 'Ramirez', 'Lewis', 'Robinson', 'Walker', 'Young',
            'Allen', 'King', 'Wright', 'Scott', 'Torres', 'Nguyen', 'Hill', 'Flores',
            'Green', 'Adams', 'Nelson', 'Baker', 'Hall', 'Rivera', 'Campbell', 'Mitchell',
            'Carter', 'Roberts', 'Gomez', 'Phillips', 'Evans', 'Turner', 'Diaz', 'Parker'
        ];

        this.departments = [
            { name: 'Emergency Medicine', description: 'Emergency and urgent care services' },
            { name: 'Internal Medicine', description: 'General internal medicine and primary care' },
            { name: 'Cardiology', description: 'Heart and cardiovascular care' },
            { name: 'Orthopedics', description: 'Bone, joint, and musculoskeletal care' },
            { name: 'Pediatrics', description: 'Medical care for children and adolescents' },
            { name: 'Radiology', description: 'Medical imaging and diagnostic services' },
            { name: 'Surgery', description: 'Surgical procedures and operations' },
            { name: 'Neurology', description: 'Brain and nervous system care' },
            { name: 'Oncology', description: 'Cancer treatment and care' },
            { name: 'Psychiatry', description: 'Mental health and psychiatric care' }
        ];

        this.staffTypes = ['Doctor', 'Nurse', 'AdministrativeStaff', 'Administrator', 'Patient'];
        
        this.specializations = [
            'General Practice', 'Emergency Medicine', 'Cardiology', 'Orthopedics', 'Pediatrics',
            'Radiology', 'Surgery', 'Neurology', 'Oncology', 'Psychiatry', 'Anesthesiology',
            'Dermatology', 'Gastroenterology', 'Pulmonology', 'Endocrinology'
        ];

        this.conditions = [
            'Hypertension', 'Diabetes Type 2', 'Asthma', 'Chronic Pain', 'Depression',
            'Anxiety Disorder', 'Arthritis', 'Heart Disease', 'COPD', 'Migraine',
            'Back Pain', 'Allergies', 'Insomnia', 'High Cholesterol', 'Obesity'
        ];

        this.appointmentStatuses = ['Scheduled', 'InProgress', 'Completed', 'Cancelled', 'No-Show'];
        this.appointmentReasons = [
            'Annual Physical', 'Follow-up Visit', 'Consultation', 'Emergency Visit',
            'Routine Checkup', 'Vaccination', 'Lab Results Review', 'Medication Review',
            'Symptom Evaluation', 'Preventive Care', 'Chronic Disease Management'
        ];

        this.vitalTypes = [
            { type: 'HeartRate', unit: 'bpm', minValue: 60, maxValue: 100 },
            { type: 'BloodPressure', unit: 'mmHg', minValue: 90, maxValue: 140 },
            { type: 'Temperature', unit: '°F', minValue: 97, maxValue: 99 },
            { type: 'RespiratoryRate', unit: 'breaths/min', minValue: 12, maxValue: 20 },
            { type: 'OxygenSaturation', unit: '%', minValue: 95, maxValue: 100 },
            { type: 'BloodGlucose', unit: 'mg/dL', minValue: 70, maxValue: 140 },
        ];


        this.inventoryItems = [
            { name: 'Surgical Gloves', description: 'Disposable latex gloves', unit: 'boxes', location: 'Supply Room A' },
            { name: 'Syringes', description: '10ml disposable syringes', unit: 'pieces', location: 'Supply Room A' },
            { name: 'Bandages', description: 'Sterile gauze bandages', unit: 'rolls', location: 'Supply Room B' },
            { name: 'IV Bags', description: 'Saline solution IV bags', unit: 'bags', location: 'Pharmacy' },
            { name: 'Thermometers', description: 'Digital thermometers', unit: 'pieces', location: 'Equipment Room' },
            { name: 'Blood Pressure Cuffs', description: 'Adult blood pressure cuffs', unit: 'pieces', location: 'Equipment Room' },
            { name: 'Stethoscopes', description: 'Medical stethoscopes', unit: 'pieces', location: 'Equipment Room' },
            { name: 'Wheelchairs', description: 'Patient wheelchairs', unit: 'pieces', location: 'Equipment Storage' }
        ];

        this.wards = ['ICU', 'General Medicine', 'Surgery', 'Pediatrics', 'Emergency', 'Maternity'];
        this.bedStatuses = ['Available', 'Occupied', 'Maintenance'];
    }

    getRandomElement(array) {
        return array[Math.floor(Math.random() * array.length)];
    }

    getRandomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }

    getRandomDate(start, end) {
        return new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
    }

    generatePhoneNumber() {
        const areaCode = this.getRandomInt(200, 999);
        const exchange = this.getRandomInt(200, 999);
        const number = this.getRandomInt(1000, 9999);
        return `(${areaCode}) ${exchange}-${number}`;
    }

    generateEmail(firstName, lastName) {
        const domains = ['gmail.com', 'yahoo.com', 'hotmail.com', 'outlook.com', 'aol.com'];
        const domain = this.getRandomElement(domains);
        return `${firstName.toLowerCase()}.${lastName.toLowerCase()}@${domain}`;
    }

    generateAddress() {
        const streetNumbers = this.getRandomInt(100, 9999);
        const streetNames = ['Main St', 'Oak Ave', 'Pine Rd', 'Elm St', 'Cedar Ln', 'Maple Dr', 'Park Ave', 'First St'];
        const cities = ['Seattle','Springfield', 'Franklin', 'Georgetown', 'Madison', 'Washington', 'Lincoln', 'Jefferson', 'Clinton'];
        const states = ['WA', 'CA', 'TX', 'FL', 'NY', 'PA', 'IL', 'OH', 'GA', 'NC', 'MI'];
        
        const streetName = this.getRandomElement(streetNames);
        const city = this.getRandomElement(cities);
        const state = this.getRandomElement(states);
        const zipCode = this.getRandomInt(10000, 99999);
        
        return `${streetNumbers} ${streetName}, ${city}, ${state} ${zipCode}`;
    }

    generateInsuranceProvider() {
        const providers = ['Blue Cross Blue Shield', 'Aetna', 'Cigna', 'UnitedHealth', 'Humana', 'Kaiser Permanente', 'Anthem'];
        return this.getRandomElement(providers);
    }

    generatePolicyNumber() {
        return `POL${this.getRandomInt(100000, 999999)}`;
    }

    generatePatientOrgId() {
        // Generate a MongoDB-like ObjectId (24 character hex string)
        const chars = '0123456789abcdef';
        let result = '';
        for (let i = 0; i < 24; i++) {
            result += chars[Math.floor(Math.random() * chars.length)];
        }
        return result;
    }
}

class DatabasePopulator {
    constructor() {
        this.generator = new FakeDataGenerator();
        this.pool = null;
    }

    async connect() {
        this.pool = await connectToDatabase();
        console.log('Connected to database for data population');
    }

    async clearExistingData() {
        console.log('Clearing existing data...');
        
        const clearQueries = [
            'DELETE FROM dbo.ReportsHistory',
            'DELETE FROM dbo.Vitals',
            'DELETE FROM dbo.Admissions',
            'DELETE FROM dbo.Beds',
            'DELETE FROM dbo.InventoryTransactions',
            'DELETE FROM dbo.InventoryItems',
            'DELETE FROM dbo.CarePlanUpdates',
            'DELETE FROM dbo.CarePlans',
            'DELETE FROM dbo.Appointments',
            'DELETE FROM dbo.Staff',
            'DELETE FROM dbo.Departments',
            'DELETE FROM dbo.Patients'
        ];

        for (const query of clearQueries) {
            try {
                await this.pool.request().query(query);
                console.log(`✓ Cleared: ${query.split(' ')[2]}`);
            } catch (error) {
                console.log(`⚠ Warning clearing ${query.split(' ')[2]}: ${error.message}`);
            }
        }
    }

    async populateDepartments() {
        console.log('Populating Departments...');
        
        for (const dept of this.generator.departments) {
            const query = `
                INSERT INTO dbo.Departments (Name, Description)
                VALUES (@name, @description)
            `;
            
            await this.pool.request()
                .input('name', sql.NVarChar(100), dept.name)
                .input('description', sql.NVarChar(500), dept.description)
                .query(query);
        }
        
        console.log(`✓ Inserted ${this.generator.departments.length} departments`);
    }

    async populatePatients(count = 100) {
        console.log(`Populating ${count} Patients...`);
        
        for (let i = 0; i < count; i++) {
            const firstName = this.generator.getRandomElement(this.generator.firstNames);
            const lastName = this.generator.getRandomElement(this.generator.lastNames);
            const gender = this.generator.getRandomElement(['M', 'F']);
            const dob = this.generator.getRandomDate(new Date('1940-01-01'), new Date('2010-12-31'));
            
            const query = `
                INSERT INTO dbo.Patients (
                    PatientOrgId, FirstName, LastName, DOB, Gender, Phone, Email, Address,
                    EmergencyContactName, EmergencyContactPhone, InsuranceProvider, InsurancePolicyNumber
                )
                VALUES (
                    @patientOrgId, @firstName, @lastName, @dob, @gender, @phone, @email, @address,
                    @emergencyContactName, @emergencyContactPhone, @insuranceProvider, @insurancePolicyNumber
                )
            `;
            
            await this.pool.request()
                .input('patientOrgId', sql.NVarChar(500), this.generator.generatePatientOrgId())
                .input('firstName', sql.NVarChar(100), firstName)
                .input('lastName', sql.NVarChar(100), lastName)
                .input('dob', sql.Date, dob)
                .input('gender', sql.Char(1), gender)
                .input('phone', sql.NVarChar(20), this.generator.generatePhoneNumber())
                .input('email', sql.NVarChar(255), this.generator.generateEmail(firstName, lastName))
                .input('address', sql.NVarChar(500), this.generator.generateAddress())
                .input('emergencyContactName', sql.NVarChar(200), 
                    `${this.generator.getRandomElement(this.generator.firstNames)} ${this.generator.getRandomElement(this.generator.lastNames)}`)
                .input('emergencyContactPhone', sql.NVarChar(20), this.generator.generatePhoneNumber())
                .input('insuranceProvider', sql.NVarChar(200), this.generator.generateInsuranceProvider())
                .input('insurancePolicyNumber', sql.NVarChar(100), this.generator.generatePolicyNumber())
                .query(query);
        }
        
        console.log(`✓ Inserted ${count} patients`);
    }

    async populateStaff(count = 50) {
        console.log(`Populating ${count} Staff members...`);
        
        // Get department IDs
        const deptResult = await this.pool.request().query('SELECT DepartmentID FROM dbo.Departments');
        const departmentIds = deptResult.recordset.map(row => row.DepartmentID);
        
        for (let i = 0; i < count; i++) {
            const firstName = this.generator.getRandomElement(this.generator.firstNames);
            const lastName = this.generator.getRandomElement(this.generator.lastNames);
            const staffType = this.generator.getRandomElement(this.generator.staffTypes);
            const specialization = this.generator.getRandomElement(this.generator.specializations);
            const departmentId = this.generator.getRandomElement(departmentIds);
            const hireDate = this.generator.getRandomDate(new Date('2010-01-01'), new Date());
            
            const query = `
                INSERT INTO dbo.Staff (
                    Name, StaffType, Specialization, DepartmentId, HireDate, Phone, Email
                )
                VALUES (
                    @name, @staffType, @specialization, @departmentId, @hireDate, @phone, @email
                )
            `;
            
            await this.pool.request()
                .input('name', sql.VarChar(100), `${firstName} ${lastName}`)
                .input('staffType', sql.VarChar(50), staffType)
                .input('specialization', sql.VarChar(100), specialization)
                .input('departmentId', sql.Int, departmentId)
                .input('hireDate', sql.Date, hireDate)
                .input('phone', sql.VarChar(20), this.generator.generatePhoneNumber())
                .input('email', sql.VarChar(255), this.generator.generateEmail(firstName, lastName))
                .query(query);
        }
        
        console.log(`✓ Inserted ${count} staff members`);
    }

    async populateAppointments(count = 200) {
        console.log(`Populating ${count} Appointments...`);
        
        // Get patient and staff IDs
        const patientResult = await this.pool.request().query('SELECT PatientId FROM dbo.Patients');
        const staffResult = await this.pool.request().query('SELECT StaffId FROM dbo.Staff');
        
        const patientIds = patientResult.recordset.map(row => row.PatientId);
        const staffIds = staffResult.recordset.map(row => row.StaffId);
        
        for (let i = 0; i < count; i++) {
            const patientId = this.generator.getRandomElement(patientIds);
            const staffId = this.generator.getRandomElement(staffIds);
            const createdBy = this.generator.getRandomElement(staffIds);
            const scheduledAt = this.generator.getRandomDate(new Date('2024-01-01'), new Date('2025-12-31'));
            const duration = this.generator.getRandomElement([15, 30, 45, 60, 90]);
            const status = this.generator.getRandomElement(this.generator.appointmentStatuses);
            const reason = this.generator.getRandomElement(this.generator.appointmentReasons);
            
            const query = `
                INSERT INTO dbo.Appointments (
                    PatientId, StaffId, ScheduledAt, DurationMinutes, Status, Reason, CreatedBy
                )
                VALUES (
                    @patientId, @staffId, @scheduledAt, @durationMinutes, @status, @reason, @createdBy
                )
            `;
            
            await this.pool.request()
                .input('patientId', sql.Int, patientId)
                .input('staffId', sql.Int, staffId)
                .input('scheduledAt', sql.DateTime, scheduledAt)
                .input('durationMinutes', sql.Int, duration)
                .input('status', sql.NVarChar(50), status)
                .input('reason', sql.NVarChar(500), reason)
                .input('createdBy', sql.Int, createdBy)
                .query(query);
        }
        
        console.log(`✓ Inserted ${count} appointments`);
    }

    async populateCarePlans(count = 80) {
        console.log(`Populating ${count} Care Plans...`);
        
        const patientResult = await this.pool.request().query('SELECT PatientId FROM dbo.Patients');
        const patientIds = patientResult.recordset.map(row => row.PatientId);
        
        for (let i = 0; i < count; i++) {
            const patientId = this.generator.getRandomElement(patientIds);
            const condition = this.generator.getRandomElement(this.generator.conditions);
            const description = `Treatment plan for ${condition.toLowerCase()}. Regular monitoring and medication management required.`;
            const diagnosisDate = this.generator.getRandomDate(new Date('2023-01-01'), new Date());
            const isResolved = Math.random() < 0.3; // 30% chance of being resolved
            const dateResolved = isResolved ? this.generator.getRandomDate(diagnosisDate, new Date()) : null;
            
            const query = `
                INSERT INTO dbo.CarePlans (
                    PatientId, Condition, Description, DiagnosisDate, DateResolved
                )
                VALUES (
                    @patientId, @condition, @description, @diagnosisDate, @dateResolved
                )
            `;
            
            await this.pool.request()
                .input('patientId', sql.Int, patientId)
                .input('condition', sql.NVarChar(200), condition)
                .input('description', sql.NVarChar(sql.MAX), description)
                .input('diagnosisDate', sql.DateTime, diagnosisDate)
                .input('dateResolved', sql.DateTime, dateResolved)
                .query(query);
        }
        
        console.log(`✓ Inserted ${count} care plans`);
    }

    async populateVitals(count = 300) {
        console.log(`Populating ${count} Vital Signs...`);
        
        const patientResult = await this.pool.request().query('SELECT PatientId FROM dbo.Patients');
        const staffResult = await this.pool.request().query('SELECT StaffId FROM dbo.Staff WHERE StaffType IN (\'Doctor\', \'Nurse\')');
        
        const patientIds = patientResult.recordset.map(row => row.PatientId);
        const staffIds = staffResult.recordset.map(row => row.StaffId);
        
        for (let i = 0; i < count; i++) {
            const patientId = this.generator.getRandomElement(patientIds);
            const staffId = this.generator.getRandomElement(staffIds);
            const vitalType = this.generator.getRandomElement(this.generator.vitalTypes);
            const value = this.generator.getRandomInt(vitalType.minValue, vitalType.maxValue);
            const recordedAt = this.generator.getRandomDate(new Date('2024-01-01'), new Date());
            
            const query = `
                INSERT INTO dbo.Vitals (
                    PatientId, VitalType, Value, Unit, RecordedAt, RecordedBy
                )
                VALUES (
                    @patientId, @vitalType, @value, @unit, @recordedAt, @recordedBy
                )
            `;
            
            await this.pool.request()
                .input('patientId', sql.Int, patientId)
                .input('vitalType', sql.NVarChar(50), vitalType.type)
                .input('value', sql.NVarChar(50), value.toString())
                .input('unit', sql.NVarChar(20), vitalType.unit)
                .input('recordedAt', sql.DateTime, recordedAt)
                .input('recordedBy', sql.Int, staffId)
                .query(query);
        }
        
        console.log(`✓ Inserted ${count} vital signs`);
    }

    async populateBeds(count = 100) {
        console.log(`Populating ${count} Beds...`);
        
        for (let i = 0; i < count; i++) {
            const ward = this.generator.getRandomElement(this.generator.wards);
            const bedNumber = `${ward.substring(0, 3).toUpperCase()}-${String(i + 1).padStart(3, '0')}`;
            const status = this.generator.getRandomElement(this.generator.bedStatuses);
            
            const query = `
                INSERT INTO dbo.Beds (Ward, BedNumber, Status)
                VALUES (@ward, @bedNumber, @status)
            `;
            
            await this.pool.request()
                .input('ward', sql.NVarChar(100), ward)
                .input('bedNumber', sql.NVarChar(50), bedNumber)
                .input('status', sql.NVarChar(20), status)
                .query(query);
        }
        
        console.log(`✓ Inserted ${count} beds`);
    }

    async populateInventoryItems() {
        console.log('Populating Inventory Items...');
        
        for (const item of this.generator.inventoryItems) {
            const quantity = this.generator.getRandomInt(10, 500);
            const reorderLevel = Math.floor(quantity * 0.2); // 20% of current stock
            
            const query = `
                INSERT INTO dbo.InventoryItems (
                    Name, Description, QuantityInStock, UnitOfMeasure, ReorderLevel, Location
                )
                VALUES (
                    @name, @description, @quantityInStock, @unitOfMeasure, @reorderLevel, @location
                )
            `;
            
            await this.pool.request()
                .input('name', sql.NVarChar(200), item.name)
                .input('description', sql.NVarChar(500), item.description)
                .input('quantityInStock', sql.Int, quantity)
                .input('unitOfMeasure', sql.NVarChar(50), item.unit)
                .input('reorderLevel', sql.Int, reorderLevel)
                .input('location', sql.NVarChar(100), item.location)
                .query(query);
        }
        
        console.log(`✓ Inserted ${this.generator.inventoryItems.length} inventory items`);
    }

    async populateAll() {
        try {
            await this.connect();
            
            console.log('Starting database population...\n');
            
            // Clear existing data first
            await this.clearExistingData();
            console.log('');
            
            // Populate in order of dependencies
            await this.populateDepartments();
            await this.populatePatients(100);
            await this.populateStaff(50);
            await this.populateAppointments(200);
            await this.populateCarePlans(80);
            await this.populateVitals(300);
            await this.populateBeds(100);
            await this.populateInventoryItems();
            
            console.log('\n✅ Database population completed successfully!');
            
            // Show summary
            await this.showSummary();
            
        } catch (error) {
            console.error('❌ Error during database population:', error.message);
            throw error;
        } finally {
            if (this.pool) {
                await this.pool.close();
                console.log('\nDatabase connection closed.');
            }
        }
    }

    async showSummary() {
        console.log('\n📊 Database Population Summary:');
        console.log('================================');
        
        const tables = [
            'Departments', 'Patients', 'Staff', 'Appointments', 
            'CarePlans', 'Vitals', 'Beds', 'InventoryItems'
        ];
        
        for (const table of tables) {
            try {
                const result = await this.pool.request().query(`SELECT COUNT(*) as count FROM dbo.${table}`);
                const count = result.recordset[0].count;
                console.log(`${table.padEnd(20)}: ${count.toLocaleString()} records`);
            } catch (error) {
                console.log(`${table.padEnd(20)}: Error getting count`);
            }
        }
    }
}

// Main execution
async function main() {
    const populator = new DatabasePopulator();
    
    try {
        await populator.populateAll();
    } catch (error) {
        console.error('Application error:', error.message);
        process.exit(1);
    }
}

// Export for use as module
module.exports = { DatabasePopulator, FakeDataGenerator };

// Run if executed directly
if (require.main === module) {
    main();
}
