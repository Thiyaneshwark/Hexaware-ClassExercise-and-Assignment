import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from '../contexts/AuthContext';
import { getCategories } from "../services/CategoryService";
import { getSubCategories } from "../services/SubCategoryService";
import { getAssets } from "../services/AssetService";
import { getAssetRequests } from "../services/AssetRequestService";
import { getReturnRequests } from "../services/ReturnRequestService";
import { getServiceRequests } from "../services/ServiceRequestService";
import HeaderFooter from "../Components/HeaderFooter";
import './Emp.css';
import { FaCog } from 'react-icons/fa';

const EmpDashboard = () => {
  const navigate = useNavigate();
  const { logout, authState } = useAuth();
  const [data, setData] = useState([]);
  const [section, setSection] = useState("");
  const [showSettings, setShowSettings] = useState(false);
  const [loggedTime, setLoggedTime] = useState('');

  useEffect(() => {
    const now = new Date();
    setLoggedTime(now.toLocaleTimeString());
  }, []);

  useEffect(() => {
    console.log('AuthState:', authState);  // Log the authState to see its content
  }, [authState]);

  const handleFetchData = async (fetchFunction, sectionName, route) => {
    try {
      const result = await fetchFunction();
      console.log(`Fetched ${sectionName} data:`, result);
      setData(result || []);
      setSection(sectionName);
      navigate(route);
    } catch (error) {
      console.error(`Error fetching ${sectionName}:`, error.message || error);
      setData([]);
      setSection(`${sectionName} (Error fetching data)`);
    }
  };

  const handleSettingsClick = () => {
    setShowSettings(!showSettings);
  };

  return (
    <div className="container">
      {authState.user && (
        <HeaderFooter userName={authState.user.sub} userRole={authState.user.role} loggedTime={loggedTime} />
      )}
      <FaCog className="settingsIcon" onClick={handleSettingsClick} />
      {showSettings && (
        <div className="settingsMenu">
          <button onClick={() => navigate('/change-password')}>Change Password</button>
          <button className="signout" onClick={logout}>Sign Out</button>
        </div>
      )}
      <div className="overlay"></div>
      <h1 className="heading">Employee Dashboard</h1>
      <div className="buttonContainer">
        <button className="button" onClick={() => handleFetchData(getCategories, "Categories", "/categories")}>
          Categories
        </button>
        <button className="button" onClick={() => handleFetchData(getSubCategories, "Subcategories", "/subcategories")}>
          Subcategories
        </button>
        <button className="button" onClick={() => handleFetchData(getAssets, "Assets", "/assets")}>
          Assets
        </button>
        <button className="button" onClick={() => handleFetchData(getAssetRequests, "Asset Requests", "/asset-requests")}>
          Asset Requests
        </button>
        <button className="button" onClick={() => handleFetchData(getReturnRequests, "Return Request", "/return-requests")}>
          Return Request
        </button>
        <button className="button" onClick={() => handleFetchData(getServiceRequests, "Service Request", "/service-requests")}>
          Service Request
        </button>
      </div>

      <div className="dataSection">
        <h2>{section}</h2>
        {data && data.length > 0 ? (
          <ul>
            {data.map((item, index) => (
              <li key={index}>{JSON.stringify(item)}</li>
            ))}
          </ul>
        ) : (
          <p>No {section.toLowerCase()} data available.</p>
        )}
      </div>
    </div>
  );
};

export default EmpDashboard;
