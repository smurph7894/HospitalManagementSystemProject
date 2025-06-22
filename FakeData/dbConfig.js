// dbConfig.js
require('dotenv').config();

module.exports = {
  master: {
    server:        'LITTLE_JUICY\\SQLEXPRESS',  // server with instance name
    database:      'master',
    user:          process.env.DB_USER,
    password:      process.env.DB_PASS,
    options: {
      encrypt:           false,          // disable TLS/SSPI lookups
      trustServerCertificate: true       // for local development
    }
  },
  hospital: {
    server:        'LITTLE_JUICY\\SQLEXPRESS',
    database:      'HospitalManagementDB',
    user:          process.env.DB_USER,
    password:      process.env.DB_PASS,
    options: {
      encrypt:           false,
      trustServerCertificate: true
    }
  }
};
