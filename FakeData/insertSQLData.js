/*
USAGE:

1. For bulk data population:
   - Uncomment the populateAllData() call in the main execution
   - Run: node insertSQLData.js

2. For individual inserts:
   - Use the exported functions in your own modules
   - Or uncomment the example calls in the main execution

3. For schema setup only:
   - Run: node insertSQLData.js (default behavior)

SQL EXPECTED OUTPUT:
  Rows inserted: # 
  (ex. Rows inserted: 1)
*/


const fs = require('fs');
const path = require('path');
const sql = require('mssql');
const config = require('./dbConfig');
const schemaDDL = require('./SqlSchema');

async function ensureSchema() {
  try {
    const pool = await sql.connect(config.master);
    // Run each batch separated by "GO"
    for (let batch of schemaDDL.split(/^GO$/m)) {
        const trimmed = batch.trim();
        if (trimmed) await pool.request().batch(trimmed);
    }
  }
  catch (error){
    console.error('SQL error', error);
  }
};

// Connection function for compatibility with createData.js
async function connectToDatabase() {
  return await sql.connect(config.hospital);
}

// Fake Data Generator Class
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

        this.appointmentStatuses = ['Scheduled', 'InProgress', 'Completed', 'Cancelled', 'NoShow'];
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

        this.notificationTypes = [
            'AppointmentChanged', 'AppointmentCancelled', 'AppointmentCreated', 'AdmissionCreated',
            'AdmissionUpdated', 'AdmissionCancelled', 'BedAssigned', 'BedReleased',
            'InventoryItemAdded', 'InventoryItemUpdated', 'InventoryItemRemoved',
            'MedicalHistoryUpdated', 'ReportAdded', 'ReportUpdated', 'StaffAdded',
            'StaffUpdated', 'StaffRemoved', 'PatientAdded', 'PatientUpdated',
            'PatientRemoved', 'VitalSignsUpdated', 'GeneralNotification', 'Message'
        ];

        this.permissions = [
            'ScheduleApp', 'ViewMedicalHistory', 'ManageInventory', 'AccessReports',
            'ManageStaff', 'ViewVitals', 'ManageAdmissions', 'ManageAppointments',
            'ManageBeds', 'ManageDepartments', 'ManageUsers'
        ];

        this.roles = ['Doctor', 'Nurse', 'Administrator', 'Patient', 'AdministrativeStaff'];
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

    generateUsername(firstName, lastName, role) {
        const rolePrefix = role.toLowerCase().replace('administrativestaff', 'admin');
        return `${rolePrefix}.${firstName.toLowerCase()}`;
    }
}

// Data Populator Class
class DataPopulator {
    constructor() {
        this.generator = new FakeDataGenerator();
        this.pool = null;
        this.generatedUserData = [];
        this.generatedChatRooms = [];
    }

    async connect() {
        this.pool = await connectToDatabase();
        console.log('Connected to database for data population');
    }

    // Utility to load JSON file
    loadJSON(filename) {
        const filePath = path.join(__dirname, 'MongoJsonFiles', filename);
        if (fs.existsSync(filePath)) {
            return JSON.parse(fs.readFileSync(filePath, 'utf8'));
        }
        return [];
    }

    // Utility to save JSON file
    saveJSON(filename, data) {
        const dirPath = path.join(__dirname, 'MongoJsonFiles');
        if (!fs.existsSync(dirPath)) {
            fs.mkdirSync(dirPath, { recursive: true });
        }
        const filePath = path.join(dirPath, filename);
        fs.writeFileSync(filePath, JSON.stringify(data, null, 2));
        console.log(`Generated ${filename} with ${data.length} records`);
    }

    // Generate JSON data first
    async generateJSONData() {
        console.log('\nGenerating JSON Data...\n');
        
        // Generate User Data
        await this.generateUserData();
        
        // Generate Notification Data
        await this.generateNotificationData();
        
        // Generate Chat Room Data
        await this.generateChatRoomData();
        
        // Generate Chat Message Data
        await this.generateChatMessageData();
        
        console.log('\nJSON Data Generation Completed!\n');
    }

