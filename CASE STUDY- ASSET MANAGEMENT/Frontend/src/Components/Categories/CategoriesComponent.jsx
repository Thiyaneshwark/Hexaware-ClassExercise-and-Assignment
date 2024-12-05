import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
  getCategories,
  createCategory,
  updateCategory,
  deleteCategory,
} from "../../services/CategoryService";
import Cookies from "js-cookie";
import {jwtDecode} from "jwt-decode";

const CategoriesComponent = () => {
  const [categories, setCategories] = useState([]);
  const [newCategory, setNewCategory] = useState("");
  const [editingCategoryId, setEditingCategoryId] = useState(null);
  const [editingCategoryName, setEditingCategoryName] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    fetchCategories();
  }, []);

  const token = Cookies.get("token");
  let decoded = null;
  if (token) {
    decoded = jwtDecode(token);
  }

  const fetchCategories = async () => {
    try {
      const data = await getCategories();
      const categoriesArray = data.$values || [];
      setCategories(categoriesArray);
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };

  const handleCreateCategory = async () => {
    try {
      if (!newCategory) {
        alert("Category name is required!");
        return;
      }
      await createCategory({ categoryName: newCategory });
      fetchCategories();
      setNewCategory("");
    } catch (error) {
      console.error("Error creating category:", error);
    }
  };

  const handleUpdateCategory = async (id) => {
    if (!editingCategoryName) {
      alert("Please provide a valid category name");
      return;
    }

    try {
      await updateCategory(id, { categoryName: editingCategoryName });
      setEditingCategoryId(null);
      fetchCategories();
    } catch (error) {
      console.error("Error updating category:", error);
    }
  };

  const handleDeleteCategory = async (id) => {
    try {
      await deleteCategory(id);
      fetchCategories();
    } catch (error) {
      console.error("Error deleting category:", error);
    }
  };

  const enableEditing = (id, name) => {
    setEditingCategoryId(id);
    setEditingCategoryName(name);
  };
  return (
    <div style={styles.container}>
      <button style={styles.backButton} onClick={() => navigate(-1)}>
        Back
      </button>
      <div style={styles.userRoleBar}>
        {decoded ? (
          <div style={styles.userRole}>User Role: <span style={styles.roleText}>{decoded.role}</span></div>
        ) : (
          <div style={styles.userRole}>No user role found</div>
        )}
      </div>

      {decoded?.role === "Admin" && (
        <div style={styles.formWrapper}>
          <input
            type="text"
            value={newCategory}
            onChange={(e) => setNewCategory(e.target.value)}
            placeholder="Enter new category name"
            style={styles.input}
          />
          <button style={styles.submitButton} onClick={handleCreateCategory}>
            Submit New Category
          </button>
        </div>
      )}

      <h1 style={styles.heading}>Categories List</h1>
      {categories && categories.length > 0 ? (
        <table style={styles.table}>
          <thead>
            <tr>
              <th style={styles.tableHeader}>Category Name</th>
              <th style={styles.tableHeader}>Actions</th>
            </tr>
          </thead>
          <tbody>
            {categories.map((category) => (
              <tr key={category.categoryId} style={styles.tableRow}>
                <td style={styles.tableCell}>
                  {editingCategoryId === category.categoryId ? (
                    <input
                      type="text"
                      value={editingCategoryName}
                      onChange={(e) => setEditingCategoryName(e.target.value)}
                      style={styles.input}
                    />
                  ) : (
                    category.categoryName
                  )}
                </td>
                <td style={styles.tableCell}>
                  {decoded?.role === "Admin" && (
                    <>
                      {editingCategoryId === category.categoryId ? (
                        <>
                          <button
                            style={styles.actionButton}
                            onClick={() => handleUpdateCategory(category.categoryId)}
                          >
                            Save
                          </button>
                          <button
                            style={styles.actionButton}
                            onClick={() => setEditingCategoryId(null)}
                          >
                            Cancel
                          </button>
                        </>
                      ) : (
                        <>
                          <button
                            style={styles.actionButton}
                            onClick={() => enableEditing(category.categoryId, category.categoryName)}
                          >
                            Update
                          </button>
                          <button
                            style={styles.actionButton}
                            onClick={() => handleDeleteCategory(category.categoryId)}
                          >
                            Delete
                          </button>
                        </>
                      )}
                    </>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p style={styles.noCategories}>No categories available</p>
      )}
    </div>
  );
};
const styles = {
  container: {
    padding: "20px",
    fontFamily: "'Arial', sans-serif",
    backgroundColor: "#f0f8ff", // Light Alice Blue
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    width: "100%",
    color: "#000",
  },
  backButton: {
    position: "absolute",
    top: "10px",
    right: "10px",
    backgroundColor: "#007bff", // Blue color
    color: "#fff",
    padding: "10px 20px",
    border: "none",
    cursor: "pointer",
    borderRadius: "4px",
    fontSize: "14px",
    fontWeight: "bold",
  },
  userRoleBar: {
    backgroundColor: "#007bff",
    color: "#fff",
    padding: "10px 20px",
    width: "10%",
    textAlign: "center",
    marginBottom: "20px",
    borderRadius: "8px",
    boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
  },
  userRole: {
    marginBottom: "1px",
    fontSize: "18px",
  },
  formWrapper: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    marginBottom: "20px",
    width: "40%",
  },
  input: {
    padding: "12px",
    margin: "8px 10px 8px 0",
    border: "1px solid #ccc",
    borderRadius: "4px",
    backgroundColor: "#ffffff", // White background for inputs
    color: "#000", // Black text color for input
    fontSize: "14px",
    flex: "1",
  },
  submitButton: {
    backgroundColor: "#007bff", // Blue color
    color: "#fff",
    padding: "12px 24px",
    border: "none",
    cursor: "pointer",
    borderRadius: "4px",
    fontSize: "16px",
    fontWeight: "bold",
  },
  heading: {
    textAlign: "center",
    marginBottom: "20px",
    color: "#000",
    fontSize: "24px",
    fontWeight: "bold",
  },
  table: {
    width: "50%",
    marginTop: "20px",
    borderCollapse: "collapse",
    color: "#000",
    borderRadius: "8px",
    boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
  },
  tableHeader: {
    backgroundColor: "#333",
    color: "#fff",
    textAlign: "left",
    padding: "12px",
    fontWeight: "bold",
  },
  tableCell: {
    padding: "12px",
    border: "1px solid #ddd",
    backgroundColor: "#f9f9f9",
    color: "#000",
  },
  tableRow: {
    textAlign: "left",
    color: "#000",
  },
  actionButton: {
    backgroundColor: "#007bff",
    color: "#fff",
    padding: "8px 16px",
    margin: "5px",
    border: "none",
    cursor: "pointer",
    borderRadius: "4px",
    fontSize: "14px",
    fontFamily: "'Arial', sans-serif",
    fontWeight: "bold",
  },
  noCategories: {
    fontSize: "18px",
    color: "#333",
  },
};

export default CategoriesComponent;
