import React, { useState, useEffect } from "react";
import Header from "./Header";

const Profile = () => {
  const [userDetails, setUserDetails] = useState(null);

  useEffect(() => {
    // Fetch the logged-in user's details
    const fetchUserDetails = async () => {
      try {
        const response = await fetch("https://localhost:7144/api/users/me"); 
        const data = await response.json();
        setUserDetails(data);
      } catch (error) {
        console.error("Error fetching user details:", error);
      }
    };

    fetchUserDetails();
  }, []);

  if (!userDetails) {
    return (
      <div>
        <Header />
        <div className="p-6">Loading user details...</div>
      </div>
    );
  }

  return (
    <div>
      <Header />
      <div className="p-6">
        <h2 className="text-2xl font-bold">User Profile</h2>
        <div className="mt-4">
          <p><strong>Name:</strong> {userDetails.name}</p>
          <p><strong>Email:</strong> {userDetails.email}</p>
          <p><strong>Role:</strong> {userDetails.role}</p>
          <p><strong>Phone:</strong> {userDetails.phone}</p>
          <p><strong>Department:</strong> {userDetails.department}</p>
          <p><strong>Join Date:</strong> {new Date(userDetails.joinDate).toLocaleDateString()}</p>
          <p><strong>Address:</strong> {userDetails.address}</p>
        </div>
        {userDetails.role === "Admin" && (
          <div className="mt-6">
            <h3 className="text-xl font-semibold">Admin Privileges</h3>
            <ul className="list-disc pl-6 mt-2">
              <li>Manage Users</li>
              <li>Approve Requests</li>
              <li>View Reports</li>
            </ul>
          </div>
        )}
      </div>
    </div>
  );
};

export default Profile;
