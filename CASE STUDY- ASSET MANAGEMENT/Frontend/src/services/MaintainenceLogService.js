import axios from "axios";
import Cookies from "js-cookie";

// Base URL for Maintenance Logs
const MAINTENANCE_LOG_BASE_URL = "https://localhost:7144/api/MaintenanceLogs";

// Retrieve token from cookies
const getToken = () => {
  const token = Cookies.get("token");
  return token ? `Bearer ${token}` : null;
};

// Global error handler for consistent error management
const handleError = (error, action) => {
  console.error(`${action} Error:`, error);
  if (error.response) {
    console.error("Response Data:", error.response.data);
    console.error("Response Status:", error.response.status);
    throw new Error(`${action}: ${error.response.data.message || 'Server Error'}`);
  } else if (error.request) {
    console.error("Request Error:", error.request);
    throw new Error(`${action}: No response received from the server.`);
  } else {
    console.error("Request Setup Error:", error.message);
    throw new Error(`${action}: ${error.message || 'Unknown Error'}`);
  }
};

// Get all maintenance logs
export const getMaintenanceLogs = async () => {
  try {
    const token = getToken();
    const response = await axios.get(MAINTENANCE_LOG_BASE_URL, {
      headers: { Authorization: token },
    });
    return response.data;
  } catch (error) {
    handleError(error, "Fetching Maintenance Logs");
  }
};

// Create a new maintenance log
export const createMaintenanceLog = async (maintenanceLog) => {
  try {
    const token = getToken();
    const response = await axios.post(MAINTENANCE_LOG_BASE_URL, maintenanceLog, {
      headers: { Authorization: token },
    });
    return response.data;
  } catch (error) {
    handleError(error, "Creating Maintenance Log");
  }
};

// Update an existing maintenance log
export const updateMaintenanceLog = async (id, maintenanceLog) => {
  try {
    const token = getToken();
    const response = await axios.put(`${MAINTENANCE_LOG_BASE_URL}/${id}`, maintenanceLog, {
      headers: { Authorization: token },
    });
    return response.data;
  } catch (error) {
    handleError(error, "Updating Maintenance Log");
  }
};

// Delete a maintenance log
export const deleteMaintenanceLog = async (id) => {
  try {
    const token = getToken();
    await axios.delete(`${MAINTENANCE_LOG_BASE_URL}/${id}`, {
      headers: { Authorization: token },
    });
    return { message: "Maintenance log deleted successfully" };
  } catch (error) {
    handleError(error, "Deleting Maintenance Log");
  }
};