    async generateUserData(count = 20) {
        console.log(`Generating ${count} User records...`);
        const userData = [];

        for (let i = 0; i < count; i++) {
            const firstName = this.generator.getRandomElement(this.generator.firstNames);
            const lastName = this.generator.getRandomElement(this.generator.lastNames);
            const role = this.generator.getRandomElement(this.generator.roles);
            const orgId = this.generator.generatePatientOrgId();
            
            // Generate appropriate permissions based on role
            let permissions = [];
            if (role === 'Doctor') {
                permissions = ['ViewMedicalHistory', 'ManageInventory', 'AccessReports', 'ViewVitals', 'ScheduleApp'];
            } else if (role === 'Nurse') {
                permissions = ['ViewVitals', 'ScheduleApp'];
            } else if (role === 'AdministrativeStaff') {
                permissions = ['ManageAppointments', 'ManageDepartments'];
            } else if (role === 'Administrator') {
                permissions = ['ManageUsers', 'ManageStaff', 'AccessReports'];
            } else if (role === 'Patient') {
                permissions = ['ScheduleApp', 'ViewMedicalHistory'];
            }

            
            const user = {
                _id: { $oid: orgId },
                Username: this.generator.generateUsername(firstName, lastName, role),
                Email: this.generator.generateEmail(firstName, lastName),
                Password: `abc${this.generator.getRandomInt(100, 999)}`,
                Roles: [role],
                Permissions: permissions,
                Profile: {
                    FullName: `${firstName} ${lastName}`,
                    Phone: this.generator.generatePhoneNumber(),
                    Address: this.generator.generateAddress()
                },
                CreatedAt: { $date: this.generator.getRandomDate(new Date('2024-01-01'), new Date()).toISOString() },
                UpdatedAt: { $date: this.generator.getRandomDate(new Date('2024-01-01'), new Date()).toISOString() }
            };

            userData.push(user);
        }

        this.generatedUserData = userData;
        this.saveJSON('UserData.json', userData);
    }

    async generateNotificationData(count = 50) {
        console.log(`Generating ${count} Notification records...`);
        const notifications = [];

        for (let i = 0; i < count; i++) {
            const user = this.generator.getRandomElement(this.generatedUserData);
            const type = this.generator.getRandomElement(this.generator.notificationTypes);
            
            const notification = {
                UserId: { $oid: user._id.$oid },
                type: type,
                Payload: {
                    message: `${type} notification for ${user.Profile.FullName}`,
                    timestamp: new Date().toISOString()
                },
                isRead: Math.random() < 0.3, // 30% chance of being read
                CreatedAt: this.generator.getRandomDate(new Date('2024-01-01'), new Date()).toISOString()
            };

            notifications.push(notification);
        }

        this.saveJSON('NotificationData.json', notifications);
    }

    async generateChatRoomData(count = 10) {
        console.log(`Generating ${count} Chat Room records...`);
        const chatRooms = [];

        const roomNames = [
            'Emergency Department', 'ICU Team', 'Surgery Planning', 'Nursing Station',
            'Administration', 'Cardiology Unit', 'Pediatrics Ward', 'General Medicine',
            'Radiology Department', 'Pharmacy Team'
        ];

        for (let i = 0; i < count; i++) {
            const roomName = roomNames[i] || `Room ${i + 1}`;
            const participantCount = this.generator.getRandomInt(3, 8);
            const participants = [];

            // Select random participants from generated users
            const shuffledUsers = [...this.generatedUserData].sort(() => 0.5 - Math.random());
            for (let j = 0; j < Math.min(participantCount, shuffledUsers.length); j++) {
                participants.push({ $oid: shuffledUsers[j]._id.$oid });
            }

            const chatRoom = {
                _id: { $oid: this.generator.generatePatientOrgId() },
                Name: roomName,
                Participants: participants,
                CreatedAt: this.generator.getRandomDate(new Date('2024-01-01'), new Date()).toISOString()
            };

            chatRooms.push(chatRoom);
        }

        this.generatedChatRooms = chatRooms;
        this.saveJSON('ChatRoomData.json', chatRooms);
    }

