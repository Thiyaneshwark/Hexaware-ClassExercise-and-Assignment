import axios from "axios";
import Cookies from "js-cookie";
import { toast } from "react-toastify";

const AUDIT_BASE_URL = process.env.REACT_APP_API_URL || "https://localhost:7144/api/Audits";

const getToken = () => {
  const token = Cookies.get("token");
  return token ? `Bearer ${token}` : null;
};

export const getAudits = async () => {
  try {
    const token = getToken();
    const response = await axios.get(AUDIT_BASE_URL, {
      headers: {
        Authorization: token,
      },
    });

    // Log in development only
    if (process.env.NODE_ENV === "development") {
      console.log("Fetched Audits:", response.data);
    }

    return Array.isArray(response.data?.$values) ? response.data.$values : [];
  } catch (error) {
    handleAxiosError(error);
    return [];
  }
};

export const createAudit = async (audit) => {
  try {
    const token = getToken();
    const response = await axios.post(AUDIT_BASE_URL, audit, {
      headers: {
        Authorization: token,
      },
    });

    console.log("Created New Audit:", response.data);
    return response.data;
  } catch (error) {
    handleAxiosError(error);
  }
};

export const updateAudit = async (id, audit) => {
  try {
    const token = getToken();
    const response = await axios.put(`${AUDIT_BASE_URL}/${id}`, audit, {
      headers: {
        Authorization: token,
      },
    });

    console.log(`Updated Audit [ID: ${id}]:`, response.data);
    return response.data;
  } catch (error) {
    handleAxiosError(error);
  }
};

export const deleteAudit = async (id) => {
  try {
    const token = getToken();
    await axios.delete(`${AUDIT_BASE_URL}/${id}`, {
      headers: {
        Authorization: token,
      },
    });

    console.log(`Deleted Audit [ID: ${id}]`);
    return { message: "Audit deleted successfully" };
  } catch (error) {
    handleAxiosError(error);
  }
};

const handleAxiosError = (error) => {
  if (error.response) {
    console.error("Server Error:", error.response.data);
    toast.error(error.response.data.message || "Server Error");
  } else if (error.request) {
    console.error("No response received:", error.request);
    toast.error("No response received from the server.");
  } else {
    console.error("Request Setup Error:", error.message);
    toast.error(error.message || "Unknown Error");
  }

  if (error.response && error.response.status === 401) {
    toast.error("Session expired. Please log in again.");
    // Redirect to login page or logout logic
  }

  throw error;
};
