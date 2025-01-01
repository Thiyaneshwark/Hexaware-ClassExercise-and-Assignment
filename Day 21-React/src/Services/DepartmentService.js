import axios from "axios";

const BASE_URL = "https://localhost:7253/api/Departments";
export const getDepartments = async () => {
  try {
    const response = await axios.get(BASE_URL);
    return response.data;
  } catch (error) {
    console.error("Error Fetching Departments", error);
    throw error;
  }
};

export const createDepartment = async (department) => {
  try {
    console.log(department);
    console.log("CreateDepartment  Called from service");
    const response = await axios.post(BASE_URL, department);
    console.log("resonse from api", response.data);
    return response.data;
  } catch (error) {
    console.error("Error While creating New Department", error);
    throw error;
  }
};

export const updateDepartment = async (id, department) => {
  try {
    const response = await axios.put(`${BASE_URL}/`, department);
    return response.data;
  } catch (error) {
    console.error("Error While updating  Department", error);
    throw error;
  }
};

export const deleteDepartment = async (id) => {
  try {
    await axios.delete(`${BASE_URL}?id=${id}`);
  } catch (error) {
    console.error("Error While Deleting  Department", error);
    throw error;
  }
};