    async generateChatMessageData(count = 100) {
        console.log(`Generating ${count} Chat Message records...`);
        const messages = [];

        const sampleMessages = [
            'Patient in room 302 needs immediate attention',
            'Lab results are ready for review',
            'Surgery scheduled for tomorrow morning',
            'Please update the patient chart',
            'Medication dosage needs adjustment',
            'Family meeting scheduled for 2 PM',
            'Discharge paperwork is ready',
            'New admission in bed 15',
            'Vital signs looking stable',
            'Doctor consultation requested'
        ];

        for (let i = 0; i < count; i++) {
            const room = this.generator.getRandomElement(this.generatedChatRooms);
            const sender = this.generator.getRandomElement(room.Participants);
            const message = this.generator.getRandomElement(sampleMessages);

            const chatMessage = {
                RoomId: { $oid: room._id.$oid },
                SenderId: { $oid: sender.$oid },
                Message: message,
                SentAt: this.generator.getRandomDate(new Date('2024-01-01'), new Date()).toISOString()
            };

            messages.push(chatMessage);
        }

        this.saveJSON('ChatMessageData.json', messages);
    }

    // SQL Population Methods
    async clearExistingData() {
        console.log('Clearing existing data...');
        
        const clearQueries = [
            'DELETE FROM dbo.ReportsHistory',
            'DELETE FROM dbo.Vitals',
            'DELETE FROM dbo.Admissions',
            'DELETE FROM dbo.InventoryTransactions',
            'DELETE FROM dbo.CarePlanUpdates',
            'DELETE FROM dbo.CarePlans',
            'DELETE FROM dbo.Appointments',
            'DELETE FROM dbo.Beds',
            'DELETE FROM dbo.InventoryItems',
            'DELETE FROM dbo.Staff',
            'DELETE FROM dbo.Departments',
            'DELETE FROM dbo.Patients'
        ];

        for (const query of clearQueries) {
            try {
                await this.pool.request().query(query);
                console.log(`Cleared: ${query.split(' ')[2]}`);
            } catch (error) {
                console.log(`Warning clearing ${query.split(' ')[2]}: ${error.message}`);
            }
        }
    }

    async populatePatientsFromJSON() {
        console.log('Populating Patients from JSON data...');
        const userData = this.loadJSON('UserData.json');

        for (const user of userData) {
            const orgId = user._id.$oid;
            const fullName = user.Profile.FullName.split(' ');
            const firstName = fullName.shift();
            const lastName = fullName.join(' ') || '';
            const dob = this.generator.getRandomDate(new Date('1940-01-01'), new Date('2010-12-31'));
            const gender = this.generator.getRandomElement(['M', 'F', 'X']);
            const phone = user.Profile.Phone;
            const email = user.Email;
            const address = user.Profile.Address;
            const emergencyContactName = `${this.generator.getRandomElement(this.generator.firstNames)} ${this.generator.getRandomElement(this.generator.lastNames)}`;
            const emergencyContactPhone = this.generator.generatePhoneNumber();
            const insuranceProvider = this.generator.generateInsuranceProvider();
            const insurancePolicyNumber = this.generator.generatePolicyNumber();
            const createdAt = new Date(user.CreatedAt.$date);
            const updatedAt = new Date(user.UpdatedAt.$date);

            const query = `
                INSERT INTO dbo.Patients (
                    PatientOrgId, FirstName, LastName, DOB, Gender,
                    Phone, Email, Address,
                    EmergencyContactName, EmergencyContactPhone,
                    InsuranceProvider, InsurancePolicyNumber,
                    CreatedAt, UpdatedAt
                )
                VALUES (
                    @orgId, @firstName, @lastName, @dob, @gender,
                    @phone, @email, @address,
                    @emergencyName, @emergencyPhone,
                    @insuranceProvider, @insurancePolicyNumber,
                    @createdAt, @updatedAt
                )
            `;

            await this.pool.request()
                .input('orgId', sql.NVarChar(500), orgId)
                .input('firstName', sql.NVarChar(100), firstName)
                .input('lastName', sql.NVarChar(100), lastName)
                .input('dob', sql.Date, dob)
                .input('gender', sql.Char(1), gender)
                .input('phone', sql.NVarChar(20), phone)
                .input('email', sql.NVarChar(255), email)
                .input('address', sql.NVarChar(500), address)
                .input('emergencyName', sql.NVarChar(200), emergencyContactName)
                .input('emergencyPhone', sql.NVarChar(20), emergencyContactPhone)
                .input('insuranceProvider', sql.NVarChar(200), insuranceProvider)
                .input('insurancePolicyNumber', sql.NVarChar(100), insurancePolicyNumber)
                .input('createdAt', sql.DateTime, createdAt)
                .input('updatedAt', sql.DateTime, updatedAt)
                .query(query);
        }

        console.log(`Inserted ${userData.length} patients from JSON`);
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
        
        console.log(`Inserted ${this.generator.departments.length} departments`);
    }

