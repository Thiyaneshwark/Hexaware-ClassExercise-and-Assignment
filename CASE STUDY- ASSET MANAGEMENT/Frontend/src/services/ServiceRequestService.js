import axios from 'axios';
import Cookies from 'js-cookie';  

const apiUrl = 'https://localhost:7144/api/ServiceRequests'; 

// Function to get the token dynamically from cookies
const getToken = () => {
  const token = Cookies.get("token");  
  if (!token) {
    throw new Error('Authorization token not found in cookies!');
  }
  return `Bearer ${token}`;  
};
export const getUserRole = async () => {
  try {
    const response = await axios.get( apiUrl, {
      headers: {
        Authorization: `Bearer ${Cookies.get('token')}`,
      },
    });

    if (response.status === 200) {
      return response.data.role;  
    } else {
      throw new Error('Failed to fetch user role');
    }
  } catch (error) {
    console.error('Error fetching user role:', error);
    throw error;
  }
};
// Global error handler
const handleError = (error, customMessage) => {
  console.error(customMessage, error);

  if (error.response) {
    // Server responded with error
    console.error('Response Data:', error.response.data);
    console.error('Response Status:', error.response.status);
    throw new Error(`${customMessage}: ${error.response.data.message || 'Server Error'}`);
  } else if (error.request) {
    // No response received
    console.error('Request Error:', error.request);
    throw new Error(`${customMessage}: No response received from the server.`);
  } else {
    // Error during request setup
    console.error('Request Setup Error:', error.message);
    throw new Error(`${customMessage}: ${error.message || 'Unknown Error'}`);
  }
};


// Fetch all service requests
export const getServiceRequests = async () => {
  try {
    const token = getToken();  
    const response = await axios.get(apiUrl, {
      headers: { Authorization: token },
    });
    if (!response.data) {
      throw new Error('No data returned from API');
    }
    return response.data;
  } catch (err) {
    handleError(err, 'Error fetching service requests');
  }
};

// Create a new service request
export const createServiceRequest = async (serviceRequestData) => {
  try {
    const token = getToken(); 
    const response = await axios.post(apiUrl, serviceRequestData, {
      headers: { Authorization: token },
    });
    if (!response.data) {
      throw new Error('No data returned after creating service request');
    }
    return response.data;
  } catch (err) {
    handleError(err, 'Error creating service request');
  }
};

// Update an existing service request
export const updateServiceRequest = async (serviceId, serviceRequestData) => {
  try {
    const token = getToken();  
    const response = await axios.put(`${apiUrl}/${serviceId}`, serviceRequestData, {
      headers: { Authorization: token },
    });
    if (!response.data) {
      throw new Error('No data returned after updating service request');
    }
    return response.data;
  } catch (err) {
    handleError(err, 'Error updating service request');
  }
};

// Delete a service request
export const deleteServiceRequest = async (serviceId) => {
  try {
    const token = getToken();  
    await axios.delete(`${apiUrl}/${serviceId}`, {
      headers: { Authorization: token },
    });
  } catch (err) {
    handleError(err, 'Error deleting service request');
  }
};
