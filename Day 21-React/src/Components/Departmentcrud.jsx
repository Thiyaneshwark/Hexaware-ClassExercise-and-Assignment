import React, { useState, useEffect } from "react";
import {
  getDepartments,
  createDepartment,
  updateDepartment,
  deleteDepartment,
} from "../Services/DepartmentService";

const Department = () => {
  const [departments, setDepartments] = useState([]);
  const [newDepartment, setNewDepartment] = useState({
    id: 0,
    name: "",
    departmentHead: "",
  });

  const [isEdit, setIsEdit] = useState(false);
  const [errors, setErrors] = useState({ name: "", departmentHead: "" });

  useEffect(() => {
    fetchDepartments();
  }, []);

  const fetchDepartments = async () => {
    try {
      const data = await getDepartments();
      setDepartments(data);
      console.log("data", data.length);
    } catch (error) {
      // console.log(error);
    }
  };

  const validateForm = () => {
    let formErrors = {};
    if (!newDepartment.name) formErrors.name = "Name Required.";
    if (!newDepartment.departmentHead)
      formErrors.departmentHead = "DepartmentHead Field Required.";
    setErrors(formErrors);
    return Object.keys(formErrors).length === 0;
  };

  const handleCreate = async () => {
    if (!validateForm()) return;

    try {
      console.log("handleCreate Called ");
      const data = await createDepartment(newDepartment);
      await fetchDepartments();
      // setDepartments([...departments, data]);
      setNewDepartment({ id: 0, name: "", departmentHead: "" });
    } catch (error) {
      // console.log(error);
    }
  };

  const handleUpdate = async () => {
    if (!validateForm()) return;

    try {
      await updateDepartment(newDepartment.id, newDepartment);
      setDepartments(
        departments.map((dept) =>
          dept.id === newDepartment.id ? newDepartment : dept
        )
      );
      setIsEdit(false);
      setNewDepartment({ id: 0, name: "", departmentHead: "" });
    } catch (error) {
      // console.log(error);
    }
  };

  const handleEdit = (department) => {
    setNewDepartment(department);
    setIsEdit(true);
  };

  const handleDelete = async (id) => {
    try {
      await deleteDepartment(id);
      setDepartments(departments.filter((dept) => dept.id !== id));
    } catch (error) {
      //console.log(error);
    }
  };

  return (
    <>
      <div>
        <h1> Departments </h1>
        <ul>
          {departments.map((department) => (
            <li key={department.id}>
              {department.name} - {department.departmentHead}
              <button onClick={() => handleEdit(department)}>Edit</button>
              <button onClick={() => handleDelete(department.id)}>
                Delete
              </button>
            </li>
          ))}
        </ul>
        <div>
          <h2>{isEdit ? "Edit Department" : "Add New Department"}</h2>
          <input
            type="text"
            placeholder="Name"
            value={newDepartment.name}
            onChange={(e) =>
              setNewDepartment({ ...newDepartment, name: e.target.value })
            }
          />
          {errors.name && <span style={{ color: "Red" }}>{errors.name}</span>}
          <input
            type="text"
            placeholder="Department Head"
            value={newDepartment.departmentHead}
            onChange={(e) =>
              setNewDepartment({
                ...newDepartment,
                departmentHead: e.target.value,
              })
            }
          />
          {errors.name && (
            <span style={{ color: "Red" }}>{errors.departmentHead}</span>
          )}

          <button onClick={isEdit ? handleUpdate : handleCreate}>
            {isEdit ? "Update" : "Add"}
          </button>
        </div>
      </div>
    </>
  );
};
export default Department;