    async populatePatients(count = 100) {
        console.log(`Populating ${count} additional Patients...`);
        
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
        
        console.log(`Inserted ${count} additional patients`);
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
        
        console.log(`Inserted ${count} staff members`);
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
        
        console.log(`Inserted ${count} beds`);
    }

    async populateInventoryItems() {
        console.log('Populating Inventory Items...');
        
        for (const item of this.generator.inventoryItems) {
            const quantity = this.generator.getRandomInt(10, 500);
            const reorderLevel = Math.floor(quantity * 0.2);
            
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
        
        console.log(`Inserted ${this.generator.inventoryItems.length} inventory items`);
    }

    async populateAppointments(count = 200) {
        console.log(`Populating ${count} Appointments...`);
        
        const patientResult = await this.pool.request().query('SELECT PatientId FROM dbo.Patients');
        const staffResult = await this.pool.request().query('SELECT StaffId FROM dbo.Staff');
        
        const patientIds = patientResult.recordset.map(row => row.PatientId);
        const staffIds = staffResult.recordset.map(row => row.StaffId);
        
        for (let i = 0; i < count; i++) {
            const patientId = this.generator.getRandomElement(patientIds);
            // 20% chance of having no staff assigned (null StaffId)
            const staffId = Math.random() < 0.2 ? null : this.generator.getRandomElement(staffIds);
            const scheduledAt = this.generator.getRandomDate(new Date('2024-01-01'), new Date('2025-12-31'));
            const duration = this.generator.getRandomElement([15, 30, 45, 60, 90]);
            const status = this.generator.getRandomElement(this.generator.appointmentStatuses);
            const reason = this.generator.getRandomElement(this.generator.appointmentReasons);
            
            const query = `
                INSERT INTO dbo.Appointments (
                    PatientId, StaffId, ScheduledAt, DurationMinutes, Status, Reason
                )
                VALUES (
                    @patientId, @staffId, @scheduledAt, @durationMinutes, @status, @reason
                )
            `;
            
            await this.pool.request()
                .input('patientId', sql.Int, patientId)
                .input('staffId', sql.Int, staffId)
                .input('scheduledAt', sql.DateTime, scheduledAt)
                .input('durationMinutes', sql.Int, duration)
                .input('status', sql.NVarChar(50), status)
                .input('reason', sql.NVarChar(500), reason)
                .query(query);
        }
        
        console.log(`Inserted ${count} appointments`);
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
            const createdAt = this.generator.getRandomDate(new Date('2024-01-01'), new Date());
            const isResolved = Math.random() < 0.3;
            const dateResolved = isResolved ? this.generator.getRandomDate(diagnosisDate, new Date()) : null;
            
            const query = `
                INSERT INTO dbo.CarePlans (
                    PatientId, Condition, Description, DiagnosisDate, DateResolved, CreatedAt
                )
                VALUES (
                    @patientId, @condition, @description, @diagnosisDate, @dateResolved, @createdAt
                )
            `;
            
            await this.pool.request()
                .input('patientId', sql.Int, patientId)
                .input('condition', sql.NVarChar(200), condition)
                .input('description', sql.NVarChar(sql.MAX), description)
                .input('diagnosisDate', sql.DateTime, diagnosisDate)
                .input('dateResolved', sql.DateTime, dateResolved)
                .input('createdAt', sql.DateTime, createdAt)
                .query(query);
        }
        
        console.log(`Inserted ${count} care plans`);
    }

