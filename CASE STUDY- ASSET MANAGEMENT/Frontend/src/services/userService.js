
import axiosInstance from "../api/axiosInstance";
import API_ROUTES from "../api/apiRoutes";

// Fetch users from the API
export const fetchUsers = async () => {
  try {
    const response = await axiosInstance.get(API_ROUTES.USERS); 
    return response.data; 
  } catch (error) {
    console.error("Error fetching users:", error);
    throw error; 
  }
};
