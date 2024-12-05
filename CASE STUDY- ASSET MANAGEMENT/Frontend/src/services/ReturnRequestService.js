import axios from 'axios';
import Cookies from 'js-cookie';

const RETURN_REQUEST_BASE_URL = 'https://localhost:7144/api/ReturnRequests';

// Function to get the token dynamically from cookies
const getToken = () => {
  const token = Cookies.get('token'); 
  return token ? `Bearer ${token}` : null; 
};

// Global error handler function for consistent error management
const handleError = (error, action) => {
  console.error(`${action} Error:`, error); 
  if (error.response) {
    // Server responded with an error
    console.error('Response Data:', error.response.data);
    console.error('Response Status:', error.response.status);
    throw new Error(`${action}: ${error.response.data.message || 'Server Error'}`);
  } else if (error.request) {
    // No response received
    console.error('Request Error:', error.request);
    throw new Error(`${action}: No response received from the server.`);
  } else {
    // Error during request setup
    console.error('Request Setup Error:', error.message);
    throw new Error(`${action}: ${error.message || 'Unknown Error'}`);
  }
};

// Get all return requests
export const getReturnRequests = async () => {
  try {
    const token = getToken(); 
    const response = await axios.get(RETURN_REQUEST_BASE_URL, {
      headers: { Authorization: token }, 
    });
    return response.data;
  } catch (error) {
    handleError(error, 'Fetching Return Requests');
  }
};

// Create a new return request
export const createReturnRequest = async (returnRequest) => {
  try {
    const token = getToken(); 
    const response = await axios.post(RETURN_REQUEST_BASE_URL, returnRequest, {
      headers: { Authorization: token }, 
    });
    return response.data;
  } catch (error) {
    handleError(error, 'Creating New Return Request');
  }
};

// Update an existing return request
export const updateReturnRequest = async (id, returnRequest) => {
  try {
    const token = getToken(); 
    const response = await axios.put(`${RETURN_REQUEST_BASE_URL}/${id}`, returnRequest, {
      headers: { Authorization: token }, 
    });
    return response.data;
  } catch (error) {
    handleError(error, 'Updating Return Request');
  }
};

// Delete a return request
export const deleteReturnRequest = async (id) => {
  try {
    const token = getToken(); 
    await axios.delete(`${RETURN_REQUEST_BASE_URL}/${id}`, {
      headers: { Authorization: token }, 
    });
  } catch (error) {
    handleError(error, 'Deleting Return Request');
  }
};