    async populateCarePlanUpdates(count = 120) {
        console.log(`Populating ${count} Care Plan Updates...`);
        
        const carePlanResult = await this.pool.request().query('SELECT CarePlanId FROM dbo.CarePlans');
        const appointmentResult = await this.pool.request().query('SELECT AppointmentId FROM dbo.Appointments');
        
        const carePlanIds = carePlanResult.recordset.map(row => row.CarePlanId);
        const appointmentIds = appointmentResult.recordset.map(row => row.AppointmentId);
        
        for (let i = 0; i < count; i++) {
            const carePlanId = this.generator.getRandomElement(carePlanIds);
            const appointmentId = this.generator.getRandomElement(appointmentIds);
            const notes = `Care plan update ${i + 1}: Patient showing improvement with current treatment protocol.`;
            
            const query = `
                INSERT INTO dbo.CarePlanUpdates (
                    CarePlanId, AppointmentId, Notes
                )
                VALUES (
                    @carePlanId, @appointmentId, @notes
                )
            `;
            
            await this.pool.request()
                .input('carePlanId', sql.Int, carePlanId)
                .input('appointmentId', sql.Int, appointmentId)
                .input('notes', sql.NVarChar(sql.MAX), notes)
                .query(query);
        }
        
        console.log(`Inserted ${count} care plan updates`);
    }

    async populateVitals(count = 300) {
        console.log(`Populating ${count} Vital Signs...`);
        
        const patientResult = await this.pool.request().query('SELECT PatientId FROM dbo.Patients');
        const staffResult = await this.pool.request().query(`SELECT StaffId FROM dbo.Staff WHERE StaffType IN ('Doctor', 'Nurse')`);
        
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
        
        console.log(`Inserted ${count} vital signs`);
    }

    async populateInventoryTransactions(count = 200) {
        console.log(`Populating ${count} Inventory Transactions...`);
        
        const itemResult = await this.pool.request().query('SELECT ItemId FROM dbo.InventoryItems');
        const staffResult = await this.pool.request().query('SELECT StaffId FROM dbo.Staff');
        
        const itemIds = itemResult.recordset.map(row => row.ItemId);
        const staffIds = staffResult.recordset.map(row => row.StaffId);
        const transactionTypes = ['Received', 'Dispensed', 'Adjustment'];
        
        for (let i = 0; i < count; i++) {
            const itemId = this.generator.getRandomElement(itemIds);
            const changeQuantity = this.generator.getRandomInt(-20, 50);
            const transactionType = this.generator.getRandomElement(transactionTypes);
            const performedBy = this.generator.getRandomElement(staffIds);
            const transactionDateTime = this.generator.getRandomDate(new Date('2024-01-01'), new Date());
            const remarks = `${transactionType} transaction of ${Math.abs(changeQuantity)} units`;
            
            const query = `
                INSERT INTO dbo.InventoryTransactions (
                    ItemId, ChangeQuantity, TransactionType, PerformedBy, TransactionDateTime, Remarks
                )
                VALUES (
                    @itemId, @changeQuantity, @transactionType, @performedBy, @transactionDateTime, @remarks
                )
            `;
            
            await this.pool.request()
                .input('itemId', sql.Int, itemId)
                .input('changeQuantity', sql.Int, changeQuantity)
                .input('transactionType', sql.NVarChar(50), transactionType)
                .input('performedBy', sql.Int, performedBy)
                .input('transactionDateTime', sql.DateTime, transactionDateTime)
                .input('remarks', sql.NVarChar(500), remarks)
                .query(query);
        }
        
        console.log(`Inserted ${count} inventory transactions`);
    }

