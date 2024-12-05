import axios from "axios";
import Cookies from "js-cookie"; 

const ASSET_ALLOCATION_BASE_URL = "https://localhost:7144/api/AssetAllocations";

// Function to get the token dynamically from cookies
const getToken = () => {
  const token = Cookies.get("token");  
  return token ? `Bearer ${token}` : null;  
};

// Get all asset allocations
export const getAssetAllocations = async () => {
  try {
    const token = getToken();  
    const response = await axios.get(ASSET_ALLOCATION_BASE_URL, {
      headers: {
        Authorization: token,  
      },
    });
    console.log("Fetched Asset Allocations:", response.data);  
    return response.data;
  } catch (error) {
    console.error("Error Fetching Asset Allocations", error);  
    throw error;
  }
};

// Create a new asset allocation
export const createAssetAllocation = async (allocation) => {
  try {
    const token = getToken();  
    const response = await axios.post(ASSET_ALLOCATION_BASE_URL, allocation, {
      headers: {
        Authorization: token,  
      },
    });
    console.log("Created New Asset Allocation:", response.data);  
    return response.data;
  } catch (error) {
    console.error("Error Creating New Asset Allocation", error);  
    throw error;
  }
};

// Update an existing asset allocation
export const updateAssetAllocation = async (id, allocation) => {
  try {
    const token = getToken();  
    const response = await axios.put(`${ASSET_ALLOCATION_BASE_URL}/${id}`, allocation, {
      headers: {
        Authorization: token,  
      },
    });
    console.log(`Updated Asset Allocation [ID: ${id}]:`, response.data);  
    return response.data;
  } catch (error) {
    console.error(`Error Updating Asset Allocation [ID: ${id}]`, error);  
    throw error;
  }
};

// Delete an asset allocation
export const deleteAssetAllocation = async (id) => {
  try {
    const token = getToken();  
    await axios.delete(`${ASSET_ALLOCATION_BASE_URL}/${id}`, {
      headers: {
        Authorization: token,  
      },
    });
    console.log(`Deleted Asset Allocation [ID: ${id}]`);  
  } catch (error) {
    console.error(`Error Deleting Asset Allocation [ID: ${id}]`, error);  
    throw error;
  }
};
