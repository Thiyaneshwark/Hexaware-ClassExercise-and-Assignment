import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
  getAssets,
  createAsset,
  updateAsset,
  deleteAsset,
} from "../../services/AssetService";
import { getCategories } from "../../services/CategoryService";
import { getSubCategories } from "../../services/SubCategoryService";
import Cookies from "js-cookie";
import {jwtDecode} from "jwt-decode";
import { toast } from "react-toastify";

const Asset = () => {
  const [decoded, setDecoded] = useState(null);
  const [assets, setAssets] = useState([]);
  const [categories, setCategories] = useState([]);
  const [subCategories, setSubCategories] = useState([]);
  const [newAsset, setNewAsset] = useState({
    assetId: "",
    assetName: "",
    categoryId: "",
    subCategoryId: "",
    value: 0,
    manufacturingDate: "",
    location: "",
  });
  const [showCreateAssetForm, setShowCreateAssetForm] = useState(false);
  const [loading, setLoading] = useState(false);
  const [actionLogs, setActionLogs] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchAssets();
    fetchCategories();
    fetchSubCategories();
    checkUserRole();
  }, []);

  const checkUserRole = () => {
    const token = Cookies.get("token");
    if (token) {
      const decodedToken = jwtDecode(token);
      setDecoded(decodedToken);
    }
  };

  const fetchAssets = async () => {
    try {
      const data = await getAssets();
      if (data && Array.isArray(data.$values)) {
        setAssets(data.$values);
      } else {
        logAction("Fetched assets data is not an array", "error");
      }
    } catch (error) {
      console.error("Error fetching assets:", error);
      logAction("Error fetching assets", "error");
    }
  };

  const fetchCategories = async () => {
    try {
      const data = await getCategories();
      if (data && Array.isArray(data.$values)) {
        setCategories(data.$values);
      } else {
        logAction("Fetched categories data is not an array", "error");
      }
    } catch (error) {
      console.error("Error fetching categories:", error);
      logAction("Error fetching categories", "error");
    }
  };

  const fetchSubCategories = async (categoryId) => {
    try {
      console.log("Fetching subcategories for categoryId:", categoryId); 
      const response = await getSubCategories(categoryId);
      console.log("Received response:", response); 
      if (response && response.$values) {
        setSubCategories(response.$values);
        logAction("Fetched subcategories successfully", "success");
        console.log("Subcategories set in state:", response.$values); 
      } else {
        setSubCategories([]);
        logAction("No subcategories found for the selected category", "info");
        console.log("No subcategories found"); 
      }
    } catch (error) {
      console.error("Error fetching subcategories:", error);
      logAction("Error fetching subcategories", "error");
    }
  };

  const handleCategoryChange = (e) => {
    const selectedCategoryId = e.target.value;
    setNewAsset((prev) => ({ ...prev, categoryId: selectedCategoryId }));
    fetchSubCategories(selectedCategoryId); // Fetch subcategories for the selected category
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewAsset((prev) => ({ ...prev, [name]: value }));
  };

  const toggleFormVisibility = () => {
    setShowCreateAssetForm((prev) => !prev);
  };

  const logAction = (message, status) => {
    const newLog = {
      message: message,
      status: status,
      timestamp: new Date().toISOString(),
    };
    setActionLogs((prevLogs) => [newLog, ...prevLogs]);
  };

  const handleCreateAsset = async () => {
    if (!validateForm()) return;
    try {
      setLoading(true);
      const newAssetData = {
        ...newAsset,
        assetId: newAsset.assetId ? parseInt(newAsset.assetId, 10) : undefined, 
      };
      const createdAsset = await createAsset(newAssetData);
      setAssets((prevAssets) => [...prevAssets, createdAsset]);
      logAction("Created asset successfully", "success");
      resetForm();
    } catch (error) {
      console.error("Error creating asset:", error);
      logAction("Error creating asset", "error");
    } finally {
      setLoading(false);
    }
  };
  

  const handleUpdateAsset = (assetId) => {
    const assetToUpdate = assets.find((asset) => asset.assetId === assetId);
    if (assetToUpdate) {
      setNewAsset({
        assetId: assetToUpdate.assetId,
        assetName: assetToUpdate.assetName,
        categoryId: assetToUpdate.categoryId,
        subCategoryId: assetToUpdate.subCategoryId,
        value: assetToUpdate.value,
        manufacturingDate: assetToUpdate.manufacturingDate,
        location: assetToUpdate.location,
      });
      setShowCreateAssetForm(true);
    }
  };

  const sendUpdateRequest = async () => {
    if (!validateForm()) return;
    try {
      setLoading(true);
      const updatedAsset = {
        assetId: newAsset.assetId,
        assetName: newAsset.assetName,
        categoryId: newAsset.categoryId,
        subCategoryId: newAsset.subCategoryId,
        value: newAsset.value,
        location: newAsset.location,
        manufacturingDate: newAsset.manufacturingDate,
      };
      const response = await updateAsset(updatedAsset.assetId, updatedAsset);
      if (response) {
        setAssets((prevAssets) =>
          prevAssets.map((asset) =>
            asset.assetId === updatedAsset.assetId ? response : asset
          )
        );
        logAction("Updated asset successfully", "success");
        setShowCreateAssetForm(false); // Hide the form after update
      } else {
        throw new Error("Error updating asset");
      }
    } catch (error) {
      console.error("Error during asset update:", error);
      logAction("Error updating asset", "error");
      toast.error("Failed to update the asset.");
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteAsset = async (assetId) => {
    if (!window.confirm("Are you sure you want to delete this asset?")) return;
    setLoading(true);
    try {
      await deleteAsset(assetId);
      setAssets((prevAssets) => prevAssets.filter((asset) => asset.assetId !== assetId));
      logAction("Deleted asset successfully", "success");
    } catch (error) {
      console.error("Error deleting asset:", error);
      logAction("Error deleting asset", "error");
    } finally {
      setLoading(false);
    }
  };

  const validateForm = () => {
    if (!newAsset.assetName.trim()) {
      toast.error("Asset Name is required!");
      return false;
    }
    if (!newAsset.categoryId) {
      toast.error("Category is required!");
      return false;
    }
    if (!newAsset.subCategoryId) {
      toast.error("Subcategory is required!");
      return false;
    }
    if (!newAsset.location.trim()) {
      toast.error("Location is required!");
      return false;
    }
    if (isNaN(newAsset.value) || newAsset.value <= 0) {
      toast.error("Value must be a valid number greater than zero!");
      return false;
    }
    return true;
  };
  

  const resetForm = () => {
    setNewAsset({
      assetId: "",
      assetName: "",
      categoryId: "",
      subCategoryId: "",
      value: 0,
      manufacturingDate: "",
      location: "",
    });
    setSubCategories([]);
  };

  return (
    <div style={styles.container}>
      <button style={styles.backButton} onClick={() => navigate(-1)}>
        Back
      </button>
      {decoded?.role === "Admin" && (
        <button onClick={toggleFormVisibility} style={styles.toggleButton}>
          {showCreateAssetForm ? "Hide Asset Creation Form" : "Show Asset Creation Form"}
        </button>
      )}

      {showCreateAssetForm && (
        <section style={styles.formSection}>
          <h2 style={styles.heading}>Create New Asset</h2>
          <div style={styles.formGroup}>
            <label style={styles.label}>Asset Name</label>
            <input
              type="text"
              name="assetName"
              value={newAsset.assetName}
              onChange={handleInputChange}
              placeholder="Asset Name"
              style={styles.input}
            />
          </div>
          <div style={styles.formGroup}>
            <label style={styles.label}>Category</label>
            <select
              name="categoryId"
              value={newAsset.categoryId}
              onChange={handleCategoryChange}
              style={styles.input}
            >
              <option value="">Select Category</option>
              {categories.map((category) => (
                <option key={category.categoryId} value={category.categoryId}>
                  {category.categoryName}
                </option>
              ))}
            </select>
          </div>
          <div style={styles.formGroup}>
            <label style={styles.label}>Subcategory</label>
            <select
              name="subCategoryId"
              value={newAsset.subCategoryId}
              onChange={handleInputChange}
              style={styles.input}
              disabled={!subCategories.length}
            >
              <option value="">Select Subcategory</option>
              {subCategories.map((sub) => (
                <option key={sub.subCategoryId} value={sub.subCategoryId}>
                  {sub.subCategoryName}
                </option>
              ))}
            </select>
          </div>
          <div style={styles.formGroup}>
            <label style={styles.label}>Cost</label>
            <input
              type="number"
              name="value"
              value={newAsset.value}
              onChange={handleInputChange}
              placeholder="Cost"
              style={styles.input}
              step="0.01"
            />
          </div>
          <div style={styles.formGroup}>
            <label style={styles.label}>Manufacturing Date</label>
            <input
              type="date"
              name="manufacturingDate"
              value={newAsset.manufacturingDate}
              onChange={handleInputChange}
              style={styles.input}
            />
          </div>
          <div style={styles.formGroup}>
            <label style={styles.label}>Location</label>
            <input
              type="text"
              name="location"
              value={newAsset.location}
              onChange={handleInputChange}
              placeholder="Location"
              style={styles.input}
            />
          </div>
          <button
            onClick={newAsset.assetId ? sendUpdateRequest : handleCreateAsset}
            style={styles.button}
            disabled={loading}
          >
            {loading ? (newAsset.assetId ? "Updating..." : "Creating...") : (newAsset.assetId ? "Update Asset" : "Create Asset")}
          </button>
        </section>
      )}

      <section style={styles.listSection}>
        <h2 style={styles.heading}>Assets</h2>
        {assets.length > 0 ? (
          <table style={styles.table}>
            <thead>
              <tr>
                <th style={styles.tableHeader}>Asset Name</th>
                <th style={styles.tableHeader}>Category</th>
                <th style={styles.tableHeader}>Subcategory</th>
                <th style={styles.tableHeader}>Cost</th>
                <th style={styles.tableHeader}>Manufacturing Date</th>
                <th style={styles.tableHeader}>Location</th>
                {decoded?.role === "Admin" && <th style={styles.tableHeader}>Actions</th>}
              </tr>
            </thead>
            <tbody>
              {assets.map((asset) => {
                const subCategoryName = subCategories.find((sub) => sub.subCategoryId === asset.subCategoryId)?.subCategoryName || "N/A";
                return (
                  <tr key={asset.assetId} style={styles.tableRow}>
                    <td style={styles.tableCell}>{asset.assetName || "N/A"}</td>
                    <td style={styles.tableCell}>{asset.category?.categoryName || "N/A"}</td>
                    <td style={styles.tableCell}>{subCategoryName}</td>
                    <td style={styles.tableCell}>{asset.value !== undefined ? asset.value.toFixed(2) : "N/A"}</td>
                    <td style={styles.tableCell}>
                      {asset.manufacturingDate ? new Intl.DateTimeFormat('en-GB').format(new Date(asset.manufacturingDate)) : "N/A"}
                    </td>
                    <td style={styles.tableCell}>{asset.location || "N/A"}</td>
                    
                    {decoded?.role === "Admin" ? (
                      <td style={styles.tableCell}>
                        <button onClick={() => handleUpdateAsset(asset.assetId)} style={styles.actionButton}>
                          Update
                        </button>
                        <button onClick={() => handleDeleteAsset(asset.assetId)} style={styles.actionButton}>
                          Delete
                        </button>
                      </td>
                    ) : (
                      <td style={styles.tableCell}>
                        View Only
                      </td>
                    )}
                  </tr>
                );
              })}
            </tbody>
          </table>
        ) : (
          <p>No assets available.</p>
        )}
      </section>

      <section style={styles.logSection}>
        <h2 style={styles.heading}>Action Logs</h2>
        <ul>
          {actionLogs.map((log, index) => (
            <li key={index} style={styles.logItem}>
              <strong>{log.timestamp}:</strong> {log.message} [{log.status}]
            </li>
          ))}
        </ul>
      </section>
    </div>
  );
};

 
const styles = {
  container: {
    padding: "20px",
    fontFamily: "'Arial', sans-serif",
    backgroundColor: "#f0f8ff", // Light Alice Blue
    display: "flex",
    flexDirection: "column", // Stack elements vertically
    alignItems: "center", // Center the elements horizontally
    width: "100%", // Ensure full width of the container
    color: "#000", // Black text color for the container
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
  heading: {
    textAlign: "center",
    marginBottom: "20px",
    color: "#000", // Black text color for headings
    fontSize: "24px", // Larger font size for headings
    fontWeight: "bold",
  },
  formWrapper: {
    display: "flex",
    flexDirection: "column", // Stack elements vertically
    alignItems: "center", // Center the elements horizontally
    maxWidth: "1200px", // Increase the max-width to make the form section wider
    width: "100%", // Ensure it is responsive
    margin: "0 auto", // Center the form horizontally
  },
  formSection: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    marginBottom: "20px",
    backgroundColor: "#e0f7fa", // Light Cyan
    padding: "20px", // Add some padding for spacing
    borderRadius: "8px",
    width: "100%", // Ensure the form section takes full width of the container
    color: "#000", // Black text color
    boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)", // Add subtle shadow for depth
  },
  formGroup: {
    marginBottom: "20px",
    width: "100%",
    maxWidth: "600px", // Match the width of input fields
    textAlign: "left", // Align text to the left
    color: "#000", // Black text color
  },
  input: {
    width: "100%",
    padding: "12px",
    margin: "8px 0",
    border: "1px solid #ccc",
    borderRadius: "4px",
    backgroundColor: "#ffffff", // White background for inputs
    color: "#000", // Black text color for input
    fontSize: "14px",
  },
  label: {
    display: "block",
    marginBottom: "8px",
    fontSize: "14px",
    fontWeight: "bold",
    color: "#000", // Black text color for labels
  },
  buttonContainer: {
    display: "flex",
    justifyContent: "center", // Center the button horizontally
    width: "100%",
  },
  submitButton: {
    backgroundColor: "#4CAF50", // Green color
    color: "#fff", // White text color for button
    padding: "12px 24px",
    border: "none",
    cursor: "pointer",
    borderRadius: "4px",
    maxWidth: "600px", // Match the button width with the input fields
    width: "100%",
    fontSize: "16px",
    fontWeight: "bold",
  },
  toggleButton: {
    backgroundColor: "#007bff", // Blue button for actions
    color: "#fff", // White text color
    padding: "12px 24px",
    marginBottom: "20px",
    border: "none",
    cursor: "pointer",
    borderRadius: "4px",
    fontSize: "16px",
    fontWeight: "bold",
  },
  table: {
    width: "100%", // Full width of the container
    marginTop: "20px",
    borderCollapse: "collapse",
    color: "#000", // Black text color for table
  },
  tableHeader: {
    backgroundColor: "#333", // Darker background color for header
    color: "#fff", // White text color for header
    textAlign: "left",
    padding: "12px",
    fontWeight: "bold",
  },
  tableCell: {
    padding: "12px",
    border: "1px solid #ddd",
    backgroundColor: "#f9f9f9", // Lighter background color for cells
    color: "#000", // Black text color for cells
  },
  tableRow: {
    textAlign: "left",
    color: "#000", // Black text color for rows
  },
  actionButton: {
    backgroundColor: "#007bff", // Blue button for actions
    color: "#fff", // White text color for buttons
    padding: "8px 16px",
    margin: "5px",
    border: "none",
    cursor: "pointer",
    borderRadius: "4px",
    fontSize: "14px",
    fontFamily: "'Arial', sans-serif",
    fontWeight: "bold",
  },
  logSection: {
    width: "100%",
    marginTop: "40px",
    backgroundColor: "#f9f9f9", // Light background for logs
    padding: "20px",
    borderRadius: "8px",
    boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
  },
  logItem: {
    padding: "8px",
    borderBottom: "1px solid #ddd",
    color: "#333",
  },
  logHeading: {
    fontSize: "20px",
    fontWeight: "bold",
    marginBottom: "10px",
    color: "#000",
  },
};


export default Asset;