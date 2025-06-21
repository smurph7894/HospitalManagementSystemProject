// dbConfig.js
module.exports = {
  master: {
    // embed the ODBC driver name you installed
    connectionString: 
      "Driver={ODBC Driver 18 for SQL Server};" +
      "Server=LITTLE_JUICY\\SQLEXPRESS;" +
      "Database=master;" +
      "Trusted_Connection=Yes;"+
      "Encrypt=yes;" +                 
      "TrustServerCertificate=yes;",
    driver: 'msnodesqlv8'
  },
  hospital: {
    connectionString: 
      "Driver={ODBC Driver 18 for SQL Server};" +
      "Server=LITTLE-JUICY\\SQLEXPRESS;" +
      "Database=HospitalManagementDB;" +
      "Trusted_Connection=Yes;" +
      "Encrypt=yes;" +
      "TrustServerCertificate=yes;",
    driver: 'msnodesqlv8'
  }
};
