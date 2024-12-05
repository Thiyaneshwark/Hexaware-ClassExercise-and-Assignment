const environments = {
  development: {
    apiUrl: 'https://localhost:7144/api/', 
  },
  production: {
    apiUrl: 'https://your-production-url/api/', 
  },
};

export const getEnvironmentConfig = () => environments[process.env.NODE_ENV || 'development'];
