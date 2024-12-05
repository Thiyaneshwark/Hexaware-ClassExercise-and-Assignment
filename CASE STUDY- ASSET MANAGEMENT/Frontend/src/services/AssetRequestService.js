import axios from "axios";
import Cookies from "js-cookie";

// Base URL for asset requests API
const ASSET_REQUEST_BASE_URL = "https://localhost:7144/api/AssetRequests";

// Function to get the token dynamically from cookies
const getToken = () => {
  const token = Cookies.get("token");  
  console.log("Token from cookies:", token); 
  return token ? `Bearer ${token}` : null;  
};

// Get all asset requests
export const getAssetRequests = async () => {
  try {
    const token = getToken();  
    const response = await axios.get(ASSET_REQUEST_BASE_URL, {
      headers: {
        Authorization: token,  
      },
    });
    console.log("API Response Data:", response.data);  
    return response.data;
  } catch (error) {
    console.error("Error Fetching Asset Requests:", error);
    throw error;
  }
};

// Create a new asset request
export const createAssetRequest = async (request) => {
  try {
    const token = getToken();  
    const response = await axios.post(ASSET_REQUEST_BASE_URL, request, {
      headers: {
        Authorization: token,  
      },
    });
    console.log("Created New Asset Request:", response.data);  
    return response.data;
  } catch (error) {
    console.error("Error Creating New Asset Request:", error);
    throw error;
  }
};

// Update an existing asset request
export const updateAssetRequest = async (id, request) => {
  try {
    const token = getToken();  
    const response = await axios.put(`${ASSET_REQUEST_BASE_URL}/${id}`, request, {
      headers: {
        Authorization: token,  
      },
    });
    console.log(`Updated Asset Request [ID: ${id}]:`, response.data);  
    return response.data;
  } catch (error) {
    console.error(`Error Updating Asset Request [ID: ${id}]:`, error);
    throw error;
  }
};

// Delete an asset request
export const deleteAssetRequest = async (id) => {
  try {
    const token = getToken();  
    await axios.delete(`${ASSET_REQUEST_BASE_URL}/${id}`, {
      headers: {
        Authorization: token,  
      },
    });
    console.log(`Deleted Asset Request [ID: ${id}]`);  
  } catch (error) {
    console.error(`Error Deleting Asset Request [ID: ${id}]:`, error);
    throw error;
  }
};