    async populateAdmissions(count = 80) {
        console.log(`Populating ${count} Admissions...`);
        
        const patientResult = await this.pool.request().query('SELECT PatientId FROM dbo.Patients');
        const bedResult = await this.pool.request().query('SELECT BedId FROM dbo.Beds');
        const staffResult = await this.pool.request().query('SELECT StaffId FROM dbo.Staff');
        
        const patientIds = patientResult.recordset.map(row => row.PatientId);
        const bedIds = bedResult.recordset.map(row => row.BedId);
        const staffIds = staffResult.recordset.map(row => row.StaffId);
        
        for (let i = 0; i < count; i++) {
            const patientId = this.generator.getRandomElement(patientIds);
            const bedId = this.generator.getRandomElement(bedIds);
            const admittedAt = this.generator.getRandomDate(new Date('2024-01-01'), new Date());
            const admitBy = this.generator.getRandomElement(staffIds);
            const isDischarged = Math.random() < 0.5;
            const dischargedAt = isDischarged ? this.generator.getRandomDate(admittedAt, new Date()) : null;
            const dischargeBy = isDischarged ? this.generator.getRandomElement(staffIds) : null;
            
            const query = `
                INSERT INTO dbo.Admissions (
                    PatientId, BedId, AdmittedAt, DischargedAt, AdmitBy, DischargeBy
                )
                VALUES (
                    @patientId, @bedId, @admittedAt, @dischargedAt, @admitBy, @dischargeBy
                )
            `;
            
            await this.pool.request()
                .input('patientId', sql.Int, patientId)
                .input('bedId', sql.Int, bedId)
                .input('admittedAt', sql.DateTime, admittedAt)
                .input('dischargedAt', sql.DateTime, dischargedAt)
                .input('admitBy', sql.Int, admitBy)
                .input('dischargeBy', sql.Int, dischargeBy)
                .query(query);
        }
        
        console.log(`Inserted ${count} admissions`);
    }

    async populateReportsHistory(count = 100) {
        console.log(`Populating ${count} Reports History...`);
        
        const staffResult = await this.pool.request().query('SELECT StaffId FROM dbo.Staff');
        const staffIds = staffResult.recordset.map(row => row.StaffId);
        const reportTypes = ['MedicalHistory', 'Inventory', 'BedOccupancy', 'DailyVitals', 'AppointmentSummary'];
        
        for (let i = 0; i < count; i++) {
            const reportType = this.generator.getRandomElement(reportTypes);
            const parameters = JSON.stringify({ 
                reportId: i + 1, 
                filter: reportType,
                dateRange: '2024-01-01 to 2024-12-31'
            });
            const generatedAt = this.generator.getRandomDate(new Date('2024-01-01'), new Date());
            const generatedBy = this.generator.getRandomElement(staffIds);
            const filePath = `/reports/${reportType.toLowerCase()}_${i + 1}.pdf`;
            
            const query = `
                INSERT INTO dbo.ReportsHistory (
                    ReportType, Parameters, GeneratedAt, GeneratedBy, FilePath
                )
                VALUES (
                    @reportType, @parameters, @generatedAt, @generatedBy, @filePath
                )
            `;
            
            await this.pool.request()
                .input('reportType', sql.NVarChar(100), reportType)
                .input('parameters', sql.NVarChar(sql.MAX), parameters)
                .input('generatedAt', sql.DateTime, generatedAt)
                .input('generatedBy', sql.Int, generatedBy)
                .input('filePath', sql.NVarChar(500), filePath)
                .query(query);
        }
        
        console.log(`Inserted ${count} reports history entries`);
    }


