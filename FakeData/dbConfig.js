// dbConfig.js
require(`dotenv`).config();

// module.exports = {
//   hospital: {
//     connectionString: 
//       "Driver={ODBC Driver 18 for SQL Server};" +
//       "Server=LITTLE-JUICY\\SQLEXPRESS;" +
//       "Database=HospitalManagementDB;" +
//       `UID=${process.env.DB_User};`  +
//       `PWD=${process.env.DB_Pass};` +
//       "Encrypt=yes;" +
//       "TrustServerCertificate=yes;",
//   }
// };

module.exports = {
  user: process.env.DB_USER,
  password: process.env.DB_PASS,
  server: process.env.DB_SERVER,
  port: Number(process.env.DB_PORT),
  database: process.env.DB_NAME,
  options: {
    encrypt: process.env.DB_ENCRYPT === 'true',
    trustServerCertificate: process.env.DB_TRUST_CERT === 'true'
  },
  // optional pool settings:
  pool: {
    max: 10,
    min: 0,
    idleTimeoutMillis: 30000
  }
};
