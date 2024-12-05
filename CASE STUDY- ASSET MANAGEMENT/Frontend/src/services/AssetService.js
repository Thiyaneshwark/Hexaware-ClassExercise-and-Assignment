import axios from "axios";
import Cookies from "js-cookie"; 
import { toast } from "react-toastify";

// Asset Base URL
const ASSET_BASE_URL = "https://localhost:7144/api/Assets";

// Function to get the token dynamically from cookies
const getToken = () => {
  const token = Cookies.get("token");  
  return token ? `Bearer ${token}` : null; 
};

// Get all assets
export const getAssets = async () => {
  try {
    const token = getToken();  
    const response = await axios.get(ASSET_BASE_URL, {
      headers: {
        Authorization: token, 
      },
    });
    console.log("Fetched Assets:", response.data); 
    return response.data;
  } catch (error) {
    handleAxiosError(error);
  }
};

// Create a new asset
// Create a new asset
export const createAsset = async (asset) => {
  try {
    const token = getToken();
    const response = await axios.post(ASSET_BASE_URL, asset, {
      headers: { Authorization: token },
    });
    return response.data;
  } catch (error) {
    handleAxiosError(error);
    throw error; // Rethrow for proper error handling in the component
  }
};

// Update an existing asset
export const updateAsset = async (id, asset) => {
  try {
    const token = getToken();
    const response = await axios.put(`${ASSET_BASE_URL}/${id}`, asset, {
      headers: { Authorization: token },
    });
    return response.data;
  } catch (error) {
    handleAxiosError(error);
    throw error; // Rethrow for proper error handling in the component
  }
};


// Delete an asset
export const deleteAsset = async (id) => {
  try {
    const token = getToken();  
    await axios.delete(`${ASSET_BASE_URL}/${id}`, {
      headers: {
        Authorization: token, 
      },
    });
    console.log(`Deleted Asset [ID: ${id}]`); 
    return { message: "Asset deleted successfully" };
  } catch (error) {
    handleAxiosError(error);
  }
};

// Global error handler
const handleAxiosError = (error) => {
  if (error.response) {
    // Server responded with error
    console.error("Server Error:", error.response.data); 
    toast.error(error.response.data.message || "Server Error");
  } else if (error.request) {
    // No response received
    console.error("No response received:", error.request); 
    toast.error("No response received from the server.");
  } else {
    // Error during request setup
    console.error("Request Setup Error:", error.message); 
    toast.error(error.message || "Unknown Error");
  }
  throw error; 
};