    async populateAll() {
        try {
            console.log('Starting Data Population Process...\n');
            
            // Step 1: Generate JSON data first
            await this.generateJSONData();
            
            // Step 2: Connect to database and populate SQL data
            await this.connect();
            await this.clearExistingData();
            console.log('');
            
            // Step 3: Populate from JSON first (for users/patients with specific IDs)
            await this.populatePatientsFromJSON();
            
            // Step 4: Populate base tables
            await this.populateDepartments();
            await this.populatePatients(100); // Additional random patients
            await this.populateStaff(50);
            await this.populateBeds(100);
            await this.populateInventoryItems();
            
            // Step 5: Populate dependent tables
            await this.populateAppointments(200);
            await this.populateCarePlans(80);
            await this.populateCarePlanUpdates(120);
            await this.populateVitals(300);
            await this.populateInventoryTransactions(200);
            await this.populateAdmissions(80);
            await this.populateReportsHistory(100);
            
            console.log('\nAll Data Population Completed Successfully!');
            
            // Show summary
            await this.showSummary();
            
        } catch (error) {
            console.error('Error during data population:', error.message);
            throw error;
        } finally {
            if (this.pool) {
                await this.pool.close();
                console.log('\nDatabase connection closed.');
            }
        }
    }

    async showSummary() {
        console.log('\nDatabase Population Summary:');
        console.log('================================');
        
        const tables = [
            'Departments', 'Patients', 'Staff', 'Beds', 'InventoryItems',
            'Appointments', 'CarePlans', 'CarePlanUpdates', 'Vitals',
            'InventoryTransactions', 'Admissions', 'ReportsHistory',
            'Notifications', 'ChatRooms', 'ChatRoomParticipants', 'ChatMessages'
        ];
        
        for (const table of tables) {
            try {
                const result = await this.pool.request().query(`SELECT COUNT(*) as count FROM dbo.${table}`);
                const count = result.recordset[0].count;
                console.log(`${table.padEnd(25)}: ${count.toLocaleString()} records`);
            } catch (error) {
                console.log(`${table.padEnd(25)}: Error getting count - ${error.message}`);
            }
        }
    }
}

// Individual Insert Functions
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
  status, reason
) {
  const pool = await sql.connect(config.hospital);
  const result = await pool.request()
    .input('PatientId', sql.Int, patientId)
    .input('StaffId', sql.Int, staffId) // StaffId can be null
    .input('ScheduledAt', sql.DateTime, scheduledAt)
    .input('DurationMinutes', sql.Int, durationMinutes)
    .input('Status', sql.NVarChar(50), status)
    .input('Reason', sql.NVarChar(500), reason)
    .query(
      `INSERT INTO dbo.Appointments (
         PatientId, StaffId, ScheduledAt, DurationMinutes,
         Status, Reason
       ) VALUES (
         @PatientId, @StaffId, @ScheduledAt, @DurationMinutes,
         @Status, @Reason
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

// Main execution function for bulk data population
async function populateAllData() {
  const populator = new DataPopulator();
  
  try {
    await populator.populateAll();
  } catch (error) {
    console.error('Application error:', error.message);
    process.exit(1);
  }
}

// Export classes and functions for use as module
module.exports = { 
  DataPopulator, 
  FakeDataGenerator,
  connectToDatabase,
  ensureSchema,
  populateAllData,
  // Individual insert functions
  insertPatient,
  insertDepartment,
  insertCarePlan,
  insertCarePlanUpdate,
  insertStaff,
  insertAppointment,
  insertInventoryItem,
  insertInventoryTransaction,
  insertBed,
  insertAdmission,
  insertVital,
  insertReportsHistory
};

// Main execution when run directly
(async () => {
  try {
    // console.log(config.hospital);
    
    // 1) Create DB + tables if missing
    await ensureSchema();

    // 2) Uncomment below to run bulk data population
    await populateAllData();

  } catch (err) {
    console.error('Error during setup/insert:', err);
  }
})();
